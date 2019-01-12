using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bms.Dao
{
    public class SaleTaskDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取销售任务数量
        /// </summary>
        /// <returns>数量</returns>
        public int countSaleTask()
        {
            string cmdText = "select count(saleTaskId) from T_SaleTask";
            int row = Convert.ToInt32(db.ExecuteScalar(cmdText, null, null));
            return row;
        }
        /// <summary>
        /// 查询有销售任务的客户
        /// </summary>
        /// <returns>客户id数据集</returns>
        public DataSet getcustomerID()
        {
            string cmdText = "select customerId from T_SaleTask";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            return ds;
        }
        /// <summary>
        /// 添加客户的销售任务总计
        /// </summary>
        /// <param name="customerId">客户id</param>
        /// <param name="allNumber">总数量</param>
        /// <param name="allPrice">总码洋</param>
        /// <param name="allKinds">品种数</param>
        /// <returns>受影响行数</returns>
        public int insertSaleStatistics(string customerId, int allNumber, double allPrice, int allKinds)
        {
            string cmdText = "insert into T_SaleStatistics(customerId,allNumber,allPrice,allKinds) values(@customerId,@allNumber,@allPrice,@allKinds)";
            string[] param = { "@customerId", "@allNumber", "@allPrice", "@allKinds" };
            object[] values = { customerId, allNumber, allPrice, allKinds };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        ///根据用户id获取他的所有销售记录
        /// </summary>
        /// <param name="customerId">客户id</param>
        /// <returns>返回数据集</returns>
        public DataSet SelectMonomers(string customerId)
        {
            string cmdText = "select number,totalPrice from V_SaleMonomer where customerId=@customerId";
            string[] param = { "@customerId" };
            object[] values = { customerId };
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
        ///根据客户id 查询是否统计过该客户的销售任务总计
        /// </summary>
        /// <param name="customerId">客户id</param>
        /// <returns>影响行数</returns>
        public int SaleStatisticsIsExistence(string customerId)
        {
            string cmdText = "select count(customerId) from T_SaleStatistics where customerId=@customerId";
            string[] param = { "@customerId" };
            object[] values = { customerId };
            int row = int.Parse(db.ExecuteScalar(cmdText, param, values).ToString());
            return row;
        }

        /// <summary>
        /// 统计销售任务总种数
        /// </summary>
        /// <param name="customer">客户id</param>
        /// <returns>返回总种数</returns>
        public int getkinds(string customerID)
        {
            string cmdText = "select bookNum,number from V_SaleMonomer where customerID=@customerID;";
            string[] param = { "@customerID" };
            object[] values = { customerID };
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
                    sltemp = 0;
                }
            }
            return zl;
        }

        /// <summary>
        /// 根据销售任务id 获取客户id
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns></returns>
        public string getCustomerId(string saleTaskId)
        {
            string comText = "select customerId from T_SaleTask where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(comText, param, values);
            string customerId = ds.Tables[0].Rows[0]["customerId"].ToString();
            if (customerId != null || customerId.Length > 0)
            {
                return customerId;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 查询所有销售任务
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet Select()
        {
            string comText = "select saleTaskId,defaultDiscount,numberLimit,totalPriceLimit,startTime,finishTime,userId from T_SaleTask";
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
        /// 添加销售任务
        /// </summary>
        /// <param name="task">销售任务实体</param>
        /// <returns>受影响行数</returns>
        public int Insert(SaleTask sale)
        {
            string cmdText = "insert into T_SaleTask(saleTaskId,userId,customerId,defaultDiscount,defaultCopy,numberLimit,priceLimit,totalPriceLimit,startTime) values(@saleTaskId,@userId,@customreId,@defaultDiscount,@defaultCopy,@numberLimit,@priceLimit,@totalPriceLimit,@startTime)";
            string[] param = { "@saleTaskId", "@userId", "@customreId", "@defaultDiscount", "@defaultCopy", "@numberLimit", "@priceLimit", "@totalPriceLimit", "@startTime" };
            object[] values = { sale.SaleTaskId, sale.UserId, sale.Customer.CustomerId, sale.DefaultDiscount, sale.DefaultCopy, sale.NumberLimit, sale.PriceLimit, sale.TotalPiceLimit, sale.StartTime };
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
        /// 删除销售任务
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns>受影响行数</returns>
        public int Delete(string saleTaskId)
        {
            string cmdText = "update T_SaleTask set deleteState = 1 where saleTaskId=@saleTaskId";
            String[] param = { "@saleTaskId" };
            String[] values = { saleTaskId.ToString() };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 根据销售任务ID获取销售任务信息 
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns></returns>
        public DataSet SelectById(string saleTaskId)
        {
            string sql = "select defaultDiscount,numberLimit,userId from T_SaleTask where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 根据销售任务id获取数量、价格、总实洋限制
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns></returns>
        public DataSet SelectBysaleTaskId(string saleTaskId)
        {
            string sql = "select numberLimit,priceLimit,defaultCopy from T_SaleTask where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 更新销售任务数量 总码洋 单价上限
        /// </summary>
        /// <param name="numberLimit">数量</param>
        /// <param name="priceLimit">单价</param>
        /// <param name="totalPriceLimit">总码洋</param>
        /// <returns>受影响行数</returns>
        public int update(int numberLimit, double priceLimit, double totalPriceLimit, double defaultDiscount, string defaultCopy, string saleId)
        {
            string cmdText = "update T_SaleTask set numberLimit=@numberLimit, priceLimit=@priceLimit, totalPriceLimit=@totalPriceLimit,defaultDiscount=@defaultDiscount,defaultCopy=@defaultCopy where saleTaskId=@saleId";
            string[] param = { "@numberLimit", "@priceLimit", "@totalPriceLimit", "@defaultDiscount", "@@defaultCopy", "@saleId" };
            object[] values = { numberLimit, priceLimit, totalPriceLimit, defaultDiscount, defaultCopy, saleId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 更新任务完成时间
        /// </summary>
        /// <param name="finishtime">时间</param>
        /// <param name="saleId">销售任务id</param>
        /// <returns>受影响行数</returns>
        public int updatefinishTime(DateTime finishtime, string saleId)
        {
            string cmdText = "update T_SaleTask set finishTime=@finishtime where saleTaskId=@saleId";
            string[] param = { "@finishtime", "@saleId" };
            object[] values = { finishtime, saleId };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 获取该销售任务下的所有销售单状态
        /// </summary>
        /// <param name="saleid">销售任务id</param>
        /// <returns></returns>
        public DataSet SelectHeadStateBySaleId(string saleId)
        {
            string comText = "select state from T_SaleHead where saleTaskId=@saleId order by state asc";
            string[] param = { "@saleId" };
            object[] values = { saleId };
            DataSet ds = db.FillDataSet(comText, param, values);
            return ds;
        }
        /// <summary>
        /// 获取日期
        /// </summary>
        /// <returns>时间字符串</returns>
        public string getSaleTaskTime()
        {
            string comText = "select startTime from T_SaleTask order by startTime desc";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                string time = ds.Tables[0].Rows[0]["startTime"].ToString();
                return time.Substring(0, 10);
            }
            return null;
        }
        /// <summary>
        /// 获取当天最后一单
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public string getSaleTaskIdByTimedesc(string time)
        {
            string comText = "SELECT saleTaskId from t_saletask where startTime like '" + time + "%' ORDER BY saleTaskId desc";
            DataSet ds = db.FillDataSet(comText, null, null);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                string saleTaskId = ds.Tables[0].Rows[0]["saleTaskId"].ToString();
                return saleTaskId;
            }
            return null;
        }
        // <summary>
        /// 完成日期
        /// </summary>
        /// <returns>时间字符串</returns>
        public string getSaleFinishTime(string saleId)
        {
            string comText = "select finishTime from T_SaleTask where saleTaskId=@saleId";
            string[] param = { "@saleId" };
            object[] values = { saleId };
            DataSet ds = db.FillDataSet(comText, param, values);
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                string time = ds.Tables[0].Rows[0]["finishTime"].ToString();
                return time;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据销售任务ID，统计销售任务的总数量，总码洋，总实洋
        /// </summary>
        /// <param name="saleTaskId">销售任务ID</param>
        /// <returns>数据集</returns>
        public DataSet getSaleTaskStatistics(string saleTaskId, string state)
        {
            string comText;
            if (state == "3")
            {
                comText = "SELECT sum(c.number) as number,sum(c.totalPrice) as totalPrice,sum(c.realPrice) as realPrice from t_saletask as A ,t_salehead as B, t_salemonomer as C where a.saleTaskId=b.saleTaskId and b.saleHeadId=c.saleHeadId and a.saleTaskId=c.saleTaskId and c.saleTaskId=@saleTaskId and B.state=3";
            }
            else
            {
                comText = "SELECT sum(c.number) as number,sum(c.totalPrice) as totalPrice,sum(c.realPrice) as realPrice from t_saletask as A ,t_salehead as B, t_salemonomer as C where a.saleTaskId=b.saleTaskId and b.saleHeadId=c.saleHeadId and a.saleTaskId=c.saleTaskId and c.saleTaskId=@saleTaskId and (B.state=1 or B.state=2)";
            }
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
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
        /// 根据销售任务id获取操作员,客户
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns>数据集</returns>
        public DataSet getcustomerName(string saleTaskId)
        {
            string cmdText = "select userName,customerName,startTime,finishTime from V_SaleTask where saleTaskId=@saleTaskId";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }
        /// <summary>
        /// 销售统计计划
        /// </summary>
        /// <param name="saleTaskId"></param>
        /// <returns></returns>
        public DataSet salesTaskStatistics(string saleTaskId)
        {
            string cmdText = "select bookNum,bookName,ISBN,unitPrice,sum(number) as allnumber ,sum(realPrice) as allrealPrice from V_SaleMonomer where saleTaskId=@saleTaskId  group by bookNum,bookName,ISBN,unitPrice order by dateTime";
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }

        /// <summary>
        /// 统计种数
        /// </summary>
        /// <param name="saleTaskId">销售任务id</param>
        /// <returns></returns>
        public int getkindsBySaleTaskId(string saleTaskId, string state)
        {
            string cmdText;
            if (state == "3")
            {
                cmdText = "SELECT c.bookNum,c.number from t_saletask as A ,t_salehead as B, t_salemonomer as C where a.saleTaskId=b.saleTaskId and b.saleHeadId=c.saleHeadId and a.saleTaskId=c.saleTaskId and c.saleTaskId=@saleTaskId and B.state=3";
            }
            else
            {
                cmdText = "SELECT c.bookNum,c.number from t_saletask as A ,t_salehead as B, t_salemonomer as C where a.saleTaskId=b.saleTaskId and b.saleHeadId=c.saleHeadId and a.saleTaskId=c.saleTaskId and c.saleTaskId=@saleTaskId and B.state !=3";
            }
            string[] param = { "@saleTaskId" };
            object[] values = { saleTaskId };
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
        /// 根据客户id获取他是否有过销售任务
        /// </summary>
        /// <param name="CustmerId">客户id</param>
        /// <returns>0该客户还没有销售任务,1该客户已有销售任务但未完成,2已完成，可以添加</returns>
        public string getcustomermsg(int CustmerId, int regionId)
        {
            string cmdText = "select startTime,finishTime from V_SaleTask where customerId=@CustmerId and regionId=@regionId ";
            string[] param = { "@CustmerId", "@regionId" };
            object[] values = { CustmerId, regionId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            string state = "";
            if (ds != null || ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string finishtime = ds.Tables[0].Rows[i]["finishTime"].ToString();
                    if (finishtime == "" || finishtime == null)
                    {
                        state = "1";
                    }
                    else
                    {
                        state = "2";
                    }
                }
            }
            else
            {
                state = "0";
            }
            return state;
        }
        /// <summary>
        ///根据当天时间 获取所有销售任务的总实洋，书籍总数，总码洋
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet getAllprice(string dateTime)
        {
            //string cmdText = "select count(bookNum) as allKinds,sum(allNum) as allNum,sum(allPrice) as allPrice,sum(allRealPrice) as allRealPrice from (select sm.ISBN,sm.bookNum,book.bookName,sm.unitPrice,sum(sm.number) as allNum,sum(sm.realPrice) as allRealPrice,sum(sm.totalPrice) as allPrice,head.state,sm.dateTime,us.userID,us.userName,ct.customerID,ct.customerName,rg.regionName from t_salemonomer as sm,t_bookbasicdata as book,t_user as us,t_customer as ct,t_salehead as head,t_saletask as task,t_region as rg where (head.state=1 or head.state=2) and sm.saleTaskId = task.saleTaskId and task.userId = us.userID and sm.bookNum = book.bookNum AND sm.ISBN = book.ISBN and task.customerId = ct.customerID and us.regionId = rg.regionId and head.userId = us.userID AND head.regionId = rg.regionId AND head.saleTaskId = task.saleTaskId AND sm.saleHeadId = head.saleHeadId and sm.dateTime like '@dateTime%' GROUP BY sm.bookNum) as statics";
            //string[] param = { "@dateTime" };
            //object[] values = { dateTime };
            //DataSet ds = db.FillDataSet(cmdText, param, values);
            //return ds;
            string sql = "select count(bookNum) as allKinds,sum(allNum) as allNum,sum(allPrice) as allPrice,sum(allRealPrice) as allRealPrice from (select sm.ISBN,sm.bookNum,book.bookName,sm.unitPrice,sum(sm.number) as allNum,sum(sm.realPrice) as allRealPrice,sum(sm.totalPrice) as allPrice,head.state,sm.dateTime,us.userID,us.userName,ct.customerID,ct.customerName,rg.regionName from t_salemonomer as sm,t_bookbasicdata as book,t_user as us,t_customer as ct,t_salehead as head,t_saletask as task,t_region as rg where (head.state=1 or head.state=2) and sm.saleTaskId = task.saleTaskId and task.userId = us.userID and sm.bookNum = book.bookNum AND sm.ISBN = book.ISBN and task.customerId = ct.customerID and us.regionId = rg.regionId and head.userId = us.userID AND head.regionId = rg.regionId AND head.saleTaskId = task.saleTaskId AND sm.saleHeadId = head.saleHeadId and sm.dateTime like '@dateTime%' GROUP BY sm.bookNum) as statics";
            string[] param = { "@dateTime" };
            object[] values = { dateTime };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 统计当天种数
        /// </summary>
        /// <returns></returns>
        public int getAllkinds(string dateTime)
        {
            string cmdText = "select bookNum,number from T_SaleMonomer where dateTime like '" + dateTime + "%'";
            float sltemp = 0;
            int zl = 0;
            DataTable dtcount = db.getkinds(cmdText, null, null);
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
        ///根据当天时间 获取所有销售任务的总实洋，书籍总数，总码洋 地区
        /// </summary>
        /// <returns>数据集</returns>
        public DataSet getAllpriceRegion(int regionId,string dateTime)
        {
            //string cmdText = "select count(bookNum) as allKinds,sum(allNum) as allNum,sum(allPrice) as allPrice,sum(allRealPrice) as allRealPrice from (select sm.ISBN,sm.bookNum,book.bookName,sm.unitPrice,sum(sm.number) as allNum,sum(sm.realPrice) as allRealPrice,sum(sm.totalPrice) as allPrice,head.state,sm.dateTime,us.userID,us.userName,ct.customerID,ct.customerName,rg.regionName from t_salemonomer as sm,t_bookbasicdata as book,t_user as us,t_customer as ct,t_salehead as head,t_saletask as task,t_region as rg where (head.state=1 or head.state=2) and sm.saleTaskId = task.saleTaskId and task.userId = us.userID and sm.bookNum = book.bookNum AND sm.ISBN = book.ISBN and task.customerId = ct.customerID and us.regionId = rg.regionId and head.userId = us.userID AND head.regionId = rg.regionId AND head.saleTaskId = task.saleTaskId AND sm.saleHeadId = head.saleHeadId and rg.regionId=@regionId and sm.dateTime like '@dateTime%' GROUP BY sm.bookNum) as statics";
            //string[] param = { "@regionId","@dateTime" };
            //object[] values = { regionId, dateTime };
            //DataSet ds = db.FillDataSet(cmdText, param, values);
            //return ds;
            string cmdText = "select sm.ISBN,sm.bookNum,book.bookName,sm.unitPrice,sum(sm.number) as allNum,sum(sm.realPrice) as allRealPrice,sum(sm.totalPrice) as allPrice,head.state,sm.dateTime,us.userID,us.userName,ct.customerID,ct.customerName,rg.regionName from t_salemonomer as sm,t_bookbasicdata as book,t_user as us,t_customer as ct,t_salehead as head,t_saletask as task,t_region as rg where (head.state=1 or head.state=2) and sm.saleTaskId = task.saleTaskId and task.userId = us.userID and sm.bookNum = book.bookNum AND sm.ISBN = book.ISBN and task.customerId = ct.customerID and us.regionId = rg.regionId and head.userId = us.userID AND head.regionId = rg.regionId AND head.saleTaskId = task.saleTaskId AND sm.saleHeadId = head.saleHeadId and us.regionId=67 and sm.dateTime like '@dateTime%' GROUP BY sm.bookNum";
            string[] param = { "@regionId", "@dateTime" };
            object[] values = { regionId, dateTime };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }
        /// <summary>
        /// 统计当天种数
        /// </summary>
        /// <returns></returns>
        public int getAllkindsRegion(string dateTime, int regionId)
        {
            string cmdText = "select bookNum,number from V_SaleMonomer where dateTime like '" + dateTime + "%' and regionId=@regionId";
            string[] param = { "@regionId" };
            object[] values = { regionId };
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
        /// 导出表格
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataTable ExportExcel(string strWhere, string state)
        {
            String cmdText;
            if (state == "3")
            {
                cmdText = "select bookNum as 书号,bookName as 书名,ISBN as ISBN,unitPrice as 单价,sum(number) as 数量 ,sum(totalPrice) as 码洋,supplier as 出版社,author as 销售折扣 from v_salemonomer where saleTaskId='" + strWhere + "' and state=3 group by bookNum,bookName,ISBN,unitPrice";
            }
            else
            {
                cmdText = "select bookNum as 书号,bookName as 书名,ISBN as ISBN,unitPrice as 单价,sum(number) as 数量 ,sum(totalPrice) as 码洋,supplier as 出版社,author as 销售折扣 from v_salemonomer where saleTaskId='" + strWhere + "' and state <>3 group by bookNum,bookName,ISBN,unitPrice";
            }
            DataSet ds = db.FillDataSet(cmdText, null, null);
            DataTable dt = null;
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        public DataTable ExportExcel(string cmdText)
        {
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
