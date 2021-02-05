using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class UserClaim : DomainBase<int>
    {
        public override int Ref => this.UserClaimRef;

        [DataMember(Name = "userClaimRef")]
        public int UserClaimRef { get; set; }

        [DataMember(Name = "userRef")]
        public int UserRef { get; set; }

        [DataMember(Name = "claimRef")]
        public int ClaimRef { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

    }
}
