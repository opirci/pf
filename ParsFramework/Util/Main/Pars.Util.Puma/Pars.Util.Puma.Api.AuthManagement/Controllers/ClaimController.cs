using Pars.Core.Service;
using Pars.Util.Puma.Business;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Pars.Util.Puma.Api.AuthManagement.Controllers
{
    [Authorize]
    public class ClaimController : ApiController
    {
        readonly ClaimBusiness claimBusiness = new ClaimBusiness();

        [HttpGet]
        [Route("api/claim/get")]
        public ClaimListResponse Get([FromUri]ClaimListRequest request)
        {
            int count = claimBusiness.Count(request.SearchText);
            //List<Claim> claims = claimBusiness.GetClaims(new TextSearch(request.SearchText), 
            List<Claim> claims = claimBusiness.Claims.GetItems( 
                c => c.Name, 
                new TextSearch(request.SearchText),
                new Paging(request.PageNumber, request.PageSize)).ToList();
            
            //test todo test
            return new ClaimListResponse()
            {
                Claims = claims,
                Count = count
            };
        }

        [HttpPost]
        [Route("api/claim/post")]
        public Claim SaveClaim(Claim request)
        {
            //var claimBusiness = new ClaimBusiness();
            var claim = claimBusiness.Save(request);
            //ClaimSaveResponse response = new ClaimSaveResponse();
            //response.Claim = claim;
            return claim;
        }

        [HttpGet]
        //[Route("api/claim/RolesOfClaim")]
        public GetRolesByClaimRefResponse RolesOfClaim(
            [FromUri(Name = "claimRef")]int claimRef,
            [FromUri(Name = "PageNumber")]int pageNumber,
            [FromUri(Name = "PageSize")]int pageSize,
            [FromUri(Name = "searchtext")]string searchtext = null)
        {
            GetRolesByClaimRefResponse response = new GetRolesByClaimRefResponse();
            var list = claimBusiness.GetRolesByClaimRef(claimRef, new Paging(pageNumber, pageSize), new TextSearch(searchtext));
            response.Values = list.ToList();
            response.Count = list.TotalCount;
            return response;
        }

        [HttpGet]
        [Route("api/supplier/GetSupplierUserLoginReport")]
        public GetSupplierUserLoginReportResponse GetSupplierUserLoginReport([FromUri]GetSupplierUserLoginReportRequest request)
        {
            SupplierBusiness business = new SupplierBusiness();

            var idp = new Pars.Core.Security.FederationIdentityProvider();
            if (!idp.IsInClaim("Vrp_Tedport_Admin"))
            {
                GetSupplierUserLoginReportResponse resp = new GetSupplierUserLoginReportResponse();
                resp.AddError("Tedport admin privilages needed to execute this service!");
                return resp;
            }

            DateWeekRange range = new DateWeekRange()
            {
                Start = new DateWeek { Year = request.StartYear, Week = request.StartWeek },
                End = new DateWeek { Year = request.EndYear, Week = request.EndWeek }
            };
            var rs = business.GetSupplierUserLoginReport(range, request.ReportType);
            GetSupplierUserLoginReportResponse response = new GetSupplierUserLoginReportResponse();
            response.Value = rs;
            response.ReportType = request.ReportType;

            return response;
        }
    }
}
