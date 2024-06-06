namespace Web.Middleware
{
    public class CounterMiddleware
    {
        RequestDelegate _next;

        public CounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, CounterClass counter)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                counter.Value++;
            }

            await _next(context);
        }
    }
}
