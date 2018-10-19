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
    public partial class regionRs : System.Web.UI.Page
    {
        FunctionBll functionBll = new FunctionBll();
        public string userName, regionName;
        User user;
        protected DataSet dsPer, ds, dsRegion;
        string rsHeadId;
        SaleTaskBll saleBll = new SaleTaskBll();
        replenishMentBll repBll = new replenishMentBll();
        RegionBll regionBll = new RegionBll();
        public int totalCount, intPageCount, pageSize = 15;
        public string saleTaskId, customerName, userNamemsg, kingdsNum, number, allTotalPrice, allRealPrice, dateTime, state;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (User)Session["user"];
            permission();
            dsRegion = regionBll.select();
            saleTaskId = Session["saleTaskID"].ToString();
            if (user.RoleId.RoleName != "超级管理员")
            {
                getData();
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

        public void getHeadMsg()
        {
            DataSet rsHeads = repBll.getHeadMsg(saleTaskId);
            saleTaskId = rsHeads.Tables[0].Rows[0]["rsHeadID"].ToString();
            customerName = rsHeads.Tables[0].Rows[0]["customerName"].ToString();
            userNamemsg = rsHeads.Tables[0].Rows[0]["userName"].ToString();
            kingdsNum = rsHeads.Tables[0].Rows[0]["kingdsNum"].ToString();
            number = rsHeads.Tables[0].Rows[0]["number"].ToString();
            allTotalPrice = rsHeads.Tables[0].Rows[0]["allTotalPrice"].ToString();
            allRealPrice = rsHeads.Tables[0].Rows[0]["allRealPrice"].ToString();
            dateTime = rsHeads.Tables[0].Rows[0]["dateTime"].ToString();
            state = rsHeads.Tables[0].Rows[0]["state"].ToString();
            if (state == "0")
            {
                state = "单据未完成";
            }
            else
            {
                state = "单据已完成";
            }
        }

        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            int regionId;
            if (user.RoleId.RoleName == "超级管理员")
            {
                regionId = Convert.ToInt32(Request["regionId"]);
            }
            else
            {
                regionId = user.ReginId.RegionId;
            }
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_ReplenishMentMononer";
            tb.OrderBy = "rsMononerID";
            tb.StrColumnlist = "rsMononerID,bookNum,bookName,price,sum(count) as allnumber,realDiscount,sum(totalPrice) as totalPrice,sum(realPrice) as realPrice,dateTime";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = "deleteState=0 and regionId='" + regionId + "' group by bookNum,bookName,price";
            //获取展示的客户数据
            ds = saleBll.selectBypage(tb, out totalCount, out intPageCount);
            //生成table
            StringBuilder strb = new StringBuilder();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<tr><td>" + ds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                strb.Append("<td><nobr>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</nobr></td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["price"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allnumber"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                strb.Append("<td><nobr>" + ds.Tables[0].Rows[i]["dateTime"].ToString() + "</nobr></td></tr>");
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

        /// <summary>
        /// 权限控制
        /// </summary>
        protected void permission()
        {
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