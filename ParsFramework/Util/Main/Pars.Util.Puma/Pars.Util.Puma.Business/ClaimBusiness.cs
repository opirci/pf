using Pars.Core;
using Pars.Util.Puma.Data.Repository;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Business
{
    public class ClaimBusiness : IClaimBusiness
    {
        readonly IClaimRepository _claimRepository;
        readonly IUserGroupRepository _userGroupRepository;      
        readonly IUserRepository _userRepository;
        readonly IRoleRepository _roleRepo;

        public ClaimBusiness() : this(
            Container.Instance.Resolve<IClaimRepository>("ClaimRepository"),         
            Container.Instance.Resolve<IUserGroupRepository>("UserGroupRepository"),
            Container.Instance.Resolve<IUserRepository>("UserRepository"),
            Container.Instance.Resolve<IRoleRepository>("RoleRepository"))
        {

        }

        public ClaimBusiness(IClaimRepository claimRepository,         
            IUserGroupRepository userGroupRepository, 
            IUserRepository userRepository,
            IRoleRepository roleRepo)
        {
            _claimRepository = claimRepository;          
            _userGroupRepository = userGroupRepository;
            _userRepository = userRepository;
            _roleRepo = roleRepo;
        }

        public ITQueryable<Claim> Claims => _claimRepository.Claims;

        //public ITQueryable<Claim> GetClaims(IPaging paging)
        //    => GetClaims(paging);

        //public ITQueryable<Claim> GetClaims(TextSearch search, IPaging paging)
        //    => _claimRepository.GetClaims(search, paging);

        public PagedList<UserGroupRelation> GetUserGroupsByClaimRef(int claimRef, IPaging paging)
            => _userGroupRepository.GetUserGroupsByClaimRef(claimRef, paging);
        
        public int Count(string searchText)
            => _claimRepository.Count(searchText);
        
        public int UserGroupCount(int claimRef)
            => _userGroupRepository.Count(claimRef);        

        public Claim Save(Claim claim)
            => _claimRepository.Save(claim);

        #region IRoleBusiness members

        public ITQueryable<Role> Roles => _roleRepo.Roles;

        //public ITQueryable<Role> GetRoles(IPaging paging)
        //    => GetRoles(null, paging);

        //public ITQueryable<Role> GetRoles(TextSearch search = null, IPaging paging = null)
        //    => _roleRepo.GetRoles(search, paging);

        //public ITQueryable<Role> GetRoles()
        //=> _roleRepository.GetRoles();

        //public List<Role> GetRolesByStartingName(string text)
        //     => _roleRepository.GetRolesByStartingName(text);

        //public List<Role> GetRolesByContainingName(string text)
        //    => _roleRepository.GetRolesByContainingName(text);

        public ITQueryable<RoleRelation> GetRolesByClaimRef(int claimRef, IPaging paging, TextSearch search = null)
            => _roleRepo.GetRolesByClaimRef(claimRef, paging, search);

        //public List<LookupItem> GetRolesAsLookupByStartingName(string text)
        //    => _roleRepository.GetRolesAsLookupByStartingName(text);
        
        //public List<LookupItem> GetRolesAsLookupByContainingName(string text)
        //    => _roleRepository.GetRolesAsLookupByContainingName(text);
        
        //public List<Role> GetRoles()
        //=> _roleRepository.GetRoles();

        //public List<Role> GetRolesByStartingName(string text)
        //     => _roleRepository.GetRolesByStartingName(text);

        //public List<Role> GetRolesByContainingName(string text)
        //    => _roleRepository.GetRolesByContainingName(text);

        //public PagedList<RoleRelation> GetRolesByClaimRef(int claimRef, IPagingContext paging, SearchContext search = null)
        //    => _roleRepository.GetRolesByClaimRef(claimRef, paging, search);

        //public List<LookupItem> GetRolesAsLookupByStartingName(string text)
        //    => _roleRepository.GetRolesAsLookupByStartingName(text);


        //public List<LookupItem> GetRolesAsLookupByContainingName(string text)
        //    => _roleRepository.GetRolesAsLookupByContainingName(text);
        

        public RoleRelation SaveRoleClaim(RoleRelation role)
            => _roleRepo.SaveRoleClaim(role);
        
        #endregion

        public PagedList<UserRelation> GetUserRelationByClaimRef(int claimRef, IPaging paging)
            => _userRepository.GetUserRelationByClaimRef(claimRef, paging);        

        public UserRelation SaveUserClaimRelation(UserRelation userRelation)
            => _userRepository.SaveUserClaimRelation(userRelation);        
    }
}
