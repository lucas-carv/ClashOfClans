using ClashOfClans.API.Data;
using ClashOfClans.API.Model;
using ClashOfClans.API.Model.Guerras;
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

            // Busca todos os clãs que tiveram guerras finalizadas recentemente
            var clans = await _context.Guerras
                .AsNoTracking()
                .Where(g => g.Status == "notInWar")
                .Select(g => g.ClanEmGuerra.Tag)
                .Distinct()
                .ToListAsync(ct);

            foreach (var clanTag in clans)
            {
                var membros = await ObterMembrosQueNaoAtacaramNasDuasUltimasGuerrasAsync(clanTag, _context, ct);

                if (membros.Count == 0)
                {
                    _logger.LogInformation("Clã {Clan}: nenhum membro inelegível encontrado.", clanTag);
                    continue;
                }

                foreach (var m in membros)
                    _logger.LogInformation("Clã {Clan} → Membro {Tag} ({Nome}) não atacou em nenhuma das 2 últimas guerras.",
                        clanTag, m.Tag, m.Nome);
            }
        }

        public async Task<List<MembroEmGuerra>> ObterMembrosQueNaoAtacaramNasDuasUltimasGuerrasAsync(string clanTag, ClashOfClansContext db, CancellationToken ct)
        {
            // Pega duas últimas guerras finalizadas do clã

            var ultimasDuasGuerras = await db.Guerras
                .AsNoTracking()
                .Where(g => g.Status == "notInWar" && g.ClanEmGuerra.Tag == clanTag)
                .OrderByDescending(g => g.FimGuerra)
                .Take(2)
                .Select(g => new { g.Id })
                .ToListAsync(ct);

            if (ultimasDuasGuerras.Count < 2)
                return [];

            var ids = ultimasDuasGuerras.Select(g => g.Id).ToArray();
            // Membros e ataques da última guerra (g1)
            var membrosComAtaques = await db.Guerras
           .AsNoTracking()
           .Where(g => ids.Contains(g.Id))
           .SelectMany(g => g.ClanEmGuerra.Membros.Select(m => new
           {
               GuerraId = g.Id,
               m.Tag,
               m.Nome,
               QuantidadeAtaques = m.Ataques.Count()
           }))
           .ToListAsync(ct);

            // 3️⃣ Agrupa por jogador
            var membrosSemAtaqueNasDuas = membrosComAtaques
                .GroupBy(m => m.Tag)
                .Where(g =>
                    g.Select(x => x.GuerraId).Distinct().Count() == 2 && // participou nas 2
                    g.All(x => x.QuantidadeAtaques == 0))                // 0 ataques em ambas
                .Select(g => new MembroEmGuerra(g.Key, g.First().Nome))
                .ToList();

            return membrosSemAtaqueNasDuas;

        }
    }
}
