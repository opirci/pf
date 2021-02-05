using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;

namespace Pars.Util.Puma.Domain
{
    [DataContract(Name = "DataTable")]
    public class DataTableProxy
    {
        [DataMember(Name = "columns")]
        public Column[] Columns { get; set; }

        [DataMember(Name = "rows")]
        public Row[] Rows { get; set; }

        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }

        [DataMember(Name = "pageNumber")]
        public int PageNumber { get; set; }

        public DataTableProxy()
        {
            TotalCount = Rows?.Length ?? 0;
            PageNumber = 1;
        }
    }

    [DataContract]
    public class Row
    {
        [DataMember(Name = "isUpdated")]
        public bool IsUpdated { get; set; }

        [DataMember(Name = "values")]
        public object[] Values { get; set; }
    }

    [DataContract]
    public class Column
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}