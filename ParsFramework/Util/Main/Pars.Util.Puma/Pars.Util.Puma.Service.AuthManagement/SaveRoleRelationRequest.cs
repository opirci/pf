using System.Runtime.Serialization;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.AuthManagement
{
    [DataContract]
    public class SaveRoleRelationRequest : RequestBase
    {
        [DataMember]
        public RoleRelation RoleRelation { get; set; }
        
    }
}