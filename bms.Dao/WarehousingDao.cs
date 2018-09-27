using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class WarehousingDao
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
        /// 添加单头信息
        /// </summary>
        /// <param name="single">单头实体对象</param>
        /// <returns></returns>
        public int insertHead(SingleHead single)
        {
            string cmdText = "insert into T_SingleHead(singleHeadId,time,regionId,userId,allBillCount,allTotalPrice,allRealPrice,type) values(@singleHeadId,@time,@regionId,@userId,@allBillCount,@allTotalPrice,@allRealPrice,@type)";
            string[] param = { "@singleHeadId", "@time","@regionId","@userId","@allBillCount","@allTotalPrice","@allRealPrice", "@type" };
            object[] values = { single.SingleHeadId, single.Time, single.Region.RegionId, single.User.UserId, single.AllBillCount, single.AllTotalPrice, single.AllRealPrice, single.Type };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 添加单体信息
        /// </summary>
        /// <param name="monomers">单体实体对象</param>
        /// <returns></returns>
        public int insertMono(Monomers monomers)
        {
            string cmdText = "insert into T_Monomers(monId,singleHeadId,ISBN,number,uPrice,totalPrice,realPrice,discount,goodsShelvesId,type) values(@monId,@singleHeadId,@ISBN,@number,@uPrice,@totalPrice,@realPrice,@discount,@goodsShelvesId,@type)";
            string[] param = { "@monId", "@singleHeadId", "@ISBN", "@number", "@uPrice", "@totalPrice", "@realPrice", "@discount", "@goodsShelvesId", "@type" };
            object[] values = { monomers.MonomersId, monomers.SingleHeadId.SingleHeadId, monomers.Isbn.Isbn, monomers.Number, monomers.UPrice.Price, monomers.TotalPrice, monomers.RealPrice, monomers.Discount, monomers.GoodsShelvesId.GoodsShelvesId, monomers.Type };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 假删除单头信息
        /// </summary>
        /// <param name="singleHeadId">单头id</param>
        /// <returns></returns>
        public int deleteHead(string singleHeadId,int type)
        {
            string cmdText = "update T_SingleHead set deleteState=1 where type=@type and singleHeadId=@singleHeadId";
            string[] param = { "@singleHeadId", "@type" };
            object[] values = { singleHeadId, type };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 查看单头数量
        /// </summary>
        /// <param name="type">类型，0：出库，1：入库，2：退货</param>
        /// <returns></returns>
        public int countHead(int type)
        {
            string cmdText = "select count(singleHeadId) from T_SingleHead where type=@type";
            string[] param = { "@type" };
            object[] values = { type };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            return row;
        }
        /// <summary>
        /// 根据单体id查询已存在行数
        /// </summary>
        /// <returns>行数</returns>
        public long SelectBymonId(long singleHeadId)
        {
            string comText = "select COUNT(monId) from T_Monomers where singleHeadId=@singleHeadId";
            string[] param = { "@singleHeadId" };
            object[] values = { singleHeadId };
            int row =Convert.ToInt32( db.ExecuteScalar(comText, param, values));
            if (row > 0)
            {
                return row;
            }
            else
            {
                return row = 0;
            }
        }
        /// <summary>
        /// 假删除单体
        /// </summary>
        /// <param name="singleHeadId">单头Id</param>
        /// <param name="monId">单体ID</param>
        /// <returns>受影响行数</returns>
        public int deleteMonomer(string singleHeadId,int monId)
        {
            string cmdText = "update T_Monomers set deleteState=1 where type=2 and singleHeadId=@singleHeadId and monId=@monId";
            string[] param = { "@singleHeadId" };
            object[] values = { singleHeadId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
    }
}
