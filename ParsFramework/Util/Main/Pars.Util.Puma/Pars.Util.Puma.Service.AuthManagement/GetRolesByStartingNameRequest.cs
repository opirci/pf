using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.AuthManagement
{
    [DataContract]
    public class GetRolesByStartingNameRequest : RequestBase
    {
        [DataMember(Name = "searchText")]
        public string SearchText { get; set; }
    }
}