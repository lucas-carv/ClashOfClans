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
            CancellationToken cancellationToken = context.CancellationToken;

            // Busca todos os clãs que tiveram guerras finalizadas recentemente
            var clans = await _context.Guerras
                .AsNoTracking()
                .Where(g => g.Status == "notInWar")
                .Select(g => g.ClanEmGuerra.Tag)
                .Distinct()
                .ToListAsync(cancellationToken);

            foreach (var clanTag in clans)
            {
                var membros = await ObterMembrosQueNaoAtacaramNasDuasUltimasGuerrasAsync(clanTag,  cancellationToken);

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

        public async Task<List<MembroEmGuerra>> ObterMembrosQueNaoAtacaramNasDuasUltimasGuerrasAsync(string clanTag, CancellationToken cancellationToken)
        {
            var ultimasDuasGuerras = await _context.Guerras
                .AsNoTracking()
                .Where(g => g.Status == "notInWar" && g.ClanEmGuerra.Tag == clanTag)
                .OrderByDescending(g => g.FimGuerra)
                .Take(2)
                .Select(g => new { g.Id })
                .ToListAsync(cancellationToken);

            if (ultimasDuasGuerras.Count < 2)
                return [];

            IEnumerable<int> guerrasIds = ultimasDuasGuerras.Select(g => g.Id);
            
            var membrosDaGuerra = await _context.Guerras
           .AsNoTracking()
           .Where(g => guerrasIds.Contains(g.Id))
           .SelectMany(g => g.ClanEmGuerra.Membros.Select(m => new
               {
                   GuerraId = g.Id,
                   m.Tag,
                   m.Nome,
                   QuantidadeAtaques = m.Ataques.Count
               }))
           .ToListAsync(cancellationToken);

            var membrosSemAtaques = membrosDaGuerra
                .GroupBy(m => m.Tag)
                .Where(g =>
                    g.Select(x => x.GuerraId).Distinct().Count() == 2 && 
                    g.All(x => x.QuantidadeAtaques == 0))               
                .Select(g => new MembroEmGuerra(g.Key, g.First().Nome))
                .ToList();

            return membrosSemAtaques;

        }
    }
}
