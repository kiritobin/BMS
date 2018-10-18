using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using bms.DBHelper;
using bms.Model;
using System.Data;

namespace bms.Dao
{
    public class replenishMentDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取补货单体信息
        /// </summary>
        /// <returns></returns>
        public DataSet Select()
        {
            string cmdText = "SELECT * FROM V_ReplenishMentMononer";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            return ds;
        }
        /// <summary>
        /// 插入补货单体
        /// </summary>
        /// <param name="rm">补货单体实体</param>
        /// <returns></returns>
        public int Insert(replenishMentMonomer rm)
        {
            string sql = "insert into T_ReplenishmentMonomer(rsMononerID,bookNum,ISBN,rsHeadID,unitPrice,count,totalPrice,realDiscount,realPrice,dateTime) VALUES(@rsMonomerID,@bookNum,@ISBN,@rsHeadID,@unitPrice,@count,@totalPrice,@realDiscount,@realPrice,@dateTime)";
            string[] param = { "@rsMonomerID", "@bookNum", "@ISBN", "@rsHeadID", "@unitPrice", "@count", "@totalPrice", "@realDiscount", "@realPrice", "@dateTime" };
            object[] values = { rm.RsMonomerID, rm.BookNum, rm.Isbn, rm.RsHeadID, rm.UnitPrice, rm.Count, rm.TotalPrice, rm.RealDiscount, rm.RealPrice, rm.Time };
            int row = db.ExecuteNoneQuery(sql, param, values);
            return row;
        }
        /// <summary>
        /// 添加补货单头
        /// </summary>
        /// <param name="rd">补货单头实体</param>
        /// <returns>受影响行数</returns>
        public int InsertRsHead(replenishMentHead rd)
        {
            string cmdText = "insert into T_ReplenishmentHead(rsHeadID,saleTaskId,kingdsNum,number,allTotalPrice,allRealPrice,userId,dateTime) VALUES(@rsHeadID,@saleTaskId,@kingdsNum,@number,@allTotalPrice,@allRealPrice,@userId,@dateTimee)";
            string[] param = { "@rsHeadID", "@saleTaskId", "@kingdsNum", "@number", "@allTotalPrice", "@allRealPrice", "@userId", "@dateTimee" };
            object[] values = { rd.RsHeadID, rd.SaleTaskId, rd.KindsNum, rd.Number, rd.AllTotalPrice, rd.AllRealPrice, rd.UserId, rd.Time };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 更新补货单头的种数，数量，总码洋，总实洋
        /// </summary>
        /// <param name="rd">销售单头实体</param>
        /// <returns>受影响行数</returns>
        public int updateRsHead(replenishMentHead rd)
        {
            string cmdText = "update T_ReplenishmentHead set kingdsNum=@kindsNum,number=@number,allTotalPrice=@allTotalPrice,allRealPrice=@allRealPrice where rsHeadID=@rsHeadID";
            string[] param = { "@kindsNum", "@number", "@allTotalPrice", "@allRealPrice", "@rsHeadID" };
            object[] values = { rd.KindsNum, rd.Number, rd.AllTotalPrice, rd.AllRealPrice, rd.RsHeadID };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 获取单头数量
        /// </summary>
        /// <returns>行数</returns>
        public int countHead()
        {
            string cmdText = "select count(rsHeadID) from T_ReplenishmentHead";
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, null, null));
            return row;
        }
        /// <summary>
        /// 获取制单日期
        /// </summary>
        /// <returns>时间字符串</returns>
        public string RsHeadTime()
        {
            string comText = "select dateTime from T_ReplenishmentHead order by dateTime desc";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string time = ds.Tables[0].Rows[0]["dateTime"].ToString();
                return time.Substring(0, 10);
            }
            return null;
        }
        /// <summary>
        /// 根据销售任务或取补货头ID
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns>补货单头id</returns>
        public string getRsHeadID(string saleTaskId)
        {
            string comText = "select rsHeadID from T_ReplenishmentHead where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            string rsHeadId;
            DataSet ds = db.FillDataSet(comText, param, values);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string text = ds.Tables[0].Rows[0].ToString();
                rsHeadId = ds.Tables[0].Rows[0]["rsHeadID"].ToString();
                if (rsHeadId == "" || rsHeadId == null)
                {
                    return rsHeadId = "none";
                }
                else
                {
                    return rsHeadId;
                }
            }
            return rsHeadId = "none";
        }
        /// <summary>
        /// 获取该补货单头下的单体数量
        /// </summary>
        /// <param name="rsHeadID">补货单头id</param>
        /// <returns>数量</returns>
        public int countMon(string rsHeadID)
        {
            string cmdText = "select count(rsMononerID) from T_ReplenishmentMonomer where rsHeadID=@rsHeadID";
            string[] param = { "@rsHeadID" };
            object[] values = { rsHeadID };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            return row;
        }
    }
}
