using System.Collections.Generic;
using System.Linq;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Data.Repository
{
    public interface IRoleRepository
    {
        ITQueryable<Role> Roles { get; }
        //ITQueryable<Role> GetRoles(IPaging paging);

        //ITQueryable<Role> GetRoles(TextSearch search = null, IPaging paging = null);
        

        int Count();

        IQueryable<RoleRelation> GetRolesByUserRef(int userRef);

        IQueryable<RoleRelation> GetRolesByUserGroupRef(int userGroupRef);

        IQueryable<RoleRelation> GetRolesByClaimRef(int claimRef);

        ITQueryable<RoleRelation> GetRolesByUserRef(int userRef, IPaging paging);

        ITQueryable<RoleRelation> GetRolesByUserGroupRef(int userGroupRef, IPaging paging);

        ITQueryable<RoleRelation> GetRolesByClaimRef(int claimRef, IPaging paging, TextSearch search = null);

        Role Save(Role role);
        void Save(List<Role> role);

        RoleRelation SaveRoleClaim(RoleRelation roleRelation);

        RoleRelation SaveRoleUser(RoleRelation roleRelation);

        RoleRelation SaveRoleUserGroup(RoleRelation roleRelation);

        void SaveRoleClaim(List<RoleRelation> roleMap);

        void SaveRoleUser(List<RoleRelation> roleMap);

        void SaveRoleUserGroup(List<RoleRelation> roleMap);
    }
}
