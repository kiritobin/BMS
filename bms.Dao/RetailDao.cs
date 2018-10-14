using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class RetailDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取该销退单头下所有的单据号和制单时间
        /// </summary>
        /// <returns></returns>
        public DataSet getAllTime(int state)
        {
            string cmdText;
            if (state == 2)
            {
                cmdText = "select retailHeadId,dateTime from T_RetailHead where state=2 ORDER BY dateTime desc";
            }
            else
            {
                cmdText = "select retailHeadId,dateTime from T_RetailHead where state=0 or state=1 ORDER BY dateTime desc";
            }
            string[] param = { "@state" };
            object[] values = { state };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }

        /// <summary>
        /// 通过书号查看是否存在在单头中
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public int selectByBookNum(long bookNum,string retailHeadId)
        {
            string cmdText = "select count(retailMonomerId) from T_RetailMonomer where bookNum=@bookNum and retailHeadId=@retailHeadId";
            string[] param = { "@bookNum", "@retailHeadId" };
            object[] values = { bookNum, retailHeadId };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            return row;
        }

        /// <summary>
        /// 添加零售单头信息
        /// </summary>
        /// <param name="salehead"></param>
        /// <returns></returns>
        public int InsertRetail(SaleHead salehead)
        {
            string cmdText = "insert into T_RetailHead(state,retailHeadId,userId,regionId,dateTime,kindsNum,number,allTotalPrice,allRealPrice) values(@state,@retailHeadId,@userId,@regionId,@dateTime,@kindsNum,@number,@allTotalPrice,@allRealPrice)";
            string[] param = { "@state", "@retailHeadId", "@userId", "@regionId", "@dateTime", "@kindsNum", "@number", "@allTotalPrice", "@allRealPrice" };
            object[] values = { salehead.State ,salehead.SaleHeadId, salehead.UserId, salehead.RegionId, salehead.DateTime, salehead.KindsNum, salehead.Number, salehead.AllTotalPrice, salehead.AllRealPrice };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 添加零售单体
        /// </summary>
        /// <param name="task">零售单体实体</param>
        /// <returns>受影响行数</returns>
        public int InsertRetail(SaleMonomer salemonomer)
        {
            string cmdText = "insert into T_RetailMonomer(retailMonomerId,bookNum,ISBN,retailHeadId,number,unitPrice,totalPrice,realDiscount,realPrice,dateTime) values(@saleIdMonomerId,@bookNum,@ISBN,@saleHeadId,@number,@unitPrice,@totalPrice,@realDiscount,@realPrice,@dateTime)";
            string[] param = { "@saleIdMonomerId", "@bookNum", "@ISBN", "@saleHeadId", "@number", "@unitPrice", "@totalPrice", "@realDiscount", "@realPrice", "@dateTime" };
            object[] values = { salemonomer.SaleIdMonomerId, salemonomer.BookNum, salemonomer.ISBN1, salemonomer.SaleHeadId, salemonomer.Number, salemonomer.UnitPrice, salemonomer.TotalPrice, salemonomer.RealDiscount, salemonomer.RealPrice, salemonomer.Datetime };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 查询零售单头
        /// </summary>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>数据集</returns>
        public SaleHead GetHead(string retailHeadId)
        {
            string cmdText = "select allTotalPrice,number,allRealPrice,kindsNum from T_RetailHead where retailHeadId=@retailHeadId";
            string[] param = { "@retailHeadId" };
            object[] values = { retailHeadId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            SaleHead sale = new SaleHead();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sale.AllRealPrice = Convert.ToDouble(ds.Tables[0].Rows[0]["allRealPrice"]);
                    sale.AllTotalPrice = Convert.ToDouble(ds.Tables[0].Rows[0]["allTotalPrice"]);
                    sale.Number = Convert.ToInt32(ds.Tables[0].Rows[0]["number"]);
                    sale.KindsNum = Convert.ToInt32(ds.Tables[0].Rows[0]["kindsNum"]);
                }
                return sale;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 查询单头下的所有单体零售单体
        /// </summary>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>数据集</returns>
        public DataSet GetRetail(string retailHeadId)
        {
            string cmdText = "select retailMonomerId,bookName,bookNum,ISBN,number,unitPrice,realDiscount,totalPrice,realPrice from V_RetailMonomer where retailHeadId=@retailHeadId";
            string[] param = { "@retailHeadId" };
            object[] values = { retailHeadId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }

        /// <summary>
        /// 查询零售单体
        /// </summary>
        /// <param name="retailMonomerId">零售单体ID</param>
        /// <returns>数据集</returns>
        public SaleMonomer GetMonomer(int retailMonomerId,string retailHeadId)
        {
            string cmdText = "select retailHeadId,unitPrice,realDiscount,totalPrice,realPrice,number from V_RetailMonomer where retailMonomerId=@retailMonomerId and retailHeadId=@retailHeadId";
            string[] param = { "@retailMonomerId", "@retailHeadId" };
            object[] values = { retailMonomerId, retailHeadId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds == null || ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                SaleMonomer sale = new SaleMonomer();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sale.UnitPrice = Convert.ToDouble(ds.Tables[0].Rows[i]["unitPrice"]);
                    sale.RealDiscount = Convert.ToDouble(ds.Tables[0].Rows[i]["realDiscount"]);
                    sale.TotalPrice = Convert.ToDouble(ds.Tables[0].Rows[i]["totalPrice"]);
                    sale.RealPrice = Convert.ToDouble(ds.Tables[0].Rows[i]["realPrice"]);
                    sale.Number = Convert.ToInt32(ds.Tables[0].Rows[i]["number"]);
                    sale.SaleHeadId = ds.Tables[0].Rows[i]["retailHeadId"].ToString();
                }
                return sale;
            }
        }

        /// <summary>
        /// 更新零售折扣
        /// </summary>
        /// <param name="realDiscount">折扣</param>
        /// <param name="realPrice">实洋</param>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>受影响行数</returns>
        public int UpdateDiscount(double realDiscount, double realPrice, string retailHeadId)
        {
            string cmdText = "update T_RetailMonomer set realDiscount=@realDiscount,realPrice=@realPrice where retailHeadId=@retailHeadId";
            string[] param = { "@realDiscount", "@realPrice", "@retailHeadId" };
            object[] values = { realDiscount, realPrice, retailHeadId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 更新零售单头实洋
        /// </summary>
        /// <param name="realPrice">实洋</param>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>受影响行数</returns>
        public int UpdateHeadReal(double allRealPrice, string retailHeadId)
        {
            string cmdText = "update T_RetailHead set allRealPrice=@allRealPrice where retailHeadId=@retailHeadId";
            string[] param = { "@allRealPrice", "@retailHeadId" };
            object[] values = { allRealPrice, retailHeadId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 更新零售数量
        /// </summary>
        /// <param name="sale">零售实体对象</param>
        /// <returns>受影响行数</returns>
        public int UpdateNumber(SaleMonomer sale)
        {
            string cmdText = "update T_RetailMonomer set number=@number,totalPrice=@totalPrice,realPrice=@realPrice where retailMonomerId=@retailMonomerId";
            string[] param = { "@number", "totalPrice", "@realPrice", "@retailMonomerId" };
            object[] values = { sale.Number,sale.TotalPrice, sale.RealPrice, sale.SaleIdMonomerId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 更新零售数量
        /// </summary>
        /// <param name="sale">零售实体对象</param>
        /// <returns>受影响行数</returns>
        public int UpdateHeadNumber(SaleHead sale)
        {
            string cmdText = "update T_RetailHead set number=@number,allTotalPrice=@totalPrice,allRealPrice=@realPrice,kindsNum=@kindsNum where retailHeadId=@retailHeadId";
            string[] param = { "@number", "totalPrice", "@realPrice", "@retailHeadId", "@kindsNum" };
            object[] values = { sale.Number, sale.AllTotalPrice, sale.AllRealPrice, sale.SaleHeadId,sale.KindsNum };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 删除单体信息
        /// </summary>
        /// <param name="retailMonomerId">零售单体ID</param>
        /// <returns></returns>
        public int delete(int retailMonomerId,string retailHeadId)
        {
            string cmdText = "delete from T_RetailMonomer where retailMonomerId=@retailMonomerId where retailHeadId=@retailHeadId";
            string[] param = { "@retailMonomerId", "@retailHeadId" };
            object[] values = { retailMonomerId, retailHeadId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 单据完成，修改type
        /// </summary>
        /// <param name="headId"></param>
        /// <returns></returns>
        public int updateType(string headId)
        {
            string cmdText = "update T_RetailHead set state=1,dateTime=@dateTime where retailHeadId=@retailHeadId";
            string[] param = { "@retailHeadId", "@dateTime" };
            object[] values = { headId, DateTime.Now };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
    }
}
