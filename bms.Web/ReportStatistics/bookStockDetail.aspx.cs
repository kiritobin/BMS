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

namespace bms.Web.ReportStatistics
{
    public partial class bookStockDetail : System.Web.UI.Page
    {
        DataSet ds, dsPer;
        SaleMonomerBll salemonBll = new SaleMonomerBll();
        WarehousingBll wareBll = new WarehousingBll();
        public int totalCount, intPageCount, pageSize = 20;
        public DataSet dsUser = null;
        public string type = "", name = "", groupType = "", userName, regionName;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail, isAdmin;
        protected void Page_Load(object sender, EventArgs e)
        {
            string op = Request["op"];
            if (op == "print")
            {
                print();
            }
            if (!IsPostBack)
            {
                type = Request.QueryString["type"];
                name = Request.QueryString["name"];
                if (type == null || type == "" || name == "" || name == null)
                {
                    type = Session["type"].ToString();
                    name = Session["name"].ToString();
                }
                else
                {
                    Session["type"] = type;
                    Session["name"] = name;
                }
            }
            getData();
            permission();
            string exportOp = Request.QueryString["op"];
            if (exportOp == "export")
            {
                export();
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            string strWhere = "";
            if (type == "regionName")
            {
                strWhere = "regionName = '" + name + "'";
            }
            else if (type == "supplier")
            {
                strWhere = "supplier = '" + name + "'";
            }
            groupType = strWhere;
            dsUser = wareBll.getUser(groupType, 1);
            string isbn = Request["isbn"];
            string price = Request["price"];
            string discount = Request["discount"];
            string bookName = Request["bookName"];
            string stockNumber = Request["stockNumber"];
            if (isbn != null && isbn != "")
            {
                strWhere += " and isbn='" + isbn + "'";
            }
            if (price != null && price != "")
            {
                strWhere += " and price='" + price + "'";
            }
            if (bookName != null && bookName != "")
            {
                strWhere += " and bookName like '%" + bookName + "%'";
            }
            if (stockNumber != "" && stockNumber != null)
            {
                string[] sArray = stockNumber.Split('于');
                string type = sArray[0];
                string number = sArray[1];
                if (strWhere == "" || strWhere == null)
                {
                    if (type == "小")
                    {
                        strWhere = " and stockNum < '" + number + "'";
                    }
                    else if (type == "等")
                    {
                        strWhere = " and stockNum = '" + number + "'";
                    }
                    else
                    {
                        strWhere = " and stockNum > '" + number + "'";
                    }
                }
                else
                {
                    if (type == "小")
                    {
                        strWhere += " and stockNum < '" + number + "'";
                    }
                    else if (type == "等")
                    {
                        strWhere += " and stockNum = '" + number + "'";
                    }
                    else
                    {
                        strWhere += " and stockNum > '" + number + "'";
                    }
                }
            }
            if (discount != null && discount != "")
            {
                strWhere += " and (author like'%" + discount + "%' or remarks like'%" + discount + "%')";
            }
            
            strWhere += " group by bookNum," + type;
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "v_stock";
            tb.OrderBy = type;
            tb.StrColumnlist = "ISBN,bookNum,bookName,price,sum(stockNum) as stockNum, author,remarks,supplier, regionName";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = strWhere;
            //获取展示的客户数据
            ds = salemonBll.selectBypage(tb, out totalCount, out intPageCount);
            StringBuilder strb = new StringBuilder();
            int dscount = ds.Tables[0].Rows.Count;
            for (int i = 0; i < dscount; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                //序号 (i + 1 + ((currentPage - 1) * pageSize)) 
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                strb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
                strb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                strb.Append("<td>" + dr["price"].ToString() + "</td>");
                strb.Append("<td>" + dr["supplier"].ToString() + "</td>");
                strb.Append("<td>" + dr["stockNum"].ToString() + "</td>");
                strb.Append("<td>" + dr["author"].ToString() + "</td>");
                strb.Append("<td>" + dr["remarks"].ToString() + "</td>");
                strb.Append("<td>" + dr["regionName"].ToString() + "</td></tr>");
            }
            strb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
        }

        /// <summary>
        /// //导出列表方法
        /// </summary>
        /// <param name="s_path">文件路径</param>
        public void downloadfile(string s_path)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(s_path);
            HttpContext.Current.Response.ContentType = "application/ms-download";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            HttpContext.Current.Response.WriteFile(file.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 导出
        /// </summary>
        public void export()
        {
            string isbn = Request.QueryString["isbn"];
            string price = Request.QueryString["price"];
            string discount = Request.QueryString["discount"];
            string bookName = Request["bookName"];
            string stockNumber = Request["stockNumber"];
            string strWhere = groupType;
            string fileName = name;
            if (isbn != null && isbn != "")
            {
                fileName += "-" + isbn;
                strWhere += " and isbn='" + isbn + "'";
            }
            if (price != null && price != "")
            {
                fileName += "-" + price;
                strWhere += " and uPrice=" + price;
            }
            if (discount != null && discount != "")
            {
                fileName += "-" + discount;
                strWhere += " and discount=" + discount;
            }
            if (bookName != null && bookName != "")
            {
                strWhere += " and bookName like '%" + bookName + "%'";
            }
            if (stockNumber != "" && stockNumber != null)
            {
                string[] sArray = stockNumber.Split('于');
                string type = sArray[0];
                string number = sArray[1];
                if (strWhere == "" || strWhere == null)
                {
                    if (type == "小")
                    {
                        strWhere = " and stockNum < '" + number + "'";
                    }
                    else if (type == "等")
                    {
                        strWhere = " and stockNum = '" + number + "'";
                    }
                    else
                    {
                        strWhere = " and stockNum > '" + number + "'";
                    }
                }
                else
                {
                    if (type == "小")
                    {
                        strWhere += " and stockNum < '" + number + "'";
                    }
                    else if (type == "等")
                    {
                        strWhere += " and stockNum = '" + number + "'";
                    }
                    else
                    {
                        strWhere += " and stockNum > '" + number + "'";
                    }
                }
            }
            string Name = fileName + "-书籍库存明细-" + DateTime.Now.ToString("yyyyMMdd") + new Random(DateTime.Now.Second).Next(10000);
            DataTable dt = wareBll.ExportExcelDetail(strWhere, type, 1);
            if (dt != null && dt.Rows.Count > 0)
            {
                var path = Server.MapPath("~/download/报表导出/入库报表导出/" + Name + ".xlsx");
                ExcelHelper.x2007.TableToExcelForXLSX(dt, path);
                downloadfile(path);
            }
            else
            {
                Response.Write("<script>alert('没有数据，不能执行导出操作!');</script>");
                Response.End();
            }
        }
        private string print()
        {
            string type = Request["type"];
            string strWhere = "";
            string name = Session["name"].ToString();
            if (type == "regionName")
            {
                strWhere = "regionName = '" + name + "' and deleteState=0";
            }
            else if (type == "supplier")
            {
                strWhere = "supplier = '" + name + "' and deleteState=0";
            }
            string isbn = Request["isbn"];
            string price = Request["price"];
            string discount = Request["discount"];
            if (isbn != null && isbn != "")
            {
                strWhere += " and isbn='" + isbn + "'";
            }
            if (price != null && price != "")
            {
                strWhere += " and uPrice=" + price;
            }
            if (discount != null && discount != "")
            {

                strWhere += " and discount=" + discount;
            }
            DataTable dt = wareBll.ExportExcelDetail(strWhere, type, 1);
            int count = dt.Rows.Count;
            StringBuilder strb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                DataRow dr = dt.Rows[i];
                strb.Append("<tr><td>" + (i + 1) + "</td>");
                strb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                strb.Append("<td>" + dr["书号"].ToString() + "</td>");
                strb.Append("<td>" + dr["书名"].ToString() + "</td>");
                strb.Append("<td>" + dr["单价"].ToString() + "</td>");
                strb.Append("<td>" + dr["数量"].ToString() + "</td>");
                strb.Append("<td>" + dr["码洋"].ToString() + "</td>");
                strb.Append("<td>" + dr["实洋"].ToString() + "</td>");
                strb.Append("<td>" + dr["折扣"].ToString() + "</td>");
                strb.Append("<td>" + dr["供应商"].ToString() + "</td>");
                strb.Append("<td>" + dr["组织名称"].ToString() + "</td></tr>");
            }
            Response.Write(strb.ToString());
            Response.End();
            return strb.ToString();
        }
        /// <summary>
        /// 权限控制
        /// </summary>
        protected void permission()
        {
            RoleBll roleBll = new RoleBll();
            FunctionBll functionBll = new FunctionBll();
            User user = (User)Session["user"];
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
            }
        }
    }
}