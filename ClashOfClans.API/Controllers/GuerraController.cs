using ClashOfClans.API.Application.Commands.Clans;
using ClashOfClans.API.Application.Commands.Guerras;
using ClashOfClans.API.Core;
using ClashOfClans.API.InputModels.Guerras;
using ClashOfClans.API.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/guerra")]
public class GuerraController(IMediatorHandler mediator) : MainController
{
    private readonly IMediatorHandler _mediator = mediator;
    [HttpPost("criar")]
    public async Task<IActionResult> CriarGuerra([FromBody] GuerraInputModel inputModel)
    {
        CriarGuerraCommand criarGuerraCommand = new(inputModel.Status, inputModel.InicioGuerra, inputModel.FimGuerra, inputModel.Clan);
        var resultado = await _mediator.EnviarComando(criarGuerraCommand);
        if (!resultado.ValidationResult.IsValid)
        {
            return CustomResponse(resultado.ValidationResult);
        }
        return NoContent();
    }

}
