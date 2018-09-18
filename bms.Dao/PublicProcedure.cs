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
    public class PublicProcedure
    {
        private MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 分页存储过程
        /// </summary>
        /// <param name="tablebuilder">分页实体</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="intPageCount">总页数</param>
        /// <returns>DataSet</returns>
        public DataSet SelectBypage(TableBuilder tablebuilder,out int totalCount, out int intPageCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("sp_page");
            MySqlParameter[] values = {
                new MySqlParameter("@strColumnlist", MySqlDbType.VarChar),
                new MySqlParameter("@strTable", MySqlDbType.VarChar),
                new MySqlParameter("@strWhere", MySqlDbType.VarChar),
                new MySqlParameter("@orderBy", MySqlDbType.VarChar),
                new MySqlParameter("@intPageNum", MySqlDbType.Int32),
                new MySqlParameter("@intPageSize", MySqlDbType.Int32),
                new MySqlParameter("@totalCount", MySqlDbType.Int32),
                new MySqlParameter("@intPageCount", MySqlDbType.Int32)
            };
            values[0].Value = tablebuilder.StrColumnlist;
            values[1].Value = tablebuilder.StrTable;
            values[2].Value = tablebuilder.StrWhere;
            values[3].Value = tablebuilder.OrderBy;
            values[4].Value = tablebuilder.IntPageNum;
            values[5].Value = tablebuilder.IntPageSize;
            values[6].Direction = ParameterDirection.Output;
            values[7].Direction = ParameterDirection.Output;

            DataSet ds = db.FillDataSetBySP(strSql.ToString(),values);
            totalCount = Convert.ToInt32(values[6].Value);
            intPageCount = Convert.ToInt32(values[7].Value);
            return ds;
        }

        /// <summary>
        /// 判断在另外一张表中是否有数据
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="primarykeyname">主键列</param>
        /// <param name="primarykey">主键参数</param>
        /// <returns></returns>
        public int isDelete(string table, string primarykeyname, string primarykey)
        {
            String cmdText = string.Format(" select count(*) as count from {0} where {1} = '{2}'", table, primarykeyname, primarykey);
            string[] param = { };
            object[] values = { };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (int.Parse(ds.Tables[0].Rows[0]["count"].ToString()) > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
