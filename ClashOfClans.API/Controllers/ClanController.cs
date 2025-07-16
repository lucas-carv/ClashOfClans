using ClashOfClans.API.Application.Clans;
using ClashOfClans.API.Core;
using ClashOfClans.API.InputModels;
using ClashOfClans.API.Model;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClashOfClans.API.Controllers;

[Route("api/v1/clan")]
public class ClanController : MainController
{
    private readonly IMediatorHandler _mediator;

    public ClanController(IMediatorHandler mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("adicionar")]
    public async Task<IActionResult> AdicionarClan(List<ClanInputModel> inputModel)
    {
        AdicionarClanCommand adicionarClanCommand = new()
        {
            MemberList = inputModel.First().MemberList,
            Tag = inputModel.First().Tag!,
            Nome = inputModel.First().Name!
        };
        var resultado = await _mediator.EnviarComando(adicionarClanCommand);
        if (!resultado.ValidationResult.IsValid)
        {
            return CustomResponse(resultado.ValidationResult);
        }
        return NoContent();
    }
}
public class MainController : Controller
{
    protected ICollection<string> Erros = new List<string>();


    protected static ProblemDetails RespostaDeErro(string title = "", string message = "", int statusCode = StatusCodes.Status400BadRequest)
    {
        return new ProblemDetails()
        {
            Status = statusCode,
            Title = (string.IsNullOrEmpty(title) ? "Problema na Requisição" : title),
            Detail = (string.IsNullOrEmpty(message) ? "" : message),
        };
    }

    protected ActionResult AcessoNaoAutorizado(string mensagem = "Você não está autorizado a acessar este recurso", string titulo = "Não Autorizado")
    {
        return new ObjectResult(RespostaDeErro(titulo, mensagem, StatusCodes.Status401Unauthorized));
    }

    protected ActionResult OperacaoNaoPermitida(string mensagem = "Você não tem permissão para realizar esta operação", string titulo = "Operação Proibida")
    {
        return new ObjectResult(RespostaDeErro(titulo, mensagem, StatusCodes.Status403Forbidden));
    }

    protected ActionResult RegistroNaoEncontrado(string mensagem = "Não foi possível encontrar o registro", string titulo = "Registro não encontrado")
    {
        return NotFound(RespostaDeErro(titulo, mensagem, StatusCodes.Status404NotFound));
    }

    protected ActionResult NaoFoiPossivelSalvar(string mensagem = "Não foi possível salvar a alteração.", string titulo = "Não foi possível salvar")
    {
        return BadRequest(RespostaDeErro(titulo, mensagem, StatusCodes.Status400BadRequest));
    }


    protected ActionResult CustomResponseCreated()
    {
        if (OperacaoValida())
        {
            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
    }

    protected ActionResult CustomResponse(object result = null)
    {
        if (OperacaoValida())
            return Ok(result);

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
    }
    protected ActionResult CustomResponse(ValidationResult result)
    {
        Dictionary<string, string[]> listaErrosValidacao = new();

        var errosValidacao = result.Errors
             .Select(c => c.ErrorMessage).ToArray();

        return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", errosValidacao }
            }));
    }


    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);

        foreach (var erro in erros)
        {
            AdicionarErroProcessamento(erro.ErrorMessage);
        }

        return CustomResponse();
    }

    //protected ActionResult CustomResponse(ResponseResult resposta)
    //{
    //    ResponsePossuiErros(resposta);

    //    return CustomResponse();
    //}

    //protected bool ResponsePossuiErros(ResponseResult resposta)
    //{
    //    if (resposta == null || !resposta.Errors.Mensagens.Any()) return false;

    //    foreach (var mensagem in resposta.Errors.Mensagens)
    //    {
    //        AdicionarErroProcessamento(mensagem);
    //    }

    //    return true;
    //}

    protected bool OperacaoValida()
    {
        return !Erros.Any();
    }

    protected void AdicionarErroProcessamento(string erro)
    {
        Erros.Add(erro);
    }

    protected void LimparErrosProcessamento()
    {
        Erros.Clear();
    }
}