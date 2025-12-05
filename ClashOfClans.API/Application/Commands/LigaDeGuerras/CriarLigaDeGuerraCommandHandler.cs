using ClashOfClans.API.Core.CommandResults;
using ClashOfClans.API.Data;
using ClashOfClans.API.Model.Guerras;
using ClashOfClans.API.Model.LigaDeClans;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

namespace ClashOfClans.API.Application.Commands.LigaDeGuerras
{
    public class LigaDeGuerraRequest : IRequest<CommandResult<bool>>
    {
        public required string ClanTag { get; set; }
        public required string Status { get; set; }
        public required string Temporada { get; set; }
        public List<LigaGuerraClanRequest> Clans { get; set; } = [];
        public List<LigaGuerraRodadaRequest> Rodadas { get; set; } = [];
    }
    public class CriarLigaDeGuerraCommandHandler(ClashOfClansContext context) : IRequestHandler<LigaDeGuerraRequest, CommandResult<bool>>
    {
        private readonly ClashOfClansContext _context = context;
        public async Task<CommandResult<bool>> Handle(LigaDeGuerraRequest request, CancellationToken cancellationToken)
        {
            bool clanExiste = await _context.Clans.AnyAsync(c => c.Tag == request.ClanTag, cancellationToken: cancellationToken);
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
                    ligaDeGuerra.AdicionarRodada(rodada.Dia, rodada.GuerraTag, rodada.ClanTag, rodada.ClanTagOponente);
                }
                _context.Add(ligaDeGuerra);
                await _context.Commit(cancellationToken);
            }

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
                    ligaDeGuerra.AdicionarRodada(rodada.Dia, rodada.GuerraTag, rodada.ClanTag, rodada.ClanTagOponente);
                }
            }

            _context.Update(ligaDeGuerra);

            await context.Commit(cancellationToken);

            bool response = true;
            return response;
        }

    }

    public class LigaGuerraClanRequest
    {
        public string Tag { get; set; }
        public int ClanLevel { get; set; }
        public string Nome { get; set; }
        public List<LigaGuerraMembroRequest> Membros { get; set; }
    }
    public class LigaGuerraMembroRequest
    {
        public string Tag { get; set; }
        public int CentroVilaLevel { get; set; }
        public string Nome { get; set; }
    }
    public class LigaGuerraRodadaRequest
    {
        public int Dia { get; init; }
        public string GuerraTag { get; init; }
        public string ClanTag { get; init; }
        public string ClanTagOponente { get; init; }
    }
}
