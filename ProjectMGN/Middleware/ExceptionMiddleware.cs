using System.Net;
using System.Text.Json;
using ProjectMGN.Models;

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
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                MessagesModel message = new()
                {
                    Content = ex.Message,
                    Status = "ERROR"
                };
                var result = JsonSerializer.Serialize(new { message });
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(result);
            }
        }
    }
}