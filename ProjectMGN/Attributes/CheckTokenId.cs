using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectMGN.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;

namespace ProjectMGN.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateUserIdAttribute : ActionFilterAttribute
    {

        private int GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null)
            {
                throw new Exception("Token is null");
            }
            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid");
            if (userIdClaim?.Value == null)
            {
                throw new Exception("Something went wrong with token");
            }
            var userId = int.Parse(userIdClaim.Value);
            var test = float.Parse("1.2");

            return userId;
        }



        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string message = "Invalid user id";
            try
            {
                string token = context.HttpContext.Request.Headers.Authorization.ToString().Split(" ").Last();
                int? userId = GetUserIdFromToken(token);

                if (context.ActionArguments["ownerId"] == null)
                {
                    context.Result = new BadRequestObjectResult(new { message });
                }

                int? ownerId = (int)context.ActionArguments["ownerId"];
                if (ownerId.GetType() != typeof(int))
                {
                    context.Result = new BadRequestObjectResult(new { message });
                }
                if (ownerId != userId)
                {
                    context.Result = new BadRequestObjectResult(new { message });
                }
                base.OnActionExecuting(context);
            }
            catch
            {
                context.Result = new BadRequestObjectResult(new { message });
            }
        }
    }
}
