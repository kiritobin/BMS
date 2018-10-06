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
        /// 添加销退单头
        /// </summary>
        /// <param name="sellOffHead"></param>
        /// <returns></returns>
        public int Insert(SellOffHead sellOffHead)
        {
            string cmdText = "insert into T_SellOffHead(sellOffHeadID, saleTaskId, userID, makingTime) VALUES(@sellOffHeadID,@saleTaskId,@userID,@makingTime)";
            string[] param = { "@sellOffHeadID", "@saleTaskId",  "@userID", "@makingTime" };
            object[] values = { sellOffHead.SellOffHeadId, sellOffHead.SaleTaskId.SaleTaskId, sellOffHead.User.UserId, sellOffHead.MakingTime };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 根据销售任务ID获取单头数量
        /// </summary>
        /// <param name="saleTaskId"></param>
        /// <returns></returns>
        public int getCount(string saleTaskId)
        {
            string cmdText = "select COUNT(sellOffHeadID) from T_SellOffHead where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            int row = int.Parse(db.ExecuteScalar(cmdText, param, values).ToString());
            return row;
        }

        /// <summary>
        /// 删除销退单头
        /// </summary>
        /// <param name="sellOffHeadId"></param>
        /// <returns></returns>
        public int Delete(string sellOffHeadId)
        {
            string cmdText = "delete from T_SellOffHead where sellOffHeadID=@sellOffHeadID";
            string[] param = { "@sellOffHeadID" };
            object[] values = { sellOffHeadId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
    }
}
