using ProjectMGN.Models;

namespace ProjectMGN.Interfaces.Services
{
    public interface IProjectsService
    {
        public void CreateProject(Project project,int OwnerId);
        public List<Project> GetAllProjects(int OwnerId);
        public void DeleteProject(int OwnerId,int ProjectId);
        public Project GetProjectById(int OwnerId,int ProjectId);
    }
}
