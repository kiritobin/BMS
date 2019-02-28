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

namespace bms.Web.ReportStatistics
{
    public partial class returnStatistics : System.Web.UI.Page
    {
        public string userName, regionName;
        public DataSet ds, dsRegion, dsCustom, dsPer;
        public DataTable dsSupplier;
        SaleMonomerBll salemonBll = new SaleMonomerBll();
        WarehousingBll wareBll = new WarehousingBll();
        BookBasicBll bookBll = new BookBasicBll();
        RegionBll regionBll = new RegionBll();
        CustomerBll customBll = new CustomerBll();
        public int totalCount, intPageCount, pageSize = 20;
        string exportAllStrWhere, exportgroupbyType, condition;
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
            if (op == "print")
            {
                print();
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
        /// <summary>
        /// 导出当前页面数据
        /// </summary>
        public void exportAll()
        {
            exportAllStrWhere = Session["exportAllStrWhere"].ToString();
            exportgroupbyType = Session["exportgroupbyType"].ToString();
            DataTable dt = wareBll.exportAll(exportAllStrWhere, exportgroupbyType, Session["time"].ToString(), 2);
            string name = "退货报表导出" + DateTime.Now.ToString("yyyyMMddhhmmss") + new Random(DateTime.Now.Second).Next(10000);
            if (dt != null && dt.Rows.Count > 0)
            {
                var path = Server.MapPath("../download/报表导出/退货报表导出/" + name + ".xlsx");
                ExcelHelper.x2007.TableToExcelForXLSX(dt, path);
                downloadfile(path);
            }
            else
            {
                Response.Write("<script language='javascript'>alert('查询不到数据，不能执行导出操作!')</script>");
            }
        }
        /// <summary>
        /// 导出所有明细
        /// </summary>
        public void exportDetail()
        {
            exportgroupbyType = Session["exportgroupbyType"].ToString();
            exportAllStrWhere = Session["exportAllStrWhere"].ToString();
            DataTable dt = wareBll.exportDe(exportgroupbyType, exportAllStrWhere, 2);
            string name = "退货报表明细导出" + DateTime.Now.ToString("yyyyMMddhhmmss") + new Random(DateTime.Now.Second).Next(10000);
            if (dt != null && dt.Rows.Count > 0)
            {
                var path = Server.MapPath("../download/报表导出/退货报表导出/" + name + ".xlsx");
                ExcelHelper.x2007.TableToExcelForXLSX(dt, path);
                downloadfile(path);
            }
            else
            {
                Response.Write("<script language='javascript'>alert('查询不到数据，不能执行导出操作!')</script>");
            }

        }
        /// <summary>
        /// 下载导出文件
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
            string time = Request["time"];
            Session["time"] = time;
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

            if (time != null && time != "")
            {
                string[] sArray = time.Split('至');
                string startTime = sArray[0];
                string endTime = sArray[1];
                if (strWhere != null && strWhere != "")
                {
                    strWhere += " and time BETWEEN'" + startTime + "' and '" + endTime + "'";
                }
                else
                {
                    strWhere = "time BETWEEN'" + startTime + "' and '" + endTime + "'";
                }
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
            tb.StrTable = "v_monomer";
            tb.OrderBy = "convert(" + groupbyType + " using gbk) collate gbk_chinese_ci";
            tb.StrColumnlist = groupbyType + ", sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice,count(bookNum) as kindsNum";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            if (strWhere == "" || strWhere == null)
            {
                tb.StrWhere = groupbyType + " like'%'" + " and type=2 GROUP BY " + groupbyType;
            }
            else
            {
                tb.StrWhere = strWhere + " and type=2 GROUP BY " + groupbyType;
            }
            Session["exportgroupbyType"] = groupbyType;
            //tb.StrWhere = search == "" ? "deleteState=0 and saleTaskId=" + "'" + saleId + "'" : search + " and deleteState = 0 and saleTaskId=" + "'" + saleId + "'";
            //获取展示的客户数据
            ds = salemonBll.selectBypage(tb, out totalCount, out intPageCount);
            //获取查询条件
            Session["exportAllStrWhere"] = tb.StrWhere;
            StringBuilder strb = new StringBuilder();
            int dscount = ds.Tables[0].Rows.Count;
            string kinds;
            for (int i = 0; i < dscount; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                //序号 (i + 1 + ((currentPage - 1) * pageSize)) 
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + dr["" + groupbyType + ""].ToString() + "</td>");
                condition = dr["" + groupbyType + ""].ToString();
                strb.Append("<td>" + dr["kindsNum"].ToString() + "</td>");
                strb.Append("<td>" + dr["allNumber"].ToString() + "</td>");
                strb.Append("<td>" + dr["allTotalPrice"].ToString() + "</td>");
                strb.Append("<td>" + dr["allRealPrice"].ToString() + "</td>");
                strb.Append("<td><button class='btn btn-info btn-sm look'><i class='fa fa-search'></i></button></td></tr>");
            }
            strb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            Response.Write(strb.ToString());
            Response.End();
            return strb.ToString();
        }

        private string print()
        {
            string groupbyType = Request["groupbyType"];
            StringBuilder strb = new StringBuilder();
            exportAllStrWhere = Session["exportAllStrWhere"].ToString();
            exportgroupbyType = Session["exportgroupbyType"].ToString();
            DataTable dt = wareBll.exportAll(exportAllStrWhere, exportgroupbyType, Session["time"].ToString(), 2);
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow dr = dt.Rows[i];
                strb.Append("<tr><td>" + (i + 1) + "</td>");
                strb.Append("<td>" + dr["" + groupbyType + ""].ToString() + "</td>");
                //condition = dr["" + groupbyType + ""].ToString();
                strb.Append("<td>" + dr["书籍种数"].ToString() + "</td>");
                strb.Append("<td>" + dr["书籍总数量"].ToString() + "</td>");
                strb.Append("<td>" + dr["总实洋"].ToString() + "</td>");
                strb.Append("<td>" + dr["总码洋"].ToString() + "</td></tr>");
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
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 14)
                {
                    funcBookStock = true;
                }
            }
        }
    }
}