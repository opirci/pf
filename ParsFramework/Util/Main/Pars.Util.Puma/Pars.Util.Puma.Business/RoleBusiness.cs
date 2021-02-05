using Pars.Core;
using Pars.Util.Puma.Data.Repository;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Business
{
    public class RoleBusiness : IRoleBusiness
    {  
        readonly IRoleRepository _roleRepo;

        public RoleBusiness() : this(Container.Instance.Resolve<IRoleRepository>("RoleRepository"))
        {

        }

        public RoleBusiness(IRoleRepository roleRepository)
        {
            _roleRepo = roleRepository;          
        }

        public ITQueryable<Role> Roles => _roleRepo.Roles;


        //public ITQueryable<Role> GetRoles(IPaging paging)
        //    => _roleRepo.GetRoles(paging);

        //public ITQueryable<Role> GetRoles(TextSearch search = null, IPaging paging = null)
        //    => _roleRepo.GetRoles(search, paging);
    }
}
