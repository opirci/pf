using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pars.Util.Puma.Service.IntegrationTest.UserSvc;

namespace Pars.Util.Puma.Service.IntegrationTest
{
    [TestClass]
    public class User
    {
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
          //  System.AppDomain.CurrentDomain.SetThreadPrincipal(claimsPrincipal);
        }

        [TestMethod]
        public void GetUsers()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var uClient = new Pars.Core.Service.ProxyBase<UserSvc.IUserService>(idp);
            var response = uClient.Client.GetUsers();
            Assert.IsTrue(response.Users.Any());
        }


        [TestMethod]
        public void SearchUsers()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var uClient = new Pars.Core.Service.ProxyBase<UserSvc.IUserService>(idp);
            var response = uClient.Client.SearchUser(new UserSearchRequest {SearchText = "MEL"});
            Assert.IsTrue(response.Users.Any());
        }
    }
}
