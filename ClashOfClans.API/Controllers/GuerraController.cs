using ClashOfClans.API.Application.Commands.Guerras;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/guerra")]
public class GuerraController(IMediator mediator) : MainController
{
    private readonly IMediator _mediator = mediator;
    [HttpPost("criar")]
    public async Task<IActionResult> CriarGuerra([FromBody] CriarGuerraRequest request)
    {
        var resultado = await _mediator.Send(request);
        return resultado.ToActionResult(this);
    }
}
