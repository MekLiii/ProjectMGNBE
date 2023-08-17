using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectMGN.Attributes;
using ProjectMGN.DTOS.Request;
using ProjectMGN.DTOS.Response;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;

namespace ProjectMGN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase

    {
        private readonly IConfigurationService _configurationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConfigurationController(IConfigurationService configuration,IHttpContextAccessor httpContextAccessor)
        {
            _configurationService = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize]
        [HttpPost("createConfiguration")]
        [ValidateUserId]
        public IActionResult CreateConfiguration(Configuration configuration)
        {
            var ownerId = _httpContextAccessor.HttpContext.Request.Headers["x-ownerId"];
            if (ownerId.Count == 0)
            {
                throw new AggregateException("Invalid owner id");
            }
            _configurationService.CreateConfiguration(configuration, int.Parse(ownerId));
            return NoContent();
        }
        [Authorize]
        [HttpGet("getConfigurations")]
        [ValidateUserId]
        public IActionResult GetAllConfigurations()
        {
            var ownerId = _httpContextAccessor.HttpContext.Request.Headers["x-ownerId"];
            if (ownerId.Count == 0)
            {
                throw new AggregateException("Invalid owner id");
            }
            var data = _configurationService.GetAllConfigurations(int.Parse(ownerId));
            return Ok(new { data });
        }
        [Authorize]
        [HttpDelete("delteConfiguration/{configurationId}")]
        [ValidateUserId]
        public IActionResult DeleteConfiguration(int configurationId)
        {
            var ownerId = _httpContextAccessor.HttpContext.Request.Headers["x-ownerId"];
            if (ownerId.Count == 0)
            {
                throw new AggregateException("Invalid owner id");
            }
            _configurationService.DeleteConfiguration(int.Parse(ownerId), configurationId);
            return NoContent();
        }

        [Authorize]
        [HttpPatch("updateConfiguration/{configurationId}")]
        public IActionResult UpdateConfiguration(UpdateConfigurationRequest configurationRequestResponse,int configurationId)
        {
            _configurationService.UpdateConfiguration(configurationRequestResponse,configurationId);
            return NoContent();
        }
        [Authorize]
        [HttpGet("getConfiguration/{ownerId}/{configurationId}")]
        [ValidateUserId]
        public IActionResult GetConfigurationById( int configurationId)
        {
            var ownerId = _httpContextAccessor.HttpContext.Request.Headers["x-ownerId"];
            if (ownerId.Count == 0)
            {
                throw new AggregateException("Invalid owner id");
            }
            return Ok(_configurationService.GetConfigurationById(int.Parse(ownerId), configurationId));
        }
    }
}
