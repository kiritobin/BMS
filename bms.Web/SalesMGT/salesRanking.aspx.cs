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
        public int totalCount, intPageCount, pageSize = 10;
        public DataSet ds;
        SaleTaskBll salebll = new SaleTaskBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            add();
            getData();
        }

        public void add()
        {
            DataSet customerds = salebll.getcustomerID();
            if (customerds != null)
            {
                for (int i = 0; i < customerds.Tables[0].Rows.Count; i++)
                {
                    string customerId = customerds.Tables[0].Rows[i]["customerId"].ToString();
                    Result resultExis = salebll.SaleStatisticsIsExistence(customerId);
                    if (resultExis == Result.记录不存在)
                    {
                        int allNumber = 0;
                        double allPrice = 0;
                        int allKinds = 0;
                        DataSet monds = salebll.SelectMonomers(customerId);
                        for (int j = 0; j < monds.Tables[0].Rows.Count; j++)
                        {
                            allNumber += int.Parse(monds.Tables[0].Rows[j]["number"].ToString());
                            allPrice += int.Parse(monds.Tables[0].Rows[j]["totalPrice"].ToString());
                            allKinds = salebll.getkinds(customerId);
                        }
                        salebll.insertSaleStatistics(customerId, allNumber, allPrice, allKinds);
                    }
                }
            }
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
            ds = salebll.selectBypage(tb, out totalCount, out intPageCount);
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