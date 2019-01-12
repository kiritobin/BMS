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
        public DataSet bookStock(string str,string groupType)
        {
            return stockDao.bookStock(str,groupType);
        }
        /// <summary>
        /// 书籍库存导出明细
        /// </summary>
        /// <returns></returns>
        public DataSet bookStockDetail(string str, string groupType)
        {
            return stockDao.bookStockDetail(str, groupType);
        }
        public DataTable bookStockDe(string str, string groupType)
        {
            DataTable excel = new DataTable();
            excel.Columns.Add("ISBN");
            excel.Columns.Add("书号");
            excel.Columns.Add("书名");
            excel.Columns.Add("单价");
            excel.Columns.Add("进货折扣");
            excel.Columns.Add("销售折扣");
            excel.Columns.Add("供应商");
            excel.Columns.Add("组织名称");
            excel.Columns.Add("备注");
            excel.Columns.Add("备注1");
            excel.Columns.Add("备注2");
            excel.Columns.Add("备注3");
            DataSet ds = stockDao.bookStockDetail(str, groupType);
            if (ds!=null)
            {
                DataRowCollection count = ds.Tables[0].Rows;
                foreach (DataRow row in count)
                {
                    string bookName = ToDBC(row[2].ToString());
                    excel.Rows.Add(row[0], row[1], bookName, row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11]);
                }
            }
            return excel;
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

        /// <summary>
        /// 书籍库存详情导出明细
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="groupType">分组方式</param>
        /// <returns></returns>
        public DataTable ExportExcelDetail(string strWhere, string groupType)
        {
            DataTable excel = new DataTable();
            excel.Columns.Add("ISBN");
            excel.Columns.Add("书号");
            excel.Columns.Add("书名");
            excel.Columns.Add("单价");
            excel.Columns.Add("数量");
            excel.Columns.Add("进货折扣");
            excel.Columns.Add("销售折扣");
            excel.Columns.Add("供应商");
            excel.Columns.Add("组织名称");
            excel.Columns.Add("备注");
            excel.Columns.Add("备注1");
            excel.Columns.Add("备注2");
            excel.Columns.Add("备注3");
            DataTable dt = stockDao.ExportExcelDetails(strWhere, groupType);
            if (dt!=null)
            {
                DataRowCollection count = dt.Rows;
                foreach (DataRow row in count)
                {
                    string bookName = ToDBC(row[2].ToString());
                    excel.Rows.Add(row[0], row[1], bookName, row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12]);
                }
            }            
            return excel;
        }

        /// <summary>
        /// 全转半
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 12288)
                {
                    array[i] = (char)32;
                    continue;
                }
                if (array[i] > 65280 && array[i] < 65375)
                {
                    array[i] = (char)(array[i] - 65248);
                }
            }
            return new string(array);
        }
    }
}
