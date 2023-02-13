using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Templ.API.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            return context.Response.WriteAsync( JsonSerializer.Serialize( new {
                context.Response.StatusCode,
                exception.Message
            }));
        }
    }
}
