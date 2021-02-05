using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class ValidateUserResponse : ResponseBase
    {
        [DataMember(Name = "isValid")]
        public bool IsValid { get; set; }
    }
}