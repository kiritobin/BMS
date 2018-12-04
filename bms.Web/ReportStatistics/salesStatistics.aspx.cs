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

namespace bms.Web.reportStatistics
{
    public partial class salesStatistics : System.Web.UI.Page
    {
        public DataSet ds, dsRegion, dsCustom;
        public DataTable dsSupplier;
        SaleMonomerBll salemonBll = new SaleMonomerBll();
        BookBasicBll bookBll = new BookBasicBll();
        RegionBll regionBll = new RegionBll();
        CustomerBll customBll = new CustomerBll();
        public int totalCount, intPageCount, pageSize = 20;
        string exportAllStrWhere, exportgroupbyType, condition, state, Time;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                //获取供应商
                dsSupplier = bookBll.selectSupplier();
                //获取组织
                dsRegion = regionBll.select();
                //获取客户
                dsCustom = customBll.select();
            }
        }
        public void exportAll()
        {
            exportAllStrWhere = Session["exportAllStrWhere"].ToString();
            exportgroupbyType = Session["exportgroupbyType"].ToString();
            DataTable dt = salemonBll.exportAll(exportAllStrWhere, exportgroupbyType, state, Time);
            //var name = DateTime.Now.ToString("yyyyMMddhhmmss") + new Random(DateTime.Now.Second).Next(10000);
            string name = "销售报表导出" + DateTime.Now.ToString("yyyyMMddhhmmss") + new Random(DateTime.Now.Second).Next(10000);
            if (dt != null && dt.Rows.Count > 0)
            {
                var path = Server.MapPath("../download/报表导出/销售报表导出/" + name + ".xlsx");
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
            DataTable dt = salemonBll.exportDel(exportgroupbyType, exportAllStrWhere);
            string name = "销售报表明细导出" + DateTime.Now.ToString("yyyyMMddhhmmss") + new Random(DateTime.Now.Second).Next(10000);
            if (dt != null && dt.Rows.Count > 0)
            {
                var path = Server.MapPath("../download/报表导出/销售报表导出/" + name + ".xlsx");
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
            string customerName = Request["customerName"];

            string saleHeadState = Request["saleHeadState"];
            string time = Request["time"];

            Time = time;

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
            if (customerName != "" && customerName != null)
            {
                strWhere = "customerName='" + customerName + "'";
            }

            if (saleHeadState != null && saleHeadState != "")
            {
                if (strWhere != null && strWhere != "")
                {
                    if (saleHeadState == "0")
                    {
                        strWhere += "";
                    }
                    else if (saleHeadState == "3")
                    {
                        strWhere += " and state='3'";
                    }
                    else
                    {
                        strWhere += " and (state='1' or state='2')";
                    }

                }
                else
                {
                    if (saleHeadState == "0")
                    {

                        strWhere = "";
                    }
                    else if (saleHeadState == "3")
                    {
                        strWhere = " state='3'";
                    }
                    else
                    {
                        strWhere = " (state='1' or state='2')";
                    }

                }
            }
            if (time != null && time != "")
            {
                string[] sArray = time.Split('至');
                string startTime = sArray[0];
                string endTime = sArray[1];
                if (strWhere != null && strWhere != "")
                {
                    strWhere += " and dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
                }
                else
                {
                    strWhere = "dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
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
            //string saleHeadId = Request["saleTaskId"];
            //string regionName = Request["regionName"];
            //string userName = Request["userName"];
            //string search = "";

            TableBuilder tb = new TableBuilder();
            tb.StrTable = "v_salemonomer";
            tb.OrderBy = "id";
            tb.StrColumnlist = groupbyType + ", sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            if (strWhere == "" || strWhere == null)
            {
                tb.StrWhere = groupbyType + " like'%'" + " GROUP BY " + groupbyType;
            }
            else
            {
                tb.StrWhere = strWhere + " GROUP BY " + groupbyType;
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
            if (saleHeadState != null && saleHeadState != "")
            {
                state = saleHeadState;
            }
            for (int i = 0; i < dscount; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                //序号 (i + 1 + ((currentPage - 1) * pageSize)) 
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + dr["" + groupbyType + ""].ToString() + "</td>");
                condition = dr["" + groupbyType + ""].ToString();
                kinds = salemonBll.getkindsGroupBy(condition, groupbyType, state, time).ToString();
                strb.Append("<td>" + kinds + "</td>");
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
    }
}