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
    public partial class salesDetails : System.Web.UI.Page
    {
        DataSet ds;
        SaleMonomerBll salemonBll = new SaleMonomerBll();
        public int totalCount, intPageCount, pageSize = 20;
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
        }
        public string getData()
        {
            //string saleId = Session["saleId"].ToString();
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
            tb.OrderBy = "allTotalPrice desc";
            tb.StrColumnlist = "customerName, sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = "customerName like '云南%' GROUP BY customerName";
            //tb.StrWhere = search == "" ? "deleteState=0 and saleTaskId=" + "'" + saleId + "'" : search + " and deleteState = 0 and saleTaskId=" + "'" + saleId + "'";
            //获取展示的客户数据
            ds = salemonBll.selectBypage(tb, out totalCount, out intPageCount);
            StringBuilder strb = new StringBuilder();
            int dscount = ds.Tables[0].Rows.Count;
            for (int i = 0; i < dscount; i++)
            {
                //序号 (i + 1 + ((currentPage - 1) * pageSize)) 
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allNumber"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allNumber"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allTotalPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allRealPrice"].ToString() + "</td>");
                strb.Append("<td><button class='btn btn-info btn-sm look'><i class='fa fa-search'></i></button></td></tr>");
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
    }
}