using System;
using System.Web;
using Pars.Core;
using Pars.Core.Data;
using Pars.Core.Security;
using Pars.Util.Puma.Data;
using Pars.Util.Puma.Data.Repository;

namespace Pars.Util.Puma.Service.User
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Container.Instance.RegisterType(typeof(ITimeProvider), typeof(SystemTimeProvider));

            Container.Instance.RegisterType(typeof(IIdentityProvider), typeof(FederationIdentityProvider));

            Container.Instance.RegisterType(typeof(IAuditableDataProvider), typeof(DefaultAuditableDataProvider));

            Container.Instance.RegisterType(typeof(IPumaUnitOfWork), typeof(PumaUnitOfWork));

            Container.Instance.RegisterType(typeof(IUserRepository), typeof(UserRepository));

            Container.Instance.RegisterType("Pars.Core.Caching.ICacheProvider, Pars.Core", "Pars.Core.Caching.Redis.RedisCacheProvider, Pars.Core.Caching.Redis", "CacheProvider", true);

            Container.Instance.RegisterType("Pars.Util.Puma.Business.IUserBusiness, Pars.Util.Puma.Business", "Pars.Util.Puma.Business.UserBusiness, Pars.Util.Puma.Business", "UserBusiness", true);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}