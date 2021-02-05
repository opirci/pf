using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    internal partial class PumaContext : DataContext, IPumaContext
    {
        public PumaContext()
            : base("name=PumaContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
        }

        public virtual DbSet<tb_Application> tb_Application { get; set; }
        public virtual DbSet<tb_Application_Locale> tb_Application_Locale { get; set; }
        public virtual DbSet<tb_ApplicationMenu> tb_ApplicationMenu { get; set; }
        public virtual DbSet<tb_AuthorizationDefinition> tb_AuthorizationDefinition { get; set; }
        public virtual DbSet<tb_AuthorizationGroup> tb_AuthorizationGroup { get; set; }
        public virtual DbSet<tb_AuthorizationGroupUser> tb_AuthorizationGroupUser { get; set; }
        public virtual DbSet<tb_Claim> tb_Claim { get; set; }
        public virtual DbSet<tb_Claim_Locale> tb_Claim_Locale { get; set; }
        public virtual DbSet<tb_Database> tb_Database { get; set; }
        public virtual DbSet<tb_Entity> tb_Entity { get; set; }
        public virtual DbSet<tb_EntityState> tb_EntityState { get; set; }
        public virtual DbSet<tb_EntityState_Locale> tb_EntityState_Locale { get; set; }
        public virtual DbSet<tb_EntityStateGroup> tb_EntityStateGroup { get; set; }
        public virtual DbSet<tb_EntityStateGroupItem> tb_EntityStateGroupItem { get; set; }
        public virtual DbSet<tb_Environment> tb_Environment { get; set; }
        public virtual DbSet<tb_Language> tb_Language { get; set; }
        public virtual DbSet<tb_Language_Locale> tb_Language_Locale { get; set; }
        public virtual DbSet<tb_LogonState> tb_LogonState { get; set; }
        public virtual DbSet<tb_LogonState_Locale> tb_LogonState_Locale { get; set; }
        public virtual DbSet<tb_MembershipDefinition> tb_MembershipDefinition { get; set; }
        public virtual DbSet<tb_MemberState> tb_MemberState { get; set; }
        public virtual DbSet<tb_MemberState_Locale> tb_MemberState_Locale { get; set; }
        public virtual DbSet<tb_Menu> tb_Menu { get; set; }
        public virtual DbSet<tb_Menu_Locale> tb_Menu_Locale { get; set; }
        public virtual DbSet<tb_MenuAction> tb_MenuAction { get; set; }
        public virtual DbSet<tb_MenuActionParam> tb_MenuActionParam { get; set; }
        public virtual DbSet<tb_MenuItem> tb_MenuItem { get; set; }
        public virtual DbSet<tb_MenuItem_Locale> tb_MenuItem_Locale { get; set; }
        public virtual DbSet<tb_MenuItemActionSecurityGroup> tb_MenuItemActionSecurityGroup { get; set; }
        public virtual DbSet<tb_MenuItemActionSecurityGroupClaim> tb_MenuItemActionSecurityGroupClaim { get; set; }
        public virtual DbSet<tb_MenuItemClaim> tb_MenuItemClaim { get; set; }
        public virtual DbSet<tb_MenuItemProperty> tb_MenuItemProperty { get; set; }
        public virtual DbSet<tb_MenuItemRole> tb_MenuItemRole { get; set; }
        public virtual DbSet<tb_MenuItemType> tb_MenuItemType { get; set; }
        public virtual DbSet<tb_MenuItemType_Locale> tb_MenuItemType_Locale { get; set; }
        public virtual DbSet<tb_MenuItemUser> tb_MenuItemUser { get; set; }
        public virtual DbSet<tb_MenuItemUserGroup> tb_MenuItemUserGroup { get; set; }
        public virtual DbSet<tb_MenuType> tb_MenuType { get; set; }
        public virtual DbSet<tb_MenuType_Locale> tb_MenuType_Locale { get; set; }
        public virtual DbSet<tb_PwdState> tb_PwdState { get; set; }
        public virtual DbSet<tb_PwdState_Locale> tb_PwdState_Locale { get; set; }
        public virtual DbSet<tb_Role> tb_Role { get; set; }
        public virtual DbSet<tb_Role_Locale> tb_Role_Locale { get; set; }
        public virtual DbSet<tb_RoleEditAuthorization> tb_RoleEditAuthorization { get; set; }
        public virtual DbSet<tb_RoleItem> tb_RoleItem { get; set; }
        public virtual DbSet<tb_Server> tb_Server { get; set; }
        public virtual DbSet<tb_User> tb_User { get; set; }
        public virtual DbSet<tb_UserClaim> tb_UserClaim { get; set; }
        public virtual DbSet<tb_UserCompany> tb_UserCompany { get; set; }
        public virtual DbSet<tb_UserEnvironment> tb_UserEnvironment { get; set; }
        public virtual DbSet<tb_UserGroup> tb_UserGroup { get; set; }
        public virtual DbSet<tb_UserGroup_Locale> tb_UserGroup_Locale { get; set; }
        public virtual DbSet<tb_UserGroupClaim> tb_UserGroupClaim { get; set; }
        public virtual DbSet<tb_UserGroupItem> tb_UserGroupItem { get; set; }
        public virtual DbSet<tb_UserGroupRole> tb_UserGroupRole { get; set; }
        public virtual DbSet<tb_UserLogon> tb_UserLogon { get; set; }
        public virtual DbSet<tb_UserLogonLog> tb_UserLogonLog { get; set; }
        public virtual DbSet<tb_UserMail> tb_UserMail { get; set; }
        public virtual DbSet<tb_UserRole> tb_UserRole { get; set; }
        public virtual DbSet<tb_UserSetting> tb_UserSetting { get; set; }
        public virtual DbSet<tb_UserType> tb_UserType { get; set; }
        public virtual DbSet<tb_UserType_Locale> tb_UserType_Locale { get; set; }
        public virtual DbSet<tb_Table> tb_Table { get; set; }
        public virtual DbSet<tb_TableColumn> tb_TableColumn { get; set; }

        #region Supply Portal
        public virtual DbSet<tb_Country> tb_Country { get; set; }
        public virtual DbSet<tb_PartyType> tb_PartyType { get; set; }
        public virtual DbSet<tb_PartyType_Locale> tb_PartyType_Locale { get; set; }
        public virtual DbSet<tb_Party> tb_Party { get; set; }
        public virtual DbSet<tb_PartyPerson> tb_PartyPerson { get; set; }
        public virtual DbSet<tb_AgeSexGroup> tb_AgeSexGroup { get; set; }
        public virtual DbSet<tb_BusinessLine> tb_BusinessLine { get; set; }
        public virtual DbSet<tb_Segment> tb_Segment { get; set; }
        public virtual DbSet<tb_SegmentBusinessLineRelation> tb_SegmentBusinessLineRelation { get; set; }
        public virtual DbSet<tb_SegmentSubSegmentRelation> tb_SegmentSubSegmentRelation { get; set; }
        public virtual DbSet<tb_SubSegment> tb_SubSegment { get; set; }
        public virtual DbSet<tb_Supplier> tb_Supplier { get; set; }
        public virtual DbSet<tb_SupplierAgeSexGroups> tb_SupplierAgeSexGroups { get; set; }
        public virtual DbSet<tb_SupplierBusinessLines> tb_SupplierBusinessLines { get; set; }
        public virtual DbSet<tb_SupplierSubSegments> tb_SupplierSubSegments { get; set; }
        public virtual DbSet<tb_SupplierType> tb_SupplierType { get; set; }
        public virtual DbSet<tb_UserParty> tb_UserParty { get; set; }
        #endregion

        #region ConfigurationService
        public virtual DbSet<tb_ConfigurationContext> tb_ConfigurationContext { get; set; }
        public virtual DbSet<tb_ConfigurationItem> tb_ConfigurationItem { get; set; }
        public virtual DbSet<tb_ConfigurationKey> tb_ConfigurationKey { get; set; }


        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region MyRegion

            modelBuilder.Types().Configure(c => c.Ignore("EntityState"));

            modelBuilder.Types().Configure(c => c.Ignore("ObjectRef"));

            modelBuilder.Types().Configure(c => c.Ignore("IsNewEntity"));

            modelBuilder.Types().Configure(c => c.Ignore("Error"));

            modelBuilder.Entity<tb_Application>()
                   .Property(e => e.RowVersion)
                   .IsFixedLength();

            modelBuilder.Entity<tb_Application>()
                .HasMany(e => e.tb_UserSetting);

            modelBuilder.Entity<tb_Application_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_ApplicationMenu>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_AuthorizationDefinition>()
                .Property(e => e.Definition)
                .IsUnicode(false);

            modelBuilder.Entity<tb_AuthorizationGroup>()
                .Property(e => e.AuthorizationGroupCode)
                .IsUnicode(false);

            modelBuilder.Entity<tb_AuthorizationGroup>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<tb_Claim>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Claim>()
                .HasMany(e => e.tb_MenuItemActionSecurityGroupClaim);

            modelBuilder.Entity<tb_Claim>()
                .HasMany(e => e.tb_MenuItemClaim);

            modelBuilder.Entity<tb_Claim>()
                .HasMany(e => e.tb_RoleItem);

            modelBuilder.Entity<tb_Claim>()
                .HasMany(e => e.tb_UserClaim);

            modelBuilder.Entity<tb_Claim>()
                .HasMany(e => e.tb_UserGroupClaim);


            modelBuilder.Entity<tb_Claim_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Database>()
               .Property(e => e.RowVersion)
               .IsFixedLength();

            modelBuilder.Entity<tb_EntityState>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_EntityState_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_EntityStateGroup>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_EntityStateGroupItem>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Environment>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Environment>()
                .HasMany(e => e.tb_UserEnvironment);

            modelBuilder.Entity<tb_Language>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Language>()
                .HasMany(e => e.tb_Language_Locale);

            modelBuilder.Entity<tb_Language_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_LogonState>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_LogonState_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MembershipDefinition>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MemberState>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MemberState_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Menu>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Menu>()
                .HasMany(e => e.tb_MenuItem);

            modelBuilder.Entity<tb_Menu_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuAction>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuActionParam>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuItem>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuItem>()
                .HasMany(e => e.tb_MenuItem_Locale);

            modelBuilder.Entity<tb_MenuItem>()
                .HasMany(e => e.tb_MenuItemActionSecurityGroup);

            modelBuilder.Entity<tb_MenuItem>()
                .HasMany(e => e.tb_MenuItemClaim);

            modelBuilder.Entity<tb_MenuItem>()
                .HasMany(e => e.tb_MenuItemProperty);

            modelBuilder.Entity<tb_MenuItem>()
                .HasMany(e => e.tb_MenuItemRole);

            modelBuilder.Entity<tb_MenuItem>()
                .HasMany(e => e.tb_MenuItemUser);

            modelBuilder.Entity<tb_MenuItem>()
                .HasMany(e => e.tb_MenuItemUserGroup);

            modelBuilder.Entity<tb_MenuItem_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuItemActionSecurityGroup>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuItemActionSecurityGroup>()
                .HasMany(e => e.tb_MenuItemActionSecurityGroupClaim);

            modelBuilder.Entity<tb_MenuItemActionSecurityGroupClaim>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuItemClaim>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuItemProperty>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuItemRole>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuItemType>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuItemType>()
                .HasMany(e => e.tb_MenuItem);

            modelBuilder.Entity<tb_MenuItemType_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuItemUser>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuItemUserGroup>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuType>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_MenuType>()
                .HasMany(e => e.tb_Menu);

            modelBuilder.Entity<tb_MenuType>()
                .HasMany(e => e.tb_MenuType_Locale);

            modelBuilder.Entity<tb_MenuType_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_PwdState>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_PwdState_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Role>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Role>()
                .HasMany(e => e.tb_MenuItemRole);

            modelBuilder.Entity<tb_Role>()
                .HasMany(e => e.tb_RoleItem);

            modelBuilder.Entity<tb_Role>()
                .HasMany(e => e.tb_UserGroupRole);

            modelBuilder.Entity<tb_Role>()
                .HasMany(e => e.tb_UserRole);

            modelBuilder.Entity<tb_Role_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_RoleEditAuthorization>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_RoleItem>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Server>()
              .Property(e => e.RowVersion)
              .IsFixedLength();

            modelBuilder.Entity<tb_Table>()
              .Property(e => e.RowVersion)
              .IsFixedLength();

            modelBuilder.Entity<tb_TableColumn>()
               .Property(e => e.RowVersion)
               .IsFixedLength();

            modelBuilder.Entity<tb_User>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_User>()
                .HasMany(e => e.tb_MenuItemUser);

            modelBuilder.Entity<tb_User>()
                .HasMany(e => e.tb_UserClaim);

            modelBuilder.Entity<tb_User>()
                .HasMany(e => e.tb_UserCompany);

            modelBuilder.Entity<tb_User>()
                .HasMany(e => e.tb_UserEnvironment);

            modelBuilder.Entity<tb_User>()
                .HasMany(e => e.tb_UserGroupItem);

            modelBuilder.Entity<tb_User>()
                .HasMany(e => e.tb_UserRole);

            modelBuilder.Entity<tb_User>()
                .HasMany(e => e.tb_UserSetting);

            modelBuilder.Entity<tb_UserClaim>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserCompany>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserEnvironment>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserGroup>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserGroup>()
                .HasMany(e => e.tb_MenuItemUserGroup);

            modelBuilder.Entity<tb_UserGroup>()
                .HasMany(e => e.tb_UserGroupClaim);

            modelBuilder.Entity<tb_UserGroup>()
                .HasMany(e => e.tb_UserGroupItem);

            modelBuilder.Entity<tb_UserGroup>()
                .HasMany(e => e.tb_UserGroupRole);

            modelBuilder.Entity<tb_UserGroup_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserGroupClaim>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserGroupItem>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserGroupRole>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserLogon>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserLogonLog>()
               .Property(e => e.RowVersion)
               .IsFixedLength();

            modelBuilder.Entity<tb_UserMail>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserRole>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserSetting>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserType>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_UserType>()
                .HasMany(e => e.tb_UserType_Locale);

            modelBuilder.Entity<tb_UserType_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            #endregion

            #region ConfigurationService
            modelBuilder.Entity<tb_ConfigurationKey>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_ConfigurationItem>()
                .Property(e => e.RowVersion)
                .IsFixedLength();
            modelBuilder.Entity<tb_ConfigurationContext>()
                .Property(e => e.RowVersion)
                .IsFixedLength();
            modelBuilder.Entity<tb_ConfigurationContext>()
                .HasMany(e => e.tb_ConfigurationItem)
                .WithRequired();
            modelBuilder.Entity<tb_ConfigurationKey>()
                .HasMany(e => e.tb_ConfigurationItem)
                .WithRequired();
            #endregion

            #region Supply Portal

            modelBuilder.Entity<tb_Country>()
              .Property(e => e.ISO2Code)
              .IsUnicode(false);

            modelBuilder.Entity<tb_Country>()
                .Property(e => e.ISO3Code)
                .IsUnicode(false);

            modelBuilder.Entity<tb_Country>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<tb_Country>()
                .Property(e => e.ShortName)
                .IsUnicode(false);

            modelBuilder.Entity<tb_Country>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Country>()
                .HasMany(e => e.tb_Supplier)
                .WithRequired(e => e.tb_Country)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_PartyType>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_PartyType>()
                .HasMany(e => e.tb_Party)
                .WithRequired(e => e.tb_PartyType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_PartyType_Locale>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Party>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Party>()
                .HasMany(e => e.tb_PartyPerson)
                .WithRequired(e => e.tb_Party)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_Party>()
                .HasMany(e => e.tb_Supplier)
                .WithRequired(e => e.tb_Party)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_PartyPerson>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_AgeSexGroup>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_AgeSexGroup>()
                .HasMany(e => e.tb_SupplierAgeSexGroups)
                .WithRequired(e => e.tb_AgeSexGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_BusinessLine>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_BusinessLine>()
                .HasMany(e => e.tb_SupplierBusinessLines)
                .WithRequired(e => e.tb_BusinessLine)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_Segment>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Segment>()
                .HasMany(e => e.tb_Supplier)
                .WithRequired(e => e.tb_Segment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_SegmentBusinessLineRelation>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_SegmentSubSegmentRelation>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_SubSegment>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_SubSegment>()
                .HasMany(e => e.tb_SupplierSubSegments)
                .WithRequired(e => e.tb_SubSegment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_Supplier>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_Supplier>()
                .HasOptional(e => e.tb_Supplier1)
                .WithRequired(e => e.tb_Supplier2);

            modelBuilder.Entity<tb_Supplier>()
                .HasMany(e => e.tb_SupplierAgeSexGroups)
                .WithRequired(e => e.tb_Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_Supplier>()
                .HasMany(e => e.tb_SupplierBusinessLines)
                .WithRequired(e => e.tb_Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_Supplier>()
                .HasMany(e => e.tb_SupplierSubSegments)
                .WithRequired(e => e.tb_Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_SupplierAgeSexGroups>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_SupplierBusinessLines>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_SupplierSubSegments>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_SupplierType>()
                .Property(e => e.RowVersion)
                .IsFixedLength();

            modelBuilder.Entity<tb_SupplierType>()
                .HasMany(e => e.tb_Supplier)
                .WithRequired(e => e.tb_SupplierType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_UserParty>()
              .Property(e => e.RowVersion)
              .IsFixedLength();

            #endregion
        }
    }
}
