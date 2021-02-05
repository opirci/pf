using Pars.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Pars.Util.Puma.Api.DataLocalizer.Models
{
    [DataContract]
    public class GetLocalesByRowRequest : RequestBase
    {
        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        [DataMember(Name = "objectRef")]
        public int ObjectRef { get; set; }

        [DataMember(Name = "languages")]
        public List<int> Languages { get; set; }
    }
}