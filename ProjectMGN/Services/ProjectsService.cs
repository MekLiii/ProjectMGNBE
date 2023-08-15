using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;

namespace ProjectMGN.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;
        private readonly IProjectConfiguration _projectConfiguration;
        public ProjectsService(IProjectsRepository projectsRepository,IProjectConfiguration projectConfiguration)
        {
            _projectsRepository = projectsRepository;
            _projectConfiguration = projectConfiguration;
        }
        
        public void CreateProject(Project project, int ownerId)
        {
            if (project.ConfigurationId != null)
            {
                _projectConfiguration.GetConfigurationById(ownerId,(int)project.ConfigurationId);
            }
            
            Project newProject = new()
            {
                ConfigurationId = project.ConfigurationId,
                Name = project.Name,
                Image = project.Image,
                OwnerId = ownerId,
                Guid = Guid.NewGuid().ToString()
            };
            
            _projectsRepository.CreateProject(newProject, ownerId);
        }
        public List<Project> GetAllProjects(int ownerId)
        {
            return _projectsRepository.GetAllProjects(ownerId);
        }
        public void DeleteProject(int ownerId,int projectId)
        {
            _projectsRepository.DeleteProject(ownerId, projectId);
        }
        public Project GetProjectById(int ownerId,int projectId)
        {
            return _projectsRepository.GetProjectById(ownerId, projectId);
        }
    }
}
