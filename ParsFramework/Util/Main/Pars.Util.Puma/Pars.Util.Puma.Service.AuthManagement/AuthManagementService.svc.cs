using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF;
using Pars.Core;
using Pars.Util.Puma.Business;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Service.AuthManagement
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ExceptionShielding("Pars.Util.Puma")]
    public class AuthManagementService : IAuthManagementService
    {
        readonly ClaimBusiness claimBusiness = new ClaimBusiness();

        public ClaimListResponse GetClaims(ClaimListRequest request)
        {
            //var claimBusiness = new ClaimBusiness();
            int count = claimBusiness.Count(request.SearchText);
            //List<Claim> claims = claimBusiness.GetClaims(request.SearchText, request.PageNumber, request.PageSize);
            List<Claim> claims = claimBusiness.Claims.GetItems(c => c.Name, new TextSearch(request.SearchText, TextSearchAs.Contains),
                new Paging(request.PageNumber, request.PageSize)).ToList();

            return new ClaimListResponse()
            {
                Claims = claims,
                Count = count
            };
        }

        public UserGroupRelationResponse GetUserGroupsByClaimRef(UserGroupRelationRequest request)
        {
            //var claimBusiness = new ClaimBusiness();
            var userGroups = claimBusiness.GetUserGroupsByClaimRef(request.ClaimRef, new Paging(request.PageNumber, request.PageSize));
            int count = claimBusiness.UserGroupCount(request.ClaimRef);

            var response = new UserGroupRelationResponse()
            {
                UserGroups = userGroups,
                Count = count
            };

            return response;
        }

        public ClaimSaveResponse SaveClaim(ClaimSaveRequest request)
        {
            //var claimBusiness = new ClaimBusiness();
            var claim = claimBusiness.Save(request.Claim);
            ClaimSaveResponse response = new ClaimSaveResponse();
            response.Claim = claim;
            return response;
        }

        public LookupResponse GetRolesAsLookupByStartingName(GetRolesByStartingNameRequest request)
        {
            // return ResponseBase.Execute<LookupResponse>(r =>
            //{
            //    r.Values = claimBusiness.GetRolesAsLookupByStartingName(request.SearchText);
            //});

            LookupResponse response = new LookupResponse();
            try
            {
                //response.Values = claimBusiness.GetRolesAsLookupByStartingName(request.SearchText);
                response.Values = claimBusiness.Roles.GetItems( r => r.Name, new TextSearch(request.SearchText, TextSearchAs.StartsWith)).AsLookup().ToList();
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }



        public LookupResponse GetRolesAsLookupByContainingName(GetRolesByContainingNameRequest request)
        {
            //return ResponseBase.Execute<LookupResponse>(r =>
            //{
            //    r.Values = claimBusiness.GetRolesAsLookupByContainingName(request.SearchText);
            //});

            LookupResponse response = new LookupResponse();
            try
            {
                //response.Values = claimBusiness.GetRolesAsLookupByContainingName(request.SearchText);
                response.Values = claimBusiness.Roles.GetItems(r => r.Name, new TextSearch(request.SearchText)).AsLookup().ToList();
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public GetRolesByClaimRefResponse GetRolesByClaimRef(GetRolesByClaimRefRequest request)
        {
            //return ResponseBase.Execute<GetRolesByClaimRefResponse>(r =>
            //{
            //    var pagedList = claimBusiness.GetRolesByClaimRef(request.Value, request.PageNumber, request.PageSize, request.SearchText);
            //    r.Values = pagedList;
            //    r.Count = pagedList.TotalCount;
            //});

            GetRolesByClaimRefResponse response = new GetRolesByClaimRefResponse();
            try
            {
                var pagedList = claimBusiness.GetRolesByClaimRef(request.Value, new Paging(request.PageNumber, request.PageSize), new TextSearch(request.SearchText));
                response.Values = new List<RoleRelation>(pagedList);
                response.Count = pagedList.TotalCount;
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public SaveRoleRelationResponse SaveRoleRelation(SaveRoleRelationRequest request)
        {
            SaveRoleRelationResponse response = new SaveRoleRelationResponse();

            try
            {
                response.Value = claimBusiness.SaveRoleClaim(request.RoleRelation);
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }
            return response;
        }
    }
}
