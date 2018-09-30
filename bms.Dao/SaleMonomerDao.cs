using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class SaleMonomerDao
    {

        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 查询所有销售单体
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet Select()
        {
            string comText = "select userName,customerName,saleTaskId,saleHeadId,saleMonomerId,bookNum,ISBN,bookName,unitPrice,number,totalPrice,realDiscount,realPrice,dateTime from V_SaleMonomer";
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
        /// 添加销售单体
        /// </summary>
        /// <param name="task">销售单体实体</param>
        /// <returns>受影响行数</returns>
        public int Insert(SaleMonomer salemonomer)
        {
            string cmdText = "insert into T_SaleMonomer(bookNo,ISBN,saleHeadId,number,unitPrice,number,totalPrice,realDiscount,realPrice,dateTime) values(@bookNo,@ISBN,@saleHeadId,@number,@unitPrice,@number,@totalPrice,@realDiscount,@realPrice,@dateTime)";
            string[] param = { "@bookNo","@ISBN","@saleHeadId","@number","@unitPrice","@number","@totalPrice","@realDiscount","@realPrice","@dateTime" };
            object[] values = { salemonomer.BookNum, salemonomer.ISBN1, salemonomer.SaleHeadId, salemonomer.Number, salemonomer.UnitPrice, salemonomer.TotalPrice, salemonomer.RealDiscount, salemonomer.RealPrice, salemonomer.Datetime };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            if (row > 0)
            {
                return row;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 删除销售单体
        /// </summary>
        /// <param name="saleTaskId">销售单体ID</param>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns>受影响行数</returns>
        public int Delete(string saleIdMonomerId, string saleHeadId)
        {
            string cmdText = "update T_SaleMonomer set deleteState = 1 where saleIdMonomerId=@saleIdMonomerId and saleHeadId=@saleHeadId";
            String[] param = { "@saleTaskId", @saleHeadId };
            String[] values = { saleIdMonomerId, saleHeadId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
    }
}
