using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace Train_Project.Handlers;

public class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IHostEnvironment environment)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken) 
    {
        logger.LogError(exception,
            "Exception occurred: {Message}", exception.Message);

        var problemDetails = CreateProblemDetails(httpContext, exception);

        httpContext.Response.StatusCode = problemDetails.Status ??
            StatusCodes.Status500InternalServerError;

        httpContext.Response.ContentType = "application/problem+json";


        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, options, cancellationToken); 
        return true;
    }

    private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
    {
        var problemDetails = CreateGenericProblemDetails(context, exception);

        AddCommonExtensions(problemDetails, context, exception);

        return problemDetails;
    }

    private ProblemDetails CreateGenericProblemDetails(
        HttpContext context,
        Exception exception)
    {
        var detail = environment.IsDevelopment()
            ? exception.Message
            : "An error occurred while processing your request.";

        return new ProblemDetails
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Title = "Internal Server Error",
            Status = StatusCodes.Status500InternalServerError,
            Detail = detail,
            Instance = context.Request.Path,
            Extensions =
            {
                ["errorCode"] = "INTERNAL_SERVER_ERROR"
            }
        };
    }

    private void AddCommonExtensions(
        ProblemDetails problemDetails,
        HttpContext context,
        Exception exception) 
    {
        problemDetails.Extensions["timestamp"] = DateTimeOffset.UtcNow;

        var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
        problemDetails.Extensions["traceId"] = traceId;

        if (context.Request.Headers.TryGetValue("X-Request-Id", out var requestId))
        {
            problemDetails.Extensions["requestId"] = requestId.ToString();
        }

        if (environment.IsDevelopment())
        {
            problemDetails.Extensions["exceptionType"] = exception.GetType().Name;
            problemDetails.Extensions["stackTrace"] = exception.StackTrace;

            if (exception.InnerException != null)
            {
                problemDetails.Extensions["innerException"] = new
                {
                    message = exception.InnerException.Message,
                    type = exception.InnerException.GetType().Name
                };
            }
        }
    }
}
