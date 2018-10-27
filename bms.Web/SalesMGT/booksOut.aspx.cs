using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Model;
using bms.Bll;
using System.Data;
using System.Text;

namespace bms.Web.SalesMGT
{
    public partial class booksOut : System.Web.UI.Page
    {
        SaleMonomerBll smBll = new SaleMonomerBll();
        DataSet ds,groupDs;
        protected int kindsNum=0,allCount=0;
        protected double allPrice;
        protected void Page_Load(object sender, EventArgs e)
        {
            groupCount();
            GetData();
        }
        public String GetData()
        {
            ds = smBll.SelectBookRanking();
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + (i+1) + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["allNum"].ToString() + "</td>");
                sb.Append("<td>" + Convert.ToDouble(ds.Tables[0].Rows[i]["allPrice"].ToString()).ToString("F2") + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            return sb.ToString();
        }
        public void groupCount()
        {
            DataSet groupds = smBll.GroupCount();
            int count = groupds.Tables[0].Rows.Count;
            if (count > 0)
            {
                kindsNum = int.Parse(groupds.Tables[0].Rows[0]["totalBooks"].ToString());
                allCount = int.Parse(groupds.Tables[0].Rows[0]["allCount"].ToString());
                allPrice = double.Parse(groupds.Tables[0].Rows[0]["allPrice"].ToString());
            }
        }
    }
}