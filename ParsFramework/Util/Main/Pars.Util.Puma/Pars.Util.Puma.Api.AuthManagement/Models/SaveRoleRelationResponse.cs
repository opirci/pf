﻿using System.Runtime.Serialization;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Api.AuthManagement
{
    [DataContract]
    public class SaveRoleRelationResponse : ResponseBase// : ResponseBase<RoleRelation>
    {
        [DataMember]
        public RoleRelation Value { get; set; }
    }
}