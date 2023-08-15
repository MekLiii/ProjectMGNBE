using ProjectMGN.DTOS.Response;
using ProjectMGN.Interfaces.Repositories;
using ProjectMGN.Interfaces.Services;
using ProjectMGN.Models;

namespace ProjectMGN.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IProjectConfiguration _projectConfiguration;

        public ConfigurationService(IProjectConfiguration projectConfiguration)
        {

            _projectConfiguration = projectConfiguration;
        }

        public void CreateConfiguration(Configuration configuration, int OwnerId)
        {
            _projectConfiguration.CreateConfiguration(configuration, OwnerId);
        }
        public List<ConfigurationResponse> GetAllConfigurations(int OwnerId)
        {
            List<Configuration> configurations = _projectConfiguration.GetAllConfigurations(OwnerId);
            List<ConfigurationResponse> configurationsWithActions = new List<ConfigurationResponse>() { };
            foreach (Configuration configuration in configurations)
            {
                ConfigurationResponse configurationWithAction = new ConfigurationResponse()
                {
                    Id = configuration.Id,
                    Name = configuration.Name,
                    Actions = _projectConfiguration.GetActions((int)configuration.Id),
                };
                configurationsWithActions.Add(configurationWithAction);
            }

            return configurationsWithActions;
        }
        public Configuration GetConfigurationById(int ownerId, int configurationId)
        {
            
            return _projectConfiguration.GetConfigurationById(ownerId, configurationId);
        }
        public void DeleteConfiguration(int ownerId, int configurationId)
        {
            _projectConfiguration.DeleteConfiguration(ownerId, configurationId);
        }
        

    }
}
