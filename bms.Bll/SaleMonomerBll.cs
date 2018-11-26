﻿using System;
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
        /// 根据销售单头ID真删除销售单头包括单体
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns>0不成功</returns>
        public Result realDeleteHeadAndMon(string saleTaskId, string saleHeadId)
        {
            int row = SaleMonomerdao.realDeleteHeadAndMon(saleTaskId, saleHeadId);
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
        public DataSet SelectBookRanking(DateTime startTime,DateTime endTime,string regionName)
        {
            DataSet ds = SaleMonomerdao.SelectBookRanking(startTime,endTime,regionName);
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
        public DataSet GroupCount(DateTime startTime, DateTime endTime, string regionName)
        {
            DataSet ds = SaleMonomerdao.GroupCount(startTime, endTime, regionName);
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
        public DataSet groupCustomer(DateTime startTime, DateTime endTime, string regionName)
        {
            DataSet ds = SaleMonomerdao.groupCustomer(startTime,endTime,regionName);
            return ds;
        }
        /// <summary>
        /// 客户所购品种数
        /// </summary>
        /// <returns></returns>
        public int customerKinds(DateTime startTime, DateTime endTime, string regionName, string customerName)
        {
            int ds = SaleMonomerdao.customerKinds(startTime,endTime,regionName,customerName);
            return ds;
        }
    }
}
