using ClashOfClans.API.Data;
using ClashOfClans.API.Model;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System;

namespace ClashOfClans.API.BackgroundServices
{
    public class AnalisarGuerrasJob : IJob
    {
        private readonly ClashOfClansContext _context;
        private readonly ILogger<AnalisarGuerrasJob> _logger;

        public AnalisarGuerrasJob(ClashOfClansContext context, ILogger<AnalisarGuerrasJob> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var ct = context.CancellationToken;

            // Busque todos os clãs que tiveram guerra finalizada recentemente (ex.: 48h)
            var limite = DateTime.UtcNow;

            var clans = await _context.Guerras
                .AsNoTracking()
                .Where(g => g.Status == "notInWar" && g.FimGuerra <= limite)
                .Select(g => g.ClanEmGuerra.Tag)
                .Distinct()
                .ToListAsync(ct);

            foreach (var clanTag in clans)
            {
                await AtualizarResumoParaUltimaGuerraDoClanAsync(clanTag, _context, ct);
                _logger.LogInformation("Resumo atualizado para o clã {Clan}.", clanTag);
            }
        }

        private static async Task AtualizarResumoParaUltimaGuerraDoClanAsync(string clanTag, ClashOfClansContext db, CancellationToken ct)
        {
            // Pega duas últimas guerras finalizadas do clã
            var duas = await db.Guerras
                .AsNoTracking()
                .Where(g => g.Status == "notInWar" && g.ClanEmGuerra.Tag == clanTag)
                .OrderByDescending(g => g.FimGuerra)
                .Select(g => new { g.Id })
                .Take(2)
                .ToListAsync(ct);

            if (duas.Count == 0) return;

            var g1Id = duas[0].Id;               // última (mais recente)
            var g2Id = duas.Count > 1 ? duas[1].Id : (int?)null; // penúltima (se houver)

            // Membros e ataques da última guerra (g1)
            var membrosG1 = await db.Guerras
                .AsNoTracking()
                .Where(g => g.Id == g1Id)
                .SelectMany(g => g.ClanEmGuerra.Membros
                    .Select(m => new
                    {
                        m.Tag,
                        m.Nome,
                        Ataques = m.Ataques.Count()
                    }))
                .ToListAsync(ct);

            var tagsG1 = membrosG1.Select(x => x.Tag).ToHashSet();

            // Conjunto de tags que também participaram da penúltima (g2)
            HashSet<string> tagsG2 = new();
            if (g2Id.HasValue)
            {
                tagsG2 = (await db.Guerras
                    .AsNoTracking()
                    .Where(g => g.Id == g2Id.Value)
                    .SelectMany(g => g.ClanEmGuerra.Membros.Select(m => m.Tag))
                    .ToListAsync(ct))
                    .ToHashSet();
            }

            // Carrega resumos atuais do clã (para upsert e para achar “ausentes em g1”)
            var resumosAtuais = await db.Set<MembroGuerraResumo>()
                .Where(r => r.ClanTag == clanTag)
                .ToListAsync(ct);

            var porTag = resumosAtuais.ToDictionary(r => r.Tag, r => r);

            // 1) Upsert para quem está na última guerra (g1)
            foreach (var m in membrosG1)
            {
                int guerrasSeq = 1 + (tagsG2.Contains(m.Tag) ? 1 : 0);

                if (!porTag.TryGetValue(m.Tag, out var resumo))
                {
                    resumo = new MembroGuerraResumo
                    {
                        ClanTag = clanTag,
                        Tag = m.Tag,
                        Nome = m.Nome,
                        GuerrasParticipadasSeq = guerrasSeq,
                        QuantidadeAtaques = m.Ataques,
                        UltimaGuerraId = g1Id
                    };
                    db.Add(resumo);
                    porTag[m.Tag] = resumo;
                }
                else
                {
                    resumo.Nome = m.Nome; // mantém atualizado
                    resumo.GuerrasParticipadasSeq = guerrasSeq;  // 1 ou 2
                    resumo.QuantidadeAtaques = m.Ataques;        // só da g1
                    resumo.UltimaGuerraId = g1Id;
                    db.Update(resumo);
                }
            }

            // 2) Zerar quem NÃO participou da última guerra (g1)
            foreach (var resumo in resumosAtuais.Where(r => !tagsG1.Contains(r.Tag)))
            {
                resumo.GuerrasParticipadasSeq = 0;
                resumo.QuantidadeAtaques = 0;
                resumo.UltimaGuerraId = g1Id; // ancora o reset na última guerra
                db.Update(resumo);
            }

            await db.SaveChangesAsync(ct);
        
        }
    }
}
