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
        public List<Configuration> GetAllConfigurations(int OwnerId)
        {
            return _projectConfiguration.GetAllConfigurations(OwnerId);
        }
        public void DeleteConfiguration(int ownerId, int configurationId)
        {
            _projectConfiguration.DeleteConfiguration(ownerId, configurationId);
        }
        public Configuration GetConfigurationById(int ownerId, int configurationId)
        {
            return GetConfigurationById(ownerId, configurationId);
        }

    }
}
