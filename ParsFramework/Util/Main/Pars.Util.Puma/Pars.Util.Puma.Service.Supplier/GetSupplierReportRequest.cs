using System.Runtime.Serialization;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class GetSupplierReportRequest : RequestBase
    {
        [DataMember(Name = "supplierCode")]
        public string SupplierCode { get; set; }

        [DataMember(Name = "supplierName")]
        public string SupplierName { get; set; }

        [DataMember(Name = "countries")]
        public int[] Countries { get; set; }

        [DataMember(Name = "segments")]
        public int[] Segments { get; set; }

        [DataMember(Name = "supplierType")]
        public int SupplierType { get; set; }

        [DataMember(Name = "userType")]
        public SupplierUserType? UserType { get; set; }

        [DataMember(Name = "logOnActive")]
        public bool LogOnActive { get; set; }

        [DataMember(Name = "dateRange")]
        public DateRange DateRange { get; set; }

        [DataMember(Name = "modifiedUser")]
        public string ModifiedUser { get; set; }
    }
}