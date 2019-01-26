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
        public DateTime startTime, endTime;
        public string regionName,type;
        protected double allPrice;
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
                type = timeDs.Tables[0].Rows[0]["type"].ToString();
            }
            groupCount();
            GetData();
        }
        public String GetData()
        {
            ds = smBll.SelectBookRanking(startTime,endTime,regionName,type);
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + (i+1) + "</td>");
                sb.Append("<td title='"+ ds.Tables[0].Rows[i]["bookName"].ToString() + "'>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["allNum"].ToString() + "</td>");
                sb.Append("<td>" + Convert.ToDouble(ds.Tables[0].Rows[i]["allPrice"].ToString()).ToString("F2") + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value='" + regionName + "' id='rName'/>");
            return sb.ToString();
        }
        public void groupCount()
        {
            DataSet groupds = smBll.GroupCount(startTime, endTime, regionName,type);
            int count = groupds.Tables[0].Rows.Count;
            if (count > 0)
            {
                kindsNum = int.Parse(groupds.Tables[0].Rows[0]["totalBooks"].ToString());
                if (kindsNum > 0)
                {
                    allCount = int.Parse(groupds.Tables[0].Rows[0]["allCount"].ToString());
                    allPrice = double.Parse(groupds.Tables[0].Rows[0]["allPrice"].ToString());
                }
            }
        }
    }
}