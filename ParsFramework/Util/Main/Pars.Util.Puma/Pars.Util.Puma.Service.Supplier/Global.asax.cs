using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Pars.Core;
using Pars.Util.Puma.Business;
using Pars.Util.Puma.Data.Repository;

namespace Pars.Util.Puma.Service.Supplier
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



            Container.Instance.RegisterType(typeof(IUserRepository), typeof(UserRepository));


            Container.Instance.RegisterType(typeof(ISupplierRepository), typeof(SupplierRepository));
            Container.Instance.RegisterType("Pars.Util.Puma.Business.ISupplierBusiness, Pars.Util.Puma.Business", "Pars.Util.Puma.Business.SupplierBusiness, Pars.Util.Puma.Business", "SupplierBusiness", true);


            //Pars.Core.Container.Instance.RegisterType(typeof(Pars.Util.Puma.Data.IPumaContext), typeof(Pars.Util.Puma.Data.PumaContext));

            //Pars.Core.Container.Instance.RegisterType(typeof(Pars.Core.Caching.ICacheProvider),
            //  typeof(Pars.Core.Caching.Redis.RedisCacheProvider));
            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Business.IDefinitionBusiness, Pars.Util.Puma.Business", "Pars.Util.Puma.Business.DefinitionBusiness, Pars.Util.Puma.Business", "DefinitionBusiness", true);

            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IDefinitionRepository, Pars.Util.Puma.Data",
                "Pars.Util.Puma.Data.Repository.DefinitionRepositoryInternal, Pars.Util.Puma.Data", "DefinitionRepositoryInternal", true);

            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IDefinitionRepository, Pars.Util.Puma.Data",
                "Pars.Util.Puma.Data.Repository.DefinitionRepository, Pars.Util.Puma.Data", "DefinitionRepository", true);

            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Business.IUserBusiness, Pars.Util.Puma.Business", "Pars.Util.Puma.Business.UserBusiness, Pars.Util.Puma.Business", "UserBusiness", true);
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