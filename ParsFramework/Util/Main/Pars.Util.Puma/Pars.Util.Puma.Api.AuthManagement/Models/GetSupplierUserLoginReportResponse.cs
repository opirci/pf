using Pars.Util.Puma.Domain;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Api.AuthManagement
{
    [DataContract]
    public class GetSupplierUserLoginReportResponse : ResponseBase
    {
        [DataMember(Name = "value")]
        public DataTableProxy Value { get; set; }
        [DataMember(Name = "reportType")]
        public byte ReportType { get; set; }
    }
}