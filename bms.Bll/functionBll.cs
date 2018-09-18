using bms.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    using Model;
    using System.Data;
    using Result = Enums.OpResult;
    public class FunctionBll
    {
        FunctionDao functiondao = new FunctionDao();
        /// <summary>
        /// 添加功能方法
        /// </summary>
        /// <param name="function">功能实体对象</param>
        /// <returns>返回结果</returns>
        public Result Insert(Function function)
        {
            int row = functiondao.Insert(function);
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
        /// 删除功能信息
        /// </summary>
        /// <param name="functioId">功能ID</param>
        /// <returns></returns>
        public Result Delete(int functioId)
        {
            int row = functiondao.Delete(functioId);
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
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public DataSet Select()
        {
            return functiondao.Select();
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tablebuilder"></param>
        /// <param name="totalCount"></param>
        /// <param name="intPageCount"></param>
        /// <returns></returns>
        public DataSet selectByPage(TableBuilder tablebuilder, out int totalCount, out int intPageCount)
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
        /// 判断该条记录是否关联在另一张表中
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="primarykeyname">主键列</param>
        /// <param name="primarykey">主键参数</param>
        /// <returns>引用代表数据存在不可删除，记录不存在表示可以删除</returns>
        public Result isDelete(string table, string primarykeyname, string primarykey)
        {
            PublicProcedure procedure = new PublicProcedure();
            int row = procedure.isDelete(table, primarykeyname, primarykey);

            if (row > 0)
            {
                return Result.关联引用;
            }
            else
            {
                return Result.记录不存在;
            }
        }
    }
}
