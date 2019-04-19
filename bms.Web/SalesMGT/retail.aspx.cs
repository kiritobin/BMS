using bms.Bll;
using bms.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.SalesMGT
{
    using Result = Enums.OpResult;
    public partial class retail : System.Web.UI.Page
    {
        protected DataSet ds;
        protected int pageSize = 20, totalCount, intPageCount;
        protected string headID="";
        public double discount;
        SaleHead single = new SaleHead();
        UserBll userBll = new UserBll();
        StockBll stockBll = new StockBll();
        BookBasicBll basicBll = new BookBasicBll();
        GoodsShelvesBll goods = new GoodsShelvesBll();
        DataTable monTable = new DataTable();
        RetailBll retailBll = new RetailBll();
        User user = new User();
        LoginBll loginBll = new LoginBll();
        Common com = new Common();
        public List<string> bookNumList = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            getIsbn();
            if (!IsPostBack)
            {
                string userID = Request.QueryString["userId"].ToString();
                if (userID != null && userID != "")
                {
                    user = loginBll.getPwdByUserId(userID);
                    if (user.UserName == null)
                    {
                        Response.Write("无此用户信息，请检查！！！");
                        Response.End();
                    }
                    else
                    {
                        Session["user"] = user;
                    }
                }
                else
                {
                    user = (User)Session["user"];
                }
            }
            string op = Request["op"];
            if (op == "add")
            {
                string bookNum = Request["bookNum"].ToString();
                add(bookNum,"one");
            }
            if(op == "insert")
            {
                insert();
            }
            if(op == "delete")
            {
                string bookNum = Request["bookNum"].ToString();
                bookNumList = (List<string>)Session["List"];
                int index = bookNumList.IndexOf(bookNum);
                bookNumList.RemoveAt(index);
                Session["List"] = bookNumList;
            }
            if(op == "preRecord")
            {
                string headId = Request["headId"];
                SaleHead sale = retailBll.GetHead(headId);
                if (sale == null)
                {
                    Response.Write("无记录:|");
                    Response.End();
                }
                else
                {
                    Response.Write(sale.KindsNum + ":|" + sale.Number + ":|" + sale.AllTotalPrice + ":|" + sale.AllRealPrice);
                    Response.End();
                }
            }
        }

        public string getIsbn()
        {
            string op = Request["op"];
            string isbn = Request["isbn"];
            string kind = Request["kind"];
            if(kind == "0")
            {
                Session["List"] = new List<string>();
            }
            double disCount = Convert.ToDouble(Request["disCount"]);
            if (isbn != null && isbn != "")
            {
                BookBasicBll bookBasicBll = new BookBasicBll();
                DataSet bookDs = bookBasicBll.SelectByIsbn(isbn);
                if (bookDs == null)
                {
                    Response.Write("ISBN不存在");
                    Response.End();
                }
                else
                {
                    int count = bookDs.Tables[0].Rows.Count;
                    if (bookDs != null && bookDs.Tables[0].Rows.Count > 0)
                    {
                        if (count == 1)
                        {
                            string bookNum = bookDs.Tables[0].Rows[0]["bookNum"].ToString();
                            add(bookNum,"one");
                        }
                        if (op == "choose")
                        {
                            int counts = bookDs.Tables[0].Rows.Count;
                            StringBuilder sb = new StringBuilder();
                            int i = 0;
                            while (i < counts)
                            {
                                DataRow dr = bookDs.Tables[0].Rows[i];
                                user = (User)Session["user"];
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
                                Response.Write("无库存|:");
                                Response.End();
                            }
                            else if (counts == 1)
                            {
                                add(bookDs.Tables[0].Rows[0]["bookNum"].ToString(),"other");
                            }
                            else
                            {
                                counts = bookDs.Tables[0].Rows.Count;
                                for (int j = 0; j < counts; j++)
                                {
                                    DataRow dr = bookDs.Tables[0].Rows[j];
                                    sb.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + dr["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                                    sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                                    sb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
                                    sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                                    sb.Append("<td>" + dr["price"].ToString() + "</td>");
                                    sb.Append("<td>" + dr["supplier"].ToString() + "</td></tr>");
                                }
                                Response.Write("|:"+sb.ToString());
                                Response.End();
                            }
                        }
                        Response.Write("一号多书");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("ISBN不存在");
                        Response.End();
                    }
                }
            }
            return null;
        }

        public void add(string bookNum,string addType)
        {
            bookNumList = (List<string>)Session["List"];
            foreach (string bookNums in bookNumList)
            {
                if(bookNums == bookNum)
                {
                    if (addType == "other")
                    {
                        Response.Write("已添加过此图书|:");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("已添加过此图书");
                        Response.End();
                    }
                }
            }
            BookBasicData bookBasicData = basicBll.SelectById(bookNum);
            string isbn = bookBasicData.Isbn;
            string bookName = bookBasicData.BookName;
            double discount=100;
            if (bookBasicData.Remarks == "")
            {
                discount = 100;
            }
            else
            {
                discount = Convert.ToInt32(bookBasicData.Remarks);
                if (discount <= 1)
                {
                    discount = discount * 100;
                }
            }
            int row = monTable.Rows.Count;
            double uPrice = bookBasicData.Price;
            SaleMonomer monomers = new SaleMonomer();
            monTable.Columns.Add("ISBN", typeof(string));
            monTable.Columns.Add("unitPrice", typeof(double));
            monTable.Columns.Add("bookNum", typeof(long));
            monTable.Columns.Add("bookName", typeof(string));
            monTable.Columns.Add("realDiscount", typeof(double));
            monTable.Columns.Add("number", typeof(int));
            monTable.Columns.Add("totalPrice", typeof(double));
            monTable.Columns.Add("realPrice", typeof(double));
            DataRow monRow = monTable.NewRow();
            monRow["ISBN"] = isbn;
            monRow["unitPrice"] = uPrice;
            monRow["bookNum"] = bookNum;
            monRow["bookName"] = bookName;
            monRow["realDiscount"] = discount;
            monRow["number"] = 1;
            monRow["totalPrice"] = uPrice;
            monRow["realPrice"] = uPrice * discount * 0.01;
            monTable.Rows.Add(monRow);
            StringBuilder sb = new StringBuilder();
            int counts = monTable.Rows.Count;
            for (int i = 0; i < counts; i++)
            {
                bookNumList.Add(bookNum);
                Session["List"] = bookNumList;
                DataRow dr = monTable.Rows[i];
                sb.Append("<tr><td>" + dr["ISBN"].ToString() + "</td>");
                sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                sb.Append("<td>" + dr["unitPrice"].ToString() + "</td>");
                sb.Append("<td style='display:none'>" + dr["number"].ToString() + "</td>");
                sb.Append("<td><div class='gw_num' style='width:100%'><em class='jian' style='height:100%;width:40%;'>-</em>");
                sb.Append("<input type = 'text' min='1' value='" + dr["number"].ToString() + "' class='num' readonly='readonly' style='width:20%;height:100%'/>");
                sb.Append("<em class='add' style='height:100%;width:40%;'>+</em></div></td>");
                sb.Append("<td>" + dr["realDiscount"].ToString() + "</td>");
                sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                sb.Append("<td style='display:none'>" + dr["realPrice"].ToString() + "</td>");
                sb.Append("<td style='display:none'>" + dr["bookNum"].ToString() + "</td>");
                sb.Append("<td><button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash'></i></button></td></tr>");
            }
            if (addType == "other")
            {
                Response.Write(addType + "|:" + sb.ToString());
                Response.End();
            }
            else
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            
        }

        public void insert()
        {
            string json = Request["json"];
            DataTable dataTable = jsonToDt(json);
            int row, rows=0;
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
            DateTime nowTime = Convert.ToDateTime(com.getDate());
            string nowDt = nowTime.ToString("yyyy-MM-dd");
            long count = 0;
            //判断数据库中是否已经有记录
            DataSet backds = retailBll.getAllTime(0);
            if (backds != null && backds.Tables[0].Rows.Count > 0)
            {
                string time = backds.Tables[0].Rows[0]["dateTime"].ToString();
                DateTime dt = Convert.ToDateTime(time);
                string sqlTime = dt.ToString("yyyy-MM-dd");
                if (sqlTime == nowDt)
                {
                    //count += 1;
                    string id = backds.Tables[0].Rows[0]["retailHeadId"].ToString();
                    int st1 = Convert.ToInt32(id.Substring(10));
                    if (st1 <= 0)
                    {
                        st1 = 0;
                    }
                    count = st1 + 1;
                }
                else
                {
                    count = 1;
                }
            }
            else
            {
                count = 1;
            }
            string retailHeadId = "LS" + Convert.ToDateTime(com.getDate()).ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
            single.AllRealPrice = allReal;
            single.AllTotalPrice = allTotal;
            single.KindsNum = Counts;
            single.Number = rows;
            single.RegionId = user.ReginId.RegionId;
            single.SaleHeadId = retailHeadId;
            single.UserId = user.UserId;
            single.DateTime = Convert.ToDateTime(com.getDate());
            single.State = 0;
            single.PayType = "未支付";
            headID = retailHeadId;
            Result result = retailBll.InsertRetail(single);
            if (result == Result.添加成功)
            {
                Session["List"] = new List<long>();
                SaleMonomer monomers = new SaleMonomer();
                int Count = dataTable.Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    DataRow dr = dataTable.Rows[i];
                    monomers.ISBN1 = dr["ISBN"].ToString();
                    monomers.UnitPrice = Convert.ToDouble(dr["单价"]);
                    monomers.BookNum = dr["书号"].ToString();
                    monomers.RealDiscount = Convert.ToDouble(dr["折扣"]);
                    monomers.SaleIdMonomerId = i+1;
                    monomers.Number = Convert.ToInt32(dr["数量"]);
                    monomers.TotalPrice = Convert.ToDouble(dr["码洋"]);
                    monomers.RealPrice = Convert.ToDouble(dr["实洋"]);
                    monomers.SaleHeadId = retailHeadId;
                    monomers.Datetime = Convert.ToDateTime(com.getDate());
                    Result mon = retailBll.InsertRetail(monomers);
                    if (mon == Result.添加失败)
                    {
                        Response.Write("添加失败|:");
                        Response.End();
                    }
                }
                Response.Write("添加成功|:" + retailHeadId);
                Response.End();
            }
            else
            {
                Response.Write("添加失败|:");
                Response.End();
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
    }
}