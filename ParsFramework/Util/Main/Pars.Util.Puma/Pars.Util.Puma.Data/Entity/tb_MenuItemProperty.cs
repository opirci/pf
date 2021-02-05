using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_MenuItemProperty: EntityBase, IAuditable
    {
        [Key]
        public int MenuItemPropertyRef { get; set; }

        public int MenuItemRef { get; set; }

        public int? SmallIconRef { get; set; }

        [StringLength(50)]
        public string SmallIconCssName { get; set; }

        public int? LargeIconRef { get; set; }

        [StringLength(50)]
        public string LargeIconCssName { get; set; }

        public int? StyleRef { get; set; }

        public bool? IsSeperator { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }

        public override int ObjectRef => this.MenuItemPropertyRef;
    }
}
