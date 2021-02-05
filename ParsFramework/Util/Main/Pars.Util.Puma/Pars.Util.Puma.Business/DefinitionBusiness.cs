using System;
using System.Collections.Generic;
using Pars.Core;
using Pars.Util.Puma.Data.Repository;
using Pars.Util.Puma.Domain;
using Pars.Core.Linq;

namespace Pars.Util.Puma.Business
{
    public class DefinitionBusiness : IDefinitionBusiness
    {
        IDefinitionRepository _repository;
        IRoleRepository _roleRepo;
        IClaimRepository _claimRepo;
        IUserRepository _userRepo;
        IUserGroupRepository _userGroupRepo;

        public DefinitionBusiness() : this(
            Container.Instance.Resolve<IDefinitionRepository>("DefinitionRepository"),
            Container.Instance.Resolve<IRoleRepository>("RoleRepository"),
            Container.Instance.Resolve<IClaimRepository>("ClaimRepository"),
            Container.Instance.Resolve<IUserRepository>("UserRepository"),
            Container.Instance.Resolve<IUserGroupRepository>("UserGroupRepository"))
        { }

        public DefinitionBusiness(IDefinitionRepository repository,
            IRoleRepository roleRepo,
            IClaimRepository claimRepo,
            IUserRepository userRepo,
            IUserGroupRepository userGroupRepo)
        {
            _repository = repository;
            _roleRepo = roleRepo;
            _claimRepo = claimRepo;
            _userRepo = userRepo;
            _userGroupRepo = userGroupRepo;
        }

        public List<EntityState> GetEntityStates()
            => _repository.GetEntityStates();

        public List<MemberState> GetMemberStates()
            => _repository.GetMemberStates();

        public List<LookupItem> GetSegmentsAsLookup()
            => _repository.GetSegmentsAsLookup();

        public List<LookupItem> GetSubSegmentsAsLookup()
             => _repository.GetSubSegmentsAsLookup();

        public List<LookupItem> GetBusinessLinesAsLookup()
             => _repository.GetBusinessLinesAsLookup();

        public List<LookupItem> GetAgeSexGroupsAsLookup()
            => _repository.GetAgeSexGroupsAsLookup();

        public List<LookupItem> GetSupplierTypesAsLookup()
            => _repository.GetSupplierTypesAsLookup();

        public List<LookupItem> GetServers()
            => _repository.GetServers();

        public List<LookupItem> GetDatabases(int serverRef)
            => _repository.GetDatabases(serverRef);

        public List<LookupItem> GetTables(int databaseRef)
            => _repository.GetTables(databaseRef);

        public List<LookupItem> GetTableColumns(int tableRef)
            => _repository.GetTableColumns(tableRef);

        public List<LookupItem> GetLanguages()
        {
            List<LookupItem> languages = new List<LookupItem>();

            var languageCollection = (Pars.Core.LanguageEnum[])Enum.GetValues(typeof(Pars.Core.LanguageEnum));

            foreach (Pars.Core.LanguageEnum language in languageCollection)
            {
                if (language != Pars.Core.LanguageEnum.None)
                    languages.Add(new LookupItem() { Ref = (int)language, Value = language.ToString() });
            }

            return languages;
        }

        public ITQueryable<User> Users { get => _userRepo.Users; }

        public ITQueryable<UserGroup> UserGroups { get => _userGroupRepo.UserGroups; }

        public ITQueryable<Claim> Claims { get => _claimRepo.Claims; }

        public ITQueryable<Role> Roles { get => _roleRepo.Roles; }

        //public ITQueryable<Role> GetRoles(IPaging paging)
        //    => _roleRepo.GetRoles(paging);

        //public ITQueryable<Role> GetRoles(TextSearch search = null, IPaging paging = null)
        //    => _roleRepo.GetRoles(search, paging);

        //public ITQueryable<Claim> GetClaims(IPaging paging)
        // => _claimRepo.GetClaims(paging);

        //public ITQueryable<Claim> GetClaims(TextSearch search = null, IPaging paging = null)
        //    => _claimRepo.GetClaims(search, paging);

        //public ITQueryable<UserGroup> GetUserGroups(IPaging paging)
        //    => _userGroupRepo.GetUserGroups(paging);

        //public ITQueryable<UserGroup> GetUserGroups(TextSearch search = null, IPaging paging = null)
        //    => _userGroupRepo.GetUserGroups(search, paging);

        //public ITQueryable<LookupItem> GetUserGroupsAsLookup(IPaging paging)
        //    => GetUserGroupsAsLookup(null, paging);

        //public ITQueryable<LookupItem> GetUserGroupsAsLookup(TextSearch search = null, IPaging paging = null)
        ////=> _userGroupRepo.UserGroupLookups.AsTQueryable();
        //=> _userGroupRepo.GetUserGroupsAsLookup(search, paging);       

        //public ITQueryable<User> GetUsers(IPaging paging)
        //    => _userRepo.GetUsers(paging);

        //public ITQueryable<User> GetUsers(TextSearch search = null, IPaging paging = null)
        //     => _userRepo.GetUsers(search, paging);
    }
}
