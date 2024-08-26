using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Notifications.Api.Middleware
{
    public class GlobalExceptionHandler(IHostEnvironment env) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = CreateProblemDetails(httpContext, exception);

            await httpContext.Response.WriteAsJsonAsync(problemDetails, options: null, contentType: "application/problem+json", cancellationToken: cancellationToken);

            return true;
        }

        public ProblemDetails CreateProblemDetails(in HttpContext context, in Exception exception)
        {

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = exception.GetType().Name,
                Title = "A problem has ocurred while your request is being processed",
            };

            if (!env.IsDevelopment()) return problemDetails;

            problemDetails.Detail = exception.Message;
            problemDetails.Extensions["CorrelationId"] = Guid.NewGuid().ToString();
            problemDetails.Extensions["Date"] = $"{DateTime.Now:dd:mm:yyyy:hh:mm:ss}";
            problemDetails.Instance = context.Request.Path;

            return problemDetails;

        }
    }
}
