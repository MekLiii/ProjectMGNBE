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
        [HttpPost("addProject/{ownerId}")]
        public IActionResult CreateProject(AddProjectRequest project, int OwnerId)
        {
            try
            {
                Projects projects = new Projects()
                {
                    ConfigurationId = null,
                    Name = project.ProjectName,
                    Image = project.Image,
                    OwnerId = OwnerId,
                    Guid = Guid.NewGuid().ToString()
                };
                _projectsService.CreateProject(projects, OwnerId);
                return NoContent();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return BadRequest(new { message });
            }
        }
        [Authorize]
        [HttpGet("{ownerId}")]
        [ValidateUserId]
        public IActionResult GetAllProjects(int OwnerId)
        {
            try
            {
                List<Projects> data = _projectsService.GetAllProjects(OwnerId);
                return Ok(new { data });
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return BadRequest(new { message });
            }
        }
        [HttpDelete("{ownerId}/{projectId}")]
        [ValidateUserId]
        [Authorize]
        public IActionResult DeleteProject(int OwnerId, int ProjectId)
        {
            try
            {
                _projectsService.DeleteProject(OwnerId, ProjectId);
                return NoContent();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return BadRequest(new { message });
            }
        }

    }
}
