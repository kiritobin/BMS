using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.DBHelper;
using bms.Model;
using System.Data;

namespace bms.Dao
{
    public class replenishMentDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取补货单体信息
        /// </summary>
        /// <returns></returns>
        public DataSet Select()
        {
            string cmdText = "SELECT * FROM V_ReplenishMentMononer";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            return ds;
        }
        /// <summary>
        /// 插入补货单体
        /// </summary>
        /// <param name="rm"></param>
        /// <returns></returns>
        public int Insert(replenishMentMonomer rm)
        {
            string sql = "insert into T_ReplenishmentMonomer(rsMonomerID,bookNum,ISBN,rsHeadID,unitPrice,count,totalPrice,realDiscount,realPrice,dateTime) VALUES(@rsMonomerID,@bookNum,@ISBN,@rsHeadID,@unitPrice,@count,@totalPrice,@realDiscount,@realPrice,@dateTime)";
            string[] param = { "@rsMonomerID", "@bookNum", "@ISBN", "@rsHeadID", "@unitPrice", "@count", "@totalPrice", "@realDiscount", "@realPrice", "@dateTime" };
            object[] values = { rm.RsMonomerID,rm.BookNum,rm.Isbn,rm.RsHeadID,rm.UnitPrice,rm.Count,rm.TotalPrice,rm.RealDiscount,rm.RealPrice,rm.Time };
            int row = db.ExecuteNoneQuery(sql, param, values);
            return row;
        }
    }
}
