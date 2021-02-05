using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class GetSupplierRequest : RequestBase
    {
        [DataMember(Name = "supplierCode")]
        public string SupplierCode { get; set; }
    }
}