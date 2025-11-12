using ClashOfClans.API.Core.CommandResults;
using ClashOfClans.API.Data;
using ClashOfClans.API.DTOs;
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

        Guerra? guerraExistente = await context.Guerras.Include(g => g.ClanEmGuerra).ThenInclude(c => c.Membros).ThenInclude(m => m.Ataques)
            .FirstOrDefaultAsync(
                g =>
                    g.InicioGuerra == request.InicioGuerra &&
                    g.FimGuerra == request.FimGuerra,
                cancellationToken: cancellationToken);
        if (guerraExistente is null)
        {
            ClanEmGuerra clanEmGuerra = new(request.Clan.Tag);
            foreach (var membro in request.Clan.Membros)
            {
                MembroEmGuerra membroEmGuerra = clanEmGuerra.AdicionarMembro(membro.Tag, membro.Nome);
                foreach (var ataque in membro.Ataques)
                {
                    membroEmGuerra.AtualizarAtaque(ataque.AtacanteTag, ataque.DefensorTag, ataque.Estrelas);
                }
            }
            Guerra novaGuerra = new(request.Status, request.InicioGuerra, request.FimGuerra, clanEmGuerra);
            context.Add(novaGuerra);
            await context.SaveChangesAsync(cancellationToken);

            UpsertGuerraResponse responseCriacao = MapearResponse(novaGuerra);
            return responseCriacao;
        }

        guerraExistente.Status = request.Status;

        foreach (var membro in request.Clan.Membros)
        {
            MembroEmGuerra membroEmGuerra = guerraExistente.ClanEmGuerra.AdicionarMembro(membro.Tag, membro.Nome);

            foreach (var ataque in membro.Ataques)
            {
                membroEmGuerra.AtualizarAtaque(ataque.AtacanteTag, ataque.DefensorTag, ataque.Estrelas);
            }
        }

        await context.SaveChangesAsync(cancellationToken);

        var responseAtualizacao = MapearResponse(guerraExistente);
        return responseAtualizacao;
    }

    private static UpsertGuerraResponse MapearResponse(Guerra guerra)
    {
        ClanEmGuerraDTO clanDTO = new()
        {
            Tag = guerra.ClanEmGuerra.Tag,
            Membros = guerra.ClanEmGuerra.Membros.Select(m => new MembroEmGuerraDTO
            {
                Tag = m.Tag,
                Nome = m.Nome,
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