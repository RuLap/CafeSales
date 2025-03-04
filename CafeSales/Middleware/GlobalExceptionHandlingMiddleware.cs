using CafeSales.Middleware.ExceptionHandling.Errors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CafeSales.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (CustomException e)
        {
            _logger.LogError(e, e.InnerMessage);

            context.Response.StatusCode = e.StatusCode;

            var problem = new ProblemDetails
            {
                Status = e.StatusCode,
                Title = "Server error",
                Detail = e.Message
            };

            var json = JsonConvert.SerializeObject(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);                
        }
    }
}