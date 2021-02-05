using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pars.Core.Security;

namespace Pars.Util.Puma.Service.IntegrationTest
{
    [TestClass]
    public class Definition
    {
        private static IIdentityProvider idp;

        [ClassInitialize]
        public static void Initialize(TestContext context)
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
            idp = new Pars.Core.Security.FederationIdentityProvider();
        }

        [TestMethod]
        public void GetEntityStates()
        {
            var esClient = new Pars.Core.Service.ProxyBase<DefinitionSvc.IDefinitionService>(idp);
            var response = esClient.Client.GetEntityStates();
            Assert.IsTrue(response.EntityStates.Any());
        }

        [TestMethod]
        public void GetMemberStates()
        {
            var definitionService = new Pars.Core.Service.ProxyBase<DefinitionSvc.IDefinitionService>(idp);
            var response = definitionService.Client.GetMemberStates();
            Assert.IsTrue(response.MemberStates.Any());
        }
    }
}
