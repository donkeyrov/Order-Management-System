using Microsoft.AspNetCore.Diagnostics;
using OrderMgt.Model.Models;

namespace OrderMgt.API.Extensions
{
    /// <summary>
    /// Provides functionality to handle exceptions in an HTTP context by generating a standardized error response.
    /// </summary>
    /// <remarks>This class implements the <see cref="IExceptionHandler"/> interface to handle exceptions that
    /// occur during HTTP request processing. It writes a JSON-formatted error response to the HTTP response body and
    /// returns a value indicating whether the exception was handled successfully.</remarks>
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
