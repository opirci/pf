using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.Definition
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDefinitionService" in both code and config file together.
    [ServiceContract]
    public interface IDefinitionService
    {
        [OperationContract]
        GetEntityStatesResponse GetEntityStates();

        [OperationContract]
        MemberStateListResponse GetMemberStates();

        [OperationContract]
        LookupResponse GetSegmentsAsLookup();

        [OperationContract]
        LookupResponse GetSubSegmentsAsLookup();

        [OperationContract]
        LookupResponse GetBusinessLinesAsLookup();

        [OperationContract]
        LookupResponse GetAgeSexGroupsAsLookup();

        [OperationContract]
        LookupResponse GetSupplierTypesAsLookup();

        [OperationContract]
        LookupResponse GetServers();

        [OperationContract]
        LookupResponse GetDatabases(GetDatabasesRequest request);

        [OperationContract]
        LookupResponse GetTables(GetTablesRequest request);

        [OperationContract]
        LookupResponse GetTableColumns(GetTableColumnsRequest request);

        [OperationContract]
        LookupResponse GetLanguages();
    }
}
