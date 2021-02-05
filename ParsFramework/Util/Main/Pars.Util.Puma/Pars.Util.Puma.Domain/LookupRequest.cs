using Pars.Core.Linq;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service
{
    [DataContract]
    public class LookupRequest : Pars.Core.Service.RequestBase
    {
        [DataMember(Name = "paging")]
        public Paging Paging { get; set; }

        [DataMember(Name = "search")]
        public TextSearch Search { get; set; }

    }
}
