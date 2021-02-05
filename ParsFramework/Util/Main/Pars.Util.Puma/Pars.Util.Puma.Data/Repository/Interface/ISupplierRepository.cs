using System.Collections.Generic;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;
using Pars.Core;

namespace Pars.Util.Puma.Data.Repository
{
    public interface ISupplierRepository
    {
        PagedList<Supplier> GetSuppliers(string code, string name, bool hasNoUsers, int supplierType, int segment, int[] subSegments, int[] businessLines, int[] ageSexGroups, IPaging paging);

        List<SupplierUser> GetSupplierUsersByPartyRef(int partyRef, int status);

        Supplier GetSupplierByCode(string supplierCode);

        List<Claim> GetSupplierClaims();

        SupplierUser SaveSupplierUser(SupplierUser supplierUser);

        bool SupplierUserExist(string mail);

        bool ValidateUser(int userRef, string supplierCode);

        PagedList<SupplierReportLine> GetSupplierReport(string supplierCode, string supplierName, int[] countries, int[] segments, int supplierType, SupplierUserType? userType, bool? logOnActive, DateRange dateRange, string modifiedUser);

        DataTableProxy GetSupplierUserLoginReport(DateWeekRange weekRange, byte reportType);
    }
}
