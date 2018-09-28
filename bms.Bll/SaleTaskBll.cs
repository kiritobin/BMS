using bms.Dao;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    using System.Data;
    using Result = Enums.OpResult;
    public class SaleTaskBll
    {
        readonly SaleTaskDao saleDao = new SaleTaskDao();
        /// <summary>
        /// 添加销售任务方法
        /// </summary>
        /// <param name="task">实体销售任务</param>
        /// <returns>返回结果</returns>
        public Result insert(SaleTask sale)
        {
            int row = saleDao.Insert(sale);
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
        /// 销售任务删除
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns>返回结果</returns>
        public Result Delete(int saleTaskId)
        {
          int row=  saleDao.Delete(saleTaskId);
            if (row>0)
            {
                return Result.删除失败;
            } else
            {
                return Result.删除失败;
            }
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tablebuilder"></param>
        /// <param name="totalCount"></param>
        /// <param name="intPageCount"></param>
        /// <returns></returns>
        public DataSet selectBypage(TableBuilder tablebuilder, out int totalCount, out int intPageCount)
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
