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
            string sql = "insert into T_ReplenishmentMonomer(rsMononerID,bookNum,ISBN,author,count,supplier,dateTime,saleTaskId,saleHeadId,saleIdMonomerId) VALUES(@rsMononerID,@bookNum,@ISBN,@author,@count,@supplier,@dateTime,@saleTaskId,@saleHeadId,@saleIdMonomerId)";
            string[] param = { "@rsMononerID", "@bookNum", "@ISBN", "@author", "@count", "@supplier", "@dateTime", "@saleTaskId", "@saleHeadId", "@saleIdMonomerId" };
            object[] values = { rm.RsMonomerID, rm.BookNum, rm.Isbn, rm.Author, rm.Count, rm.Supplier, rm.DateTime, rm.SaleTaskId, rm.SaleHeadId, rm.SaleIdMonomerId };
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
            string cmdText = "insert into T_ReplenishmentHead(saleTaskId,kingdsNum,number,userId,dateTime) VALUES(@saleTaskId,@kingdsNum,@number,@userId,@dateTimee)";
            string[] param = { "@saleTaskId", "@kingdsNum", "@number", "@userId", "@dateTimee" };
            object[] values = { rd.SaleTaskId, rd.KindsNum, rd.Number, rd.UserId, rd.Time };
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
            string cmdText = "update T_ReplenishmentHead set kingdsNum=@kindsNum,number=@number where saleTaskId=@saleTaskId";
            string[] param = { "@kindsNum", "@number", "@saleTaskId" };
            object[] values = { rd.KindsNum, rd.Number, rd.SaleTaskId };
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
            string comText = "select saleTaskId from T_ReplenishmentHead where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            string rsHeadId;
            DataSet ds = db.FillDataSet(comText, param, values);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string text = ds.Tables[0].Rows[0].ToString();
                rsHeadId = ds.Tables[0].Rows[0]["saleTaskId"].ToString();
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
        public int countMon(string saleTaskId)
        {
            string cmdText = "select count(rsMononerID) from T_ReplenishmentMonomer where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            return row;
        }
        /// <summary>
        /// 统计补货单头种数
        /// </summary>
        /// <param name="rsHeadID">补货单头id</param>
        /// <returns></returns>
        public int getkinds(string rsHeadID)
        {
            string cmdText = "select bookNum,count from T_ReplenishmentMonomer where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { rsHeadID };
            float sltemp = 0;
            int zl = 0;
            DataTable dtcount = db.getkinds(cmdText, param, values);
            DataView dv = new DataView(dtcount);
            DataTable dttemp = dv.ToTable(true, "bookNum");
            for (int i = 0; i < dttemp.Rows.Count; i++)
            {
                string bn = dttemp.Rows[i]["bookNum"].ToString();
                DataRow[] dr = dtcount.Select("bookNum='" + bn + "'");
                for (int j = 0; j < dr.Length; j++)
                {
                    float count = float.Parse(dr[j]["count"].ToString().Trim());
                    sltemp += float.Parse(dr[j]["count"].ToString().Trim());
                }
                if (sltemp > 0)
                {
                    zl++;
                    sltemp = 0;
                }
            }
            return zl;
        }
        /// <summary>
        /// 获取该单头id下的书本数量
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>结果</returns>
        public int getsBookNumberSum(string rsHeadID)
        {
            string cmdtext = "select sum(count) from T_ReplenishmentMonomer where saleTaskId=@rsHeadID";
            string[] param = { "@rsHeadID" };
            object[] values = { rsHeadID };
            string sumstring = db.ExecuteScalar(cmdtext, param, values).ToString();
            int sum;
            if (sumstring == "" || sumstring == null)
            {
                return sum = 0;
            }
            else
            {
                return sum = Convert.ToInt32(sumstring);
            }
        }
        /// <summary>
        /// 获取该单头id下的码洋
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>结果</returns>
        public double getsBookTotalPrice(string rsHeadID)
        {
            string cmdtext = "select sum(totalPrice) from T_ReplenishmentMonomer where saleTaskId=@rsHeadID";
            string[] param = { "@rsHeadID" };
            object[] values = { rsHeadID };
            string sumstring = db.ExecuteScalar(cmdtext, param, values).ToString();
            double sum;
            if (sumstring == "" || sumstring == null)
            {
                return sum = 0;
            }
            else
            {
                return sum = Convert.ToInt32(sumstring);
            }
        }
        /// <summary>
        /// 获取该单头id下的实洋
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>结果</returns>
        public double getsBookRealPrice(string rsHeadID)
        {
            string cmdtext = "select sum(realPrice) from T_ReplenishmentMonomer where saleTaskId=@rsHeadID";
            string[] param = { "@rsHeadID" };
            object[] values = { rsHeadID };
            string sumstring = db.ExecuteScalar(cmdtext, param, values).ToString();
            double sum;
            if (sumstring == "" || sumstring == null)
            {
                return sum = 0;
            }
            else
            {
                return sum = Convert.ToDouble(sumstring);
            }
        }
        /// <summary>
        /// 根据补货单头id判断其单体是否有数据
        /// </summary>
        /// <param name="rsHeadID">补货单头id</param>
        /// <returns>count</returns>
        public int getRsMonCount(string rsHeadID)
        {
            string cmdtext = "select count(rsMononerID) from T_ReplenishmentMonomer where saleTaskId=@rsHeadID";
            string[] param = { "@rsHeadID" };
            object[] values = { rsHeadID };
            string sumstring = db.ExecuteScalar(cmdtext, param, values).ToString();
            int sum;
            if (sumstring == "" || sumstring == null)
            {
                return sum = 0;
            }
            else
            {
                return sum = int.Parse((sumstring));
            }
        }
        /// <summary>
        /// 删除补货单
        /// </summary>
        /// <param name="rsHeadID">补货单头id</param>
        /// <returns>受影响行数</returns>
        public int Delete(string rsHeadID)
        {
            string cmdText = "update T_SaleTask set deleteState = 1 where saleTaskId=@rsHeadID";
            String[] param = { "@rsHeadID" };
            String[] values = { rsHeadID.ToString() };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 根据补货单头id获取单头信息
        /// </summary>
        /// <param name="rsHeadId">补货单头id</param>
        /// <returns>数据集</returns>
        public DataSet getHeadMsg(string rsHeadId)
        {
            string cmdtext = "select customerName,userName,kingdsNum,number,dateTime,state from V_ReplenishMentHead where saleTaskId=@rsHeadID";
            string[] param = { "@rsHeadID" };
            object[] values = { rsHeadId };
            DataSet ds = db.FillDataSet(cmdtext, param, values);
            return ds;
        }

        /// <summary>
        /// 根据地区ID获取补货单体统计
        /// </summary>
        /// <param name="ID">地区ID或客户ID</param>
        /// <param name="type">条件类型：0为地区，1为客户</param>
        /// <returns>数据集</returns>
        public int getTotalMon(int ID, int type)
        {
            string cmdtext;
            if (type == 0)
            {
                cmdtext = "select sum(count) as number,regionName from V_ReplenishMentMononer where regionId=@ID";
            }
            else
            {
                cmdtext = "select sum(count) as number,regionName from V_ReplenishMentMononer where customerId=@ID";
            }
            string[] param = { "@ID" };
            object[] values = { ID };
            DataSet ds = db.FillDataSet(cmdtext, param, values);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                string number = ds.Tables[0].Rows[0]["number"].ToString();
                if (number == null || number == "")
                {
                    return 0;
                }
                else
                {
                    int row = Convert.ToInt32(number);
                    return row;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据地区id获取补货单体总书籍品种数
        /// </summary>
        /// <param name="ID">地区ID或客户ID</param>
        /// <param name="type">条件类型：0为地区，1为客户</param>
        /// <returns></returns>
        public int getMonkinds(int ID, int type)
        {
            string cmdText;
            if (type == 0)
            {
                cmdText = "select bookNum,count from V_ReplenishMentMononer where regionId=@ID";
            }
            else
            {
                cmdText = "select bookNum,count from V_ReplenishMentMononer where customerId=@ID";
            }
            string[] param = { "@ID" };
            object[] values = { ID };
            float sltemp = 0;
            int zl = 0;
            DataTable dtcount = db.getkinds(cmdText, param, values);
            DataView dv = new DataView(dtcount);
            DataTable dttemp = dv.ToTable(true, "bookNum");
            for (int i = 0; i < dttemp.Rows.Count; i++)
            {
                string bn = dttemp.Rows[i]["bookNum"].ToString();
                DataRow[] dr = dtcount.Select("bookNum='" + bn + "'");
                for (int j = 0; j < dr.Length; j++)
                {
                    float count = float.Parse(dr[j]["count"].ToString().Trim());
                    sltemp += float.Parse(dr[j]["count"].ToString().Trim());
                }
                if (sltemp > 0)
                {
                    zl++;
                    sltemp = 0;
                }
            }
            return zl;
        }
    }
}
