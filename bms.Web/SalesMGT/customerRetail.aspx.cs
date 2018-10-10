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
    public partial class customerRetail : System.Web.UI.Page
    {
        protected DataSet ds;
        protected int pageSize = 20, totalCount, intPageCount;
        public double discount;
        SaleHead single = new SaleHead();
        UserBll userBll = new UserBll();
        SaleMonomerBll retailBll = new SaleMonomerBll();
        StockBll stockBll = new StockBll();
        BookBasicBll basicBll = new BookBasicBll();
        GoodsShelvesBll goods = new GoodsShelvesBll();
        DataTable monTable = new DataTable();
        SaleHeadBll saleBll = new SaleHeadBll();
        public List<long> bookNumList = new List<long>();
        protected void Page_Load(object sender, EventArgs e)
        {
            getIsbn();
            string op = Request["op"];
            if (op == "add")
            {
                long bookNum = Convert.ToInt64(Request["bookNum"]);
                add(bookNum);
            }
            if (op == "insert")
            {
                insert();
            }
            if (op == "delete")
            {
                long bookNum = Convert.ToInt64(Request["bookNum"]);
                bookNumList = (List<long>)Session["List"];
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
            if (kind == "0")
            {
                Session["List"] = new List<long>();
            }
            double disCount = Convert.ToDouble(Request["disCount"]);
            int billCount = Convert.ToInt32(Request["billCount"]);
            if (isbn != null && isbn != "")
            {
                BookBasicBll bookBasicBll = new BookBasicBll();
                DataSet bookDs = bookBasicBll.SelectByIsbn(isbn);
                int count = bookDs.Tables[0].Rows.Count;
                if (bookDs != null && bookDs.Tables[0].Rows.Count > 0)
                {
                    if (count == 1)
                    {
                        long bookNum = Convert.ToInt64(bookDs.Tables[0].Rows[0]["bookNum"]);
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
            return null;
        }

        public void add(long bookNum)
        {
            bookNumList = (List<long>)Session["List"];
            foreach (long bookNums in bookNumList)
            {
                if (bookNums == bookNum)
                {
                    Response.Write("已添加过此图书");
                    Response.End();
                }
            }
            BookBasicData bookBasicData = basicBll.SelectById(Convert.ToInt64(bookNum));
            string isbn = bookBasicData.Isbn;
            string bookName = bookBasicData.BookName;
            int billCount = Convert.ToInt32(Request["billCount"]);
            double discount = 1;
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
                sb.Append("<td><input class='number' type='hidden' value='" + dr["number"].ToString() + "'/>");
                sb.Append("<input class='number' type='number' style='width:50px;border:none;' name='points',min='1' value='" + dr["number"].ToString() + "'/></td>");
                sb.Append("<td>" + dr["realDiscount"].ToString() + "</td>");
                sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                sb.Append("<td><input type='hidden' value='" + dr["bookNum"].ToString() + "'/>");
                sb.Append("<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash'></i></button></td></tr>");
            }
            Response.Write(sb.ToString());
            Response.End();

        }

        public void insert()
        {
            string json = Request["json"];
            DataTable dataTable = jsonToDt(json);
            int row, rows = 0;
            double total, allTotal = 0, real, allReal = 0;
            int Counts = dataTable.Rows.Count;
            for (int i = 0; i < Counts; i++)
            {
                DataRow dr = monTable.Rows[i];
                row = Convert.ToInt32(dr["number"]);
                total = Convert.ToDouble(dr["totalPrice"]);
                real = Convert.ToDouble(dr["realPrice"]);
                rows = rows + row;
                allTotal = allTotal + total;
                allReal = allReal + real;
            }
            User user = (User)Session["user"];
            //int count = saleBll.countRetail() + 1;
            //string retailHeadId = "LS" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
            single.AllRealPrice = allReal;
            single.AllTotalPrice = allTotal;
            single.DateTime = DateTime.Now;
            single.KindsNum = Counts;
            single.Number = rows;
            single.RegionId = user.ReginId.RegionId;
            //single.SaleHeadId = retailHeadId;
            single.UserId = user.UserId;
            single.SaleTaskId = "0";
            //Result result = saleBll.InsertRetail(single);
            //if (result == Result.添加成功)
            //{
                Session["List"] = new List<long>();
                SaleMonomer monomers = new SaleMonomer();
                int Count = dataTable.Rows.Count;
                for (int i = 0; i < Count; i++)
                {
                    DataRow dr = monTable.Rows[i];
                    monomers.ISBN1 = dr["ISBN"].ToString();
                    monomers.UnitPrice = Convert.ToDouble(dr["unitPrice"]);
                    monomers.BookNum = Convert.ToInt64(dr["bookNum"]);
                    monomers.RealDiscount = Convert.ToDouble(dr["realDiscount"]);
                    monomers.SaleIdMonomerId = i + 1;
                    monomers.Number = Convert.ToInt32(dr["number"]);
                    monomers.TotalPrice = Convert.ToDouble(dr["totalPrice"]);
                    monomers.RealPrice = Convert.ToDouble(dr["realPrice"]);
                    //monomers.SaleHeadId = retailHeadId;
                    Result mon = retailBll.Insert(monomers);
                    if (mon == Result.添加失败)
                    {
                        Response.Write("添加失败");
                        Response.End();
                    }
                }
                //Response.Write("添加成功");
                //Response.End();
            //}
            //else
            //{
            //    Response.Write("添加失败");
            //    Response.End();
            //}

        }

        //public string getTotal()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    string op = Request["op"];
        //    if (op == "add")
        //    {
        //        int counts = monTable.Rows.Count;
        //        for (int i = 0; i < counts; i++)
        //        {
        //            DataRow dr = monTable.Rows[i];
        //            sb.Append("<li>时间：" + DateTime.Now + "</li>");
        //            sb.Append("<li>种类：" + DateTime.Now + "</li>");
        //            sb.Append("<li>数量：" + DateTime.Now + "</li>");
        //            sb.Append("<li>总码洋：" + DateTime.Now + "</li>");
        //            sb.Append("<li>总实洋：" + DateTime.Now + "</li>");
        //        }
        //        Response.Write(sb.ToString());
        //        Response.End();
        //    }
        //    return sb.ToString();
        //}
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