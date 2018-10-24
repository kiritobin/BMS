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
        public DataSet ds,groupDs;
        SaleTaskBll salebll = new SaleTaskBll();
        SaleMonomerBll smBll = new SaleMonomerBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            groupCount();
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
            ds = smBll.groupCustomer();
            StringBuilder strb = new StringBuilder();
            int kinds = 0;
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string custName = ds.Tables[0].Rows[i]["customerName"].ToString();
                strb.Append("<tr><td>" + (i + 1).ToString() + "</td>");
                strb.Append("<td>" + custName + "</td>");
                kinds = smBll.customerKinds(custName);
                strb.Append("<td>" + kinds + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allCount"].ToString() + "</td>");
                strb.Append("<td>" + double.Parse(ds.Tables[0].Rows[i]["allPrice"].ToString()).ToString("F2") + "</td></tr>");
            }
            strb.Append("</tbody>");
            return strb.ToString();
        }
        public void groupCount()
        {
            groupDs = smBll.GroupCount();
            kindsNum = groupDs.Tables[0].Rows.Count;
            for (int i = 0; i < kindsNum; i++)
            {
                allCount += Convert.ToInt32(groupDs.Tables[0].Rows[i]["allCount"].ToString());
                allPrice += Convert.ToDouble(groupDs.Tables[0].Rows[i]["allPrice"].ToString());
            }
        }
    }
}