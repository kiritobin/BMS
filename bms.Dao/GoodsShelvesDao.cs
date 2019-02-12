using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class GoodsShelvesDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 货架删除
        /// </summary>
        /// <param name="goodsShelvesId">货架id</param>
        /// <returns>受影响行数</returns>
        public int Delete(string goodsShelvesId)
        {
            string cmdText = "update T_GoodsShelves set deleteState = 1 where goodsShelvesId=@goodsShelvesId";
            String[] param = { "@goodsShelvesId" };
            String[] values = { goodsShelvesId.ToString() };
            return db.ExecuteNoneQuery(cmdText, param, values);
        }
        /// <summary>
        /// 真货架删除
        /// </summary>
        /// <param name="goodsShelvesId"></param>
        /// <returns></returns>
        public int DeleteTrue(string goodsShelvesId)
        {
            string cmdText = "DELETE FROM t_goodsshelves WHERE goodsShelvesId = @goodsShelvesId";
            String[] param = { "@goodsShelvesId" };
            String[] values = { goodsShelvesId };
            int row = Convert.ToInt32(db.ExecuteNoneQuery(cmdText, param, values));
            return db.ExecuteNoneQuery(cmdText, param, values);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="shelves">货架</param>
        /// <returns>受影响行数</returns>
        public int Insert(GoodsShelves shelves)
        {
            string cmdText = "insert into T_GoodsShelves(goodsShelvesId,shelvesName,regionId) values(@goodsShelvesId,@shelvesName,@regionId)";
            String[] param = { "@goodsShelvesId", "@shelvesName", "@regionId" };
            String[] values = { shelves.GoodsShelvesId,shelves.ShelvesName,shelves.RegionId.RegionId.ToString() };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
       /// <summary>
       /// 根据地区id查询货架
       /// </summary>
       /// <param name="regionId">地区id</param>
       /// <returns></returns>
        public DataSet Select(int regionId)
        {
            string cmdText = "select goodsShelvesId,shelvesName,regionId,regionName from V_GoodsShelves where regionId = @regionId";
            String[] param = { "@regionId" };
            String[] values = { regionId.ToString() };
            DataSet ds = db.FillDataSet(cmdText, param, values);
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
        /// 判断有无货架
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        public DataSet isGoodsShelves(int regionId)
        {
            string cmdText = "select goodsShelvesId,shelvesName from T_GoodsShelves where regionId = @regionId";
            String[] param = { "@regionId" };
            String[] values = { regionId.ToString() };
            DataSet ds = db.FillDataSet(cmdText, param, values);
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
        /// 通过地区id和货架名查货架id
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="shelvesName"></param>
        /// <returns></returns>
        public DataSet SelectByIdName(int regionId,string shelvesName)
        {
            string cmdText = "select goodsShelvesId from T_GoodsShelves where regionId = @regionId and shelvesName=@shelvesName";
            String[] param = { "@regionId", "@shelvesName" };
            String[] values = { regionId.ToString(),shelvesName };
            DataSet ds = db.FillDataSet(cmdText, param, values);
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
        /// 查询货架名是否重复
        /// </summary>
        /// <param name="shelves">货架实体对象</param>
        /// <returns></returns>
        public int selectByName(GoodsShelves shelves)
        {
            //货架ID
            string cmdText = "select count(goodsShelvesId) from T_GoodsShelves where goodsShelvesId=@goodsShelvesId";
            String[] param = { "@goodsShelvesId"};
            String[] values = { shelves.GoodsShelvesId };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            //货架名称
            string cmdTexts = "select count(goodsShelvesId) from T_GoodsShelves where regionId = @regionId and shelvesName=@shelvesName";
            String[] parames = { "@shelvesName", "@regionId" };
            String[] valuess = { shelves.ShelvesName, shelves.RegionId.RegionId.ToString() };
            int rows = Convert.ToInt32(db.ExecuteScalar(cmdTexts, parames, valuess));
            if (row > 0 && rows > 0)//都重复
            {
                return 2;
            }
            else if (row > 0 && rows <= 0)//货架ID重复
            {
                return 1;
            }
            else if (rows > 0 && row <= 0)//货架名称重复
            {
                return -1;
            }
            else//都不重复
            {
                return 0;
            }
        }
    }
}
