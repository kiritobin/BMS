using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using bms.Dao;

namespace bms.Bll
{
    public class UserBll
    {
        UserDao userDao = new UserDao();
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet select()
        {
            return userDao.select();
        }

        /// <summary>
        /// 根据地区id获取用户信息
        /// </summary>
        /// <param name="regionId">地区id</param>
        /// <returns>数据集</returns>
        public DataSet selectByRegion(int regionId)
        {
            return userDao.selectByRegion(regionId);
        }
    }
}
