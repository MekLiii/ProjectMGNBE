using ProjectMGN.Models;

namespace ProjectMGN.Interfaces.Repositories
{
    public interface IProjectConfiguration
    {
        public void CreateConfiguration(Configuration configuration, int OwnerId)
        public List<Configuration> GetAllConfigurations(int OwnerId);
        public void DeleteConfiguration(int ownerId, int configurationId);
        public Configuration GetConfigurationById(int ownerId, int configurationId);
    }
}
