using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class GetSupplierResponse : ResponseBase
    {
        [DataMember(Name = "value")]
        public Domain.Supplier Value { get; set; }

        [DataMember(Name = "isAdmin")]
        public bool IsAdmin { get; set; }
    }
}