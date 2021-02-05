using Pars.Util.Puma.Domain;
using Pars.Core.Linq;
using Pars.Core;

namespace Pars.Util.Puma.Data.Repository
{
    public interface IUserGroupRepository
    {
        ITQueryable<UserGroup> UserGroups { get; }

        //IQueryable<LookupItem> UserGroupLookups { get; }
        //ITQueryable<UserGroup> GetUserGroups(IPaging paging);
        //ITQueryable<UserGroup> GetUserGroups(TextSearch search = null, IPaging paging = null);
        //ITQueryable<LookupItem> GetUserGroupsAsLookup(IPaging paging);
        //ITQueryable<LookupItem> GetUserGroupsAsLookup(TextSearch search = null, IPaging paging = null);

        PagedList<UserGroupRelation> GetUserGroupsByClaimRef(int claimRef, IPaging paging);
        int Count(int claimRef);
    }
}
