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
    }
}
