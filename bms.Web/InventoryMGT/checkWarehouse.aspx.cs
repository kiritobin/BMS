using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.InventoryMGT
{
    public partial class w : System.Web.UI.Page
    {
        protected DataTable shDt;
        protected DataSet dsGoods, ds,dsPer;
        protected int pageSize = 20, totalCount, intPageCount;
        protected string shId, shOperator, shCount, shRegionName, shTotalPrice, shRealPrice, shTime,userName,regionName;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail;
        UserBll userBll = new UserBll();
        WarehousingBll warehousingBll = new WarehousingBll();
        string singleHeadId;
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
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
            shDt = warehousingBll.SelectSingleHead(singleHeadId);
            int count = shDt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                shId = shDt.Rows[i]["singleHeadId"].ToString();
                shOperator = shDt.Rows[i]["userName"].ToString();
                shCount = shDt.Rows[i]["allBillCount"].ToString();
                shRegionName = shDt.Rows[i]["regionName"].ToString();
                shTotalPrice = shDt.Rows[i]["allTotalPrice"].ToString();
                shRealPrice = shDt.Rows[i]["allRealPrice"].ToString();
                shTime = Convert.ToDateTime(shDt.Rows[i]["time"]).ToString("yyyy年MM月dd日");
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
            if (op=="print")
            {
                StringBuilder sb = new StringBuilder();
                SaleMonomerBll saleMonomerBll = new SaleMonomerBll();
                DataSet dataSet = saleMonomerBll.checkStock(singleHeadId);
                DataRowCollection drc = dataSet.Tables[0].Rows;
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    sb.Append("<tr><td>" + (i + 1) + "</td>");
                    sb.Append("<td>" + drc[i]["ISBN"].ToString() + "</td >");
                    sb.Append("<td>" + drc[i]["bookName"].ToString() + "</td>");
                    sb.Append("<td>" + drc[i]["number"].ToString() + "</td>");
                    sb.Append("<td>" + drc[i]["uPrice"].ToString() + "</td >");
                    sb.Append("<td>" + drc[i]["discount"].ToString() + "</td >");
                    sb.Append("<td>" + drc[i]["totalPrice"].ToString() + "</td >");
                    sb.Append("<td>" + drc[i]["realPrice"].ToString() + "</td >");
                    sb.Append("<td>" + drc[i]["shelvesName"].ToString() + "</td ></tr >");
                }
                Response.Write(sb);
                Response.End();
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
            tbd.StrTable = "V_Monomer";
            tbd.OrderBy = "singleHeadId";
            tbd.StrColumnlist = "singleHeadId,monId,ISBN,number,uPrice,totalPrice,realPrice,discount,shelvesId,shelvesName,type,deleteState,bookName,regionName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "deleteState=0 and singleHeadId='" + singleHeadId + "'";
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);

            //生成table
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                System.Data.DataRow dr = ds.Tables[0].Rows[i];
                sb.Append("<tr><td>" + dr["monId"].ToString() + "</td>");
                sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                sb.Append("<td>" + dr["number"].ToString() + "</td>");
                sb.Append("<td>" + dr["uPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["discount"].ToString() + "</td>");
                sb.Append("<td>" + dr["shelvesName"].ToString() + "</td></tr>");
            }
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            sb.Append("<input type='hidden' value='" + ds.Tables[0].Rows[0]["regionName"].ToString() + "' id='sourceRegin' />");
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }
        protected void permission()
        {
            FunctionBll functionBll = new FunctionBll();
            User user = (User)Session["user"];
            userName = user.UserName;
            regionName = user.ReginId.RegionName;
            Role role = new Role();
            role = user.RoleId;
            int roleId = role.RoleId;
            dsPer = functionBll.SelectByRoleId(roleId);
            for (int i = 0; i < dsPer.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 1)
                {
                    funcOrg = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 2)
                {
                    funcRole = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 3)
                {
                    funcUser = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 4)
                {
                    funcGoods = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 5)
                {
                    funcCustom = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 6)
                {
                    funcLibrary = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 7)
                {
                    funcBook = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 8)
                {
                    funcPut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 9)
                {
                    funcOut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 10)
                {
                    funcSale = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 11)
                {
                    funcSaleOff = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 12)
                {
                    funcReturn = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 13)
                {
                    funcSupply = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 14)
                {
                    funcRetail = true;
                }
            }
        }
    }
}