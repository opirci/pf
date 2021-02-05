using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace Pars.Util.Puma.Service.Supplier
{
    [ServiceContract]
    public interface ISupplierService
    {

        [OperationContract]
        GetSuppliersFilteredResponse GetSuppliersFiltered(GetSuppliersFilteredRequest request);

        [OperationContract]
        GetSupplierReportResponse GetSupplierReport(GetSupplierReportRequest request);

        [OperationContract]
        GetSupplierUserLoginReportResponse GetSupplierUserLoginReport(GetSupplierUserLoginReportRequest request);

        [OperationContract]
        GetSupplierUsersResponse GetSupplierUsers(GetSupplierUsersRequest request);

        [OperationContract]
        GetSupplierResponse GetSupplier(GetSupplierRequest request);

        [OperationContract]
        SaveSupplierUserResponse SaveSupplierUser(SaveSupplierUserRequest request);

        [OperationContract]
        SupplierUserExistsResponse SupplierUserExists(SupplierUserExistsRequest request);

        [OperationContract]
        GetSupplierClaimsResponse GetSupplierClaims();

        [OperationContract]
        ValidateUserResponse ValidateUser(ValidateUserRequest request);
    }
}