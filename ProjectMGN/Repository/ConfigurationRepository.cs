using ProjectMGN.Db;
using ProjectMGN.Models;
using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.DTOS.Response;

namespace ProjectMGN.Repository
{
    public class ConfigurationRepository : IProjectConfiguration
    {
        private readonly ProjectMGNDB _dbContext;

        public ConfigurationRepository(ProjectMGNDB dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Configuration> GetAllConfigurations(int OwnerId)
        {
            List<Configuration> configurations = _dbContext.Configuration.Where(config => config.OwnerId == OwnerId).ToList();
            Console.WriteLine(configurations);
            return configurations;
        }
        public void CreateConfiguration(Configuration configuration, int OwnerId)
        {
            Configuration isConfigurationExists = _dbContext.Configuration.FirstOrDefault(configurationFromDb => configurationFromDb.OwnerId == OwnerId && configurationFromDb.Name == configuration.Name);
            if (isConfigurationExists != null)
            {
                throw new InvalidOperationException("Configuration already exists");
            }
            _dbContext.Configuration.Add(configuration);
            _dbContext.SaveChanges();
        }
        public void DeleteConfiguration(int ownerId, int configurationId)
        {
            Configuration configurationToDelete = _dbContext.Configuration.FirstOrDefault(configuration => configuration.Id == configurationId);
            if (configurationToDelete == null)
            {
                throw new InvalidCastException("Configuration not found");
            }
            _dbContext.Configuration.Remove(configurationToDelete);
        }
        public Configuration GetConfigurationById(int ownerId, int configurationId)
        {
            Configuration configuration = _dbContext.Configuration.FirstOrDefault(configuration => configuration.Id == configurationId);
            if (configuration == null)
            {
                throw new InvalidCastException("Configuration not found");
            }
            return configuration;
        }
        public List<ActionResponse> GetActions(int configurationId)
        {
            List<ActionResponse> actions = _dbContext.Actions.Where(action => action.ConfigurationId == configurationId)
               .Select(action => new ActionResponse
               {
                   ActionName = action.ActionName,
                   Command = "test", 
                   Id = action.Id,
               })
                .ToList();
            return actions;
        }

    }
}
