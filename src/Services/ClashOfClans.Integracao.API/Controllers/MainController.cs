using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClashOfClans.Integracao.API.Controllers;

[ApiController]
public class MainController : Controller
{
    protected ICollection<string> Erros = new List<string>();


    protected static ProblemDetails RespostaDeErro(string title = "", string message = "", int statusCode = StatusCodes.Status400BadRequest)
    {
        return new ProblemDetails()
        {
            Status = statusCode,
            Title = (string.IsNullOrEmpty(title) ? "Problema na Requisi��o" : title),
            Detail = (string.IsNullOrEmpty(message) ? "" : message),
        };
    }

    protected ActionResult AcessoNaoAutorizado(string mensagem = "Voc� n�o est� autorizado a acessar este recurso", string titulo = "N�o Autorizado")
    {
        return new ObjectResult(RespostaDeErro(titulo, mensagem, StatusCodes.Status401Unauthorized));
    }

    protected ActionResult OperacaoNaoPermitida(string mensagem = "Voc� n�o tem permiss�o para realizar esta opera��o", string titulo = "Opera��o Proibida")
    {
        return new ObjectResult(RespostaDeErro(titulo, mensagem, StatusCodes.Status403Forbidden));
    }

    protected ActionResult RegistroNaoEncontrado(string mensagem = "N�o foi poss�vel encontrar o registro", string titulo = "Registro n�o encontrado")
    {
        return NotFound(RespostaDeErro(titulo, mensagem, StatusCodes.Status404NotFound));
    }

    protected ActionResult NaoFoiPossivelSalvar(string mensagem = "N�o foi poss�vel salvar a altera��o.", string titulo = "N�o foi poss�vel salvar")
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
