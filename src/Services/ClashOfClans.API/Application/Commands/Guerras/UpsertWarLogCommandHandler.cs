using ClashOfClans.API.BackgroundServices.IntegrationAPIClashOfClans.Responses;
using ClashOfClans.API.Core.CommandResults;
using ClashOfClans.API.Data;
using ClashOfClans.API.Model.Guerras;
using ClashOfClans.API.Services.Guerras;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static ClashOfClans.API.Core.CommandResults.ValidationErrors;

namespace ClashOfClans.API.Application.Commands.Guerras;

public record UpsertWarLogRequest(List<WarLogResponseEntryDTO> Items) : IRequest<CommandResult<bool>>;
//public record UpsertWarLogResponse(string Status, DateTime InicioGuerra, DateTime FimGuerra, ClanEmGuerraDTO Clan);
public class UpsertWarLogCommandHandler(ClashOfClansContext context) : IRequestHandler<UpsertWarLogRequest, CommandResult<bool>>
{
    public async Task<CommandResult<bool>> Handle(UpsertWarLogRequest request, CancellationToken cancellationToken)
    {
        List<LogGuerra> logs = new();

        foreach (var log in request.Items)
        {
            bool clanExiste = await context.Clans.AnyAsync(c => c.Tag == log.ClanWarLog.Tag, cancellationToken: cancellationToken);
            if (!clanExiste)
            {
                return ClanValidationErros.ClanNaoExiste;
            }
            var logExiste = context.LogsGuerras.Any(l => l.FimGuerra.Equals(log.FimGuerra));
            if (logExiste)
                continue;

            if (string.IsNullOrEmpty(log.Resultado))
                continue;

            LogGuerra logGuerra = new(log.Resultado, log.FimGuerra, log.QuantidadeMembros, log.AtaquesPorMembro, log.ModificadorDeBatalha);

            var clan = log.ClanWarLog;
            var oponente = log.OpponenteWarLog;
            logGuerra.AdicionarClan(clan.Tag, clan.Nome, clan.ClanLevel, clan.QuantidadeAtaques, clan.Estrelas, clan.PorcentagemDestruicao, clan.ExpGanho);
            logGuerra.AdicionarOponente(oponente.Tag, oponente.Nome, oponente.ClanLevel, oponente.Estrelas, oponente.PorcentagemDestruicao);
            logs.Add(logGuerra);
        }
        context.AddRange(logs);
        await context.Commit(cancellationToken);
        return true;

        //GuerraValidationErros guerra = guerraService.AtualizarGuerra(guerraExistente, request.Status, request.FimGuerra, request.Clan);
        //await context.Commit(cancellationToken);

        //UpsertGuerraResponse responseAtualizacao = MapearResponse(guerra);
        //return responseAtualizacao;
    }
}

public class WarLogResponseEntryDTO
{
    public string Resultado { get; set; }
    public DateTime FimGuerra { get; set; }
    public int QuantidadeMembros { get; set; }
    public int AtaquesPorMembro { get; set; }
    public string ModificadorDeBatalha { get; set; }
    public ClanLogDTO ClanWarLog { get; set; }
    public OponenteLogDTO OpponenteWarLog { get; set; }
}

public class ClanLogDTO
{
    public string Tag { get; set; }
    public string Nome { get; set; }
    public int ClanLevel { get; set; }
    public int QuantidadeAtaques { get; set; }
    public int Estrelas { get; set; }
    public decimal PorcentagemDestruicao { get; set; }
    public int ExpGanho { get; set; }
}
public class OponenteLogDTO
{
    public string Tag { get; set; }
    public string Nome { get; set; }
    public int ClanLevel { get; set; }
    public int Estrelas { get; set; }
    public decimal PorcentagemDestruicao { get; set; }
}