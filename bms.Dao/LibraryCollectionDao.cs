using bms.DBHelper;
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
            string comText = "select customerName,customerID from T_Customer";
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
    }
}
