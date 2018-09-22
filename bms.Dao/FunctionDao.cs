using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class FunctionDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 添加功能
        /// </summary>
        /// <param name="function">功能实体</param>
        /// <returns>受影响行数</returns>
        public int Insert(Function function)
        {
            string cmdText = "insert into T_Function(functionName) values(@functionName)";
            String[] param = { "@functionName" };
            String[] values = { function.FunctionName };
            return db.ExecuteNoneQuery(cmdText, param, values);
        }
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="functionId">功能ID</param>
        /// <returns>受影响行数</returns>
        public int Delete(int functionId)
        {
            string cmdText = "delete from T_Function where functionId=@functionId";
            String[] param = { "@functionId" };
            String[] values = { functionId.ToString() };
            return db.ExecuteNoneQuery(cmdText, param, values);
        }
        /// <summary>
        /// 根据角色id查询功能数据
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>返回查询到的表格数据</returns>
        public DataSet SelectByRoleId(int roleId)
        {
            string cmdText = "select functionName,functionId from V_Permission where roleId=@roleId";
            string[] param = { "@roleId" };
            object[] values = { roleId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
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
        /// 查询所有功能信息
        /// </summary>
        /// <returns></returns>
        public DataSet Select()
        {
            string cmdText = "select functionId,functionName from T_Function";
            DataSet ds = db.FillDataSet(cmdText, null, null);
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
