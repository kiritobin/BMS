using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using bms.Dao;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class ConfigurationBll
    {
        ConfigurationDao dao = new ConfigurationDao();
        public Result Insert(DateTime startTime,DateTime endTime,string regionName)
        {
            int row = dao.Insert(startTime, endTime, regionName);
            if (row > 0)
            {
                return Result.添加成功;
            }
            else
            {
                return Result.添加失败;
            }
        }
        public Result Update(DateTime startTime, DateTime endTime, string regionName)
        {
            int row = dao.Update(startTime, endTime, regionName);
            if (row > 0)
            {
                return Result.更新成功;
            }
            else
            {
                return Result.更新失败;
            }
        }
        public Result isExist(string regionName)
        {
            int count = dao.isExist(regionName);
            if (count > 0)
            {
                return Result.记录存在;
            }
            else
            {
                return Result.记录不存在;
            }
        }
        public DataSet getDateTime(string regionName)
        {
            DataSet ds = dao.getDateTime(regionName);
            return ds;
        }
    }
}
