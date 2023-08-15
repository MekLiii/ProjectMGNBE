using ProjectMGN.Db;
using ProjectMGN.DTOS.Request;
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
            List<Configuration> configurations =
                _dbContext.Configuration.Where(config => config.OwnerId == OwnerId).ToList();
            Console.WriteLine(configurations);
            return configurations;
        }

        public Configuration GetConfigurationById(int ownerId, int configurationId)
        {
            Console.WriteLine($"ownerId{ownerId}, configId ${configurationId}");
            Configuration configuration =
                _dbContext.Configuration.FirstOrDefault(configuration => configuration.Id == configurationId);
            if (configuration == null)
            {
                throw new ArgumentException("Configuration not found");
            }

            return configuration;
        }

        public void CreateConfiguration(Configuration configuration, int OwnerId)
        {
            var isConfigurationExists = _dbContext.Configuration.FirstOrDefault(configurationFromDb =>
                configurationFromDb.OwnerId == OwnerId && configurationFromDb.Name == configuration.Name);
            if (isConfigurationExists != null)
            {
                throw new ArgumentException("Configuration already exists");
            }

            _dbContext.Configuration.Add(configuration);
            _dbContext.SaveChanges();
        }

        public void UpdateConfiguration(UpdateConfigurationRequest updatedConfigurationRequest,int configurationId)
        {
            var existingConfiguration = _dbContext.Configuration.FirstOrDefault(configurationFromDb =>
                configurationFromDb.Id == configurationId);

            if (existingConfiguration == null)
            {
                throw new ArgumentException("Configuration does not exist");
            }

            if (updatedConfigurationRequest.Name == null || updatedConfigurationRequest.Name.Length < 3)
            {
                throw new ArgumentException("Invalid name");
            }

            existingConfiguration.Name = updatedConfigurationRequest.Name;
            _dbContext.SaveChanges();
        }

        public void DeleteConfiguration(int ownerId, int configurationId)
        {
            var configurationToDelete =
                _dbContext.Configuration.FirstOrDefault(configuration => configuration.Id == configurationId);
            if (configurationToDelete == null)
            {
                throw new ArgumentException("Configuration not found");
            }

            _dbContext.Configuration.Remove(configurationToDelete);
            _dbContext.SaveChanges();
        }


        public List<ActionResponse> GetActions(int configurationId)
        {
            List<ActionResponse> actions = _dbContext.Actions
                .Where(action => action.ConfigurationId == configurationId)
                .Join(
                    _dbContext.Comands,
                    action => action.commandId,
                    command => command.Id,
                    (action, command) => new ActionResponse
                    {
                        ActionName = action.ActionName,
                        Command = command.Comand,
                        Id = action.Id,
                    })
                .ToList();

            return actions;
        }
    }
}