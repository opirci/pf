using Pars.Util.Puma.Domain;
using System.ServiceModel;

namespace Pars.Util.Puma.Service.AuthManagement
{
    [ServiceContract]
    public interface IAuthManagementService
    {
        [OperationContract]
        ClaimListResponse GetClaims(ClaimListRequest request);

        [OperationContract]
        UserGroupRelationResponse GetUserGroupsByClaimRef(UserGroupRelationRequest request);

        [OperationContract]
        ClaimSaveResponse SaveClaim(ClaimSaveRequest request);


        [OperationContract]
        GetRolesByClaimRefResponse GetRolesByClaimRef(GetRolesByClaimRefRequest request);

        [OperationContract]
        SaveRoleRelationResponse SaveRoleRelation(SaveRoleRelationRequest request);

        //[OperationContract]
        //GetRolesByStartingNameResponse GetRolesByStartingName(GetRolesByStartingNameRequest request);

        //[OperationContract]
        //GetRolesByContainingNameResponse GetRolesByContainingName(GetRolesByContainingNameRequest request);

        [OperationContract]
        LookupResponse GetRolesAsLookupByStartingName(GetRolesByStartingNameRequest request);

        [OperationContract]
        LookupResponse GetRolesAsLookupByContainingName(GetRolesByContainingNameRequest request);



    }
}
