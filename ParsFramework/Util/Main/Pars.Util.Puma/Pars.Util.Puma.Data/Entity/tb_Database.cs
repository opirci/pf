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
    public partial class tb_Database : EntityBase, IAuditable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_Database()
        {
            tb_Table = new HashSet<tb_Table>();
        }

        public override int ObjectRef => this.DatabaseRef;

        [Key]
        public int DatabaseRef { get; set; }

        public int ServerRef { get; set; }

        public int? ApplicationRef { get; set; }

        public int? EnvironmentRef { get; set; }

        [Required]
        [StringLength(100)]
        public string DatabaseName { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_Table> tb_Table { get; set; }
    }
}
