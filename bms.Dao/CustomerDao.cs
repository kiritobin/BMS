﻿using System;
using System.Data;
using bms.DBHelper;
using bms.Model;

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
            string cmdText = "select customerID,customerName,customerPwd,regionId from V_Customer";
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
        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="customer">客户实体</param>
        /// <returns></returns>
        public int Insert(Customer customer)
        {
            string cmdText = "insert into T_Customer(customerID,customerName,customerPwd,regionId) values(@customerID,@customerName,@customerPwd,@regionId)";
            String[] param = { "@customerID","@customerName", "@customerPwd", "@regionId" };
            object[] values = { customer.CustomerId.ToString(), customer.CustomerName ,customer.CustomerPwd,customer.RegionId.RegionId};
            return db.ExecuteNoneQuery(cmdText, param, values);
        }
        /// <summary>
        /// 查找账号是否存在
        /// </summary>
        /// <param name="customerId">账号</param>
        /// <returns>符合条件的记录条数</returns>
        public int SelectById(string customerId)
        {
            string cmdText = "select count(customerID) from T_Customer where customerID=@customerId";
            string[] param = { "@customerId" };
            string[] values = { customerId };
            return Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
        }
        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="customer">客户实体</param>
        /// <returns></returns>
        public int update(Customer customer)
        {
            string sql = "update T_Customer set customerName=@customerName,regionId=@regionId where customerID=@customerID";
            string[] param = { "@customerID", "@customerName", "@regionId" };
            object[] values = { customer.CustomerId.ToString(), customer.CustomerName, customer.RegionId.RegionId.ToString() };
            int row = db.ExecuteNoneQuery(sql, param, values);
            return row;
        }
        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="customerID">客户Id</param>
        /// <returns></returns>
        public int delete(int customerID)
        {
            string sql = "delete from T_Customer where customerID = @customerID";
            string[] param = { "@customerID" };
            object[] values = { customerID };
            return db.ExecuteNoneQuery(sql, param, values);
        }
        /// <summary>
        /// 根据账号获取客户实体
        /// </summary>
        /// <param name="customerID">账号</param>
        /// <returns></returns>
        public DataSet getCustomer(int customerID)
        {
            string cmdText = "select customerID,customerName,customerPwd,regionId from T_Customer where customerID=@customerID";
            String[] param = { "@customerID" };
            object[] values = { customerID };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }
        /// <summary>
        /// 重置客户密码
        /// </summary>
        /// <param name="customerID">账户</param>
        /// <param name="customerPwd">重置后的密码</param>
        /// <returns></returns>
        public int ResetPwd(int customerID,string customerPwd)
        {
            string sql = "update T_Customer set customerPwd=@customerPwd where customerID=@customerID";
            string[] param = { "@customerID", "@customerPwd" };
            object[] values = { customerID, customerPwd };
            return db.ExecuteNoneQuery(sql, param, values);
        }
    }
}
