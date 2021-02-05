using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Microsoft.Practices.Unity;
using Pars.Core;
using Pars.Util.Puma.Business;
using Pars.Util.Puma.Data.Repository;

namespace Pars.Util.Puma.Service.DataLocalizer
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

            Container.Instance.RegisterType(typeof(IDataLocalizerRepository), typeof(DataLocalizerRepository));

            Container.Instance.RegisterType(typeof(IDataLocalizerBusiness), typeof(DataLocalizerBusiness));

            //Container.Instance.RegisterType("Pars.Util.Puma.Business.IDataLocalizerBusiness, Pars.Util.Puma.Business", "Pars.Util.Puma.Business.DataLocalizerBusiness, Pars.Util.Puma.Business", "DataLocalizerBusiness", true);
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