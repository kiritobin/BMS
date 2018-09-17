﻿using bms.DBHelper;
using bms.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class LoginDao
    {
        private MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 传入用户账号取到账号和密码
        /// </summary>
        /// <param name="userID">用户账号</param>
        /// <returns></returns>
        public User getPwdByUserId(string userID)
        {
            string sql = "select userID,userPwd,roleId from T_User where userID=@userID";
            string[] param = { "@userID" };
            object[] values = { userID };
            User user = new User();
            MySqlDataReader reader = db.ExecuteReader(sql, param, values);
            while (reader.Read())
            {
                user.UserId = reader.GetInt32(0);
                user.Pwd = reader.GetString(1);
                user.RoleId.RoleId = reader.GetInt32(2);
            }
            reader.Close();
            return user; ;
        }
        /// <summary>
        /// 传入客户账号取到账号和密码
        /// </summary>
        /// <param name="customerID">客户账号</param>
        /// <returns></returns>
        public Customer getPwdByCustomId(string customerID)
        {
            string sql = "select customerID,customerPwd from T_Customer where customerID=@customerID";
            string[] param = { "@customerID" };
            object[] values = { customerID };
            Customer custom = new Customer();
            MySqlDataReader reader = db.ExecuteReader(sql, param, values);
            while (reader.Read())
            {
                custom.CustomerId = reader.GetInt32(0);
                custom.CustomerPwd = reader.GetString(1);
            }
            reader.Close();
            return custom; ;
        }
    }
}
