using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.CustomerMGT
{
    public partial class customerPurchase : System.Web.UI.Page
    {
        public string userName;
        public int currentPage = 1, pageSize = 20, totalCount, intPageCount;
        BookBasicBll bookbll = new BookBasicBll();
        public int kindsNum = 0, allNum = 0;
        public string allTotalPrice = "", allRealPrice = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            string op = Request["op"];
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
        }

        protected string getData()
        {
            Customer custom = (Customer)Session["custom"];
            userName = custom.CustomerName;
            int cusId = custom.CustomerId;
            //string search = "";
            string search = "(state=1 or state=2) and deleteState=0 and customerID=" + cusId;
            string bookName = Request["bookName"];
            string source = Request["source"];
            string isbn = Request["isbn"];
            string time = Request["time"];
            if (bookName != "" && bookName != null)
            {
                search = search + " and bookName like '%" + bookName + "%'";
            }
            if (source != "" && source != null)
            {
                search = search + " and regionName like '%" + source + "%'";
            }
            if (isbn != "" && isbn != null)
            {
                search = search + " and ISBN like '%" + isbn + "%'";
            }
            if (time != null && time != "")
            {
                string[] sArray = time.Split('至');
                string startTime = sArray[0];
                string endTime = sArray[1];
                if (search != null && search != "")
                {
                    search += " and dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
                }
                else
                {
                    search += "dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
                }
            }
            //else
            //{
            //    search = "deleteState=0 and bookName like '%" + bookName + "%' and regionName='"+ source + "' and customerID=" + cusId;
            //}

            //获取汇总数据
            customerPurchaseBll cpBll = new customerPurchaseBll();
            DataSet sumDs = cpBll.getSummary(cusId, search);
            kindsNum = int.Parse(sumDs.Tables[0].Rows[0]["kindsNum"].ToString());
            if (kindsNum > 0)
            {
                allNum = int.Parse(sumDs.Tables[0].Rows[0]["alln"].ToString());
                allRealPrice = sumDs.Tables[0].Rows[0]["arp"].ToString();
                allTotalPrice = sumDs.Tables[0].Rows[0]["atp"].ToString();
            }

            currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            DataSet ds;
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_SaleMonomer";
            tbd.OrderBy = "saleIdMonomerId";
            tbd.StrColumnlist = "bookName,ISBN,saleIdMonomerId,unitPrice,number,totalPrice,realPrice,realDiscount,dateTime,regionName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = bookbll.selectBypage(tbd, out totalCount, out intPageCount);

            //生成table
            StringBuilder sb = new StringBuilder();
            int j = ds.Tables[0].Rows.Count;
            for (int i = 0; i < j; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                sb.Append("<tr><td>" + dr["saleIdMonomerId"].ToString() + "</td >");
                sb.Append("<td>" + dr["ISBN"].ToString() + "</td >");
                sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                sb.Append("<td>" + dr["unitPrice"].ToString() + "</td >");
                sb.Append("<td>" + dr["number"].ToString() + "</td >");
                sb.Append("<td>" + dr["totalPrice"].ToString() + "</td >");
                sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["realDiscount"].ToString() + "</td>");
                sb.Append("<td>" + dr["regionName"].ToString() + "</td>");
                sb.Append("<td>" + dr["dateTime"].ToString() + "</td></tr>");
            }
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }
    }
}