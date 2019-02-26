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
using System.Web.SessionState;

namespace bms.Web.wechat
{
    using Result = Enums.OpResult;
    /// <summary>
    /// retail 的摘要说明
    /// </summary>
    public class retail : IHttpHandler, IRequiresSessionState
    {
        SaleHead single = new SaleHead();
        StockBll stockBll = new StockBll();
        RetailBll retailBll = new RetailBll();
        retailM retailM = new retailM();
        public void ProcessRequest(HttpContext context)
        {
            string op = context.Request["op"];
            if (op == "isbn")
            {
                isbn(context);
            }
            if (op == "choose")
            {
                choose(context);
            }
            if (op == "bookNum")
            {
                bookNum(context);
            }
            if (op == "insert")
            {
                insert(context);
            }
            if(op == "openid")
            {
                openid(context);
            }
            if(op == "checkDetail")
            {
                checkDetail(context);
            }
            if (op == "del")
            {
                del(context);
            }
        }
        /// <summary>
        /// 第一次扫描isbn
        /// </summary>
        /// <param name="context"></param>
        public void isbn(HttpContext context)
        {
            string isbn = context.Request["isbn"];
            if (isbn != null && isbn != "")
            {
                DataSet bookDs = retailBll.SelectByIsbn(isbn);
                if (bookDs == null)
                {
                    retailM.type = "ISBN不存在";
                    string json = JsonHelper.JsonSerializerBySingleData(retailM);
                    context.Response.Write(json);
                    context.Response.End();
                }
                else
                {
                    int count = bookDs.Tables[0].Rows.Count;
                    if (bookDs != null && count > 0)
                    {
                        if (count == 1)
                        {
                            retailM.type = "一号一书";
                            double discount = 100;
                            if (bookDs.Tables[0].Rows[0]["discount"].ToString() != null && bookDs.Tables[0].Rows[0]["discount"].ToString() != "")
                            {
                                discount = Convert.ToDouble(bookDs.Tables[0].Rows[0]["discount"]);
                            }
                            bookDs.Tables[0].Columns.Add("number", typeof(string));
                            bookDs.Tables[0].Rows[0]["number"] = "1";
                            bookDs.Tables[0].Columns.Add("focus", typeof(bool));
                            bookDs.Tables[0].Rows[0]["focus"] = false;
                            bookDs.Tables[0].Columns.Add("totalPrice", typeof(string));
                            bookDs.Tables[0].Rows[0]["totalPrice"] = bookDs.Tables[0].Rows[0]["price"].ToString();
                            bookDs.Tables[0].Columns.Add("totalReal", typeof(string));
                            bookDs.Tables[0].Rows[0]["totalReal"] = (Convert.ToDouble(bookDs.Tables[0].Rows[0]["price"]) * discount * 0.01).ToString();
                            string data = JsonHelper.ToJson(bookDs.Tables[0], "retail");
                            retailM.data = data;
                            string json = JsonHelper.JsonSerializerBySingleData(retailM);
                            context.Response.Write(json);
                            context.Response.End();
                        }
                        retailM.type = "一号多书";
                        string strJson = JsonHelper.JsonSerializerBySingleData(retailM);
                        context.Response.Write(strJson);
                        context.Response.End();
                    }
                    else
                    {
                        retailM.type = "ISBN不存在";
                        string strJson = JsonHelper.JsonSerializerBySingleData(retailM);
                        context.Response.Write(strJson);
                        context.Response.End();
                    }
                }
            }
        }
        /// <summary>
        /// 一号多书获取数据
        /// </summary>
        /// <param name="context"></param>
        public void choose(HttpContext context)
        {
            string isbn = context.Request["isbn"];
            if (isbn != null && isbn != "")
            {
                DataSet bookDs = retailBll.SelectByIsbn(isbn);
                if (bookDs == null)
                {
                    retailM.type = "ISBN不存在";
                    string json = JsonHelper.JsonSerializerBySingleData(retailM);
                    context.Response.Write(json);
                    context.Response.End();
                }
                else
                {
                    int counts = bookDs.Tables[0].Rows.Count;
                    StringBuilder sb = new StringBuilder();
                    int i = 0;
                    while (i < counts)
                    {
                        DataRow dr = bookDs.Tables[0].Rows[i];
                        string bookNum = dr["bookNum"].ToString();
                        int stockNum = stockBll.selectStockNum(dr["bookNum"].ToString());
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
                        retailM.type = "无库存信息";
                        string json = JsonHelper.JsonSerializerBySingleData(retailM);
                        context.Response.Write(json);
                        context.Response.End();
                    }
                    else if (counts == 1)
                    {
                        retailM.type = "一号一书";
                        double discount = 100;
                        if (bookDs.Tables[0].Rows[0]["discount"].ToString() != null && bookDs.Tables[0].Rows[0]["discount"].ToString() != "")
                        {
                            discount = Convert.ToDouble(bookDs.Tables[0].Rows[0]["discount"]);
                        }
                        bookDs.Tables[0].Columns.Add("number", typeof(string));
                        bookDs.Tables[0].Rows[0]["number"] = "1";
                        bookDs.Tables[0].Columns.Add("focus", typeof(bool));
                        bookDs.Tables[0].Rows[0]["focus"] = false;
                        bookDs.Tables[0].Columns.Add("totalPrice", typeof(string));
                        bookDs.Tables[0].Rows[0]["totalPrice"] = bookDs.Tables[0].Rows[0]["price"].ToString();
                        bookDs.Tables[0].Columns.Add("totalReal", typeof(string));
                        bookDs.Tables[0].Rows[0]["totalReal"] = (Convert.ToDouble(bookDs.Tables[0].Rows[0]["price"]) * discount * 0.01).ToString();
                        string data = JsonHelper.ToJson(bookDs.Tables[0], "retail");
                        retailM.data = data;
                        string json = JsonHelper.JsonSerializerBySingleData(retailM);
                        context.Response.Write(json);
                        context.Response.End();
                    }
                    else
                    {
                        retailM.type = "一号多书数据";
                        bookDs.Tables[0].Columns.Add("totalPrice", typeof(string));
                        bookDs.Tables[0].Columns.Add("number", typeof(string));
                        bookDs.Tables[0].Columns.Add("totalReal", typeof(string));
                        bookDs.Tables[0].Columns.Add("color", typeof(string));
                        for (int j = 0; j < bookDs.Tables[0].Rows.Count; j++)
                        {
                            double discount = 100;
                            if (bookDs.Tables[0].Rows[j]["discount"].ToString() != null && bookDs.Tables[0].Rows[j]["discount"].ToString() != "")
                            {
                                discount = Convert.ToDouble(bookDs.Tables[0].Rows[j]["discount"]);
                            }
                            bookDs.Tables[0].Rows[j]["number"] = "1";
                            bookDs.Tables[0].Rows[j]["totalPrice"] = bookDs.Tables[0].Rows[j]["price"].ToString();
                            bookDs.Tables[0].Rows[j]["totalReal"] = (Convert.ToDouble(bookDs.Tables[0].Rows[j]["price"]) * discount * 0.01).ToString();
                            bookDs.Tables[0].Rows[j]["color"] = "";
                        }
                        string data = JsonHelper.ToJson(bookDs.Tables[0], "retail");
                        retailM.data = data;
                        string json = JsonHelper.JsonSerializerBySingleData(retailM);
                        context.Response.Write(json);
                        context.Response.End();
                    }
                }
            }
        }
        /// <summary>
        /// 根据书号查找书籍信息
        /// </summary>
        /// <param name="context"></param>
        public void bookNum(HttpContext context)
        {
            string bookNum = context.Request["bookNum"];
            DataSet bookDs = retailBll.SelectByBookNum(bookNum);
            if (bookDs == null)
            {
                retailM.type = "找不到书籍信息";
                string json = JsonHelper.JsonSerializerBySingleData(retailM);
                context.Response.Write(json);
                context.Response.End();
            }
            else
            {
                int count = bookDs.Tables[0].Rows.Count;
                if (bookDs != null && count > 0)
                {
                    retailM.type = "一号一书";
                    double discount = 100;
                    if (bookDs.Tables[0].Rows[0]["discount"].ToString() != null && bookDs.Tables[0].Rows[0]["discount"].ToString() != "")
                    {
                        discount = Convert.ToDouble(bookDs.Tables[0].Rows[0]["discount"]);
                    }
                    bookDs.Tables[0].Columns.Add("number", typeof(string));
                    bookDs.Tables[0].Rows[0]["number"] = "1";
                    bookDs.Tables[0].Columns.Add("focus", typeof(bool));
                    bookDs.Tables[0].Rows[0]["focus"] = false;
                    bookDs.Tables[0].Columns.Add("totalPrice", typeof(string));
                    bookDs.Tables[0].Rows[0]["totalPrice"] = bookDs.Tables[0].Rows[0]["price"].ToString();
                    bookDs.Tables[0].Columns.Add("totalReal", typeof(string));
                    bookDs.Tables[0].Rows[0]["totalReal"] = (Convert.ToDouble(bookDs.Tables[0].Rows[0]["price"]) * discount * 0.01).ToString();
                    string data = JsonHelper.ToJson(bookDs.Tables[0], "retail");
                    retailM.data = data;
                    string json = JsonHelper.JsonSerializerBySingleData(retailM);
                    context.Response.Write(json);
                    context.Response.End();
                }
                else
                {
                    retailM.type = "找不到书籍信息";
                    string strJson = JsonHelper.JsonSerializerBySingleData(retailM);
                    context.Response.Write(strJson);
                    context.Response.End();
                }
            }
        }
        /// <summary>
        /// 插入数据库
        /// </summary>
        /// <param name="context"></param>
        public void insert(HttpContext context)
        {
            string openid = context.Request["openid"];
            string json = context.Request["data"];
            DataTable dataTable = jsonToDt(json);
            string kindNum = context.Request["kindNum"];
            string totalNumber = context.Request["totalNumber"];
            string totalPrice = context.Request["totalPrice"];
            string totalReal = context.Request["totalReal"];
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
            single.AllRealPrice = Convert.ToDouble(totalReal);
            single.AllTotalPrice = Convert.ToDouble(totalPrice);
            single.KindsNum = Convert.ToInt32(kindNum);
            single.Number = Convert.ToInt32(totalNumber);
            single.RegionId = 67;
            single.SaleHeadId = retailHeadId;
            single.UserId = "99999";
            single.DateTime = DateTime.Now;
            single.State = 0;
            single.PayType = "未支付";
            single.OpenId = openid;
            Result result = retailBll.Insert(single);
            if (result == Result.添加成功)
            {
                SaleMonomer monomers = new SaleMonomer();
                int Count = dataTable.Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    DataRow dr = dataTable.Rows[i];
                    monomers.ISBN1 = dr["ISBN"].ToString();
                    monomers.UnitPrice = Convert.ToDouble(dr["price"]);
                    monomers.BookNum = dr["bookNum"].ToString();
                    monomers.RealDiscount = Convert.ToDouble(dr["discount"]);
                    monomers.SaleIdMonomerId = i + 1;
                    monomers.Number = Convert.ToInt32(dr["number"]);
                    monomers.TotalPrice = Convert.ToDouble(dr["totalPrice"]);
                    monomers.RealPrice = Convert.ToDouble(dr["totalReal"]);
                    monomers.SaleHeadId = retailHeadId;
                    monomers.Datetime = DateTime.Now;
                    Result mon = retailBll.InsertRetail(monomers);
                    if (mon == Result.添加失败)
                    {
                        retailM.type = "添加失败";
                        string jsonFail = JsonHelper.JsonSerializerBySingleData(retailM);
                        context.Response.Write(jsonFail);
                        context.Response.End();
                    }
                }
                retailM.type = "添加成功";
                retailM.retailHeadId = retailHeadId;
                string jsonSucc = JsonHelper.JsonSerializerBySingleData(retailM);
                context.Response.Write(jsonSucc);
                context.Response.End();
            }
            else
            {
                retailM.type = "添加失败";
                string jsonFail = JsonHelper.JsonSerializerBySingleData(retailM);
                context.Response.Write(jsonFail);
                context.Response.End();
            }

        }
        /// <summary>
        /// 用户查看我的订单
        /// </summary>
        /// <param name="context"></param>
        public void openid(HttpContext context)
        {
            string openid = context.Request["openid"];
            if(openid !=null && openid != "")
            {
                DataSet dsHead = retailBll.selectHead(openid);
                if(dsHead == null|| dsHead.Tables[0].Rows.Count <= 0)
                {
                    retailM.type = "暂无订单信息";
                    string json = JsonHelper.JsonSerializerBySingleData(retailM);
                    context.Response.Write(json);
                    context.Response.End();
                }
                else
                {
                    retailM.type = "数据";
                    string data = JsonHelper.ToJson(dsHead.Tables[0], "retail");
                    retailM.data = data;
                    string json = JsonHelper.JsonSerializerBySingleData(retailM);
                    context.Response.Write(json);
                    context.Response.End();
                }
            }
        }
        /// <summary>
        /// 查看详情页面
        /// </summary>
        /// <param name="context"></param>
        public void checkDetail(HttpContext context)
        {
            string headid = context.Request["headid"];
            SaleHead retail = retailBll.GetHead(headid);
            if (retail == null)
            {
                retailM.type = "暂无订单信息";
                string json = JsonHelper.JsonSerializerBySingleData(retailM);
                context.Response.Write(json);
                context.Response.End();
            }
            else
            {
                DataSet dsDetail = retailBll.GetRetail(headid);
                if (dsDetail == null || dsDetail.Tables[0].Rows.Count <= 0)
                {
                    retailM.type = "暂无订单信息";
                    string json = JsonHelper.JsonSerializerBySingleData(retailM);
                    context.Response.Write(json);
                    context.Response.End();
                }
                else
                {
                    if (retail.PayType == "未支付")
                    {
                        retailM.type = "未支付";
                        retailM.retailHeadId = retail.SaleHeadId;
                        retailM.allNumber = retail.Number.ToString();
                        retailM.kindsNum = retail.KindsNum.ToString();
                        retailM.allTotalPrice = retail.AllTotalPrice.ToString();
                        retailM.allRealPrice = retail.AllRealPrice.ToString();
                        string data = JsonHelper.ToJson(dsDetail.Tables[0], "retail");
                        retailM.data = data;
                        string json = JsonHelper.JsonSerializerBySingleData(retailM);
                        context.Response.Write(json);
                        context.Response.End();
                    }
                    else
                    {
                        retailM.allNumber = retail.Number.ToString();
                        retailM.kindsNum = retail.KindsNum.ToString();
                        retailM.allTotalPrice = retail.AllTotalPrice.ToString();
                        retailM.allRealPrice = retail.AllRealPrice.ToString();
                        retailM.type = "数据";
                        string data = JsonHelper.ToJson(dsDetail.Tables[0], "retail");
                        retailM.data = data;
                        string json = JsonHelper.JsonSerializerBySingleData(retailM);
                        context.Response.Write(json);
                        context.Response.End();
                    }
                }
            }
        }
        /// <summary>
        /// 小程序删除单头
        /// </summary>
        /// <param name="context"></param>
        public void del(HttpContext context)
        {
            string headId = context.Request["headId"];
            Result del = retailBll.UpdateDel(headId);
            if(del == Result.删除成功)
            {
                retailM.type = "删除成功";
                string json = JsonHelper.JsonSerializerBySingleData(retailM);
                context.Response.Write(json);
                context.Response.End();
            }
            else
            {
                retailM.type = "删除失败";
                string json = JsonHelper.JsonSerializerBySingleData(retailM);
                context.Response.Write(json);
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