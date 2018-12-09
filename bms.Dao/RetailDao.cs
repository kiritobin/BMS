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
                cmdText = "select retailHeadId,dateTime from T_RetailHead where state=2 ORDER BY retailHeadId desc";
            }
            else
            {
                cmdText = "select retailHeadId,dateTime from T_RetailHead where state=0 or state=1 ORDER BY retailHeadId desc";
            }
            string[] param = { "@state" };
            object[] values = { state };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }

        /// <summary>
        /// 通过书号查看是否存在在单体中
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public int selectByBookNum(string bookNum,string retailHeadId)
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
            string cmdText = "insert into T_RetailHead(state,retailHeadId,userId,regionId,dateTime,kindsNum,number,allTotalPrice,allRealPrice,payment) values(@state,@retailHeadId,@userId,@regionId,@dateTime,@kindsNum,@number,@allTotalPrice,@allRealPrice,@payment)";
            string[] param = { "@state", "@retailHeadId", "@userId", "@regionId", "@dateTime", "@kindsNum", "@number", "@allTotalPrice", "@allRealPrice", "@payment" };
            object[] values = { salehead.State ,salehead.SaleHeadId, salehead.UserId, salehead.RegionId, salehead.DateTime, salehead.KindsNum, salehead.Number, salehead.AllTotalPrice, salehead.AllRealPrice,salehead.PayType };
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
            string cmdText = "select allTotalPrice,number,allRealPrice,kindsNum,userName,retailHeadId,dateTime,regionName from V_RetailHead where retailHeadId=@retailHeadId";
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
                    sale.UserName = ds.Tables[0].Rows[0]["userName"].ToString();
                    sale.RegionName = ds.Tables[0].Rows[0]["regionName"].ToString();
                    sale.SaleHeadId = ds.Tables[0].Rows[0]["retailHeadId"].ToString();
                    sale.DateTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["dateTime"]);
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
        /// 查询单头下的状态
        /// </summary>
        /// <param name="retailHeadId">零售单头ID</param>
        /// <returns>数据集</returns>
        public int GetRetailType(string retailHeadId)
        {
            string cmdText = "select state from T_RetailHead where retailHeadId=@retailHeadId";
            string[] param = { "@retailHeadId" };
            object[] values = { retailHeadId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            int state = Convert.ToInt32(ds.Tables[0].Rows[0]["state"]);
            return state;
        }

        /// <summary>
        /// 查询零售单体
        /// </summary>
        /// <param name="retailMonomerId">零售单体ID</param>
        /// <param name="retailHeadId">零售单头ID</param>
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
            string cmdText = "update T_RetailMonomer set number=@number,totalPrice=@totalPrice,realPrice=@realPrice where retailMonomerId=@retailMonomerId and retailHeadId=@retailHeadId";
            string[] param = { "@number", "totalPrice", "@realPrice", "@retailMonomerId", "@retailHeadId" };
            object[] values = { sale.Number,sale.TotalPrice, sale.RealPrice, sale.SaleIdMonomerId, sale.SaleHeadId };
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
            string cmdText = "delete from T_RetailMonomer where retailMonomerId=@retailMonomerId and retailHeadId=@retailHeadId";
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
        public int updateType(string headId, User user,string payType)
        {
            string cmdText = "update T_RetailHead set state=1,dateTime=@dateTime,userId=@userId,payment=@payType where retailHeadId=@retailHeadId";
            string[] param = { "@retailHeadId", "@dateTime", "@userId", "@payType" };
            object[] values = { headId, DateTime.Now, user.UserId, payType };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }

        /// <summary>
        /// 根据ISBN查找书号，单价，折扣
        /// </summary>
        /// <param name="ISBN">ISBN</param>
        /// <returns></returns>
        public DataSet SelectByIsbn(string ISBN,string retailHeadId)
        {
            MySqlHelp db = new MySqlHelp();
            string comTexts = "select count(id) from T_RetailMonomer where ISBN=@ISBN and retailHeadId=@retailHeadId";
            string[] parames = { "@ISBN", "@retailHeadId" };
            object[] value = { ISBN, retailHeadId };
            int row = Convert.ToInt32(db.ExecuteScalar(comTexts, parames, value));
            if (row == 0)
            {
                return null;
            }
            else
            {
                string comText = "select bookNum,ISBN,price,bookName,supplier from T_BookBasicData where ISBN=@ISBN";
                string[] param = { "@ISBN" };
                object[] values = { ISBN };
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
        }

        /// <summary>
        /// 根据书号查找isbn，单价，折扣
        /// </summary>
        /// <param name="bookNum">书号</param>
        /// <returns></returns>
        public BookBasicData SelectByBookNum(long bookNum, string retailHeadId)
        {
            MySqlHelp db = new MySqlHelp();
            string comTexts = "select count(id) from T_RetailMonomer where bookNum=@bookNum and retailHeadId=@retailHeadId";
            string[] parames = { "@bookNum", "@retailHeadId" };
            object[] value = { bookNum, retailHeadId };
            int row = Convert.ToInt32(db.ExecuteScalar(comTexts, parames, value));
            if (row == 0)
            {
                return null;
            }
            else
            {
                string comText = "select ISBN,price,bookName,supplier,remarks from T_BookBasicData where bookNum=@bookNum";
                string[] param = { "@bookNum" };
                object[] values = { bookNum };
                DataSet ds = db.FillDataSet(comText, param, values);
                if (ds != null || ds.Tables[0].Rows.Count > 0)
                {
                    string isbn = ds.Tables[0].Rows[0]["isbn"].ToString();
                    string price = ds.Tables[0].Rows[0]["price"].ToString();
                    string remarks = ds.Tables[0].Rows[0]["remarks"].ToString();
                    string bookName = ds.Tables[0].Rows[0]["bookName"].ToString();
                    string supplier = ds.Tables[0].Rows[0]["supplier"].ToString();
                    BookBasicData bookBasic = new BookBasicData();
                    bookBasic.Isbn = isbn;
                    bookBasic.Price = Convert.ToDouble(price);
                    bookBasic.Remarks = remarks;
                    bookBasic.BookName = bookName;
                    bookBasic.Publisher = supplier;
                    return bookBasic;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 零售统计
        /// </summary>
        /// <returns></returns>
        public  DataSet GroupRetail(DateTime startTime,DateTime endTime,string regionName)
        {
            //string cmdText = "select bookNum,sum(number) as allCount,sum(totalPrice) as allPrice from v_retailmonomer where state=1 GROUP BY bookNum;";
            string cmdText = @"select count(bookNum) as retailKinds,sum(allCount) as allNum,sum(allPrice) as allPrice from ((select bookNum,sum(number) as allCount,sum(totalPrice) as allPrice,dateTime from v_retailmonomer where state=1 and dateTime BETWEEN @startTime and @endTime and regionName=@regionName GROUP BY bookNum) as temp)";
            string[] param = { "@startTime", "@endTime","@regionName" };
            object[] values = { startTime, endTime, regionName };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }


        /// <summary>
        /// 获取书籍种类
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="type">分组类型</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public int getkindsGroupBy(string strWhere, string type, string time)
        {
            string cmdText = "";
            string startTime = "";
            string endTime = "";
            if (time != "" && time != null)
            {
                string[] sArray = time.Split('至');
                startTime = sArray[0];
                endTime = sArray[1];
                cmdText = "select bookNum, SUM(number) as 数量 from v_retailmonomer where " + type + " = @strWhere and dateTime BETWEEN'" + startTime + "' and '" + endTime + "' GROUP BY bookNum HAVING 数量!=0";
            }
            else
            {
                cmdText = "select bookNum, SUM(number) as 数量 from v_retailmonomer where " + type + " = @strWhere GROUP BY bookNum HAVING 数量!=0";
            }
            string[] param = { "@strWhere"};
            object[] values = { strWhere };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            int allCount = ds.Tables[0].Rows.Count;
            return allCount;
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
            if (groupbyType == "payment")
            {
                name = "支付方式";
            }
            else if (groupbyType == "regionName")
            {
                name = "组织名称";
            }
            if (strWhere != "" && strWhere != null)
            {
                cmdText = "select " + groupbyType + " as " + name + ", ISBN,bookNum as 书号,bookName as 书名,unitPrice as 定价,sum(number) as 数量,sum(totalPrice) as 码洋,sum(realPrice) as 实洋,realDiscount as 折扣,dateTime as 交易日期,userName as 收银员,supplier as 供应商 from v_retailmonomer where " + strWhere + ",booknum order by convert(" + groupbyType + " using gbk) collate gbk_chinese_ci";
            }
            else
            {
                cmdText = "select " + groupbyType + " as " + name + ", ISBN,bookNum as 书号,bookName as 书名,unitPrice as 定价,sum(number) as 数量,sum(totalPrice) as 码洋,sum(realPrice) as 实洋,realDiscount as 折扣,dateTime as 交易日期,userName as 收银员,supplier as 供应商 from v_retailmonomer GROUP BY " + groupbyType + ",booknum,userName,supplier order by convert(" + groupbyType + " using gbk) collate gbk_chinese_ci";
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
        /// 导出页面上查询到的结果
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="groupbyType">groupby条件</param>
        /// <param name="state">状态</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public DataTable exportAll(string strWhere, string groupbyType, string time)
        {
            DataTable exportdt = new DataTable();
            String cmdText = "";
            string condition = "";
            int kinds = 0;
            if (groupbyType == "payment")
            {
                exportdt.Columns.Add("支付方式", typeof(string));
                cmdText = "select payment, sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice from v_retailmonomer where " + strWhere + " order by allTotalPrice desc";
            }
            else if (groupbyType == "regionName")
            {
                exportdt.Columns.Add("地区名称", typeof(string));
                cmdText = "select regionName, sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice from v_retailmonomer where " + strWhere + " order by allTotalPrice desc";
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
                kinds = getkindsGroupBy(condition, groupbyType, time);
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
        /// 获取收银员
        /// </summary>
        /// <param name="strWhere">筛选条件</param>
        /// <returns></returns>
        public DataSet getUser(string strWhere)
        {
            String cmdText = "select userName from v_retailmonomer where " + strWhere + " group by userName";
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
        /// <param name="type">分组条件</param>
        /// <returns>返回一个DataTable的选题记录集合</returns>
        public DataTable ExportExcel(string strWhere, string type)
        {
            String cmdText = "select ISBN,bookNum as 书号,bookName as 书名,unitPrice as 单价,sum(number) as 数量, sum(totalPrice) as 码洋,sum(realPrice) as 实洋,realDiscount as 折扣,supplier as 供应商, DATE_FORMAT(dateTime,'%Y-%m-%d %H:%i:%s') as 交易时间,userName as 收银员,payment as 支付方式 from v_retailmonomer where " + strWhere + " group by bookNum,userName,supplier," + type;
            DataSet ds = db.FillDataSet(cmdText, null, null);
            DataTable dt = null;
            int count = ds.Tables[0].Rows.Count;
            dt = ds.Tables[0];
            return dt;
        }
    }
}
