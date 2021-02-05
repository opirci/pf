using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Pars.Core.Data;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class UserGroupRelation : DomainBase<int>, IRelation
    {
        [DataMember]
        public int UserGroupRef { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int RelationRef { get; set; }

        public int RelatedRef { get; set; }

        [DataMember]
        public MemberState MemberState { get; set; }
        [DataMember]
        public DateTime ValidFrom { get; set; }
        [DataMember]
        public DateTime ValidTo { get; set; }

        public override int Ref => RelationRef;
    }
}
