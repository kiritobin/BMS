﻿using bms.Dao;
using bms.Model;
using System;
using System.Data;
using static bms.Bll.Enums;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class BookBasicBll
    {
        BookBasicDao basicDao = new BookBasicDao();
        /// <summary>
        /// 获取所有书本基础数据的ISBN，单价，书名
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            return basicDao.Select();
        }

        /// <summary>
        /// 根据书号查找isbn，单价，折扣
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public BookBasicData SelectById(long bookNum)
        {
            return basicDao.SelectById(bookNum);
        }

        /// <summary>
        /// 根据ISBN查找书号，单价，折扣
        /// </summary>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public DataSet SelectByIsbn(string ISBN)
        {
            return basicDao.SelectByIsbn(ISBN);
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
        /// 判断是否关联引用
        /// </summary>
        /// <param name="table">表明</param>
        /// <param name="primarykeyname">主键名</param>
        /// <param name="primarykey">主键</param>
        /// <returns></returns>
        public OpResult IsDelete(string table, string primarykeyname, string primarykey)
        {
            PublicProcedure procedure = new PublicProcedure();
            int row = procedure.isDelete(table, primarykeyname, primarykey);
            if (row > 0)
            {
                return Enums.OpResult.关联引用;
            }
            else
            {
                return Enums.OpResult.记录不存在;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns>执行结果</returns>
        public Result Delete(long bookNum)
        {
            int row = basicDao.Delete(bookNum);
            if (row>0)
            {
                return Result.删除成功;
            }
            else {
                return Result.删除失败;
            }
        }
        /// <summary>
        /// 取得最新书号
        /// </summary>
        /// <returns></returns>
        public BookBasicData getBookNum()
        {
            return basicDao.getBookNum();
        }
        /// <summary>
        /// 更新最新书号
        /// </summary>
        /// <param name="bookNum"></param>
        /// <returns></returns>
        public Result updateBookNum(string bookNum)
        {
            int row = basicDao.updateBookNum(bookNum);
            if (row > 0)
            {
                return Result.更新成功;
            }
            else
            {
                return Result.更新失败;
            }
        }
    }
}
