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
        /// 查询所有功能
        /// </summary>
        /// <returns>返回查询的数据表</returns>
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
