using FCG.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace FCG.Api.Middlewares;

// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class TratamentoErroMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TratamentoErroMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public TratamentoErroMiddleware(RequestDelegate next, ILogger<TratamentoErroMiddleware> logger, IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {

        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {

            await HandleExpectionAsync(httpContext, ex);
        }
    }

    private async Task HandleExpectionAsync(HttpContext httpContext, Exception ex)
    {
        var statusCode = ex switch
        {
            DomainException => HttpStatusCode.BadRequest,
            ArgumentException => HttpStatusCode.BadRequest,
            KeyNotFoundException => HttpStatusCode.NotFound,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            _ => HttpStatusCode.InternalServerError
        };

        _logger.LogError(
            ex,
            "Erro não tratado. Path: {Path}, Method: {Method}, StatusCode: {StatusCode}",
            httpContext.Request.Path,
            httpContext.Request.Method,
            (int)statusCode
        );

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;

        var resposta = new
        {
            statusCode = (int)statusCode,
            message = statusCode == HttpStatusCode.InternalServerError
                ? "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde."
                : ex.Message,
            detail = _environment.IsDevelopment() ? ex.StackTrace : null
        };

        var json = JsonSerializer.Serialize(resposta);

        await httpContext.Response.WriteAsync(json);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class TratamentoErroMiddlewareExtensions
{
    public static IApplicationBuilder UseTratamentoErroMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TratamentoErroMiddleware>();
    }
}
