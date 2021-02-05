using Pars.Core.Data;
using Pars.Core.Localization;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_MenuItem: EntityBase, ILocalizable, IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_MenuItem()
        {
            tb_MenuAction = new HashSet<tb_MenuAction>();
            tb_MenuItem_Locale = new HashSet<tb_MenuItem_Locale>();
            tb_MenuItemActionSecurityGroup = new HashSet<tb_MenuItemActionSecurityGroup>();
            tb_MenuItemClaim = new HashSet<tb_MenuItemClaim>();
            tb_MenuItemProperty = new HashSet<tb_MenuItemProperty>();
            tb_MenuItemRole = new HashSet<tb_MenuItemRole>();
            tb_MenuItemUser = new HashSet<tb_MenuItemUser>();
            tb_MenuItemUserGroup = new HashSet<tb_MenuItemUserGroup>();
        }

        [Key]
        public int MenuItemRef { get; set; }

        public Guid MenuItemGuid { get; set; }

        public int MenuRef { get; set; }

        public int? ParentMenuItemRef { get; set; }

        public int ItemLevel { get; set; }

        public int RelativeOrder { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public short MenuItemTypeRef { get; set; }

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
        public virtual ICollection<tb_MenuAction> tb_MenuAction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_MenuItem_Locale> tb_MenuItem_Locale { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_MenuItemActionSecurityGroup> tb_MenuItemActionSecurityGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_MenuItemClaim> tb_MenuItemClaim { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_MenuItemProperty> tb_MenuItemProperty { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_MenuItemRole> tb_MenuItemRole { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_MenuItemUser> tb_MenuItemUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_MenuItemUserGroup> tb_MenuItemUserGroup { get; set; }

        public override int ObjectRef => this.MenuItemRef;
    }
}
