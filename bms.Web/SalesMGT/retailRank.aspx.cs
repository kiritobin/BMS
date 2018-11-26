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
        public DateTime startTime, endTime;
        public string regionName;
        RetailBll rtBll = new RetailBll();
        ConfigurationBll cBll = new ConfigurationBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            regionName = Session["regionName"].ToString();
            DataSet timeDs = cBll.getDateTime(regionName);
            if (timeDs.Tables[0].Rows.Count > 0)
            {
                string st = timeDs.Tables[0].Rows[0]["startTime"].ToString();
                startTime = DateTime.Parse(st);
                string et = timeDs.Tables[0].Rows[0]["endTime"].ToString();
                endTime = DateTime.Parse(et);
            }
            groupRetail();
            GetData();
        }
        public String GetData()
        {
            DataSet ds;
            SellOffMonomerBll smBll = new SellOffMonomerBll();
            ds = smBll.getRetailRank(startTime,endTime,regionName);
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + (i + 1) + "</td>");
                sb.Append("<td title='"+ ds.Tables[0].Rows[i]["bookName"].ToString() + "'>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["num"].ToString() + "</td>");
                sb.Append("<td>" + Convert.ToDouble(ds.Tables[0].Rows[i]["allPrice"].ToString()).ToString("F2") + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value='" + regionName + "' id='rName'/>");
            return sb.ToString();
        }
        public void groupRetail()
        {
            ds = rtBll.GroupRetail(startTime,endTime,regionName);
            kindsNum = int.Parse(ds.Tables[0].Rows[0]["retailKinds"].ToString());
            if (kindsNum > 0)
            {
                allCount = int.Parse(ds.Tables[0].Rows[0]["allNum"].ToString());
                allPrice = Double.Parse(Convert.ToDouble(ds.Tables[0].Rows[0]["allPrice"].ToString()).ToString("F2"));
            }
        }
    }
}