namespace Notifications.Api.Middleware
{
    public class CorrelationIdMiddleware
    {
        private const string _correlationIdHeader = "X-Correlation-Id";

        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ICorrelationIdSentinel correlationIdSentinel)
        {
            var correlationId = GetCorrelationIdTrace(context, correlationIdSentinel);
            AddToResponse(context, correlationId);

            await _next(context);
        }

        private static string GetCorrelationIdTrace(HttpContext context, ICorrelationIdSentinel correlationIdSentinel)
        {
            if (context.Request.Headers.TryGetValue(_correlationIdHeader, out var correlationId))
            {
                correlationIdSentinel.Set(correlationId!);

                return correlationId!;
            }

            return correlationIdSentinel.Get();
        }

        private static void AddToResponse(HttpContext context, string correlationId)
        {

            context.Response.OnStarting(() => {

                context.Response.Headers.Append(_correlationIdHeader, new[] { correlationId });
                return Task.CompletedTask;
            });


        }
    }
    public class CorrelationIdSentinel : ICorrelationIdSentinel
    {
        private string _correlationId = Guid.NewGuid().ToString();
        private const string CorrelationIdHeader = "X-Correlation-Id";
        public string Get() => _correlationId;
        public string GetHeaderName() => CorrelationIdHeader;
        public void Set(string correlationId) => _correlationId = correlationId;
    }
    public interface ICorrelationIdSentinel
    {
        public string Get();
        public void Set(string correlationId);

        public string GetHeaderName();
    }
}
