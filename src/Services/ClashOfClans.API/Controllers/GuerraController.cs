using ClashOfClans.API.Application.Commands.Guerras;
using ClashOfClans.API.Data;
using ClashOfClans.API.Model.Guerras;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/guerra")]
public class GuerraController(IMediator mediator, ClashOfClansContext context) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("clanTag/{clanTag}")]
    public async Task<IActionResult> ObterGuerras(string clanTag)
    {
        var guerras = await context.Guerras
            .Where(g =>
                g.ClanEmGuerra.Tag == clanTag &&
                g.TipoGuerra.Equals("Normal"))
            .OrderByDescending(g => g.FimGuerra)
            .ToListAsync();

        var guerrasViewModel = guerras.Select(g => new GuerraViewModel
        {
            FimGuerra = g.FimGuerra,
            InicioGuerra = g.InicioGuerra,
            Status = g.Status,
            TipoGuerra = g.TipoGuerra
        });
        return Ok(guerrasViewModel);
    }

    [HttpPut("criar")]
    public async Task<IActionResult> UpsertGuerra([FromBody] UpsertGuerraRequest request)
    {
        var resultado = await _mediator.Send(request);
        return resultado.ToActionResult(this);
    }
}

public class GuerraViewModel
{
    public string Status { get; set; }
    public DateTime InicioGuerra { get; set; }
    public DateTime FimGuerra { get; set; }
    public string TipoGuerra { get; set; }
}
