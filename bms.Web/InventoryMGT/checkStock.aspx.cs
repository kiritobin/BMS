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

namespace bms.Web.InventoryMGT
{
    public partial class checkStock : System.Web.UI.Page
    {
        public DataTable putDt;
        public DataSet ds;
        public string putId, putOperator, putCount, putRegionName, putTotalPrice, putRealPrice, putTime;
        public int totalCount, intPageCount, pageSize = 20;
        WarehousingBll warehousingBll = new WarehousingBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            string singleHeadId = Request.QueryString["returnId"];
            //string singleHeadId = "20180927000001";
            putDt = warehousingBll.SelectSingleHead(singleHeadId);
            int count = putDt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                putId = putDt.Rows[i]["singleHeadId"].ToString();
                putOperator = putDt.Rows[i]["userName"].ToString();
                putCount = putDt.Rows[i]["allBillCount"].ToString();
                putRegionName = putDt.Rows[i]["regionName"].ToString();
                putTotalPrice = putDt.Rows[i]["allTotalPrice"].ToString();
                putRealPrice = putDt.Rows[i]["allRealPrice"].ToString();
                putTime = Convert.ToDateTime(putDt.Rows[i]["time"]).ToString("yyyy年MM月dd日 HH:mm:ss");
            }
            string op = Request["op"];
            if(op== "export")
            {
                export();
            }
        }

        protected string getData()
        {
            UserBll userBll = new UserBll();
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_Monomer";
            tbd.OrderBy = "singleHeadId";
            tbd.StrColumnlist = "singleHeadId,ISBN,number,uPrice,discount,realPrice,totalPrice,shelvesName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "singleHeadId="+ putId+" and type=1";
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);

            //生成table
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            int count = ds.Tables[0].Rows.Count;
            DataRowCollection drc = ds.Tables[0].Rows;
            for (int i = 0; i < count; i++)
            {
                sb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + drc[i]["singleHeadId"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["ISBN"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["number"].ToString() + "</td>");
                sb.Append("<td>" + drc[i]["uPrice"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["discount"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["totalPrice"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["realPrice"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["shelvesName"].ToString() + "</td ></tr >");
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

        public void export()
        {        
            var name = putId+"明细"+DateTime.Now.ToString("yyyyMMdd") + new Random(DateTime.Now.Second).Next(10000);
                DataTable dt = warehousingBll.ExportExcel(putId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    var path = Server.MapPath("~/download/入库明细导出/" + name + ".xls");
                    ExcelHelper.x2003.TableToExcelForXLS(dt, path);
                    downloadfile(path);
                }
                else
                {
                    Response.Write("没有数据，不能执行导出操作!");
                    Response.End();
                }
        }
    }
}