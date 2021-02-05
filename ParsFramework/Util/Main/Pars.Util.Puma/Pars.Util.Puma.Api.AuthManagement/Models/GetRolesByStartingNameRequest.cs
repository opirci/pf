using Pars.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Pars.Util.Puma.Api.AuthManagement
{
    [DataContract]
    public class GetRolesByStartingNameRequest : RequestBase
    {
        [DataMember(Name = "searchText")]
        public string SearchText { get; set; }
    }
}