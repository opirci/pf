using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Pars.Core;
using Pars.Util.Puma.Business;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.Supplier
{
    public class SupplierService : ISupplierService
    {
        IUserBusiness _userBusiness;
        ISupplierBusiness _supplierBusiness;

        public SupplierService(IUserBusiness business, ISupplierBusiness supplierBusiness)
        {
            _userBusiness = business;
            _supplierBusiness = supplierBusiness;
        }

        public SupplierService() : this(Container.Instance.Resolve<IUserBusiness>("UserBusiness"),
            Container.Instance.Resolve<ISupplierBusiness>("SupplierBusiness")
        )
        {

        }

        public GetSupplierReportResponse GetSupplierReport(GetSupplierReportRequest request)
        {

            GetSupplierReportResponse response = new GetSupplierReportResponse();
            try
            {
                PagedList<SupplierReportLine> ret = _supplierBusiness.GetSupplierReport(request.SupplierCode, request.SupplierName,
                request.Countries, request.Segments, request.SupplierType, request.UserType, request.LogOnActive, request.DateRange, request.ModifiedUser);
                response.Values = new List<SupplierReportLine>(ret);
                response.Count = ret.TotalCount;
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public GetSupplierUserLoginReportResponse GetSupplierUserLoginReport(GetSupplierUserLoginReportRequest request)
        {
            GetSupplierUserLoginReportResponse response = new GetSupplierUserLoginReportResponse();
            try
            {
                DateWeekRange range = new DateWeekRange()
                {
                    Start = new DateWeek { Year = request.StartYear, Week = request.StartWeek },
                    End = new DateWeek { Year = request.EndYear, Week = request.EndWeek }
                };
                response.Value = _supplierBusiness.GetSupplierUserLoginReport(range, request.ReportType);
                response.ReportType = request.ReportType;
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public GetSuppliersFilteredResponse GetSuppliersFiltered(GetSuppliersFilteredRequest request)
        {
            //return ResponseBase.Execute<GetSuppliersFilteredResponse>(r =>
            //{
            //var pagedList = _userBusiness.GetSuppliers(request.Code, request.Name, request.HasNoUsers, request.SupplierType,
            //         request.Segment, request.SubSegments, request.BusinessLines, request.AgeSexGroups, request.PageNumber, request.PageSize);

            //response.Values = new List<Supplier>(pagedList);
            //response.Count = pagedList.TotalCount;
            //});

            GetSuppliersFilteredResponse response = new GetSuppliersFilteredResponse();
            try
            {
                var pagedList = _supplierBusiness.GetSuppliers(request.Code, request.Name, request.HasNoUsers,
                    request.SupplierType,
                    request.Segment, request.SubSegments, request.BusinessLines, request.AgeSexGroups,
                    request.PageNumber, request.PageSize);

                response.Values = new List<Domain.Supplier>(pagedList);
                response.Count = pagedList.TotalCount;
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public GetSupplierUsersResponse GetSupplierUsers(GetSupplierUsersRequest request)
        {
            var response = new GetSupplierUsersResponse();

            try
            {
                List<SupplierUser> supplierUsers = _supplierBusiness.GetSupplierUsersByPartyRef(request.PartyRef, request.Status);
                response.Values = supplierUsers;
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public GetSupplierResponse GetSupplier(GetSupplierRequest request)
        {
            var response = new GetSupplierResponse();

            try
            {
                Domain.Supplier supplier = _supplierBusiness.GetSupplierByCode(request.SupplierCode);
                response.Value = supplier;
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public GetSupplierClaimsResponse GetSupplierClaims()
        {
            var response = new GetSupplierClaimsResponse();

            try
            {
                List<Claim> claims = _supplierBusiness.GetSupplierClaims();
                response.Values = claims;
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public SaveSupplierUserResponse SaveSupplierUser(SaveSupplierUserRequest request)
        {
            var response = new SaveSupplierUserResponse();

            try
            {
                SupplierUser supplierUser = _supplierBusiness.SaveSupplierUser(request.SupplierUser);
                response.SupplierUser = supplierUser;
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public SupplierUserExistsResponse SupplierUserExists(SupplierUserExistsRequest request)
        {
            var response = new SupplierUserExistsResponse();

            try
            {
                bool exists = _supplierBusiness.SupplierUserExist(request.Mail);
                response.Exists = exists;
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public ValidateUserResponse ValidateUser(ValidateUserRequest request)
        {
            var response = new ValidateUserResponse();

            try
            {
                response.IsValid = _supplierBusiness.ValidateUser(request.UserRef, request.SupplierCode);
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }
    }


}