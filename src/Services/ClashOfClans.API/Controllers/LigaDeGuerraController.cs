using ClashOfClans.API.Application.Commands.LigaDeGuerras;
using ClashOfClans.API.Core.CommandResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/liga-guerra")]
public class LigaDeGuerraController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Cria uma liga de guerra
    /// </summary>
    /// <response code="200">Cria uma liga de guerra</response>
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [HttpPost("criar")]
    public async Task<IActionResult> CriarGuerra([FromBody] LigaDeGuerraRequest request)
    {
        CommandResult<bool> resultado = await _mediator.Send(request);
        return resultado.ToActionResult(this);
    }
}
