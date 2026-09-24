using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Train_Project.Filters
{
    public class RequestTimingFilter : IActionFilter
    {
        private const string StopwatchKey = "__RequestTimingFilter_Stopwatch";
        private readonly ILogger<RequestTimingFilter> _logger;

        public RequestTimingFilter(ILogger<RequestTimingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items[StopwatchKey] = Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Items[StopwatchKey] is not Stopwatch stopwatch)
                return;

            stopwatch.Stop();

            _logger.LogInformation(
                "Action {Action} {Method} {Path} completed with {StatusCode} in {ElapsedMs} ms",
                context.ActionDescriptor.DisplayName,
                context.HttpContext.Request.Method,
                context.HttpContext.Request.Path,
                context.HttpContext.Response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }
    }
}
