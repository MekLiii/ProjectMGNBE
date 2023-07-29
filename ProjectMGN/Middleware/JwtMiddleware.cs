using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ProjectMGN.Interfaces.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMGN.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IToken token)
        {
            if (!context.Request.Path.StartsWithSegments("/api/registerUser"))
            {
                var authorizationHeader = context.Request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                {
                    try
                    {
                        var JWTtoken = authorizationHeader.Substring("Bearer ".Length);
                        var userId = token.ValidateToken(JWTtoken);
                        if (userId != null)
                        {
                            context.Items["User"] = userService.GetUserById(userId.Value);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle token validation errors
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { message = ex.Message }));
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
