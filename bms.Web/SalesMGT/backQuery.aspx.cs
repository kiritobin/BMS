using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using bms.Model;
using System.Data;
using System.Text;

namespace bms.Web.SalesMGT
{
    using NPOI.SS.Util;
    using Result = Enums.OpResult;
    public partial class backQuery : System.Web.UI.Page
    {
        public DataSet ds;
        public DataSet bookds;
        SellOffMonomerBll smBll = new SellOffMonomerBll();
        protected int totalCount;
        protected int intPageCount;
        public StringBuilder strbook = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            string op = Request["op"];
            GetData();
            if(op == "search")
            {
                string ISBN = Request["ISBN"];
                BookBasicBll bookbll = new BookBasicBll();
                bookds = bookbll.SelectByIsbn(ISBN);
                if (bookds != null)
                {
                    //如果有两条及两条以上显示表格
                    if (bookds.Tables[0].Rows.Count > 1)
                    {
                        getbook();
                    }
                    else
                    {
                       addSalemon();
                    }
                }
                else
                {
                    Response.Write("无数据");
                    Response.End();
                }
            }
        }

        public void addSalemon()
        {
            string isbn = Request["ISBN"];//ISBN
            BookBasicBll bookbll = new BookBasicBll();
            bookds = bookbll.SelectByIsbn(isbn);
            string bookNo = bookds.Tables[0].Rows[0]["bookNum"].ToString();//书号
            double unitPrice = double.Parse(bookds.Tables[0].Rows[0]["price"].ToString());//定价
            double discount = double.Parse(Request["discount"]);//实际折扣
            int count = int.Parse(Request["count"]);//数量
            double totalPrice = unitPrice * count;//码洋
            double d = totalPrice * discount;
            double realPrice = Math.Round(d, 2);//实洋
            string headId = Session["sell"].ToString();//单头Id
            int moNum = smBll.GetCount(headId);
            int smId;
            smId = moNum + 1;//单体Id
            DateTime time = DateTime.Now;
            SellOffMonomer sm = new SellOffMonomer()
            {
                SellOffMonomerId = smId.ToString(),
                SellOffHeadId = headId,
                BookNum = long.Parse(bookNo),
                ISBN1 = isbn,
                Count = count,
                TotalPrice = totalPrice,
                RealPrice = realPrice,
                Price = unitPrice,
                Time = time,
                Discount = discount
            };
            Result row = smBll.Insert(sm);
            if(row == Result.添加成功)
            {
                Response.Write("添加成功");
                Response.End();
            }
            else
            {
                Response.Write("添加失败");
                Response.End();
            }
        }

        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public String GetData()
        {
            Session["sell"] = "00001";
            string sellId = Session["sell"].ToString();
            //int pagesize = 2;
            //TableBuilder tb = new TableBuilder()
            //{
            //    StrTable = "T_SellOffMonomer",
            //    OrderBy = "sellOffHead",
            //    StrColumnlist = "sellOffHead,sellOffMonomerId,bookNum,isbn,price,count,totalPrice,realDiscount,realPrice,dateTime",
            //    IntPageSize = pagesize,
            //    IntPageNum = 1,
            //    StrWhere = ""
            //};
            //ds = smBll.selecByPage(tb, out totalCount, out intPageCount);
            ds = smBll.Select();
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                string sohId = ds.Tables[0].Rows[i]["sellOffHead"].ToString();
                if (sohId == sellId)
                {
                    sb.Append("<tr><td>" + ds.Tables[0].Rows[i]["sellOffMonomerId"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["isbn"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["price"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["count"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["dateTime"].ToString() + "</td>");
                    sb.Append("<td>" + "<button class='btn btn-danger'><i class='fa fa-trash' aria-hidden='true'></i></button>" + "</td>");
                    sb.Append("</tr>");
                }
            }
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            sb.Append("</tbody>");
            return sb.ToString();
        }
        public String getbook()
        {
            strbook.Append("<thead>");
            strbook.Append("<tr>");
            strbook.Append("<th>" + "<div class='pretty inline'><input type = 'radio' name='radio'><label><i class='mdi mdi-check'></i></label></div>" + "</th>");
            strbook.Append("<th>" + "书号" + "</th>");
            strbook.Append("<th>" + "书名" + "</th>");
            strbook.Append("<th>" + "单价" + "</th>");
            strbook.Append("<th>" + "出版社" + "</th>");
            strbook.Append("</tr>");
            strbook.Append("</thead>");
            strbook.Append("<tbody>");
            for (int i = 0; i < bookds.Tables[0].Rows.Count; i++)
            {
                strbook.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["price"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["supplier"].ToString() + "</td></tr>");
            }
            strbook.Append("</tbody>");
            Response.Write(strbook.ToString());
            Response.End();
            return strbook.ToString();
        }
    }
}