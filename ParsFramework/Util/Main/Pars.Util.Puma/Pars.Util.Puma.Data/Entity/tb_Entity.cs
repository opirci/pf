using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_Entity:EntityBase, IAuditable, ISoftDeletable
    {
        [Key]
        public int EntityRef { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }

        public bool IsDeleted { get; set; }
        public override int ObjectRef => this.EntityRef;
    }
}
