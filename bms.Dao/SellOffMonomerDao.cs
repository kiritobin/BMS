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
    }
}
