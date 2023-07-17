using ProjectMGN.Models;

namespace ProjectMGN.Interfaces.Repositories
{
    public interface IProjectsRepository
    {
        public void CreateProject(Projects projects);
        public Projects GetAllProjects(int OwnerId);
    }
}
