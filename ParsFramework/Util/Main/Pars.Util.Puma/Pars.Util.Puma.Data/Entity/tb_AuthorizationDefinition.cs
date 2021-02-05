using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_AuthorizationDefinition: EntityBase, IAuditable
    {
        [Key]
        public int AuthorizationDefinitionRef { get; set; }

        [StringLength(50)]
        public string Definition { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }
        public override int ObjectRef => this.AuthorizationDefinitionRef;
    }
}
