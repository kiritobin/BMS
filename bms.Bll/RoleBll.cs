using bms.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    public class RoleBll
    {
        RoleDao roleDao = new RoleDao();
        /// <summary>
        /// 获取所有角色信息
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet select()
        {
            return roleDao.select();
        }
    }
}
