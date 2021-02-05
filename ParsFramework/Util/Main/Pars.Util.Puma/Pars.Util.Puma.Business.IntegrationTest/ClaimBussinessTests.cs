using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pars.Util.Puma.Data.Repository;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;
using Pars.Core;

namespace Pars.Util.Puma.Business.IntegrationTest
{
    [TestClass]
    public class ClaimBussinessTests
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            #region Unity Mappings
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
            #endregion

            #region IIdentityProvider
            string environmentKey = "test"; //test : live : dev

            string relayingParty = "http://localhost/wpfshell/";
            string issuerAddress = "https://sts" + environmentKey + ".lcwaikiki.com/sts/issue/wstrust/mixed/windows";
            string issuerName = "https://sts" + environmentKey + ".lcwaikiki.com";
            string spnIdentity = "LCWAIKIKI\\pars" + environmentKey + "acc";


            ClaimsPrincipal claimsPrincipal = null;

            var fim = new Core.IdentityModel.FederationIdentityManager();
            claimsPrincipal = fim.GetPrincipal(1, relayingParty, issuerAddress, issuerName, spnIdentity);
            Assert.IsNotNull(claimsPrincipal);


            Thread.CurrentPrincipal = claimsPrincipal;
            AppDomain.CurrentDomain.SetThreadPrincipal(claimsPrincipal);
            #endregion
        }

        [TestMethod]
        public void GetClaims_PageNoOne_PageCountTen()
        {
            ClaimBusiness business = new ClaimBusiness();
            List<Domain.Claim> claims = business.Claims.GetItems(c => c.Name, new Paging(1, 10)).ToList(); 
            Assert.IsTrue(claims.Count == 10);
        }

        [TestMethod]
        public void GetUserRelationByClaimRef_PageNoOne_PageCountTen()
        {
            ClaimBusiness business = new ClaimBusiness();
            PagedList<UserRelation> userRelations = business.GetUserRelationByClaimRef(2062, new Paging(1, 10));
            
            Assert.IsTrue(userRelations.Count > 0);
            Assert.IsTrue(userRelations.Any());
        }
    }
}
