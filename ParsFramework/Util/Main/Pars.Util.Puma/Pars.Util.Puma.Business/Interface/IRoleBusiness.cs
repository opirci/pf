
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Business
{
    public interface IRoleBusiness
    {
        ITQueryable<Role> Roles { get; }

        //ITQueryable<Role> GetRoles(IPaging paging);

        //ITQueryable<Role> GetRoles(TextSearch search = null, IPaging paging = null);

        //List<Role> GetRoles();

        //List<RoleRelation> GetRolesByClaimRef(int claimRef);

        //RoleRelation SaveRoleClaim(RoleRelation role);

        //List<Role> GetRolesStartsWithName(string text);


        //void SaveRoleClaims(List<RoleRelation> role);

        //void Save(List<Role> role);

        //Role Save(Role role);

        //RoleRelation SaveRoleUser(RoleRelation role);

        //void SaveRoleUser(List<RoleRelation> role);

        //RoleRelation SaveRoleUserGroup(RoleRelation role);

        //void SaveRoleUserGroup(List<RoleRelation> role);
    }
}
