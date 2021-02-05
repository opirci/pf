using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Data.Repository.Interface
{
    public interface IConfigurationRepository
    {
        void SaveConfigurationContext(ConfigurationContext context);
        List<ConfigurationContext> GetConfigurationContextList();
        int GetApplicationRefOfContext(int contextRef);
        int GetContextRef(int applicationRef, string configKey);
        List<ConfigurationItem> GetConfigurationOfContextByConfigKey(int applicationRef, string configKey);
        List<ConfigurationItem> GetConfigurationItemsByContextRef(int contextRef);
        void SaveConfigurationItem(ConfigurationItem item);
        void SaveConfigurationKey(ConfigurationKey key);
        List<ConfigurationKey> GetConfigurationKeys();
        List<Application> GetApplications();
    }
}
