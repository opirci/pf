using System;
using Pars.Core.Data;
using Pars.Core.Security;

namespace Pars.Util.Puma.Data
{
    public class PumaUnitOfWork : DataUnitOfWorkBase, IPumaUnitOfWork
    {
        private readonly IPumaContext _context;

        public PumaUnitOfWork() : this(new PumaContext())
        {

        }
        internal PumaUnitOfWork(PumaContext context) : base(context)
        {
            _context = context;

            _applicationRepository = CreateRepository<tb_Application>(context);
            _claimRepository = CreateRepository<tb_Claim>(context);
            _claimLocaleRepository = CreateRepository<tb_Claim_Locale>(context);
            _entityStateRepository = CreateRepository<tb_EntityState>(context);
            _entityStateLocaleRepository = CreateRepository<tb_EntityState_Locale>(context);
            _userClaimRepository = CreateRepository<tb_UserClaim>(context);
            _userRepository = CreateRepository<tb_User>(context);
            _userGroupRepository = CreateRepository<tb_UserGroup>(context);
            _userGroupItemRepository = CreateRepository<tb_UserGroupItem>(context);
            _userGroupLocaleRepository = CreateRepository<tb_UserGroup_Locale>(context);
            _userLogonRepository = CreateRepository<tb_UserLogon>(context);
            _userLogonLogRepository = CreateRepository<tb_UserLogonLog>(context);
            _userMailRepository = CreateRepository<tb_UserMail>(context);
            _roleRepository = CreateRepository<tb_Role>(context);
            _roleLocaleRepository = CreateRepository<tb_Role_Locale>(context);
            _roleItemRepository = CreateRepository<tb_RoleItem>(context);
            _userRoleRepository = CreateRepository<tb_UserRole>(context);
            _userGroupRoleRepository = CreateRepository<tb_UserGroupRole>(context);
            _userGroupClaimRepository = CreateRepository<tb_UserGroupClaim>(context);
            _userEnvironmentRepository = CreateRepository<tb_UserEnvironment>(context);
            _userCompanyRepository = CreateRepository<tb_UserCompany>(context);
            _memberStateRepository = CreateRepository<tb_MemberState>(context);

            _supplierRepository = CreateRepository<tb_Supplier>(context);
            _supplierTypeRepository = CreateRepository<tb_SupplierType>(context);
            _partyRepository = CreateRepository<tb_Party>(context);
            _partyPersonRepository = CreateRepository<tb_PartyPerson>(context);
            _subSegmentRepository = CreateRepository<tb_SubSegment>(context);
            _segmentRepository = CreateRepository<tb_Segment>(context);
            _businessLineRepository = CreateRepository<tb_BusinessLine>(context);
            _ageSexGroupRepository = CreateRepository<tb_AgeSexGroup>(context);
            _suplierSubSegmentsRepository = CreateRepository<tb_SupplierSubSegments>(context);
            _supplierBusinessLinesRepository = CreateRepository<tb_SupplierBusinessLines>(context);
            _supplierAgeSexGroupsRepository = CreateRepository<tb_SupplierAgeSexGroups>(context);
            _segmentSubSegmentRelationRepository = CreateRepository<tb_SegmentSubSegmentRelation>(context);
            _segmentBusinessLineRelationRepository = CreateRepository<tb_SegmentBusinessLineRelation>(context);
            _userPartyRepository = CreateRepository<tb_UserParty>(context);
            _databaseRepository = CreateRepository<tb_Database>(context);
            _serverRepository = CreateRepository<tb_Server>(context);
            _tableRepository = CreateRepository<tb_Table>(context);
            _tableColumnRepository = CreateRepository<tb_TableColumn>(context);
            _configurationContextRepository = CreateRepository<tb_ConfigurationContext>(context);
            _configurationKeyRepository = CreateRepository<tb_ConfigurationKey>(context);
            _configurationItemRepository = CreateRepository<tb_ConfigurationItem>(context);
        }

