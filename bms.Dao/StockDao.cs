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
            string cmdText = "insert into T_Stock(stockNum,bookNum,ISBN,regionId,goodsShelvesId) values(@stockNum,@bookNum,@ISBN,@regionId,@goodsShelvesId)";
            string[] param = { "@stockNum", "@bookNum", "@ISBN", "@regionId", "@goodsShelvesId" };
            object[] values = { stock.StockNum, stock.BookNum.BookNum, stock.ISBN.Isbn, stock.RegionId.RegionId, stock.GoodsShelvesId.GoodsShelvesId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 根据货架号更新库存信息
        /// </summary>
        /// <param name="stockNum">库存数量</param>
        /// <param name="goodsShelvesId">货架号</param>
        /// <returns></returns>
        public int update(int stockNum, int goodsShelvesId, string bookNum)
        {
            string cmdText = "update T_Stock set stockNum=@stockNum where goodsShelvesId=@goodsShelvesId and bookNum=@bookNum";
            string[] param = { "@stockNum", "@goodsShelvesId", "@bookNum" };
            object[] values = { stockNum, goodsShelvesId, bookNum };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }


        /// <summary>
        /// 根据ISBN查询货架id，库存数量
        /// </summary>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public DataSet SelectByBookNum(string bookNum, int regionId)
        {
            string cmdText = "select goodsShelvesId,stockNum from T_Stock where bookNum = @bookNum and regionId = @regionId order by stockNum asc";
             String[] param = { "@bookNum", "@regionId" };
             String[] values = { bookNum.ToString(), regionId.ToString() };
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
        public int getStockNum(string bookNum, int goodsShelf, int regionId)
        {
            string cmdText = "select stockNum from T_Stock where goodsShelvesId = @goodsShelf and bookNum=@bookNum and regionId=@regionId";
            String[] param = { "@goodsShelf", "@bookNum", "@regionId" };
            String[] values = { goodsShelf.ToString(), bookNum.ToString(), regionId.ToString() };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
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
        public int GetByBookNum(string bookNum, int goodsShelf)
        {
            string cmdText = "select count(stockId) from T_Stock where bookNum=@bookNum and goodsShelvesId=@goodsShelf";
            String[] param = { "@bookNum", "@goodsShelf" };
            String[] values = { bookNum.ToString(), goodsShelf.ToString() };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            return row;
        }
        /// <summary>
        /// 通过书号获取库存
        /// </summary>
        /// <param name="bookNum"></param>
        /// <returns></returns>
        public int selectStockNum(string bookNum)
        {
            string cmdText = "select stockNum from T_Stock where bookNum=@bookNum";
            String[] param = { "@bookNum" };
            String[] values = { bookNum.ToString() };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            int count, counts = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    count = Convert.ToInt32(ds.Tables[0].Rows[i]["stockNum"]);
                    counts = counts + count;
                }
                return counts;
            }
            else
            {
                return 0;
            }
        }
    }
}
