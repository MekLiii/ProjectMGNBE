using ProjectMGN.Db;
using ProjectMGN.Models;
using ProjectMGN.Interfaces.Repositories;

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
            List<Configuration> configurations = _dbContext.Configurations.Where(config => config.OwnerId == OwnerId).ToList();
            return configurations;
        }
        public void CreateConfiguration(Configuration configuration, int OwnerId)
        {
            Configuration isConfigurationExists = _dbContext.Configurations.FirstOrDefault(configurationFromDb => configurationFromDb.OwnerId == OwnerId && configurationFromDb.Name == configuration.Name);
            if (isConfigurationExists != null)
            {
                throw new InvalidOperationException("Configuration already exists");
            }
            _dbContext.Configurations.Add(configuration);
            _dbContext.SaveChanges();
        }
        public void DeleteConfiguration(int ownerId, int configurationId)
        {
            Configuration configurationToDelete = _dbContext.Configurations.FirstOrDefault(configuration => configuration.Id == configurationId);
            if (configurationToDelete == null)
            {
                throw new InvalidCastException("Configuration not found");
            }
            _dbContext.Configurations.Remove(configurationToDelete);
        }
        public Configuration GetConfigurationById(int ownerId, int configurationId)
        {
            Configuration configuration = _dbContext.Configurations.FirstOrDefault(configuration => configuration.Id == configurationId);
            if (configuration == null)
            {
                throw new InvalidCastException("Configuration not found");
            }
            return configuration;
        }   
    }
}
