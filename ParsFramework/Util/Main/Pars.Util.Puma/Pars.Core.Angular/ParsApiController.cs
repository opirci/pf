using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pars.Core.Angular
{
    public class ParsApiController : ApiController
    {
        //[HttpGet]
        //public HttpResponseMessage GetDataInExcel(string htmlTable, string fileName )
        //{
        //    //ExcelRichTextHtmlUtility.SetRichTextFromHtml
        //    return ToExcel(htmlTable, fileName);            
        //}

        [HttpGet]
        public string GetVersion()
        {
            return "1.0.0";
        }

        [HttpPost]
        public string AsExcel(string[][] data, string worksheetName = null)
        {
            DataTable dt = new DataTable();
            int colCount = 0;
            foreach (var col in data[0])
            {
                dt.Columns.Add(col);
                colCount++;
            }
            bool first = true;
            int rowCount = 0;
            foreach (var row in data)
            {
                rowCount++;
                if (row.Length != colCount)
                {
                    throw new ArgumentOutOfRangeException($"Row number {rowCount} has incompatible number of values");
                }
                if (first)
                {
                    first = false;
                    continue;
                }
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.Length; i++)
                {
                    dr[dt.Columns[i].ColumnName] = row[i];
                }
                dt.Rows.Add(dr);
            }
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(worksheetName ?? "Sheet1");
                ws.Cells["A1"].LoadFromDataTable(dt, true);
                
                var res = Convert.ToBase64String( pck.GetAsByteArray());

                return res;
            }
            //ExcelRichTextHtmlUtility.SetRichTextFromHtml

        }

        [HttpPost]
        public HttpResponseMessage GetExcel(string[][] data, string fileName, string worksheetName = null)
        {
            DataTable dt = new DataTable();
            foreach (var col in data[0])
            {
                dt.Columns.Add(col);
            }
            bool first = true;
            foreach (var row in data)
            {
                if (first)
                {
                    first = false;
                    continue;
                }
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.Length; i++)
                {
                    dr[dt.Columns[i].ColumnName] = row[i];
                }
                dt.Rows.Add(dr);
            }
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(worksheetName ?? "Sheet1");
                ws.Cells["A1"].LoadFromDataTable(dt, true);
                return ToExcel(pck.Stream, fileName);
            }
            //ExcelRichTextHtmlUtility.SetRichTextFromHtml

        }

        static HttpResponseMessage ToExcel(Stream stream, string fileName)
        {

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            //If you have Physical file Read the fileStream and use it.

            response.Content = new StreamContent(stream);

            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;//your file Name- text.xls
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
            //response.Content.Headers.ContentType  = new MediaTypeHeaderValue("application/octet-stream");
            response.Content.Headers.ContentLength = stream.Length;
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }

        static HttpResponseMessage ToExcel(string content, string fileName)
        {
            return ToExcel(StringToStream(content), fileName);
        }

        static Stream StringToStream(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
