using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    using Dao;
    using Model;
    using System.Data;
    using Result = Enums.OpResult;
    public class SaleMonomerBll
    {
        SaleMonomerDao SaleMonomerdao = new SaleMonomerDao();
        /// <summary>
        /// 查询该销售单头下是否有单体
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns></returns>
        public int SelectBySaleHeadId(string saleHeadId)
        {
            int count = SaleMonomerdao.SelectBySaleHeadId(saleHeadId);
            if (count == 0)
            {
                return count = 0;
            }
            else
            {
                return count;
            }
        }
        /// <summary>
        /// 删除销售单头
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns></returns>
        public Result realDelete(string saleTaskId, string saleHeadId)
        {

            int row = SaleMonomerdao.realDelete(saleTaskId,saleHeadId);
            if (row > 0)
            {
                return Result.删除成功;
            }
            else
            {
                return Result.删除失败;
            }
        }
        public int getkinds(string saleTaskId, string saleHeadId)
        {
            return SaleMonomerdao.getkinds(saleTaskId, saleHeadId);

        }
        /// <summary>
        /// 根据销售单头ID查询该销售单的状态
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns>状态</returns>
        public int saleheadstate(string saleTaskId, string saleHeadId)
        {
            DataSet ds = SaleMonomerdao.saleheadstate(saleTaskId, saleHeadId);
            int state;
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return state = Convert.ToInt32(ds.Tables[0].Rows[0]["state"].ToString());
            }
            else
            {
                return state = 4;
            }
        }
        /// <summary>
        /// 单体添加
        /// </summary>
        /// <param name="salemonomer">单体实体</param>
        /// <returns>返回结果</returns>
        public Result Insert(SaleMonomer salemonomer)
        {
            int row = SaleMonomerdao.Insert(salemonomer);
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
        /// 删除单体
        /// </summary>
        /// <param name="saleIdMonomerId">单体id</param>
        /// <param name="saleHeadId">单头id</param>
        /// <returns></returns>
        public Result Delete(string saleIdMonomerId, string saleHeadId)
        {
            int row = SaleMonomerdao.Delete(saleIdMonomerId, saleHeadId);
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
        /// 判断是否关联引用
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="primarykeyname">主键名</param>
        /// <param name="primarykey">主键</param>
        /// <returns></returns>
        public Result IsDelete(string table, string primarykeyname, string primarykey)
        {
            PublicProcedure pp = new PublicProcedure();
            int row = pp.isDelete(table, primarykeyname, primarykey);
            if (row > 0)
            {
                return Result.关联引用;
            }
            else
            {
                return Result.记录不存在;
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
        /// <summary>
        /// 更新销售单体
        /// </summary>
        /// <param name="salemonomer">销售单体实体</param>
        /// <returns></returns>
        public Result Update(SaleMonomer salemonomer)
        {
            int row = SaleMonomerdao.Update(salemonomer);
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
        /// 更新单头
        /// </summary>
        /// <param name="salehead">单头实体</param>
        /// <returns></returns>
        public Result updateHead(SaleHead salehead)
        {
            int row = SaleMonomerdao.updateHead(salehead);
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
        /// 更新销售单头状态
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">销售单头</param>
        /// <param name="state">状态 0新创单据 1采集中 2已完成</param>
        /// <returns>受影响行数</returns>
        public Result updateHeadstate(string saleTaskId, string saleHeadId, int state)
        {
            int row = SaleMonomerdao.updateHeadstate(saleTaskId, saleHeadId, state);
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
        /// 根据单头获取单体
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns></returns>
        public DataSet SelectMonomers(string saleHeadId)
        {
            return SaleMonomerdao.SelectMonomers(saleHeadId);
        }
        /// <summary>
        /// 判断该书号是否是第一次添加
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public int SelectnumberBysaletask(string saleTaskId, string bookNum)
        {
            return SaleMonomerdao.SelectnumberBysaletask(saleTaskId, bookNum);
        }

        /// <summary>
        /// 通过书号查询在单体中是否存在记录
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="type">单体类型（0：出库，1：入库，2：退货）</param>
        /// <returns></returns>
        public Result SelectBybookNum(string retailHeadId, string bookNum)
        {
            int row = SaleMonomerdao.SelectBybookNum(retailHeadId, bookNum);
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
        /// 获取该书号在该销售任务下的已购数量
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="bookNum">书号</param>
        /// <returns>返回数据集</returns>
        public DataSet SelectCountBybookNum(string saleTaskId, string bookNum)
        {
            return SaleMonomerdao.SelectCountBybookNum(saleTaskId, bookNum);
        }
    }
}
