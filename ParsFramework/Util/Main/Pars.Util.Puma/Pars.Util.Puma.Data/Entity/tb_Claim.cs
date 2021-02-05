using Pars.Core.Data;
using Pars.Core.Localization;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Claim: EntityBase, IAuditable, ILocalizable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Claim()
        {
            tb_Claim_Locale = new HashSet<tb_Claim_Locale>();
            tb_MenuItemActionSecurityGroupClaim = new HashSet<tb_MenuItemActionSecurityGroupClaim>();
            tb_MenuItemClaim = new HashSet<tb_MenuItemClaim>();
            tb_RoleItem = new HashSet<tb_RoleItem>();
            tb_UserClaim = new HashSet<tb_UserClaim>();
            tb_UserGroupClaim = new HashSet<tb_UserGroupClaim>();
        }

        [Key]
        public int ClaimRef { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public short? EntityStateRef { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Claim_Locale> tb_Claim_Locale { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_MenuItemActionSecurityGroupClaim> tb_MenuItemActionSecurityGroupClaim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_MenuItemClaim> tb_MenuItemClaim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_RoleItem> tb_RoleItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UserClaim> tb_UserClaim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_UserGroupClaim> tb_UserGroupClaim { get; set; }

        public override int ObjectRef => this.ClaimRef;
    }
}
