using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace ProjectMGN.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = exception.Message;
            var responseObj = new { message };

            string jsonResponse = JsonSerializer.Serialize(responseObj);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
