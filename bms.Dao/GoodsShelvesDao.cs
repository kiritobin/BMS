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
        public int Delete(int goodsShelvesId)
        {
            string cmdText = "update T_GoodsShelves set deleteState = 1 where goodsShelvesId=@goodsShelvesId";
            String[] param = { "@goodsShelvesId" };
            String[] values = { goodsShelvesId.ToString() };
            return db.ExecuteNoneQuery(cmdText, param, values);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="shelves">货架</param>
        /// <returns>受影响行数</returns>
        public int Insert(GoodsShelves shelves)
        {
            string cmdText = "insert into T_GoodsShelves(shelvesName,regionId) values(@shelvesName,@regionId)";
            String[] param = { "@shelvesName", "@regionId" };
            String[] values = { shelves.ShelvesName,shelves.RegionId.RegionId.ToString() };
            return db.ExecuteNoneQuery(cmdText, param, values);
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
        /// 查询货架名是否重复
        /// </summary>
        /// <param name="shelves">货架实体对象</param>
        /// <returns></returns>
        public DataSet selectByName(GoodsShelves shelves)
        {
            string cmdText = "select shelvesName=@shelvesName from T_GoodsShelves where regionId = @regionId";
            String[] param = { "@shelvesName", "@regionId" };
            String[] values = { shelves.ShelvesName, shelves.RegionId.RegionId.ToString() };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
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
