﻿using bms.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class LibraryCollectionDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取所有客户馆藏数据的ISBN，客户id
        /// </summary>
        /// <returns></returns>
        public DataTable Select(string customerId)
        {
            string comText = "select ISBN,bookName,price,collectionNum,customerId from T_LibraryCollection where customerId=@customerId";
            string[] param = { "@customerId" };
            string[] values = { customerId.ToString() };
            DataSet ds = db.FillDataSet(comText, param, values);
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
        /// 查询馆藏数据是否存在
        /// </summary>
        /// <param name="customerId">客户id</param>
        /// <param name="bookNum">ISBN</param>
        /// <returns>受影响行数</returns>
        public int Selectbook(string customerId, string ISBN)
        {
            string comText = "select count(ISBN) from T_LibraryCollection where customerId=@customerId and ISBN=@ISBN";
            string[] param = { "@customerId", "@ISBN" };
            string[] values = { customerId, ISBN };
            int rows = int.Parse(db.ExecuteScalar(comText, param, values).ToString());
            return rows;
        }

        /// <summary>
        /// replace批量导入数据库
        /// </summary>
        /// <param name="strSql">导入数据字符串</param>
        /// <returns></returns>
        public int Replace(string strSql)
        {
            int row = 0;
            string cmdText = "replace into T_LibraryCollection(ISBN,bookName,price,collectionNum,customerId) SELECT ISBN,bookName,price values" + strSql;
            row = db.ExecuteNoneQuery(cmdText, null, null);
            return row;
        }

        /// <summary>
        /// 通过地区获取客户姓名和ID
        /// </summary>
        /// <returns></returns>
        public DataSet getCustomerByReg(int regionId)
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select customerName,customerID from T_Customer where regionId=@regionId";
            string[] param = { "@regionId" };
            string[] values = { regionId.ToString() };
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
        /// <summary>
        /// 查询客户数据
        /// </summary>
        /// <returns></returns>
        public DataSet getCustomer()
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select customerName,customerID from T_Customer where deleteState=0 order by convert(customerName using gbk) collate gbk_chinese_ci";
            DataSet ds = db.FillDataSet(comText, null, null);
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
        /// 删除馆藏 
        /// </summary>
        /// <param name="libraryId"></param>
        /// <returns></returns>
        public int Delete(int libraryId)
        {
            string cmdText = "delete from T_LibraryCollection where libraryId=@libraryId";
            String[] param = { "@libraryId" };
            String[] values = { libraryId.ToString() };
            return db.ExecuteNoneQuery(cmdText, param, values);
        }
        public int deleteByCus(int customId)
        {
            string cmdText = "delete from T_LibraryCollection where customerId=@customerId";
            String[] param = { "@customerId" };
            String[] values = { customId.ToString() };
            return db.ExecuteNoneQuery(cmdText, param, values);
        }
    }
}
