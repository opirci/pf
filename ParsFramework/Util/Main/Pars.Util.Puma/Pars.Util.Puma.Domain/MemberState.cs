using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class MemberState
    {
        [DataMember]
        public short MemberStateRef { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
