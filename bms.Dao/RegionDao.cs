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
    }
}
