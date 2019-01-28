using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using bms.Dao;

namespace bms.Bll
{
    public class customerPurchaseBll
    {
        customerPurchaseDao dao = new customerPurchaseDao();
        /// <summary>
        /// 获取汇总数据
        /// </summary>
        /// <param name="cusId">客户ID</param>
        /// <param name="search">查询条件</param>
        /// <returns></returns>
        public DataSet getSummary(int cusId, string search)
        {
            DataSet ds = dao.getSummary(cusId, search);
            return ds;
        }
    }
}
