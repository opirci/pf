using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.Definition
{
    [DataContract]
    public class GetDatabasesRequest : RequestBase
    {
        [DataMember(Name = "serverRef")]
        public int ServerRef { get; set; }
    }
}