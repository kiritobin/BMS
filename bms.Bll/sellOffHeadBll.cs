using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using bms.Dao;
using bms.Model;

namespace bms.Bll
{
    public class sellOffHeadBll
    {
        sellOffHeadDao dao = new sellOffHeadDao();
        /// <summary>
        /// 获取销退单头信息
        /// </summary>
        /// <returns></returns>
        public DataSet Select()
        {
            DataSet ds = dao.Select();
            return ds;
        }

    }
}
