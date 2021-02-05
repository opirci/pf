using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.Definition
{
    [DataContract]
    public class MemberStateListResponse
    {
        [DataMember]
        public List<MemberState> MemberStates { get; set; }
    }
}