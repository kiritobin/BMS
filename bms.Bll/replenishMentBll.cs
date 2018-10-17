using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.Model;
using bms.Dao;
using System.Data;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class replenishMentBll
    {
        replenishMentDao dao = new replenishMentDao();
        /// <summary>
        /// 获取补货单体信息
        /// </summary>
        /// <returns></returns>
        public DataSet Select()
        {
            DataSet ds = dao.Select();
            return ds;
        }
        /// <summary>
        /// 插入补货单体
        /// </summary>
        /// <param name="rm"></param>
        /// <returns></returns>
        public Result Insert(replenishMentMonomer rm)
        {
            int row = dao.Insert(rm);
            if (row > 0)
            {
                return Result.添加成功;
            }
            else
            {
                return Result.添加失败;
            }
        }
    }
}
