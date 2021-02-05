using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class ValidateUserRequest : RequestBase
    {
        [DataMember(Name = "userRef")]
        public int UserRef { get; set; }

        [DataMember(Name = "supplierCode")]
        public string SupplierCode { get; set; }
    }
}