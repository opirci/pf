using Pars.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Data
{
    public class tb_ConfigurationKey : EntityBase, IAuditable
    {
        [Key]
        public int ConfigurationKeyRef { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public override int ObjectRef => ConfigurationKeyRef;
        public int CreatedUserRef { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedUserRef { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid? SessionRef { get; set; }
        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_ConfigurationItem> tb_ConfigurationItem { get; set; }
    }
}
