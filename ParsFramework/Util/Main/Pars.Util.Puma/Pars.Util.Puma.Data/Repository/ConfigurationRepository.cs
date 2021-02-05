using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Data.Repository
{
    public class ConfigurationRepository
    {
        private readonly IPumaUnitOfWork unitOfwork;

        public ConfigurationRepository() :
            this(Pars.Core.Container.Instance.Resolve<IPumaUnitOfWork>())
        {
        }

        public ConfigurationRepository(IPumaUnitOfWork unitOfwork)
        {
            this.unitOfwork = unitOfwork;
        }

        public void SaveConfigurationContext(ConfigurationContext context)
        {
            unitOfwork.ConfigurationContext.Enroll(new tb_ConfigurationContext { ApplicationRef = context.ApplicationRef, Name = context.Name, ContextRef = context.ContextRef});
            unitOfwork.Save();
        }

        public List<ConfigurationContext> GetConfigurationContextList()
        {
            var contexts = unitOfwork.ConfigurationContext.GetAll().ToList();
            List<ConfigurationContext> result = new List<ConfigurationContext>();
            contexts.ForEach(i => result.Add(new ConfigurationContext() { ContextRef = i.ContextRef, ApplicationRef = i.ApplicationRef, Name = i.Name }));
            return result;
        }

        public int GetApplicationRefOfContext(int contextRef)
        {
            return unitOfwork.ConfigurationContext.Single(x => x.ContextRef == contextRef).ApplicationRef;
        }

        public int GetContextRef(int applicationRef, string configKey)
        {
            return unitOfwork.ConfigurationContext.Find(c => c.ApplicationRef == applicationRef && c.Name == configKey).FirstOrDefault().ContextRef;
        }
        public List<ConfigurationItem> GetConfigurationOfContextByConfigKey(int applicationRef, string configKey)
        {
            int contextRef = GetContextRef(applicationRef, configKey);
            return this.GetConfigurationItemsByContextRef(contextRef);
        }
        public List<ConfigurationItem> GetConfigurationItemsByContextRef(int contextRef)
        {
            return (from ite in unitOfwork.ConfigurationItem
                    join
                    key in unitOfwork.ConfigurationKey on ite.ConfigurationKeyRef equals key.ConfigurationKeyRef
                    where ite.ContextRef == contextRef
                    select new ConfigurationItem
                    {
                        ContextRef = ite.ContextRef,
                        Value = ite.Value,
                        ConfigurationItemRef = ite.ConfigurationItemRef,
                        ConfigurationKeyRef = ite.ConfigurationKeyRef,
                        ConfigurationKeyName = key.Name
                    }).ToList();
        }

        public void SaveConfigurationItem(ConfigurationItem item)
        {
            unitOfwork.ConfigurationItem.Enroll(new tb_ConfigurationItem { ContextRef = item.ContextRef, ConfigurationKeyRef = item.ConfigurationKeyRef, Value = item.Value, ConfigurationItemRef = item.ConfigurationItemRef});
            unitOfwork.Save();
        }

        public void SaveConfigurationKey(ConfigurationKey key)
        {
            unitOfwork.ConfigurationKey.Enroll(new tb_ConfigurationKey() { ConfigurationKeyRef = key.ConfigurationKeyRef, Description = key.Description, Name = key.Name, EntityState = key.DataEntityState });
            unitOfwork.Save();
        }

        public List<ConfigurationKey> GetConfigurationKeys()
        {
            var temp = unitOfwork.ConfigurationKey.GetAll().ToList();
            List<ConfigurationKey> result = new List<ConfigurationKey>();
            temp.ForEach(i=> result.Add(new ConfigurationKey(){ConfigurationKeyRef = i.ConfigurationKeyRef, Name = i.Name, Description = i.Description}));
            return result;
        }

        public List<Application> GetApplications()
        {
            return unitOfwork.Application.GetAll().Select(tbApplication => new Application() {ApplicationGuid = tbApplication.ApplicationGuid, Name = tbApplication.Name, ApplicationRef = tbApplication.ApplicationRef}).ToList();
        }
    }
}
