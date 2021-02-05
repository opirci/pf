using Pars.Util.Puma.Domain;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Api.DataLocalizer.Models
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