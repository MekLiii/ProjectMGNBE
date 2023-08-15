﻿using Microsoft.AspNetCore.Authorization;
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

        public ConfigurationController(IConfigurationService configuration)
        {
            _configurationService = configuration;
        }
        [Authorize]
        [HttpPost("createConfiguration/{ownerId}")]
        [ValidateUserId]
        public IActionResult CreateConfiguration(Configuration configuration, int ownerId)
        {
            _configurationService.CreateConfiguration(configuration, ownerId);
            return NoContent();
        }
        [Authorize]
        [HttpGet("getConfigurations/{OwnerId}")]
        [ValidateUserId]
        public IActionResult GetAllConfigurations(int ownerId)
        {
            var data = _configurationService.GetAllConfigurations(ownerId);
            return Ok(new { data });
        }
        [Authorize]
        [HttpDelete("delteConfiguration/{ownerId}/{configurationId}")]
        [ValidateUserId]
        public IActionResult DeleteConfiguration(int ownerId, int configurationId)
        {
            _configurationService.DeleteConfiguration(ownerId, configurationId);
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
        public IActionResult GetConfigurationById(int ownerId, int configurationId)
        {
            return Ok(_configurationService.GetConfigurationById(ownerId, configurationId));
        }
    }
}
