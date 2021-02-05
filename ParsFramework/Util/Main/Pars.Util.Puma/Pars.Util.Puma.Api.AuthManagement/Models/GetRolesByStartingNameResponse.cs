using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Api.AuthManagement
{
    [DataContract]
    public class GetRolesByStartingNameResponse : ResponseBase
    {
        [DataMember(Name = "roles")]
        public List<Role> Roles { get; set; }

        public GetRolesByStartingNameResponse()
        {
            Roles = new List<Role>();
        }
    }
}