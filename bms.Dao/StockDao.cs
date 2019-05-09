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
        public int update(int stockNum, string goodsShelvesId, string bookNum)
        {
            string cmdText = "update T_Stock set stockNum=@stockNum where goodsShelvesId=@goodsShelvesId and bookNum=@bookNum";
            string[] param = { "@stockNum", "@goodsShelvesId", "@bookNum" };
            object[] values = { stockNum, goodsShelvesId, bookNum };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }


        /// <summary>
        /// 根据书号，组织Id查询货架id，库存数量
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="regionId">组织Id</param>
        /// <returns></returns>
        public DataSet SelectByBookNum(string bookNum, int regionId)
        {
            string cmdText = "select goodsShelvesId,stockNum from T_Stock where bookNum = @bookNum and regionId = @regionId order by stockNum asc";
             String[] param = { "@bookNum", "@regionId" };
             String[] values = { bookNum.ToString(), regionId.ToString() };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ds;
            }
        }
        /// <summary>
        /// 书籍库存导出全部
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="groupbyType"></param>
        /// <returns></returns>
        public DataSet bookStock(string str, string groupType)
        {
            string cmdText = "select sum(stockNum) as 库存数量,count(bookNum) as 品种数,supplier as 供应商, regionName as 组织名称 from v_stock where " + str + " group by " + groupType + " order by "+ groupType;
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
        /// <summary>
        /// 书籍库存导出明细
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public DataSet bookStockDetail(string str,string groupType)
        {
            string cmdText = "select ISBN,bookNum as 书号,bookName as 书名,price as 单价,sum(stockNum) as 数量, author as 进货折扣,remarks as 销售折扣,supplier as 供应商, regionName as 组织名称,shelvesName as 货架名称,goodsShelvesId as 货架编号,dentification as 备注,remarksOne as 备注1,remarksTwo as 备注2,remarksThree as 备注3 from v_stock where " + str + " group by bookNum," + groupType + " order by " + groupType;
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

        /// <summary>
        /// 获取库存数量
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="goodsShelf">货架Id</param>
        /// <returns></returns>
        public int getStockNum(string bookNum, string goodsShelf, int regionId)
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
        public int GetByBookNum(string bookNum, string goodsShelf)
        {
            string cmdText = "select count(stockId) from T_Stock where bookNum=@bookNum and goodsShelvesId=@goodsShelf";
            String[] param = { "@bookNum", "@goodsShelf" };
            String[] values = { bookNum.ToString(), goodsShelf.ToString() };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            return row;
        }
        /// <summary>
        /// 通过书号,组织Id获取库存
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="regionId">组织Id</param>
        /// <returns></returns>
        public int selectStockNum(string bookNum,int regionId)
        {
            string cmdText = "select sum(stockNum) as stockNum from T_Stock where bookNum=@bookNum and regionId=@regionId";
            String[] param = { "@bookNum" ,"@regionId" };
            String[] values = { bookNum, regionId.ToString() };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string count = ds.Tables[0].Rows[0]["stockNum"].ToString();
                if(count != null && count != "")
                {
                    return Convert.ToInt32(count);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 通过书号获取库存
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public int selectStockNum(string bookNum)
        {
            string cmdText = "select stockNum from T_Stock where bookNum=@bookNum";
            String[] param = { "@bookNum" };
            String[] values = { bookNum };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if(ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                return 0;
            }
            else
            {
                cmdText = "select sum(stockNum) as stockNum from T_Stock where bookNum=@bookNum";
                int count = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
                return count;
            }
        }

        /// <summary>
        /// 导出成Excel表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="groupType">分组条件</param>
        /// <returns>返回一个DataTable的选题记录集合</returns>
        public DataTable ExportExcelDetails(string strWhere, string groupType)
        {
            String cmdText = "select ISBN,bookNum as 书号,bookName as 书名,price as 单价,sum(stockNum) as 数量, author as 进货折扣,remarks as 销售折扣,supplier as 供应商, regionName as 组织名称,shelvesName as 货架名称,goodsShelvesId as 货架编号,dentification as 备注,remarksOne as 备注1,remarksTwo as 备注2,remarksThree as 备注3 from v_stock where " + strWhere + " group by bookNum," + groupType + " order by "+ groupType;
            DataSet ds = db.FillDataSet(cmdText, null, null);
            DataTable dt = null;
            int count = ds.Tables[0].Rows.Count;
            dt = ds.Tables[0];
            return dt;
        }

        /// <summary>
        /// 书籍库存打印统计
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="groupType"></param>
        /// <returns></returns>
        public DataTable census(string strWhere, string groupType)
        {
            String cmdText = "select count(书号) as 品种数量,sum(数量) as 总数 from((select ISBN,bookNum as 书号,bookName as 书名,price as 单价,sum(stockNum) as 数量, author as 进货折扣,remarks as 销售折扣,supplier as 供应商, regionName as 组织名称,shelvesName as 货架名称,goodsShelvesId as 货架编号,dentification as 备注,remarksOne as 备注1,remarksTwo as 备注2,remarksThree as 备注3 from v_stock where " + strWhere + " group by bookNum," + groupType + " order by " + groupType + ") as temp)";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            DataTable dt = null;
            int count = ds.Tables[0].Rows.Count;
            dt = ds.Tables[0];
            return dt;
        }
    }
}
