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
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;
        private readonly IToken _tokenService;

        public ProjectsController(IProjectsService projectsService, IToken tokenService)
        {
            _projectsService = projectsService;
            _tokenService = tokenService;
        }

        [Authorize]
        [HttpPost("/addProject")]
        public IActionResult CreateProject(AddProjectRequest project)
        {
            string token = HttpContext.Request.Headers.Authorization;
            var cleanToken = token.Split(" ")[1];
            var ownerId = _tokenService.UserIdFromToken(cleanToken);
            Project newProject = new()
            {
                ConfigurationId = project.ConfigurationId,
                Name = project.ProjectName,
                Image = project.Image,
                OwnerId = ownerId,
                Guid = Guid.NewGuid().ToString()
            };

            _projectsService.CreateProject(newProject, ownerId);
            return NoContent();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            string token = HttpContext.Request.Headers.Authorization;
            var cleanToken = token.Split(" ")[1];
            var ownerId = _tokenService.UserIdFromToken(cleanToken);
            var data = _projectsService.GetAllProjects(ownerId);
            return Ok(new { data });
        }

        [HttpDelete("/{projectId:int}")]
        [Authorize]
        public IActionResult DeleteProject(int ownerId, int projectId)
        {
            _projectsService.DeleteProject(ownerId, projectId);
            return NoContent();
        }

        [HttpGet("getProject/{projectId:int}")]
        [Authorize]
        public IActionResult GetProject(int projectId)
        {
            string token = HttpContext.Request.Headers.Authorization;
            var cleanToken = token.Split(" ")[1];
            var ownerId = _tokenService.UserIdFromToken(cleanToken);
            var data = _projectsService.GetProjectById(ownerId, projectId);
            return Ok(new { data });
        }
    }
}