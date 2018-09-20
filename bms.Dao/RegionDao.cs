using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.DBHelper;
using System.Data;

namespace bms.Dao
{
    public class RegionDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取所有地区信息
        /// </summary>
        /// <returns></returns>
        public DataSet select()
        {
            string cmdText = "select * from T_Region";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            if(ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加分公司
        /// </summary>
        /// <param name="regionName">分公司名称</param>
        /// <returns></returns>
        public int insert(string regionName)
        {
            string cmdText = "insert into T_Region(regionName) values(@regionName)";
            string[] param = { "@regionName" };
            object[] values = { regionName };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 删除分公司
        /// </summary>
        /// <param name="regionId">分公司id</param>
        /// <returns></returns>
        public int delete(int regionId)
        {
            string cmdText = "delete from T_Region where regionId = @regionId";
            string[] param = { "@regionId" };
            object[] values = { regionId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
    }
}
