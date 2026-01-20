using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ClashOfClans.API.Core;

public static class ProblemDetailsHandlerExtension
{
    public static void UseProblemDetailsExceptionHandler(this IApplicationBuilder app, ILogger logger = null)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async contexto =>
            {
                var exceptionHandlerFeature = contexto.Features.Get<IExceptionHandlerFeature>();

                if (exceptionHandlerFeature != null)
                {
                    var excecao = exceptionHandlerFeature.Error;

                    var problemDetails = new ProblemDetails
                    {
                        Instance = contexto.Request.HttpContext.Request.Path
                    };

                    contexto.Response.ContentType = "application/problem+json";

                    if (excecao is DomainException domainException)
                    {
                        var validation = new ValidationProblemDetails(new Dictionary<string, string[]>
                                    {
                                        { "Mensagens", new string [1] { domainException.Message } }
                                    })
                        {
                            Instance = contexto.Request.HttpContext.Request.Path,
                            Title = "Erro de Validação",
                            Status = (int)domainException.StatusCode
                        };

                        contexto.Response.StatusCode = validation.Status.Value;

                        var json = JsonSerializer.Serialize(validation);

                        await contexto.Response.WriteAsync(json);
                    }
                    else if (excecao is ValidationException validationException)
                    {
                        var errosValidacao = validationException.Errors.Select(c => c.ErrorMessage).ToArray();

                        var validation = new ValidationProblemDetails(new Dictionary<string, string[]>
                                    {
                                        { "Mensagens", errosValidacao }
                                    })
                        {
                            Instance = contexto.Request.HttpContext.Request.Path,
                            Title = "Erro de Validação",
                            Status = 400
                        };

                        contexto.Response.StatusCode = validation.Status.Value;

                        var json = JsonSerializer.Serialize(validation);

                        await contexto.Response.WriteAsync(json);
                    }
                    else if (excecao is UnauthorizedAccessException unauthorizedException)
                    {
                        problemDetails.Title = "Não autorizado";
                        problemDetails.Status = StatusCodes.Status401Unauthorized;
                        problemDetails.Detail = excecao.Message;

                        contexto.Response.StatusCode = problemDetails.Status.Value;

                        var json = JsonSerializer.Serialize(problemDetails);

                        await contexto.Response.WriteAsync(json);
                    }
                    else
                    {
                        problemDetails.Title = "Falha no Servidor";
                        problemDetails.Status = StatusCodes.Status500InternalServerError;
                        problemDetails.Detail = excecao.Message;

                        contexto.Response.StatusCode = problemDetails.Status.Value;

                        var json = JsonSerializer.Serialize(problemDetails);

                        await contexto.Response.WriteAsync(json);
                    }
                }
            });
        });
    }
}
public class DomainException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public DomainException()
    {
        StatusCode = HttpStatusCode.BadRequest;
    }

    public DomainException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message)
    {
        StatusCode = statusCode;
    }

    public DomainException(string message, Exception innerException, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}
