using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sup.tb_SupplierAgeSexGroups")]
    public partial class tb_SupplierAgeSexGroups : EntityBase
    {
        [Key]
        public int SupplierAgeSexGroupsRef { get; set; }

        public int SupplierRef { get; set; }

        public int AgeSexGroupRef { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }

        public virtual tb_AgeSexGroup tb_AgeSexGroup { get; set; }

        public virtual tb_Supplier tb_Supplier { get; set; }
        public override int ObjectRef => SupplierAgeSexGroupsRef;
    }
}
