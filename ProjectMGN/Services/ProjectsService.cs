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
        
        public void CreateProject(Projects projects, int OwnerId)
        {
            _projectsRepository.CreateProject(projects, OwnerId);
            return;
        }
        public List<Projects> GetAllProjects(int OwnerId)
        {
            return _projectsRepository.GetAllProjects(OwnerId);
        }
    }
}
