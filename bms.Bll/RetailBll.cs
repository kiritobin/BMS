using bms.Dao;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class RetailBll
    {
        RetailDao dao = new RetailDao();
        /// <summary>
        /// 获取该销退单头下所有的单据号和制单时间
        /// </summary>
        /// <returns></returns>
        public DataSet getAllTime(int state)
        {
            DataSet ds = dao.getAllTime(state);
            return ds;
        }

        /// <summary>
        /// 通过书号查看是否存在在单头中
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public Result selectByBookNum(string bookNum, string retailHeadId)
        {
            int row = dao.selectByBookNum(bookNum, retailHeadId);
            if (row > 0)
            {
                return Result.记录存在;
            }
            else
            {
                return Result.记录不存在;
            }
        }

        /// <summary>
        /// 零售单头添加
        /// </summary>
        /// <param name="salehead">销售单头实体</param>
        /// <returns>返回结果</returns>
        public Result InsertRetail(SaleHead salehead)
        {
            int row = dao.InsertRetail(salehead);
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
        /// 零售单体添加
        /// </summary>
        /// <param name="salemonomer">零售单体实体</param>
        /// <returns>返回结果</returns>
        public Result InsertRetail(SaleMonomer salemonomer)
        {
            int row = dao.InsertRetail(salemonomer);
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
        /// 根据单头ID查询零售单头信息
        /// </summary>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>受影响行数</returns>
        public SaleHead GetHead(string retailHeadId)
        {
            return dao.GetHead(retailHeadId);
        }

        /// <summary>
        /// 查询单头下的所有单体零售单体
        /// </summary>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>受影响行数</returns>
        public DataSet GetRetail(string retailHeadId)
        {
            DataSet ds = dao.GetRetail(retailHeadId);
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                return ds;
            }
        }

        /// <summary>
        /// 查询单头下的状态
        /// </summary>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>数据集</returns>
        public int GetRetailType(string retailHeadId)
        {
            return dao.GetRetailType(retailHeadId);
        }

        /// <summary>
        /// 查询零售单体
        /// </summary>
        /// <param name="retailMonomerId">零售单体ID</param>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>受影响行数</returns>
        public SaleMonomer GetMonomer(int retailMonomerId, string retailHeadId)
        {
            return dao.GetMonomer(retailMonomerId, retailHeadId);
        }

        /// <summary>
        /// 更新零售折扣
        /// </summary>
        /// <param name="realDiscount">折扣</param>
        /// <param name="realPrice">实洋</param>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>受影响行数</returns>
        public Result UpdateDiscount(double realDiscount, double realPrice, string retailHeadId)
        {
            int row = dao.UpdateDiscount(realDiscount, realPrice, retailHeadId);
            if (row > 0)
            {
                return Result.更新成功;
            }
            else
            {
                return Result.更新失败;
            }
        }

        /// <summary>
        /// 更新零售单头实洋
        /// </summary>
        /// <param name="realPrice">实洋</param>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>受影响行数</returns>
        public Result UpdateHeadReal(double allRealPrice, string retailHeadId)
        {
            int row = dao.UpdateHeadReal(allRealPrice, retailHeadId);
            if (row > 0)
            {
                return Result.更新成功;
            }
            else
            {
                return Result.更新失败;
            }
        }

        /// <summary>
        /// 更新零售数量
        /// </summary>
        /// <param name="sale">零售实体对象</param>
        /// <returns>受影响行数</returns>
        public Result UpdateNumber(SaleMonomer sale)
        {
            int row = dao.UpdateNumber(sale);
            if (row > 0)
            {
                return Result.更新成功;
            }
            else
            {
                return Result.更新失败;
            }
        }

        /// <summary>
        /// 更新零售数量
        /// </summary>
        /// <param name="sale">零售实体对象</param>
        /// <returns>受影响行数</returns>
        public Result UpdateHeadNumber(SaleHead sale)
        {
            int row = dao.UpdateHeadNumber(sale);
            if (row > 0)
            {
                return Result.更新成功;
            }
            else
            {
                return Result.更新失败;
            }
        }

        /// <summary>
        /// 单据完成，修改type
        /// </summary>
        /// <param name="headId">单头Id</param>
        /// <returns>受影响行数</returns>
        public Result updateType(string headId,User user, string payType)
        {
            int row = dao.updateType(headId,user,payType);
            if (row > 0)
            {
                return Result.更新成功;
            }
            else
            {
                return Result.更新失败;
            }
        }

        /// <summary>
        /// 删除单体信息
        /// </summary>
        /// <param name="retailMonomerId">零售单体ID</param>
        /// <returns></returns>
        public Result delete(int retailMonomerId, string retailHeadId)
        {
            int row = dao.delete(retailMonomerId, retailHeadId);
            if (row > 0)
            {
                return Result.删除成功;
            }
            else
            {
                return Result.删除失败;
            }
        }

        /// <summary>
        /// 根据ISBN查找书号，单价，折扣
        /// </summary>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public DataSet SelectByIsbn(string ISBN, string retailHeadId)
        {
            return dao.SelectByIsbn(ISBN, retailHeadId);
        }
        /// <summary>
        /// 根据书号查找isbn，单价，折扣
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public SaleMonomer SelectBookNum(string bookNum, string retailHeadId)
        {
            return dao.SelectBookNum(bookNum, retailHeadId);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tablebuilder"></param>
        /// <param name="totalCount"></param>
        /// <param name="intPageCount"></param>
        /// <returns></returns>
        public DataSet selectBypage(TableBuilder tablebuilder, out int totalCount, out int intPageCount)
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
        /// 零售统计
        /// </summary>
        /// <returns></returns>
        public DataSet GroupRetail(DateTime startTime,DateTime endTime,string regionName)
        {
            DataSet ds = dao.GroupRetail(startTime,endTime,regionName);
            return ds;
        }

        /// <summary>
        /// 获取书籍种类
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="type">分组类型</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public int getkindsGroupBy(string strWhere, string type, string time)
        {
            return dao.getkindsGroupBy(strWhere, type, time);
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
            if (groupbyType == "payment")
            {
                name = "支付方式";
            }
            else if (groupbyType == "regionName")
            {
                name = "组织名称";
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
            excel.Columns.Add("折扣");
            excel.Columns.Add("交易日期");
            excel.Columns.Add("收银员");
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
        /// <summary>
        /// 导出页面上查询到的结果
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="groupbyType">groupby条件</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public DataTable exportAll(string strWhere, string groupbyType, string time)
        {
            return dao.exportAll(strWhere, groupbyType, time);
        }

        /// <summary>
        /// 获取收银员
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
            excel.Columns.Add("折扣");
            excel.Columns.Add("供应商");
            excel.Columns.Add("交易时间");
            excel.Columns.Add("收银员");
            excel.Columns.Add("支付方式");
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
                    excel.Rows.Add(row[0], row[1], bookName, row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14], row[15]);
                }
            }
            return excel;
        }
        /// <summary>
        /// 根据ISBN查找书号，单价，折扣
        /// </summary>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public DataTable SelectByIsbn(string ISBN)
        {
            return dao.SelectByIsbn(ISBN);
        }

        /// <summary>
        /// 根据书号查找ISBN，单价，折扣
        /// </summary>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public DataSet SelectByBookNum(string bookNum)
        {
            return dao.SelectByBookNum(bookNum);
        }

        /// <summary>
        /// 零售明细打印统计
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable census(string strWhere, string type)
        {
            return dao.census(strWhere, type);
        }

        /// <summary>
        /// 小程序零售单头添加
        /// </summary>
        /// <param name="salehead">销售单头实体</param>
        /// <returns>返回结果</returns>
        public Result Insert(SaleHead salehead)
        {
            int row = dao.Insert(salehead);
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
        /// 小程序获取用户单头信息
        /// </summary>
        /// <param name="openid">用户唯一标识</param>
        /// <returns></returns>
        public DataSet selectHead(string openid)
        {
            return dao.selectHead(openid);
        }

        /// <summary>
        /// 小程序删除单头
        /// </summary>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>受影响行数</returns>
        public Result UpdateDel(string retailHeadId)
        {
            int row = dao.UpdateDel(retailHeadId);
            Result result = Result.删除失败;
            if (row > 0)
            {
                result = Result.删除成功;
            }
            return result;
        }
    }
}
