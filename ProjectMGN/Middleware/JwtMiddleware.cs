using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectMGN.Interfaces.Services;

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
            try
            {
              
                var JWTtoken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = token.ValidateToken(JWTtoken);
                Console.WriteLine("JWT token");
                if (userId != null)
                {
                    context.Items["User"] = userService.GetUserById(userId.Value);
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                //make a response with status code 400
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new { message = ex.Message }));
            }
        }
    }
}
