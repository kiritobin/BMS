﻿using bms.DBHelper;
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
        /// 查询该单头下是否有单体
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>行数</returns>

        public int SelectBySaleHeadId(string saleHeadId)
        {
            string cmdText = "select count(saleIdMonomerId) from T_SaleMonomer where saleHeadId=@saleHeadId";
            string[] param = { "@saleHeadId" };
            object[] values = { saleHeadId };
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
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
        /// 统计种数
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">单头id</param>
        /// <returns></returns>
        public int getkinds(string saleTaskId, string saleHeadId)
        {
            string cmdText = "select bookNum,number from V_SaleMonomer where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
            string[] param = { "@saleTaskId", "@saleHeadId" };
            object[] values = { saleTaskId, saleHeadId };
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
                    sltemp += float.Parse(dr[j]["number"].ToString().Trim());
                }
                if (sltemp > 0)
                {
                    zl++;
                }
            }
            return zl;
        }
        /// <summary>
        /// 根据销售单头ID查询该销售单的状态
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns>数据集</returns>
        public DataSet saleheadstate(string saleTaskId,string saleHeadId)
        {
            string cmdText = "select state from T_SaleHead where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
            string[] param = { "saleTaskId", "@saleHeadId" };
            object[] values = { saleTaskId, saleHeadId };
            DataSet ds = db.FillDataSet(cmdText, param, values);

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
        /// 根据销售单头获取单体
        /// </summary>
        /// <param name="saleHeadId">单头ID</param>
        /// <returns>单体数据集</returns>
        public DataSet SelectMonomers(string saleHeadId)
        {
            string cmdText = "select number,totalPrice,realPrice from T_SaleMonomer where saleHeadId=@saleHeadId";
            string[] param = { "@saleHeadId" };
            object[] values = { saleHeadId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
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
            string cmdText = "insert into T_SaleMonomer(saleIdMonomerId,bookNum,ISBN,saleHeadId,number,unitPrice,totalPrice,realDiscount,realPrice,dateTime,alreadyBought) values(@saleIdMonomerId,@bookNum,@ISBN,@saleHeadId,@number,@unitPrice,@totalPrice,@realDiscount,@realPrice,@dateTime,@alreadyBought)";
            string[] param = { "@saleIdMonomerId", "@bookNum", "@ISBN", "@saleHeadId", "@number", "@unitPrice", "@totalPrice", "@realDiscount", "@realPrice", "@dateTime", "alreadyBought" };
            object[] values = { salemonomer.SaleIdMonomerId, salemonomer.BookNum, salemonomer.ISBN1, salemonomer.SaleHeadId, salemonomer.Number, salemonomer.UnitPrice, salemonomer.TotalPrice, salemonomer.RealDiscount, salemonomer.RealPrice, salemonomer.Datetime, salemonomer.AlreadyBought };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
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
        /// <summary>
        /// 更新销售单体
        /// </summary>
        /// <param name="salemonomer">销售单体实体</param>
        /// <returns></returns>
        public int Update(SaleMonomer salemonomer)
        {
            string cmdText = "update T_SaleMonomer set number=@number or uPrice=@uPrice or totalPice=@totalPrice or realPrice=@realPrice or discount=@discount or type=@type";
            string[] param = { "@number", "@uPrice", "@totalPrice", "@realPrice", "@discount", "@type" };
            object[] values = { salemonomer.Number, salemonomer.UnitPrice, salemonomer.TotalPrice, salemonomer.RealDiscount, salemonomer.RealDiscount, salemonomer.Type };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 根据销售单头ID真删除销售单头
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns>受影响行数</returns>
        public int realDelete(string saleTaskId, string saleHeadId)
        {
            string cmdText = "delete from T_SaleHead where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
            String[] param = { "@saleTaskId", "@saleHeadId" };
            String[] values = { saleTaskId, saleHeadId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 更新单头 总数量 品种数，码洋，实洋
        /// </summary>
        /// <param name="salehead">单头实体</param>
        /// <returns></returns>
        public int updateHead(SaleHead salehead)
        {
            string cmdTexts = "update T_SaleHead set kindsNum=@kindsNum,number=@number,allTotalPrice=@allTotalPrice,allRealPrice=@allRealPrice where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
            string[] parames = { "@kindsNum", "@number", "@allTotalPrice", "@allRealPrice", "@saleTaskId", "@saleHeadId" };
            object[] value = { salehead.KindsNum, salehead.Number, salehead.AllTotalPrice, salehead.AllRealPrice, salehead.SaleTaskId, salehead.SaleHeadId };
            int row = db.ExecuteNoneQuery(cmdTexts, parames, value);
            return row;
        }
        /// <summary>
        /// 更新销售单头状态
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">销售单头</param>
        /// <param name="state">状态 0新创单据 1采集中 2已完成</param>
        /// <returns>受影响行数</returns>
        public int updateHeadstate(string saleTaskId, string saleHeadId, int state)
        {
            string cmdTexts = "update T_SaleHead set state=@state where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
            string[] parames = { "@state", "@saleTaskId", "@saleHeadId" };
            object[] value = { state, saleTaskId, saleHeadId };
            int row = db.ExecuteNoneQuery(cmdTexts, parames, value);
            return row;
        }


        /// <summary>
        /// 通过书号查询在单体中是否存在记录
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="type">单体类型（0：出库，1：入库，2：退货）</param>
        /// <returns></returns>
        public int SelectBybookNum(string retailHeadId, string bookNum)
        {
            string comText = "select COUNT(monId) from T_RetailMonomer where bookNum=@bookNum and retailHeadId=@retailHeadId";
            string[] param = { "@bookNum", "@retailHeadId" };
            object[] values = { bookNum, retailHeadId };
            int row = Convert.ToInt32(db.ExecuteScalar(comText, param, values));
            return row;
        }
        /// <summary>
        /// 判断该书号是否是第一次添加
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public int SelectnumberBysaletask(string saleTaskId, string bookNum)
        {
            string comText = "select COUNT(bookNum) from V_SaleMonomer where saleTaskId=@saleTaskId and bookNum=@bookNum";
            string[] param = { "@saleTaskId", "@bookNum" };
            object[] values = { saleTaskId, bookNum };
            int row = Convert.ToInt32(db.ExecuteScalar(comText, param, values));
            return row;
        }

        /// <summary>
        /// 获取该书号在该销售任务下的已购数量
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <param name="bookNum">书号</param>
        /// <returns>返回数据集</returns>
        public DataSet SelectCountBybookNum(string saleTaskId, string bookNum)
        {
            string comText = "select alreadyBought from V_SaleMonomer where saleTaskId=@saleTaskId and bookNum=@bookNum order by alreadyBought desc;";
            string[] param = { "@saleTaskId", "@bookNum" };
            object[] values = { saleTaskId, bookNum };
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
        /// 获取销售单体中的数据统计
        /// </summary>
        /// <returns>返回数据集</returns>
        public DataSet SelectBookRanking()
        {
            string sql = "select bookNum1,bookName,unitPrice,allNum,allTotalPrice from V_BookRanking limit 0,10";
            DataSet ds = db.FillDataSet(sql, null, null);
            return ds;
        }
        /// <summary>
        /// 获取销售单体最后一条数据
        /// </summary>
        /// <param name="saleTaskId">销售id</param>
        /// <param name="saleHeadId">销售单头id</param>
        /// <returns>数据集</returns>
        public DataSet GetSaleby(string saleTaskId, string saleHeadId)
        {
            string cmdtext = "select bookNum,bookName,ISBN,unitPrice,number,realDiscount,realPrice,dateTime,alreadyBought from V_SaleMonomer where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId order by dateTime desc";
            string[] param = { "@saleTaskId", "@saleHeadId" };
            object[] values = { saleTaskId, saleHeadId };
            DataSet ds = db.FillDataSet(cmdtext,param,values);
            if (ds != null)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
    }
}
