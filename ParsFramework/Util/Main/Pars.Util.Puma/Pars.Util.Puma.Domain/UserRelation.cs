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
    public class UserRelation : EntityBase, IRelation
    {
        [DataMember]
        public int RelationRef { get; set; }

        public int RelatedRef { get; set; }

        [DataMember]
        public int UserRef { get; set; }

        [DataMember]
        public string Username { get; set; }

        
        [DataMember]
        public MemberState MemberState { get; set; }

        [DataMember]
        public DateTime ValidFrom { get; set; }

        [DataMember]
        public DateTime ValidTo { get; set; }

        public override int ObjectRef
        {
            get { return RelationRef; }
        }
    }
}
