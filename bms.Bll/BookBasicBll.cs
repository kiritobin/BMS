using bms.Dao;
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
        /// 添加基础数据
        /// </summary>
        /// <param name="basic">基础数据实体对象</param>
        /// <returns></returns>
        public Result Insert(BookBasicData basic)
        {
            int row = basicDao.Insert(basic);
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
        /// 获取所有书本基础数据的ISBN，单价，书名
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            return basicDao.Select();
        }
        /// <summary>
        /// 取行数
        /// </summary>
        /// <returns></returns>
        public int SelectCount()
        {
            return basicDao.SelectCount();
        }
        /// <summary>
        /// 通过ISBN和书名获取书号
        /// </summary>
        /// <param name="ISBN"></param>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public DataTable getBookNum(string ISBN, string bookName)
        {
            return basicDao.getBookNum(ISBN,bookName);
        }

        public DataTable getBookNumByNum(string booknum)
        {
            return basicDao.getBookNumByNum(booknum);
        }

        /// <summary>
        /// 根据书号查找isbn，单价，折扣
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public BookBasicData SelectById(string bookNum)
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
        public Result Delete(string bookNum)
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
        /// <summary>
        /// 查询供应商
        /// </summary>
        /// <returns></returns>
        public DataTable selectSupplier()
        {
            return basicDao.selectSupplier();
        }
        /// <summary>
        /// 获取制单员
        /// </summary>
        /// <returns></returns>
        public DataTable selectZdy()
        {
            return basicDao.selectZdy();
        }

        /// <summary>
        /// 获取来源组织/收货组织
        /// </summary>
        /// <returns></returns>
        public DataTable selectSource()
        {
            return basicDao.selectSource();
        }

        /// <summary>
        ///导出书籍基础数据
        /// </summary>
        /// <returns></returns>
        public DataTable excelBook(string search)
        {
            DataTable excel = new DataTable();
            excel.Columns.Add("书号");
            excel.Columns.Add("ISBN");
            excel.Columns.Add("书名");
            excel.Columns.Add("出版日期");
            excel.Columns.Add("定价");
            excel.Columns.Add("出版社");
            excel.Columns.Add("预收数量");
            excel.Columns.Add("进货折扣");
            excel.Columns.Add("销售折扣");
            excel.Columns.Add("备注");
            DataTable dt = basicDao.excelBook(search);
            if (dt != null)
            {
                DataRowCollection count = dt.Rows;
                foreach (DataRow row in count)
                {
                    string bookName = ToDBC(row[2].ToString());
                    excel.Rows.Add(row[0], row[1], bookName, row[3], row[4], row[5], row[6], row[7], row[8], row[9]);
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
