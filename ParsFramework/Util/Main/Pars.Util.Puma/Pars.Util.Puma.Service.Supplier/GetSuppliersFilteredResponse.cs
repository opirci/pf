using Pars.Util.Puma.Domain;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class GetSuppliersFilteredResponse : ResponseBase
    {
        [DataMember(Name = "values")]
        public List<Domain.Supplier> Values { get; set; }

        [DataMember(Name = "count")]
        public int Count { get; set; }
    }
}