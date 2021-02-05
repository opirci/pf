using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Serialization;
using System.Data;
using System.ComponentModel;
using Pars.Util.Puma.UI.Mvc.SupplierSvc;

namespace Pars.Util.Puma.UI.Mvc.api
{
    public class TestController : ApiControllerBase
    {
        [HttpGet]
        public SupplierSvc.DataTable GetSupplierReportAsDataTable([FromUri]GetSupplierReportRequest request)
        {
            var idp = new Pars.Core.Security.FederationIdentityProvider();
            if (!idp.IsInClaim("Vrp_Tedport_Admin"))
            {
                //GetSupplierReportResponse resp = new GetSupplierReportResponse();
                //resp.errorMessages = new[] { "Tedport admin privilages needed to execute this service!" };
                //return resp;
            }
            GetSupplierReportResponse res = ProxyOf<ISupplierService>().GetSupplierReport(request);
            SupplierSvc.DataTable proxy = ToProxy(ToDataTable(res.values));
            proxy.totalCount = res.count;
            proxy.pageNumber = 1;
            return proxy;
        }

        [HttpGet]
        public SupplierSvc.DataTable GetDataTable()
        {
            SupplierSvc.DataTable dt = new SupplierSvc.DataTable();
            List<Row> rows = new List<Row>();
            rows.Add(new Row { isUpdated = false, values = new object[] { "ali", "veli", "selami" } });
            dt.rows = rows.ToArray();
            return dt;
        }

        [HttpPut]
        public void SaveDataTable([FromBody]SupplierSvc.DataTable dt)
        {

        }
        [HttpGet]
        public TestResponse TestIt()
        {
            TestResponse res = new TestResponse();
            res.Name = "Hi from TestIt";
            res.errorMessages = new string[] { "Error Message One", "Error Message Two" };
            res.warningMessages = new string[] { "Warning One", "Warning Two" };
            System.Threading.Thread.Sleep(3000);
            return res;
        }

        static System.Data.DataTable ToDataTable<T>(IEnumerable<T> data)
        {
            PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(T));
            System.Data.DataTable table = new System.Data.DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        static Type FromName(string name)
        {
            return Type.GetType(name);
        }

        static SupplierSvc.DataTable ToProxy(System.Data.DataTable dataTable)
        {
            SupplierSvc.DataTable prx = new SupplierSvc.DataTable();
            List<Column> columns = new List<Column>();
            List<Row> rows = new List<Row>();

            foreach (DataColumn col in dataTable.Columns)
            {
                if (col.DataType == typeof(System.Runtime.Serialization.ExtensionDataObject))
                    continue;

                columns.Add(new Column { name = col.ColumnName, type = col.DataType.FullName });
            }

            prx.columns = columns.ToArray();

            Row row = null;
            List<object> vals = null;
            foreach (DataRow drow in dataTable.Rows)
            {
                row = new Row();
                vals = new List<object>();
                foreach (Column col in columns)
                {
                    vals.Add(ClearDbNull(drow[col.name]));
                }
                row.values = vals.ToArray();
                rows.Add(row);
            }
            prx.rows = rows.ToArray();

            return prx;
        }

        static object ClearDbNull(object value) => value == DBNull.Value ? null : value;


    }

    [DataContract]
    public class TestResponse : ResponseBase
    {
        [DataMember]
        public string Name { get; set; }
    }
}