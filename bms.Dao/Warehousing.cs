using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class Warehousing
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取所有入库单头数据的ISBN，单价，书名
        /// </summary>
        /// <returns></returns>
        public DataTable Select()
        {
            string comText = "select ISBN,customerId from T_SingleHead";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                return dt;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取出库单头的所有信息
        /// </summary>
        /// <param name="type">1为入库，0为出库，2为退货</param>
        /// <returns></returns>
        public DataTable SelectSingleHead(int type,string singleHeadId)
        {
            string cmdText = "select singleHeadId,time,userName,regionName,allBillCount,allTotalPrice,allRealPrice from V_SingleHead where singleHeadId=@singleHeadId and type=@type";
            string[] param = { "@type", "@singleHeadId" };
            object[] values = { type,singleHeadId};
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if(ds != null || ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                return dt;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 添加单体信息
        /// </summary>
        /// <param name="monomers">单体实体对象</param>
        /// <returns></returns>
        public int insert(Monomers monomers)
        {
            string cmdText = "insert into T_Monomers(monId,singleHeadId,ISBN,number,uPrice,totalPrice,realPrice,discount,goodsShelvesId,type) values(@monId,@singleHeadId,@ISBN,@number,@uPrice,@totalPrice,@realPrice,@discount,@goodsShelvesId,@type)";
            string[] param = { "@monId", "@singleHeadId", "@ISBN", "@number", "@uPrice", "@totalPrice", "@realPrice", "@discount", "@goodsShelvesId", "@type" };
            object[] values = { monomers.MonomersId, monomers.SingleHeadId.SingleHeadId, monomers.Isbn.Isbn, monomers.Number, monomers.UPrice.Price, monomers.TotalPrice, monomers.RealPrice, monomers.Discount, monomers.GoodsShelvesId.GoodsShelvesId, monomers.Type };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
    }
}
