using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pars.Util.Puma.Domain
{
    public static class Extensions
    {
        public static void SetProperty<T1>(this IDataChanged item, ref T1 property, T1 value)
        {
            if (value == null || value.Equals(property))
                return;

            property = value;
            item.HasChanged = true;
        }

        static Type FromName(string name)
        {
            return Type.GetType(name);
        }

        public static DataTable ToDataTable(this DataTableProxy dataTableProxy)
        {
            DataTable dt = new DataTable();
            foreach (Column col in dataTableProxy.Columns)
            {
                dt.Columns.Add(col.Name, FromName(col.Type));
            }

            foreach (Row col in dataTableProxy.Rows)
            {
                DataRow rw = dt.NewRow();
                for (int i = 0; i < col.Values.Length; i++)
                {
                    rw[i] = col.Values[i];
                }
                dt.Rows.Add(rw);
            }
            return dt;
        }

        public static DataTableProxy ToProxy(this DataTable dataTable)
        {
            DataTableProxy prx = new DataTableProxy();
            List<Column> columns = new List<Column>();
            List<Row> rows = new List<Row>();

            foreach (DataColumn col in dataTable.Columns)
            {
                if (col.DataType == typeof(System.Runtime.Serialization.ExtensionDataObject))
                    continue;

                columns.Add(new Column { Name = col.ColumnName, Type = col.DataType.FullName });
            }

            prx.Columns = columns.ToArray();

            Row row = null;
            List<object> vals = null;
            foreach (DataRow drow in dataTable.Rows)
            {
                row = new Row();
                vals = new List<object>();
                foreach (Column col in columns)
                {
                    vals.Add(ClearDbNull(drow[col.Name]));
                }
                row.Values = vals.ToArray();
                rows.Add(row);
            }
            prx.Rows = rows.ToArray();

            return prx;
        }

        static object ClearDbNull(object value) => value == DBNull.Value ? null : value;

        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
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
    }
}
