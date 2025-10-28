using ClashOfClans.API.Core.CommandResults;
using ClashOfClans.API.Data;
using ClashOfClans.API.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Application.Commands.Clans;


public class CriarClanRequest(string Tag, string Nome, IEnumerable<MembroDTO> Membros) : IRequest<CommandResult<CriarClanResponse>>
{
    public required string Tag { get; init; } = Tag;
    public required string Nome { get; init; } = Nome;
    public IEnumerable<MembroDTO> Membros { get; init; } = Membros;
}

public class CriarClanResponse(string Tag, string Nome, IEnumerable<MembroDTO> Membros)
{
    public string Tag { get; init; } = Tag;
    public string Nome { get; init; } = Nome;
    public IEnumerable<MembroDTO> Membros { get; set; } = Membros;
}

public class CriarClanCommandHandler(ClashOfClansContext context) : IRequestHandler<CriarClanRequest, CommandResult<CriarClanResponse>>
{
    private readonly ClashOfClansContext _context = context;

    public async Task<CommandResult<CriarClanResponse>> Handle(CriarClanRequest command, CancellationToken cancellationToken)
    {
        bool clanExiste = await _context.Clans.AnyAsync(c => c.Tag == command.Tag, cancellationToken: cancellationToken);
        if (clanExiste)
        {
            return ValidationErrors.Clan.ClanJaExiste;
        }

        Clan clan = new(command.Tag, command.Nome);

        foreach (var membro in command.Membros)
        {
            clan.AdicionarMembro(membro.Tag, membro.Nome);
        }

        _context.Add(clan);
        await _context.SaveChangesAsync(cancellationToken);

        var membros = clan.Membros
            .Select(m => new MembroDTO()
            {
                Nome = m.Nome,
                Tag = m.Tag
            });

        CriarClanResponse response = new(clan.Tag, clan.Nome, membros);
        return response;
    }
}

public class MembroDTO
{
    //[Required(ErrorMessage = "A Tag do membro é obrigatória")]
    public required string Tag { get; set; }
    //[Required(ErrorMessage = "O nome do membro é obrigatório")]
    public required string Nome { get; set; }
}