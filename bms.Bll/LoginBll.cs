using bms.Dao;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    public class LoginBll
    {
        LoginDao loginDao = new LoginDao();

        /// <summary>
        /// 获取到用户账号、密码、角色类型
        /// </summary>
        /// <param name="userID">用户账号</param>
        /// <returns></returns>
        public User getPwdByUserId(string userID)
        {
            return loginDao.getPwdByUserId(userID);
        }
        /// <summary>
        /// 获取到客户账号、密码
        /// </summary>
        /// <param name="customerID">客户账号</param>
        /// <returns></returns>
        public Customer getPwdByCustomId(string customerID)
        {
            return loginDao.getPwdByCustomId(customerID);
        }
    }
}
