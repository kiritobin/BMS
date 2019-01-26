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
    using Result = Enums.OpResult;
    public partial class seniority : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 10, kindsNum, allCount;
        public double allPrice;
        public DataSet ds, groupDs;
        public DateTime startTime, endTime;
        public string regionName,type;
        SaleTaskBll salebll = new SaleTaskBll();
        SaleMonomerBll smBll = new SaleMonomerBll();
        ConfigurationBll cBll = new ConfigurationBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
            }
            groupCount();
            string op = Request["op"];
            getData();
        }

        //public void add()
        //{
        //    DataSet customerds = salebll.getcustomerID();
        //    if (customerds != null)
        //    {
        //        for (int i = 0; i < customerds.Tables[0].Rows.Count; i++)
        //        {
        //            string customerId = customerds.Tables[0].Rows[i]["customerId"].ToString();
        //            Result resultExis = salebll.SaleStatisticsIsExistence(customerId);
        //            if (resultExis == Result.记录不存在)
        //            {
        //                int allNumber = 0;
        //                double allPrice = 0;
        //                int allKinds = 0;
        //                DataSet monds = salebll.SelectMonomers(customerId);
        //                for (int j = 0; j < monds.Tables[0].Rows.Count; j++)
        //                {
        //                    allNumber += int.Parse(monds.Tables[0].Rows[j]["number"].ToString());
        //                    allPrice += double.Parse(monds.Tables[0].Rows[j]["totalPrice"].ToString());
        //                    allKinds = salebll.getkinds(customerId);
        //                }
        //                salebll.insertSaleStatistics(customerId, allNumber, allPrice, allKinds);
        //            }
        //        }
        //    }
        //}

        public string getData()
        {
            ds = smBll.groupCustomer(startTime,endTime,regionName,type);
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string custName = ds.Tables[0].Rows[i]["customerName"].ToString();
                strb.Append("<tr><td>" + (i + 1).ToString() + "</td>");
                strb.Append("<td>" + custName + "</td>");
                strb.Append("<td>" + smBll.customerKinds(startTime, endTime, regionName,custName) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allCount"].ToString() + "</td>");
                strb.Append("<td>" + double.Parse(ds.Tables[0].Rows[i]["allPrice"].ToString()).ToString("F2") + "</td></tr>");
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value='" + regionName + "' id='rName'/>");
            //Response.Write(strb.ToString());
            //Response.End();
            return strb.ToString();
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