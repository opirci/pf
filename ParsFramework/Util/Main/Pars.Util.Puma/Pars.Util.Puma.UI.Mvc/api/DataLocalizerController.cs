using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Pars.Core.Data;
using Pars.Util.Puma.UI.Mvc.DataLocalizerSvc;
using Pars.Util.Puma.Domain;
using Column = Pars.Util.Puma.UI.Mvc.DataLocalizerSvc.Column;
using Row = Pars.Util.Puma.UI.Mvc.DataLocalizerSvc.Row;

namespace Pars.Util.Puma.UI.Mvc.api
{
    public class DataLocalizerController : ApiControllerBase
    {
        [HttpPut]
        public GetLocalesByTableResponse GetLocalesByTable([FromBody] GetLocalesByTableRequest request)
        {
            //var idp = new Pars.Core.Security.FederationIdentityProvider();
            //if (!idp.IsInClaim("Vrp_Tedport_Admin"))
            //{
            //    GetSupplierReportResponse resp = new GetSupplierReportResponse();
            //    resp.errorMessages = new[] { "Tedport admin privilages needed to execute this service!" };
            //    return resp;
            //}
            return ProxyOf<IDataLocalizerService>().GetLocalesByTable(request);
        }

        [HttpPut]
        public GetLocalesByRowResponse GetLocalesByRow([FromBody] GetLocalesByRowRequest request)
        {
            return ProxyOf<IDataLocalizerService>().GetLocalesByRow(request);
        }

        [HttpPut]
        public SaveLocalesResponse SaveLocales([FromBody] SaveLocalesRequest request)
        {
            return ProxyOf<IDataLocalizerService>().SaveLocales(request);
        }

        [HttpPost]
        public GetLocalesByTableResponse ImportTable()
        {
            var httpRequest = HttpContext.Current.Request;
            var dt = new System.Data.DataTable();

            if (httpRequest.Files.Count == 1)
            {

                var postedFile = httpRequest.Files[0];
                var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + $"{postedFile.FileName}.{Guid.NewGuid()}");
                postedFile.SaveAs(filePath);
                postedFile.InputStream.Dispose();

                dt.LoadExcel(filePath);
            }

            var response = new GetLocalesByTableResponse();
            response.value = new DataLocalizerSvc.DataLocalization();
            response.value.localeTable = ToProxy(dt);

            return response;
        }

        static DataLocalizerSvc.DataTable ToProxy(System.Data.DataTable dataTable)
        {
            DataLocalizerSvc.DataTable prx = new DataLocalizerSvc.DataTable();
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
}