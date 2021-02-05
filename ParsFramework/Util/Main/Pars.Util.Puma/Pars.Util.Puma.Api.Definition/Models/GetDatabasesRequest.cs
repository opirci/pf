using Pars.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Pars.Util.Puma.Api.Definition.Models
{
    [DataContract]
    public class GetDatabasesRequest : RequestBase
    {
        [DataMember(Name = "serverRef")]
        public int ServerRef { get; set; }
    }
}