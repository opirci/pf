using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pars.Core;
using Pars.Core.Collection;
using Pars.Core.Security;
using Pars.Util.Puma.Domain;

namespace Pars.Util.Puma.Data.Repository
{
    public class DataLocalizerRepository : IDataLocalizerRepository
    {
        private readonly IPumaUnitOfWork _uow;

        public DataLocalizerRepository(IPumaUnitOfWork unitOfWork)
        {
            _uow = unitOfWork;
        }

        public DataLocalizerRepository() : this(Pars.Core.Container.Instance.Resolve<IPumaUnitOfWork>())
        {

        }

        public DataLocalization GetLocalesByTable(DataLocalization dataLocalization, List<int> languages)
        {
            // SQL query generation:

            var schemaTableName = dataLocalization.TableName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            var schemaName = schemaTableName[0];
            var tableName = $"{schemaTableName[1]}_Locale";
            var columnName = schemaTableName[1].Replace("tb_", String.Empty);
            var databaseName = dataLocalization.DatabaseName;
            var serverName = dataLocalization.ServerName;

            var filteredColumns = new List<string>() { dataLocalization.ColumnName };
            filteredColumns = filteredColumns.Distinct().ToList();
            filteredColumns.Insert(0, "T.LanguageRef");
            filteredColumns.Insert(1, $"T.{columnName}Ref");

            var columnsText = String.Join(",", filteredColumns);
            var languageCodes = String.Join(",", languages);

            var destination = $"[{databaseName}].[{schemaName}].[{tableName}]";
            var destionationMaster = $"[{databaseName}].[{schemaName}].[{schemaTableName[1]}]";

            var sql =
                $@"SELECT {columnsText} FROM {destination} AS T INNER JOIN {destionationMaster} AS M ON T.{columnName}Ref = M.{columnName}Ref WHERE T.LanguageRef IN ({languageCodes}) AND M.IsDeleted=0 AND T.IsDeleted=0";

            //DataTable initialization:

            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn()
            {
                ColumnName = "ObjectRef",
                DataType = typeof(int)
            };
            dt.Columns.Add(dc);
            dt.PrimaryKey = new DataColumn[] { dt.Columns["ObjectRef"] };

            languages.ForEach(language =>
            {
                string languageName = Enum.GetName(typeof(LanguageEnum), language);
                dc = new DataColumn(languageName, typeof(string));
                dc.DefaultValue = "";
                dt.Columns.Add(dc);
            });

            //Fetching data:

            var connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = serverName;
            connStringBuilder.InitialCatalog = databaseName;
            connStringBuilder.UserID = ConfigurationManager.AppSettings["uid"];
            connStringBuilder.Password = ConfigurationManager.AppSettings["pwd"];

            using (var conn = new SqlConnection(connStringBuilder.ConnectionString))
            {
                var cmd = new SqlCommand(sql, conn);
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                    return dataLocalization;

                while (reader.Read())
                {
                    var languageRef = reader.GetInt32(0);
                    string language = Enum.GetName(typeof(LanguageEnum), languageRef);

                    if (string.IsNullOrEmpty(language))
                        continue;

                    var objRef = reader.GetInt32(1);
                    var value = (reader[2] == null || reader[2] == DBNull.Value) ? null : reader.GetString(2);

                    DataRow row = dt.Rows.Find(objRef);

                    if (row == null)
                    {
                        row = dt.NewRow();
                        row["ObjectRef"] = objRef;

                        row[language] = value;

                        dt.Rows.Add(row);
                    }
                    else
                    {
                        row[language] = value;
                    }
                }

                dataLocalization.LocaleTable = dt.ToProxy();
            }

            return dataLocalization;
        }

