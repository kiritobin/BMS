using bms.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    public class SalesDetailsBll
    {
        SalesDetailsDao dao = new SalesDetailsDao();
        /// <summary>
        /// 获取采集人
        /// </summary>
        /// <param name="strWhere">筛选条件</param>
        /// <returns></returns>
        public DataSet getUser(string strWhere)
        {
            return dao.getUser(strWhere);
        }

        /// <summary>
        /// 导出成Excel表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>返回一个DataTable的选题记录集合</returns>
        public DataTable ExportExcel(string strWhere, string type)
        {
            return dao.ExportExcel(strWhere, type);
        }
    }
}
