using Pars.Core.Data;
using Pars.Core.Localization;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_MenuItemType: EntityBase, ILocalizable, IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_MenuItemType()
        {
            tb_MenuItem = new HashSet<tb_MenuItem>();
            tb_MenuItemType_Locale = new HashSet<tb_MenuItemType_Locale>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short MenuItemTypeRef { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

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
        public virtual ICollection<tb_MenuItem> tb_MenuItem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_MenuItemType_Locale> tb_MenuItemType_Locale { get; set; }

        public override int ObjectRef => this.MenuItemTypeRef;
    }
}
