using System.Collections.Generic;
using System.Runtime.Serialization;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class GetSupplierReportResponse : ResponseBase
    {
        [DataMember(Name = "values")]
        public List<SupplierReportLine> Values { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}
