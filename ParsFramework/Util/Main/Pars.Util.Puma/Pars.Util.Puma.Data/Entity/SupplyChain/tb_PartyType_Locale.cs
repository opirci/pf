using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Definition.tb_PartyType_Locale")]
    public partial class tb_PartyType_Locale : EntityBase
    {
        [Key]
        public int PartyTypeLocaleRef { get; set; }

        public int PartyTypeRef { get; set; }

        public int LanguageRef { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }

        public virtual tb_PartyType tb_PartyType { get; set; }

        public override int ObjectRef => PartyTypeLocaleRef;
    }
}
