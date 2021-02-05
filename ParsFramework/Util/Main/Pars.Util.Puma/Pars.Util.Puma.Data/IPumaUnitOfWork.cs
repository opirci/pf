using Pars.Core.Data;
using Pars.Core.Security;

namespace Pars.Util.Puma.Data
{
    public interface IPumaUnitOfWork : IDataUnitOfWork
    {
        IDataRepository<tb_Application> Application { get; }
        IDataRepository<tb_Claim> Claim { get; }
        IDataRepository<tb_Claim_Locale> ClaimLocale { get; }
        IDataRepository<tb_ConfigurationContext> ConfigurationContext { get; }
        IDataRepository<tb_ConfigurationItem> ConfigurationItem { get; }
        IDataRepository<tb_ConfigurationKey> ConfigurationKey { get; }
        IDataRepository<tb_Database> Database { get; }
        IDataRepository<tb_EntityState> EntityState { get; }
        IDataRepository<tb_EntityState_Locale> EntityStateLocale { get; }
        IDataRepository<tb_UserClaim> UserClaim { get; }
        IDataRepository<tb_User> User { get; }
        IDataRepository<tb_UserGroup> UserGroup { get; }
        IDataRepository<tb_UserGroup_Locale> UserGroupLocale { get; }
        IDataRepository<tb_Role> Role { get; }
        IDataRepository<tb_Role_Locale> RoleLocale { get; }
        IDataRepository<tb_RoleItem> RoleItem { get; }
        IDataRepository<tb_UserGroupRole> UserGroupRole { get; }
        IDataRepository<tb_UserRole> UserRole { get; }
        IDataRepository<tb_UserLogon> UserLogon { get; }
        IDataRepository<tb_UserLogonLog> UserLogonLog { get; }
        IDataRepository<tb_UserGroupClaim> UserGroupClaim { get; }
        IDataRepository<tb_UserGroupItem> UserGroupItem { get; }
        IDataRepository<tb_UserMail> UserMail { get; }
        IDataRepository<tb_UserEnvironment> UserEnvironment { get; }
        IDataRepository<tb_UserCompany> UserCompany { get; }
        IDataRepository<tb_MemberState> MemberState { get; }
        IDataRepository<tb_Supplier> Supplier { get; }
        IDataRepository<tb_SupplierType> SupplierType { get; }
        IDataRepository<tb_Party> Party { get; }
        IDataRepository<tb_PartyPerson> PartyPerson { get; }
        IDataRepository<tb_SubSegment> SubSegment { get; }
        IDataRepository<tb_Segment> Segment { get; }
        IDataRepository<tb_BusinessLine> BusinessLine { get; }
        IDataRepository<tb_AgeSexGroup> AgeSexGroups { get; }
        IDataRepository<tb_SupplierSubSegments> SuplierSubSegments { get; }
        IDataRepository<tb_SupplierBusinessLines> SupplierBusinessLines { get; }
        IDataRepository<tb_SupplierAgeSexGroups> SupplierAgeSexGroups { get; }
        IDataRepository<tb_UserParty> UserParty { get; }
        IDataRepository<tb_SegmentSubSegmentRelation> SegmentSubSegmentRelation { get; }
        IDataRepository<tb_SegmentBusinessLineRelation> SegmentBusinessLineRelation { get; }
        IDataRepository<tb_Server> Server { get; }
        IDataRepository<tb_Table> Table { get; }
        IDataRepository<tb_TableColumn> TableColumn { get; }
        IIdentityProvider IdentityProvider { get; }

    }
}
