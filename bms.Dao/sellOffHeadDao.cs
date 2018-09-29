using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.DBHelper;
using bms.Model;
using System.Data;

namespace bms.Dao
{
    public class sellOffHeadDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取销退单头信息
        /// </summary>
        /// <returns></returns>
        public DataSet Select()
        {
            string sql = "select sellOffHeadID,saleTaskId,userName,customerName,kinds,count,totalPrice,realPrice,makingTime from V_SellOffHead";
            DataSet ds = db.FillDataSet(sql, null, null);
            return ds;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sellOffHead"></param>
        /// <returns></returns>
        public int Insert(SellOffHead sellOffHead)
        {
            string cmdText = "insert into T_SellOffHead(sellOffHeadID,saleTaskId,kinds,count,totalPrice,realPrice,userID,state,makingTime) VALUES(@sellOffHeadID,@saleTaskId,@kinds,@count,@totalPrice,@realPrice,@userID,@state,@makingTime)";
            string[] param = { "@sellOffHeadID", "@saleTaskId", "@kinds", "@count", "@totalPrice", "@realPrice", "@userID", "@state", "@makingTime" };
            object[] values = { sellOffHead.SellOffHeadId, sellOffHead.SaleTaskId.SaleTaskId, sellOffHead.Kinds, sellOffHead.Count, sellOffHead.TotalPrice, sellOffHead.RealPrice, sellOffHead.User.UserId, sellOffHead.State, sellOffHead.MakingTime };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
    }
}