        public DataLocalization GetLocalesByRow(string guid, int objectRef, List<int> languages)
        {
            var itemGuid = Guid.Parse(guid);
            var dataLocalization = (from tc in _uow.TableColumn.Table
                                    join t in _uow.Table.Table on tc.TableRef equals t.TableRef
                                    join d in _uow.Database.Table on t.DatabaseRef equals d.DatabaseRef
                                    join s in _uow.Server.Table on d.ServerRef equals s.ServerRef
                                    where tc.TableColumnGuid == itemGuid
                                    select new DataLocalization()
                                    {
                                        ServerName = s.ServerName,
                                        DatabaseName = d.DatabaseName,
                                        TableName = t.TableName,
                                        ColumnName = tc.TableColumnName
                                    }).FirstOrDefault();

            // SQL query generation:

            var schemaTableName = dataLocalization.TableName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            var schemaName = schemaTableName[0];
            var tableName = $"{schemaTableName[1]}_Locale";
            var columnName = schemaTableName[1].Replace("tb_", String.Empty);
            var databaseName = dataLocalization.DatabaseName;
            var serverName = dataLocalization.ServerName;

            var filteredColumns = new List<string>() { dataLocalization.ColumnName };

            filteredColumns = filteredColumns.Distinct().ToList();
            filteredColumns.Insert(0, "T.LanguageRef");
            filteredColumns.Insert(1, $"T.{columnName}Ref");
            var columnsText = String.Join(",", filteredColumns);

            var languageCodes = String.Join(",", languages);

            var destination = $"[{databaseName}].[{schemaName}].[{tableName}]";
            var destionationMaster = $"[{databaseName}].[{schemaName}].[{schemaTableName[1]}]";

            string sql = String.Format(@"SELECT {0} FROM {1} AS T INNER JOIN {4} AS M ON T.{3}Ref = M.{3}Ref WHERE T.{3}Ref=@Ref AND T.LanguageRef IN ({2}) AND M.IsDeleted=0 AND T.IsDeleted=0", columnsText, destination, languageCodes, columnName, destionationMaster);
            //string sql = String.Format(@"SELECT {0} FROM {1} WHERE LanguageRef IN ({2})", columnsText, destination, languageCodes);

            //DataTable initialization:

            var dt = new DataTable();

            var dcLanguage = new DataColumn()
            {
                ColumnName = "Language",
                DataType = typeof(string)
            };

            var dcObjectRef = new DataColumn()
            {
                ColumnName = "ObjectRef",
                DataType = typeof(int)
            };

            var dcValue = new DataColumn()
            {
                ColumnName = "Value",
                DataType = typeof(string)
            };

            dt.Columns.Add(dcLanguage);
            dt.Columns.Add(dcObjectRef);
            dt.Columns.Add(dcValue);
            dt.PrimaryKey = new DataColumn[] { dcLanguage };

            var connStringBuilder = new SqlConnectionStringBuilder();
            connStringBuilder.DataSource = serverName;
            connStringBuilder.InitialCatalog = databaseName;
            connStringBuilder.UserID = ConfigurationManager.AppSettings["uid"];
            connStringBuilder.Password = ConfigurationManager.AppSettings["pwd"];

            using (var conn = new SqlConnection(connStringBuilder.ConnectionString/*this._dbContext.Database.Connection.ConnectionString*/))
            {
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Ref", objectRef);
                conn.Open();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int languageRef = reader.GetInt32(0);
                    LanguageEnum language = (LanguageEnum)languageRef;

                    string value = reader[dataLocalization.ColumnName] != DBNull.Value
                        ? reader[dataLocalization.ColumnName].ToString()
                        : null;

                    DataRow newRow = dt.NewRow();
                    newRow["Language"] = language;
                    newRow["ObjectRef"] = objectRef;
                    newRow["Value"] = value;

                    dt.Rows.Add(newRow);
                }
            }

            foreach (int x in languages)
            {
                if (Enum.GetName(typeof(LanguageEnum), x) == null)
                    continue;

                LanguageEnum language = (LanguageEnum)x;
                if (dt.Rows.Find(language) == null)
                {
                    DataRow newRow = dt.NewRow();
                    newRow["Language"] = language;
                    newRow["ObjectRef"] = objectRef;
                    newRow["Value"] = null;

                    dt.Rows.Add(newRow);
                }
            }

