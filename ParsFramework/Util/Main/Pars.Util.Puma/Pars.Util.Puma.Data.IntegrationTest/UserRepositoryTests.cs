using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pars.Core;
using Pars.Core.Data;
using Pars.Core.Security;
using Pars.Util.Puma.Data.Repository;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;
using User = Pars.Util.Puma.Domain.User;

namespace Pars.Util.Puma.Data.IntegrationTest
{
    [TestClass]
    public class UserRepositoryTests
    {
        static UserRepository _repository;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            #region Unity Mappings
            Container.Instance.RegisterType(typeof(ITimeProvider), typeof(SystemTimeProvider));
            Container.Instance.RegisterType(typeof(IIdentityProvider), typeof(FederationIdentityProvider));
            Container.Instance.RegisterType(typeof(IAuditableDataProvider), typeof(DefaultAuditableDataProvider));
            Container.Instance.RegisterType(typeof(IPumaUnitOfWork), typeof(PumaUnitOfWork));
            Container.Instance.RegisterType(typeof(IClaimRepository), typeof(ClaimRepository));
            Container.Instance.RegisterType(typeof(IUserRepository), typeof(UserRepository));
            //Container.Instance.RegisterType(typeof(IClaimRepository), typeof(ClaimRepository));

            Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IDefinitionRepository, Pars.Util.Puma.Data", "Pars.Util.Puma.Data.Repository.DefinitionRepository, Pars.Util.Puma.Data", "DefinitionRepository");
            Container.Instance.RegisterType("Pars.Util.Puma.Data.Repository.IDefinitionRepository, Pars.Util.Puma.Data", "Pars.Util.Puma.Data.Repository.DefinitionRepositoryInternal, Pars.Util.Puma.Data", "DefinitionRepositoryInternal");
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

            _repository = new UserRepository(new PumaUnitOfWork());
        }

        [TestMethod]
        public void GetAllUsers()
        {
            List<User> users = _repository.Users.GetItems().ToList();
            Assert.IsTrue(users.Any());
        }

        [TestMethod]
        public void SearchUser()
        {
            string searchTerm = "kut";
            List<LookupItem> users = _repository.Users
                .GetItems(u => u.Username, new TextSearch(searchTerm, TextSearchAs.StartsWith))
                .AsLookup(p => p.UserRef, p => p.Username).ToList();
            Assert.IsTrue(users.Any());
            Assert.IsTrue(users.Any(a => a.Value.StartsWith(searchTerm)));
        }

        [TestMethod]
        public void GetUserRelationByClaimRef()
        {
            PagedList<UserRelation> users = _repository.GetUserRelationByClaimRef(10, new Paging(1, 20));
            Assert.IsTrue(users.Any());
            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public void SaveUserClaimRelation()
        {
            int claimRef = 1;
            UserRelation user = new UserRelation
            {
                UserRef = 1046,
                RelatedRef = claimRef,
                MemberState = new MemberState { MemberStateRef = 1 },//valid
                ValidFrom = new DateTime(2016, 12, 9),
                ValidTo = new DateTime(2017, 12, 9),
                EntityState = EntityStates.Added
            };
            user = _repository.SaveUserClaimRelation(user);

            List<UserRelation> users = _repository.GetUserRelationByClaimRef(1, new Paging(1, int.MaxValue));
            Assert.IsTrue(users.Any(i => i.UserRef == user.UserRef));

            user.EntityState = EntityStates.Deleted;
            _repository.SaveUserClaimRelation(user);
        }
    }
}
