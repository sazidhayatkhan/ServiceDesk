using System.Net;
using System.Text.Json;

namespace ServiceDesk.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            await WriteErrorResponse(
                context,
                HttpStatusCode.NotFound,
                ex.Message);
        }
        catch (ArgumentException ex)
        {
            await WriteErrorResponse(
                context,
                HttpStatusCode.BadRequest,
                ex.Message);
        }
        catch (Exception)
        {
            await WriteErrorResponse(
                context,
                HttpStatusCode.InternalServerError,
                "An unexpected error occurred.");
        }
    }

    private static async Task WriteErrorResponse(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var response = new
        {
            message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}