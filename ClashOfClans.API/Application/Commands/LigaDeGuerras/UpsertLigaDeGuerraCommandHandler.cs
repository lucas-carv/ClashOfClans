using ClashOfClans.API.Core.CommandResults;
using ClashOfClans.API.Data;
using ClashOfClans.API.DTOs.Guerras;
using ClashOfClans.API.Model.Guerras;
using ClashOfClans.API.Model.LigaDeClans;
using ClashOfClans.API.Services.Guerras;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Application.Commands.LigaDeGuerras
{
    public record LigaDeGuerraRequest(string ClanTag, string Status, string Temporada, List<ClanEmGuerraDTO> Clans, List<LigaGuerraRodadaRequest> Rodadas) : IRequest<CommandResult<bool>>;

    public class UpsertLigaDeGuerraCommandHandler(ClashOfClansContext context, GuerraService guerraService) : IRequestHandler<LigaDeGuerraRequest, CommandResult<bool>>
    {
        public async Task<CommandResult<bool>> Handle(LigaDeGuerraRequest request, CancellationToken cancellationToken)
        {
            bool clanExiste = await context.Clans.AnyAsync(c => c.Tag == request.ClanTag, cancellationToken: cancellationToken);
            if (!clanExiste)
                return ValidationErrors.Clan.ClanNaoExiste;

            LigaDeGuerra? ligaDeGuerra = await context.LigaDeGuerras
                .Include(l => l.Rodadas!)
                .Include(l => l.Clans!).ThenInclude(c => c.Membros)
                .FirstOrDefaultAsync(l => l.ClanTag == request.ClanTag && l.Temporada == request.Temporada, cancellationToken: cancellationToken);

            if (ligaDeGuerra is null)
            {
                ligaDeGuerra = new(request.ClanTag, request.Status, request.Temporada);

                foreach (var clan in request.Clans)
                {
                    LigaGuerraClan ligaGuerraClan = ligaDeGuerra.AdicionarClan(clan.Tag, clan.Nome, clan.ClanLevel);

                    foreach (var membro in clan.Membros)
                    {
                        ligaGuerraClan.AdicionarMembro(membro.Tag, membro.Nome, membro.CentroVilaLevel);
                    }
                }
                foreach (var rodada in request.Rodadas)
                {
                    ligaDeGuerra.AdicionarRodada(rodada.Status, rodada.Dia, rodada.GuerraTag, rodada.ClanTag, rodada.ClanTagOponente, rodada.InicioGuerra, rodada.FimGuerra);
                }
                context.Add(ligaDeGuerra);
            }
            else
            {
                foreach (var clan in request.Clans)
                {
                    LigaGuerraClan ligaGuerraClan = ligaDeGuerra.AdicionarClan(clan.Tag, clan.Nome, clan.ClanLevel);
                    ligaGuerraClan.AtualizarLevelClan(clan.ClanLevel);

                    foreach (var membro in clan.Membros)
                    {
                        ligaGuerraClan.AdicionarMembro(membro.Tag, membro.Nome, membro.CentroVilaLevel);
                    }
                    foreach (var rodada in request.Rodadas)
                    {
                        ligaDeGuerra.AdicionarRodada(rodada.Status, rodada.Dia, rodada.GuerraTag, rodada.ClanTag, rodada.ClanTagOponente, rodada.InicioGuerra, rodada.FimGuerra);
                    }
                }
            }
            foreach (var rodada in request.Rodadas)
            {
                Guerra? guerra = await context.Guerras
                        .Include(g => g.ClanEmGuerra)
                            .ThenInclude(c => c.MembrosEmGuerra)
                                .ThenInclude(m => m.Ataques)
                        .FirstOrDefaultAsync(g => g.ClanEmGuerra.Tag == rodada.ClanTag && g.GuerraTag == rodada.GuerraTag,
                                             cancellationToken);
                ClanEmGuerraDTO clan = request.Clans.First(c => c.Tag == rodada.ClanTag);

                if (guerra is null)
                {
                    guerra = guerraService.CriarGuerra(rodada.Status, rodada.InicioGuerra, rodada.FimGuerra, rodada.TipoGuerra, clan);
                    guerra.DefinirGuerraTag(rodada.GuerraTag);
                    context.Guerras.Add(guerra);
                    continue;
                }
                else
                {
                    guerraService.AtualizarGuerra(guerra, rodada.Status, rodada.InicioGuerra, rodada.FimGuerra, clan);
                    guerra.DefinirGuerraTag(rodada.GuerraTag);
                }
            }

            await context.Commit(cancellationToken);

            bool response = true;
            return response;
        }

    }

    public record LigaGuerraClanRequest
    {
        public string Tag { get; set; }
        public int ClanLevel { get; set; }
        public string Nome { get; set; }
        public List<LigaGuerraMembroRequest> Membros { get; set; }
    }
    public record LigaGuerraMembroRequest
    {
        public string Tag { get; set; }
        public int CentroVilaLevel { get; set; }
        public string Nome { get; set; }
    }
    public record LigaGuerraRodadaRequest
    {
        public string Status { get; set; }
        public int Dia { get; init; }
        public string GuerraTag { get; init; }
        public string ClanTag { get; init; }
        public string ClanTagOponente { get; init; }
        public string TipoGuerra { get; set; }
        public DateTime InicioGuerra { get; init; }
        public DateTime FimGuerra { get; init; }
    }
}
