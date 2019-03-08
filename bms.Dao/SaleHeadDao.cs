using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class SaleHeadDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 查询所有销售单头
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet Select()
        {
            string comText = "select saleHeadId,saleTaskId,kindsNum,number,allTotalPrice,allRealPrice,userId,regionId from T_SaleHead";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取销售任务id
        /// </summary>
        /// <param name="saleHeadId"></param>
        /// <returns></returns>
        public string SelectTaskByheadId(string saleHeadId)
        {
            string comText = "select saleTaskId from T_SaleHead where saleHeadId=@saleHeadId";
            string[] param = { "@saleHeadId" };
            object[] values = { saleHeadId };
            DataSet ds = db.FillDataSet(comText, param, values);
            string saleTaskId = ds.Tables[0].Rows[0]["saleTaskId"].ToString();

            if (saleTaskId != null || saleTaskId.Length > 0)
            {
                return saleTaskId;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 判断单头编号
        /// </summary>
        /// <param name="saleHeadId">销售任务ID</param>
        /// <returns></returns>
        public int countHead()
        {
            string cmdText = "select count(saleHeadId) from T_SaleHead";
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, null, null));
            return row;
        }
        /// <summary>
        /// 微信小程序判断单头编号
        /// </summary>
        /// <param name="saleHeadId">销售任务ID</param>
        /// <returns></returns>
        public int WeChatcountHead()
        {
            string cmdText = "select count(saleHeadId) from T_SaleHead_copy";
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, null, null));
            return row;
        }

        /// <summary>
        /// 添加销售单头
        /// </summary>
        /// <param name="task">销售单头实体</param>
        /// <returns>受影响行数</returns>
        public int Insert(SaleHead salehead)
        {
            string cmdText = "insert into T_SaleHead(saleHeadId,saleTaskId,kindsNum,number,allTotalPrice,allRealPrice,userId,regionId,dateTime,state,remarks) values(@saleHeadId,@saleTaskId,@kindsNum,@number,@allTotalPrice,@allRealPrice,@userId,@regionId,@dateTime,@state,@remarks)";
            string[] param = { "@saleHeadId", "@saleTaskId", "@kindsNum", "@number", "@allTotalPrice", "@allRealPrice", "@userId", "@regionId", "@dateTime", "@state", "@remarks" };
            object[] values = { salehead.SaleHeadId, salehead.SaleTaskId, salehead.KindsNum, salehead.Number, salehead.AllTotalPrice, salehead.AllRealPrice, salehead.UserId, salehead.RegionId, salehead.DateTime, salehead.State, salehead.Remarks };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 添加预采销售单头
        /// </summary>
        /// <param name="task">销售单头实体</param>
        /// <returns>受影响行数</returns>
        public int perInsert(SaleHead salehead)
        {
            string cmdText = "insert into t_salehead_copy(saleHeadId,saleTaskId,kindsNum,number,allTotalPrice,allRealPrice,userId,regionId,dateTime,state) values(@saleHeadId,@saleTaskId,@kindsNum,@number,@allTotalPrice,@allRealPrice,@userId,@regionId,@dateTime,@state)";
            string[] param = { "@saleHeadId", "@saleTaskId", "@kindsNum", "@number", "@allTotalPrice", "@allRealPrice", "@userId", "@regionId", "@dateTime", "@state" };
            object[] values = { salehead.SaleHeadId, salehead.SaleTaskId, salehead.KindsNum, salehead.Number, salehead.AllTotalPrice, salehead.AllRealPrice, salehead.UserId, salehead.RegionId, salehead.DateTime, salehead.State };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 假删除销售单头
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns>受影响行数</returns>
        public int Delete(string saleTaskId, string saleHeadId)
        {
            string cmdText = "update T_SaleHead set deleteState = 1 where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
            String[] param = { "@saleTaskId", "@saleHeadId" };
            String[] values = { saleTaskId, saleHeadId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 获取制单日期
        /// </summary>
        /// <returns>时间字符串</returns>
        public string getSaleHeadTime()
        {
            string comText = "select dateTime from T_SaleHead order by dateTime desc";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                string time = Convert.ToDateTime(ds.Tables[0].Rows[0]["dateTime"]).ToString("yyyyMMdd");
                return time;
            }
            return null;
        }

        /// <summary>
        /// 获取数据采集制单日期
        /// </summary>
        /// <returns>时间字符串</returns>
        public string getPerSaleHeadTime()
        {
            string comText = "select dateTime from T_SaleHead_copy order by dateTime desc";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                string time = ds.Tables[0].Rows[0]["dateTime"].ToString();
                return time.Substring(0, 10);
            }
            return null;
        }

        /// <summary>
        /// 数据采集根据当天时间获取单头编号
        /// </summary>
        /// <returns>时间字符串</returns>
        public string getPerSaleHeadIdByTime(string nowtime)
        {
            //SELECT saleTaskId from t_saletask where startTime like  ORDER BY startTime desc
            string comText = "select saleHeadId from T_SaleHead_copy where dateTime like '" + nowtime + "%' order by saleHeadId desc";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                string saleHeadId = ds.Tables[0].Rows[0]["saleHeadId"].ToString();
                return saleHeadId;
            }
            return null;
        }

        /// <summary>
        /// 团采根据当天时间获取单头编号
        /// </summary>
        /// <returns>时间字符串</returns>
        public string getSaleHeadIdByTime(string nowtime)
        {
            //SELECT saleTaskId from t_saletask where startTime like  ORDER BY startTime desc
            string comText = "select saleHeadId from T_SaleHead where dateTime like '" + nowtime + "%' order by saleHeadId desc";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                string saleHeadId = ds.Tables[0].Rows[0]["saleHeadId"].ToString();
                return saleHeadId;
            }
            return null;
        }

        /// <summary>
        /// 根据单头id 销售任务id获取销售单头信息
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">销售单头id</param>
        /// <returns>数据集</returns>
        public DataSet getSaleHeadBasic(string saleTaskId, string saleHeadId)
        {
            string comText = "select saleHeadId,saleTaskId,kindsNum,number,allTotalPrice,allRealPrice,dateTime,userName from V_SaleHead where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
            String[] param = { "@saleTaskId", "@saleHeadId" };
            String[] values = { saleTaskId, saleHeadId };
            DataSet ds = db.FillDataSet(comText, param, values);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据单头id 销售任务id获取预采销售单头信息
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">销售单头id</param>
        /// <returns>数据集</returns>
        public DataSet getPerSaleHeadBasic(string saleTaskId, string saleHeadId)
        {
            string comText = "select saleHeadId,saleTaskId,kindsNum,number,allTotalPrice,allRealPrice,dateTime,userName from V_PerSaleHead where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
            String[] param = { "@saleTaskId", "@saleHeadId" };
            String[] values = { saleTaskId, saleHeadId };
            DataSet ds = db.FillDataSet(comText, param, values);
            if (ds.Tables.Count == 1 && ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return ds;
            }
        }


        /// <summary>
        /// 获取操作员
        /// </summary>
        /// <returns></returns>
        public DataSet slectCzy()
        {
            string comText = "select distinct userName from V_SaleHead order by convert(userName using gbk) collate gbk_chinese_ci";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据销售任务id，销售单头,该销售单头下的所有销售单体
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">销售单头id</param>
        /// <returns>基础数据表</returns>
        public DataTable getSaleAllbyHeadIdandStaskId(string saleTaskId, string saleHeadId)
        {
            string comText = "select saleIdMonomerId,bookNum,bookName,saleHeadId,saleTaskId, sum(number) as number from V_SaleMonomer where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId group by bookNum,bookName,saleHeadId,saleTaskId,number";
            String[] param = { "@saleTaskId", "@saleHeadId" };
            String[] values = { saleTaskId, saleHeadId };
            DataSet ds = db.FillDataSet(comText, param, values);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据销售任务id获取该销售计划下所有销售单头
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns></returns>
        public DataTable getSaleHeadIdbyStaskId(string saleTaskId)
        {
            string comText = "select saleHeadId from T_SaleHead where saleTaskId=@saleTaskId and (state=1 or state=3) and deleteState=0";
            String[] param = { "@saleTaskId" };
            String[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(comText, param, values);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }
    }
}
