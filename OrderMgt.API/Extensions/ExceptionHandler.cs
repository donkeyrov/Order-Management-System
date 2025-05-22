using Microsoft.AspNetCore.Diagnostics;
using OrderMgt.Model.Models;

namespace OrderMgt.API.Extensions
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = new ErrorResponse()
            { 
                StatusCode = httpContext.Response.StatusCode,
                Title = "Internal Server Error.",
                ExceptionMessage = exception.Message
            };

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken: cancellationToken);
            return true;
        }
    }
}
