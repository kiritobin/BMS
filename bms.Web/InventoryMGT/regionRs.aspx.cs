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
        public string userName, regionName, region;
        User user;
        protected DataSet dsPer, ds, dsRegion;
        SaleTaskBll saleBll = new SaleTaskBll();
        replenishMentBll repBll = new replenishMentBll();
        RegionBll regionBll = new RegionBll();
        public int totalCount, intPageCount, pageSize = 20, counts,kinds, regionId;
        public string saleTaskId, customerName, userNamemsg, kingdsNum, number, allTotalPrice, allRealPrice, dateTime, state;
        RoleBll roleBll = new RoleBll();
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail, isAdmin;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (User)Session["user"];
            permission();
            dsRegion = regionBll.select();
            getData();
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
                print();
            }
        }

        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            regionId= Convert.ToInt32(Request["regionId"]);
            string search = "";
            StringBuilder strb = new StringBuilder();
            string op = Request["op"];
            if (regionId > 0)
            {
                if (user.RoleId.RoleName == "超级管理员")
                {
                    search = " and regionId=" + regionId;
                    region = regionBll.selectById(regionId);
                    kinds = repBll.getMonkinds(regionId,0);
                    counts = repBll.getTotalMon(regionId,0);
                }
            }
            else
            {
                if (user.RoleId.RoleName == "超级管理员")
                {
                    search = "";
                    counts = 0;
                    kinds = 0;
                    region = "";
                }
                else
                {
                    regionId = user.ReginId.RegionId;
                    search = " and regionId=" + regionId;
                    kinds = repBll.getMonkinds(regionId,0);
                    counts = repBll.getTotalMon(regionId,0);
                    region = regionBll.selectById(regionId);
                }
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
            tb.StrColumnlist = "regionName,customerName,rsMononerID,bookNum,ISBN,bookName,sum(count) as count,dateTime";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = "ISNULL(finishTime) and deleteState=0"+search + " group by regionName,customerName,rsMononerID,bookNum,ISBN,bookName";
            //获取展示的客户数据
            ds = saleBll.selectBypage(tb, out totalCount, out intPageCount);
            //生成table
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                strb.Append("<td><nobr>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</nobr></td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["count"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</td>");
                strb.Append("<td><nobr>" + Convert.ToDateTime(ds.Tables[0].Rows[i]["dateTime"].ToString()).ToString("yyyy/MM/dd") + "</nobr></td></tr>");
            }
            strb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            strb.Append("</tbody>");
            if (op == "paging")
            {
                Response.Write(strb.ToString()+ ":|" + kinds+":|"+ counts + ":|" + region );
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
            string userId = user.UserId;
            DataSet dsRole = roleBll.selectRole(userId);
            string roleName = dsRole.Tables[0].Rows[0]["roleName"].ToString();
            if (roleName == "超级管理员")
            {
                isAdmin = true;
            }
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

        //打印
        private void print()
        {
            WarehousingBll warehousingBll = new WarehousingBll();
            DataSet ds = warehousingBll.regionRs(regionId);
            StringBuilder strb = new StringBuilder();

            region = regionBll.selectById(regionId);
            kinds = repBll.getMonkinds(regionId, 0);
            counts = repBll.getTotalMon(regionId, 0);
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                strb.Append("<td><nobr>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</nobr></td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["count"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</td>");
                strb.Append("<td><nobr>" + Convert.ToDateTime(ds.Tables[0].Rows[i]["dateTime"].ToString()).ToString("yyyy/MM/dd") + "</nobr></td></tr>");
            }
            strb.Append("</tbody>");
            Response.Write(strb.ToString() + ":|" + kinds + ":|" + counts + ":|" + region);
            Response.End();
        }
    }
}