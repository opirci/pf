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
    public class tb_ConfigurationContext : EntityBase, IAuditable
    {
        [Key]
        public int ContextRef { get; set; }
        public override int ObjectRef => ContextRef;
        public int ApplicationRef { get; set; }
        public string Name { get; set; }
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
