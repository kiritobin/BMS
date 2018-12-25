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
        public Result update(int stockNum, int goodsShelvesId, string bookNum)
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
        /// 根据书号，组织Id查询货架id，库存数量
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="regionId">组织Id</param>
        /// <returns></returns>
        public DataSet SelectByBookNum(string bookNum, int regionId)
        {
            return stockDao.SelectByBookNum(bookNum,regionId);
        }
        /// <summary>
        /// 书籍库存导出全部
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="groupbyType"></param>
        /// <returns></returns>
        public DataSet bookStock(string str)
        {
            return stockDao.bookStock(str);
        }
        /// <summary>
        /// 书籍库存导出明细
        /// </summary>
        /// <returns></returns>
        public DataSet bookStockDetail()
        {
            return stockDao.bookStockDetail();
        }

        /// <summary>
        /// 判断此书号是否有库存
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="goodsShelf">货架号</param>
        /// <returns></returns>
        public Result GetByBookNum(string bookNum, int goodsShelf)
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
        public int getStockNum(string bookNum, int goodsShelf,int regionId)
        {
            return stockDao.getStockNum(bookNum, goodsShelf, regionId);
        }
        /// <summary>
        /// 通过书号、地区ID获取库存
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="regionId">地区id</param>
        /// <returns></returns>
        public int selectStockNum(string bookNum, int regionId)
        {
            return stockDao.selectStockNum(bookNum,regionId);
        }

        /// <summary>
        /// 通过书号获取库存
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public int selectStockNum(string bookNum)
        {
            return stockDao.selectStockNum(bookNum);
        }
    }
}
