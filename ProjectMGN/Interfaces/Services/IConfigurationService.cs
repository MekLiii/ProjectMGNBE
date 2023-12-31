﻿using ProjectMGN.DTOS.Request;
using ProjectMGN.DTOS.Response;
using ProjectMGN.Models;

namespace ProjectMGN.Interfaces.Services
{
    public interface IConfigurationService
    {
        public void CreateConfiguration(Configuration configuration, int OwnerId);
        public List<ConfigurationResponse> GetAllConfigurations(int OwnerId);
        public void DeleteConfiguration(int ownerId, int configurationId);
        public ConfigurationResponse GetConfigurationById(int ownerId, int configurationId);
        public void UpdateConfiguration(UpdateConfigurationRequest configurationRequest,int configurationId);

    }
}
