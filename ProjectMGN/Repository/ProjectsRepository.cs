using ProjectMGN.Db;
using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.Models;

namespace ProjectMGN.Repository
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly ProjectMGNDB _dbContext;

        public ProjectsRepository(ProjectMGNDB dbContext)
        {
            _dbContext = dbContext;
        }

        public Projects GetAllProjects(int OwnerId)
        {
            Projects projects = _dbContext.Projects.FirstOrDefault(project => project.OwnerId == OwnerId);
            return projects;
        }
        public void CreateProject(Projects project)
        {
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
        }
    }
}
