using ClashOfClans.API.Application.Commands.Clans;
using ClashOfClans.API.Core;
using ClashOfClans.API.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/guerra")]
public class GuerraController(IMediatorHandler mediator) : MainController
{
    private readonly IMediatorHandler _mediator = mediator;
    //[HttpPost("criar")]
    //public async Task<IActionResult> CriarGuerra([FromBody] GuerraInputModel inputModel)
    //{
    //    CriarClanCommand adicionarClanCommand = new(inputModel.Tag, inputModel.Nome, inputModel.Membros);
    //    var resultado = await _mediator.EnviarComando(adicionarClanCommand);
    //    if (!resultado.ValidationResult.IsValid)
    //    {
    //        return CustomResponse(resultado.ValidationResult);
    //    }
    //    return NoContent();
    //}

}
