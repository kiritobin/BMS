using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using bms.Bll;
using bms.Model;
using System.Text;
using bms.Bll;

namespace bms.Web.SalesMGT
{
    using Result = Enums.OpResult;
    public partial class backManagement : System.Web.UI.Page
    {
        protected DataSet ds;
        sellOffHeadBll soBll = new sellOffHeadBll();
        UserBll uBll = new UserBll();
        protected int totalCount;
        protected int intPageCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        public void getData()
        {
            int pagesize = 2;
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SellOffHead";
            tb.OrderBy = "sellOffHeadID";
            tb.StrColumnlist = "sellOffHeadID,saleTaskId,kinds,count,totalPrice,realPrice,userID,makingTime";
            tb.IntPageSize = pagesize;
            tb.IntPageNum = 1;
            tb.StrTable = "";
            ds = uBll.selectByPage(tb, out totalCount, out intPageCount);
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + ds.Tables[0].Rows[i]["sellOffHeadID"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["saleTaskId"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["userName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["kinds"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["count"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["makingTime"].ToString() + "</td>");
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
        }
    }
}