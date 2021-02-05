using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pars.Core.Security;
using Pars.Core.Service;
using Pars.Util.Puma.Service.IntegrationTest.DataLocalizerSvc;

namespace Pars.Util.Puma.Service.IntegrationTest
{
    [TestClass]
    public class DataLocalizer
    {
        [DebuggerStepThrough]
        public static T ProxyOf<T>() where T : class
        {
            return new Pars.Core.Service.ProxyBase<T>(new Pars.Core.Security.FederationIdentityProvider()).Client;
        }

        [TestInitialize]
        public void Initialize()
        {
            string environmentKey = "dev";//test : live : dev

            string relayingParty = "http://localhost/wpfshell/";
            string issuerAddress = "https://sts" + environmentKey + ".lcwaikiki.com/sts/issue/wstrust/mixed/windows";
            string issuerName = "https://sts" + environmentKey + ".lcwaikiki.com";
            string spnIdentity = "LCWAIKIKI\\pars" + environmentKey + "acc";


            ClaimsPrincipal claimsPrincipal = null;

            var fim = new Pars.Core.IdentityModel.FederationIdentityManager();
            claimsPrincipal = fim.GetPrincipal(1, relayingParty, issuerAddress, issuerName, spnIdentity);
            Assert.IsNotNull(claimsPrincipal);


            System.Threading.Thread.CurrentPrincipal = claimsPrincipal;
            // System.AppDomain.CurrentDomain.SetThreadPrincipal(claimsPrincipal);
        }

        [TestMethod]
        public void GetLocalesByRow()
        {
            var idp = new FederationIdentityProvider();
            var proxy = new ProxyBase<IDataLocalizerService>(idp);
            var request = new GetLocalesByRowRequest();
            request.objectRef = 1;
            request.guid = "64D5BD62-81F8-4AFD-883D-1C19277DFCB6";
            request.languages = new int[] { 1, 2, 10, 11 };
            var response = proxy.Client.GetLocalesByRow(request);
        }
    }
}
