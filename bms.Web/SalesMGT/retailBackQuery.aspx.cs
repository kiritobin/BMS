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

namespace bms.Web.SalesMGT
{
    public partial class retailBackQuery : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 15;
        public DataSet ds;
        RetailBll retailbll = new RetailBll();
        string retailHeadId;
        protected void Page_Load(object sender, EventArgs e)
        {
            retailHeadId = Session["retailHeadId"].ToString();
            getData();
        }
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string search;
            search = "deleteState=0 and retailHeadId=" + "'" + retailHeadId + "'";

            TableBuilder tb = new TableBuilder();
            tb.StrTable = "T_RetailMonomer";
            tb.OrderBy = "retailMonomerId";
            tb.StrColumnlist = "bookNum,ISBN,unitPrice,number,totalPrice,realDiscount,dateTime,realPrice";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = search;
            //获取展示的客户数据
            ds = retailbll.selectBypage(tb, out totalCount, out intPageCount);
            //生成table
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td><nobr>" + ds.Tables[0].Rows[i]["bookNum"].ToString() + "</nobr></td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["number"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["dateTime"].ToString() + "</td></tr>");
                // strb.Append("<td style='width:150px;'><button class='btn btn-success btn-sm btn_search'>&nbsp 查看 &nbsp</button></td></tr>");
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
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