using System;
using System.Data;
using bms.Bll;
using bms.Model;
using System.Text;
using System.Web.Security;

namespace bms.Web.SalesMGT
{
    public partial class backManagement : System.Web.UI.Page
    {
        protected DataSet ds;
        sellOffHeadBll soBll = new sellOffHeadBll();
        UserBll uBll = new UserBll();
        protected int totalCount;
        protected int intPageCount;
        public DataSet cutds;
        protected void Page_Load(object sender, EventArgs e)
        {
            string op = Request["op"];
            getData();
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
            CustomerBll cutBll = new CustomerBll();
            cutds = cutBll.select();
        }
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public String getData()
        {
            int pagesize = 20;
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string search = "";
            //string stockId = Request["stockId"];
            string sellId = Request["sellId"];
            string cutomerName = Request["customer"];
            if ((sellId == "" || sellId == null) && (cutomerName == "" || cutomerName == null))
            {
                search = "saleTaskId='XSRW20181005000005' and deleteState=0";
            }
            else if (sellId != "" && sellId != null && (cutomerName == "" || cutomerName == null))
            {
                search = "saleTaskId='XSRW20181005000005' and deleteState=0 and sellOffHeadID=" + "'" + sellId + "'";
            }
            else if ((sellId == "" || sellId == null) && cutomerName != "" && cutomerName != null)
            {
                search = "saleTaskId='XSRW20181005000005' and deleteState=0 and customerName=" + "'" + cutomerName + "'";
            }
            else
            {
                search = "saleTaskId='XSRW20181005000005' and deleteState=0 and customerName=" + "'" + cutomerName + "'" + " and sellOffHeadID=" + "'" + sellId + "'";
            }
            //if ((stockId == "" || stockId == null) && (sellId == "" || sellId == null) && (cutomerName == "" || cutomerName == null))
            //{
            //    search = search + "and deleteState=0";
            //}
            //else if (stockId != "" && stockId != null && (sellId == "" || sellId == null) && (cutomerName == "" || cutomerName == null))
            //{
            //    search = "deleteState=0 and saleTaskId='" + stockId + "'";
            //}
            //else if (sellId != "" && sellId != null && (stockId == "" || stockId == null) && (cutomerName == "" || cutomerName == null))
            //{
            //    search = "deleteState=0 and sellOffHeadID='" + sellId + "'";
            //}
            //else if (cutomerName != "" && cutomerName != null && (sellId == "" || sellId == null) && (stockId == "" || stockId == null))
            //{
            //    search = "deleteState=0 and customerName='" + cutomerName + "'";
            //}
            //else if (cutomerName != "" && cutomerName != null && sellId != "" && sellId != null && (stockId == "" || stockId == null))
            //{
            //    search = "deleteState=0 and cutomerName='" + cutomerName + "' and sellOffHeadID='" + sellId + "'";
            //}
            //else if (cutomerName != "" && cutomerName != null && stockId != "" && stockId != null && (sellId == "" || sellId == null))
            //{
            //    search = "deleteState=0 and cutomerName='" + cutomerName + "' and saleTaskId='" + stockId + "'";
            //}
            //else if (stockId != "" && stockId != null && sellId != "" && sellId != null && (cutomerName == "" || cutomerName == null))
            //{
            //    search = "deleteState=0 and saleTaskId='" + stockId + "' and sellOffHeadID='" + sellId + "'";
            //}
            //else
            //{
            //    search = "deleteState=0 and saleTaskId='" + stockId + "' and sellOffHeadID='" + sellId + "' and customerName='" + cutomerName + "'";
            //}
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SellOffHead";
            tb.OrderBy = "saleTaskId";
            tb.StrColumnlist = "sellOffHeadID,saleTaskId,kinds,count,totalPrice,realPrice,userName,customerName,makingTime,defaultDiscount";
            tb.IntPageSize = pagesize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = search;
            ds = uBll.selectByPage(tb, out totalCount, out intPageCount);
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + ds.Tables[0].Rows[i]["saleTaskId"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["sellOffHeadID"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["userName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["kinds"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["count"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["defaultDiscount"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["makingTime"].ToString() + "</td>");
                strb.Append("<td>" + "<button class='btn btn-success btn-sm btn_add'><i class='fa fa-plus fa-lg'></i></button>" + "<button class='btn btn-info btn-sm search_back'><i class='fa fa-search'></i></button>" + "<button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button>" + "</td></tr>");
            }
            strb.Append("</tbody>");
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