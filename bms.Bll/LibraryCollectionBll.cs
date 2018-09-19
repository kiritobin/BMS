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
        public DataTable Select()
        {
            return libraryDao.Select();
        }
    }
}
