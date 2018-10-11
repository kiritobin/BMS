using bms.Dao;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class RetailBll
    {
        RetailDao dao = new RetailDao();
        /// <summary>
        /// 获取该销退单头下所有的单据号和制单时间
        /// </summary>
        /// <returns></returns>
        public DataSet getAllTime(int state)
        {
            DataSet ds = dao.getAllTime(state);
            return ds;
        }

        /// <summary>
        /// 通过书号查看是否存在在单头中
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public Result selectByBookNum(long bookNum, string retailHeadId)
        {
            int row = dao.selectByBookNum(bookNum, retailHeadId);
            if (row > 0)
            {
                return Result.记录存在;
            }
            else
            {
                return Result.记录不存在;
            }
        }

        /// <summary>
        /// 销售单头添加
        /// </summary>
        /// <param name="salehead">销售单头实体</param>
        /// <returns>返回结果</returns>
        public Result InsertRetail(SaleHead salehead)
        {
            int row = dao.InsertRetail(salehead);
            if (row > 0)
            {
                return Result.添加成功;
            }
            else
            {
                return Result.添加失败;
            }
        }

        /// <summary>
        /// 零售单体添加
        /// </summary>
        /// <param name="salemonomer">零售单体实体</param>
        /// <returns>返回结果</returns>
        public Result InsertRetail(SaleMonomer salemonomer)
        {
            int row = dao.InsertRetail(salemonomer);
            if (row > 0)
            {
                return Result.添加成功;
            }
            else
            {
                return Result.添加失败;
            }
        }

        /// <summary>
        /// 查询零售单体
        /// </summary>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>受影响行数</returns>
        public SaleHead GetHead(string retailHeadId)
        {
            return dao.GetHead(retailHeadId);
        }

        /// <summary>
        /// 查询单头下的所有单体零售单体
        /// </summary>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>受影响行数</returns>
        public DataSet GetRetail(string retailHeadId)
        {
            DataSet ds = dao.GetRetail(retailHeadId);
            if(ds == null || ds.Tables[0].Rows.Count<=0)
            { 
                return null;
            }
            else
            {
                return ds;
            }
        }

        /// <summary>
        /// 查询零售单体
        /// </summary>
        /// <param name="retailMonomerId">零售单体ID</param>
        /// <returns>受影响行数</returns>
        public SaleMonomer GetMonomer(int retailMonomerId)
        {
            return dao.GetMonomer(retailMonomerId);
        }

        /// <summary>
        /// 更新零售折扣
        /// </summary>
        /// <param name="realDiscount">折扣</param>
        /// <param name="realPrice">实洋</param>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>受影响行数</returns>
        public Result UpdateDiscount(double realDiscount, double realPrice, string retailHeadId)
        {
            int row = dao.UpdateDiscount(realDiscount, realPrice, retailHeadId);
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
        /// 更新零售单头实洋
        /// </summary>
        /// <param name="realPrice">实洋</param>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>受影响行数</returns>
        public Result UpdateHeadReal(double allRealPrice, string retailHeadId)
        {
            int row = dao.UpdateHeadReal(allRealPrice, retailHeadId);
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
        /// 更新零售数量
        /// </summary>
        /// <param name="sale">零售实体对象</param>
        /// <returns>受影响行数</returns>
        public Result UpdateNumber(SaleMonomer sale)
        {
            int row = dao.UpdateNumber(sale);
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
        /// 更新零售数量
        /// </summary>
        /// <param name="sale">零售实体对象</param>
        /// <returns>受影响行数</returns>
        public Result UpdateHeadNumber(SaleHead sale)
        {
            int row = dao.UpdateHeadNumber(sale);
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
        /// 更新零售数量
        /// </summary>
        /// <param name="sale">零售实体对象</param>
        /// <returns>受影响行数</returns>
        public Result updateType(string headId)
        {
            int row = dao.updateType(headId);
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
        /// 删除单体信息
        /// </summary>
        /// <param name="retailMonomerId">零售单体ID</param>
        /// <returns></returns>
        public Result delete(int retailMonomerId)
        {
            int row = dao.delete(retailMonomerId);
            if(row > 0)
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
