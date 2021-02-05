using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sup.tb_Segment")]
    public partial class tb_Segment : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Segment()
        {
            tb_Supplier = new HashSet<tb_Supplier>();
        }

        [Key]
        public int SegmentRef { get; set; }

        [Required]
        [StringLength(100)]
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
        public virtual ICollection<tb_Supplier> tb_Supplier { get; set; }

        public override int ObjectRef => SegmentRef;
    }
}
