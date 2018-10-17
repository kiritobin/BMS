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
using System.Text;

namespace bms.Web.InventoryMGT
{
    public partial class addRs : System.Web.UI.Page
    {
        public string userName, regionName;
        protected DataSet dsPer,ds,bookds;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail;
        replenishMentBll rmBll = new replenishMentBll();
        BookBasicBll bookBll = new BookBasicBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
            string op = Request["op"];
            getData();
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
            if(op== "searchISBN")
            {
                string ISBN = Request["ISBN"];
                string bookNum = Request["bookNO"];
                bookds = bookBll.SelectByIsbn(ISBN);
                if (bookds != null && bookds.Tables[0].Rows.Count > 0)
                {
                    if (bookNum == "" || bookNum == null)
                    {
                        if (bookds.Tables[0].Rows.Count > 1)
                        {
                            showBook();
                        }
                    }
                    else
                    {
                        Response.Write(getData());
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("暂无此数据");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public String getData()
        {
            ds = rmBll.Select();
            DataTable dsdt = ds.Tables[0];
            StringBuilder sb = new StringBuilder();
            sb.Append("<thead>");//表头
            sb.Append("<tr>");
            sb.Append("<th>" + "序号" + "</th>");
            sb.Append("<th>" + "补货日期" + "</th>");
            sb.Append("<th>" + "ISBN号" + "</th>");
            sb.Append("<th>" + "书号" + "</th>");
            sb.Append("<th>" + "书名" + "</th>");
            sb.Append("<th>" + "单价" + "</th>");
            sb.Append("<th>" + "数量" + "</th>");
            sb.Append("<th>" + "码洋" + "</th>");
            sb.Append("<th>" + "实际折扣" + "</th>");
            sb.Append("<th>" + "实洋" + "</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");//表体
            sb.Append(WriteTable());
            for (int i = 0; i < dsdt.Rows.Count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + (i + 1) + "</td>");
                sb.Append("<td>" + dsdt.Rows[i]["dateTime"].ToString() + "</td>");
                sb.Append("<td>" + dsdt.Rows[i]["ISBN"].ToString() + "</td>");
                sb.Append("<td>" + dsdt.Rows[i]["bookNum"].ToString() + "</td>");
                sb.Append("<td>" + dsdt.Rows[i]["bookName"].ToString() + "</td>");
                sb.Append("<td>" + dsdt.Rows[i]["price"].ToString() + "</td>");
                sb.Append("<td>" + dsdt.Rows[i]["count"].ToString() + "</td>");
                sb.Append("<td>" + dsdt.Rows[i]["totalPrice"].ToString() + "</td>");
                sb.Append("<td>" + dsdt.Rows[i]["realDiscount"].ToString() + "</td>");
                sb.Append("<td>" + dsdt.Rows[i]["realPrice"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            //Response.Write(sb.ToString());
            //Response.End();
            return sb.ToString();
        }
        /// <summary>
        /// 一号多书展示
        /// </summary>
        /// <returns></returns>
        public String showBook()
        {
            string ISBN = Request["ISBN"];
            StringBuilder sb = new StringBuilder();
            sb.Append("<thead class='much'>");//thead
            sb.Append("<tr>");
            sb.Append("<th>" + "<div class='pretty inline'><input type = 'radio' name='radio'><label><i class='mdi mdi-check'></i></label></div>" + "</th>");
            sb.Append("<th>" + "ISBN" + "</th>");
            sb.Append("<th>" + "书号" + "</th>");
            sb.Append("<th>" + "书名" + "</th>");
            sb.Append("<th>" + "单价" + "</th>");
            //strbook.Append("<th>" + "出版社" + "</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");//thead
            sb.Append("<tbody>");//tbody
            for (int i = 0; i < bookds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                sb.Append("<td>" + ISBN + "</td>");
                sb.Append("<td>" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                sb.Append("<td>" + bookds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                sb.Append("<td>" + bookds.Tables[0].Rows[i]["price"].ToString() + "</td>");
            }
            sb.Append("</tbody>");//tbody
            Response.Write(sb.ToString());
            Response.End();
            return sb.ToString();
        }
        /// <summary>
        /// 带输入框的表格
        /// </summary>
        /// <returns></returns>
        public String WriteTable()
        {
            string bookNum = "";
            string ISBN = "";
            string bookName = "";
            DateTime time;
            double unitPrice = 0;
            string isbn = Request["ISBN"];
            bookNum = Request["bookNO"];
            if (isbn != "" || isbn != null)
            {
                ISBN = isbn;
            }
            if (bookNum == "" || bookNum == null)
            {
                if (bookds != null)
                {
                    bookNum = bookds.Tables[0].Rows[0]["bookNum"].ToString();//书号
                    bookName = bookds.Tables[0].Rows[0]["bookName"].ToString();
                    BookBasicData book = new BookBasicData();
                    book = bookBll.SelectById(bookNum);
                    unitPrice = book.Price;//定价
                    time = DateTime.Now;
                }
            }
            else
            {
                unitPrice = double.Parse(Request["price"]);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr calss='first'>");
            sb.Append("<td>" + "</td>");
            sb.Append("<td>" + "</td>");
            sb.Append("<td>" + "<input type='text' id='inputISBN' class='textareaISBN' autofocus='autofocus' value='" + ISBN + "' />" + "</td>");
            sb.Append("<td>" + bookNum + "</td>");
            sb.Append("<td>" + bookName + "</td>");
            sb.Append("<td>" + unitPrice + "</td>");
            sb.Append("<td>" + "<input type='text' id='inputCount' class='textareaCount' />" + "</td>");
            sb.Append("<td>" + "</td>");
            sb.Append("<td>" + "</td>");
            sb.Append("<td>" + "</td>");
            sb.Append("</tr>");
            return sb.ToString();
        }
        /// <summary>
        /// 权限控制
        /// </summary>
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
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 14)
                {
                    funcRetail = true;
                }
            }
        }
    }
}