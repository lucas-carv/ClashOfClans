using ClashOfClans.API.Core.CommandResults;
using ClashOfClans.API.Data;
using ClashOfClans.API.Model.LigaDeClans;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Application.Commands.LigaDeGuerras
{
    public class LigaDeGuerraRequest : IRequest<CommandResult<bool>>
    {
        public string Status { get; set; }
        public string Temporada { get; set; }
        public List<LigaGuerraClan>? Clans { get; set; }
        public List<LigaGuerraRodada>? Rodadas { get; set; }
    }
    public class CriarLigaDeGuerraCommandHandler(ClashOfClansContext context) : IRequestHandler<LigaDeGuerraRequest, CommandResult<bool>>
    {
        private readonly ClashOfClansContext _context = context;
        public async Task<CommandResult<bool>> Handle(LigaDeGuerraRequest command, CancellationToken cancellationToken)
        {
            //bool clanExiste = await _context.Clans.AnyAsync(c => c.Tag == command.Clans.Any(c.Tag == Tag), cancellationToken: cancellationToken);
            //if (clanExiste)
            //{
            //    return ValidationErrors.Clan.ClanJaExiste;
            //}

            LigaDeGuerra ligaDeGuerra = new()
            {
                Clans = command.Clans.Select(c => new Model.LigaDeClans.LigaGuerraClan()
                {
                    ClanLevel = c.ClanLevel,
                    Membros = c.Membros.Select(m => new Model.LigaDeClans.LigaGuerraMembro()
                    {
                        CentroVilaLevel = m.CentroVilaLevel,
                        Nome = m.Nome,
                        Tag = m.Tag
                    }).ToList(),
                    Nome = c.Nome,
                    Tag = c.Tag
                }).ToList(),
                Rodadas = command.Rodadas.Select(r => new Model.LigaDeClans.LigaGuerraRodada()
                {
                    GuerraTags = r.GuerraTags
                }).ToList(),
                Status = command.Status,
                Temporada = command.Temporada
            };

            _context.Update(ligaDeGuerra);
            await _context.Commit(cancellationToken);

            bool response = true;
            return response;
        }

    }

    public class LigaGuerraClan
    {
        public string Tag { get; set; }
        public int ClanLevel { get; set; }
        public string Nome { get; set; }
        public List<LigaGuerraMembro> Membros { get; set; }
    }
    public class LigaGuerraMembro
    {
        public string Tag { get; set; }
        public int CentroVilaLevel { get; set; }
        public string Nome { get; set; }
    }
    public class LigaGuerraRodada
    {
        public List<string> GuerraTags { get; set; }
    }
}
