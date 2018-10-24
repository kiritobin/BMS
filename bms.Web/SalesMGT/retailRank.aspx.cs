using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using System.Data;
using System.Text;

namespace bms.Web.SalesMGT
{
    public partial class retailRank : System.Web.UI.Page
    {
        protected DataSet ds;
        protected int kindsNum, allCount;
        protected double allPrice;
        RetailBll rtBll = new RetailBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            groupRetail();
            GetData();
        }
        public String GetData()
        {
            DataSet ds;
            SellOffMonomerBll smBll = new SellOffMonomerBll();
            ds = smBll.getRetailRank();
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + (i + 1) + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["allNum"].ToString() + "</td>");
                sb.Append("<td>" + Convert.ToDouble(ds.Tables[0].Rows[i]["allTotalPrice"].ToString()).ToString("F2") + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            return sb.ToString();
        }
        public void groupRetail()
        {
            ds = rtBll.GroupRetail();
            kindsNum = ds.Tables[0].Rows.Count;
            for (int i = 0; i < kindsNum; i++)
            {
                allCount += Convert.ToInt32(ds.Tables[0].Rows[i]["allCount"].ToString());
                allPrice += Convert.ToDouble(ds.Tables[0].Rows[i]["allPrice"].ToString());
            }
        }
    }
}