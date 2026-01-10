using ClashOfClans.API.Application.Commands.LigaDeGuerras;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/liga-guerra")]
public class LigaDeGuerraController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("criar")]
    public async Task<IActionResult> CriarGuerra([FromBody] LigaDeGuerraRequest request)
    {
        var resultado = await _mediator.Send(request);
        return resultado.ToActionResult(this);
    }
}
