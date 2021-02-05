using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Pars.Util.Puma.Service.AuthManagement
{
    [DataContract]
    public class UserGroupRelationRequest
    {
        [DataMember]
        public int ClaimRef { get; set; }

        [DataMember]
        public int PageNumber { get; set; }

        [DataMember]
        public int PageSize { get; set; }
    }
}