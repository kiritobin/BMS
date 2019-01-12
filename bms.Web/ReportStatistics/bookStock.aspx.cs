using bms.Bll;
using bms.DBHelper;
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

namespace bms.Web.ReportStatistics
{
    public partial class bookStock : System.Web.UI.Page
    {
        StockBll StockBll = new StockBll();
        SaleMonomerBll salemonBll = new SaleMonomerBll();
        BookBasicBll bookBll = new BookBasicBll();
        RegionBll regionBll = new RegionBll();
        CustomerBll customBll = new CustomerBll();
        public string userName, regionName;
        public DataSet ds, dsRegion, dsCustom, dsPer;
        public DataTable dsSupplier;
        public int totalCount, intPageCount, pageSize = 20;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail, isAdmin, funcBookStock;
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["user"];
            userName = user.UserName;
            regionName = user.ReginId.RegionName;
            string op = Request["op"];
            if (op == "paging")
            {
                getData();
            }
            if (op == "exportAll")
            {
                exportAll();
            }
            if (op == "exportDe")
            {
                exportDetail();
            }
            else
            {
                permission();
                //获取供应商
                dsSupplier = bookBll.selectSupplier();
                //获取组织
                dsRegion = regionBll.select();
                //获取客户
                dsCustom = customBll.select();
            }
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

        private void exportAll()
        {
            User user = (User)Session["user"];
            int regionId = user.ReginId.RegionId;
            string roleName = user.RoleId.RoleName;
            string strWhere = "";
            string groupbyType = Request["groupbyType"];
            string supplier = Request["supplier"];
            string regionName = Request["regionName"];
            if (groupbyType == "state" || groupbyType == null)
            {
                groupbyType = "supplier";
            }
            if (supplier != "" && supplier != null)
            {
                strWhere = "supplier='" + supplier + "'";
            }
            if (regionName != "" && regionName != null)
            {
                strWhere = "regionName='" + regionName + "'";
            }

            if (roleName != "超级管理员")
            {
                if (strWhere == "" || strWhere == null)
                {
                    strWhere = "regionId=" + regionId;
                }
                else
                {
                    strWhere += "and regionId=" + regionId;
                }
            }
            string str="";
            if (strWhere == "" || strWhere == null)
            {
                str = groupbyType + " like'%'";
            }
            else
            {
                str = strWhere;
            }
            string file = "书籍库存导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DataTable dt = StockBll.bookStock(str, groupbyType).Tables[0];
            int count = dt.Rows.Count;
            if (count>0)
            {
                ExcelHelp.dtToExcelForXlsxByNpoi(dt, file);
            }
            else
            {
                Response.Write("<script language='javascript'>alert('查询不到数据，不能执行导出操作!')</script>");
            }
        }

        private void exportDetail()
        {
            User user = (User)Session["user"];
            int regionId = user.ReginId.RegionId;
            string roleName = user.RoleId.RoleName;
            string strWhere = "";
            string groupbyType = Request["groupbyType"];
            string supplier = Request["supplier"];
            string regionName = Request["regionName"];
            if (groupbyType == "state" || groupbyType == null)
            {
                groupbyType = "supplier";
            }
            if (supplier != "" && supplier != null)
            {
                strWhere = "supplier='" + supplier + "'";
            }
            if (regionName != "" && regionName != null)
            {
                strWhere = "regionName='" + regionName + "'";
            }

            if (roleName != "超级管理员")
            {
                if (strWhere == "" || strWhere == null)
                {
                    strWhere = "regionId=" + regionId;
                }
                else
                {
                    strWhere += "and regionId=" + regionId;
                }
            }
            string str = "";
            if (strWhere == "" || strWhere == null)
            {
                str = groupbyType + " like'%'";
            }
            else
            {
                str = strWhere;
            }
            string file = "书籍库存明细导出" + DateTime.Now.ToString("yyyyMMddHHmmss");
            DataTable dt = StockBll.bookStockDe(str,groupbyType);
            int count = dt.Rows.Count;
            if (count > 0)
            {
                ExcelHelp.dtToExcelForXlsxByNpoi(dt, file);
            }
            else
            {
                Response.Write("<script language='javascript'>alert('查询不到数据，不能执行导出操作!')</script>");
            }
        }

        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            User user = (User)Session["user"];
            int regionId = user.ReginId.RegionId;
            string roleName = user.RoleId.RoleName;
            string strWhere = "";
            string groupbyType = Request["groupbyType"];
            string supplier = Request["supplier"];
            string regionName = Request["regionName"];
            if (groupbyType == "state" || groupbyType == null)
            {
                groupbyType = "supplier";
            }
            if (supplier != "" && supplier != null)
            {
                strWhere = "supplier='" + supplier + "'";
            }
            if (regionName != "" && regionName != null)
            {
                strWhere = "regionName='" + regionName + "'";
            }

            if (roleName != "超级管理员")
            {
                if (strWhere == "" || strWhere == null)
                {
                    strWhere = "regionId=" + regionId;
                }
                else
                {
                    strWhere += "and regionId=" + regionId;
                }
            }
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }

            TableBuilder tb = new TableBuilder();
            tb.StrTable = "v_stock";
            tb.OrderBy = "stockNum desc";
            tb.StrColumnlist = "count(bookNum) as kindsNum,sum(stockNum) as stockNum,supplier, regionName";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            if (strWhere == "" || strWhere == null)
            {
                tb.StrWhere = groupbyType + " like'%'" + " GROUP BY " + groupbyType;
            }
            else
            {
                tb.StrWhere = strWhere + "  GROUP BY " + groupbyType;
            }
            Session["exportgroupbyType"] = groupbyType;
            //tb.StrWhere = search == "" ? "deleteState=0 and saleTaskId=" + "'" + saleId + "'" : search + " and deleteState = 0 and saleTaskId=" + "'" + saleId + "'";
            //获取展示的客户数据
            ds = salemonBll.selectBypage(tb, out totalCount, out intPageCount);
            //获取查询条件
            Session["exportAllStrWhere"] = tb.StrWhere;
            StringBuilder strb = new StringBuilder();
            int dscount = ds.Tables[0].Rows.Count;
            for (int i = 0; i < dscount; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                if (groupbyType == "supplier")
                {
                    strb.Append("<td>" + dr["supplier"].ToString() + "</td>");
                    strb.Append("<td>" + dr["kindsNum"].ToString() + "</td>");
                    strb.Append("<td>" + dr["stockNum"].ToString() + "</td>");
                    strb.Append("<td>" + dr["regionName"].ToString() + "</td>");
                }
                else
                {
                    strb.Append("<td>" + dr["regionName"].ToString() + "</td>");
                    strb.Append("<td>" + dr["kindsNum"].ToString() + "</td>");
                    strb.Append("<td>" + dr["stockNum"].ToString() + "</td>");
                    strb.Append("<td>" + dr["supplier"].ToString() + "</td>");
                }
                strb.Append("<td><button class='btn btn-info btn-sm look'><i class='fa fa-search'></i></button></td></tr>");
            }
            strb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
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
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 15)
                {
                    funcBookStock = true;
                }
            }
        }
    }
}