        private readonly Lazy<IDataRepository<tb_Application>> _applicationRepository;
        private readonly Lazy<IDataRepository<tb_Claim>> _claimRepository;
        private readonly Lazy<IDataRepository<tb_Claim_Locale>> _claimLocaleRepository;
        private readonly Lazy<IDataRepository<tb_EntityState>> _entityStateRepository;
        private readonly Lazy<IDataRepository<tb_EntityState_Locale>> _entityStateLocaleRepository;
        private readonly Lazy<IDataRepository<tb_UserClaim>> _userClaimRepository;
        private readonly Lazy<IDataRepository<tb_User>> _userRepository;
        private readonly Lazy<IDataRepository<tb_UserGroup>> _userGroupRepository;
        private readonly Lazy<IDataRepository<tb_UserGroup_Locale>> _userGroupLocaleRepository;
        private readonly Lazy<IDataRepository<tb_UserLogon>> _userLogonRepository;
        private readonly Lazy<IDataRepository<tb_UserLogonLog>> _userLogonLogRepository;
        private readonly Lazy<IDataRepository<tb_UserGroupClaim>> _userGroupClaimRepository;
        private readonly Lazy<IDataRepository<tb_UserGroupItem>> _userGroupItemRepository;
        private readonly Lazy<IDataRepository<tb_UserMail>> _userMailRepository;
        private readonly Lazy<IDataRepository<tb_Role>> _roleRepository;
        private readonly Lazy<IDataRepository<tb_Role_Locale>> _roleLocaleRepository;
        private readonly Lazy<IDataRepository<tb_RoleItem>> _roleItemRepository;
        private readonly Lazy<IDataRepository<tb_UserRole>> _userRoleRepository;
        private readonly Lazy<IDataRepository<tb_UserGroupRole>> _userGroupRoleRepository;
        private readonly Lazy<IDataRepository<tb_UserEnvironment>> _userEnvironmentRepository;
        private readonly Lazy<IDataRepository<tb_UserCompany>> _userCompanyRepository;
        private readonly Lazy<IDataRepository<tb_MemberState>> _memberStateRepository;

        private readonly Lazy<IDataRepository<tb_Supplier>> _supplierRepository;
        private readonly Lazy<IDataRepository<tb_SupplierType>> _supplierTypeRepository;
        private readonly Lazy<IDataRepository<tb_Party>> _partyRepository;
        private readonly Lazy<IDataRepository<tb_PartyPerson>> _partyPersonRepository;
        private readonly Lazy<IDataRepository<tb_SubSegment>> _subSegmentRepository;
        private readonly Lazy<IDataRepository<tb_Segment>> _segmentRepository;
        private readonly Lazy<IDataRepository<tb_BusinessLine>> _businessLineRepository;
        private readonly Lazy<IDataRepository<tb_AgeSexGroup>> _ageSexGroupRepository;
        private readonly Lazy<IDataRepository<tb_SupplierSubSegments>> _suplierSubSegmentsRepository;
        private readonly Lazy<IDataRepository<tb_SupplierBusinessLines>> _supplierBusinessLinesRepository;
        private readonly Lazy<IDataRepository<tb_SupplierAgeSexGroups>> _supplierAgeSexGroupsRepository;
        private readonly Lazy<IDataRepository<tb_SegmentSubSegmentRelation>> _segmentSubSegmentRelationRepository;
        private readonly Lazy<IDataRepository<tb_SegmentBusinessLineRelation>> _segmentBusinessLineRelationRepository;
        private readonly Lazy<IDataRepository<tb_UserParty>> _userPartyRepository;
        private readonly Lazy<IDataRepository<tb_Database>> _databaseRepository;
        private readonly Lazy<IDataRepository<tb_Server>> _serverRepository;
        private readonly Lazy<IDataRepository<tb_Table>> _tableRepository;
        private readonly Lazy<IDataRepository<tb_TableColumn>> _tableColumnRepository;
        private readonly Lazy<IDataRepository<tb_ConfigurationContext>> _configurationContextRepository;
        private readonly Lazy<IDataRepository<tb_ConfigurationKey>> _configurationKeyRepository;
        private readonly Lazy<IDataRepository<tb_ConfigurationItem>> _configurationItemRepository;


