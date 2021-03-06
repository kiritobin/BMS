﻿using bms.DBHelper;
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
        /// 添加基础数据
        /// </summary>
        /// <param name="basic">基础数据实体对象</param>
        /// <returns></returns>
        public int Insert(BookBasicData basic)
        {
            string cmdText = "insert into T_BookBasicData(bookNum,ISBN,bookName,supplier,publishTime,price,catalog,author,remarks,dentification,remarksOne,remarksTwo,remarksThree) values(@bookNum,@ISBN,@bookName,@supplier,@publishTime,@price,@catalog,@author,@remarks,@dentification,@remarksOne,@remarksTwo,@remarksThree)";
            String[] param = { "@bookNum", "@ISBN", "@bookName", "@supplier", "@publishTime", "@price", "@catalog", "@author", "@remarks", "@dentification", "@remarksOne", "@remarksTwo", "@remarksThree" };
            object[] values = { basic.BookNum, basic.Isbn, basic.BookName, basic.Publisher, basic.PublishTime, basic.Price, basic.Catalog, basic.Author, basic.Remarks, basic.Dentification, basic.Remarks1, basic.Remarks2, basic.Remarks3 };
            return db.ExecuteNoneQuery(cmdText, param, values);
        }


        /// <summary>
        /// 获取所有书本基础数据的ISBN，单价，书名
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select ISBN,price,bookName,supplier,author,remarks from T_BookBasicData order by convert(supplier using gbk) collate gbk_chinese_ci";
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
        /// 取行数
        /// </summary>
        /// <returns></returns>
        public int SelectCount()
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select count(bookNum) from T_BookBasicData";
            int count = Convert.ToInt32(db.ExecuteScalar(comText, null, null));
            return count;
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
            string comText = "select bookNum,price,author from T_BookBasicData where ISBN=@ISBN and bookName=@bookName ";
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
        /// 通过书号 获取单价 进货折扣
        /// </summary>
        /// <param name="booknum"></param>
        /// <returns></returns>
        public DataTable getBookNumByNum(string booknum)
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select bookNum,price,author from T_BookBasicData where bookNum=@bookNum";
            string[] parames = {"@bookNum" };
            object[] value = { booknum };
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

        /// <summary>
        /// 查询供应商
        /// </summary>
        /// <returns></returns>
        public DataTable selectSupplier()
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select distinct supplier from T_BookBasicData order by convert(supplier using gbk) collate gbk_chinese_ci";
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
        /// 获取制单员
        /// </summary>
        /// <returns></returns>
        public DataTable selectZdy()
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select distinct userName from v_stockstatistics order by convert(userName using gbk) collate gbk_chinese_ci";
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
        /// 获取来源组织/收货组织
        /// </summary>
        /// <returns></returns>
        public DataTable selectSource()
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select distinct regionName from v_stockstatistics order by convert(regionName using gbk) collate gbk_chinese_ci";
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
        /// 基础数据导出
        /// </summary>
        /// <returns></returns>
        public DataTable excelBook(string search)
        {
            if (search == "")
            {
                String sql = @"select bookNum as '书号',ISBN,bookName as '书名',publishTime as '出版日期',price as '定价',supplier as '出版社',catalog as '预收数量',author as '进货折扣',remarks as '销售折扣',dentification as '备注' from t_bookbasicdata";
                DataSet ds = db.FillDataSet(sql, null, null);
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
            else
            {
                String sql = @"select bookNum as '书号',ISBN,bookName as '书名',publishTime as '出版日期',price as '定价',supplier as '出版社',catalog as '预收数量',author as '进货折扣',remarks as '销售折扣',dentification as '备注' from t_bookbasicdata where " +search;
                DataSet ds = db.FillDataSet(sql, null, null);
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
        }
    }
}
