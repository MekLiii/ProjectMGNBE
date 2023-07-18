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

        public List<Projects> GetAllProjects(int OwnerId)
        {
            List<Projects> projects = _dbContext.Projects.Where(proj => proj.OwnerId == OwnerId).ToList();
            return projects;
        }
        public void CreateProject(Projects project,int OwnerId)
        {
            Projects isProjectExists = _dbContext.Projects.FirstOrDefault(projectFromDb => projectFromDb.OwnerId == OwnerId && projectFromDb.Name == project.Name );
            if(isProjectExists != null) {
                throw new InvalidOperationException("Project already exists");
            }
            _dbContext.Projects.Add(project);
            _dbContext.SaveChanges();
        }
    }
}
