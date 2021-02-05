using Pars.Util.Puma.Domain;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.DataLocalizer
{
    [DataContract]
    public class GetLocalesByRowRequest: RequestBase
    {
        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        [DataMember(Name = "objectRef")]
        public int ObjectRef { get; set; }

        [DataMember(Name = "languages")]
        public List<int> Languages { get; set; }
    }
}