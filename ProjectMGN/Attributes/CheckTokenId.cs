using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjectMGN.Interfaces.Services;
using System.IdentityModel.Tokens.Jwt;

namespace ProjectMGN.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ValidateUserIdAttribute : ActionFilterAttribute
    {
        
        private int GetuserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid");
            int userId = Int32.Parse(userIdClaim?.Value);

            return userId;
        }



        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string token = context.HttpContext.Request.Headers.Authorization.ToString().Split(" ").Last();
                int? userId = GetuserIdFromToken(token);

                int? ownerId = (int)context.ActionArguments["ownerId"];
                Console.WriteLine("ownerId" + ownerId);
                if (ownerId.GetType() != typeof(int))
                {
                    context.Result = new BadRequestResult();
                    return;
                }
                if (ownerId != userId)
                {
                    context.Result = new UnauthorizedResult();
                }
                base.OnActionExecuting(context);
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine(message);
                context.Result = new BadRequestResult();
            }
        }
    }
}
