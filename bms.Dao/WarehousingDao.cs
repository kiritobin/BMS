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
            string cmdText = "insert into T_SingleHead(singleHeadId,time,regionId,userId,type,remarks) values(@singleHeadId,@time,@regionId,@userId,@type,@remarks)";
            string[] param = { "@singleHeadId", "@time","@regionId","@userId", "@type", "@remarks" };
            object[] values = { single.SingleHeadId, single.Time, single.Region.RegionId, single.User.UserId,single.Type, single.Remarks };
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
            string cmdText = "insert into T_Monomers(monId,singleHeadId,bookNum,ISBN,number,uPrice,totalPrice,realPrice,discount,shelvesId,type) values(@monId,@singleHeadId,@bookNum,@ISBN,@number,@uPrice,@totalPrice,@realPrice,@discount,@shelvesId,@type)";
            string[] param = { "@monId", "@singleHeadId", "@bookNum", "@ISBN", "@number", "@uPrice", "@totalPrice", "@realPrice", "@discount", "@shelvesId", "@type" };
            object[] values = { monomers.MonomersId, monomers.SingleHeadId.SingleHeadId,monomers.BookNum.BookNum, monomers.Isbn.Isbn, monomers.Number, monomers.UPrice.Price, monomers.TotalPrice, monomers.RealPrice, monomers.Discount,monomers.ShelvesId, monomers.Type };
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
        /// 通过书号查询在单体中是否存在记录
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <param name="type">单体类型（0：出库，1：入库，2：退货）</param>
        /// <returns></returns>
        public int SelectBybookNum(string singleHeadId,string bookNum, int type)
        {
            string comText = "select COUNT(monId) from T_Monomers where bookNum=@bookNum and type=@type and singleHeadId=@singleHeadId";
            string[] param = { "@bookNum", "@type", "@singleHeadId" };
            object[] values = { bookNum, type, singleHeadId };
            int row = Convert.ToInt32(db.ExecuteScalar(comText, param, values));
            return row;
        }

        /// <summary>
        /// 通过书号查询在单头中是否存在记录
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public int SelectBybookNumInHead(string bookNum)
        {
            string comText = "select COUNT(monId) from T_SingleHead where bookNum=@bookNum";
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

        /// <summary>
        /// 导出成Excel表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns>返回一个DataTable的选题记录集合</returns>
        public DataTable ExportExcel(string strWhere)
        {
            String cmdText = "select singleHeadId as 单据编号,bookNum as 书号,ISBN as ISBN号,bookName as 书名,sum(number) as 商品数量,uPrice as 单价,discount as 折扣,sum(totalPrice) as 码洋,sum(realPrice) as 实洋,shelvesName as 货架 from V_Monomer where singleHeadId=@strWhere group by bookNum,bookName,ISBN,uPrice order by time desc";
            string[] param = { "@strWhere"};
            object[] values = { strWhere};
            DataSet ds = db.FillDataSet(cmdText, param, values);
            DataTable dt = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 获取该销退单头下所有的单据号和制单时间
        /// </summary>
        /// <returns></returns>
        public DataSet getAllTime(int type)
        {
            string cmdText = "select singleHeadId,time from T_SingleHead where type=@type ORDER BY time desc";
            string[] param = { "@type" };
            object[] values = { type };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }
        /// <summary>
        /// 根据入库单头ID获取单体合计
        /// </summary>
        /// <param name="singleHeadId"></param>
        /// <returns></returns>
        public DataSet addupRKMonomer(string singleHeadId)
        {
            string cmdText = "select singleHeadId,sum(number),SUM(totalPrice),SUM(realPrice) from T_Monomers where singleHeadId=@singleHeadId GROUP BY singleHeadId";
            string[] param = { "@singleHeadId" };
            object[] values = { singleHeadId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 补货查看
        /// </summary>
        /// <returns></returns>
        public DataSet checkRs(string saleTaskId)
        {
            string cmdText = "select rsMononerID,bookNum,bookName,sum(count) as allnumber,dateTime from V_ReplenishMentMononer where deleteState=0 and saleTaskId=@saleTaskId group by bookNum,bookName order by rsMononerID";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        public DataSet regionRs(int regionId)
        {
            string cmdText = "select regionName,customerName,rsMononerID,bookNum,ISBN,bookName,sum(count) as count,dateTime from V_ReplenishMentMononer where ISNULL(finishTime) and deleteState=0 and regionId=@regionId group by regionName,customerName,rsMononerID,bookNum,ISBN,bookName order by rsMononerID";
            string[] param = { "@regionId" };
            object[] values = { regionId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        public DataSet customerRs(int customerId)
        {
            string cmdText = "select regionName,customerName,rsMononerID,bookNum,ISBN,bookName,sum(count) as count,dateTime from V_ReplenishMentMononer where ISNULL(finishTime) and deleteState=0 and customerId=@customerId group by regionName,customerName,rsMononerID,bookNum,ISBN,bookName order by rsMononerID";
            string[] param = { "@customerId" };
            object[] values = { customerId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        public DataSet getKinds(int type,string where)
        {
            //string cmdText = "SELECT SUM(NUMBER) AS sl,COUNT(DISTINCT bookNum) as pz,sum(realPrice) as sy,sum(totalPrice) as my FROM v_monomer WHERE "+where+" and SINGLEHEADID IN (SELECT singleHeadId FROM t_singlehead WHERE TYPE=@type)";
            string cmdText = "";
            if (where == "")
            {
                cmdText = "SELECT SUM(NUMBER) AS sl,COUNT(DISTINCT bookNum) as pz,sum(realPrice) as sy,sum(totalPrice) as my FROM v_monomer WHERE TYPE=@type";
            }
            else
            {
                cmdText = "SELECT SUM(NUMBER) AS sl,COUNT(DISTINCT bookNum) as pz,sum(realPrice) as sy,sum(totalPrice) as my FROM v_monomer WHERE " + where + " and  TYPE=@type";
            }
            string[] param = { "@type" };
            object[] values = { type };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        ///根据当天时间 获取所有销售任务的总实洋，书籍总数，总码洋
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet getAllprice(string dateTime,int type)
        {
            string cmdText = "SELECT sum(a.number) as number,sum(a.totalPrice) as totalPrice,sum(a.realPrice) as realPrice from T_Monomers as a,T_SingleHead as b where a.singleHeadId=b.singleHeadId and b.time like '" + dateTime + "%' and a.type=@type";
            string[] param = { "@type" };
            object[] values = { type };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }
        /// <summary>
        /// 统计当天种数
        /// </summary>
        /// <returns></returns>
        public int getAllkinds(string dateTime,int type)
        {
            string cmdText = "select booknum,number from V_Monomer as m,T_SingleHead as s where m.type=@type  and s.time like '" + dateTime+"%'";
            float sltemp = 0;
            int zl = 0;
            string[] param = { "@type" };
            object[] values = { type };
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
        /// 根据当天时间 获取所有销售任务的总实洋，书籍总数，总码洋 地区
        /// </summary>
        /// <param name="dateTime">时间</param>
        /// <param name="regionId">地区id</param>
        /// <param name="type">出入退类型</param>
        /// <returns></returns>
        public DataSet getAllpriceRegion(string dateTime, int regionId,int type)
        {
            string cmdText = "SELECT sum(a.number) as number,sum(a.totalPrice) as totalPrice,sum(a.realPrice) as realPrice from T_Monomers as a,T_SingleHead as b where a.singleHeadId=b.singleHeadId and b.time like '" + dateTime+"%' and b.regionId=@regionId and a.type=@type";
            string[] param = { "@regionId" ,"@type"};
            object[] values = { regionId ,type};
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }
        /// <summary>
        /// 统计当天种数
        /// </summary>
        /// <returns></returns>
        public int getAllkindsRegion(string dateTime, int regionId,int type)
        {
            string cmdText = "select bookNum,number from V_Monomer as m,T_SingleHead as s where m.type=@type  and m.regionId=@regionId and s.time like '" + dateTime+"%'";
            string[] param = { "@regionId","@type" };
            object[] values = { regionId, type };
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
        /// <param name="groupbyType">类型</param>
        /// <param name="time">时间</param>
        /// <param name="type">出入退状态</param>
        /// <returns></returns>
        public int getkindsGroupBy(string strWhere, string groupbyType, string time, int type)
        {
            string cmdText = "";
            string startTime = "";
            string endTime = "";
            if (time != "" && time != null)
            {
                string[] sArray = time.Split('至');
                startTime = sArray[0];
                endTime = sArray[1];
                cmdText = "select bookNum, SUM(number) as 数量 from v_monomer where " + groupbyType + " = @strWhere and type=@type and time BETWEEN'" + startTime + "' and '" + endTime + "' GROUP BY bookNum";
            }
            else
            {
                cmdText = "select bookNum, SUM(number) as 数量 from v_monomer where " + groupbyType + " = @strWhere and type=@type GROUP BY bookNum";
            }
            
            string[] param = { "@strWhere", "@type" };
            object[] values = { strWhere, type };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            int allCount = ds.Tables[0].Rows.Count;
            return allCount;
        }

        /// <summary>
        /// 导出页面上查询到的结果
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="groupbyType">groupby条件</param>
        /// <param name="time">时间</param>
        /// <param name="type">出入退类型</param>
        /// <returns></returns>
        public DataTable exportAll(string strWhere, string groupbyType, string time, int type)
        {
            DataTable exportdt = new DataTable();
            String cmdText = "";
            string condition = "";
            int kinds = 0;
            if (groupbyType == "supplier")
            {
                exportdt.Columns.Add("供应商", typeof(string));
                cmdText = "select supplier, sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice from v_monomer where " + strWhere + " order by time desc";
            }
            else if (groupbyType == "regionName")
            {
                exportdt.Columns.Add("地区名称", typeof(string));
                cmdText = "select regionName, sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice from v_monomer where " + strWhere + " order by time desc";
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
                kinds = getkindsGroupBy(condition, groupbyType, time, type);
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
        /// <param name="type">出入退类型</param>
        /// <returns></returns>
        public DataTable exportDel(string groupbyType, string strWhere, int type)
        {
            string cmdText = "",regionName="";
            if (type == 1)
            {
                regionName = "入库来源";
            }
            else
            {
                regionName = "接收组织";
            }
            //所选分组条件如客户 ISBN    书号 书名  定价 数量  码洋 实洋  销折 采集日期    采集人用户名 采集状态（销售单或预采单）			供应商
            if (strWhere != "" && strWhere != null)
            {
                cmdText = "select ISBN,bookNum as 书号,bookName as 书名,uPrice as 定价,sum(number) as 数量,sum(totalPrice) as 码洋,sum(realPrice) as 实洋,author as 进货折扣,time as 制单日期,userName as 制单人, supplier as 供应商, regionName as " + regionName + ",dentification as 备注,remarksOne as 备注1,remarksTwo as 备注2,remarksThree as 备注3 from v_monomer where type=" + type + " and " + strWhere + ",booknum,userName order by time desc";
            }
            else
            {
                cmdText = "select ISBN,bookNum as 书号,bookName as 书名,uPrice as 定价,sum(number) as 数量,sum(totalPrice) as 码洋,sum(realPrice) as 实洋,author as 进货折扣,time as 制单日期,userName as 制单人, supplier as 供应商, regionName as " + regionName + ",dentification as 备注,remarksOne as 备注1,remarksTwo as 备注2,remarksThree as 备注3 from v_monomer where type=" + type + " GROUP BY " + groupbyType + ",booknum,userName order by time desc";
            }
            DataSet ds = db.FillDataSet(cmdText, null, null);
            DataTable dt = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 获取制单员
        /// </summary>
        /// <param name="strWhere">筛选条件</param>
        /// <param name="type">出入退类型</param>
        /// <returns></returns>
        public DataSet getUser(string strWhere, int type)
        {
            if (strWhere != null && strWhere != "")
            {
                strWhere = " and " + strWhere;
            }
            else
            {
                strWhere = "";
            }
            String cmdText = "select userName from v_monomer where  type=" + type + strWhere + " group by userName";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 导出成Excel表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="groupType">分组条件</param>
        /// <param name="type">出入退类型</param>
        /// <returns>返回一个DataTable的选题记录集合</returns>
        public DataTable ExportExcelDetails(string strWhere, string groupType, int type)
        {
            if(strWhere != null && strWhere != "")
            {
                strWhere = " and " + strWhere;
            }
            else
            {
                strWhere = "";
            }
            string regionName = "接收组织";
            if(type == 1)
            {
                regionName = "入库来源";
            }
            String cmdText = "select ISBN,bookNum as 书号,bookName as 书名,uPrice as 单价,sum(number) as 数量, sum(totalPrice) as 码洋,sum(realPrice) as 实洋,author as 进货折扣,supplier as 供应商, DATE_FORMAT(time,'%Y-%m-%d %H:%i:%s') as 制单时间,userName as 制单员,regionName as " + regionName + ",dentification as 备注,remarksOne as 备注1,remarksTwo as 备注2,remarksThree as 备注3 from v_monomer where type=" + type+ strWhere + " group by bookNum,userName," + groupType + " order by time desc";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            DataTable dt = null;
            int count = ds.Tables[0].Rows.Count;
            dt = ds.Tables[0];
            return dt;
        }
    }
}
