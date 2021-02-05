using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    [DataContract]
    public class DataLocalization
    {
        [DataMember(Name = "serverName")]
        public string ServerName { get; set; }
        [DataMember(Name = "databaseName")]
        public string DatabaseName { get; set; }
        [DataMember(Name = "tableName")]
        public string TableName { get; set; }
        [DataMember(Name = "columnName")]
        public string ColumnName { get; set; }
        [DataMember(Name = "localeTable")]
        public DataTableProxy LocaleTable { get; set; }
    }
}
