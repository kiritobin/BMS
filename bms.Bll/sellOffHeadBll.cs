using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using bms.Dao;
using bms.Model;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class sellOffHeadBll
    {
        sellOffHeadDao dao = new sellOffHeadDao();
        /// <summary>
        /// 获取销退单头信息
        /// </summary>
        /// <returns></returns>
        public DataSet Select()
        {
            DataSet ds = dao.Select();
            return ds;
        }
        /// <summary>
        /// 添加销退单头
        /// </summary>
        /// <param name="sellHead">销退单头实体</param>
        /// <returns></returns>
        public Result Insert(SellOffHead sellHead)
        {
            int row = dao.Insert(sellHead);
            if(row > 0)
            {
                return Result.添加成功;
            }
            else
            {
                return Result.添加失败;
            }
        }
        /// <summary>
        /// 根据销售任务Id返回销退单头数量
        /// </summary>
        /// <param name="saleTaskId"></param>
        /// <returns></returns>
        public int getCount(string saleTaskId)
        {
            int row = dao.getCount(saleTaskId);
            return row;
        }
        /// <summary>
        /// 删除销退单头
        /// </summary>
        /// <param name="sellOffHeadId"></param>
        /// <returns></returns>
        public Result Delete(string sellOffHeadId)
        {
            int row = dao.Delete(sellOffHeadId);
            if (row > 0)
            {
                return Result.删除成功;
            }
            else
            {
                return Result.删除失败;
            }
        }
    }
}
