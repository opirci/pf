using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Service.Definition
{
    [DataContract]
    public class GetTableColumnsRequest : RequestBase
    {
        [DataMember(Name = "tableRef")]
        public int TableRef { get; set; }
    }
}