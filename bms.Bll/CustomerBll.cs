using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.Dao;
using System.Data;
using bms.Model;

namespace bms.Bll
{
    public class CustomerBll
    {
        CustomerDao customerDao = new CustomerDao();
        /// <summary>
        /// 获取所有客户信息
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet select()
        {
            return customerDao.select();
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="tablebuilder"></param>
        /// <param name="totalCount"></param>
        /// <param name="intPageCount"></param>
        /// <returns></returns>
        public DataSet selectByPage(TableBuilder tablebuilder, out int totalCount, out int intPageCount)
        {
            PublicProcedure procedure = new PublicProcedure();
            DataSet ds = procedure.SelectBypage(tablebuilder, out totalCount, out intPageCount);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
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
