using ClashOfClans.API.Data;
using ClashOfClans.API.Model;
using ClashOfClans.API.Model.Clans;
using ClashOfClans.API.Model.Guerras;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Jobs;

public class DetectarMembrosInativosEmGuerrasJob(ClashOfClansContext context) : IJob
{
    private readonly ClashOfClansContext _context = context;

    public async Task Execute(IJobExecutionContext context)
    {
        CancellationToken cancellationToken = context.CancellationToken;

        var limite = DateTime.Now.AddDays(-2);

        List<string> clansTags = await _context.Guerras
            .AsNoTracking()
            .Where(g => g.Status == "WarEnded" && g.FimGuerra >= limite)
            .Select(g => g.ClanEmGuerra.Tag)
            .Distinct()
            .ToListAsync(cancellationToken);

        foreach (var clanTag in clansTags)
        {
            List<Guerra> ultimasCincoGuerras = await _context.Guerras
                .Include(g => g.ClanEmGuerra)
                    .ThenInclude(a => a.MembrosEmGuerra)
                        .ThenInclude(a => a.Ataques)
                .Where(g => g.Status == "WarEnded" && g.ClanEmGuerra.Tag == clanTag)
                .OrderByDescending(g => g.FimGuerra)
                .Take(5)
                .ToListAsync(cancellationToken);

            if (ultimasCincoGuerras.Count == 5)
                continue;

            DateTime dataLimiteEntrada = ultimasCincoGuerras
                .Min(g => g.InicioGuerra);

            List<Membro> membrosElegiveis = await _context.Clans
                .Where(c => c.Tag == clanTag)
                .SelectMany(c => c.Membros)
                .Where(m =>
                    m.Situacao == SituacaoMembro.Ativo &&
                    m.DataEntrada <= dataLimiteEntrada
                )
                .ToListAsync(cancellationToken);

            HashSet<string> tagsMembrosQueParticiparam = ultimasCincoGuerras
                .SelectMany(g => g.ClanEmGuerra.MembrosEmGuerra)
                .Select(mg => mg.Tag)
                .Distinct()
                .ToHashSet();

            List<Membro> membrosAtivosNaoParticipantes = membrosElegiveis
                .Where(m => !tagsMembrosQueParticiparam.Contains(m.Tag))
                .ToList();

            if (membrosAtivosNaoParticipantes.Count == 0)
                continue;

            List<MembroInativoGuerra> antigos = await _context.MembrosInativosGuerras
                .Where(x => x.ClanTag == clanTag)
                .ToListAsync(cancellationToken);

            if (antigos.Count > 0)
            {
                _context.MembrosInativosGuerras.RemoveRange(antigos);
            }

            var novosRegistros = membrosAtivosNaoParticipantes
                .Select(m => new MembroInativoGuerra(m.Tag, m.Nome, clanTag, DateTime.Now, m.DataEntrada)).ToList();

            _context.MembrosInativosGuerras.AddRange(novosRegistros);
            await _context.Commit(cancellationToken);
        }
    }
}
public static class DetectarMembrosInativosEmGuerrasJobConfiguration
{
    public static void AddDetectarMembrosInativosJob(this IServiceCollectionQuartzConfigurator configurator)
    {
        JobKey jobKey = new(nameof(DetectarMembrosInativosEmGuerrasJob));
        configurator.AddJob<DetectarMembrosInativosEmGuerrasJob>(opts => opts.WithIdentity(jobKey));

        configurator.AddTrigger(opts => opts
            .ForJob(jobKey)
            .WithIdentity($"{nameof(DetectarMembrosInativosEmGuerrasJob)}-trigger")
            .StartNow()
            .WithSimpleSchedule(x => x.WithIntervalInHours(2)
            .RepeatForever())
            );
    }
}
