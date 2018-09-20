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
        /// <summary>
        /// 获取所有客户馆藏数据的ISBN，客户id
        /// </summary>
        /// <returns></returns>
        public DataTable Select(int customerId)
        {
            MySqlHelp db = new MySqlHelp();
            string comText = "select ISBN as ISBN,customerId as 客户ID from T_LibraryCollection where customerId=@customerId";
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
