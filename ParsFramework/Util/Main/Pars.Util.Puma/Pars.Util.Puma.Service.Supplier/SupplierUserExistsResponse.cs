using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class SupplierUserExistsResponse : ResponseBase
    {
        [DataMember(Name = "value")]
        public bool Exists { get; set; }
    }
}