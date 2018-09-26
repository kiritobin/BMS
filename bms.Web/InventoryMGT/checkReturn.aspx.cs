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
    public partial class checkReturn : System.Web.UI.Page
    {
        public DataTable shDt;
        public DataSet ds;
        public string shId,shOperator,shCount,shRegionName, shTotalPrice, shRealPrice,shTime;
        public int totalCount, intPageCount, pageSize = 20;
        WarehousingBll warehousingBll = new WarehousingBll();
        protected void Page_Load(object sender, EventArgs e)
        {
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
            string singleHeadId = Request.QueryString["returnId"];
            //string singleHeadId = "20180927000002";
            shDt = warehousingBll.SelectSingleHead(2, singleHeadId);
            int count = shDt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                shId = shDt.Rows[i]["singleHeadId"].ToString();
                shOperator = shDt.Rows[i]["userName"].ToString();
                shCount = shDt.Rows[i]["allBillCount"].ToString();
                shRegionName = shDt.Rows[i]["regionName"].ToString();
                shTotalPrice = shDt.Rows[i]["allTotalPrice"].ToString();
                shRealPrice = shDt.Rows[i]["allRealPrice"].ToString();
                shTime = Convert.ToDateTime(shDt.Rows[i]["time"]).ToString("yyyy年MM月dd日 HH:mm:ss");
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
            tbd.StrTable = "V_Monomers";
            tbd.OrderBy = "singleHeadId";
            tbd.StrColumnlist = "singleHeadId,ISBN,number,uPrice,discount,totalPrice,realPrice,shelvesName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "";
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
    }
}