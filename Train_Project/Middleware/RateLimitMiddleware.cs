namespace Train_Project.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;

        private static int Counter = 0;

        private static DateTime LastRequestTime = DateTime.Now;

        public RateLimitMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Counter++;
            if (DateTime.Now.Subtract(LastRequestTime).Seconds > 10)
            {
                Counter = 1;
                LastRequestTime = DateTime.Now;
                await _next(httpContext);

            }
            else
            {
                if (Counter > 3)
                {
                    LastRequestTime = DateTime.Now;
                    await httpContext.Response.WriteAsync("Rate limit exceeded. Please try again later.");
                }
                else
                {
                    LastRequestTime = DateTime.Now;
                    await _next(httpContext);
                }
            }
        }
    }
}
