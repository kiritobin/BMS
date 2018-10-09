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
    public partial class seniority : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 10;
        public DataSet ds;
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();

        }
        public string getData()
        {
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SaleStatistics";
            tb.OrderBy = "allPrice desc";
            tb.StrColumnlist = "allNumber,allPrice,allKinds,customerName";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = 1;
            //tb.StrWhere = search;
            tb.StrWhere = "";
            //获取展示的客户数据
            ds = salemonbll.selectBypage(tb, out totalCount, out intPageCount);
            //生成table
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1).ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allKinds"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allNumber"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allPrice"].ToString() + ".00￥" + "</td></tr>");
            }
            strb.Append("</tbody>");
            return strb.ToString();
        }
    }
}