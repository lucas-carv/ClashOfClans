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

    /// <summary>
    /// Obtém clans
    /// </summary>
    /// <response code="200">Retorna os clans cadastrados</response>
    [ProducesResponseType(typeof(List<ClanViewModel>), StatusCodes.Status200OK)]
    [HttpGet]
    public async Task<IActionResult> ObterClans()
    {
        var clans = await context.Clans.ToListAsync();
        IEnumerable<ClanViewModel> clansViewModels = clans.Select(c => new ClanViewModel
        {
            Tag = c.Tag,
            Nome = c.Nome
        });

        return Ok(clansViewModels);
    }

    /// <summary>
    /// Obtém clan por tag
    /// </summary>
    /// <response code="200">Retorna um clan</response>
    [ProducesResponseType(typeof(Clan), StatusCodes.Status200OK)]
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

    /// <summary>
    /// Cria um clan
    /// </summary>
    /// <response code="200">Cria o clan e retorna o response</response>
    [ProducesResponseType(typeof(CriarClanResponse), StatusCodes.Status200OK)]
    [HttpPost("criar")]
    public async Task<IActionResult> CriarClan([FromBody] CriarClanRequest request)
    {
        CommandResult<CriarClanResponse> resultado = await _mediator.Send(request);
        return resultado.ToActionResult(this);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("atualizar")]
    public async Task<IActionResult> AtualizarClan([FromBody] AtualizarClanRequest request)
    {
        CommandResult<AtualizarClanResponse> resultado = await _mediator.Send(request);
        return resultado.ToActionResult(this);
    }
}

public class ClanViewModel
{
    public string Tag { get; init; }
    public string Nome { get; init; }
}
