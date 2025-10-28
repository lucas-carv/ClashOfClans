using ClashOfClans.API.Core.CommandResults;
using ClashOfClans.API.Data;
using ClashOfClans.API.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Application.Commands.Clans;


public record CriarClanRequest(string Tag, string Nome, IEnumerable<MembroDTO> Membros) : IRequest<CommandResult<CriarClanResponse>>;
public record CriarClanResponse(string Tag, string Nome, IEnumerable<MembroDTO> Membros);

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

public record MembroDTO
{
    public required string Tag { get; set; }
    public required string Nome { get; set; }
}