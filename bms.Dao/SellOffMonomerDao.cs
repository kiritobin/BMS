using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using bms.Model;
using bms.DBHelper;

namespace bms.Dao
{
    public class SellOffMonomerDao
    {
        MySqlHelp db = new MySqlHelp();
        /// <summary>
        /// 获取销退单头信息
        /// </summary>
        /// <returns></returns>
        public DataSet Select(string sellOffHeadId)
        {
            string cmdText = "select sellOffHead,sellOffMonomerId,bookNum,isbn,price,count,totalPrice,realDiscount,realPrice,dateTime,bookName from v_selloffmonomer where sellOffHead=@sellOffHeadId";
            string[] param = { "@sellOffHeadId" };
            object[] values = { sellOffHeadId };
            DataSet ds = db.FillDataSet(cmdText, param, values);
            return ds;
        }
        /// <summary>
        /// 通过书号查询销售单中是否有此数据
        /// </summary>
        /// <param name="bookNum"></param>
        /// <returns></returns>
        public int getBookCount(string bookNum)
        {
            string cmdText = "select count(bookNum) from V_SaleMonomer where bookNum=@bookNum";
            string[] param = { "@bookNum" };
            object[] values = { bookNum };
            int row = int.Parse(db.ExecuteScalar(cmdText, param, values).ToString());
            return row;
        }
        /// <summary>
        /// 添加销退单头
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        public int Insert(SellOffMonomer sm)   
        {
            string cmdText = "insert into T_SellOffMonomer(sellOffMonomerId,sellOffHead,bookNum,isbn,price,count,totalPrice,realPrice,dateTime,realDiscount) values(@sellOffMonomerId,@sellOffHeadID,@bookNum,@isbn,@price,@count,@totalPrice,@realPrice,@dateTime,@realDiscount)";
            string[] param = { "@sellOffMonomerId", "@sellOffHeadID", "@bookNum", "@isbn", "@price", "@count", "@totalPrice", "@realPrice", "@dateTime", "@realDiscount" };
            object[] values = { sm.SellOffMonomerId, sm.SellOffHeadId, sm.BookNum, sm.ISBN1, sm.Price, sm.Count, sm.TotalPrice, sm.RealPrice, sm.Time,sm.Discount };
            int row = db.ExecuteNoneQuery(cmdText, param, values);
            return row;
        }
        /// <summary>
        /// 通过单头Id获取单体数量
        /// </summary>
        /// <param name="sellOffHeaId"></param>
        /// <returns></returns>
        public int GetCount(string sellOffHeaId)
        {
            string cmdText = "select COUNT(sellOffMonomerId) from T_SellOffMonomer where sellOffHead=@sellOffHead";
            string[] param = { "@sellOffHead" };
            object[] values = { sellOffHeaId };
            int row = int.Parse(db.ExecuteScalar(cmdText, param, values).ToString());
            return row;
        }
        /// <summary>
        /// 根据单头Id获取任务Id
        /// </summary>
        /// <param name="sellOffHeadId"></param>
        /// <returns></returns>
        public DataSet getSaleTask(string sellOffHeadId)
        {
            string sql = "select saleTaskId from T_SellOffHead where sellOffHeadID=@sellHeadId";
            string[] param = { "@sellHeadId" };
            object[] values = { sellOffHeadId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 根据任务Id获取默认折扣
        /// </summary>
        /// <param name="saleTaskId"></param>
        /// <returns></returns>
        public DataSet getDisCount(string saleTaskId,string bookNum)
        {
            //string sql = "select defaultDiscount from T_SaleTask where saleTaskId=@saleTaskId";
            string sql = "select * from ((select bookNum,realDiscount from v_salemonomer where saleTaskId=@saleTaskId GROUP BY bookNum) as temp) where bookNum=@bookNum";
            string[] param = { "@saleTaskId","@bookNum" };
            object[] values = { saleTaskId,bookNum };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 根据书号到销售单体视图获取信息
        /// </summary>
        /// <param name="bookNum"></param>
        /// <returns></returns>
        public DataSet selctByBookNum(string bookNum,string saleTaskId)
        {
            string sql = "select number from V_SaleMonomer where bookNum=@bookNum and saleTaskId=@saleTaskId";
            string[] param = { "@bookNum", "@saleTaskId" };
            object[] values = { bookNum,saleTaskId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 通过书号和单头Id查询单体表中相应的书籍数量
        /// </summary>
        /// <param name="bookNum"></param>
        /// <param name="sellOffHeadId"></param>
        /// <returns></returns>
        public DataSet selecctSm(string bookNum,string sellOffHeadId)
        {
            string sql = "select count from T_SellOffMonomer where bookNum=@bookNum and sellOffHead=@sellOffHeadId";
            string[] param = { "@bookNum", "@sellOffHeadId" };
            object[] values = { bookNum, sellOffHeadId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 获取该单头的相应统计
        /// </summary>
        /// <param name="sellOffHeadId"></param>
        /// <returns></returns>
        public DataSet getAllNum(string sellOffHeadId)
        {
            string sql = "select sum(count),sum(totalPrice),sum(realPrice) from T_SellOffMonomer where sellOffHead=@sellOffHeadId";
            string[] param = { "@sellOffHeadId" };
            object[] values = { sellOffHeadId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 获取零售排行
        /// </summary>
        /// <returns></returns>
        public DataSet getRetailRank(DateTime startTime,DateTime endTime,string regionName)
        {
            string sql = "select bookNum,bookName,unitPrice,sum(number) as num,sum(totalPrice) as allPrice,dateTime from v_retailmonomer where state=1 and dateTime BETWEEN @startTime and @endTime and regionName=@regionName GROUP BY bookNum ORDER BY num desc limit 0,10";
            string[] param = { "@startTime", "@endTime", "@regionName" };
            object[] values = { startTime, endTime, regionName };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        
        public DataSet searchSalesDetail(string saletaskId,string saleheadId)
        {
            string sql = "select bookNum,bookName,ISBN,unitPrice,realDiscount,sum(number) as allnumber ,sum(realPrice) as allrealPrice,userName from V_SaleMonomer where saleTaskId=@saleTaskId and saleHeadId=@saleHeadId group by bookNum,bookName,ISBN,unitPrice,realDiscount ORDER BY dateTime";
            string[] param = { "@saletaskId", "@saleheadId" };
            object[] values = { saletaskId , saleheadId };
            DataSet ds = db.FillDataSet(sql, param, values);
            return ds;
        }
        /// <summary>
        /// 销退统计获取品种
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="type"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public int getsellOffKinds(string strWhere, string type, string time)
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
            if ((time != "" && time != null))
            {
                cmdText = "select count(bookNum) from ((select bookNum,customerName,supplier,count(bookNum) as kinds,dateTime from v_selloff where "+type+ "= @strWhere and dateTime BETWEEN'" + startTime + "' and '" + endTime + "' GROUP BY bookNum) as temp)";
            }
            else
            {
                cmdText = "select count(bookNum) from ((select bookNum,customerName,supplier,sum(count),dateTime from v_selloff  where " + type + "= @strWhere GROUP BY bookNum) as temp)";
            }
            string[] param = { "@strWhere" };
            object[] values = { strWhere };
            string val = db.ExecuteScalar(cmdText, param, values).ToString();
            int row = int.Parse(val);
            return row;
        }
        /// <summary>
        /// 获取销退操作员
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet getSellOffOperator(string strWhere)
        {
            string cmdText = "select userName,bookName,supplier from v_selloff where " + strWhere + " group by userName ORDER BY convert(userName using gbk) collate gbk_chinese_ci";
            DataSet ds = db.FillDataSet(cmdText, null, null);
            return ds;
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
            if (groupbyType == "supplier")
            {
                exportdt.Columns.Add("供应商", typeof(string));
                cmdText = "select supplier, sum(count) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice from v_selloff where "+strWhere+" order by allTotalPrice desc";
            }
            else if (groupbyType == "regionName")
            {
                exportdt.Columns.Add("地区名称", typeof(string));
                cmdText = "select regionName, sum(count) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice from v_selloff where " + strWhere + " order by allTotalPrice desc";
            }
            else
            {
                exportdt.Columns.Add("客户名称", typeof(string));
                cmdText = "select customerName, sum(count) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice from v_selloff where " + strWhere + " order by allTotalPrice desc";
            }
            DataSet ds = db.FillDataSet(cmdText, null, null);
            exportdt.Columns.Add("书籍品种数", typeof(long));
            exportdt.Columns.Add("书籍总数量", typeof(long));
            exportdt.Columns.Add("总实洋", typeof(double));
            exportdt.Columns.Add("总码洋", typeof(double));
            int allcount = ds.Tables[0].Rows.Count;
            for (int i = 0; i < allcount; i++)
            {
                condition = ds.Tables[0].Rows[i]["" + groupbyType + ""].ToString();
                kinds = getsellOffKinds(condition, groupbyType, time);
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
                //cmdText = "select " + groupbyType + " as " + name + ", ISBN,bookNum as 书号,bookName as 书名,price as 定价,sum(number) as 数量,sum(totalPrice) as 码洋,sum(realPrice) as 实洋,remarks as 销售折扣,dateTime as 采集日期,userName as 采集人用户名, state as 采集状态,supplier as 供应商 from v_salemonomer where " + strWhere + ",booknum order by convert(" + groupbyType + " using gbk) collate gbk_chinese_ci";
                cmdText = "select " + groupbyType + " as " + name + ", ISBN,bookNum as 书号,bookName as 书名,price as 定价,sum(count) as 数量,sum(totalPrice) as 码洋,sum(realPrice) as 实洋,realDiscount as 销售折扣, DATE_FORMAT(dateTime,'%Y-%m-%d %H:%i:%s') as 销退日期,userName as 操作员,supplier as 供应商,dentification as 备注,remarksOne as 备注1,remarksTwo as 备注2,remarksThree as 备注3 from v_selloff where " + strWhere + ",bookNum order by convert(" + groupbyType + " using gbk) collate gbk_chinese_ci";
            }
            else
            {
                cmdText = "select " + groupbyType + " as " + name + ", ISBN,bookNum as 书号,bookName as 书名,price as 定价,sum(count) as 数量,sum(totalPrice) as 码洋,sum(realPrice) as 实洋,realDiscount as 销售折扣,DATE_FORMAT(dateTime,'%Y-%m-%d %H:%i:%s') as 销退日期,userName as 操作员,supplier as 供应商,dentification as 备注,remarksOne as 备注1,remarksTwo as 备注2,remarksThree as 备注3 from v_selloff bookNum order by convert(" + groupbyType + " using gbk) collate gbk_chinese_ci";
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
        /// 销退明细页面导出Excel
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="type">分组条件</param>
        /// <returns></returns>
        public DataTable ExportExcel(string strWhere, string type)
        {
            //String cmdText = "select ISBN,bookNum as 书号,bookName as 书名,price as 单价,sum(number) as 数量, sum(totalPrice) as 码洋,sum(realPrice) as 实洋,realDiscount as 销售折扣,supplier as 供应商,dateTime as 采集时间,userName as 采集人,state from v_salemonomer where " + strWhere + " group by bookNum," + type;
            String cmdText = "select isbn as ISBN,bookNum as 书号,bookName as 书名,price as 单价,sum(count) as 数量, sum(totalPrice) as 码洋,sum(realPrice) as 实洋,realDiscount as 销售折扣,supplier as 供应商,DATE_FORMAT(dateTime,'%Y-%m-%d %H:%i:%s') as 销退日期,userName as 操作员,dentification as 备注,remarksOne as 备注1,remarksTwo as 备注2,remarksThree as 备注3 from v_selloff where " + strWhere + " group by bookNum," + type;
            DataSet ds = db.FillDataSet(cmdText, null, null);
            DataTable dt = null;
            int count = ds.Tables[0].Rows.Count;
            if (count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}
