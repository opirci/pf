using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Pars.Core;
using Pars.Core.Service;
using Pars.Util.Puma.UI.Mvc.AuthSvc;
using Auth = Pars.Util.Puma.UI.Mvc.AuthSvc;
using Def = Pars.Util.Puma.UI.Mvc.DefinitionSvc;

namespace Pars.Util.Puma.UI.Mvc.api
{
    public class DefinitionController : ApiControllerBase
    {

        [HttpGet]
        public IEnumerable<Domain.EntityState> EntityStates()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var es = new Pars.Core.Service.ProxyBase<DefinitionSvc.IDefinitionService>(idp);
            return es.Client.GetEntityStates().EntityStates.AsEnumerable();

        }

        [HttpGet]
        public IEnumerable<Domain.MemberState> MemberStates()
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            var definitionService = new Pars.Core.Service.ProxyBase<DefinitionSvc.IDefinitionService>(idp);
            return definitionService.Client.GetMemberStates().MemberStates.AsEnumerable();
        }

        [HttpGet]
        public Domain.LookupResponse Segments()
        {
            try
            {
                return ProxyOf<DefinitionSvc.IDefinitionService>().GetSegmentsAsLookup();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpGet]
        public Domain.LookupResponse SubSegments()
            => ProxyOf<DefinitionSvc.IDefinitionService>().GetSubSegmentsAsLookup();

        [HttpGet]
        public Domain.LookupResponse BusinessLines()
            => ProxyOf<DefinitionSvc.IDefinitionService>().GetBusinessLinesAsLookup();

        [HttpGet]
        public Domain.LookupResponse AgeSexGroups()
            => ProxyOf<DefinitionSvc.IDefinitionService>().GetAgeSexGroupsAsLookup();

        [HttpGet]
        public Domain.LookupResponse SupplierTypes()
            => ProxyOf<DefinitionSvc.IDefinitionService>().GetSupplierTypesAsLookup();

        [HttpGet]
        public Domain.LookupResponse Servers()
            => ProxyOf<Def.IDefinitionService>().GetServers();

        [HttpGet]
        public Domain.LookupResponse Databases([FromUri]Def.GetDatabasesRequest request)
            => ProxyOf<Def.IDefinitionService>().GetDatabases(request);

        [HttpGet]
        public Domain.LookupResponse Tables([FromUri]Def.GetTablesRequest request)
            => ProxyOf<Def.IDefinitionService>().GetTables(request);

        [HttpGet]
        public Domain.LookupResponse Columns([FromUri]Def.GetTableColumnsRequest request)
            => ProxyOf<Def.IDefinitionService>().GetTableColumns(request);

        [HttpGet]
        public Domain.LookupResponse Languages()
            => ProxyOf<Def.IDefinitionService>().GetLanguages();

        //[HttpGet]
        //public DataTableProxy GetPersonsAsDataTable()
        //{
        //    System.Data.DataTable personDt = DataTableOps.ToDataTable(Person.CreatePersons());
        //    DataTableProxy personDtPx = personDt.ToProxy();
        //    Console.WriteLine(personDtPx);
        //    return personDtPx;
        //}

        //[HttpPut]
        //public void SetPersonsAsDataTable([FromBody] DataTableProxy data)
        //{
        //    try
        //    {
        //        Console.WriteLine(data);
        //        System.Data.DataTable personDt = data.ToDataTable();
        //        DataTableProxy personDtPx = personDt.ToProxy();
        //        Console.WriteLine(personDt);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.GetMessageDeep());

        //    }


        //}


    }
}
