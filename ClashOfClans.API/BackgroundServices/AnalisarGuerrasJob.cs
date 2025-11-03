using ClashOfClans.API.Data;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System;

namespace ClashOfClans.API.BackgroundServices
{
    public class AnalisarGuerrasJob : IJob
    {
        private readonly IServiceProvider _sp;
        private readonly ILogger<AnalisarGuerrasJob> _log;

        public AnalisarGuerrasJob(IServiceProvider sp, ILogger<AnalisarGuerrasJob> log)
        {
            _sp = sp;
            _log = log;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using var scope = _sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ClashOfClansContext>();
            var ct = context.CancellationToken;

            // 1) Guerras finalizadas nas últimas 24h (ajuste conforme sua janela)
            var limite = DateTime.UtcNow.AddDays(-1);
            var guerras = await db.Guerras
                .Where(g => g.Status == "Finalizada" && g.FimGuerra >= limite)
                .Select(g => new { g.Id, ClanTag = g.ClanEmGuerra.Tag, g.FimGuerra })
                .OrderByDescending(g => g.FimGuerra)
                .ToListAsync(ct);

            foreach (var guerra in guerras)
            {
                var membrosSemAtaque = await MembrosSemAtaqueNaGuerraAsync(guerra.Id, db, ct);

                if (membrosSemAtaque.Count == 0)
                {
                    _log.LogInformation("Guerra {GuerraId} ({Clan}): todos atacaram.", guerra.Id, guerra.ClanTag);
                    continue;
                }

                // 3) Faça sua ação: persistir sugestão/alertar/etc.
                foreach (var m in membrosSemAtaque)
                {
                    _log.LogInformation("Guerra {GuerraId} ({Clan}) -> sem ataque: {Tag} - {Nome}",
                        guerra.Id, guerra.ClanTag, m.Tag, m.Nome);

                    // Exemplo: inserir em uma tabela de sugestões (se tiver)
                    // await db.SugerirRemocao.AddAsync(new SugerirRemocao { TagJogador = m.Tag, GuerraId = guerra.Id, Motivo = "Sem ataque" }, ct);
                }

                // Se persistiu algo acima:
                // await db.SaveChangesAsync(ct);
            }
        }
        private static async Task<List<(string Tag, string Nome)>> MembrosSemAtaqueNaGuerraAsync(
        int guerraId, ClashOfClansContext db, CancellationToken ct)
        {
            var membros = await db.Guerras
                .AsNoTracking()
                .Where(g => g.Id == guerraId)
                .SelectMany(g => g.ClanEmGuerra.Membros)
                .Where(m => !m.Ataques.Any()) // coleção vazia = nenhum ataque nessa guerra
                .Select(m => new { m.Tag, m.Nome })
                .ToListAsync(ct);

            return membros.Select(x => (x.Tag, x.Nome)).ToList();
        }

        public async Task<IReadOnlyList<string>> MembrosSemAtacarEmDuasUltimasGuerrasAsync(string clanTag, ClashOfClansContext context, CancellationToken ct)
        {
            // Pega as 2 últimas guerras finalizadas do clã
            var duasUltimas = await context.Guerras
                .AsNoTracking()
                .Where(g => g.Status == "Finalizada" && g.ClanEmGuerra.Tag == clanTag)
                .OrderByDescending(g => g.FimGuerra)
                .Select(g => new { g.Id })
                .Take(2)
                .ToListAsync(ct);

            if (duasUltimas.Count < 2) return [];

            var ids = duasUltimas.Select(x => x.Id).ToArray();

            // Para essas guerras, projeta (GuerraId, TagJogador, TemAtaque)
            var marca = await context.Guerras
                .AsNoTracking()
                .Where(g => ids.Contains(g.Id))
                .SelectMany(g => g.ClanEmGuerra.Membros
                    .Select(m => new {
                        GuerraId = g.Id,
                        Tag = m.Tag,
                        TemAtaque = m.Ataques.Any()
                    }))
                .ToListAsync(ct);

            // Agrupa por jogador, verifica presença nas 2 e zero ataques nas 2
            var candidatos = marca
                .GroupBy(x => x.Tag)
                .Where(g => g.Select(x => x.GuerraId).Distinct().Count() == 2
                         && g.All(x => x.TemAtaque == false))
                .Select(g => g.Key)
                .ToList();

            return candidatos;
        }
    }
}
