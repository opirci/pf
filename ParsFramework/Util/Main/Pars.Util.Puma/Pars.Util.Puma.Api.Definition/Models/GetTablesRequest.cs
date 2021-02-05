using Pars.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Pars.Util.Puma.Api.Definition.Models
{
    [DataContract]
    public class GetTablesRequest : RequestBase
    {
        [DataMember(Name = "databaseRef")]
        public int DatabaseRef { get; set; }
    }
}