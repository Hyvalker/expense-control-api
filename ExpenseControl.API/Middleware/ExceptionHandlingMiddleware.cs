using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace ExpenseControl.API.Middleware;

/// <summary>
/// Middleware global para captura e tratamento de exceções na API.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Intercepta o pipeline de requisições para capturar exceções.
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// Processa a exceção capturada e define a resposta HTTP apropriada.
    /// </summary>
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError; 
        var message = "Ocorreu um erro inesperado no servidor.";

        // Mapeamento de exceções de regra de negócio para status code
        if (exception is InvalidOperationException)
        {
            code = HttpStatusCode.BadRequest;
            message = exception.Message;
        }
        else if (exception is KeyNotFoundException)
        {
            code = HttpStatusCode.NotFound;
            message = exception.Message;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        

        var result = JsonSerializer.Serialize(new ErrorDetails
        {
            StatusCode = (int) code,
            Message = message
        });
        
        return context.Response.WriteAsync(result);
    }
}