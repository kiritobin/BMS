using bms.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Bll
{
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

    }
}
