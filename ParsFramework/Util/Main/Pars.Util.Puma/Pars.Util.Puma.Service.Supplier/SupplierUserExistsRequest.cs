using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class SupplierUserExistsRequest : RequestBase
    {
        [DataMember(Name = "mail")]
        public string Mail { get; set; }
    }
}