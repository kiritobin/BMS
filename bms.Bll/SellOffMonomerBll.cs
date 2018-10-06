using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.Model;
using bms.Dao;
using System.Data;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class SellOffMonomerBll
    {
        SellOffMonomerDao dao = new SellOffMonomerDao();
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tablebuilder"></param>
        /// <param name="totalCount"></param>
        /// <param name="intPageCount"></param>
        /// <returns></returns>
        public DataSet selecByPage(TableBuilder tablebuilder, out int totalCount, out int intPageCount)
        {
            PublicProcedure procedure = new PublicProcedure();
            DataSet ds = procedure.SelectBypage(tablebuilder, out totalCount, out intPageCount);
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
        /// 获取销退单体信息
        /// </summary>
        /// <returns></returns>
        public DataSet Select()
        {
            DataSet ds = dao.Select();
            return ds;
        }
        /// <summary>
        /// 根据ISBN号查询书籍基础数据
        /// </summary>
        /// <param name="ISBN"></param>
        /// <returns></returns>
        public DataSet SelectByISBN(string ISBN)
        {
            DataSet ds = dao.SelectByISBN(ISBN);

            if (ds.Tables[0].Rows.Count > 0)
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
