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
        public DataSet Select(string sellOffHeadID)
        {
            DataSet ds = dao.Select(sellOffHeadID);
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
        /// 根据销售任务Id返回时间数据集
        /// </summary>
        /// <param name="saleTaskId"></param>
        /// <returns></returns>
        public DataSet getMakeTime(string saleTaskId)
        {
            DataSet ds = dao.getMakeTime(saleTaskId);
            if (ds != null)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取该销退单头下所有的单据号和制单时间
        /// </summary>
        /// <returns></returns>
        public DataSet getAllTime()
        {
            DataSet ds = dao.getAllTime();
            return ds;
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
        /// <summary>
        /// 获取相应的时间数量
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public int getTimeCount(DateTime time)
        {
            int row = dao.getTimeCount(time);
            return row;
        }
        /// <summary>
        /// 获取种类数量
        /// </summary>
        /// <param name="sellOffHeadId"></param>
        /// <returns></returns>
        public int getKinds(string sellOffHeadId)
        {
            int kinds = dao.getKinds(sellOffHeadId);
            return kinds;
        }
        /// <summary>
        /// 通过单体计算后更新单头信息
        /// </summary>
        /// <param name="sell"></param>
        /// <returns></returns>
        public Result Update(SellOffHead sell)
        {
            int row = dao.Update(sell);
            if (row > 0)
            {
                return Result.更新成功;
            }
            else
            {
                return Result.更新失败;
            }
        }

        /// <summary>
        /// 导出表格
        /// </summary>
        /// <param name="strWhere">销退单头Id</param>
        /// <returns></returns>
        public DataTable ExportExcel(string strWhere)
        {
            return dao.ExportExcel(strWhere);
        }
    }
}
