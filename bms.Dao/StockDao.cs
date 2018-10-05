using bms.DBHelper;
using bms.Model;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class StockDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 添加库存信息
        /// </summary>
        /// <param name="stock">库存实体对象</param>
        /// <returns></returns>
        public int insert(Stock stock)
        {
            string cmdText = "insert into T_Stock(stockNum,ISBN,regionId,goodsShelvesId) values(@stockNum,@ISBN,@regionId,@goodsShelvesId)";
            string[] param = { "@stockNum", "@ISBN", "@regionId", "@goodsShelvesId" };
            object[] values = { stock.StockNum, stock.ISBN.Isbn, stock.RegionId.RegionId, stock.GoodsShelvesId.GoodsShelvesId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 根据货架号更新库存信息
        /// </summary>
        /// <param name="stockNum">库存数量</param>
        /// <param name="goodsShelvesId">货架号</param>
        /// <returns></returns>
        public int update(int stockNum,int goodsShelvesId)
        {
            string cmdText = "update T_Stock set stockNum=@stockNum where goodsShelvesId=@goodsShelvesId";
            string[] param = { "@stockNum", "@goodsShelvesId" };
            object[] values = { stockNum,goodsShelvesId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }


        /// <summary>
        /// 根据ISBN查询货架id，库存数量
        /// </summary>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public DataSet SelectByIsbn(string ISBN)
        {
            string cmdText = "select goodsShelvesId,stockNum from T_Stock where ISBN = @ISBN order by stockNum desc";
            String[] param = { "@ISBN" };
            String[] values = { ISBN };
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
        /// 根据货架id和ISBN查询库存数量
        /// </summary>
        /// <param name="goodsShelvesId">货架id</param>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public int SelectByGoodsId(int goodsShelvesId,string ISBN)
        {
            string cmdText = "select stockNum from T_Stock where goodsShelvesId = @goodsShelvesId and ISBN=@ISBN";
            String[] param = { "@goodsShelvesId" , "@ISBN" };
            String[] values = { goodsShelvesId.ToString(), ISBN };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                int goodsId = Convert.ToInt32(ds.Tables[0].Rows[0]["stockNum"]);
                return goodsId;
            }
            else
            {
                return 0;
            }
        }
    }
}
