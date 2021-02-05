using System.Runtime.Serialization;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.DataLocalizer
{
    [DataContract]
    public class SaveLocalesRequest : RequestBase
    {
        [DataMember(Name="value")]
        public DataLocalization Value { get; set; }
    }
}