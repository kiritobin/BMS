using bms.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class UserDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public DataSet select()
        {
            string comText = "select userId,userName,regionName,roleName from V_User";
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
        /// 根据地区id获取用户信息
        /// </summary>
        /// <param name="regionId">地区id</param>
        /// <returns></returns>
        public DataSet selectByRegion(int regionId)
        {
            string comText = "select userId,userName,regionName,roleName from V_User where regionId=@regionId";
            string[] param = { "@regionId" };
            object[] values = { regionId };
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
