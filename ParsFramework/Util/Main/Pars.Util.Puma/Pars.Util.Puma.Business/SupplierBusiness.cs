using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pars.Core;
using Pars.Util.Puma.Business;
using Pars.Util.Puma.Data.Repository;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Business
{
    public class SupplierBusiness : ISupplierBusiness
    {
        readonly ISupplierRepository _supplierRepository;

        public SupplierBusiness(ISupplierRepository repository)
        {
            _supplierRepository = repository;
        }

        public SupplierBusiness() : this(Container.Instance.Resolve<ISupplierRepository>())
        {

        }
        public PagedList<Supplier> GetSuppliers(string code, string name, bool hasNoUsers, int supplierType, int segment, int[] subSegments,
          int[] businessLines, int[] ageSexGroups, int pageNumber, int pageSize) =>
          _supplierRepository.GetSuppliers(code, name, hasNoUsers, supplierType, segment, subSegments,
              businessLines, ageSexGroups, new Paging(pageNumber, pageSize));


        public PagedList<SupplierReportLine> GetSupplierReport(string supplierCode, string supplierName, int[] countries, int[] segments, int supplierType,
                SupplierUserType? userType, bool? logOnActive, DateRange dateRange, string modifiedUser) =>
            _supplierRepository.GetSupplierReport(supplierCode, supplierName, countries, segments, supplierType,
                userType, logOnActive, dateRange, modifiedUser);

        public DataTableProxy GetSupplierUserLoginReport(DateWeekRange weekRange, byte reportType)
        {
            return _supplierRepository.GetSupplierUserLoginReport(weekRange, reportType);
        }


        public List<SupplierUser> GetSupplierUsersByPartyRef(int partyRef, int status)
        {
            List<SupplierUser> supplierUsers = _supplierRepository.GetSupplierUsersByPartyRef(partyRef, status);
            return supplierUsers;
        }

        public List<Claim> GetSupplierClaims()
        {
            List<Claim> claims = _supplierRepository.GetSupplierClaims();
            return claims;
        }

        public Supplier GetSupplierByCode(string supplierCode)
        {
            Supplier supplier = _supplierRepository.GetSupplierByCode(supplierCode);
            return supplier;
        }

        public SupplierUser SaveSupplierUser(SupplierUser supplierUser)
        {
            SupplierUser user = _supplierRepository.SaveSupplierUser(supplierUser);
            return user;
        }

        public bool SupplierUserExist(string mail)
        {
            return _supplierRepository.SupplierUserExist(mail);
        }

        public bool ValidateUser(int userRef, string supplierCode)
        {
            return _supplierRepository.ValidateUser(userRef, supplierCode);
        }
    }
}
