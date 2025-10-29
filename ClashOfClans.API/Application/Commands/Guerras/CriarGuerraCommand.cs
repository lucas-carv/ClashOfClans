using ClashOfClans.API.Core.CommandResults;
using ClashOfClans.API.Data;
using ClashOfClans.API.Model.Guerras;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Application.Commands.Guerras;
public record UpsertGuerraRequest(string Status, DateTime InicioGuerra, DateTime FimGuerra, ClanEmGuerraDTO Clan) : IRequest<CommandResult<UpsertGuerraResponse>>;
public record UpsertGuerraResponse(string Status, DateTime InicioGuerra, DateTime FimGuerra, ClanEmGuerraDTO Clan);

public class UpsertGuerraCommandHandler(ClashOfClansContext context) : IRequestHandler<UpsertGuerraRequest, CommandResult<UpsertGuerraResponse>>
{
    public async Task<CommandResult<UpsertGuerraResponse>> Handle(UpsertGuerraRequest request, CancellationToken cancellationToken)
    {
        bool clanExiste = await context.Clans.AnyAsync(c => c.Tag == request.Clan.Tag, cancellationToken: cancellationToken);
        if (!clanExiste)
        {
            return ValidationErrors.Clan.ClanNaoExiste;
        }

        bool existeGuerra = await context.Guerras
            .AnyAsync(
                g =>
                    g.InicioGuerra == request.InicioGuerra &&
                    g.FimGuerra == request.FimGuerra,
                cancellationToken: cancellationToken);
        if (existeGuerra)
        {
            return ValidationErrors.Guerra.GuerraJaExiste;
        }

        ClanEmGuerra clanEmGuerra = new(request.Clan.Tag);

        foreach (var participantesGuerra in request.Clan.Membros)
        {
            clanEmGuerra.AdicionarMembro(participantesGuerra.Tag, participantesGuerra.Nome);
        }

        Guerra guerra = new(request.Status, request.InicioGuerra, request.FimGuerra, clanEmGuerra);
        context.Add(guerra);
        await context.SaveChangesAsync(cancellationToken);

        ClanEmGuerraDTO clan = new()
        {
            Tag = guerra.ClanEmGuerra.Tag,
            Membros = guerra.ClanEmGuerra.Membros
                .Select(c => new MembroEmGuerraDTO()
                {
                    Nome = c.Nome,
                    Tag = c.Tag
                })
        };
        UpsertGuerraResponse response = new(guerra.Status, guerra.InicioGuerra, guerra.FimGuerra, clan);
        return response;
    }
}

public record ClanEmGuerraDTO
{
    public string Tag { get; set; } = string.Empty;
    public IEnumerable<MembroEmGuerraDTO> Membros { get; set; } = [];
}
public record MembroEmGuerraDTO
{
    public required string Tag { get; set; }
    public required string Nome { get; set; }
    public IEnumerable<AtaquesDTO> Ataques { get; set; } = [];
}
public record AtaquesDTO
{
    public int Estrelas { get; set; }
}