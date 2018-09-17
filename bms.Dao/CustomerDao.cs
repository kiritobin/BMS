using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using bms.DBHelper;

namespace bms.Dao
{
    public class CustomerDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取所有客户信息
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet select()
        {
            string cmdText = "select * from V_Customer";
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
