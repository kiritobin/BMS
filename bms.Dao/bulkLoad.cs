using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class bulkLoad
    {
        //datatable导入mysql
        public int BulkInsert(DataTable table)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;
            if (string.IsNullOrEmpty(table.TableName)) throw new Exception("请给DataTable的TableName属性附上表名称");
            if (table.Rows.Count == 0) return 0;
            int insertCount = 0;
            string tmpPath = Path.GetTempFileName();
            string csv = DataTableToCsv(table);
            File.WriteAllText(tmpPath, csv);
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlTransaction tran = null;
            try
            {
                conn.Open();
                tran = conn.BeginTransaction();
                MySqlBulkLoader bulk = new MySqlBulkLoader(conn)
                {
                    FieldTerminator = ",",
                    FieldQuotationCharacter = '"',
                    EscapeCharacter = '"',
                    LineTerminator = "\r\n",
                    FileName = tmpPath,
                    NumberOfLinesToSkip = 0,
                    TableName = table.TableName,
                };
                insertCount = bulk.Load();
                tran.Commit();
            }
            catch (SqlException ex)
            {
                if (tran != null) tran.Rollback();
                throw ex;
            }
            conn.Close();
            File.Delete(tmpPath);
            return insertCount;
        }
        //datatable转csv方法
        private string DataTableToCsv(DataTable table)
        {
            DataColumn colum;
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    colum = table.Columns[i];
                    if (i != 0) sb.Append(",");
                    if (colum.DataType == typeof(string) && row[colum].ToString().Contains(","))
                    {
                        sb.Append("\"" + row[colum].ToString().Replace("\"", "\"\"") + "\"");
                    }
                    else sb.Append(row[colum].ToString());
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
