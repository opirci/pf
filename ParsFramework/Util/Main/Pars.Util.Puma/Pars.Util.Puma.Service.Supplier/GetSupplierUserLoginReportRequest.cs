using Pars.Util.Puma.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class GetSupplierUserLoginReportRequest : RequestBase
    {
        //[DataMember(Name = "range")]
        //public DateWeekRange Range { get; set; }

        [DataMember(Name = "startYear")]
        public int StartYear { get; set; }

        [DataMember(Name = "startWeek")]
        public int StartWeek { get; set; }

        [DataMember(Name = "endYear")]
        public int EndYear { get; set; }

        [DataMember(Name = "endWeek")]
        public int EndWeek { get; set; }        

        [DataMember(Name = "reportType")]
        public byte ReportType { get; set; }
    }
}