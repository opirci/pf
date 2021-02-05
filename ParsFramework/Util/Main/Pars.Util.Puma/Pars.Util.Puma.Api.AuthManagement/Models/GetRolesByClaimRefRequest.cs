using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Pars.Core.Service;


namespace Pars.Util.Puma.Api.AuthManagement
{
    [DataContract]
    public class GetRolesByClaimRefRequest : RequestBase
    {
        [DataMember(Name = "value")]
        public int Value { get; set; }

        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }

        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }

        [DataMember(Name = "searchText")]
        public string SearchText { get; set; }
    }
}