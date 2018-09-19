using bms.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class LibraryCollectionDao
    {
        /// <summary>
        /// 获取所有客户馆藏数据的ISBN，单价，书名
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select ISBN,customerId from T_LibraryCollection";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                return dt;
            }
            else
            {
                return null;
            }
        }
    }
}
