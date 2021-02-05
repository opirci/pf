using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_AuthorizationGroup:EntityBase, IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_AuthorizationGroup()
        {
            tb_AuthorizationGroupUser = new HashSet<tb_AuthorizationGroupUser>();
        }

        [Key]
        public int AuthorizationGroupRef { get; set; }

        [StringLength(50)]
        public string AuthorizationGroupCode { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_AuthorizationGroupUser> tb_AuthorizationGroupUser { get; set; }

        public override int ObjectRef => this.AuthorizationGroupRef;
    }
}
