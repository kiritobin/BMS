using bms.DBHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    public class Common
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns></returns>
        public String getDate()
        {
            string cmdText = "select now()";
            String[] param = null;
            object[] values = null;
            return db.ExecuteScalar(cmdText, param, values).ToString();
        }
    }
}
