using System.Collections.Concurrent;

namespace Train_Project.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly ConcurrentDictionary<string, RequestInfo> Requests = new();

        private const int MaxRequests = 3;
        private static readonly TimeSpan TimeWindow = TimeSpan.FromSeconds(10);

        public RateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            var now = DateTime.UtcNow;


            var requestInfo = Requests.GetOrAdd(
                ipAddress,
                _ => new RequestInfo
                {
                    Count = 0,
                    WindowStart = now
                });

            lock (requestInfo)
            {
                if (now - requestInfo.WindowStart >= TimeWindow)
                {
                    requestInfo.Count = 0;
                    requestInfo.WindowStart = now;
                }

                requestInfo.Count++;

                if (requestInfo.Count > MaxRequests)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    httpContext.Response.ContentType = "text/plain";

                    return;
                }
            }

            await _next(httpContext);
        }

        private class RequestInfo
        {
            public int Count { get; set; }
            public DateTime WindowStart { get; set; }
        }
    }
}