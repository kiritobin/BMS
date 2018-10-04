using bms.DBHelper;
using bms.Model;
using MySql.Data.MySqlClient;
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
        public DataTable SelectSingleHead(string singleHeadId)
        {
            string cmdText = "select singleHeadId,time,userName,regionName,allBillCount,allTotalPrice,allRealPrice from V_SingleHead where singleHeadId=@singleHeadId";
            string[] param = { "@singleHeadId" };
            object[] values = { singleHeadId};
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
        /// 获取出库单体的所有信息
        /// </summary>
        /// <param name="type">1为入库，0为出库，2为退货</param>
        /// <returns></returns>
        public DataTable SelectMonomers(string singleHeadId)
        {
            string cmdText = "select number,totalPrice,realPrice from T_Monomers where singleHeadId=@singleHeadId";
            string[] param = { "@singleHeadId" };
            object[] values = { singleHeadId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
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
        /// 添加单头信息
        /// </summary>
        /// <param name="single">单头实体对象</param>
        /// <returns></returns>
        public int insertHead(SingleHead single)
        {
            string cmdText = "insert into T_SingleHead(singleHeadId,time,regionId,userId,type) values(@singleHeadId,@time,@regionId,@userId,@type)";
            string[] param = { "@singleHeadId", "@time","@regionId","@userId", "@type" };
            object[] values = { single.SingleHeadId, single.Time, single.Region.RegionId, single.User.UserId,single.Type };
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
            string cmdText = "insert into T_Monomers(monId,singleHeadId,bookNum,ISBN,number,uPrice,totalPrice,realPrice,discount,type) values(@monId,@singleHeadId,@bookNum,@ISBN,@number,@uPrice,@totalPrice,@realPrice,@discount,@type)";
            string[] param = { "@monId", "@singleHeadId", "@bookNum", "@ISBN", "@number", "@uPrice", "@totalPrice", "@realPrice", "@discount", "@type" };
            object[] values = { monomers.MonomersId, monomers.SingleHeadId.SingleHeadId,monomers.BookNum.BookNum, monomers.Isbn.Isbn, monomers.Number, monomers.UPrice.Price, monomers.TotalPrice, monomers.RealPrice, monomers.Discount, monomers.Type };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 更新单头数量，码洋，实洋
        /// </summary>
        /// <param name="single"></param>
        /// <returns></returns>
        public int updateHead(SingleHead single)
        {
            string cmdTexts = "update T_SingleHead set allBillCount=@allBillCount,allTotalPrice=@allTotalPrice,allRealPrice=@allRealPrice where singleHeadId=@singleHeadId";
            string[] parames = { "@singleHeadId", "@allBillCount", "@allTotalPrice", "@allRealPrice" };
            object[] value = { single.SingleHeadId, single.AllBillCount, single.AllTotalPrice, single.AllRealPrice };
            int row = db.ExecuteNoneQuery(cmdTexts, parames, value);
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
        public long SelectBymonId(string singleHeadId)
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
        /// 通过书号查询存不存在
        /// </summary>
        /// <param name="bookNum"></param>
        /// <returns></returns>
        public int SelectBybookNum(string bookNum)
        {
            string comText = "select COUNT(monId) from T_Monomers where bookNum=@bookNum";
            string[] param = { "@bookNum" };
            object[] values = { bookNum };
            int row = Convert.ToInt32(db.ExecuteScalar(comText, param, values));
            return row;
        }
        /// <summary>
        /// 假删除单体
        /// </summary>
        /// <param name="singleHeadId">单头Id</param>
        /// <param name="monId">单体ID</param>
        /// <returns>受影响行数</returns>
        public int deleteMonomer(string singleHeadId,int monId,int type)
        {
            string cmdText = "update T_Monomers set deleteState=1 where type=@type and singleHeadId=@singleHeadId and monId=@monId";
            string[] param = { "@singleHeadId", "@monId","@type" };
            object[] values = { singleHeadId, monId ,type};
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 读取isbn
        /// </summary>
        /// <returns></returns>
        public DataTable getISBNbook()
        {
            string cmdText = "select ISBN,bookName from T_BookBasicData";
            DataSet ds = db.FillDataSet(cmdText, null, null);
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
        /// 获取折扣
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public Monomers getDiscount()
        {
            string sql = "select disCount from T_NewBook";
            Monomers monomers = new Monomers();
            MySqlDataReader reader = db.ExecuteReader(sql, null, null);
            while (reader.Read())
            {
                monomers.Discount = reader.GetDouble(0);
            }
            reader.Close();
            return monomers; ;
        }

        /// <summary>
        /// 更新折扣
        /// </summary>
        /// <param name="disCount"></param>
        /// <returns></returns>
        public int updateDiscount(double disCount)
        {
            string cmdText = "update T_NewBook set disCount=@disCount";
            string[] param = { "@disCount" };
            object[] values = { disCount };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
    }
}
