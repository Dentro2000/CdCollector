using cd_collection.core.Exceptions;
using Microsoft.AspNetCore.Http;


namespace cd_collection.infrastructure.Middleware;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleException(exception, context);
        }
    }

    private async Task HandleException(Exception exception, HttpContext context)
    {
        var (statusCode, error) = exception switch
        {
            CustomException => (StatusCodes.Status400BadRequest,
                new Error(exception.GetType().Name.Replace("Exception", ""), exception.Message)),
            _ => (StatusCodes.Status500InternalServerError, new Error("error", "There was an error"))
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }

    private record Error(string Code, string Reason);
}