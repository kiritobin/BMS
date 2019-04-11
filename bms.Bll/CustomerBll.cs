using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.Dao;
using System.Data;
using bms.Model;

namespace bms.Bll
{
    using Result = Enums.OpResult;
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
        public Result Insert(Customer customer)
        {
            int count = customerDao.Insert(customer);
            if (count > 0)
            {
                return Result.添加成功;
            }
            else
            {
                return Result.添加失败;
            }
        }
        /// <summary>
        /// 查找账号是否存在
        /// </summary>
        /// <param name="customerId">账号</param>
        /// <returns></returns>
        public bool SelectById(string customerId,string customName)
        {
            int count = customerDao.SelectById(customerId, customName);
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="customer">客户实体</param>
        /// <returns></returns>
        public Result update(Customer customer)
        {
            int row = customerDao.update(customer);
            if (row > 0)
            {
                return Result.更新成功;
            }
            else
            {
                return Result.更新失败;
            }
        }
        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="customerID">客户Id</param>
        /// <returns></returns>
        public Result Delete(int customerID)
        {
            int row = customerDao.delete(customerID);
            if (row > 0)
            {
                return Result.删除成功;
            }
            else
            {
                return Result.删除失败;
            }
        }
        /// <summary>
        /// 根据账号获取客户对象
        /// </summary>
        /// <param name="customerID">账号</param>
        /// <returns></returns>
        public Customer getCustomer(int customerID)
        {
            DataSet ds = customerDao.getCustomer(customerID);
            Customer cust = new Customer();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int i = ds.Tables[0].Rows.Count - 1;
                cust.CustomerId = Convert.ToInt32(ds.Tables[0].Rows[i]["customerID"].ToString());
                cust.CustomerName = ds.Tables[0].Rows[i]["customerName"].ToString();
                return cust;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据客户名称获取客户实体
        /// </summary>
        /// <param name="customerName">客户姓名</param>
        /// <returns></returns>
        public Customer getCustomerBuName(string customerName)
        {
            DataSet ds = customerDao.getCustomerBuName(customerName);
            Customer cust = new Customer();
            if (ds != null)
            {
                int i = ds.Tables[0].Rows.Count - 1;
                cust.CustomerId = Convert.ToInt32(ds.Tables[0].Rows[0]["customerID"].ToString());
                cust.CustomerName = ds.Tables[0].Rows[0]["customerName"].ToString();
                return cust;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 判断在另外一张表中是否有数据
        /// </summary>
        ///<param name = "table" > 表名 </ param >
        /// <param name="primarykeyname">主键列</param>
        /// <param name="primarykey">主键参数</param>
        /// <returns>管理引用代表数据存在不可删除，记录不存在表示可以删除</returns>
        public Result IsDelete(string table, string primarykeyname, string primarykey)
        {
            PublicProcedure pp = new PublicProcedure();
            int row = pp.isDelete(table, primarykeyname, primarykey);
            if (row > 0)
            {
                return Result.关联引用;
            }
            else
            {
                return Result.记录不存在;
            }
        }

        /// <summary>
        /// 查询客户的删除状态
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Result DeleteState(int customerId)
        {
            DataSet ds = customerDao.DeleteState(customerId);
            int row = Convert.ToInt32(ds.Tables[0].Rows[0]["deleteState=1"]);
            if (row > 0)
            {
                return Result.记录不存在;
            }
            else
            {
                return Result.记录存在;
            }
        }
    }
}
