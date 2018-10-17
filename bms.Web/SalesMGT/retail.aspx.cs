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
        public double discount;
        SaleHead single = new SaleHead();
        UserBll userBll = new UserBll();
        StockBll stockBll = new StockBll();
        BookBasicBll basicBll = new BookBasicBll();
        GoodsShelvesBll goods = new GoodsShelvesBll();
        DataTable monTable = new DataTable();
        RetailBll retailBll = new RetailBll();
        public List<string> bookNumList = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            getIsbn();
            string op = Request["op"];
            if (op == "add")
            {
                string bookNum = Request["bookNum"].ToString();
                add(bookNum);
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
        }

        public string getIsbn()
        {
            string op = Request["op"];
            string isbn = Request["isbn"];
            string kind = Request["kind"];
            if(kind == "0")
            {
                Session["List"] = new List<long>();
            }
            double disCount = Convert.ToDouble(Request["disCount"]);
            int billCount = Convert.ToInt32(Request["billCount"]);
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
                            add(bookNum);
                        }
                        if (op == "choose")
                        {
                            int counts = bookDs.Tables[0].Rows.Count;
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < counts; i++)
                            {
                                DataRow dr = bookDs.Tables[0].Rows[i];
                                //sb.Append("<tr><td><input type='checkbox' name='checkbox' class='check' value='" + dr["bookNum"].ToString() + "' /></td>");
                                //sb.Append("<tr><td><input type='radio' name='radio' class='radio' value='" + dr["bookNum"].ToString() + "' /></td>");
                                sb.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + dr["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                                sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                                sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                                sb.Append("<td>" + dr["price"].ToString() + "</td>");
                                sb.Append("<td>" + dr["supplier"].ToString() + "</td></tr>");
                            }
                            Response.Write(sb.ToString());
                            Response.End();
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

        public void add(string bookNum)
        {
            bookNumList = (List<string>)Session["List"];
            foreach (string bookNums in bookNumList)
            {
                if(bookNums == bookNum)
                {
                    Response.Write("已添加过此图书");
                    Response.End();
                }
            }
            BookBasicData bookBasicData = basicBll.SelectById(bookNum);
            string isbn = bookBasicData.Isbn;
            string bookName = bookBasicData.BookName;
            int billCount = Convert.ToInt32(Request["billCount"]);
            double discount=1;
            if (bookBasicData.Remarks == "")
            {
                discount = 1;
            }
            if (discount > 1 && discount <= 10)
            {
                discount = discount * 0.1;
            }
            else if (discount > 10)
            {
                discount = discount * 0.01;
            }
            int row = monTable.Rows.Count;
            double uPrice = bookBasicData.Price;
            SaleMonomer monomers = new SaleMonomer();
            double totalPrice = Convert.ToDouble((billCount * uPrice).ToString("0.00"));
            double realPrice = Convert.ToDouble((totalPrice * discount).ToString("0.00"));
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
            monRow["realDiscount"] = discount * 100;
            monRow["number"] = 1;
            monRow["totalPrice"] = uPrice;
            monRow["realPrice"] = uPrice * discount;
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
                sb.Append("<td><div class='gw_num'><em class='jian'>-</em><input type = 'text' min='1' value='" + dr["number"].ToString() + "' class='num'/><em class='add'>+</em></div></td>");
                //sb.Append("<td><input class='number' type='number' style='width:50px;border:none;' name='points',min='1' value='" + dr["number"].ToString() + "'/></td>");
                sb.Append("<td>" + dr["realDiscount"].ToString() + "</td>");
                sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                sb.Append("<td style='display:none'>" + dr["bookNum"].ToString() + "</td>");
                sb.Append("<td><button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash'></i></button></td></tr>");
            }
            Response.Write(sb.ToString());
            Response.End();
            
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
            User user = (User)Session["user"];
            DateTime nowTime = DateTime.Now;
            string nowDt = nowTime.ToString("yyyy-MM-dd");
            long count = 0;
            //判断数据库中是否已经有记录
            DataSet backds = retailBll.getAllTime(0);
            if (backds != null && backds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < backds.Tables[0].Rows.Count; i++)
                {
                    string time = backds.Tables[0].Rows[i]["dateTime"].ToString();
                    DateTime dt = Convert.ToDateTime(time);
                    string sqlTime = dt.ToString("yyyy-MM-dd");
                    if (sqlTime == nowDt)
                    {
                        //count += 1;
                        string id = backds.Tables[0].Rows[i]["retailHeadId"].ToString();
                        int st1 = Convert.ToInt32(id.Substring(10));
                        if (st1 <= 0)
                        {
                            st1 = 0;
                        }
                        int rowes = st1 + 1;
                        count = rowes;
                        break;
                    }
                }
                if(count == 0)
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
                    monomers.Datetime = DateTime.Now;
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