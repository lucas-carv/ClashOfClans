using ClashOfClans.API.Core.CommandResults;
using ClashOfClans.API.Data;
using ClashOfClans.API.DTOs;
using ClashOfClans.API.Model.Guerras;
using ClashOfClans.API.Services.Guerras;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Application.Commands.Guerras;

public record UpsertGuerraRequest(string Status, DateTime InicioGuerra, DateTime FimGuerra, string TipoGuerra, ClanEmGuerraDTO Clan) : IRequest<CommandResult<UpsertGuerraResponse>>;
public record UpsertGuerraResponse(string Status, DateTime InicioGuerra, DateTime FimGuerra, ClanEmGuerraDTO Clan);

public class UpsertGuerraCommandHandler(ClashOfClansContext context, GuerraService guerraService) : IRequestHandler<UpsertGuerraRequest, CommandResult<UpsertGuerraResponse>>
{
    public async Task<CommandResult<UpsertGuerraResponse>> Handle(UpsertGuerraRequest request, CancellationToken cancellationToken)
    {
        bool clanExiste = await context.Clans.AnyAsync(c => c.Tag == request.Clan.Tag, cancellationToken: cancellationToken);
        if (!clanExiste)
        {
            return ValidationErrors.Clan.ClanNaoExiste;
        }

        Guerra? guerraExistente = await context.Guerras.Include(g => g.ClanEmGuerra).ThenInclude(c => c.MembrosEmGuerra).ThenInclude(m => m.Ataques)
            .FirstOrDefaultAsync(
                g =>
                    g.InicioGuerra == request.InicioGuerra &&
                    g.ClanEmGuerra.Tag == request.Clan.Tag,
                cancellationToken: cancellationToken);
        if (guerraExistente is null)
        {
            Guerra novaGuerra = guerraService.CriarGuerra(request.Status, request.InicioGuerra, request.FimGuerra, request.TipoGuerra, request.Clan);

            context.Add(novaGuerra);
            await context.SaveChangesAsync(cancellationToken);

            UpsertGuerraResponse responseCriacao = MapearResponse(novaGuerra);
            return responseCriacao;
        }

        Guerra guerra = guerraService.AtualizarGuerra(guerraExistente, request.Status, request.InicioGuerra, request.FimGuerra, request.Clan);
        context.Update(guerra);
        await context.Commit(cancellationToken);

        UpsertGuerraResponse responseAtualizacao = MapearResponse(guerra);
        return responseAtualizacao;
    }

    private static UpsertGuerraResponse MapearResponse(Guerra guerra)
    {
        ClanEmGuerraDTO clanDTO = new()
        {
            Tag = guerra.ClanEmGuerra.Tag,
            Membros = guerra.ClanEmGuerra.MembrosEmGuerra.Select(m => new MembroEmGuerraDTO
            {
                Tag = m.Tag,
                Nome = m.Nome,
                CentroVilaLevel = m.CentroVilaLevel,
                Ataques = m.Ataques.Select(a => new AtaquesDTO
                {
                    AtacanteTag = a.AtacanteTag,
                    DefensorTag = a.DefensorTag,
                    Estrelas = a.Estrelas
                })
            })
        };

        return new UpsertGuerraResponse(guerra.Status, guerra.InicioGuerra, guerra.FimGuerra, clanDTO);
    }
}