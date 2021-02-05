using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Business.Interface
{
    public interface IConfigurationBusiness
    {
        ConfigurationContext GetConfigurationOfContext(int contextRef);
        int GetApplicationRefOfContext(int applicationRef);

        List<ConfigurationItem> GetConfigurationOfContextByConfigKey(int applicationRef, string configKey);

        void SaveConfigurationContext(ConfigurationContext entiy);

        List<ConfigurationContext> GetConfigurationContextList();

        List<Application> GetApplications();

        List<ConfigurationItem> GetConfigurationItems(int configurationContextRef);

        List<ConfigurationKey> GetConfigurationKeyList();
        void SaveConfigurationItem(ConfigurationItem entiy);
        void SaveConfigurationKey(ConfigurationKey configurationKey);
    }
}
