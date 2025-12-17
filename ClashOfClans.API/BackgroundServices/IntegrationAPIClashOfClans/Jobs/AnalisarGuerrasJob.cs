using ClashOfClans.API.Data;
using ClashOfClans.API.Model;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Jobs;

[DisallowConcurrentExecution]
public class AnalisarGuerrasJob(ClashOfClansContext context, ILogger<AnalisarGuerrasJob> logger) : IJob
{
    private readonly ClashOfClansContext _context = context;
    private readonly ILogger<AnalisarGuerrasJob> _logger = logger;

    public async Task Execute(IJobExecutionContext context)
    {
        CancellationToken cancellationToken = context.CancellationToken;

        List<string> clansTags = await _context.Guerras
            .AsNoTracking()
            .Where(g => g.Status == "WarEnded")
            .Select(g => g.ClanEmGuerra.Tag)
            .Distinct()
            .ToListAsync(cancellationToken);

        foreach (var clanTag in clansTags)
        {
            IEnumerable<MembroGuerraResumo> membros = await ObterAtaquesDeMembros(clanTag, cancellationToken);
            foreach (var membro in membros)
            {
                MembroGuerraResumo? membroExiste = _context.MembrosGuerrasResumo.FirstOrDefault(m => m.MembroTag == membro.MembroTag && m.ClanTag == clanTag);
                if (membroExiste is null)
                {
                    _context.Add(membro);
                    continue;
                }
                membroExiste.AtualizarQuantidadeAtaques(membro.QuantidadeAtaques);
                membroExiste.GuerrasParticipadasSeq = membro.GuerrasParticipadasSeq;
            }
            await _context.Commit(cancellationToken);
        }
    }

    public async Task<List<MembroGuerraResumo>> ObterAtaquesDeMembros(string clanTag, CancellationToken cancellationToken)
    {
        var ultimasDuasGuerras = await _context.Guerras
            .AsNoTracking()
            .Where(g => g.Status == "WarEnded" && g.ClanEmGuerra.Tag == clanTag)
            .OrderByDescending(g => g.FimGuerra)
            .Take(2)
            .Select(g => new { g.Id })
            .ToListAsync(cancellationToken);

        if (ultimasDuasGuerras.Count < 2)
            return [];

        IEnumerable<int> guerrasIds = ultimasDuasGuerras.Select(g => g.Id);

        var membrosDaGuerra = await _context.MembrosEmGuerra
            .AsNoTracking()
            .Join(_context.ClansEmGuerra,
                m => m.ClanEmGuerraId,
                c => c.Id,
                (m, c) => new { Membro = m, Clan = c })
            .Join(_context.Guerras,
                mc => mc.Clan.GuerraId,
                g => g.Id,
                (mc, g) => new { mc.Membro, mc.Clan, Guerra = g })
            .Where(x => guerrasIds.Contains(x.Guerra.Id) && x.Membro.FoiRemovido != null)
            .Select(x => new
            {
                GuerraId = x.Guerra.Id,
                x.Membro.Tag,
                x.Membro.Nome,
                QuantidadeAtaques = x.Membro.Ataques.Count
            })
            .ToListAsync(cancellationToken);

        List<MembroGuerraResumo> membros = [.. membrosDaGuerra
            .GroupBy(m => m.Tag)
            .Select(g => new MembroGuerraResumo(clanTag, g.Key, g.First().Nome)
            {
                QuantidadeAtaques = g.Sum(x => x.QuantidadeAtaques),
                GuerrasParticipadasSeq = g.Select(x => x.GuerraId).Distinct().Count()
            })];

        return membros;
    }
}

public static class AnalisarGuerrasJobConfiguration
{
    public static void AddAnalisarGuerras(this IServiceCollectionQuartzConfigurator configurator)
    {
        JobKey jobKey = new(nameof(AnalisarGuerrasJob));
        configurator.AddJob<AnalisarGuerrasJob>(opts => opts.WithIdentity(jobKey));

        configurator.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(AnalisarGuerrasJob)}-trigger")
            .WithSimpleSchedule(x => x.WithIntervalInMinutes(5)
            .RepeatForever())
            );
    }
}
