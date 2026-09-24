using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Train_Project.Exciption;

namespace Train_Project.Handlers;

public class ValidationExceptionHandler(
    ILogger<ValidationExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken) 
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        logger.LogWarning(
            "Validation error occurred: {ErrorCount} validation errors",
            validationException.Errors.Count);

        var problemDetails = new ValidationProblemDetails(validationException.Errors)
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            Title = "One or more validation errors occurred.",
            Status = StatusCodes.Status422UnprocessableEntity,
            Detail = validationException.Message,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions["errorCode"] = "VALIDATION_ERROR";
        problemDetails.Extensions["timestamp"] = DateTimeOffset.UtcNow;
        problemDetails.Extensions["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
        httpContext.Response.ContentType = "application/problem+json";

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, options, cancellationToken);
        return true;
    }
}
