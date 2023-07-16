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
                Console.WriteLine(context.Request.Headers["Authorization"]);
                var JWTtoken = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                Console.WriteLine(JWTtoken);
                var userId = token.ValidateToken(JWTtoken);
                Console.WriteLine($"JWT token: {userId}");
                if (userId != null)
                {
                    context.Items["User"] = userService.GetUserById(userId.Value);
                    Console.WriteLine(context.Items["User"] + "test");
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
