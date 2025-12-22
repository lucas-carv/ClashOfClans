using ClashOfClans.API.Application.Commands.Clans;
using ClashOfClans.API.Core.CommandResults;
using ClashOfClans.API.Data;
using ClashOfClans.API.Model.Clans;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/clan")]
public class ClanController(IMediator mediator, ClashOfClansContext context) : ControllerBase
{
    private readonly IMediator _mediator = mediator;


    [HttpGet()]
    public async Task<IActionResult> ObterClans()
    {
        var clans = await context.Clans.Include(c => c.Membros.Where(m => m.Situacao == SituacaoMembro.Ativo)).ToListAsync();

        return Ok(clans);
    }

    [HttpGet("{tag}")]
    public async Task<IActionResult> ObterPorTag(string tag)
    {
        Clan? clan = await context.Clans
            .Include(c => 
                c.Membros
                .Where(m => m.Situacao == SituacaoMembro.Ativo))
            .FirstOrDefaultAsync(c => c.Tag == tag);
        return Ok(clan);
    }

    [HttpPost("criar")]
    public async Task<IActionResult> CriarClan([FromBody] CriarClanRequest request)
    {
        CommandResult<CriarClanResponse> resultado = await _mediator.Send(request);
        return resultado.ToActionResult(this);
    }

    [HttpPut("atualizar")]
    public async Task<IActionResult> AtualizarClan([FromBody] AtualizarClanRequest request)
    {
        CommandResult<AtualizarClanResponse> resultado = await _mediator.Send(request);
        return resultado.ToActionResult(this);
    }
}
