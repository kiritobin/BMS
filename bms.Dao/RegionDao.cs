using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.DBHelper;
using System.Data;

namespace bms.Dao
{
    public class RegionDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取所有地区信息
        /// </summary>
        /// <returns></returns>
        public DataSet select()
        {
            string cmdText = "select * from T_Region";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            if(ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }


        public int insert(string regionName)
        {
            string cmdText = "insert into T_Rigion(regionName) values(@regionName)";
            string[] param = { "@regionName" };
            object[] values = { regionName };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
    }
}
