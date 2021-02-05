using System.Collections.Generic;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Business
{
    public interface IDefinitionBusiness
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

        List<LookupItem> GetLanguages();

        //ITQueryable<Role> GetRoles(IPaging paging);

        //ITQueryable<Role> GetRoles(TextSearch search = null, IPaging paging = null);

        //ITQueryable<Claim> GetClaims(IPaging paging);

        //ITQueryable<Claim> GetClaims(TextSearch search = null, IPaging paging = null);

        //ITQueryable<UserGroup> GetUserGroups(IPaging paging);

        //ITQueryable<UserGroup> GetUserGroups(TextSearch search = null, IPaging paging = null);

        //ITQueryable<LookupItem> GetUserGroupsAsLookup(IPaging paging);

        //ITQueryable<LookupItem> GetUserGroupsAsLookup(TextSearch search = null, IPaging paging = null);

        //ITQueryable<User> GetUsers(IPaging paging);

        //ITQueryable<User> GetUsers(TextSearch search = null, IPaging paging = null);

        ITQueryable<User> Users { get; }

        ITQueryable<UserGroup> UserGroups { get; }

        ITQueryable<Role> Roles { get; }

        ITQueryable<Claim> Claims { get; }


    }
}