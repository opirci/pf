using Pars.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Pars.Util.Puma.Api.DataLocalizer.Models
{
    [DataContract]
    public class SaveLocalesResponse : ResponseBase
    {
        [DataMember(Name = "isSuccess")]
        public bool IsSuccess { get; set; }
    }
}