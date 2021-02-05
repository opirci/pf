using Pars.Util.Puma.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class GetSupplierUserLoginReportResponse : ResponseBase
    {
        [DataMember(Name = "value")]
        public DataTableProxy Value { get; set; }
        [DataMember(Name = "ReportType")]
        public byte ReportType { get; set; }
    }
}