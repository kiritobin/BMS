﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
            string cmdText = "select * from V_Customer";
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
            string sql = "update T_Customer set customerID=@customerID,customerName=@customerName,customerPwd=@customerPwd,regionId=@regionId";
            string[] param = { "@customerID", "@customerName", "@customerPwd", "@regionId" };
            object[] values = { customer.CustomerId, customer.CustomerName, customer.CustomerPwd, customer.RegionId.RegionId };
            return db.ExecuteNoneQuery(sql, param, values);
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
    }
}
