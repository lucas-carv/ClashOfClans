using ClashOfClans.API.Data;
using ClashOfClans.API.Model;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace ClashOfClans.API.BackgroundServices;

public class DetectarMembrosInativosEmGuerrasJob : IJob
{
    public readonly ClashOfClansContext _context;

    public DetectarMembrosInativosEmGuerrasJob(ClashOfClansContext context)
    {
        _context = context;
    }

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
            var ultimasCincoGuerras = await _context.Guerras
               .Include(g => g.ClanEmGuerra).ThenInclude(a => a.MembrosEmGuerra).ThenInclude(a => a.Ataques)
               .Where(g => g.Status == "WarEnded" && g.ClanEmGuerra.Tag == clanTag)
               .OrderByDescending(g => g.FimGuerra)
               .Take(5)
               .ToListAsync(cancellationToken);

            if (ultimasCincoGuerras.Count < 2)
                return;

            var membrosAtivos = await _context.Clans
                .Where(c => c.Tag == clanTag)
                .SelectMany(c => c.Membros) // "achata" a lista de membros
                .Where(m => m.Situacao == SituacaoMembro.Ativo)
                .ToListAsync(cancellationToken);


            var tagsMembrosQueParticiparam = ultimasCincoGuerras
                .SelectMany(g => g.ClanEmGuerra.MembrosEmGuerra)
                .Select(mg => mg.Tag) // ajuste se o nome for diferente
                .Distinct()
                .ToHashSet(); // HashSet para Contains mais rápido

            var membrosAtivosNaoParticipantes = membrosAtivos
                .Where(m => !tagsMembrosQueParticiparam.Contains(m.Tag)) // ajuste o nome da prop
                .ToList();

            
        }
    }
}