using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_MenuActionParam: EntityBase, IAuditable
    {
        [Key]
        public int MenuActionParamRef { get; set; }

        public int? MenuActionRef { get; set; }

        public int? ActionParamRef { get; set; }

        public string ParamValue { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }

        public override int ObjectRef => this.MenuActionParamRef;
    }
}
