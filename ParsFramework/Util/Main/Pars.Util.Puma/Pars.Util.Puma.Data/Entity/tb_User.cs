using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_User: EntityBase, IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_User()
        {
            tb_MenuItemUser = new HashSet<tb_MenuItemUser>();
            tb_UserClaim = new HashSet<tb_UserClaim>();
            tb_UserCompany = new HashSet<tb_UserCompany>();
            tb_UserEnvironment = new HashSet<tb_UserEnvironment>();
            tb_UserGroupItem = new HashSet<tb_UserGroupItem>();
            tb_UserLogon = new HashSet<tb_UserLogon>();
            tb_UserRole = new HashSet<tb_UserRole>();
            tb_UserSetting = new HashSet<tb_UserSetting>();
        }

        [Key]
        public int UserRef { get; set; }

        public Guid UserUid { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Domain { get; set; }

        public bool? IsDomainUser { get; set; }

        public int? PartyRef { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(50)]
        public string SurName { get; set; }

        public short? UserTypeRef { get; set; }

        public int? StartMenuRef { get; set; }

        public int? LanguageRef { get; set; }

        public short? TimeZoneRef { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }

        public int? PersonelID { get; set; }

        public int? HRPersonelRef { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_MenuItemUser> tb_MenuItemUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UserClaim> tb_UserClaim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UserCompany> tb_UserCompany { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UserEnvironment> tb_UserEnvironment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UserGroupItem> tb_UserGroupItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UserLogon> tb_UserLogon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UserRole> tb_UserRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UserSetting> tb_UserSetting { get; set; }

        public override int ObjectRef => this.UserRef;
    }
}
