using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.DataLocalizer
{
    [DataContract]
    public class GetLocalesByRowResponse : ResponseBase
    {
        [DataMember(Name = "value")]
        public DataLocalization Value { get; set; }
    }
}