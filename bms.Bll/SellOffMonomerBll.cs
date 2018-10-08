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
        public DataSet getDisCount(string saleTaskId)
        {
            SaleTask st = new SaleTask();
            DataSet ds = dao.getDisCount(saleTaskId);
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
    }
}
