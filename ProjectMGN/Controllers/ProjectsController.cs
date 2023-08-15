using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectMGN.Attributes;
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

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;

        }
        [Authorize]
        [ValidateUserId]
        [HttpPost("addProject/{ownerId}")]
        public IActionResult CreateProject(AddProjectRequest project, int OwnerId)
        {
            Project _project = new()
            {
                ConfigurationId = project.ConfigurationId,
                Name = project.ProjectName,
                Image = project.Image,
                OwnerId = OwnerId,
                Guid = Guid.NewGuid().ToString()
            };

            _projectsService.CreateProject(_project, OwnerId);
            return NoContent();
        }
        [Authorize]
        [HttpGet("{ownerId}")]
        [ValidateUserId]
        public IActionResult GetAllProjects(int OwnerId)
        {
            List<Project> data = _projectsService.GetAllProjects(OwnerId);
            return Ok(new { data });
        }
        [HttpDelete("{ownerId}/{projectId}")]
        [ValidateUserId]
        [Authorize]
        public IActionResult DeleteProject(int OwnerId, int ProjectId)
        {
            _projectsService.DeleteProject(OwnerId, ProjectId);
            return NoContent();
        }
        [HttpGet("getProject/{ownerId}/{projectId}")]
        [ValidateUserId]
        [Authorize]
        public IActionResult GetProject(int OwnerId, int ProjectId)
        {
            Project data = _projectsService.GetProjectById(OwnerId, ProjectId);
            return Ok(new { data });
        }

    }
}
