using bms.DBHelper;
using bms.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
        /// <summary>
        /// 实现关联表的插入
        /// </summary>
        /// <param name="tabInsert">插入实体</param>
        /// <returns></returns>
        public int InsertManyTable(TableInsertion tabInsert,out int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("sp_Insert");
            MySqlParameter[] values =
            {
                new MySqlParameter("@inRegionName",MySqlDbType.VarChar),
                new MySqlParameter("@inShelvesName",MySqlDbType.VarChar),
                new MySqlParameter("@count",MySqlDbType.Int32)
            };
            values[0].Value = tabInsert.InRegionName;
            values[1].Value = tabInsert.InShelvesName;
            values[2].Direction = ParameterDirection.Output;
            ArrayList array = db.ExecuteSp(strSql.ToString(), values);
            count = Convert.ToInt32(values[2].Value);
            //int outNum = int.Parse(array.ToString());
            return count;
        }

        /// <summary>
        /// 根据索引和pagesize返回记录
        /// </summary>
        /// <param name="dt">记录集 DataTable</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="pagesize">一页的记录数</param>
        /// <returns></returns>
        public DataTable SplitDataTable(DataTable dt, int PageIndex, int PageSize)
        {
            if (dt == null)
            {
                return null;
            }
            if (PageIndex == 0)
                return dt;
            DataTable newdt = dt.Clone();
            //newdt.Clear();
            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }

            return newdt;
        }
    }
}
