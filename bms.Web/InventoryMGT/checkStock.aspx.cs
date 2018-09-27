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
            putDt = warehousingBll.SelectSingleHead(1, singleHeadId);
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