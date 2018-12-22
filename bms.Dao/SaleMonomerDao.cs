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
                    float count = float.Parse(dr[j]["number"].ToString().Trim());
                    sltemp += float.Parse(dr[j]["number"].ToString().Trim());
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
        /// 获取书籍种类
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public int getkindsGroupBy(string strWhere, string type, string state, string time)
        {
            string cmdText = "";
            string startTime = "";
            string endTime = "";
            if (time != "" && time != null)
            {
                string[] sArray = time.Split('至');
                startTime = sArray[0];
                endTime = sArray[1];
            }
            if ((state != null && state != "") || (time != "" && time != null))
            {
                if ((state == "1"))
                {
                    if ((time != "" && time != null) && (state != null && state != ""))
                    {
                        cmdText = "select bookNum, SUM(number) as 数量 from V_SaleMonomer where " + type + " = @strWhere and (state=1 or state=2) and dateTime BETWEEN'" + startTime + "' and '" + endTime + "' GROUP BY bookNum HAVING 数量!=0";
                    }
                    else
                    {
                        cmdText = "select bookNum, SUM(number) as 数量 from V_SaleMonomer where " + type + " = @strWhere and (state=1 or state=2) GROUP BY bookNum HAVING 数量!=0";
                    }
                }
                else
                {
                    if ((time != "" && time != null) && (state != null && state != ""))
                    {
                        cmdText = "select bookNum, SUM(number) as 数量 from V_SaleMonomer where " + type + " = @strWhere and state=" + state + " and dateTime BETWEEN'" + startTime + "' and '" + endTime + "' GROUP BY bookNum HAVING 数量!=0";
                    }
                    else
                    {
                        if ((time != "" && time != null))
                        {
                            cmdText = "select bookNum, SUM(number) as 数量 from V_SaleMonomer where " + type + " = @strWhere and dateTime BETWEEN'" + startTime + "' and '" + endTime + "' GROUP BY bookNum HAVING 数量!=0";
                        }
                        else
                        {
                            cmdText = "select bookNum, SUM(number) as 数量 from V_SaleMonomer where " + type + " = @strWhere and state=" + state + " GROUP BY bookNum HAVING 数量!=0";
                        }
                    }
                }
            }
            else
            {
                cmdText = "select bookNum, SUM(number) as 数量 from V_SaleMonomer where " + type + " = @strWhere GROUP BY bookNum HAVING 数量!=0";
            }
            string[] param = { "@strWhere", "@state" };
            object[] values = { strWhere, state };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            int allCount = ds.Tables[0].Rows.Count;
            return allCount;
        }
        /// <summary>
        /// 根据销售单头ID查询该销售单的状态
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns>数据集</returns>
        public DataSet saleheadstate(string saleTaskId, string saleHeadId)
        {
            string cmdText = "select state from T_SaleHead where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
            string[] param = { "@saleTaskId", "@saleHeadId" };
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
        /// 入库查询
        /// </summary>
        /// <param name="singleHeadId"></param>
        /// <returns></returns>
        public DataSet checkStock(string singleHeadId)
        {
            string cmdText = "select singleHeadId,ISBN,number,uPrice,discount,totalPrice,realPrice,shelvesName,bookName from V_Monomer where singleHeadId=@singleHeadId and deleteState=0 order by singleHeadId";
            string[] param = { "@singleHeadId" };
            object[] values = { singleHeadId };
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
            string cmdText = "insert into T_SaleMonomer(saleIdMonomerId,bookNum,ISBN,saleHeadId,number,unitPrice,totalPrice,realDiscount,realPrice,dateTime,alreadyBought,saleTaskId) values(@saleIdMonomerId,@bookNum,@ISBN,@saleHeadId,@number,@unitPrice,@totalPrice,@realDiscount,@realPrice,@dateTime,@alreadyBought,@saleTaskId)";
            string[] param = { "@saleIdMonomerId", "@bookNum", "@ISBN", "@saleHeadId", "@number", "@unitPrice", "@totalPrice", "@realDiscount", "@realPrice", "@dateTime", "@alreadyBought", "@saleTaskId" };
            object[] values = { salemonomer.SaleIdMonomerId, salemonomer.BookNum, salemonomer.ISBN1, salemonomer.SaleHeadId, salemonomer.Number, salemonomer.UnitPrice, salemonomer.TotalPrice, salemonomer.RealDiscount, salemonomer.RealPrice, salemonomer.Datetime, salemonomer.AlreadyBought, salemonomer.SaleTaskId };
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
        /// 根据销售单头ID真删除销售单头包括单体
        /// </summary>
        /// <param name="saleHeadId">销售单头ID</param>
        /// <returns>0不成功</returns>
        public int realDeleteHeadAndMon(string saleTaskId, string saleHeadId)
        {
            string cmdMon = "delete from t_salemonomer where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
            String[] param1 = { "@saleTaskId", "@saleHeadId" };
            String[] values2 = { saleTaskId, saleHeadId };
            int monrow = db.ExecuteNoneQuery(cmdMon, param1, values2);
            if (monrow != 0)
            {
                string cmdText = "delete from T_SaleHead where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
                String[] param = { "@saleTaskId", "@saleHeadId" };
                String[] values = { saleTaskId, saleHeadId };
                int headrow = db.ExecuteNoneQuery(cmdText, param, values);
                if (monrow != 0)
                {
                    return headrow;
                }
                else
                {
                    return headrow = 0;
                }
            }
            else
            {
                return monrow = 0;
            }
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
        public int wechatupdateHead(SaleHead salehead)
        {
            string cmdTexts = "update T_SaleHead set state=3,kindsNum=@kindsNum,number=@number,allTotalPrice=@allTotalPrice,allRealPrice=@allRealPrice where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId";
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
        public DataSet SelectBookRanking(DateTime startTime, DateTime endTime, string regionName)
        {
            string sql = @"select bookNum,unitPrice,sum(number) as allNum,sum(totalPrice) as allPrice,bookName,dateTime from v_salemonomer where (state=1 or state=2) and dateTime BETWEEN @startTime and @endTime and regionName=@regionName GROUP BY bookNum ORDER BY allNum desc LIMIT 0,10;";
            string[] param = { "@startTime", "@endTime", "@regionName" };
            object[] values = { startTime, endTime, regionName };
            DataSet ds = db.FillDataSet(sql, param, values);
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
            DataSet ds = db.FillDataSet(cmdtext, param, values);
            if (ds != null)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 更新已购数量
        /// </summary>
        /// <param name="alreadyBought">数量</param>
        /// <param name="bookNum">书号</param>
        /// <param name="saleId">销售任务id</param>
        /// <returns>受影响行数</returns>
        public int updateAlreadyBought(int alreadyBought, string bookNum, string saleId)
        {
            string cmdText = "update T_SaleMonomer set alreadyBought=@alreadyBought where bookNum=@bookNum and saleTaskId=@saleId";
            //string cmdText = "update V_SaleMonomer set alreadyBought=@alreadyBought where bookNum=@bookNum and saleTaskId=@saleId";
            string[] param = { "@alreadyBought", "@bookNum", "@saleId" };
            object[] values = { alreadyBought, bookNum, saleId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 获取销售单头的状态
        /// </summary>
        /// <param name="saleHeadId">销售单头</param>
        /// <returns>返回销售单头状态</returns>
        public string getsaleHeadState(string saleHeadId, string saleTaskId)
        {
            string cmdtext = "select state from T_SaleHead where saleHeadId=@saleHeadId and saleTaskId=@saleTaskId";
            string[] param = { "@saleHeadId", "@saleTaskId" };
            object[] values = { saleHeadId, saleTaskId };
            DataSet ds = db.FillDataSet(cmdtext, param, values);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                string state = ds.Tables[0].Rows[0]["state"].ToString();
                return state;
            }
            return null;
        }
        /// <summary>
        ///根据销售任务id
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns>返回销售单头状态</returns>
        public string getsaleHeadStatesBysaleTaskId(string saleTaskId)
        {
            string cmdtext = "select state from T_SaleHead where saleTaskId=@saleTaskId and state=3";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            string state;
            int row = db.ExecuteNoneQuery(cmdtext, param, values);
            if (row <= 0)
            {
                state = "1";
            }
            else
            {
                state = "3";
            }
            return state;
        }
        //SELECT SUM(number) FROM T_SaleMonomer WHERE saleHeadId='XS20181012000005'
        /// <summary>
        /// 获取该单头id下的书本数量
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>结果</returns>
        public int getsBookNumber(string saleHeadId)
        {
            string cmdtext = "select sum(number) from T_SaleMonomer where saleHeadId=@saleHeadId";
            string[] param = { "@saleHeadId" };
            object[] values = { saleHeadId };
            int sum = Convert.ToInt32(db.ExecuteScalar(cmdtext, param, values));
            if (sum > 0)
            {
                return sum;
            }
            else
            {
                return sum = 0;
            }
        }
        //SELECT SUM(number) FROM T_SaleMonomer WHERE saleHeadId='XS20181012000005'
        /// <summary>
        /// 获取该单头id下的书本数量
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>结果</returns>
        public int getsBookNumberSum(string saleHeadId, string saleId)
        {
            string cmdtext = "select sum(number) from T_SaleMonomer where saleHeadId=@saleHeadId and saleTaskId=@saleId";
            string[] param = { "@saleHeadId", "@saleId" };
            object[] values = { saleHeadId, saleId };
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
        public double getsBookTotalPrice(string saleHeadId, string saleId)
        {
            string cmdtext = "select sum(totalPrice) from T_SaleMonomer where saleHeadId=@saleHeadId and saleTaskId=@saleId";
            string[] param = { "@saleHeadId", "@saleId" };
            object[] values = { saleHeadId, saleId };
            string sumstring = db.ExecuteScalar(cmdtext, param, values).ToString();
            double sum;
            if (sumstring == "" || sumstring == null)
            {
                return sum = 0;
            }
            else
            {
                return sum = double.Parse(sumstring);
            }
        }
        /// <summary>
        /// 获取该单头id下的实洋
        /// </summary>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>结果</returns>
        public double getsBookRealPrice(string saleHeadId, string saleId)
        {
            string cmdtext = "select sum(realPrice) from T_SaleMonomer where saleHeadId=@saleHeadId and saleTaskId=@saleId";
            string[] param = { "@saleHeadId", "@saleId" };
            object[] values = { saleHeadId, saleId };
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
        /// 计算销售单头
        /// </summary>
        /// <param name="saleHeadId">销售单头id</param>
        /// <param name="saleId">销售任务ID</param>
        /// <returns></returns>
        public DataSet calculationSaleHead(string saleHeadId, string saleId)
        {
            string cmdtext = @"SELECT sum(A.allNum) as 数量,sum(A.alltotalPrice) as 总码洋,sum(A.allrealPrice) as 总实洋 from((select bookNum, ISBN, sum(number) as allNum, sum(totalPrice) as alltotalPrice, sum(realPrice) as allrealPrice from T_SaleMonomer where saleHeadId = @saleHeadId and saleTaskId = @saleId GROUP BY booknum, ISBN HAVING allNum != 0) as A)";
            //string cmdtext = "select sum(realPrice) from T_SaleMonomer where saleHeadId=@saleHeadId and saleTaskId=@saleId";
            string[] param = { "@saleHeadId", "@saleId" };
            object[] values = { saleHeadId, saleId };
            DataSet ds = db.FillDataSet(cmdtext, param, values);
            return ds;
        }
        /// <summary>
        /// 获取该书籍在此销售单头中的已购数量
        /// </summary>
        /// <param name="saleHeadId">销售单头</param>
        /// <param name="saleId">销售任务</param>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public DataSet getsalemonDetail(string saleHeadId, string saleId, string bookNum)
        {
            string cmdtext = "select bookNum,ISBN,sum(number) as number from v_salemonomer where saleTaskId=@saleId and saleHeadId=@saleHeadId and bookNum=@bookNum GROUP BY bookNum,ISBN;";
            //string cmdtext = "select sum(realPrice) from T_SaleMonomer where saleHeadId=@saleHeadId and saleTaskId=@saleId";
            string[] param = { "@saleHeadId", "@saleId", "@bookNum" };
            object[] values = { saleHeadId, saleId, bookNum };
            DataSet ds = db.FillDataSet(cmdtext, param, values);
            return ds;
        }
        /// <summary>
        /// 单头id，销售任务id，获取单体信息 group by
        /// </summary>
        /// <param name="saleId">销售任务id</param>
        /// <param name="saleHeadId">单头id</param>
        /// <returns>数据集</returns>
        public DataSet getSalemonBasic(string saleId, string saleHeadId)
        {
            string cmdtext = "select bookNum,bookName,ISBN,unitPrice,realDiscount,sum(number) as allnumber ,sum(realPrice) as allrealPrice from V_SaleMonomer where saleTaskId=@saleId and saleHeadId=@saleHeadId group by bookNum,bookName,ISBN,unitPrice,realDiscount;";
            string[] param = { "@saleId", "@saleHeadId" };
            object[] values = { saleId, saleHeadId };
            DataSet ds = db.FillDataSet(cmdtext, param, values);
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
        /// 获取该书在销售单头下的总数量
        /// </summary>
        /// <param name="saleId">销售任务id</param>
        /// <param name="saleHeadId">销售单头id</param>
        /// <param name="bookNum">书号</param>
        /// <returns>数量</returns>
        public int getSalemonBookNumber(string saleId, string saleHeadId, string bookNum)
        {
            string cmdtext = "select sum(number) from T_SaleMonomer where saleTaskId=@saleId and saleHeadId=@saleHeadId and bookNum=@bookNum";
            string[] param = { "@saleId", "@saleHeadId", "@bookNum" };
            object[] values = { saleId, saleHeadId, bookNum };
            int number = Convert.ToInt32(db.ExecuteScalar(cmdtext, param, values).ToString());
            return number;
        }
        /// <summary>
        /// 获取该书在销售单头下的总实洋
        /// </summary>
        /// <param name="saleId">销售任务id</param>
        /// <param name="saleHeadId">销售单头id</param>
        /// <param name="bookNum">书号</param>
        /// <returns>总实洋</returns>
        public double getSalemonBookRealPrice(string saleId, string saleHeadId, string bookNum)
        {
            string cmdtext = "select sum(realPrice) from T_SaleMonomer where saleTaskId=@saleId and saleHeadId=@saleHeadId and bookNum=@bookNum";
            string[] param = { "@saleId", "@saleHeadId", "@bookNum" };
            object[] values = { saleId, saleHeadId, bookNum };
            double realPrice = double.Parse(db.ExecuteScalar(cmdtext, param, values).ToString());
            return realPrice;
        }

        /// <summary>
        /// 根据书号和销售任务id获取该书的已购数量
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns>数量</returns>
        public int getBookNumberSumByBookNum(string bookNum, string saleTaskId)
        {
            string cmdtext = "select sum(number) from V_SaleMonomer where bookNum=@bookNum and saleTaskId=@saleTaskId;";
            string[] param = { "@bookNum", "@saleTaskId" };
            object[] values = { bookNum, saleTaskId };
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
        /// 微信汇总
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>数据集</returns>
        public DataSet wechatSummary(string condition)
        {
            string cmdtext = @"select count(a.bookNum) as kinds,sum(a.allrealPrice) as allrealPrice,sum(a.totalPrice) as totalPrice,sum(allnumber) as allnumber from (select bookNum,bookName,ISBN,unitPrice,realDiscount,sum(number) as allnumber ,sum(realPrice) as allrealPrice ,sum(totalPrice) as totalPrice from V_salemonomer where  " + condition + ") as a";

            DataSet ds = db.FillDataSet(cmdtext, null, null);
            return ds;
        }
        /// <summary>
        /// 根据销售任务id，销售单头ID，和书号，查询该销售单的已购数
        /// </summary>
        /// /// <param name="saleTaskId">销售任务id</param>
        /// <param name="saleHeadId">销售单头ID</param>
        /// /// <param name="bookNum">书号</param>
        /// <returns>数据集</returns>
        public int getSaleNumber(string saleTaskId, string saleHeadId, string bookNum)
        {
            string cmdText = "select sum(number) from T_SaleMonomer where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId and bookNum=@bookNum";
            string[] param = { "@saleTaskId", "@saleHeadId", "@bookNum" };
            object[] values = { saleTaskId, saleHeadId, bookNum };
            string ds = db.ExecuteScalar(cmdText, param, values).ToString();
            int sum;
            if (ds != null || ds != "")
            {

                return sum = int.Parse(ds);
            }
            else
            {
                return sum = 0;
            }
        }
        /// <summary>
        /// 根据销售单头ID和销售任务id获取单体数量
        /// </summary>
        /// <param name="HeadId">销售单头ID</param>
        /// <param name="saletaskId">销售任务id</param>
        /// <returns></returns>
        public int SelectcountbyHeadID(string HeadId, string saletaskId)
        {
            string comText = "select COUNT(saleIdMonomerId) from t_salemonomer where saleHeadId=@HeadId and saleTaskId=@saletaskId";
            string[] param = { "@HeadId", "@saletaskId" };
            object[] values = { HeadId, saletaskId };
            int row = Convert.ToInt32(db.ExecuteScalar(comText, param, values));
            return row;
        }

        /// <summary>
        /// 团采统计
        /// </summary>
        /// <returns></returns>
        public DataSet GroupCount(DateTime startTime, DateTime endTime, string regionName)
        {
            //string sql = "select count(*) as totalBooks,sum(allCount) as allCount,sum(allPrice) as allPrice from ((select bookNum,sum(number) as allCount,sum(totalPrice) as allPrice from v_salemonomer where (state=1 or state=2) GROUP BY bookNum ORDER BY allCount desc) as temp)";
            string sql = @"select count(*) as totalBooks,sum(allCount) as allCount,sum(allPrice) as allPrice from ((select dateTime,bookNum,sum(number) as allCount,sum(totalPrice) as allPrice,regionName from v_salemonomer where (state=1 or state=2) and dateTime between @startTime and  @endTime and regionName=@regionName GROUP BY bookNum ORDER BY allCount desc) as temp)";
            string[] param = { "@startTime", "@endTime", "@regionName" };
            object[] values = { startTime, endTime, regionName };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 客户采购统计
        /// </summary>
        /// <returns></returns>
        public DataSet groupCustomer(DateTime startTime, DateTime endTime, string regionName)
        {
            //string sql = "select customerName,SUM(number) as allCount,SUM(totalPrice) as allPrice from v_salemonomer  where (state=1 or state=2) GROUP BY customerID ORDER BY allCount desc LIMIT 0,10";
            //string sql = "SELECT sum(A.totalPrice) as totalPrice,D.customerName as customerName,sum(A.number) as number from t_salemonomer as A,t_salehead as B,t_saletask as C,t_customer as D where a.saleHeadId = b.saleHeadId and b.saleTaskId=c.saleTaskId and c.customerID=d.customerID and (b.state=1 or b.state=2) group by D.customerName ORDER BY totalPrice desc LIMIT 0,10";
            string sql = @"select customerName,SUM(number) as allCount,SUM(totalPrice) as allPrice,regionName from v_salemonomer  where (state=1 or state=2) and dateTime between @startTime and  @endTime and regionName=@regionName GROUP BY customerID ORDER BY allCount desc LIMIT 0,10";
            string[] param = { "@startTime", "@endTime", "@regionName" };
            object[] values = { startTime, endTime, regionName };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 客户所购品种数
        /// </summary>
        /// <returns></returns>
        //public DataSet customerKinds()
        //{
        //    string sql = "select bookNum,customerName from v_salemonomer GROUP BY bookNum";
        //    DataSet ds = db.FillDataSet(sql, null, null);
        //    return ds;
        //}
        public int customerKinds(DateTime startTime, DateTime endTime, string regionName, string customerName)
        {
            //string cmdText = @"select count(bookNum) as customerKinds from ((select customerName, bookNum, SUM(number) as allCount, SUM(totalPrice) as allPrice from v_salemonomer  where (state = 1 or state = 2) and customerName = @customerName GROUP BY bookNum) as temp)";
            string cmdText = @"select count(bookNum) as customerKinds from ((select customerName, bookNum, SUM(number) as allCount, SUM(totalPrice) as allPrice from v_salemonomer  where (state = 1 or state = 2) and dateTime BETWEEN @startTime and @endTime and regionName=@regionName and customerName = @customerName GROUP BY bookNum) as temp)";
            string[] param = { "@startTime", "@endTime", "@regionName", "@customerName" };
            object[] values = { startTime, endTime, regionName, customerName };
            int kinds = Convert.ToInt32(db.ExecuteScalar(cmdText, param, values));
            return kinds;
        }

        /// <summary>
        /// 导出表格
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataTable ExportExcel(string strWhere)
        {
            String cmdText = "select bookNum as 书号,bookName as 书名,ISBN as ISBN,unitPrice as 单价,sum(number) as 数量 ,sum(totalPrice) as 码洋,supplier as 出版社,author as 销售折扣 from v_salemonomer where saleHeadId='" + strWhere + "' group by bookNum,bookName,ISBN,unitPrice";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            DataTable dt = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        /// <summary>
        /// 导出页面上查询到的结果
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="groupbyType">groupby条件</param>
        /// <param name="state">状态</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public DataTable exportAll(string strWhere, string groupbyType, string state, string time)
        {
            DataTable exportdt = new DataTable();
            String cmdText = "";
            string condition = "";
            int kinds = 0;
            if (groupbyType == "supplier")
            {
                exportdt.Columns.Add("供应商", typeof(string));
                cmdText = "select supplier, sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice from v_salemonomer where " + strWhere + " order by allTotalPrice desc";
            }
            else if (groupbyType == "regionName")
            {
                exportdt.Columns.Add("地区名称", typeof(string));
                cmdText = "select regionName, sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice from v_salemonomer where " + strWhere + " order by allTotalPrice desc";
            }
            else
            {
                exportdt.Columns.Add("客户名称", typeof(string));
                cmdText = "select customerName, sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice from v_salemonomer where " + strWhere + " order by allTotalPrice desc";
            }
            DataSet ds = db.FillDataSet(cmdText, null, null);
            exportdt.Columns.Add("书籍种数", typeof(long));
            exportdt.Columns.Add("书籍总数量", typeof(long));
            exportdt.Columns.Add("总实洋", typeof(double));
            exportdt.Columns.Add("总码洋", typeof(double));
            int allcount = ds.Tables[0].Rows.Count;
            for (int i = 0; i < allcount; i++)
            {
                condition = ds.Tables[0].Rows[i]["" + groupbyType + ""].ToString();
                kinds = getkindsGroupBy(condition, groupbyType, state, time);
                exportdt.Rows.Add(ds.Tables[0].Rows[i]["" + groupbyType + ""].ToString(), Convert.ToInt64(kinds), Convert.ToInt64(ds.Tables[0].Rows[i]["allNumber"].ToString()), Convert.ToDouble(ds.Tables[0].Rows[i]["allRealPrice"].ToString()), Convert.ToDouble(ds.Tables[0].Rows[i]["allTotalPrice"].ToString()));
            }
            if (exportdt.Rows.Count > 0)
            {
                return exportdt;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 导出明细
        /// </summary>
        /// <param name="groupbyType">groupby方式</param>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public DataTable exportDel(string groupbyType, string strWhere)
        {
            String cmdText = "";
            string name = "";
            //所选分组条件如客户 ISBN    书号 书名  定价 数量  码洋 实洋  销折 采集日期    采集人用户名 采集状态（销售单或预采单）			供应商
            if (groupbyType == "supplier")
            {
                name = "供应商名称";
            }
            else if (groupbyType == "regionName")
            {
                name = "组织名称";
            }
            else
            {
                name = "客户名称";
            }
            if (strWhere != "" && strWhere != null)
            {
                cmdText = "select " + groupbyType + " as " + name + ", ISBN,bookNum as 书号,bookName as 书名,price as 定价,sum(number) as 数量,sum(totalPrice) as 码洋,sum(realPrice) as 实洋,remarks as 销售折扣,DATE_FORMAT(dateTime,'%Y-%m-%d %H:%i:%s') as 采集日期,userName as 采集人用户名, state as 采集状态,supplier as 供应商 from v_salemonomer where " + strWhere + ",booknum,userName HAVING 数量!=0 order by convert(" + groupbyType + " using gbk) collate gbk_chinese_ci";
            }
            else
            {
                cmdText = "select " + groupbyType + " as " + name + ", ISBN,bookNum as 书号,bookName as 书名,price as 定价,sum(number) as 数量,sum(totalPrice) as 码洋,sum(realPrice) as 实洋,remarks as 销售折扣,DATE_FORMAT(dateTime,'%Y-%m-%d %H:%i:%s') as 采集日期,userName as 采集人用户名, state as 采集状态,supplier as 供应商 from v_salemonomer GROUP BY " + groupbyType + ",booknum,userName HAVING 数量!=0 order by convert(" + groupbyType + " using gbk) collate gbk_chinese_ci";
            }
            DataSet ds = db.FillDataSet(cmdText, null, null);
            DataTable dt = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

    }
}
