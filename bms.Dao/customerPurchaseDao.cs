using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using bms.DBHelper;

namespace bms.Dao
{
    public class customerPurchaseDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取汇总数据
        /// </summary>
        /// <param name="cusId">客户ID</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        public DataSet getSummary(int cusId,string search)
        {
            string sql = @"select count(bookNum) as kindsNum,sum(allTotalPrice) as atp,sum(allRealPrice) as arp,sum(allNum) as alln from((select ISBN,bookNum,bookName,unitPrice,sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice,sum(number) as allNum,realDiscount,dateTime,customerId from v_salemonomer where " + search+ " GROUP BY bookNum) as temp)";
            string[] param = { "@cusId" };
            object[] value = { cusId };
            DataSet ds = db.FillDataSet(sql, param, value);
            return ds;
        }
    }
}
