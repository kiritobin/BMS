using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using bms.Model;
using System.Data;
using System.Text;

namespace bms.Web.SalesMGT
{
    public partial class backQuery : System.Web.UI.Page
    {
        public DataSet ds;
        public DataSet searchDs;
        SellOffMonomerBll smBll = new SellOffMonomerBll();
        protected int totalCount;
        protected int intPageCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            string op = Request["op"];
            GetData();
            if(op== "searcisbn")
            {
                SearchIsbn();
            }
        }
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public String GetData()
        {
            Session["sell"] = "00001";
            string sellId = Session["sell"].ToString();
            //int pagesize = 2;
            //TableBuilder tb = new TableBuilder()
            //{
            //    StrTable = "T_SellOffMonomer",
            //    OrderBy = "sellOffHead",
            //    StrColumnlist = "sellOffHead,sellOffMonomerId,bookNum,isbn,price,count,totalPrice,realDiscount,realPrice,dateTime",
            //    IntPageSize = pagesize,
            //    IntPageNum = 1,
            //    StrWhere = ""
            //};
            //ds = smBll.selecByPage(tb, out totalCount, out intPageCount);
            ds = smBll.Select();
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                string sohId = ds.Tables[0].Rows[i]["sellOffHead"].ToString();
                if (sohId == sellId)
                {
                    sb.Append("<tr><td>" + ds.Tables[0].Rows[i]["sellOffMonomerId"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["isbn"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["price"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["count"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["dateTime"].ToString() + "</td>");
                    sb.Append("<td>" + "<button class='btn btn-danger'><i class='fa fa-trash' aria-hidden='true'></i></button>" + "</td>");
                    sb.Append("</tr>");
                }
            }
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            sb.Append("</tbody>");
            return sb.ToString();
        }
        public void SearchIsbn()
        {
            string isNO = Request["isbn"];
            searchDs = smBll.SelectByISBN(isNO);
        }
    }
}