using System.Collections.Concurrent;

namespace Train_Project.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;

        // Keep a separate request counter for each IP address.
        private static readonly ConcurrentDictionary<string, RequestInfo> Requests = new();

        // Maximum number of requests allowed from one IP in the time window.
        private const int MaxRequests = 3;
        private static readonly TimeSpan TimeWindow = TimeSpan.FromSeconds(10);

        public RateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            // Get the client's IP address.
            var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            var now = DateTime.UtcNow;

            // Get the existing request information for this IP,
            // or create a new entry if this is the first request.
            var requestInfo = Requests.GetOrAdd(
                ipAddress,
                _ => new RequestInfo
                {
                    Count = 0,
                    WindowStart = now
                });

            lock (requestInfo)
            {
                // If the 10-second window has expired, start a new window.
                if (now - requestInfo.WindowStart >= TimeWindow)
                {
                    requestInfo.Count = 0;
                    requestInfo.WindowStart = now;
                }

                requestInfo.Count++;

                // If the IP has exceeded the limit, stop the request.
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