        public IDataRepository<tb_Application> Application => _applicationRepository.Value;
        public IDataRepository<tb_Claim> Claim => _claimRepository.Value;
        public IDataRepository<tb_Claim_Locale> ClaimLocale => _claimLocaleRepository.Value;
        public IDataRepository<tb_EntityState> EntityState => _entityStateRepository.Value;
        public IDataRepository<tb_EntityState_Locale> EntityStateLocale => _entityStateLocaleRepository.Value;
        public IDataRepository<tb_UserClaim> UserClaim => _userClaimRepository.Value;
        public IDataRepository<tb_User> User => _userRepository.Value;
        public IDataRepository<tb_UserGroup> UserGroup => _userGroupRepository.Value;
        public IDataRepository<tb_UserGroup_Locale> UserGroupLocale => _userGroupLocaleRepository.Value;
        public IDataRepository<tb_UserLogon> UserLogon => _userLogonRepository.Value;
        public IDataRepository<tb_UserLogonLog> UserLogonLog => _userLogonLogRepository.Value;
        public IDataRepository<tb_UserGroupClaim> UserGroupClaim => _userGroupClaimRepository.Value;
        public IDataRepository<tb_UserGroupItem> UserGroupItem => _userGroupItemRepository.Value;
        public IDataRepository<tb_UserMail> UserMail => _userMailRepository.Value;
        public IDataRepository<tb_MemberState> MemberState => _memberStateRepository.Value;
        public IDataRepository<tb_Role> Role => _roleRepository.Value;
        public IDataRepository<tb_Role_Locale> RoleLocale => _roleLocaleRepository.Value;
        public IDataRepository<tb_RoleItem> RoleItem => _roleItemRepository.Value;
        public IDataRepository<tb_UserRole> UserRole => _userRoleRepository.Value;
        public IDataRepository<tb_UserGroupRole> UserGroupRole => _userGroupRoleRepository.Value;
        public IDataRepository<tb_UserEnvironment> UserEnvironment => _userEnvironmentRepository.Value;
        public IDataRepository<tb_UserCompany> UserCompany => _userCompanyRepository.Value;
        public IDataRepository<tb_Supplier> Supplier => _supplierRepository.Value;
        public IDataRepository<tb_SupplierType> SupplierType => _supplierTypeRepository.Value;
        public IDataRepository<tb_Party> Party => _partyRepository.Value;
        public IDataRepository<tb_PartyPerson> PartyPerson => _partyPersonRepository.Value;
        public IDataRepository<tb_SubSegment> SubSegment => _subSegmentRepository.Value;
        public IDataRepository<tb_Segment> Segment => _segmentRepository.Value;
        public IDataRepository<tb_BusinessLine> BusinessLine => _businessLineRepository.Value;
        public IDataRepository<tb_AgeSexGroup> AgeSexGroups => _ageSexGroupRepository.Value;
        public IDataRepository<tb_SupplierSubSegments> SuplierSubSegments => _suplierSubSegmentsRepository.Value;
        public IDataRepository<tb_SupplierBusinessLines> SupplierBusinessLines => _supplierBusinessLinesRepository.Value;
        public IDataRepository<tb_SupplierAgeSexGroups> SupplierAgeSexGroups => _supplierAgeSexGroupsRepository.Value;
        public IDataRepository<tb_UserParty> UserParty => _userPartyRepository.Value;
        public IDataRepository<tb_SegmentSubSegmentRelation> SegmentSubSegmentRelation => _segmentSubSegmentRelationRepository.Value;
        public IDataRepository<tb_SegmentBusinessLineRelation> SegmentBusinessLineRelation => _segmentBusinessLineRelationRepository.Value;
        public IDataRepository<tb_Database> Database => _databaseRepository.Value;
        public IDataRepository<tb_Server> Server => _serverRepository.Value;
        public IDataRepository<tb_Table> Table => _tableRepository.Value;
        public IDataRepository<tb_TableColumn> TableColumn => _tableColumnRepository.Value;
        public IDataRepository<tb_ConfigurationContext> ConfigurationContext => _configurationContextRepository.Value;
        public IDataRepository<tb_ConfigurationKey> ConfigurationKey => _configurationKeyRepository.Value;
        public IDataRepository<tb_ConfigurationItem> ConfigurationItem => _configurationItemRepository.Value;
        public IIdentityProvider IdentityProvider => Pars.Core.Container.Instance.Resolve<IIdentityProvider>();
        
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        private static Lazy<IDataRepository<TEntity>> CreateRepository<TEntity>(IDbContext context)
            where TEntity : EntityBase
        {
            return new Lazy<IDataRepository<TEntity>>(() => new DataRepository<TEntity>(context));

            //return
            //    new Lazy<IDataRepository<TEntity>>(
            //        () =>
            //            Pars.Core.Container.Instance.Resolve<IDataRepository<TEntity>>
            //            (new Dictionary<string, object>
            //            {
            //                {"context", context}
            //            }));
        }
    }
}
