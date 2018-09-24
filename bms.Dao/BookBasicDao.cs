using bms.DBHelper;
using bms.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class BookBasicDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取所有书本基础数据的ISBN，单价，书名
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select ISBN,price,bookName from T_BookBasicData";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                return dt;
            }
            else
            {
                return null;
            }
        }
        public int Delete(long bookNum)
        {
            string cmdText = "delete from T_BookBasicData where bookNum=@bookNum";
            String[] param = { "@bookNum" };
            String[] values = { bookNum.ToString() };
            return db.ExecuteNoneQuery(cmdText, param, values);
        }
        /// <summary>
        /// 取得最新书号
        /// </summary>
        /// <returns></returns>
        public BookBasicData getBookNum()
        {
            string sql = "select bookNum from T_NewBook";
            BookBasicData bookBasic = new BookBasicData();
            MySqlDataReader reader = db.ExecuteReader(sql, null, null);
            while (reader.Read())
            {
                bookBasic.NewBookNum = reader.GetString(0);
            }
            reader.Close();
            return bookBasic; ;
        }
        /// <summary>
        /// 更新最新书号
        /// </summary>
        /// <param name="bookNum"></param>
        /// <returns></returns>
        public int updateBookNum(string bookNum)
        {
            string cmdText = "update T_NewBook set bookNum=@bookNum";
            String[] param = { "@bookNum" };
            String[] values = { bookNum };
            return db.ExecuteNoneQuery(cmdText, param, values);
        }
    }
}
