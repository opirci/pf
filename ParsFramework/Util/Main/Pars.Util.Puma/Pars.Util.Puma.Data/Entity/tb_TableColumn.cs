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
    public partial class tb_TableColumn : EntityBase, IAuditable
    {
        public override int ObjectRef => this.TableColumnRef;

        [Key]
        public int TableColumnRef { get; set; }

        public int TableRef { get; set; }

        [Required]
        [StringLength(100)]
        public string TableColumnName { get; set; }

        public Guid TableColumnGuid { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public int CreatedUserRef { get; set; }

        public DateTime CreatedDate { get; set; }

        public int ModifiedUserRef { get; set; }

        public DateTime ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }

        public virtual tb_Table tb_Table { get; set; }
    }
}
