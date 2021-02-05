using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    public class tb_ConfigurationItem:EntityBase, IAuditable
    {
        [Key]
        public int ConfigurationItemRef { get; set; }

        public int ContextRef { get; set; }
        public int ConfigurationKeyRef { get; set; }
        public string Value { get; set; }
        public override int ObjectRef => ConfigurationItemRef;
        public int CreatedUserRef { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedUserRef { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid? SessionRef { get; set; }
        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }
    }
}
