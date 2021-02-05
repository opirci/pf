using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sup.tb_Supplier")]
    public partial class tb_Supplier : EntityBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Supplier()
        {
            tb_SupplierAgeSexGroups = new HashSet<tb_SupplierAgeSexGroups>();
            tb_SupplierBusinessLines = new HashSet<tb_SupplierBusinessLines>();
            tb_SupplierSubSegments = new HashSet<tb_SupplierSubSegments>();
        }

        [Key]
        public int SupplierRef { get; set; }

        [Required]
        [StringLength(100)]
        public string SupplierCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public int PartyRef { get; set; }

        public int CountryRef { get; set; }

        public int SegmentRef { get; set; }

        public int SupplierTypeRef { get; set; }

        public bool? IsActive { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }

        public virtual tb_Country tb_Country { get; set; }

        public virtual tb_Party tb_Party { get; set; }

        public virtual tb_Segment tb_Segment { get; set; }

        public virtual tb_Supplier tb_Supplier1 { get; set; }

        public virtual tb_Supplier tb_Supplier2 { get; set; }

        public virtual tb_SupplierType tb_SupplierType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_SupplierAgeSexGroups> tb_SupplierAgeSexGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_SupplierBusinessLines> tb_SupplierBusinessLines { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_SupplierSubSegments> tb_SupplierSubSegments { get; set; }

        public override int ObjectRef => SupplierRef;
    }
}
