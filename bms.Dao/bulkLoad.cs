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
        public int BulkInsert(DataTable table)
        {
            //string connectionString = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;
            string connectionString = "server=localhost;database=bms;uid=bms;pwd=bms123;Convert Zero Datetime=True;Allow Zero Datetime=True;CharSet=utf8";
            if (string.IsNullOrEmpty(table.TableName)) throw new Exception("请给DataTable的TableName属性附上表名称");
            if (table.Rows.Count == 0) return 0;
            int insertCount = 0;
            string tmpPath = Path.GetTempFileName();
            string csv = DataTableToCsv(table);
            File.WriteAllText(tmpPath, csv);
            MySqlTransaction tran = null;
            MySqlConnection conn = new MySqlConnection(connectionString);
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
            catch (MySqlException ex)
            {
                if (tran != null) tran.Rollback();
                throw ex;
            }
            finally
            {
                conn.Dispose();
                conn.Close();
            }
            File.Delete(tmpPath);
            return insertCount;
        }
        private static string DataTableToCsv(DataTable table)
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
