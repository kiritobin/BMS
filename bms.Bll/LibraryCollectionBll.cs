using bms.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    public class LibraryCollectionBll
    {
        LibraryCollectionDao libraryDao = new LibraryCollectionDao();
        /// <summary>
        /// 获取所有客户馆藏数据的ISBN，单价，书名
        /// </summary>
        /// <returns></returns>
        public DataTable Select(int customerId)
        {
            return libraryDao.Select(customerId);
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
    }
}
