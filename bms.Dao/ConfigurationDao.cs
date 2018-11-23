using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.DBHelper;
using System.Data;

namespace bms.Dao
{
    public class ConfigurationDao
    {
        MySqlHelp db = new MySqlHelp();
        public DataSet select()
        {
            string cmdText = "select * from t_configuration";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            return ds;
        }
        public int Insert(DateTime startTime, DateTime endTime, string regionName)
        {
            //string cmdText;
            //if (regionName != "" || regionName != null)
            //{
            string cmdText = "insert into t_configuration(startTime,endTime,regionName) value(@startTime,@endTime,@regionName)";
            string[] param = { "@startTime", "@endTime", "@regionName" };
            object[] values = { startTime, endTime, regionName };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
            //}
            //else
            //{
            //    cmdText = "insert into t_configuration(startTime,endTime) value(@startTime,@endTime)";
            //    string[] param = { "@startTime", "@endTime" };
            //    object[] values = { startTime, endTime };
            //    int row = db.ExecuteNoneQuery(cmdText, param, values);
            //    return row;
            //}
        }
        public int Update(DateTime startTime, DateTime endTime, string regionName)
        {
            string cmdText = "update t_configuration set startTime=@startTime,endTime=@endTime where regionName=@regionName";
            string[] param = { "@startTime", "@endTime", "@regionName" };
            object[] values = { startTime, endTime, regionName };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        public int isExist(string regionName)
        {
            string cmdText = "select count(regionName) from t_configuration where regionName=@regionName";
            string[] param = { "@regionName" };
            object[] values = { regionName };
            int count = int.Parse(db.ExecuteScalar(cmdText, param, values).ToString());
            return count;
        }
    }
}
