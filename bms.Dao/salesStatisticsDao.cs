using bms.DBHelper;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class salesStatisticsDao
    {
        MySqlHelp db = new MySqlHelp();
       //导出当前查询
        public DataSet exportTotal()
        {
            String cmdText = "";
            string[] param = { };
            object[] values = { };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        //导出所有明细
        public DataSet exportAllDetails()
        {
            String cmdText = "";
            string[] param = { };
            object[] values = { };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        //导出单个明细
        public DataSet exportDetails()
        {
            String cmdText = "";
            string[] param = { };
            object[] values = { };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
    }
}
