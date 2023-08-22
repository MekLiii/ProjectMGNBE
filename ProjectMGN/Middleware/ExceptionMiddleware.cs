

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectMGN.DTOS.Response;
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
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                var errorResponse = new MessageResponse()
                {
                    Message = new MessageResponse.MessageDto()
                    {
                        Type = "ERROR",
                        Content = ex.Message
                    },
                };
                var result = JsonSerializer.Serialize(errorResponse);
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(result);
            }
        }
    }
}
