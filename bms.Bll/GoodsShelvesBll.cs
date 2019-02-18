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
    public class GoodsShelvesBll
    {
        GoodsShelvesDao shelvesdao = new GoodsShelvesDao();

        /// <summary>
        /// 添加书架方法
        /// </summary>
        /// <param name="shelves">书架</param>
        /// <returns>返回结果</returns>
        public Result Insert(GoodsShelves shelves)
        {
            int row = shelvesdao.Insert(shelves);
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
        /// 删除书架的方法
        /// </summary>
        /// <param name="goodsShelvesId">书架id</param>
        /// <returns>返回结果</returns>
        public Result Delete(string goodsShelvesId)
        {
            int row = shelvesdao.Delete(goodsShelvesId);
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
        /// 真删除书架的方法
        /// </summary>
        /// <param name="goodsShelvesId"></param>
        /// <returns></returns>
        public Result DeleteTrue(string goodsShelvesId)
        {
            int row = shelvesdao.DeleteTrue(goodsShelvesId);
            if (row > 0)
            {
                return Result.删除成功;
            }
            else
            {
                return Result.删除失败;
            }
        }

        //public int DeleteTrue(string goodsShelvesId)
        //{
        //    return shelvesdao.DeleteTrue(goodsShelvesId);
        //}

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns>返回查询到的表格数据</returns>
        public DataSet Select(int regionId)
        {
            return shelvesdao.Select(regionId);
        }
        /// <summary>
        /// 判断有无货架
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public DataSet isGoodsShelves(int regionId)
        {
            return shelvesdao.isGoodsShelves(regionId);
        }
        /// <summary>
        /// 判断货架存在
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public int isgoodsId(int regionId)
        {
            return shelvesdao.isgoodsId(regionId);
        }
        /// <summary>
        /// 查询货架名是否重复
        /// </summary>
        /// <param name="shelves">货架实体对象</param>
        /// <returns></returns>
        public Result selectByName(GoodsShelves shelves)
        {
            //return shelvesdao.selectByName(shelves);
            int row = shelvesdao.selectByName(shelves);
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
        /// 获取分页数据
        /// </summary>
        /// <param name="tablebuilder"></param>
        /// <param name="totalCount">总页数</param>
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
