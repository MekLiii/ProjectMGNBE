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
        private bool IsBase64String(string base64string)
        {
            try
            {
                Convert.FromBase64String(base64string);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public void CreateProject(Projects projects, int OwnerId)
        {
            if (projects.Image != null && IsBase64String(projects.Image))
            {
                throw new InvalidOperationException("Image is not base64 string");
            }
            _projectsRepository.CreateProject(projects, OwnerId);
            return;
        }
        public List<Projects> GetAllProjects(int OwnerId)
        {
            return _projectsRepository.GetAllProjects(OwnerId);
        }
    }
}
