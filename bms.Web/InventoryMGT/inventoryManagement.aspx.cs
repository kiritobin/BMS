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
        public string search = "", userName, regionName;
        public DataSet ds, dsPer;
        public User user;
        RoleBll roleBll = new RoleBll();
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail, isAdmin, funcBookStock;
        BookBasicBll bookbll = new BookBasicBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (User)Session["user"];
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
            string bookISBN = Request["bookISBN"];
            string bookName = Request["bookName"];
            string stockNumber = Request["stockNumber"];
            string supplier = Request["supplier"];
            if (user.RoleId.RoleName == "超级管理员")
            {
                string area = Request["bookArea"];
                currentPage = Convert.ToInt32(Request["page"]);
                if (currentPage == 0)
                {
                    currentPage = 1;
                }
                if (supplier != "" && supplier != null)
                {
                    if (search == "" || search == null)
                    {
                        search = "supplier like '%" + supplier + "%'";
                    }
                    else
                    {
                        search += " and supplier like '%" + supplier + "%'";
                    }
                }
                if (bookName != "" && bookName != null)
                {
                    if (search == "" || search == null)
                    {
                        search = "bookName like '%" + bookName + "%'";
                    }
                    else
                    {
                        search += " and bookName like '%" + bookName + "%'";
                    }
                }
                if (bookISBN != "" && bookISBN != null)
                {
                    if (search == "" || search == null)
                    {
                        search = "ISBN like '%" + bookISBN + "%'";
                    }
                    else
                    {
                        search += " and ISBN like '%" + bookISBN + "%'";
                    }
                }
                if (area != "" && area != null)
                {
                    if (search == "" || search == null)
                    {
                        search = "regionName like '%" + area + "%'";
                    }
                    else
                    {
                        search += " and regionName like '%" + area + "%'";
                    }
                }
                if (stockNumber != "" && stockNumber != null)
                {
                    string[] sArray = stockNumber.Split('于');
                    string type = sArray[0];
                    string number = sArray[1];
                    if (search == "" || search == null)
                    {
                        if (type == "小")
                        {
                            search = "stockNum < '" + number + "'";
                        }
                        else if (type == "等")
                        {
                            search = "stockNum = '" + number + "'";
                        }
                        else
                        {
                            search = "stockNum > '" + number + "'";
                        }
                    }
                    else
                    {
                        if (type == "小")
                        {
                            search += " and stockNum < '" + number + "'";
                        }
                        else if (type == "等")
                        {
                            search += " and stockNum = '" + number + "'";
                        }
                        else
                        {
                            search += " and stockNum > '" + number + "'";
                        }
                    }
                }
            }
            else
            {
                string region = "regionId=" + user.ReginId.RegionId;
                if (search == "" || search == null)
                {
                    search += region;
                }
                currentPage = Convert.ToInt32(Request["page"]);
                if (currentPage == 0)
                {
                    currentPage = 1;
                }
                string area = Request["bookArea"];
                currentPage = Convert.ToInt32(Request["page"]);
                if (currentPage == 0)
                {
                    currentPage = 1;
                }
                if (supplier != "" && supplier != null)
                {
                    search += " and supplier like '%" + supplier + "%'";
                }
                if (bookName != "" && bookName != null)
                {
                    search += " and bookName like '%" + bookName + "%'";
                }
                if (bookISBN != "" && bookISBN != null)
                {
                    search += " and ISBN = '" + bookISBN + "'";
                }
                if (stockNumber != "" && stockNumber != null)
                {
                    string[] sArray = stockNumber.Split('于');
                    string type = sArray[0];
                    string number = sArray[1];
                    if (type == "小")
                    {
                        search += " and stockNum < '" + number + "'";
                    }
                    else if (type == "等")
                    {
                        search += " and stockNum = '" + number + "'";
                    }
                    else
                    {
                        search += " and stockNum > '" + number + "'";
                    }
                }
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
            if (ds == null)
            {
                j = 0;
            }
            //生成table
            StringBuilder sb = new StringBuilder();
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
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
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
            userName = user.UserName;
            regionName = user.ReginId.RegionName;
            Role role = new Role();
            role = user.RoleId;
            int roleId = role.RoleId;
            dsPer = functionBll.SelectByRoleId(roleId);
            string userId = user.UserId;
            DataSet dsRole = roleBll.selectRole(userId);
            string roleName = dsRole.Tables[0].Rows[0]["roleName"].ToString();
            if (roleName == "超级管理员")
            {
                isAdmin = true;
            }
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
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 14)
                {
                    funcRetail = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 15)
                {
                    funcBookStock = true;
                }
            }
        }
    }
}