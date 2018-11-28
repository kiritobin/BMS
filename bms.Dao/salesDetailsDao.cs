using bms.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class SalesDetailsDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取采集人
        /// </summary>
        /// <param name="strWhere">筛选条件</param>
        /// <returns></returns>
        public DataSet getUser(string strWhere)
        {
            String cmdText = "select userName from v_salemonomer where "+ strWhere + " group by userName";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 导出成Excel表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="type">分组条件</param>
        /// <returns>返回一个DataTable的选题记录集合</returns>
        public DataTable ExportExcel(string strWhere,string type)
        {
            String cmdText = "select ISBN,bookNum as 书号,bookName as 书名,price as 单价,sum(number) as 数量, sum(totalPrice) as 码洋,sum(realPrice) as 实洋,realDiscount as 销售折扣,dateTime as 采集时间,userName as 采集人,state as 采集状态,supplier as 供应商 from v_salemonomer where " + strWhere+" group by bookNum,"+ type;
            DataSet ds = db.FillDataSet(cmdText, null, null);
            DataTable dt = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}
