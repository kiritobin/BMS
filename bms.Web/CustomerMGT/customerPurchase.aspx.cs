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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Customer custom = (Customer)Session["custom"];
                userName = custom.CustomerName;
                int cusId = custom.CustomerId;
            }
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
            Customer custom=(Customer)Session["custom"];
            userName = custom.CustomerName;
            int cusId = custom.CustomerId;
            string search = "";
            string bookName = Request["bookName"];
            string source = Request["source"];
            if ((bookName == ""|| bookName==null)&&(source==""||source==null))
            {
                search = "deleteState=0 and customerID=" + cusId;
            }
            else if ((bookName == "" || bookName == null)&&(source != "" || source != null))
            {
                search = "deleteState=0 and regionName='" +source+ "' and customerID=" + cusId;
            }
            else if ((bookName != "" || bookName != null) && (source == "" || source == null))
            {
                search = "deleteState=0 and bookName like '%" + bookName + "%' and customerID=" + cusId;
            }
            else
            {
                search = "deleteState=0 and bookName like '%" + bookName + "%' and regionName='"+ source + "' and customerID=" + cusId;
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