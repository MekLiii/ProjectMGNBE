using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectMGN.Attributes;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;

namespace ProjectMGN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase

    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(IConfigurationService configuration)
        {
            _configurationService = configuration;
        }
        [Authorize]
        [HttpPost("createConfiguration/{OwnerId}")]
        [ValidateUserId]
        public IActionResult CreateConfiguration(Configuration configuration, int OwnerId)
        {
            _configurationService.CreateConfiguration(configuration, OwnerId);
            return NoContent();
        }
        [Authorize]
        [HttpGet("getConfigurations/{OwnerId}")]
        [ValidateUserId]
        public IActionResult GetAllConfigurations(int OwnerId)
        {
            List<Configuration> data = _configurationService.GetAllConfigurations(OwnerId);
            return Ok(new { data });
        }
        [Authorize]
        [HttpDelete("delteConfiguration/${ownerId}/${configurationId}")]
        [ValidateUserId]
        public IActionResult DeleteConfiguration(int ownerId, int configurationId)
        {
            _configurationService.DeleteConfiguration(ownerId, configurationId);
            return NoContent();
        }
        [Authorize]
        [HttpGet("/configuration/${ownerId}/${configurationId}")]
        [ValidateUserId]
        public IActionResult GetConfigurationById(int ownerId, int configurationId)
        {
            return Ok(_configurationService.GetConfigurationById(ownerId, configurationId));
        }
    }
}
