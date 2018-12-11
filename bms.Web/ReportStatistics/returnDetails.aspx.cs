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
    public partial class returnDetails : System.Web.UI.Page
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
            string op = Request["op"];
            if (op == "print") {
                Print();
            }
        }
        public String Print()
        {
            string isbn = Request.QueryString["isbn"];
            string price = Request.QueryString["price"];
            string discount = Request.QueryString["discount"];
            string user = Request.QueryString["user"];
            string time = Request.QueryString["time"];
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
            if (user != null && user != "")
            {
                fileName += "-" + user;
                strWhere += " and userName='" + user + "'";
            }
            if (time != null && time != "")
            {
                fileName += "-" + time;
                strWhere += " and time='" + time + "'";
            }
            DataTable dt = wareBll.ExportExcelDetail(strWhere, type, 2);
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
                strb.Append("<td>" + dr["制单时间"].ToString() + "</td>");
                strb.Append("<td>" + dr["制单员"].ToString() + "</td>");
                strb.Append("<td>" + dr["入库来源"].ToString() + "</td></tr>");
            }
            Response.Write(strb.ToString());
            Response.End();
            return strb.ToString();
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
                strWhere = "regionName = '" + name + "' and deleteState=0";
            }
            else if (type == "supplier")
            {
                strWhere = "supplier = '" + name + "' and deleteState=0";
            }
            groupType = strWhere;
            dsUser = wareBll.getUser(groupType, 2);
            string isbn = Request["isbn"];
            string price = Request["price"];
            string discount = Request["discount"];
            string user = Request["user"];
            string time = Request["time"];
            if (isbn != null && isbn != "")
            {
                strWhere += " and isbn='" + isbn + "'";
            }
            if (price != null && price != "")
            {
                strWhere += " and uPrice='" + price + "'";
            }
            if (discount != null && discount != "")
            {
                strWhere += " and discount='" + discount + "'";
            }
            if (user != null && user != "" && user != "0")
            {
                strWhere += " and userName='" + user + "'";
            }
            if (time != null && time != "")
            {
                string[] sArray = time.Split('至');
                string startTime = sArray[0];
                string endTime = sArray[1];
                strWhere += " and time BETWEEN'" + startTime + "' and '" + endTime + "'";
            }
            strWhere += " and type=2 group by bookNum,userName"+type;
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "v_monomer";
            tb.OrderBy = type;
            tb.StrColumnlist = "isbn,bookNum,bookName,uPrice,sum(number) as number, sum(totalPrice) as totalPrice,sum(realPrice) as realPrice,discount,time,userName,regionName,supplier";
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
                strb.Append("<td>" + dr["isbn"].ToString() + "</td>");
                strb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
                strb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                strb.Append("<td>" + dr["uPrice"].ToString() + "</td>");
                strb.Append("<td>" + dr["supplier"].ToString() + "</td>");
                strb.Append("<td>" + dr["number"].ToString() + "</td>");
                strb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                strb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                strb.Append("<td>" + dr["discount"].ToString() + "</td>");
                strb.Append("<td>" + dr["time"].ToString() + "</td>");
                strb.Append("<td>" + dr["userName"].ToString() + "</td>");
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
            string user = Request.QueryString["user"];
            string time = Request.QueryString["time"];
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
            if (user != null && user != "")
            {
                fileName += "-" + user;
                strWhere += " and userName='" + user + "'";
            }
            if (time != null && time != "")
            {
                fileName += "-" + time;
                strWhere += " and time='" + time + "'";
            }
            string Name = fileName + "-退货明细-" + DateTime.Now.ToString("yyyyMMdd") + new Random(DateTime.Now.Second).Next(10000);
            DataTable dt = wareBll.ExportExcelDetail(strWhere, type, 2);
            if (dt != null && dt.Rows.Count > 0)
            {
                var path = Server.MapPath("~/download/报表导出/退货报表导出/" + Name + ".xlsx");
                ExcelHelper.x2007.TableToExcelForXLSX(dt, path);
                downloadfile(path);
            }
            else
            {
                Response.Write("<script>alert('没有数据，不能执行导出操作!');</script>");
                Response.End();
            }
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