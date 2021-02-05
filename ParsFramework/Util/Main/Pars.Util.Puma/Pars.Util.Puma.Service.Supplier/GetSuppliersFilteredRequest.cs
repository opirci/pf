using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Pars.Core.Service;

namespace Pars.Util.Puma.Service.Supplier
{
    [DataContract]
    public class GetSuppliersFilteredRequest : RequestBase
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "hasNoUsers")]
        public bool HasNoUsers { get; set; }

        [DataMember(Name = "supplierType")]
        public int SupplierType { get; set; }

        [DataMember(Name = "segment")]
        public int Segment { get; set; }

        [DataMember(Name = "subSegments")]
        public int[] SubSegments { get; set; }

        [DataMember(Name = "businessLines")]
        public int[] BusinessLines { get; set; }

        [DataMember(Name = "ageSexGroups")]
        public int[] AgeSexGroups { get; set; }

        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }

        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }
    }
}