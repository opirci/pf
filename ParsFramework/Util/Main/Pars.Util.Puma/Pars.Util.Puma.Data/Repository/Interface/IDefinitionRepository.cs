using System.Collections.Generic;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Data.Repository
{
    public interface IDefinitionRepository
    {
        List<EntityState> GetEntityStates();

        List<MemberState> GetMemberStates();

        List<LookupItem> GetSegmentsAsLookup();

        List<LookupItem> GetSubSegmentsAsLookup();

        List<LookupItem> GetBusinessLinesAsLookup();

        List<LookupItem> GetAgeSexGroupsAsLookup();

        List<LookupItem> GetSupplierTypesAsLookup();

        List<LookupItem> GetServers();

        List<LookupItem> GetDatabases(int serverRef);

        List<LookupItem> GetTables(int databaseRef);

        List<LookupItem> GetTableColumns(int tableRef);

    }
}