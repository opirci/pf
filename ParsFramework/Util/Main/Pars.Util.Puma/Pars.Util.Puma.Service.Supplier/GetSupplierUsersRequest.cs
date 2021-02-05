using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class GetSupplierUsersRequest : RequestBase
    {
        [DataMember(Name = "partyRef")]
        public int PartyRef { get; set; }

        [DataMember(Name = "status")]
        public int Status { get; set; }
    }
}