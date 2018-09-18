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
        /// 获取分页信息
        /// </summary>
        /// <param name="tablebuilder">分页方法</param>
        /// <param name="totalCount">返回的总记录数</param>
        /// <param name="intPageCount">总页数</param>
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
        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="customer">客户实体</param>
        /// <returns>返回最终结果（成功或失败）</returns>
        public Enums.OpResult Insert(Customer customer)
        {
            int count = customerDao.Insert(customer);
            if (count > 0)
            {
                return Enums.OpResult.添加成功;
            }
            else
            {
                return Enums.OpResult.添加失败;
            }
        }
        public bool SelectById(string customerId)
        {
            int count = customerDao.SelectById(customerId);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
