using Notifications.Infraestruture.Correlation;

namespace Notifications.Api.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ICorrelationIdSentinel correlationIdSentinel)
        {
            var correlationId = GetCorrelationIdTrace(context, correlationIdSentinel);

            AddToResponse(context, correlationIdSentinel.GetHeaderName(), correlationId);

            await _next(context);
        }

        private static string GetCorrelationIdTrace(HttpContext context, ICorrelationIdSentinel correlationIdSentinel)
        {
            if (context.Request.Headers.TryGetValue(correlationIdSentinel.GetHeaderName(), out var correlationId))
            {
                correlationIdSentinel.Set(correlationId!);

                return correlationId!;
            }

            return correlationIdSentinel.Get();
        }

        private static void AddToResponse(HttpContext context, string HeaderName, string correlationId)
        {
            context.Response.OnStarting(() =>
            {

                context.Response.Headers.Append(HeaderName, new[] { correlationId });
                return Task.CompletedTask;
            });
        }
    }
 
}
