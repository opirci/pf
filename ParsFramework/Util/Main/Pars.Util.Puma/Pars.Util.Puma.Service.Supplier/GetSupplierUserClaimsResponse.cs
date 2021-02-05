using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class GetSupplierUserClaimsResponse : ResponseBase
    {
        [DataMember(Name = "values")]
        public List<UserClaim> Values { get; set; }
    }
}