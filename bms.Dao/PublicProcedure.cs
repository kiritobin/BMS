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
            strSql.Append("sp_page ");
            MySqlParameter[] values = {
                new MySqlParameter("@strColumnlist", SqlDbType.VarChar),
                new MySqlParameter("@strTable", SqlDbType.VarChar),
                new MySqlParameter("@strWhere", SqlDbType.VarChar),
                new MySqlParameter("@orderBy", SqlDbType.VarChar),
                new MySqlParameter("@intPageNum", SqlDbType.Int),
                new MySqlParameter("@intPageSize", SqlDbType.Int),
                new MySqlParameter("@totalCount", SqlDbType.Int),
                new MySqlParameter("@intPageCount", SqlDbType.Int)
            };
            values[0].Value = tablebuilder.StrColumnlist;
            values[1].Value = tablebuilder.StrTable;
            values[2].Value = tablebuilder.StrWhere;
            values[3].Value = tablebuilder.OrderBy;
            values[4].Value = tablebuilder.IntPageNum;
            values[5].Value = tablebuilder.IntPageSize;
            values[6].Direction = ParameterDirection.Output;
            values[7].Direction = ParameterDirection.Output;

            DataSet ds = db.FillDataSetBySP(strSql.ToString(), values);
            totalCount = Convert.ToInt32(values[6].Value);
            intPageCount = Convert.ToInt32(values[7].Value);
            return ds;
        }
    }
}
