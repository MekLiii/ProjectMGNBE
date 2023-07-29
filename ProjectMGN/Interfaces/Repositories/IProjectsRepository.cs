using ProjectMGN.Models;

namespace ProjectMGN.Interfaces.Repositories
{
    public interface IProjectsRepository
    {
        public void CreateProject(Project project,int OwnerId);
        public List<Project> GetAllProjects(int OwnerId);
        public void DeleteProject(int OwnerId,int ProjectId);
    }
}
