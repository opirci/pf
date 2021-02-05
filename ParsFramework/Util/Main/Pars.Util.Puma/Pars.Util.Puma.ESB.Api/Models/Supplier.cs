using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pars.Util.Puma.ESB.Api.Models
{
    public class Supplier
    {
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string CountryISOCode { get; set; }
        public string SupplierType { get; set; }
        public string Segment { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
    }

    public class SupplierObject
    {
        public Supplier Supplier { get; set; }
    }
}