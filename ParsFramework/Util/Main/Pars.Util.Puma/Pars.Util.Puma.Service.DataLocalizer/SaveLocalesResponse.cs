using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.DataLocalizer
{
    [DataContract]
    public class SaveLocalesResponse : ResponseBase
    {
        [DataMember(Name = "isSuccess")]
        public bool IsSuccess { get; set; }
    }
}