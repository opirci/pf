using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pars.Util.Puma.Service.IntegrationTest.AuthManagementSvc;

namespace Pars.Util.Puma.Service.IntegrationTest
{
    [TestClass]
    public class AuthManagement
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
            // System.AppDomain.CurrentDomain.SetThreadPrincipal(claimsPrincipal);
        }

        [TestMethod]
        public void GetClaims()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var cClient = new Pars.Core.Service.ProxyBase<IAuthManagementService>(idp);
            var response = cClient.Client.GetClaims(new ClaimListRequest { PageSize = 10, PageNumber = 1 });
            Assert.IsTrue(response.Claims.Any());
        }

        [TestMethod]
        public void GetClaimsWithSearch()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var cClient = new Pars.Core.Service.ProxyBase<AuthManagementSvc.IAuthManagementService>(idp);
            var response = cClient.Client.GetClaims(new ClaimListRequest { PageSize = 10, PageNumber = 1 });
            Assert.IsTrue(response.Claims.Any());
        }

        [TestMethod]
        public void GetUserGroupsByClaimRef()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var cClient = new Pars.Core.Service.ProxyBase<AuthManagementSvc.IAuthManagementService>(idp);
            int claimRef = 2;
            var response = cClient.Client.GetUserGroupsByClaimRef(new UserGroupRelationRequest()
            {
                ClaimRef = claimRef,
                PageNumber = 1,
                PageSize = 10

            });
            Assert.IsTrue(response.UserGroups.Any());
        }

        [TestMethod]
        public void SaveClaim()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var cClient = new Pars.Core.Service.ProxyBase<AuthManagementSvc.IAuthManagementService>(idp);
            ClaimSaveResponse response;
            try
            {
                response = cClient.Client.SaveClaim(new ClaimSaveRequest
                {
                    Claim = new AuthManagementSvc.Claim
                    {
                        ClaimRef = 0,
                        EntityState = new EntityState { EntityStateRef = 1, Name = "Aktif" },
                        Name = "ThisisClaimFromTest",
                        HasChanged = true,
                        LocaleData =
                            new LocaleData { LocaleRef = 0, Value = "ThisisClaimFromTest_Locale", HasChanged = true }
                    }
                });

                //TODO: Buraya delete methodunun gelmesi gerekmektedir.
                Assert.IsTrue(response.Claim.ClaimRef > 0);
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        [TestMethod]
        public void GetRolesByClaimRef()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var cClient = new Pars.Core.Service.ProxyBase<IAuthManagementService>(idp);
            int claimRef = 3;
            GetRolesByClaimRefResponse response = null;
            try
            {
                response = cClient.Client.GetRolesByClaimRef(new GetRolesByClaimRefRequest()
                {
                    value = claimRef,
                    pageNumber = 1,
                    pageSize = 10

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Assert.IsTrue(response.values != null && response.values.Any());
        }

        [TestMethod]
        public void GetRolesAsLookupByContainingName()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var cClient = new Pars.Core.Service.ProxyBase<IAuthManagementService>(idp);
            string searchText = "a";
            LookupResponse response = null;
            try
            {
                response = cClient.Client.GetRolesAsLookupByContainingName(new GetRolesByContainingNameRequest()
                {
                    searchText = searchText
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (response.errorMessages.Count > 0)
                Console.WriteLine("There are errors. Check response.ErrorMessages");
            else
                Assert.IsTrue(response.values.Any());
        }

        [TestMethod]
        public void GetRolesAsLookupByStartingName()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var cClient = new Pars.Core.Service.ProxyBase<IAuthManagementService>(idp);
            string searchText = "TES";
            LookupResponse response = null;
            try
            {
                response = cClient.Client.GetRolesAsLookupByStartingName(new GetRolesByStartingNameRequest()
                {
                    searchText = searchText
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (response.errorMessages.Count > 0)
                Console.WriteLine("There are errors. Check response.ErrorMessages");
            else
                Assert.IsTrue(response.values.Any());
        }
    }
}
