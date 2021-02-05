using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_MenuItem_Locale: EntityBase, IAuditable, ILocale
    {
        [Key]
        public int MenuItemLocaleRef { get; set; }

        public int MenuItemRef { get; set; }

        public int LanguageRef { get; set; }

        [StringLength(100)]
        public string Text { get; set; }

        [StringLength(255)]
        public string Hint { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

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

        public override int ObjectRef => this.MenuItemLocaleRef;
    }
}
