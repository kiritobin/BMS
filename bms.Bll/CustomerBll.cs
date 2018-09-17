using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.Dao;
using System.Data;

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
    }
}
