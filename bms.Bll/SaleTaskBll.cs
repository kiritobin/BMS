using bms.Dao;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bms.Bll
{
    using Result = Enums.OpResult;
    public class SaleTaskBll
    {
        readonly SaleTaskDao saleDao = new SaleTaskDao();
        public Result insert(SaleTask sale)
        {
            int row = saleDao.insert(sale);
            if(row > 0)
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
