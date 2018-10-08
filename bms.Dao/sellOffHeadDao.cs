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
        public DataSet Select(string sellOffHeadId)
        {
            string sql = "select sellOffHeadID,saleTaskId,userName,customerName,kinds,count,totalPrice,realPrice,makingTime,regionId,regionName from V_SellOffHead where sellOffHeadID=@sellOffHeadID";
            string[] param = { "@sellOffHeadID" };
            object[] values = { sellOffHeadId };
            DataSet ds = db.FillDataSet(sql, param, values);
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
        /// 根据销售任务Id返回时间数据集
        /// </summary>
        /// <param name="saleTaskId"></param>
        /// <returns></returns>
        public DataSet getMakeTime(string saleTaskId)
        {
            string cmdText = "select makingTime from T_SellOffHead where saleTaskId=@saleTaskId ORDER BY makingTime desc";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
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
        /// <summary>
        /// 根据销售任务Id获取销售单头信息
        /// </summary>
        /// <param name="saleTaskId"></param>
        /// <returns></returns>
        public DataSet selectBySaleTask(string saleTaskId)
        {
            string sql = "select saleHeadId from T_SaleHead where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 获取相应的时间数量
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public int getTimeCount(DateTime time)
        {
            string sql = "select count(makingTime) from T_SellOffHead where makingTime=@makingTime";
            string[] param = { "@makingTime" };
            object[] values = { time };
            int row = int.Parse(db.ExecuteScalar(sql, param, values).ToString());
            return row;
        }
    }
}
