using bms.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class LibraryCollectionBll
    {
        LibraryCollectionDao libraryDao = new LibraryCollectionDao();
        /// <summary>
        /// 获取所有客户馆藏数据的ISBN，单价，书名
        /// </summary>
        /// <returns></returns>
        public DataTable Select(string customerId)
        {
            return libraryDao.Select(customerId);
        }

        /// <summary>
        /// replace批量导入数据库
        /// </summary>
        /// <param name="strSql">导入数据字符串</param>
        /// <returns></returns>
        public Result Replace(string strSql)
        {
            int row = libraryDao.Replace(strSql);
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
            /// 通过地区获取客户姓名和ID
            /// </summary>
            /// <returns></returns>
            public DataSet getCustomerByReg(int regionId)
        {
            return libraryDao.getCustomerByReg(regionId);
        }
        /// <summary>
        /// 查询客户数据
        /// </summary>
        /// <returns></returns>
        public DataSet getCustomer()
        {
            return libraryDao.getCustomer();
        }
        /// <summary>
        /// 删除馆藏
        /// </summary>
        /// <param name="libraryId"></param>
        /// <returns></returns>
        public Result Delete(int libraryId)
        {
            int count = libraryDao.Delete(libraryId);
            if (count>0)
            {
                return Result.删除成功;
            }
            else
            {
                return Result.删除失败;
            }
        }
        /// <summary>
        /// 通过客户id删除
        /// </summary>
        /// <param name="customId"></param>
        /// <returns></returns>
        public Result deleteByCus(int customId)
        {
            int count = libraryDao.deleteByCus(customId);
            if (count > 0)
            {
                return Result.删除成功;
            }
            else
            {
                return Result.删除失败;
            }
        }
        /// <summary>
        /// 查询图书数据是否存在
        /// </summary>
        /// <param name="customerId">客户id</param>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public Result Selectbook(string customerId, string ISBN)
        {
            int row = libraryDao.Selectbook(customerId, ISBN);
            if (row>0)
            {
                return Result.记录存在;
            }
            else
            {
                return Result.记录不存在;
            }
        }
    }
}
