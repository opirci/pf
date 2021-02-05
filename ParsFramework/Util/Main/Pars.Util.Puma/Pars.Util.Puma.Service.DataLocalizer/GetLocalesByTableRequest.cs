using System.Collections.Generic;
using System.Runtime.Serialization;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.DataLocalizer
{
    [DataContract]
    public class GetLocalesByTableRequest : RequestBase
    {
        [DataMember(Name = "value")]
        public DataLocalization Value { get; set; }

        [DataMember(Name = "languages")]
        public List<int> Languages { get; set; }
    }
}