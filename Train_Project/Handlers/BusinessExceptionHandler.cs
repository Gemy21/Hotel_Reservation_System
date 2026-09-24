using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Train_Project.Exciption;

namespace Train_Project.Handlers;

public class BusinessExceptionHandler(
    ILogger<BusinessExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not BaseException baseException || exception is ValidationException)
        {
            return false;
        }

        logger.LogWarning(
            exception,
            "Business exception occurred: {ErrorCode} - {Message}",
            baseException.ErrorCode,
            baseException.Message);
         
        var problemDetails = new ProblemDetails 
        {
            Type = GetProblemTypeUrl(baseException.StatusCode), 
            Title = GetStatusTitle(baseException.StatusCode),
            Status = baseException.StatusCode,
            Detail = baseException.Message,
            Instance = httpContext.Request.Path, 
            Extensions =
            {
                ["errorCode"] = baseException.ErrorCode,
                ["timestamp"] = DateTimeOffset.UtcNow,
                ["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier 
            }
        };

        httpContext.Response.StatusCode = baseException.StatusCode;
        httpContext.Response.ContentType = "application/problem+json";

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, options, cancellationToken);
        return true;
    }

    private static string GetProblemTypeUrl(int statusCode) 
    {
        return statusCode switch
        {
            400 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            401 => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
            403 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3",
            404 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            409 => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            _ => $"https://httpstatuses.com/{statusCode}"
        };
    }

    private static string GetStatusTitle(int statusCode)
    {
        return statusCode switch
        {
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            409 => "Conflict",
            _ => "An error occurred"
        };
    }
}
