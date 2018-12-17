using bms.Bll;
using bms.DBHelper;
using bms.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace bms.Web.wechat
{
    using Result = Enums.OpResult;
    /// <summary>
    /// retail 的摘要说明
    /// </summary>
    public class retailModel : IHttpHandler
    {
        SaleHead single = new SaleHead();
        UserBll userBll = new UserBll();
        StockBll stockBll = new StockBll();
        BookBasicBll basicBll = new BookBasicBll();
        GoodsShelvesBll goods = new GoodsShelvesBll();
        DataTable monTable = new DataTable();
        RetailBll retailBll = new RetailBll();
        User user = new User();
        public List<string> bookNumList = new List<string>();
        retailM retail = new retailM();
        public void ProcessRequest(HttpContext context)
        {
            string op = context.Request.QueryString["op"];
            if (op == "isbn")
            {
                string isbn = context.Request.QueryString["isbn"];
                if (isbn != null && isbn != "")
                {
                    BookBasicBll bookBasicBll = new BookBasicBll();
                    DataSet bookDs = bookBasicBll.SelectByIsbn(isbn);
                    if (bookDs == null)
                    {
                        retail.type = "ISBN不存在";
                        string json = JsonHelper.JsonSerializerBySingleData(retail);
                        context.Response.Write(json);
                        context.Response.End();
                    }
                    else
                    {
                        int count = bookDs.Tables[0].Rows.Count;
                        if (bookDs != null && bookDs.Tables[0].Rows.Count > 0)
                        {
                            if (count == 1)
                            {
                                retail.type = "一号一书";
                                string data = JsonHelper.ToJson(bookDs.Tables[0], "retail");
                                retail.data = data;
                                retail.price = bookDs.Tables[0].Rows[0]["price"].ToString();
                                retail.author = bookDs.Tables[0].Rows[0]["author"].ToString();
                                string json = JsonHelper.JsonSerializerBySingleData(retail);
                                context.Response.Write(json);
                                context.Response.End();
                            }
                            if (op == "choose")
                            {
                                int counts = bookDs.Tables[0].Rows.Count;
                                StringBuilder sb = new StringBuilder();
                                int i = 0;
                                while (i < counts)
                                {
                                    DataRow dr = bookDs.Tables[0].Rows[i];
                                    user = (User)context.Session["user"];
                                    string bookNum = dr["bookNum"].ToString();
                                    int stockNum = stockBll.selectStockNum(dr["bookNum"].ToString(), user.ReginId.RegionId);
                                    if (stockNum <= 0)
                                    {
                                        bookDs.Tables[0].Rows.RemoveAt(i);
                                        counts--;
                                    }
                                    else
                                    {
                                        i++;
                                    }
                                }
                                if (counts == 0)
                                {
                                    retail.type = "无库存";
                                    string json = JsonHelper.JsonSerializerBySingleData(retail);
                                    context.Response.Write(json);
                                    context.Response.End();
                                }
                                else if (counts == 1)
                                {
                                    retail.type = "一号一书";
                                    string data = JsonHelper.ToJson(bookDs.Tables[0],"retail");
                                    retail.data = data;
                                    retail.price = bookDs.Tables[0].Rows[0]["price"].ToString();
                                    double author = 100;
                                    if(bookDs.Tables[0].Rows[0]["author"].ToString() != null && bookDs.Tables[0].Rows[0]["author"].ToString() != "")
                                    {
                                        author = Convert.ToDouble(bookDs.Tables[0].Rows[0]["author"]);
                                    }
                                    retail.realPrice = (Convert.ToDouble(bookDs.Tables[0].Rows[0]["price"]) * author * 0.01).ToString();
                                    string json = JsonHelper.JsonSerializerBySingleData(retail);
                                    context.Response.Write(json);
                                    context.Response.End();
                                }
                                else
                                {
                                    retail.type = "一号多书数据";
                                    string data = JsonHelper.ToJson(bookDs.Tables[0], "retail");
                                    retail.data = data;
                                    string json = JsonHelper.JsonSerializerBySingleData(retail);
                                    context.Response.Write(json);
                                    context.Response.End();
                                }
                            }
                            retail.type = "一号多书";
                            string strJson = JsonHelper.JsonSerializerBySingleData(retail);
                            context.Response.Write(strJson);
                            context.Response.End();
                        }
                        else
                        {
                            retail.type = "ISBN不存在";
                            string strJson = JsonHelper.JsonSerializerBySingleData(retail);
                            context.Response.Write(strJson);
                            context.Response.End();
                        }
                    }
                }
            }
            if(op == "insert")
            {
                insert(context);
            }
        }
        public void insert(HttpContext context)
        {
            string json = context.Request.QueryString["json"];
            DataTable dataTable = jsonToDt(json);
            int row, rows = 0;
            double total, allTotal = 0, real, allReal = 0;
            int Counts = dataTable.Rows.Count;
            for (int i = 0; i < Counts; i++)
            {
                DataRow dr = dataTable.Rows[i];
                row = Convert.ToInt32(dr["数量"]);
                total = Convert.ToDouble(dr["码洋"]);
                real = Convert.ToDouble(dr["实洋"]);
                rows = rows + row;
                allTotal = allTotal + total;
                allReal = allReal + real;
            }
            DateTime nowTime = DateTime.Now;
            string nowDt = nowTime.ToString("yyyy-MM-dd");
            long count = 0;
            //判断数据库中是否已经有记录
            DataSet backds = retailBll.getAllTime(0);
            if (backds != null && backds.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < backds.Tables[0].Rows.Count; j++)
                {
                    string time = backds.Tables[0].Rows[j]["dateTime"].ToString();
                    DateTime dt = Convert.ToDateTime(time);
                    string sqlTime = dt.ToString("yyyy-MM-dd");
                    if (sqlTime == nowDt)
                    {
                        //count += 1;
                        string id = backds.Tables[0].Rows[j]["retailHeadId"].ToString();
                        int st1 = Convert.ToInt32(id.Substring(10));
                        if (st1 <= 0)
                        {
                            st1 = 0;
                        }
                        count = st1 + 1;
                        break;
                    }
                    else
                    {
                        count = 1;
                        break;
                    }
                }
                if (count == 0)
                {
                    count = 1;
                }
            }
            else
            {
                count = 1;
            }
            string retailHeadId = "LS" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
            single.AllRealPrice = allReal;
            single.AllTotalPrice = allTotal;
            single.KindsNum = Counts;
            single.Number = rows;
            single.RegionId = user.ReginId.RegionId;
            single.SaleHeadId = retailHeadId;
            single.UserId = user.UserId;
            single.DateTime = DateTime.Now;
            single.State = 0;
            single.PayType = "未支付";
            retail.retailHeadId = retailHeadId;
            Result result = retailBll.InsertRetail(single);
            if (result == Result.添加成功)
            {
                context.Session["List"] = new List<long>();
                SaleMonomer monomers = new SaleMonomer();
                int Count = dataTable.Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    DataRow dr = dataTable.Rows[i];
                    monomers.ISBN1 = dr["ISBN"].ToString();
                    monomers.UnitPrice = Convert.ToDouble(dr["单价"]);
                    monomers.BookNum = dr["书号"].ToString();
                    monomers.RealDiscount = Convert.ToDouble(dr["折扣"]);
                    monomers.SaleIdMonomerId = i + 1;
                    monomers.Number = Convert.ToInt32(dr["数量"]);
                    monomers.TotalPrice = Convert.ToDouble(dr["码洋"]);
                    monomers.RealPrice = Convert.ToDouble(dr["实洋"]);
                    monomers.SaleHeadId = retailHeadId;
                    monomers.Datetime = DateTime.Now;
                    Result mon = retailBll.InsertRetail(monomers);
                    if (mon == Result.添加失败)
                    {
                        retail.type = "添加失败";
                        string jsonFail = JsonHelper.JsonSerializerBySingleData(retail);
                        context.Response.Write(jsonFail);
                        context.Response.End();
                    }
                }
                retail.type = "添加成功";
                retail.retailHeadId = retailHeadId;
                string jsonSucc = JsonHelper.JsonSerializerBySingleData(retail);
                context.Response.Write(jsonSucc);
                context.Response.End();
            }
            else
            {
                retail.type = "添加失败";
                string jsonFail = JsonHelper.JsonSerializerBySingleData(retail);
                context.Response.Write(jsonFail);
                context.Response.End();
            }

        }

        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public DataTable jsonToDt(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        //Columns
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        //Rows
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }
                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}