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
        /// <summary>
        /// 通过ISBN和书名获取书号 单价 进货折扣
        /// </summary>
        /// <param name="ISBN"></param>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public DataTable getBookNum(string ISBN,string bookName)
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select bookNum,price,author from T_BookBasicData where ISBN=@ISBN and bookName=@bookName";
            string[] parames = { "@ISBN" , "@bookName" };
            object[] value = { ISBN,bookName };
            DataSet ds = db.FillDataSet(comText, parames, value);
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

        /// <summary>
        /// 根据书号查找isbn，单价，折扣
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public BookBasicData SelectById(string bookNum)
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select ISBN,price,bookName,supplier,remarks,author from T_BookBasicData where bookNum=@bookNum";
            string[] param = { "@bookNum" };
            object[] values = { bookNum };
            DataSet ds = db.FillDataSet(comText, param, values);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                string isbn = ds.Tables[0].Rows[0]["isbn"].ToString();
                string price = ds.Tables[0].Rows[0]["price"].ToString();
                string remarks = ds.Tables[0].Rows[0]["remarks"].ToString();
                string bookName = ds.Tables[0].Rows[0]["bookName"].ToString();
                string supplier = ds.Tables[0].Rows[0]["supplier"].ToString();
                string author = ds.Tables[0].Rows[0]["author"].ToString();
                BookBasicData bookBasic = new BookBasicData();
                bookBasic.Isbn = isbn;
                bookBasic.Price = Convert.ToDouble(price);
                bookBasic.Remarks = remarks;
                bookBasic.BookName = bookName;
                bookBasic.Publisher = supplier;
                bookBasic.Author = author;
                return bookBasic;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 根据ISBN查找书号，单价，折扣
        /// </summary>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public DataSet SelectByIsbn(string ISBN)
        {
            MySqlHelp db = new MySqlHelp();
            string comTexts = "select count(bookNum) from T_BookBasicData where ISBN=@ISBN";
            string[] parames = { "@ISBN" };
            object[] value = { ISBN };
            int row = Convert.ToInt32(db.ExecuteScalar(comTexts, parames, value));
            if (row == 0)
            {
                return null;
            }
            else
            {
                string comText = "select bookNum,ISBN,price,author,bookName,supplier from T_BookBasicData where ISBN=@ISBN";
                string[] param = { "@ISBN" };
                object[] values = { ISBN };
                DataSet ds = db.FillDataSet(comText, param, values);
                if (ds != null || ds.Tables[0].Rows.Count > 0)
                {
                    return ds;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 删除基础数据
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public int Delete(string bookNum)
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
