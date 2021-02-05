using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class GetSupplierUserClaimsRequest : RequestBase
    {
        [DataMember(Name = "userRef")]
        public int UserRef { get; set; }
    }
}