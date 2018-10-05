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
        public Result update(int stockNum, int goodsShelvesId, long bookNum)
        {
            int row = stockDao.update(stockNum, goodsShelvesId, bookNum);
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
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public DataSet SelectByBookNum(long bookNum)
        {
            return stockDao.SelectByBookNum(bookNum);
        }

        /// <summary>
        /// 判断此书号是否有库存
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="goodsShelf">货架号</param>
        /// <returns></returns>
        public Result GetByBookNum(long bookNum, int goodsShelf)
        {
            int row = stockDao.GetByBookNum(bookNum, goodsShelf);
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
        /// 获取库存数量
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="goodsShelf">货架Id</param>
        /// <returns></returns>
        public int getStockNum(long bookNum, int goodsShelf)
        {
            return stockDao.getStockNum(bookNum, goodsShelf);
        }
    }
}
