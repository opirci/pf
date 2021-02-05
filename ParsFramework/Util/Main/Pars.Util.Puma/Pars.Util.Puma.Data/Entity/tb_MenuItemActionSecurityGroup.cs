using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_MenuItemActionSecurityGroup: EntityBase,IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_MenuItemActionSecurityGroup()
        {
            tb_MenuItemActionSecurityGroupClaim = new HashSet<tb_MenuItemActionSecurityGroupClaim>();
        }

        [Key]
        public int MenuItemActionSecurityGroupRef { get; set; }

        public int MenuItemRef { get; set; }

        public int ActionSecurityGroupRef { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }

        public Guid? SessionRef { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_MenuItemActionSecurityGroupClaim> tb_MenuItemActionSecurityGroupClaim { get; set; }

        public override int ObjectRef => this.MenuItemActionSecurityGroupRef;
    }
}
