using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Pars.Core;
using Pars.Util.Puma.Data.Repository;

namespace Pars.Util.Puma.Service.AuthManagement
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Pars.Core.Container.Instance.RegisterType(typeof(Pars.Core.ITimeProvider),
                typeof(Pars.Core.SystemTimeProvider));

            Pars.Core.Container.Instance.RegisterType(typeof(Pars.Core.Security.IIdentityProvider),
                typeof(Pars.Core.Security.FederationIdentityProvider));

            Pars.Core.Container.Instance.RegisterType(typeof(Pars.Core.Data.IAuditableDataProvider),
                typeof(Pars.Core.Data.DefaultAuditableDataProvider));
            Pars.Core.Container.Instance.RegisterType(typeof(Pars.Util.Puma.Data.IPumaUnitOfWork),
                typeof(Pars.Util.Puma.Data.PumaUnitOfWork));


            //Pars.Core.Container.Instance.RegisterType(typeof(Pars.Util.Puma.Data.IPumaContext), typeof(Pars.Util.Puma.Data.PumaContext));

            //Pars.Core.Container.Instance.RegisterType(typeof(Pars.Core.Caching.ICacheProvider),
            //  typeof(Pars.Core.Caching.Redis.RedisCacheProvider));

            //Pars.Core.Container.Instance.RegisterTypeWithParameterlessConstructor("Pars.Util.Puma.Data.Repository.IClaimRepository, Pars.Util.Puma.Data",
            //    "Pars.Util.Puma.Data.Repository.ClaimRepositoryInternal, Pars.Util.Puma.Data", "ClaimRepositoryInternal");

            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IUserRepository, Pars.Util.Puma.Data",
                "Pars.Util.Puma.Data.Repository.UserRepository, Pars.Util.Puma.Data", "UserRepository", true);
            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IClaimRepository, Pars.Util.Puma.Data",
                "Pars.Util.Puma.Data.Repository.ClaimRepository, Pars.Util.Puma.Data", "ClaimRepository", true);

            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IRoleRepository, Pars.Util.Puma.Data",
               "Pars.Util.Puma.Data.Repository.RoleRepository, Pars.Util.Puma.Data", "RoleRepository", true);

            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IUserGroupRepository, Pars.Util.Puma.Data",
               "Pars.Util.Puma.Data.Repository.UserGroupRepository, Pars.Util.Puma.Data", "UserGroupRepository", true);

            Pars.Core.Container.Instance.RegisterType(typeof(ISupplierRepository), typeof(SupplierRepository));

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