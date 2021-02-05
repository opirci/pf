using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pars.Core.Security;
using Pars.Util.Puma.Domain;
using System.Security.Claims;
using Claim = Pars.Util.Puma.Domain.Claim;

namespace Pars.Util.Puma.Data.IntegrationTest
{
    [TestClass]
    public class EntityTests
    {

        private IPumaUnitOfWork unitOfwork;

        //private IIdentityProvider idp = null;

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            string environmentKey = "test"; //test : live : dev

            string relayingParty = "http://localhost/wpfshell/";
            string issuerAddress = "https://sts" + environmentKey + ".lcwaikiki.com/sts/issue/wstrust/mixed/windows";
            string issuerName = "https://sts" + environmentKey + ".lcwaikiki.com";
            string spnIdentity = "LCWAIKIKI\\pars" + environmentKey + "acc";


            ClaimsPrincipal claimsPrincipal = null;

            var fim = new Pars.Core.IdentityModel.FederationIdentityManager();
            claimsPrincipal = fim.GetPrincipal(1, relayingParty, issuerAddress, issuerName, spnIdentity);
            Assert.IsNotNull(claimsPrincipal);


            System.Threading.Thread.CurrentPrincipal = claimsPrincipal;
            System.AppDomain.CurrentDomain.SetThreadPrincipal(claimsPrincipal);
        }

        [TestInitialize]
        public void InitializeTest()
        {
            var idp = Pars.Core.Container.Instance.Resolve<IIdentityProvider>();
            unitOfwork = Pars.Core.Container.Instance.Resolve<IPumaUnitOfWork>();
        }

        [TestMethod]
        public void SelectEntitiesWithLocales()
        {
            var claim = (from c in unitOfwork.Claim.Table
                         join es in unitOfwork.EntityState.Table on c.EntityStateRef equals es.EntityStateRef
                         select new Claim()
                         {
                             ClaimRef = c.ClaimRef,
                             Name = c.Name,
                             EntityState = new EntityState()
                             {
                                 EntityStateRef = es.EntityStateRef,
                                 Name = es.Name
                             },
                             LocaleData = new LocaleData()
                             {
                                 LocaleRef = c.tb_Claim_Locale.FirstOrDefault().ClaimLocaleRef,
                                 Value = c.tb_Claim_Locale.FirstOrDefault().Description
                             }
                         }).ToList();
        }

        [TestMethod]
        public void SelectSingleEntityWitLocale()
        {
            var claims = (from c in unitOfwork.Claim.Table
                          join l in unitOfwork.ClaimLocale.Table on c.ClaimRef equals l.ClaimRef
                          join es in unitOfwork.EntityState.Table on c.EntityStateRef equals es.EntityStateRef
                          join esl in unitOfwork.EntityStateLocale.Table on es.EntityStateRef equals esl.EntityStateRef
                          select new Claim()
                          {
                              ClaimRef = c.ClaimRef,
                              Name = c.Name,
                              EntityState = new EntityState()
                              {
                                  EntityStateRef = es.EntityStateRef,
                                  Name = es.Name,
                                  Description = esl.Text
                              },
                              LocaleData = new LocaleData()
                              {
                                  Value = l.Description,
                                  LocaleRef = l.ClaimLocaleRef
                              }
                          }).ToList();
        }
    }
}
