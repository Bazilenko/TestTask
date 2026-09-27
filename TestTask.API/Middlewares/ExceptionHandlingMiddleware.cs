using System.Net;
using System.Text.Json;
using TestTask.BLL.Exceptions;

namespace TestTask.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = exception switch
        {
            BadRequestException => (int)HttpStatusCode.BadRequest, // 400
            NotFoundException => (int)HttpStatusCode.NotFound,     // 404
            ConflictException => (int)HttpStatusCode.Conflict,     // 409
            _ => (int)HttpStatusCode.InternalServerError           // 500 
        };

        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = context.Response.StatusCode == 500 
                ? "An unexpected internal server error occurred." 
                : exception.Message 
        };

        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}