using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;

namespace ProjectMGN.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;
        public ProjectsService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }
        
        public void CreateProject(Project project, int OwnerId)
        {
            Project _project = new()
            {
                ConfigurationId = null,
                Name = project.Name,
                Image = project.Image,
                OwnerId = OwnerId,
                Guid = Guid.NewGuid().ToString()
            };
            _projectsRepository.CreateProject(_project, OwnerId);
            return;
        }
        public List<Project> GetAllProjects(int OwnerId)
        {
            return _projectsRepository.GetAllProjects(OwnerId);
        }
        public void DeleteProject(int OwnerId,int ProjectId)
        {
            _projectsRepository.DeleteProject(OwnerId, ProjectId);
            return;
        }
    }
}
