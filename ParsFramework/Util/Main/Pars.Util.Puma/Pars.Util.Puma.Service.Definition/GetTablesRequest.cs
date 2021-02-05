using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.Definition
{
    [DataContract]
    public class GetTablesRequest : RequestBase
    {
        [DataMember(Name = "databaseRef")]
        public int DatabaseRef { get; set; }
    }
}