using bms.Dao;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class StockBll
    {
        StockDao stockDao = new StockDao();
        /// <summary>
        /// 添加库存信息
        /// </summary>
        /// <param name="stock">库存实体对象</param>
        /// <returns></returns>
        public Result insert(Stock stock)
        {
            int row = stockDao.insert(stock);
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
        /// 根据货架号更新库存信息
        /// </summary>
        /// <param name="stockNum">库存数量</param>
        /// <param name="goodsShelvesId">货架号</param>
        /// <returns></returns>
        public Result update(int stockNum, int goodsShelvesId)
        {
            int row = stockDao.update(stockNum, goodsShelvesId);
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
        /// 根据ISBN查询货架id，库存数量
        /// </summary>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public DataSet SelectByIsbn(string ISBN)
        {
            return stockDao.SelectByIsbn(ISBN);
        }

        /// <summary>
        /// 根据货架id和ISBN查询库存数量
        /// </summary>
        /// <param name="goodsShelvesId">货架id</param>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public int SelectByGoodsId(int goodsShelvesId, string ISBN)
        {
            return stockDao.SelectByGoodsId(goodsShelvesId, ISBN);
        }
    }
}
