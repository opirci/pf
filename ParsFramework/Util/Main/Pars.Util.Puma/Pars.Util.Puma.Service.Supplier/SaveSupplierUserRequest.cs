using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class SaveSupplierUserRequest : RequestBase
    {
        [DataMember(Name = "value")]
        public SupplierUser SupplierUser { get; set; }

        [DataMember(Name = "hasAdminClaim")]
        public bool HasAdminClaim { get; set; }

        [DataMember(Name = "hasManagerClaim")]
        public bool HasManagerClaim { get; set; }

        [DataMember(Name = "hasFinanceClaim")]
        public bool HasFinanceClaim { get; set; }

        [DataMember(Name = "countryRef")]
        public int CountryRef { get; set; }
    }
}