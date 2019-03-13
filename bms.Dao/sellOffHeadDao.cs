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
            string cmdText = "select sellOffHeadID,makingTime from T_SellOffHead where saleTaskId=@saleTaskId ORDER BY sellOffHeadID desc";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }

        /// <summary>
        /// 获取该销退单头下所有的单据号和制单时间
        /// </summary>
        /// <returns></returns>
        public DataSet getAllTime()
        {
            string cmdText = "select sellOffHeadID,makingTime from T_SellOffHead ORDER BY makingTime desc";
            DataSet ds = db.FillDataSet(cmdText, null, null);
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
        /// <summary>
        /// 获取种类数量
        /// </summary>
        /// <param name="sellOffHeadId"></param>
        /// <returns></returns>
        public int getKinds(string sellOffHeadId)
        {
            string sql = "select bookNum,count from T_SellOffMonomer where sellOffHead=@sellOffHeadId";
            string[] param = { "@sellOffHeadId" };
            object[] values = { sellOffHeadId };
            float sltemp = 0;
            int zl = 0;
            DataTable dt = db.getkinds(sql, param, values);
            DataView dv = new DataView(dt);
            DataTable dttemp = dv.ToTable(true, "bookNum");
            for(int i = 0; i < dttemp.Rows.Count; i++)
            {
                string bnum = dttemp.Rows[i]["bookNum"].ToString();
                DataRow[] dr = dt.Select("bookNum='" + bnum + "'");
                for(int j = 0; j < dr.Length; j++)
                {
                    sltemp += float.Parse(dr[j]["count"].ToString().Trim());
                }
                if (sltemp > 0)
                {
                    zl++;
                }
            }
            return zl;
        }
        /// <summary>
        /// 通过单体计算后更新单头信息
        /// </summary>
        /// <param name="sell"></param>
        /// <returns></returns>
        public int Update(SellOffHead sell)
        {
            string sql = "update T_SellOffHead set kinds=@kinds,count=@count,totalPrice=@totalPrice,realPrice=@realPrice,state=@state where  sellOffHeadID=@sellOffHeadId";
            string[] param = { "@kinds", "@count", "@totalPrice", "@realPrice", "@sellOffHeadId","@state" };
            object[] values = { sell.Kinds, sell.Count, sell.TotalPrice, sell.RealPrice, sell.SellOffHeadId,sell.State };
            int row = db.ExecuteNoneQuery(sql, param, values);
            return row;
        }
        /// <summary>
        /// 导出表格
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataTable ExportExcel(string strWhere)
        {
            String cmdText = "select sellOffHead as 单据编号,bookNum as 书号,bookName as 书名,isbn as ISBN,price as 单价,sum(count) as 数量 ,sum(totalPrice) as 码洋,sum(realPrice) as 实洋 from v_selloffmonomer where sellOffHead=@strWhere group by bookNum,bookName,isbn,price order by dateTime desc";
            string[] param = { "@strWhere" };
            object[] values = { strWhere };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            DataTable dt = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}
