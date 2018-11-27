using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.DBHelper;
using System.Data;
using bms.Model;

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
            string cmdText = "select regionId,regionName from T_Region where deleteState=0 order by convert(regionName using gbk) collate gbk_chinese_ci";
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
        /// 根据地区ID获取地区信息
        /// </summary>
        /// <returns></returns>
        public string selectById(int regionId)
        {
            string cmdText = "select regionName from T_Region where deleteState=0 and regionId=@regionId";
            string[] param = { "@regionId" };
            object[] value = { regionId };
            DataSet ds = db.FillDataSet(cmdText, param, value);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["regionName"].ToString();
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
        /// 删除分公司(根据id来将删除状态改为1)
        /// </summary>
        /// <param name="regionId">分公司id</param>
        /// <returns></returns>
        public int delete(int regionId)
        {
            string cmdText = "update T_Region set deleteState=1 where regionId = @regionId";
            string[] param = { "@regionId" };
            object[] values = { regionId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 查找是否组织名称是否存在
        /// </summary>
        /// <param name="regionName">组织名称</param>
        /// <returns></returns>
        public int isExit(string regionName)
        {
            string cmdText = "select count(regionName) from T_Region where regionName=@regionName";
            string[] param = { "@regionName" };
            object[] values = { regionName };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            return row;
        }
        /// <summary>
        /// 更新组织
        /// </summary>
        /// <param name="region">组织实体</param>
        /// <returns>返回受影响的行数</returns>
        public int Update(Region region)
        {
            string cmdText = "update T_Region set regionName=@regionName where regionId=@regionId";
            string[] param = { "@regionName", "@regionId" };
            object[] values = { region.RegionName, region.RegionId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
    }
}
