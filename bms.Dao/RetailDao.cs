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
        public DataSet getAllTime()
        {
            string cmdText = "select retailHeadId,dateTime from T_RetailHead ORDER BY dateTime desc";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            return ds;
        }

        /// <summary>
        /// 添加零售单头信息
        /// </summary>
        /// <param name="salehead"></param>
        /// <returns></returns>
        public int InsertRetail(SaleHead salehead)
        {
            string cmdText = "insert into T_RetailHead(retailHeadId,userId,regionId,dateTime,kindsNum,number,allTotalPrice,allRealPrice) values(@retailHeadId,@userId,@regionId,@dateTime,@kindsNum,@number,@allTotalPrice,@allRealPrice)";
            string[] param = { "@retailHeadId", "@userId", "@regionId", "@dateTime", "@kindsNum", "@number", "@allTotalPrice", "@allRealPrice" };
            object[] values = { salehead.SaleHeadId, salehead.UserId, salehead.RegionId, salehead.DateTime, salehead.KindsNum, salehead.Number, salehead.AllTotalPrice, salehead.AllRealPrice };
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
    }
}
