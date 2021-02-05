using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pars.Core.Data;

namespace Pars.Util.Puma.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_UserParty : EntityBase
    {
        [Key]
        public int UserPartyRef { get; set; }

        public int UserRef { get; set; }

        public int PartyRef { get; set; }

        public bool IsActive { get; set; }

        public int? CreatedUserRef { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? ModifiedUserRef { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public Guid? SessionRef { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }

        public override int ObjectRef => UserPartyRef;

    }
}
