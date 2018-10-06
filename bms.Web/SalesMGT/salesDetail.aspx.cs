using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.SalesMGT
{
    using Result = Enums.OpResult;
    public partial class salesDetail : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20;
        public string type, defaultdiscount;
        public DataSet ds, bookds;
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        public StringBuilder strbook = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();

            string saleId = Session["saleId"].ToString();
            SaleTaskBll saletaskbll = new SaleTaskBll();
            SaleTask task = saletaskbll.selectById(saleId);
            defaultdiscount = ((task.DefaultDiscount) * 100).ToString();
            string op = Request["op"];
            if (op == "back")
            {
                string SaleHeadId = Session["saleheadId"].ToString();
                Result result = salemonbll.SelectBySaleHeadId(SaleHeadId);
                if (result == Result.删除成功)
                {
                    Response.Write("删除成功");
                    Response.End();
                }
                else if (result == Result.删除失败)
                {
                    Response.Write("删除失败");
                    Response.End();
                }
                else
                {
                    Response.Write("已有数据");
                    Response.End();
                }
            }
            if (op == "search")
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
            if (op == "add")
            {

            }
        }
        public void addSalemon()
        {
            string bookISBN = Request["ISBN"];
            int disCount = int.Parse(Request["disCount"])/100;
            int number = Convert.ToInt32(Request["number"]);
            long bookNum = long.Parse(bookds.Tables[0].Rows[0]["bookNum"].ToString());
            string saleHeadId = Session["saleheadId"].ToString();
            int price = Convert.ToInt32(bookds.Tables[0].Rows[0]["price"].ToString());
            int totalPrice = price * number;
            int realPrice = totalPrice * disCount;
            DateTime Time = DateTime.Now.ToLocalTime();
            SaleMonomer newSalemon = new SaleMonomer()
            {
                BookNum = bookNum,
                ISBN1 = bookISBN,
                SaleHeadId = saleHeadId,
                Number = number,
                UnitPrice = price,
                TotalPrice = totalPrice,
                RealPrice = realPrice,
                RealDiscount = disCount,
                Datetime = Time
            };
            salemonbll.Insert(newSalemon);
        }

        public string getbook()
        {
            strbook.Append("<thead>");
            strbook.Append("<tr>");
            strbook.Append("<th>" + "<div class='pretty inline'><input type = 'radio' name='radio'><label><i class='mdi mdi-check'></i></label></div>" + "</th>");
            strbook.Append("<th>" + "书号" + "</th>");
            strbook.Append("<th>" + "ISBN" + "</th>");
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
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["price"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["supplier"].ToString() + "</td></tr>");
            }
            strbook.Append("</tbody>");
            Response.Write(strbook.ToString());
            Response.End();
            return strbook.ToString();
        }
        public string getData()
        {
            string saleheadId = Session["saleheadId"].ToString();
            type = Session["saleType"].ToString();
            //string saleId = Session["saleId"].ToString();
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string bookName = Request["bookName"];
            string ISBN = Request["ISBN"];
            string search;
            if ((ISBN == "" || ISBN == null) && (bookName == "" || bookName == null))
            {
                search = "";
            }
            else if ((bookName != null || bookName != "") && (ISBN == "" || ISBN == null))
            {

                search = String.Format("bookName='{0}'", bookName);
            }
            else if ((bookName == null || bookName == "") && (ISBN != "" || ISBN != null))
            {
                search = String.Format("ISBN='{0}'", ISBN);
            }
            else
            {
                search = String.Format("ISBN='{0}' and bookName='{1}'", ISBN, bookName);
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SaleMonomer";
            tb.OrderBy = "ISBN";
            tb.StrColumnlist = "bookName,ISBN,unitPrice,number,realDiscount,realPrice,dateTime";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            //tb.StrWhere = search;
            tb.StrWhere = search == "" ? "deleteState=0 and saleHeadId=" + "'" + saleheadId + "'" : search + " and deleteState=0 and saleHeadId=" + "'" + saleheadId + "'";
            //获取展示的客户数据
            ds = salemonbll.selectBypage(tb, out totalCount, out intPageCount);
            //获取客户下拉数据
            //customerds = custBll.select();
            //生成table
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["number"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["dateTime"].ToString() + "</td>");
                strb.Append("<td>" + "<button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button>" + "</td></tr>");
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
        }
    }
}