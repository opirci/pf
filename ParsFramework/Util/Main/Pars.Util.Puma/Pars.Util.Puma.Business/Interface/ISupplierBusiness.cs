using System.Collections.Generic;
using Pars.Core;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Business
{
    public interface ISupplierBusiness
    {
        PagedList<Supplier> GetSuppliers(string code, string name, bool hasNoUsers, int supplierType, int segment,
           int[] subSegments, int[] businessLines, int[] ageSexGroups, int pageNumber, int pageSize);

        PagedList<SupplierReportLine> GetSupplierReport(string supplierCode, string supplierName, int[] countries, int[] segments, int supplierType,
            SupplierUserType? userType, bool? logOnActive, DateRange dateRange, string modifiedUser);

        DataTableProxy GetSupplierUserLoginReport(DateWeekRange weekRange, byte reportType);

        List<SupplierUser> GetSupplierUsersByPartyRef(int partyRef, int status);

        Supplier GetSupplierByCode(string supplierCode);

        List<Claim> GetSupplierClaims();

        SupplierUser SaveSupplierUser(SupplierUser supplierUser);

        bool SupplierUserExist(string mail);

        bool ValidateUser(int userRef, string supplierCode);
    }
}
