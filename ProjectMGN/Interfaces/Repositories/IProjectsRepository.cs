using ProjectMGN.Models;

namespace ProjectMGN.Interfaces.Repositories
{
    public interface IProjectsRepository
    {
        public void CreateProject(Projects projects,int OwnerId);
        public List<Projects> GetAllProjects(int OwnerId);
    }
}
