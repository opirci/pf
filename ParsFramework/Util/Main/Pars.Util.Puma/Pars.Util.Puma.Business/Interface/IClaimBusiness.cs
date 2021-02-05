using System.Collections.Generic;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;
using Pars.Core;

namespace Pars.Util.Puma.Business
{
    public interface IClaimBusiness
    {
        #region IRoleBusiness memberes

        ITQueryable<RoleRelation> GetRolesByClaimRef(int claimRef, IPaging paging, TextSearch search = null);
        
        ITQueryable<Role> Roles { get; }
        //ITQueryable<Role> GetRoles(IPaging paging);
        //ITQueryable<Role> GetRoles(TextSearch search = null, IPaging paging = null);


        //List<Role> GetRoles();
        //List<Role> GetRolesByStartingName(string text);
        //List<Role> GetRolesByContainingName(string text);
        //List<LookupItem> GetRolesAsLookupByStartingName(string text);
        //List<LookupItem> GetRolesAsLookupByContainingName(string text);

        RoleRelation SaveRoleClaim(RoleRelation role);


        #endregion

        ITQueryable<Claim> Claims { get; }

        //ITQueryable<Claim> GetClaims(TextSearch search, IPaging paging);
        PagedList<UserGroupRelation> GetUserGroupsByClaimRef(int claimRef, IPaging paging);
        int Count(string searchText);
        int UserGroupCount(int claimRef);
        Claim Save(Claim claim);
        UserRelation SaveUserClaimRelation(UserRelation userRelation);
        PagedList<UserRelation> GetUserRelationByClaimRef(int claimRef, IPaging paging);
    }
}
