using System.Collections.Generic;
using System.Runtime.Serialization;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Api.AuthManagement
{
    [DataContract]
    public class GetRolesByClaimRefResponse : ResponseBase
    {
        [DataMember(Name = "values")]
        public List<RoleRelation> Values { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}