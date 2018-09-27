using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.InventoryMGT
{
    public partial class w : System.Web.UI.Page
    {
        protected DataTable shDt;
        protected DataSet dsGoods, ds;
        protected int pageSize = 20, totalCount, intPageCount;
        protected string shId, shOperator, shCount, shRegionName, shTotalPrice, shRealPrice, shTime;
        UserBll userBll = new UserBll();
        WarehousingBll warehousingBll = new WarehousingBll();
        string singleHeadId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                singleHeadId = Request.QueryString["sId"];
                if (singleHeadId != "" && singleHeadId != null)
                {
                    Session["singleHeadId"] = singleHeadId;
                }
                else
                {
                    singleHeadId = Session["singleHeadId"].ToString();
                }
            }
            getData();
            shDt = warehousingBll.SelectSingleHead(0, singleHeadId);
            int count = shDt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                shId = shDt.Rows[i]["singleHeadId"].ToString();
                shOperator = shDt.Rows[i]["userName"].ToString();
                shCount = shDt.Rows[i]["allBillCount"].ToString();
                shRegionName = shDt.Rows[i]["regionName"].ToString();
                shTotalPrice = shDt.Rows[i]["allTotalPrice"].ToString();
                shRealPrice = shDt.Rows[i]["allRealPrice"].ToString();
                shTime = Convert.ToDateTime(shDt.Rows[i]["time"]).ToString("yyyy年MM月dd日 HH:mm:ss");
            }
            string op = Request["op"];
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
        }
        /// <summary>
        /// 获取分页数据
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
            string op = Request["op"];
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_Monomers";
            tbd.OrderBy = "monId";
            tbd.StrColumnlist = "singleHeadId,monId,ISBN,number,uPrice,totalPrice,realPrice,discount,goodsShelvesId,shelvesName,type,deleteState";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "deleteState=0 and singleHeadId='" + singleHeadId + "'";
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);

            //生成table
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<tbody>");
            int count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                System.Data.DataRow dr = ds.Tables[0].Rows[i];
                sb.Append("<tr><td>" + dr["monId"].ToString() + "</td>");
                sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                sb.Append("<td>" + dr["number"].ToString() + "</td>");
                sb.Append("<td>" + dr["uPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["discount"].ToString() + "</td>");
                sb.Append("<td>" + dr["shelvesName"].ToString() + "</td></tr>");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }
    }
}