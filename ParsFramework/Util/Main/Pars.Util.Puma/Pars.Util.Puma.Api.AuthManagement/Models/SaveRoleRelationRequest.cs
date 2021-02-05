using System.Runtime.Serialization;
using Pars.Util.Puma.Domain;
using Pars.Core.Service;

namespace Pars.Util.Puma.Api.AuthManagement
{
    [DataContract]
    public class SaveRoleRelationRequest : Pars.Core.Service.RequestBase
    {
        [DataMember]
        public RoleRelation RoleRelation { get; set; }
        
    }
}