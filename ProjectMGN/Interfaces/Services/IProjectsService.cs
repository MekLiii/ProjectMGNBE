using ProjectMGN.Models;

namespace ProjectMGN.Interfaces.Services
{
    public interface IProjectsService
    {
        public void CreateProject(Projects projects,int OwnerId);
        public List<Projects> GetAllProjects(int OwnerId);
        public void DeleteProject(int OwnerId,int ProjectId);
    }
}
