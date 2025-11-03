using ClashOfClans.API.Core;
using ClashOfClans.API.Core.CommandResults;
using ClashOfClans.API.Data;
using ClashOfClans.API.DTOs;
using ClashOfClans.API.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Application.Commands.Clans;

public record AtualizarClanRequest(string Tag, string Nome, IEnumerable<MembroClanDTO> Membros) : IRequest<CommandResult<AtualizarClanResponse>>;
public record AtualizarClanResponse(string Tag, string Nome, IEnumerable<MembroClanDTO> Membros);

public class AtualizarClanCommandHandler(ClashOfClansContext context) : IRequestHandler<AtualizarClanRequest, CommandResult<AtualizarClanResponse>>
{
    public async Task<CommandResult<AtualizarClanResponse>> Handle(AtualizarClanRequest request, CancellationToken cancellationToken)
    {
        Clan? clan = await context.Clans.FirstOrDefaultAsync(c => c.Tag == request.Tag, cancellationToken: cancellationToken);
        if (clan is null)
        {
            return ValidationErrors.Clan.ClanNaoExiste;
        }

        IEnumerable<MembroClanDTO> membrosParaAdicionar = request.Membros
            .Where(me => !clan.Membros.Any(m => m.Tag == me.Tag && m.Situacao == SituacaoMembro.Ativo))
            .Select(membroDTO => membroDTO);

        foreach (var membro in membrosParaAdicionar)
        {
            clan.AdicionarMembro(membro.Tag, membro.Nome);
        }

        IEnumerable<string> membrosTagParaInativar = clan.Membros
            .Where(m => !request.Membros.Any(me => me.Tag == m.Tag))
            .Select(m => m.Tag);

        foreach (var membroTag in membrosTagParaInativar)
        {
            clan.InativarMembro(membroTag);
        }
        await context.SaveChangesAsync(cancellationToken);

        IEnumerable<MembroClanDTO> membros = clan.Membros.Select(m =>
            new MembroClanDTO
            {
                Nome = m.Nome,
                Tag = m.Tag
            });
        AtualizarClanResponse response = new (clan.Tag, clan.Nome, membros);
        return response;
    }
}
