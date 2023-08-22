using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectMGN.DTOS.Request;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;

namespace ProjectMGN.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ConfigurationController : ControllerBase

    {
        private readonly IConfigurationService _configurationService;
        private readonly IToken _tokenService;

        public ConfigurationController(IConfigurationService configuration,IToken tokenService)
        {
            _configurationService = configuration;
            _tokenService = tokenService;
        }
        [Authorize]
        [HttpPost("createConfiguration")]
        public IActionResult CreateConfiguration(Configuration configuration)
        {
            string token = HttpContext.Request.Headers.Authorization;
            var cleanToken = token.Split(" ")[1];
            var ownerId = _tokenService.UserIdFromToken(cleanToken);
          
            _configurationService.CreateConfiguration(configuration, ownerId);
            return NoContent();
        }
        [Authorize]
        [HttpGet("configurations")]
        public IActionResult GetAllConfigurations()
        {
            string token = HttpContext.Request.Headers.Authorization;
            var cleanToken = token.Split(" ")[1];
            var ownerId = _tokenService.UserIdFromToken(cleanToken);
            var data = _configurationService.GetAllConfigurations(ownerId);
            return Ok(new { data });
        }
        [Authorize]
        [HttpDelete("deleteConfiguration/{configurationId:int}")]
        public IActionResult DeleteConfiguration(int configurationId)
        {
            string token = HttpContext.Request.Headers.Authorization;
            var cleanToken = token.Split(" ")[1];
            var ownerId = _tokenService.UserIdFromToken(cleanToken);
            _configurationService.DeleteConfiguration(ownerId, configurationId);
            return NoContent();
        }

        [Authorize]
        [HttpPatch("updateConfiguration/{configurationId:int}")]
        public IActionResult UpdateConfiguration(UpdateConfigurationRequest configurationRequestResponse,int configurationId)
        {
            _configurationService.UpdateConfiguration(configurationRequestResponse,configurationId);
            return NoContent();
        }
        [Authorize]
        [HttpGet("getConfiguration/{configurationId:int}")]
        public IActionResult GetConfigurationById( int configurationId)
        {
            string token = HttpContext.Request.Headers.Authorization;
            var cleanToken = token.Split(" ")[1];
            Console.WriteLine("token"  + token);
            var ownerId = _tokenService.UserIdFromToken(cleanToken);
            return Ok(_configurationService.GetConfigurationById(ownerId, configurationId));
        }
    }
}
