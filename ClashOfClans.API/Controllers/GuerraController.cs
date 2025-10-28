using ClashOfClans.API.Application.Commands.Guerras;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/guerra")]
public class GuerraController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpPut("criar")]
    public async Task<IActionResult> UpsertGuerra([FromBody] UpsertGuerraRequest request)
    {
        var resultado = await _mediator.Send(request);
        return resultado.ToActionResult(this);
    }
}
