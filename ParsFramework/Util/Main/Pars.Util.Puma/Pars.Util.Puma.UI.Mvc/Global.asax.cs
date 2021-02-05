using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Services;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Pars.Util.Puma.UI.Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
          //  BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }

        public override void Init()
        {
            base.Init();
            var sam = System.IdentityModel.Services.FederatedAuthentication.SessionAuthenticationModule;
            sam.IsReferenceMode = true;

        }

        protected void Application_Error(object o, EventArgs e)
        {
            var ex = Context.Error;
            if (ex is SecurityTokenException)
            {
                Context.ClearError();
                if (FederatedAuthentication.SessionAuthenticationModule != null)
                {
                    FederatedAuthentication.SessionAuthenticationModule.SignOut();
                }
            }
            else
            {                
                //TODO: log error
                Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["ErrorUrl"]);
            }
        }
    }
}
