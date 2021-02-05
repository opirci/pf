using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Pars.Util.Puma.Api.Definition
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            App_Start.SecurityConfig.ConfigureGlobal(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

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
            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Business.IDefinitionBusiness, Pars.Util.Puma.Business",
                "Pars.Util.Puma.Business.DefinitionBusiness, Pars.Util.Puma.Business", "DefinitionBusiness", true);

            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IDefinitionRepository, Pars.Util.Puma.Data",
                "Pars.Util.Puma.Data.Repository.DefinitionRepositoryInternal, Pars.Util.Puma.Data", "DefinitionRepositoryInternal", true);
            
            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IDefinitionRepository, Pars.Util.Puma.Data",
                "Pars.Util.Puma.Data.Repository.DefinitionRepository, Pars.Util.Puma.Data", "DefinitionRepository", true);

            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IRoleRepository, Pars.Util.Puma.Data",
                "Pars.Util.Puma.Data.Repository.RoleRepository, Pars.Util.Puma.Data", "RoleRepository", true);

            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IClaimRepository, Pars.Util.Puma.Data",
                "Pars.Util.Puma.Data.Repository.ClaimRepository, Pars.Util.Puma.Data", "ClaimRepository", true);

            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IUserRepository, Pars.Util.Puma.Data",
                "Pars.Util.Puma.Data.Repository.UserRepository, Pars.Util.Puma.Data", "UserRepository", true);

            Pars.Core.Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IUserGroupRepository, Pars.Util.Puma.Data",
                "Pars.Util.Puma.Data.Repository.UserGroupRepository, Pars.Util.Puma.Data", "UserGroupRepository", true);
        }
    }
}
