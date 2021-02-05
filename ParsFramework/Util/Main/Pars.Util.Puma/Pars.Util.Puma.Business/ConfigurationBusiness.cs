using System.Collections.Generic;
using Pars.Core;
using Pars.Util.Puma.Business.Interface;
using Pars.Util.Puma.Data.Repository.Interface;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Business
{
    public class ConfigurationBusiness : IConfigurationBusiness
    {
        private IConfigurationRepository _configurationRepository;
        public ConfigurationBusiness(IConfigurationRepository repository)
        {
            _configurationRepository = repository;
        }

        public ConfigurationBusiness() : this(Container.Instance.Resolve<IConfigurationRepository>())
        {
            
        }
        public ConfigurationContext GetConfigurationOfContext(int contextRef)
        {
            ConfigurationContext result = new ConfigurationContext();
            result.ApplicationRef = _configurationRepository.GetApplicationRefOfContext(contextRef);
            result.ConfigurationItems = _configurationRepository.GetConfigurationItemsByContextRef(contextRef);
            return result;
        }

        public int GetApplicationRefOfContext(int contextRef)
        {
            return _configurationRepository.GetApplicationRefOfContext(contextRef);
        }

        public List<ConfigurationItem> GetConfigurationOfContextByConfigKey(int applicationRef, string configKey)
        {
            return _configurationRepository.GetConfigurationOfContextByConfigKey(applicationRef, configKey);
        }

        public void SaveConfigurationContext(ConfigurationContext context)
        {
            _configurationRepository.SaveConfigurationContext(context);
        }

        public List<ConfigurationContext> GetConfigurationContextList()
        {
            return _configurationRepository.GetConfigurationContextList();
        }

        public List<Application> GetApplications()
        {
            return _configurationRepository.GetApplications();
        }

        public List<ConfigurationItem> GetConfigurationItems(int configurationContextRef)
        {
            return _configurationRepository.GetConfigurationItemsByContextRef(configurationContextRef);
        }

        public List<ConfigurationKey> GetConfigurationKeyList()
        {
            return _configurationRepository.GetConfigurationKeys();
        }

        public void SaveConfigurationItem(ConfigurationItem configurationItem)
        {
            _configurationRepository.SaveConfigurationItem(configurationItem);
        }

        public void SaveConfigurationKey(ConfigurationKey configurationKey)
        {
            _configurationRepository.SaveConfigurationKey(configurationKey);
        }
    }
}
