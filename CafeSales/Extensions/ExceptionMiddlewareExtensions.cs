using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Net;
using CafeSales.Middleware.ExceptionHandling.Errors;

namespace CafeSales.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        Log.Error(contextFeature.Error.ToString());

                        var errorDetails = new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        };
                        await context.Response.WriteAsync(errorDetails.ToString());
                    }
                });
            });
        }
    }
}