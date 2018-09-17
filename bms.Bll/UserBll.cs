using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using bms.Dao;
using bms.Model;

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

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="regionId"></param>
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
