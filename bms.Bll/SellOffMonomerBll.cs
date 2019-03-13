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
        public DataSet Select(string sellOffHeadId)
        {
            DataSet ds = dao.Select(sellOffHeadId);
            return ds;
        }
        /// <summary>
        /// 通过书号查询销售单中是否有此数据
        /// </summary>
        /// <param name="bookNum"></param>
        /// <returns></returns>
        public int getBookCount(string bookNum)
        {
            int row = dao.getBookCount(bookNum);
            return row;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        public Result Insert(SellOffMonomer sm)
        {
            int row = dao.Insert(sm);
            if (row > 0)
            {
                return Result.添加成功;
            }
            else
            {
                return Result.添加失败;
            }
        }
        /// <summary>
        /// 通过单头Id获取单体数量
        /// </summary>
        /// <param name="sellOffHeaId"></param>
        /// <returns></returns>
        public int GetCount(string sellOffHeaId)
        {
            int row = dao.GetCount(sellOffHeaId);
            return row;
        }
        /// <summary>
        /// 通过销退单头Id获取销售任务Id
        /// </summary>
        /// <param name="sellOffHeadId"></param>
        /// <returns></returns>
        public DataSet getSaleTask(string sellOffHeadId)
        {
            SellOffHead sh = new SellOffHead();
            DataSet ds = dao.getSaleTask(sellOffHeadId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 通过销售任务Id获取默认折扣
        /// </summary>
        /// <param name="saleTaskId"></param>
        /// <returns></returns>
        public DataSet getDisCount(string saleTaskId,string bookNum)
        {
            SaleTask st = new SaleTask();
            DataSet ds = dao.getDisCount(saleTaskId,bookNum);
            return ds;
        }
        /// <summary>
        /// 根据书号到销售单体视图获取信息
        /// </summary>
        /// <param name="bookNum"></param>
        /// <returns></returns>
        public DataSet selctByBookNum(string bookNum,string saleTaskId)
        {
            DataSet ds = dao.selctByBookNum(bookNum, saleTaskId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            return null;
        }
        /// <summary>
        /// 通过书号和单头Id查询单体表中相应的书籍数量
        /// </summary>
        /// <param name="bookNum"></param>
        /// <param name="sellOffHeadId"></param>
        /// <returns></returns>
        public DataSet selecctSm(string bookNum, string sellOffHeadId)
        {
            DataSet ds = dao.selecctSm(bookNum, sellOffHeadId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取该单头的相应统计
        /// </summary>
        /// <param name="sellOffHeadId"></param>
        /// <returns></returns>
        public DataSet getAllNum(string sellOffHeadId)
        {
            DataSet ds = dao.getAllNum(sellOffHeadId);
            return ds;
        }
        /// <summary>
        /// 获取零售排行
        /// </summary>
        /// <returns></returns>
        public DataSet getRetailRank(DateTime startTime,DateTime endTime,string regionName)
        {
            DataSet ds = dao.getRetailRank(startTime,endTime,regionName);
            return ds;
        }

        public DataSet searchSalesDetailCopy(string saletaskId, string saleheadId)
        {
            return dao.searchSalesDetailCopy(saletaskId, saleheadId);
        }
        public DataSet searchSalesDetail(string saletaskId, string saleheadId)
        {
            return dao.searchSalesDetail(saletaskId, saleheadId);
        }
        /// <summary>
        /// 根据查询条件获取当前种类
        /// </summary>
        /// <param name="strWhere">where条件</param>
        /// <param name="type">group条件</param>
        /// <param name="time">时间点</param>
        /// <returns></returns>
        public int getsellOffKinds(string strWhere, string type, string time)
        {
            int row = dao.getsellOffKinds(strWhere, type, time);
            return row;
        }
        /// <summary>
        /// 获取操作员
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet getSellOffOperator(string strWhere)
        {
            DataSet ds = dao.getSellOffOperator(strWhere);
            return ds;
        }
        /// <summary>
        /// 导出页面上查询到的结果
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="groupbyType">groupby条件</param>
        /// <param name="state">状态</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public DataTable exportAll(string strWhere, string groupbyType, string time)
        {
            return dao.exportAll(strWhere, groupbyType, time);
        }
        /// <summary>
        /// 导出明细
        /// </summary>
        /// <param name="groupbyType">groupby方式</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public DataTable exportDel(string groupbyType, string strWhere)
        {
            return dao.exportDel(groupbyType, strWhere);
        }
        public DataTable exportDe(string groupbyType, string strWhere)
        {
            string name = "";
            //所选分组条件如客户 ISBN    书号 书名  定价 数量  码洋 实洋  销折 采集日期    采集人用户名 采集状态（销售单或预采单）			供应商
            if (groupbyType == "supplier")
            {
                name = "供应商名称";
            }
            else if (groupbyType == "regionName")
            {
                name = "组织名称";
            }
            else
            {
                name = "客户名称";
            }
            DataTable excel = new DataTable();
            excel.Columns.Add(name);
            excel.Columns.Add("ISBN");
            excel.Columns.Add("书号");
            excel.Columns.Add("书名");
            excel.Columns.Add("定价");
            excel.Columns.Add("数量");
            excel.Columns.Add("码洋");
            excel.Columns.Add("实洋");
            excel.Columns.Add("销售折扣");
            excel.Columns.Add("销退日期");
            excel.Columns.Add("操作员");
            excel.Columns.Add("供应商");
            excel.Columns.Add("备注");
            excel.Columns.Add("备注1");
            excel.Columns.Add("备注2");
            excel.Columns.Add("备注3");
            DataTable dt = dao.exportDel(groupbyType, strWhere);
            if (dt!=null)
            {
                DataRowCollection count = dt.Rows;
                foreach (DataRow row in count)
                {
                    string bookName = ToDBC(row[3].ToString());
                    excel.Rows.Add(row[0], row[1], row[2], bookName, row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14], row[15]);
                }
            }
            return excel;
        }
        /// <summary>
        /// 销退明细页面导出Excel
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="type">分组条件</param>
        /// <returns></returns>
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
            excel.Columns.Add("销退日期");
            excel.Columns.Add("操作员");
            excel.Columns.Add("备注");
            excel.Columns.Add("备注1");
            excel.Columns.Add("备注2");
            excel.Columns.Add("备注3");
            DataTable dt = dao.ExportExcel(strWhere, type);
            if (dt!=null)
            {
                DataRowCollection count = dt.Rows;
                foreach (DataRow row in count)
                {
                    string bookName = ToDBC(row[2].ToString());
                    excel.Rows.Add(row[0], row[1], bookName, row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14]);
                }
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
