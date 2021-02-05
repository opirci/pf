using Pars.Core.Data;
using Pars.Core.Localization;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Language: EntityBase, ILocalizable, IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Language()
        {
            tb_Language_Locale = new HashSet<tb_Language_Locale>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short LanguageRef { get; set; }

        [StringLength(50)]
        public string CultureName { get; set; }

        [StringLength(50)]
        public string NativeName { get; set; }

        public bool? IsDefault { get; set; }

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

        [StringLength(50)]
        public string EnglishName { get; set; }

        public int? LanguageId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Language_Locale> tb_Language_Locale { get; set; }

        public override int ObjectRef => this.LanguageRef;
    }
}
