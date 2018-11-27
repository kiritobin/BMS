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

namespace bms.Web.ReportStatistics
{
    public partial class salesDetails : System.Web.UI.Page
    {
        DataSet ds;
        SaleMonomerBll salemonBll = new SaleMonomerBll();
        public int totalCount, intPageCount, pageSize = 20;
        string type = "", name = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                type = Request.QueryString["type"];
                name = Request.QueryString["name"];
                if(type == null || type== "" || name == "" || name == null)
                {
                    type = Session["type"].ToString();
                    name = Session["name"].ToString();
                }
                else
                {
                    Session["type"] = type;
                    Session["name"] = name;
                }
            }
            getData();
        }
        public string getData()
        {
            string isbn = Request["isbn"];
            string price = Request["price"];
            string discount = Request["discount"];
            string user = Request["user"];
            string time = Request["time"];
            string state = Request["state"];
            string strWhere="";
            if(type == "supplier")
            {
                strWhere = "supplier = '"+ name + "' and deleteState=0";
            }
            else if(type == "regionName")
            {
                strWhere = "regionName = '" + name + "' and deleteState=0";
            }
            else if (type == "customerName")
            {
                strWhere = "customerName = '" + name + "' and deleteState=0";
            }
            if (isbn != null && isbn != "")
            {
                strWhere += " and isbn='" + isbn + "'";
            }
            if (price != null && price != "")
            {
                strWhere += " and price='" + price + "'";
            }
            if (discount != null && discount != "")
            {
                strWhere += " and realDiscount='" + discount + "'";
            }
            if (user != null && user != "")
            {
                strWhere += " and userName='" + user + "'";
            }
            if (time != null && time != "")
            {
                string[] sArray = time.Split('至');
                string startTime = sArray[0];
                string endTime = sArray[1];
                strWhere += " and dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
            }
            if (state != null && state != "")
            {
                strWhere += " and state='" + state + "'";
            }
            strWhere += " group by bookNum";
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "v_salemonomer";
            tb.OrderBy = "id";
            tb.StrColumnlist = "id,isbn,bookNum,bookName,price,sum(number) as number, sum(totalPrice) as totalPrice,sum(realPrice) as realPrice,realDiscount,dateTime,userName,state,supplier";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = strWhere;
            //获取展示的客户数据
            ds = salemonBll.selectBypage(tb, out totalCount, out intPageCount);
            StringBuilder strb = new StringBuilder();
            int dscount = ds.Tables[0].Rows.Count;
            string states = "", stateName = "";
            for (int i = 0; i < dscount; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                //序号 (i + 1 + ((currentPage - 1) * pageSize)) 
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + dr["isbn"].ToString() + "</td>");
                strb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
                strb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                strb.Append("<td>" + dr["price"].ToString() + "</td>");
                strb.Append("<td>" + dr["number"].ToString() + "</td>");
                strb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                strb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                strb.Append("<td>" + dr["realDiscount"].ToString() + "</td>");
                strb.Append("<td>" + dr["dateTime"].ToString() + "</td>");
                strb.Append("<td>" + dr["userName"].ToString() + "</td>");
                states = dr["state"].ToString();
                if(states == "0")
                {
                    stateName = "新建单据";
                }
                else if(states == "3")
                {
                    stateName = "预采";
                }
                else
                {
                    stateName = "完成";
                }
                strb.Append("<td>" + stateName + "</td>");
                strb.Append("<td>" + dr["supplier"].ToString() + "</td></tr>");
            }
            strb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
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