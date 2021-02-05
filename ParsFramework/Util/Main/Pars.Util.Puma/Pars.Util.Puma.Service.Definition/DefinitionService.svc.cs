using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.WCF;
using Pars.Core;
using Pars.Util.Puma.Business;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.Definition
{
    [ExceptionShielding("Pars.Util.Puma")]
    public class DefinitionService : IDefinitionService
    {
        IDefinitionBusiness _business;

        public DefinitionService() : this(Container.Instance.Resolve<IDefinitionBusiness>("DefinitionBusiness"))
        {
        }

        public DefinitionService(IDefinitionBusiness business)
        {
            _business = business;
        }

        public GetEntityStatesResponse GetEntityStates()
        {
            GetEntityStatesResponse result = new GetEntityStatesResponse() { EntityStates = _business.GetEntityStates() };
            return result;
        }

        public MemberStateListResponse GetMemberStates()
        {
            MemberStateListResponse reponse = new MemberStateListResponse()
            {
                MemberStates = _business.GetMemberStates()
            };

            return reponse;
        }

        public LookupResponse GetSegmentsAsLookup()
        {
            //return ResponseBase.Execute<LookupResponse>(r =>
            //{
            //var pagedList = _business.GetSegmentsAsLookup();
            //r.Values = new List<LookupItem>(pagedList);
            //});

            LookupResponse response = new LookupResponse();
            try
            {
                var pagedList = _business.GetSegmentsAsLookup();
                response.Values = new List<LookupItem>(pagedList);
            }
            catch (Exception ex)
            {
                response.AddError(ex.GetMessageDeep());
            }

            return response;
        }

        public LookupResponse GetSubSegmentsAsLookup()
            => ResponseBase.Execute<LookupResponse>(r =>
               {
                   var pagedList = _business.GetSubSegmentsAsLookup();
                   r.Values = new List<LookupItem>(pagedList);
               });


        public LookupResponse GetBusinessLinesAsLookup()
         => ResponseBase.Execute<LookupResponse>(r =>
         {
             var pagedList = _business.GetBusinessLinesAsLookup();
             r.Values = new List<LookupItem>(pagedList);
         });

        public LookupResponse GetAgeSexGroupsAsLookup()
          => ResponseBase.Execute<LookupResponse>(r =>
          {
              var pagedList = _business.GetAgeSexGroupsAsLookup();
              r.Values = new List<LookupItem>(pagedList);
          });

        public LookupResponse GetSupplierTypesAsLookup()
         => ResponseBase.Execute<LookupResponse>(r =>
         {
             var pagedList = _business.GetSupplierTypesAsLookup();
             r.Values = new List<LookupItem>(pagedList);
         });

        public LookupResponse GetServers()
            => ResponseBase.Execute<LookupResponse>(r =>
            {
                var result = _business.GetServers();
                r.Values = new List<LookupItem>(result);
            });

        public LookupResponse GetDatabases(GetDatabasesRequest request)
            => ResponseBase.Execute<LookupResponse>(r =>
            {
                var result = _business.GetDatabases(request.ServerRef);
                r.Values = new List<LookupItem>(result);
            });

        public LookupResponse GetTables(GetTablesRequest request)
            => ResponseBase.Execute<LookupResponse>(r =>
            {
                var result = _business.GetTables(request.DatabaseRef);
                r.Values = new List<LookupItem>(result);
            });

        public LookupResponse GetTableColumns(GetTableColumnsRequest request)
            => ResponseBase.Execute<LookupResponse>(r =>
            {
                var result = _business.GetTableColumns(request.TableRef);
                r.Values = new List<LookupItem>(result);
            });

        public LookupResponse GetLanguages()
          => ResponseBase.Execute<LookupResponse>(r =>
          {
              var result = _business.GetLanguages();
              r.Values = new List<LookupItem>(result);
          });
    }
}
