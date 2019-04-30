using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    using Dao;
    using Model;
    using System.Data;
    using Result = Enums.OpResult;
    public class SaleMonomerBll
    {

        public DataSet Select()
        {
            return SaleMonomerdao.Select();
        }

        SaleMonomerDao SaleMonomerdao = new SaleMonomerDao();
        /// <summary>
        /// 查询该销售单头下是否有单体
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns></returns>
        public int SelectBySaleHeadId(string saleHeadId)
        {
            int count = SaleMonomerdao.SelectBySaleHeadId(saleHeadId);
            if (count == 0)
            {
                return count = 0;
            }
            else
            {
                return count;
            }
        }
        
        /// <summary>
        /// 查询该预采单头下是否有单体
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns></returns>
        public int SelectByPerSaleHeadId(string saleHeadId)
        {
            int count = SaleMonomerdao.SelectByPerSaleHeadId(saleHeadId);
            if (count == 0)
            {
                return count = 0;
            }
            else
            {
                return count;
            }
        }

        /// <summary>
        /// 删除销售单头
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns></returns>
        public Result realDelete(string saleTaskId, string saleHeadId)
        {

            int row = SaleMonomerdao.realDelete(saleTaskId, saleHeadId);
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
        /// 根据销售单头ID和销售任务id获取单体数量
        /// </summary>
        /// <param name="HeadId">销售单头ID</param>
        /// <param name="saletaskId">销售任务id</param>
        /// <returns></returns>
        public int SelectcountbyHeadID(string HeadId, string saletaskId)
        {
            return SaleMonomerdao.SelectcountbyHeadID(HeadId, saletaskId);
        }

        /// <summary>
        /// 微信小程序根据销售单头ID和销售任务id获取单体数量
        /// </summary>
        /// <param name="HeadId">销售单头ID</param>
        /// <param name="saletaskId">销售任务id</param>
        /// <returns></returns>
        public int WeChatSelectcountbyHeadID(string HeadId, string saletaskId)
        {
            return SaleMonomerdao.WeChatSelectcountbyHeadID(HeadId, saletaskId);
        }
        /// <summary>
        /// 根据销售单头ID和销售任务更新销售单头删除状态
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns>0不成功</returns>
        public Result DeleteHead(string saleTaskId, string saleHeadId,string type)
        {
            int row = SaleMonomerdao.DeleteHead(saleTaskId, saleHeadId, type);
            if (row == 0)
            {
                return Result.删除失败;
            }
            else
            {
                return Result.删除成功;
            }
        }

        /// <summary>
        /// 统计品种数
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">销售单头id</param>
        /// <returns></returns>
        public int getkinds(string saleTaskId, string saleHeadId)
        {
            return SaleMonomerdao.getkinds(saleTaskId, saleHeadId);

        }
        /// <summary>
        /// 统计预采品种数
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">销售单头id</param>
        /// <returns></returns>
        public int getperkinds(string saleTaskId, string saleHeadId)
        {
            return SaleMonomerdao.getperkinds(saleTaskId, saleHeadId);

        }

        /// <summary>
        /// 获取书籍种类
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public int getkindsGroupBy(string strWhere, string type, string state, string time)
        {
            return SaleMonomerdao.getkindsGroupBy(strWhere, type, state, time);
        }
        /// <summary>
        /// 根据销售单头ID查询该销售单的状态
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns>状态</returns>
        public int saleheadstate(string saleTaskId, string saleHeadId)
        {
            DataSet ds = SaleMonomerdao.saleheadstate(saleTaskId, saleHeadId);
            int state;
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return state = Convert.ToInt32(ds.Tables[0].Rows[0]["state"].ToString());
            }
            else
            {
                return state = 4;
            }
        }
        //
        /// <summary>
        /// 根据预采销售单头ID查询该销售单的状态
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns>状态</returns>
        public int perSaleheadstate(string saleTaskId, string saleHeadId)
        {
            DataSet ds = SaleMonomerdao.perSaleheadstate(saleTaskId, saleHeadId);
            int state;
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return state = Convert.ToInt32(ds.Tables[0].Rows[0]["state"].ToString());
            }
            else
            {
                return state = 4;
            }
        }


        public DataSet checkStock(string singleHeadId)
        {
            return SaleMonomerdao.checkStock(singleHeadId);
        }
        /// <summary>
        /// 单体添加
        /// </summary>
        /// <param name="salemonomer">单体实体</param>
        /// <returns>返回结果</returns>
        public Result Insert(SaleMonomer salemonomer)
        {
            int row = SaleMonomerdao.Insert(salemonomer);
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
        /// 预采单体添加
        /// </summary>
        /// <param name="salemonomer">单体实体</param>
        /// <returns>返回结果</returns>
        public Result perInsert(SaleMonomer salemonomer)
        {
            int row = SaleMonomerdao.perInsert(salemonomer);
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
        /// 删除单体
        /// </summary>
        /// <param name="saleIdMonomerId">单体id</param>
        /// <param name="saleHeadId">单头id</param>
        /// <returns></returns>
        public Result Delete(string saleIdMonomerId, string saleHeadId)
        {
            int row = SaleMonomerdao.Delete(saleIdMonomerId, saleHeadId);
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
        /// 判断是否关联引用
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="primarykeyname">主键名</param>
        /// <param name="primarykey">主键</param>
        /// <returns></returns>
        public Result IsDelete(string table, string primarykeyname, string primarykey)
        {
            PublicProcedure pp = new PublicProcedure();
            int row = pp.isDelete(table, primarykeyname, primarykey);
            if (row > 0)
            {
                return Result.关联引用;
            }
            else
            {
                return Result.记录不存在;
            }
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
        /// 微信预采删除
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>数据集</returns>
        public Result wechatPerDel(string condition, int state)
        {
            int rows = SaleMonomerdao.wechatPerDel(condition, state);
            if (rows > 0)
            {
                return Result.删除成功;
            }
            else
            {
                return Result.删除失败;

            }
        }


        /// <summary>
        /// 微信汇总
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>数据集</returns>
        public DataSet wechatPerSummary(string condition, int state)
        {
            DataSet ds = SaleMonomerdao.wechatPerSummary(condition, state);
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
        /// 更新销售单体
        /// </summary>
        /// <param name="salemonomer">销售单体实体</param>
        /// <returns></returns>
        public Result Update(SaleMonomer salemonomer)
        {
            int row = SaleMonomerdao.Update(salemonomer);
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
        /// 更新单头
        /// </summary>
        /// <param name="salehead">单头实体</param>
        /// <returns></returns>
        public Result updateHead(SaleHead salehead)
        {
            int row = SaleMonomerdao.updateHead(salehead);
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
        /// 更新团采单头
        /// </summary>
        /// <param name="salehead"></param>
        /// <returns></returns>
        public Result wechatupdateHead(SaleHead salehead)
        {
            int row = SaleMonomerdao.wechatupdateHead(salehead);
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
        /// 更新预采单头
        /// </summary>
        /// <param name="salehead">单头实体</param>
        /// <returns></returns>
        public Result wechatupdatePerHead(SaleHead salehead)
        {
            int row = SaleMonomerdao.wechatupdatePerHead(salehead);
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
        /// 更新销售单头状态
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">销售单头</param>
        /// <param name="state">状态 0新创单据 1采集中 2已完成</param>
        /// <returns>受影响行数</returns>
        public Result updateHeadstate(string saleTaskId, string saleHeadId, int state)
        {
            int row = SaleMonomerdao.updateHeadstate(saleTaskId, saleHeadId, state);
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
        /// 更新预采单头状态
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">销售单头</param>
        /// <param name="state">状态 0新创单据 1采集中 2已完成</param>
        /// <returns>受影响行数</returns>
        public Result updatePerHeadstate(string saleTaskId, string saleHeadId, int state)
        {
            int row = SaleMonomerdao.updatePerHeadstate(saleTaskId, saleHeadId, state);
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
        /// 根据单头获取单体
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns></returns>
        public DataSet SelectMonomers(string saleHeadId)
        {
            return SaleMonomerdao.SelectMonomers(saleHeadId);
        }
        /// <summary>
        /// 判断该书号是否是第一次添加
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public int SelectnumberBysaletask(string saleTaskId, string bookNum)
        {
            return SaleMonomerdao.SelectnumberBysaletask(saleTaskId, bookNum);
        }

        /// <summary>
        /// 通过书号查询在单体中是否存在记录
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="type">单体类型（0：出库，1：入库，2：退货）</param>
        /// <returns></returns>
        public Result SelectBybookNum(string retailHeadId, string bookNum)
        {
            int row = SaleMonomerdao.SelectBybookNum(retailHeadId, bookNum);
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
        /// 获取该书号在该销售任务下的已购数量
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="bookNum">书号</param>
        /// <returns>返回数据集</returns>
        public DataSet SelectCountBybookNum(string saleTaskId, string bookNum)
        {
            return SaleMonomerdao.SelectCountBybookNum(saleTaskId, bookNum);
        }
        /// <summary>
        /// 查询销售单体中的数据统计
        /// </summary>
        /// <returns></returns>
        public DataSet SelectBookRanking(DateTime startTime, DateTime endTime, string regionName, string type)
        {
            DataSet ds = SaleMonomerdao.SelectBookRanking(startTime, endTime, regionName, type);
            return ds;
        }

        /// <summary>
        /// 更新已购数量
        /// </summary>
        /// <param name="alreadyBought">数量</param>
        /// <param name="bookNum">书号</param>
        /// <param name="saleId">销售任务id</param>
        /// <returns>受影响行数</returns>
        public int updateAlreadyBought(int alreadyBought, string bookNum, string saleId)
        {
            return SaleMonomerdao.updateAlreadyBought(alreadyBought, bookNum, saleId);
        }

        /// <summary>
        /// 获取销售单头的状态
        /// </summary>
        /// <param name="saleHeadId">销售单头</param>
        /// <returns>返回销售单头状态</returns>
        public string getsaleHeadState(string saleHeadId, string saleTaskId)
        {
            return SaleMonomerdao.getsaleHeadState(saleHeadId, saleTaskId);
        }
        /// <summary>
        ///根据销售任务id 获取销售单头的状态
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns>返回销售单头状态</returns>
        public string getsaleHeadStatesBysaleTaskId(string saleTaskId)
        {
            return SaleMonomerdao.getsaleHeadStatesBysaleTaskId(saleTaskId);
        }

        /// <summary>
        /// 获取该单头id下的书本数量
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>结果</returns>
        public int getsBookNumberSum(string saleHeadId, string saleId)
        {
            return SaleMonomerdao.getsBookNumberSum(saleHeadId, saleId);
        }
        /// <summary>
        /// 获取该单头id下的码洋
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>结果</returns>
        public double getsBookTotalPrice(string saleHeadId, string saleId)
        {
            return SaleMonomerdao.getsBookTotalPrice(saleHeadId, saleId);
        }
        /// <summary>
        /// 获取该单头id下的实洋
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>结果</returns>
        public double getsBookRealPrice(string saleHeadId, string saleId)
        {
            return SaleMonomerdao.getsBookRealPrice(saleHeadId, saleId);
        }
        /// <summary>
        /// 计算销售单头
        /// </summary>
        /// <param name="saleHeadId">销售单头id</param>
        /// <param name="saleId">销售任务ID</param>
        /// <returns></returns>
        public DataSet calculationSaleHead(string saleHeadId, string saleId)
        {
            DataSet ds = SaleMonomerdao.calculationSaleHead(saleHeadId, saleId);
            string sign = ds.Tables[0].Rows[0]["数量"].ToString();
            if (sign != null && sign != "")
            {
                return ds;
            }
            else
            {
                return null;
            }
        }


        ///
        /// <summary>
        /// 计算预采单头
        /// </summary>
        /// <param name="saleHeadId">预采单头id</param>
        /// <param name="saleId">销售任务ID</param>
        /// <returns></returns>
        public DataSet calculationPerSaleHead(string saleHeadId, string saleId)
        {
            DataSet ds = SaleMonomerdao.calculationPerSaleHead(saleHeadId, saleId);
            string sign = ds.Tables[0].Rows[0]["数量"].ToString();
            if (sign != null && sign != "")
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取该书籍在此销售单头中的已购数量
        /// </summary>
        /// <param name="saleHeadId">销售单头</param>
        /// <param name="saleId">销售任务</param>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public DataSet getsalemonDetail(string saleHeadId, string saleId, string bookNum)
        {
            DataSet ds = SaleMonomerdao.getsalemonDetail(saleHeadId, saleId, bookNum);
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
        /// 获取该书籍在此预采单头中的已购数量
        /// </summary>
        /// <param name="saleHeadId">预采单头</param>
        /// <param name="saleId">销售任务</param>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public DataSet getPersalemonDetail(string saleHeadId, string saleId, string bookNum)
        {
            DataSet ds = SaleMonomerdao.getPersalemonDetail(saleHeadId, saleId, bookNum);
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
        /// 根据书号，单头id，销售任务id，获取单体信息
        /// </summary>
        /// <param name="saleId">销售任务id</param>
        /// <param name="saleHeadId">单头id</param>
        /// <param name="bookNum">书号</param>
        /// <returns>数据集</returns>
        public DataSet getSalemonBasic(string saleId, string saleHeadId)
        {
            return SaleMonomerdao.getSalemonBasic(saleId, saleHeadId);
        }

        /// <summary>
        /// 获取该书在销售单头下的总数量
        /// </summary>
        /// <param name="saleId">销售任务id</param>
        /// <param name="saleHeadId">销售单头id</param>
        /// <param name="bookNum">书号</param>
        /// <returns>数量</returns>
        public int getSalemonBookNumber(string saleId, string saleHeadId, string bookNum)
        {
            return SaleMonomerdao.getSalemonBookNumber(saleId, saleHeadId, bookNum);
        }
        /// <summary>
        /// 获取该书在销售单头下的总实洋
        /// </summary>
        /// <param name="saleId">销售任务id</param>
        /// <param name="saleHeadId">销售单头id</param>
        /// <param name="bookNum">书号</param>
        /// <returns>总实洋</returns>
        public double getSalemonBookRealPrice(string saleId, string saleHeadId, string bookNum)
        {
            return SaleMonomerdao.getSalemonBookRealPrice(saleId, saleHeadId, bookNum);
        }

        /// <summary>
        /// 根据书号和销售任务id获取该书的已购数量
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns>数量</returns>
        public int getBookNumberSumByBookNum(string bookNum, string saleTaskId)
        {
            return SaleMonomerdao.getBookNumberSumByBookNum(bookNum, saleTaskId);
        }
        /// <summary>
        /// 根据销售任务id，销售单头ID，和书号，查询该销售单的已购数
        /// </summary>
        /// /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">销售单头ID</param>
        /// /// <param name="bookNum">书号</param>
        /// <returns>数据集</returns>
        public int getSaleNumber(string saleTaskId, string saleHeadId, string bookNum)
        {
            return SaleMonomerdao.getSaleNumber(saleTaskId, saleHeadId, bookNum);
        }
        /// <summary>
        /// 团采排行
        /// </summary>
        /// <returns></returns>
        public DataSet GroupCount(DateTime startTime, DateTime endTime, string regionName, string type)
        {
            DataSet ds = SaleMonomerdao.GroupCount(startTime, endTime, regionName, type);
            return ds;
        }

        //public DataSet msg()
        //{
        //    return SaleMonomerdao.msg();
        //}

        /// <summary>
        /// 客户采购统计
        /// </summary>
        /// <returns></returns>
        public DataSet groupCustomer(DateTime startTime, DateTime endTime, string regionName, string type)
        {
            DataSet ds = SaleMonomerdao.groupCustomer(startTime, endTime, regionName, type);
            return ds;
        }
        /// <summary>
        /// 客户所购品种数
        /// </summary>
        /// <returns></returns>
        public int customerKinds(DateTime startTime, DateTime endTime, string regionName, string customerName, string type)
        {
            int ds = SaleMonomerdao.customerKinds(startTime, endTime, regionName, customerName, type);
            return ds;
        }

        /// <summary>
        /// 导出表格
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataTable ExportExcel(string strWhere)
        {
            return SaleMonomerdao.ExportExcel(strWhere);
        }


        /// <summary>
        /// 预采导出
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public DataTable ExportExcelsCopy(string strWhere)
        {
            DataTable excel = new DataTable();

            excel.Columns.Add("书号");
            excel.Columns.Add("书名");
            excel.Columns.Add("ISBN");
            excel.Columns.Add("单价");
            excel.Columns.Add("数量");
            excel.Columns.Add("码洋");
            excel.Columns.Add("出版社");
            excel.Columns.Add("销售折扣");
            DataTable dt = SaleMonomerdao.ExportExcelCopy(strWhere);
            if (dt != null)
            {
                DataRowCollection count = dt.Rows;
                foreach (DataRow row in count)
                {
                    string bookName = ToDBC(row[1].ToString());
                    excel.Rows.Add(row[0], bookName, row[2], row[3], row[4], row[5], row[6], row[7]);
                }
            }
            return excel;
        }

        public DataTable ExportExcels(string strWhere)
        {
            DataTable excel = new DataTable();

            excel.Columns.Add("书号");
            excel.Columns.Add("书名");
            excel.Columns.Add("ISBN");
            excel.Columns.Add("单价");
            excel.Columns.Add("数量");
            excel.Columns.Add("码洋");
            excel.Columns.Add("出版社");
            excel.Columns.Add("销售折扣");
            DataTable dt = SaleMonomerdao.ExportExcel(strWhere);
            if (dt != null)
            {
                DataRowCollection count = dt.Rows;
                foreach (DataRow row in count)
                {
                    string bookName = ToDBC(row[1].ToString());
                    excel.Rows.Add(row[0], bookName, row[2], row[3], row[4], row[5], row[6], row[7]);
                }
            }
            return excel;
        }
        /// <summary>
        /// 导出页面上查询到的结果
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="groupbyType">groupby条件</param>
        /// <param name="state">状态</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public DataTable exportAll(string strWhere, string groupbyType, string state, string time)
        {
            return SaleMonomerdao.exportAll(strWhere, groupbyType, state, time);
        }
        /// <summary>
        /// 导出明细
        /// </summary>
        /// <param name="groupbyType">groupby方式</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public DataTable exportDel(string groupbyType, string strWhere)
        {
            return SaleMonomerdao.exportDel(groupbyType, strWhere);
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
            excel.Columns.Add("进货折扣");
            excel.Columns.Add("采集日期");
            excel.Columns.Add("采集人用户名");
            excel.Columns.Add("采集状态");
            excel.Columns.Add("供应商");
            excel.Columns.Add("备注");
            excel.Columns.Add("备注1");
            excel.Columns.Add("备注2");
            excel.Columns.Add("备注3");
            DataTable dt = SaleMonomerdao.exportDel(groupbyType, strWhere);
            if (dt != null)
            {
                DataRowCollection count = dt.Rows;
                foreach (DataRow row in count)
                {
                    string state = row[11].ToString();
                    if (state == "3" || state == "" | state == null)
                    {
                        state = "预采";
                    }
                    if (state == "1" || state == "2")
                    {
                        state = "现采";
                    }
                    string bookName = ToDBC(row[3].ToString());
                    excel.Rows.Add(row[0], row[1], row[2], bookName, row[4], row[5], row[6], row[7], row[8], row[9], row[10], state, row[12], row[13], row[14], row[15], row[16], row[17]);
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
        /// 根据书号和销售任务id判断是否买过此书
        /// </summary>
        /// <param name="HeadId">销售单头ID</param>
        /// <param name="saletaskId">销售任务id</param>
        /// <returns>true 已存在</returns>
        public Boolean isexites(string saletaskId, string booknum)
        {
            return SaleMonomerdao.isexites(saletaskId, booknum);

        }
        /// <summary>
        /// 添加现采销售单体
        /// </summary>
        /// <param name="RegionId">地区id</param>
        /// <param name="sale">销售单体</param>
        /// <returns></returns>
        public Result addsale(int RegionId, SaleMonomer sale)
        {
            string saleText= "insert into T_SaleMonomer set saleIdMonomerId="+sale.SaleIdMonomerId+",bookNum='"+sale.BookNum+"',ISBN='"+sale.ISBN1+"',saleHeadId='"+sale.SaleHeadId+"',number="+sale.Number+",unitPrice="+sale.UnitPrice+",totalPrice="+sale.TotalPrice+",realDiscount="+sale.RealDiscount+",realPrice="+sale.RealPrice+",dateTime='"+sale.Datetime+"',alreadyBought="+sale.AlreadyBought+",saleTaskId='"+sale.SaleTaskId+"'";
            string stockText= "select goodsShelvesId,stockNum from T_Stock where bookNum = '"+sale.BookNum+"' and regionId = "+RegionId+" order by stockNum asc";
          
            int row= SaleMonomerdao.addsale(saleText, stockText, sale);
            if (row > 0)
            {
                return Result.添加成功;
            }
            else
            {
                return Result.添加失败;
            }
        }
    }
}
