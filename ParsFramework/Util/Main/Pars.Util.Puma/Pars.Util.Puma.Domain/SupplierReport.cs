using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public struct SupplierUserType
    {
        [DataMember(Name = "isAdmin")]
        public bool IsAdmin { get; set; }

        [DataMember(Name = "isManager")]
        public bool IsManager { get; set; }

        [DataMember(Name = "isFinance")]
        public bool IsFinance { get; set; }

        [DataMember(Name = "isRepresentative")]
        public bool IsRepresentative { get; set; }
    }

    [DataContract]
    public class SupplierReportLine
    {
        [DataMember(Name = "supplierCode")]
        public string SupplierCode { get; set; }

        [DataMember(Name = "supplierName")]
        public string SupplierName { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "segmentName")]
        public string SegmentName { get; set; }


        [DataMember(Name = "subSegmentName")]
        public string SubSegmentName { get; set; }

        [DataMember(Name = "supplierType")]
        public string SupplierType { get; set; }

        [DataMember(Name = "userName")]
        public string UserName { get; set; }

        [DataMember(Name = "userSurname")]
        public string UserSurname { get; set; }


        [DataMember(Name = "userEmail")]
        public string UserEmail { get; set; }

        [DataMember(Name = "modifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [DataMember(Name = "modifiedUser")]
        public string ModifiedUser { get; set; }


        //[DataMember(Name = "isAdmin")]
        //public bool IsAdmin { get; set; }

        [DataMember(Name = "isManager")]
        public bool IsManager { get; set; }

        [DataMember(Name = "isFinance")]
        public bool IsFinance { get; set; }

        [DataMember(Name = "isRepresentative")]
        public bool IsRepresentative { get; set; }

        [DataMember(Name = "logonState")]
        public bool LogonState { get; set; }
    }
}
