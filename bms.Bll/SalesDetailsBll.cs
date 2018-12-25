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
        public DataTable ExportExcels(string strWhere, string type)
        {
            DataTable excel = new DataTable();
            excel.Columns.Add("ISBN");
            excel.Columns.Add("书号");
            excel.Columns.Add("书名");
            excel.Columns.Add("单价");
            excel.Columns.Add("数量");
            excel.Columns.Add("码洋");
            excel.Columns.Add("实洋");
            excel.Columns.Add("销售折扣");
            excel.Columns.Add("供应商");
            excel.Columns.Add("采集时间");
            excel.Columns.Add("采集人");
            excel.Columns.Add("备注");
            excel.Columns.Add("备注1");
            excel.Columns.Add("备注2");
            excel.Columns.Add("备注3");
            excel.Columns.Add("采集状态");
            DataTable dt = dao.ExportExcel(strWhere, type);
            DataRowCollection count = dt.Rows;
            foreach (DataRow row in count)
            {
                string bookName = ToDBC(row[2].ToString());
                excel.Rows.Add(row[0], row[1], bookName,row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10],row[11], row[12], row[13], row[14], row[15]);
            }
            return excel;
        }
        /// <summary>
        /// 全转半
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] array = input.ToCharArray();
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == 12288)
                {
                    array[i] = (char)32;
                    continue;
                }
                if (array[i] > 65280 && array[i] < 65375)
                {
                    array[i] = (char)(array[i] - 65248);
                }
            }
            return new string(array);
        }
    }
}
