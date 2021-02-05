using System.Collections.Generic;
using System.Runtime.Serialization;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.DataLocalizer
{
    [DataContract]
    public class GetLocalesByTableResponse : ResponseBase
    {
        [DataMember(Name = "value")]
        public DataLocalization Value { get; set; }
    }
}