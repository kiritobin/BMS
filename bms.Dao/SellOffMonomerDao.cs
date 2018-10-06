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
        public DataSet Select()
        {
            string cmdText = "select sellOffHead,sellOffMonomerId,bookNum,isbn,price,count,totalPrice,realDiscount,realPrice,dateTime from T_SellOffMonomer";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            return ds;
        }
        /// <summary>
        /// 根据ISBN号查询书籍基础数据
        /// </summary>
        /// <param name="ISBN"></param>
        /// <returns></returns>
        public DataSet SelectByISBN(string ISBN)
        {
            string cmdText = "select bookNum,bookName,supplier from T_BookBasicData where ISBN=@ISBN";
            string[] param = { "@ISBN" };
            object[] values = { ISBN };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
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
        public SellOffHead getSaleTask(string sellOffHeadId)
        {
            string sql = "select saleTaskId from T_SellOffHead where sellOffHeadID=@sellHeadId";
            string[] param = { "@sellHeadId" };
            object[] values = { sellOffHeadId };
            SellOffHead sh = new SellOffHead();
            sh.SaleTaskId.SaleTaskId = db.ExecuteScalar(sql, param, values).ToString();
            return sh;
        }
        /// <summary>
        /// 根据任务Id获取默认折扣
        /// </summary>
        /// <param name="saleTaskId"></param>
        /// <returns></returns>
        public SaleTask getDisCount(string saleTaskId)
        {
            string sql = "select defaultDiscount from T_SaleTask where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            SaleTask st = new SaleTask();
            st.DefaultDiscount = double.Parse(db.ExecuteScalar(sql, param, values).ToString());
            return st;
        }
    }
}
