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
        /// 查询所有功能
        /// </summary>
        /// <returns>返回查询的数据表</returns>
        public DataSet Select()
        {
            string cmdText = "select goodsShelvesId,shelvesName,regionId,regionName from V_GoodsShelves";
            DataSet ds = db.FillDataSet(cmdText, null, null);
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
