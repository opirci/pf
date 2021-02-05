using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pars.Util.Puma.Service.IntegrationTest.SupplierSvc;

namespace Pars.Util.Puma.Service.IntegrationTest
{
    [TestClass]
    public class Supplier
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
        public void GetSupplierUsers()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var proxy = new Pars.Core.Service.ProxyBase<ISupplierService>(idp);
            var response = proxy.Client.GetSupplierUsers(new GetSupplierUsersRequest() { partyRef = 1 });
            Assert.IsTrue(response.values.Any());
        }

        [TestMethod]
        public void GetSupplier()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var proxy = new Pars.Core.Service.ProxyBase<ISupplierService>(idp);
            var response = proxy.Client.GetSupplier(new GetSupplierRequest() { supplierCode = "JB007" });
            Assert.IsNotNull(response.value);
        }

        [TestMethod]
        public void GetSuppliersFiltered()
        {
            var request = new GetSuppliersFilteredRequest
            {
                subSegments = new int[0],
                businessLines = new int[0],
                ageSexGroups = new int[0],
                pageNumber = 1,
                pageSize = 50
            };
            var response = ProxyOf<ISupplierService>().GetSuppliersFiltered(request);
            Assert.IsTrue(response.values.Length != 0);
        }
    }
}
