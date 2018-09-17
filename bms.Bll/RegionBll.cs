using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using bms.Dao;
using bms.Model;

namespace bms.Bll
{
    public class RegionBll
    {
        RegionDao regionDao = new RegionDao();
        /// <summary>
        /// 获取所有地区信息
        /// </summary>
        /// <returns></returns>
        public DataSet select()
        {
            return regionDao.select();
        }
    }
}
