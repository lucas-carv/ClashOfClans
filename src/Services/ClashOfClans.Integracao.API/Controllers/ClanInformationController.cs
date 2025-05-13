using ClashOfClans.Integracao.API.Application.Commands.Guerras;
using ClashOfClans.Integracao.API.Core;
using ClashOfClans.Integracao.API.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace ClashOfClans.Integracao.API.Controllers;

[Route("api/v1/coc/clan")]
public class ClanInformationController : MainController
{
    private readonly IMediatorHandler _mediator;

    public ClanInformationController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Adiciona guerras em lote
    /// </summary>
    /// <response code="204">Retorna sucesso sem conteúdo no corpo da mensagem</response>
    /// <response code="400">Um ou mais dados de entrada inválidos</response> 
    [HttpPost("guerra")]
    public async Task<IActionResult> InserirFiliaisEmLote(GuerrasInputModels inputModel)
    {
        var resultado = await _mediator.EnviarComando(new AdicionarGuerrasCommand(inputModel.Items));
        if (!resultado.ValidationResult.IsValid)
        {
            return CustomResponse(resultado.ValidationResult);
        }
        return CustomResponse(resultado.Response);
    }
}