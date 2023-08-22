using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectMGN.DTOS.Request;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;
using ProjectMGN.Messages;


namespace ProjectMGN.Controllers
{
    [Route("api")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;


        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult RegisterUser(RegisterUserRequest user)
        {
            _userService.RegisterUser(user);
            var message = new CustomMessages().UserCreatedSuccessFully;
            return Ok(new { message });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var user = _userService.LoginService(loginRequest);
            var data = user;

            return Ok(new { data });
        }
    }
}