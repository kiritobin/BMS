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
        public int update(int stockNum,int goodsShelvesId,long bookNum)
        {
            string cmdText = "update T_Stock set stockNum=@stockNum where goodsShelvesId=@goodsShelvesId and bookNum=@bookNum";
            string[] param = { "@stockNum", "@goodsShelvesId","@bookNum" };
            object[] values = { stockNum,goodsShelvesId, bookNum };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }


        /// <summary>
        /// 根据ISBN查询货架id，库存数量
        /// </summary>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public DataSet SelectByBookNum(long bookNum)
        {
            string cmdText = "select goodsShelvesId,stockNum from T_Stock where bookNum = @bookNum order by stockNum desc";
            String[] param = { "@bookNum" };
            String[] values = { bookNum.ToString() };
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
        /// 获取库存数量
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="goodsShelf">货架Id</param>
        /// <returns></returns>
        public int getStockNum(long bookNum, int goodsShelf)
        {
            string cmdText = "select stockNum from T_Stock where goodsShelvesId = @goodsShelf and bookNum=@bookNum";
            String[] param = { "@goodsShelvesId" , "@bookNum" };
            String[] values = { goodsShelf.ToString(), bookNum.ToString() };
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

        /// <summary>
        /// 判断此书号是否有库存
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="goodsShelf">货架号</param>
        /// <returns></returns>
        public int GetByBookNum(long bookNum, int goodsShelf)
        {
            string cmdText = "select count(stockId) from T_Stock where bookNum = @bookNum order by stockNum desc";
            String[] param = { "@bookNum" };
            String[] values = { bookNum.ToString() };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            return row;
        }
    }
}
