using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using bms.Model;
using bms.DBHelper;

namespace bms.Dao
{
    public class SellOffMonomerDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取销退单头信息
        /// </summary>
        /// <returns></returns>
        public DataSet Select(string sellOffHeadId)
        {
            string cmdText = "select sellOffHead,sellOffMonomerId,bookNum,isbn,price,count,totalPrice,realDiscount,realPrice,dateTime from T_SellOffMonomer where sellOffHead=@sellOffHeadId";
            string[] param = { "@sellOffHeadId" };
            object[] values = { sellOffHeadId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }
        /// <summary>
        /// 通过书号查询销售单中是否有此数据
        /// </summary>
        /// <param name="bookNum"></param>
        /// <returns></returns>
        public int getBookCount(string bookNum)
        {
            string cmdText = "select count(bookNum) from V_SaleMonomer where bookNum=@bookNum";
            string[] param = { "@bookNum" };
            object[] values = { bookNum };
            int row = int.Parse(db.ExecuteScalar(cmdText, param, values).ToString());
            return row;
        }
        /// <summary>
        /// 添加销退单头
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        public int Insert(SellOffMonomer sm)   
        {
            string cmdText = "insert into T_SellOffMonomer(sellOffMonomerId,sellOffHead,bookNum,isbn,price,count,totalPrice,realPrice,dateTime,realDiscount) values(@sellOffMonomerId,@sellOffHeadID,@bookNum,@isbn,@price,@count,@totalPrice,@realPrice,@dateTime,@realDiscount)";
            string[] param = { "@sellOffMonomerId", "@sellOffHeadID", "@bookNum", "@isbn", "@price", "@count", "@totalPrice", "@realPrice", "@dateTime", "@realDiscount" };
            object[] values = { sm.SellOffMonomerId, sm.SellOffHeadId, sm.BookNum, sm.ISBN1, sm.Price, sm.Count, sm.TotalPrice, sm.RealPrice, sm.Time,sm.Discount };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 通过单头Id获取单体数量
        /// </summary>
        /// <param name="sellOffHeaId"></param>
        /// <returns></returns>
        public int GetCount(string sellOffHeaId)
        {
            string cmdText = "select COUNT(sellOffMonomerId) from T_SellOffMonomer where sellOffHead=@sellOffHead";
            string[] param = { "@sellOffHead" };
            object[] values = { sellOffHeaId };
            int row = int.Parse(db.ExecuteScalar(cmdText, param, values).ToString());
            return row;
        }
        /// <summary>
        /// 根据单头Id获取任务Id
        /// </summary>
        /// <param name="sellOffHeadId"></param>
        /// <returns></returns>
        public DataSet getSaleTask(string sellOffHeadId)
        {
            string sql = "select saleTaskId from T_SellOffHead where sellOffHeadID=@sellHeadId";
            string[] param = { "@sellHeadId" };
            object[] values = { sellOffHeadId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 根据任务Id获取默认折扣
        /// </summary>
        /// <param name="saleTaskId"></param>
        /// <returns></returns>
        public DataSet getDisCount(string saleTaskId)
        {
            string sql = "select defaultDiscount from T_SaleTask where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 根据书号到销售单体视图获取信息
        /// </summary>
        /// <param name="bookNum"></param>
        /// <returns></returns>
        public DataSet selctByBookNum(string bookNum,string saleTaskId)
        {
            string sql = "select number from V_SaleMonomer where bookNum=@bookNum and saleTaskId=@saleTaskId";
            string[] param = { "@bookNum", "@saleTaskId" };
            object[] values = { bookNum,saleTaskId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 通过书号和单头Id查询单体表中相应的书籍数量
        /// </summary>
        /// <param name="bookNum"></param>
        /// <param name="sellOffHeadId"></param>
        /// <returns></returns>
        public DataSet selecctSm(string bookNum,string sellOffHeadId)
        {
            string sql = "select count from T_SellOffMonomer where bookNum=@bookNum and sellOffHead=@sellOffHeadId";
            string[] param = { "@bookNum", "@sellOffHeadId" };
            object[] values = { bookNum, sellOffHeadId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
    }
}
