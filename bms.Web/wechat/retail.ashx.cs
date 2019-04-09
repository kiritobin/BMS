using bms.Bll;
using bms.DBHelper;
using bms.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
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
        RegionBll regionBll = new RegionBll();
        UserBll userBll = new UserBll();
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
            if(op == "del")
            {
                del(context);
            }
            if(op == "regionSubmit")
            {
                string userId = context.Request["userId"];
                Result isUser = userBll.isUser(userId);
                int regionId = Convert.ToInt32(userId.Substring(0, userId.Length - 2));
                string regionName = regionBll.selectById(regionId);
                if (isUser == Result.记录不存在)
                {
                    retailM.type = "未查询到相关组织信息";
                    string json = JsonHelper.JsonSerializerBySingleData(retailM);
                    context.Response.Write(json);
                    context.Response.End();
                }
                else
                {
                    retailM.type = "成功";
                    retailM.userid = userId;
                    retailM.regionId = regionId.ToString();
                    retailM.regionName = regionName;
                    string json = JsonHelper.JsonSerializerBySingleData(retailM);
                    context.Response.Write(json);
                    context.Response.End();
                }
            }
            if (op == "getOpenid")
            {
                getOponid(context);
            }
        }
        /// <summary>
        /// 第一次扫描isbn
        /// </summary>
        /// <param name="context"></param>
        public void isbn(HttpContext context)
        {
            string isbn = context.Request["isbn"];
            string regionId = context.Request["regionId"];
            if (isbn != null && isbn != "")
            {
                DataTable bookDs = retailBll.SelectByIsbn(isbn);
                if (bookDs == null)
                {
                    retailM.type = "ISBN不存在";
                    string json = JsonHelper.JsonSerializerBySingleData(retailM);
                    context.Response.Write(json);
                    context.Response.End();
                }
                else
                {
                    int count = bookDs.Rows.Count;
                    if (bookDs != null && count > 0)
                    {
                        if (count == 1)
                        {
                            string bookNum = bookDs.Rows[0]["bookNum"].ToString();
                            int stockNum = stockBll.selectStockNum(bookNum, Convert.ToInt32(regionId));
                            if (stockNum <= 0)
                            {
                                retailM.type = "无库存信息";
                                string json = JsonHelper.JsonSerializerBySingleData(retailM);
                                context.Response.Write(json);
                                context.Response.End();
                            }
                            else
                            {
                                retailM.type = "一号一书";
                                double discount = 100;
                                if (bookDs.Rows[0]["discount"].ToString() != null && bookDs.Rows[0]["discount"].ToString() != "")
                                {
                                    discount = Convert.ToDouble(bookDs.Rows[0]["discount"]);
                                }
                                bookDs.Columns.Add("number", typeof(string));
                                bookDs.Rows[0]["number"] = "1";
                                bookDs.Columns.Add("focus", typeof(bool));
                                bookDs.Rows[0]["focus"] = false;
                                bookDs.Columns.Add("totalPrice", typeof(string));
                                bookDs.Rows[0]["totalPrice"] = bookDs.Rows[0]["price"].ToString();
                                bookDs.Columns.Add("totalReal", typeof(string));
                                bookDs.Rows[0]["totalReal"] = (Convert.ToDouble(bookDs.Rows[0]["price"]) * discount * 0.01).ToString();
                                bookDs.Columns.Add("stockNum", typeof(string));
                                bookDs.Rows[0]["stockNum"] = stockNum.ToString();
                                string data = JsonHelper.ToJson(bookDs, "retail");
                                retailM.data = data;
                                string json = JsonHelper.JsonSerializerBySingleData(retailM);
                                context.Response.Write(json);
                                context.Response.End();
                            }
                        }
                        else
                        {
                            retailM.type = "一号多书";
                            string strJson = JsonHelper.JsonSerializerBySingleData(retailM);
                            context.Response.Write(strJson);
                            context.Response.End();
                        }
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
            string regionId = context.Request["regionId"];
            if (isbn != null && isbn != "")
            {
                DataTable bookDs = retailBll.SelectByIsbn(isbn);
                if (bookDs == null)
                {
                    retailM.type = "ISBN不存在";
                    string json = JsonHelper.JsonSerializerBySingleData(retailM);
                    context.Response.Write(json);
                    context.Response.End();
                }
                else
                {
                    int counts = bookDs.Rows.Count;
                    StringBuilder sb = new StringBuilder();
                    int i = 0;
                    bookDs.Columns.Add("stockNum", typeof(string));
                    while (i < counts)
                    {
                        DataRow dr = bookDs.Rows[i];
                        string bookNum = dr["bookNum"].ToString();
                        int stockNum = stockBll.selectStockNum(dr["bookNum"].ToString(), Convert.ToInt32(regionId));
                        if (stockNum <= 0)
                        {
                            bookDs.Rows.RemoveAt(i);
                            counts--;
                        }
                        else
                        {
                            bookDs.Rows[i]["stockNum"] = stockNum.ToString();
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
                        if (bookDs.Rows[0]["discount"].ToString() != null && bookDs.Rows[0]["discount"].ToString() != "")
                        {
                            discount = Convert.ToDouble(bookDs.Rows[0]["discount"]);
                        }
                        bookDs.Columns.Add("number", typeof(string));
                        bookDs.Rows[0]["number"] = "1";
                        bookDs.Columns.Add("focus", typeof(bool));
                        bookDs.Rows[0]["focus"] = false;
                        bookDs.Columns.Add("totalPrice", typeof(string));
                        bookDs.Rows[0]["totalPrice"] = bookDs.Rows[0]["price"].ToString();
                        bookDs.Columns.Add("totalReal", typeof(string));
                        bookDs.Rows[0]["totalReal"] = (Convert.ToDouble(bookDs.Rows[0]["price"]) * discount * 0.01).ToString();
                        string data = JsonHelper.ToJson(bookDs, "retail");
                        retailM.data = data;
                        string json = JsonHelper.JsonSerializerBySingleData(retailM);
                        context.Response.Write(json);
                        context.Response.End();
                    }
                    else
                    {
                        retailM.type = "一号多书数据";
                        bookDs.Columns.Add("totalPrice", typeof(string));
                        bookDs.Columns.Add("number", typeof(string));
                        bookDs.Columns.Add("totalReal", typeof(string));
                        bookDs.Columns.Add("color", typeof(string));
                        for (int j = 0; j < bookDs.Rows.Count; j++)
                        {
                            double discount = 100;
                            if (bookDs.Rows[j]["discount"].ToString() != null && bookDs.Rows[j]["discount"].ToString() != "")
                            {
                                discount = Convert.ToDouble(bookDs.Rows[j]["discount"]);
                            }
                            bookDs.Rows[j]["number"] = "1";
                            bookDs.Rows[j]["totalPrice"] = bookDs.Rows[j]["price"].ToString();
                            bookDs.Rows[j]["totalReal"] = (Convert.ToDouble(bookDs.Rows[j]["price"]) * discount * 0.01).ToString();
                            bookDs.Rows[j]["color"] = "";
                        }
                        string data = JsonHelper.ToJson(bookDs, "retail");
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
            string regionId = context.Request["regionId"];
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
                    int stockNum = stockBll.selectStockNum(bookNum, Convert.ToInt32(regionId));
                    bookDs.Tables[0].Columns.Add("stockNum", typeof(string));
                    bookDs.Tables[0].Rows[0]["stockNum"] = stockNum.ToString();
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
            string userId = context.Request["userid"];
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
            int Length = userId.Length;
            string regionId = userId.Substring(0, userId.Length - 2);
            string retailHeadId = "LS" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
            single.AllRealPrice = Convert.ToDouble(totalReal);
            single.AllTotalPrice = Convert.ToDouble(totalPrice);
            single.KindsNum = Convert.ToInt32(kindNum);
            single.Number = Convert.ToInt32(totalNumber);
            single.RegionId = Convert.ToInt32(regionId);
            single.SaleHeadId = retailHeadId;
            single.UserId = userId;
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
        /// <summary>
        /// List数组 转换成Json字符串
        /// </summary>
        /// <typeparam name="T">list数组类型</typeparam>
        /// <param name="list">list数组</param>
        /// <returns></returns>
        public string ListToJson<T>(List<T> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            int row = list.Count;
            for(int i=0;i< row; i++)
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, list[i]);
                StreamReader reader = new StreamReader(ms);
                ms.Position = 0;
                string str = reader.ReadToEnd();
                reader.Close();
                ms.Close();
                sb.Append(str);
                sb.Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]");
            return sb.ToString();
        }
        /// <summary>
        /// 获取微信openid
        /// </summary>
        /// <param name="context"></param>
        public void getOponid(HttpContext context)
        {
            string js_code = context.Request["js_code"];
            string serviceAddress = "https://api.weixin.qq.com/sns/jscode2session?appid=wx1c7c98a39f0501b6&secret=b12cb19870e0fc79c88b8d6eddfb39f0&js_code=" + js_code + "&grant_type=authorization_code";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            string json = JsonHelper.JsonSerializerBySingleData(retString);
            context.Response.Write(json);
            context.Response.End();
        }
    }
}