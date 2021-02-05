using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Api.DataLocalizer.Models
{
    [DataContract]
    public class GetLocalesByTableResponse : ResponseBase
    {
        [DataMember(Name = "value")]
        public DataLocalization Value { get; set; }
    }
}