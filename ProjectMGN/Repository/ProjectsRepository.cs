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

        public List<Project> GetAllProjects(int ownerId)
        {
            var projects = _dbContext.Projects.Where(proj => proj.OwnerId == ownerId).ToList();
            return projects;
        }
        public Project GetProjectById(int ownerId, int projectId)
        {
            var project = _dbContext.Projects.FirstOrDefault(project => project.Id == projectId);
            if (project == null)
            {
                throw new InvalidCastException("Project not found");
            }
            return project;
        }
        public void CreateProject(Project project, int ownerId)
        {
            var isProjectExists = _dbContext.Projects.FirstOrDefault(projectFromDb => projectFromDb.OwnerId == ownerId && projectFromDb.Name == project.Name);
            if (isProjectExists != null)
            {
                throw new InvalidOperationException("Project already exists");
            }
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
        }
        public void DeleteProject(int ownerId, int projectId)
        {
            var projectToDelete = _dbContext.Projects.FirstOrDefault(project => project.Id == projectId);
            if (projectToDelete == null)
            {
                throw new InvalidCastException("Project not found");
            }
            _dbContext.Projects.Remove(projectToDelete);
        }
        
    }
}
