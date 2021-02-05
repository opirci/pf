using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.AuthManagement
{
    [DataContract]
    public class UserGroupRelationResponse
    {
        [DataMember]
        public List<UserGroupRelation> UserGroups { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}