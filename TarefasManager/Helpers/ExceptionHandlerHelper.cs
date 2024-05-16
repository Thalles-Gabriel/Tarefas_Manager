using Microsoft.AspNetCore.Mvc;
namespace TarefasManager.Helpers;

public class ExceptionHandlerHelper : IMiddleware
{

    private readonly ILogger<ExceptionHandlerHelper> _logger;

    public ExceptionHandlerHelper(ILogger<ExceptionHandlerHelper> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error interno de servidor.");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            ProblemDetails problem = new()
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "Erro de servidor",
                Title = e.InnerException?.ToString() ?? e.ToString(),
                Detail = e.Message
            };

            await context.Response.WriteAsJsonAsync<ProblemDetails>(problem);
        }
    }
}
