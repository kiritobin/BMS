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
    public partial class tradeManagement : System.Web.UI.Page
    {
        public DataSet ds, customerds;
        public int totalCount, intPageCount, pageSize = 20;
        SaleTaskBll saleBll = new SaleTaskBll();
        CustomerBll custBll = new CustomerBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            string op = Request["op"];
            if (op == "add")
            {
                string Custmer = Request["Custmer"];
                string numberLimit = Request["numberLimit"];
                string priceLimit = Request["priceLimit"];
                string totalPriceLimit = Request["totalPriceLimit"];
                double defaultDiscount = double.Parse(Request["defaultDiscount"]);
                Customer customer = new Customer();
                SaleTask sale = new SaleTask()
                {
                    DefaultDiscount = defaultDiscount,
                    
                };
            }
        }
        /// <summary>
        /// 获取基础数据及查询方法
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string search = Request["search"];
            if (search == "" || search == null)
            {
                search = "deleteState=0";
            }
            else
            {
                search = String.Format(" saleTaskId {0} and deleteState=0", "like '%" + search + "%'");
            }

            TableBuilder tb = new TableBuilder();
            tb.StrTable = "T_SaleTask";
            tb.OrderBy = "saleTaskId";
            tb.StrColumnlist = "saleTaskId,defaultDiscount,priceLimit,numberLimit,totalPriceLimit,startTime,finishTime,userId";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = search;
            //获取展示的客户数据
            ds = saleBll.selectBypage(tb, out totalCount, out intPageCount);
            //获取客户下拉数据
            customerds = custBll.select();
            //生成table
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<td>" + ds.Tables[0].Rows[i]["saleTaskId"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["defaultDiscount"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["priceLimit"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["numberLimit"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["totalPriceLimit"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["startTime"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["finishTime"].ToString() + "</td>");
                strb.Append("<td>" + "<button class='btn btn-success btn-sm' onclick=window.location.href = 'salesManagement.aspx''>&nbsp 售 &nbsp</button>");
                strb.Append("<button class='btn btn-success btn-sm' onclick='window.location.href = 'backManagement.aspx''>&nbsp 退 &nbsp</button>");
                strb.Append("<button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button>" + " </td></tr>");
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
        }
    }
}