using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Pars.Util.Puma.UI.Mvc.AuthSvc;

namespace Pars.Util.Puma.UI.Mvc.api
{
    public class ClaimController : ApiController
    {
        // GET api/<controller>
        public ClaimListResponse Get()
        {
            var keyValuePairs = Request.GetQueryNameValuePairs();
            string pageNumber = keyValuePairs.FirstOrDefault(x => x.Key == "pNumber").Value;
            string pageSize = keyValuePairs.FirstOrDefault(x => x.Key == "pSize").Value;
            string searchText = keyValuePairs.FirstOrDefault(x => x.Key == "searchtext").Value;

            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var claim = new Pars.Core.Service.ProxyBase<AuthSvc.IAuthManagementService>(idp);
            var response = claim.Client.GetClaims(new ClaimListRequest { PageNumber = Convert.ToInt32(pageNumber), PageSize = Convert.ToInt32(pageSize), SearchText = searchText });
            //var claimListModel = new ClaimListModel();
            //claimListModel.ClaimList = response.Claims.AsEnumerable();
            //claimListModel.ClaimCount = response.Count;
            return response;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        public UserGroupRelationResponse GetUserGroupsByClaimRef(int claimRef)
        {
            IEnumerable<KeyValuePair<string, string>> keyValuePairs = Request.GetQueryNameValuePairs();
            string pageNumber = keyValuePairs.FirstOrDefault(x => x.Key == "pNumber").Value;
            string pageSize = keyValuePairs.FirstOrDefault(x => x.Key == "pSize").Value;

            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var authService = new Pars.Core.Service.ProxyBase<AuthSvc.IAuthManagementService>(idp);
            var response =
                authService.Client.GetUserGroupsByClaimRef(new UserGroupRelationRequest()
                {
                    ClaimRef = claimRef,
                    PageNumber = Convert.ToInt32(pageNumber),
                    PageSize = Convert.ToInt32(pageSize)
                });


            return response;
        }

        

        // POST api/<controller>
        //[HttpPost]
        public AuthSvc.Claim Post([FromBody]AuthSvc.Claim value)
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var claim = new Pars.Core.Service.ProxyBase<AuthSvc.IAuthManagementService>(idp);
            var response = claim.Client.SaveClaim(new ClaimSaveRequest() { Claim = value });
            return response.Claim;
        }

        #region Role
        [HttpGet]

        //public List<RoleRelation> RolesOfClaim(int claimRef)
        public GetRolesByClaimRefResponse RolesOfClaim(int claimRef, int pNumber, int pSize, string searchtext = null)
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var claim = new Pars.Core.Service.ProxyBase<AuthSvc.IAuthManagementService>(idp);
            var response = claim.Client.GetRolesByClaimRef(new GetRolesByClaimRefRequest() { value = claimRef, pageNumber = pNumber, pageSize = pSize, searchText = searchtext });
            return response;
            //return response.Values;
        }

        [HttpGet]
        public LookupResponse RolesByContainingName(string text)
        {
            LookupResponse response = null;
            try
            {
                var idp = new Pars.Core.Security.FederationIdentityProvider();
                var claim = new Pars.Core.Service.ProxyBase<AuthSvc.IAuthManagementService>(idp);
                response = claim.Client.GetRolesAsLookupByContainingName(new GetRolesByContainingNameRequest() { searchText = text });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return response;
            //return response.Roles;
        }


        [HttpGet]
        //public List<Role> RolesByStartingName(string text)
        public LookupResponse RolesByStartingName(string text)
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var claim = new Pars.Core.Service.ProxyBase<AuthSvc.IAuthManagementService>(idp);
            var response = claim.Client.GetRolesAsLookupByStartingName(new GetRolesByStartingNameRequest() { searchText = text });
            return response;
            //return response.Roles;
        }
        #endregion

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}