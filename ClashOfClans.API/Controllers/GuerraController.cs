using ClashOfClans.API.Application.Commands.Clans;
using ClashOfClans.API.Application.Commands.Guerras;
using ClashOfClans.API.Core;
using ClashOfClans.API.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/guerra")]
public class GuerraController(IMediatorHandler mediator) : MainController
{
    private readonly IMediatorHandler _mediator = mediator;
    [HttpPost("criar")]
    public async Task<IActionResult> CriarGuerra([FromServices] IMediator mediator, [FromBody] CriarGuerraRequest request)
    {
        var resultado = await mediator.Send(request);
        //if (!resultado.ValidationResult.IsValid)
        //{
        //    return CustomResponse(resultado.ValidationResult);
        //}
        return NoContent();
    }

}