            dataLocalization.LocaleTable = dt.ToProxy();

            return dataLocalization;
        }

        public bool SaveLocales(DataLocalization dataLocalization)
        {
            var idp = Container.Instance.Resolve<IIdentityProvider>();

            DataTable localesTable = dataLocalization.LocaleTable.ToDataTable();
            bool result = true;
            //string date = DateTime.Now.ToString();

            foreach (DataRow row in localesTable.Rows)
            {
                var values = new List<string>();
                var columns = new List<string>();
                var filteredColumns = new List<string>();
                int? languageRef = null;

                foreach (DataColumn dataColumn in localesTable.Columns)
                {
                    values.Clear();
                    columns.Clear();
                    filteredColumns.Clear();

                    if (dataColumn.ColumnName.Equals("ObjectRef") || dataColumn.ColumnName.Equals("Language"))
                        continue;

                    var value = row[dataColumn].ToString();
                    var column = dataLocalization.ColumnName;

                    if (!filteredColumns.Contains(value))
                        filteredColumns.Add($"'{value.Replace("'", "''")}'");

                    if (!columns.Contains(column))
                        columns.Add(column);

                    values.Add($"[{column}] = '{value.Replace("'", "''")}'");

                    var valuesText = String.Join(",", filteredColumns);
                    var updateText = String.Join(",", values);
                    updateText += $", ModifiedUserRef = {idp.UserRef}, ModifiedDate = GETDATE()";

                    var schemaTableName = dataLocalization.TableName.Split(new char[] { '.' },
                        StringSplitOptions.RemoveEmptyEntries);
                    var schemaName = schemaTableName[0];
                    var tableName = $"{schemaTableName[1]}_Locale";
                    var columnName = schemaTableName[1].Replace("tb_", String.Empty);
                    var databaseName = dataLocalization.DatabaseName;
                    var serverName = dataLocalization.ServerName;
                    int objectRef = Convert.ToInt32(row["ObjectRef"]);
                    string languageName = dataColumn.ColumnName == "Value"
                        ? row["Language"].ToString()
                        : dataColumn.ColumnName;
                    languageRef = (int)((LanguageEnum)Enum.Parse(typeof(LanguageEnum), languageName));

                    columns.AddRange(new string[]
                    {
                        $"{columnName}Ref", "LanguageRef", "CreatedUserRef", "CreatedDate", "ModifiedUserRef",
                        "ModifiedDate", "SessionRef", "RowVersion", "IsDeleted"
                    });
                    var columnsText = String.Join(",", columns);

                    var destination = $"[{databaseName}].[{schemaName}].[{tableName}]";

                    string sql =
                        $@"IF EXISTS(SELECT * FROM {destination} WHERE {columnName}Ref = @Ref AND LanguageRef = @LanguageRef) 
                               BEGIN
                                    UPDATE {destination} SET {updateText} WHERE {columnName}Ref=@Ref AND LanguageRef = @LanguageRef
                               END
                           ELSE 
                               BEGIN 
                                    INSERT INTO {destination} ({columnsText}) VALUES({valuesText}, {objectRef}, {languageRef}, {idp.UserRef}, (SELECT GETDATE()), {idp.UserRef}, (SELECT GETDATE()), '{idp.SessionRef}', NULL, 0)
                               END";


                    var connStringBuilder = new SqlConnectionStringBuilder
                    {
                        DataSource = serverName,
                        InitialCatalog = databaseName,
                        UserID = ConfigurationManager.AppSettings["uid"],
                        Password = ConfigurationManager.AppSettings["pwd"]
                    };

                    using (var conn = new SqlConnection(connStringBuilder.ConnectionString))
                    {
                        var cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Ref", objectRef);
                        cmd.Parameters.AddWithValue("@LanguageRef", languageRef);
                        //cmd.Parameters.AddWithValue("@V", valuesText == "''" ? DBNull.Value.ToString() : valuesText);
                        conn.Open();

                        if (result)
                            result = cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            return result;
        }
    }
}
