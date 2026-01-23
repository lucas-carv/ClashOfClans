using ClashOfClans.API.Application.Commands.Guerras;
using ClashOfClans.API.Data;
using ClashOfClans.API.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/guerra")]
public class GuerraController(IMediator mediator, ClashOfClansContext context) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Obtém guerras por tag do clan
    /// </summary>
    /// <response code="200">Retorna os guerras pela tag do clan</response>
    [ProducesResponseType(typeof(List<GuerraViewModel>), StatusCodes.Status200OK)]
    [HttpGet("clanTag/{clanTag}")]
    public async Task<IActionResult> ObterGuerras(string clanTag)
    {
        List<GuerraViewModel> guerras = await context.Guerras
            .Where(g =>
                g.ClansEmGuerra.Any(c => c.Tag == clanTag &&
                g.TipoGuerra.Equals("Normal")))
            .OrderByDescending(g => g.FimGuerra)
            .Select(g => new GuerraViewModel
            {
                FimGuerra = g.FimGuerra,
                InicioGuerra = g.InicioGuerra,
                Status = g.Status,
                TipoGuerra = g.TipoGuerra
            })
            .ToListAsync();

        return Ok(guerras);
    }


    /// <summary>
    /// Obtém logs de guerras por tag do clan
    /// </summary>
    /// <response code="200">Retorna os logs de guerras pela tag do clan</response>
    [ProducesResponseType(typeof(IEnumerable<LogGuerraViewModel>), StatusCodes.Status200OK)]
    [HttpGet("log/clanTag/{clanTag}")]
    public async Task<IActionResult> ObterLogs(string clanTag)
    {
        var logs = await context.LogsGuerras
            .Include(a => a.Clan)
            .Include(a => a.Oponente)
            .Where(g => g.Clan.Tag == clanTag)
            .OrderByDescending(g => g.FimGuerra)
            .Join(context.Guerras,
                log => log.FimGuerra.Date,
                guerra => guerra.FimGuerra.Date,
                (l, g) => new { Log = l, Guerra = g })
            .Select(x => new
            {
                ClanNome = x.Log.Clan.Nome,
                EstrelasClan = x.Log.Clan.Estrelas,
                EstrelasOponente = x.Log.Oponente.Estrelas,
                OponenteNome = x.Log.Oponente.Nome,
                Resultado = x.Log.Resultado,
                InicioGuerra = x.Guerra.InicioGuerra,
                FimGuerra = x.Guerra.FimGuerra
            })
            .ToListAsync();

        IEnumerable<LogGuerraViewModel> resultado = logs.Select(l => new LogGuerraViewModel
        {
            ClanNome = l.ClanNome,
            EstrelasClan = l.EstrelasClan,
            EstrelasOponente = l.EstrelasOponente,
            OponenteNome = l.OponenteNome,
            Resultado = l.Resultado,
            FimGuerra = l.FimGuerra,
            InicioGuerra = l.InicioGuerra
        });
        return Ok(resultado);
    }

    /// <summary>
    /// Cria ou atualiza uma guerra
    /// </summary>
    /// <response code="200">Cria ou atualiza uma guerra e retorna o response</response>
    [ProducesResponseType(typeof(UpsertGuerraResponse), StatusCodes.Status200OK)]
    [HttpPut("criar")]
    public async Task<IActionResult> UpsertGuerra([FromBody] UpsertGuerraRequest request)
    {
        var resultado = await _mediator.Send(request);
        return resultado.ToActionResult(this);
    }
}
