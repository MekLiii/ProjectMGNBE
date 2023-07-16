using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectMGN.Db;
using ProjectMGN.DTOS.Request;
using ProjectMGN.DTOS.Response;
using ProjectMGN.Interfaces;
using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;
using ProjectMGN.Services;

namespace ProjectMGN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
      
        public UsersController(IUserService userService)
        {
            _userService = userService;
            
        }

        [AllowAnonymous]
        //[Authorize]
        [HttpPost("createuser")]
        public IActionResult RegisterUser(User user)
        {
            try
            {
                _userService.RegisterUser(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            try
            {
                LoginResponse user =  _userService.LoginService(loginRequest);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok("Test");
        }
    }
}
