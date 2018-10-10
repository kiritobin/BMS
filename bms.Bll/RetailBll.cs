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
        public DataSet getAllTime()
        {
            DataSet ds = dao.getAllTime();
            return ds;
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
    }
}
