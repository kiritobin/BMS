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

namespace bms.Web.InventoryMGT
{
    public partial class inventoryManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 20, totalCount, intPageCount;
        public string search = "",userName,regionName;
        public DataSet ds,dsPer;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply;
        BookBasicBll bookbll = new BookBasicBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
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
            string bookISBN = Request["btnISBN"];
            string bookName = Request["bookName"];
            string area = Request["bookArea"];
            currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            if ((bookName == "" || bookName == null) && (area == null || area == "") && (bookISBN == null || bookISBN == ""))
            {
                search = "";
            }
            else if ((bookName != "" && bookName != null) && (area == null || area == "") && (bookISBN == null || bookISBN == ""))
            {
                search = String.Format(" bookName like '%{0}%'", bookName);
            }
            else if ((bookName == "" || bookName == null) && (area != "" && area != null) && (bookISBN == null || bookISBN == ""))
            {
                search = "regionName=' " + area + "'";
            }
            else if ((bookName == "" || bookName == null) && (bookISBN != "" && bookISBN != null) && (area == null || area == ""))
            {
                search = "ISBN='" + bookISBN + "'";
            }
            else if ((bookName == "" || bookName == null) && (bookISBN != "" && bookISBN != null) && (area != null && area != ""))
            {
                search = "regionName='" + area + "' and ISBN='" + bookISBN + "'";
            }
            else if ((bookName != "" && bookName != null) && (area != null && area != "") && (bookISBN == null || bookISBN == ""))
            {
                search = String.Format(" bookName like '%{0}%' and regionName = '{1}'", bookName, area);
            }
            else if ((bookName != "" && bookName != null) && (area == null || area == "") && (bookISBN != null && bookISBN != ""))
            {
                search = String.Format(" bookName like '%{0}%' and ISBN='{1}'", bookName, bookISBN);
            }
            else
            {
                search = String.Format(" bookName like '%{0}%' and regionName = '{1}' and ISBN='{2}'", bookName, area, bookISBN);
            }
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_Stock";
            tbd.OrderBy = "ISBN";
            tbd.StrColumnlist = "ISBN,bookName,supplier,price,regionName,stockNum,shelvesName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = bookbll.selectBypage(tbd, out totalCount, out intPageCount);
            int j = ds.Tables[0].Rows.Count;
            if (ds==null)
            {
                j = 0;
            }
            //生成table
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            j = ds.Tables[0].Rows.Count;
            for (int i = 0; i < j; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                sb.Append("<tr><td>" + dr["ISBN"].ToString() + "</td>");
                sb.Append("<td>" + dr["bookName"].ToString() + "</td >");
                sb.Append("<td>" + dr["supplier"].ToString() + "</td >");
                sb.Append("<td>" + dr["price"].ToString() + "</td >");
                sb.Append("<td>" + dr["regionName"].ToString() + "</td >");
                sb.Append("<td>" + dr["shelvesName"].ToString() + "</td >");
                sb.Append("<td>" + dr["stockNum"].ToString() + "</td></tr>");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }
        protected void permission()
        {
            FunctionBll functionBll = new FunctionBll();
            User user = (User)Session["user"];
            userName = user.UserName;
            regionName = user.ReginId.RegionName;
            Role role = new Role();
            role = user.RoleId;
            int roleId = role.RoleId;
            dsPer = functionBll.SelectByRoleId(roleId);
            for (int i = 0; i < dsPer.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 1)
                {
                    funcOrg = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 2)
                {
                    funcRole = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 3)
                {
                    funcUser = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 4)
                {
                    funcGoods = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 5)
                {
                    funcCustom = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 6)
                {
                    funcLibrary = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 7)
                {
                    funcBook = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 8)
                {
                    funcPut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 9)
                {
                    funcOut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 10)
                {
                    funcSale = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 11)
                {
                    funcSaleOff = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 12)
                {
                    funcReturn = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 13)
                {
                    funcSupply = true;
                }
            }
        }
    }
}