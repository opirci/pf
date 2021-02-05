using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Pars.Core.Data
{
    public static class Extensions
    {

        public static void SaveExcel(this DataTable dataTable, string path, string worksheetName = null)
        {
            using (ExcelPackage pck = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add(worksheetName ?? "Sheet1");
                ws.Cells["A1"].LoadFromDataTable(dataTable, true);
                pck.Save();
            }
        }

        public static void LoadExcel(this DataTable dataTable, string path)
        {
            using (var package = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    package.Load(stream);
                }
                var worksheet = package.Workbook.Worksheets.First();
                bool hasHeader = true;
                foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    dataTable.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (var rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                {
                    var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                    var row = dataTable.NewRow();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                    dataTable.Rows.Add(row);
                }
            }
        }

    }

    /*
    public static class Extensions
    {
      
        public static void LoadExcel(this DataTable dataTable, string path)
        {
            var fileName = path;

            EXCEL.Application excelApp = new EXCEL.Application();
            EXCEL.Workbook newWorkbook = excelApp.Workbooks.Add();
            EXCEL.Workbook excelWorkbook = excelApp.Workbooks.Open(fileName,
                    0, true, 5, "", "", true, EXCEL.XlPlatform.xlWindows, "",
                    false, false, 0, true, false, false);

            EXCEL.Sheets excelSheets = excelWorkbook.Worksheets;
            EXCEL.Worksheet excelWorksheet = excelSheets.get_Item(1);
            EXCEL.Range lastCell = excelWorksheet.Cells.SpecialCells(EXCEL.XlCellType.xlCellTypeLastCell, Type.Missing);
            EXCEL.Range excelCell = excelWorksheet.get_Range("A1", lastCell);
            System.Array myvalues = excelCell.Cells.Value2;

            int vertical = myvalues.GetLength(0);
            int horizontal = myvalues.GetLength(1);
            string nullOlanSutunBasliklariStr = "";
            object[,] myVals = myvalues as object[,];

            for (int columnindex = 1; columnindex <= horizontal; columnindex++)
            {
                if (myVals[1, columnindex] == null)
                    nullOlanSutunBasliklariStr += columnindex.ToString() + ",";
            }
            if (nullOlanSutunBasliklariStr != "")
                nullOlanSutunBasliklariStr = nullOlanSutunBasliklariStr.Substring(0, nullOlanSutunBasliklariStr.Length - 1);
            string[] nullOlanSutunBasliklari = nullOlanSutunBasliklariStr.Split(',');

            using (DataTable dtExcel = new DataTable())
            {
             
                for (int columnindex = 1; columnindex <= horizontal; columnindex++)
                {
                    if (nullOlanSutunBasliklari.Contains(columnindex.ToString()))
                        continue;
                    dtExcel.Columns.Add(myVals[1, columnindex].ToString());
                }

                for (int rowindex = 2; rowindex <= vertical; rowindex++)
                {
                    List<object> objectNumbers = new List<object>();
                    for (int columnindex = 1; columnindex <= horizontal; columnindex++)
                    {
                        object myVal = myVals[rowindex, columnindex];

                        if (nullOlanSutunBasliklari.Contains(columnindex.ToString()))
                            continue;
                        if (rowindex == 2)
                        {
                            if (myVal == null)
                                dtExcel.Columns[columnindex - 1].DataType = System.Type.GetType("System.String");
                            else
                                dtExcel.Columns[columnindex - 1].DataType = myVal.GetType();
                        }

                        objectNumbers.Add(null);

                    }
                    dtExcel.Rows.Add(objectNumbers.ToArray());
                }
                foreach (DataRow row in dtExcel.Rows)
                {
                    dataTable.ImportRow(row);
                }
            }
        }
    }
      */
}


