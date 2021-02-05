using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pars.Core;
using Pars.Core.Data;
using Pars.Core.Security;
using Pars.Util.Puma.Data;
using Pars.Util.Puma.Data.Repository;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Business.IntegrationTest
{
    [TestClass]
    public class SupplierBusinessTest
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
        public void Get_SupplierUsers_By_PartyRef()
        {
            var business = new SupplierBusiness();
            List<SupplierUser> supplierUsers = business.GetSupplierUsersByPartyRef(1, 1);
            Assert.IsTrue(supplierUsers.Count == 1);
        }

        [TestMethod]
        public void Get_Supplier_By_Code()
        {
            var business = new SupplierBusiness();
            Supplier supplier = business.GetSupplierByCode("JB007");
            Assert.IsNotNull(supplier);

        }

        [TestMethod]
        public void Get_Suppliers_Filtered()
        {
            var business = new SupplierBusiness();

            //string code = "1";
            //string name = "b";
            //bool hasNoUsers = false;
            //int supplierType = 99;
            //int segment = 98;
            //int[] subSegments = { 97, 87 };
            //int[] businessLines = { 96, 86 };
            //int[] ageSexGroups = { 95, 85 };
            //int pageNumber = 0;
            //int pageSize = 0;


            string code = null;
            string name = null;
            bool hasNoUsers = false;
            int supplierType = 2;
            int segment = 0;
            int[] subSegments = { 0 };
            int[] businessLines = { 0 };
            int[] ageSexGroups = { 0 };
            int pageNumber = 0;
            int pageSize = 0;

            PagedList<Supplier> supplier = business.GetSuppliers(code, name, hasNoUsers, supplierType, segment, subSegments,
                businessLines, ageSexGroups, pageNumber, pageSize);
            Assert.IsNotNull(supplier);
            Assert.IsTrue(supplier.Count > 0);

        }


        [TestMethod]
        public void Get_Suppliers_Report()
        {
            var business = new SupplierBusiness();

            //string code = "1";
            //string name = "b";
            //bool hasNoUsers = false;
            //int supplierType = 99;
            //int segment = 98;
            //int[] subSegments = { 97, 87 };
            //int[] businessLines = { 96, 86 };
            //int[] ageSexGroups = { 95, 85 };
            //int pageNumber = 0;
            //int pageSize = 0;


            string supplierCode = null;
            string supplierName = null;
            bool logOnActive = false;
            SupplierUserType? userType = null;
            //int segment = 0;
            int[] countries = { 0 };
            int[] segments = { 0 };
            int supplierType = 0;
            DateRange dateRange = new DateRange();
            string modifiedUser = null;
            //int pageSize = 0;

            PagedList<SupplierReportLine> supplier = business.GetSupplierReport(supplierCode, supplierName, countries, segments, supplierType,
                userType, logOnActive, dateRange, modifiedUser);
            Assert.IsNotNull(supplier);
            Assert.IsTrue(supplier.Count > 0);

        }

        [TestMethod]
        public void Get_Supplier_User_Login_Report()
        {
            var business = new SupplierBusiness();
           DataTableProxy dataTable = business.GetSupplierUserLoginReport(new DateWeekRange(new DateWeek(2017, 10), new DateWeek(2017, 15)), 1);
            Assert.IsNotNull(dataTable);
            Assert.IsTrue(dataTable.Rows.Length > 0);
        }
    }
}
