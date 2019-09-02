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
    using Result = Enums.OpResult;
    public partial class lnventoryList : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 20, totalCount, intPageCount;
        public string search = "",userName,regionName;
        RoleBll roleBll = new RoleBll();
        sellOffHeadBll sellBll = new sellOffHeadBll();
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail, isAdmin, funcBookStock;
        public DataSet ds, dsRegion, dsPer,dsUser;
        UserBll userBll = new UserBll();
        RegionBll regionBll = new RegionBll();
        WarehousingBll wareBll = new WarehousingBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
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
            else if (op == "add")
            {
                User user = (User)Session["user"];
                DateTime nowTime = DateTime.Now;
                string nowDt = nowTime.ToString("yyyy-MM-dd");
                long count = 0;
                //判断数据库中是否已经有记录
                DataSet backds = wareBll.getAllTime(1);
                if (backds != null && backds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < backds.Tables[0].Rows.Count; i++)
                    {
                        string time = backds.Tables[0].Rows[i]["time"].ToString();
                        DateTime dt = Convert.ToDateTime(time);
                        string sqlTime = dt.ToString("yyyy-MM-dd");
                        if (sqlTime == nowDt)
                        {
                            //count += 1;
                            string id = backds.Tables[0].Rows[i]["singleHeadId"].ToString();
                            string st1 = id.Substring(10);
                            long rowes = long.Parse(st1) + 1;
                            count = rowes;
                            break;
                        }
                        else
                        {
                            count = 1;
                            break;
                        }
                    }
                    if (count == 0)
                    {
                        count = 1;
                    }
                }
                else
                {
                    count = 1;
                }
                string source = Request["source"];
                string remarks = Request["remarks"];
                if (remarks == "" || remarks == null)
                {
                    remarks = "";
                }
                SingleHead single = new SingleHead();
                Region region = new Region();
                region.RegionId = Convert.ToInt32(source);
                single.Region = region;
                single.SingleHeadId = "RK" + DateTime.Now.Date.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                single.Time = DateTime.Now;
                single.Type = 1;
                single.User = user;
                single.Remarks = remarks;
                Result row = wareBll.insertHead(single);
                if (row == Result.添加成功)
                {
                    Response.Write("添加成功");
                    Response.End();
                }
                else
                {
                    Response.Write("添加失败");
                    Response.End();
                }
            }
            else if (op == "del")
            {
                string Id = Request["account"];
                Result row = wareBll.deleteHead(Id, 1);
                if (row == Result.删除成功)
                {
                    Response.Write("删除成功");
                    Response.End();
                }
                else
                {
                    Response.Write("删除失败");
                    Response.End();
                }
            }
        }


        /// <summary>
        /// 获取数据
        /// </summary>
        protected string getData()
        {
            User user = (User)Session["user"];
            int regionId = user.ReginId.RegionId;
            string roleName = user.RoleId.RoleName;
            currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string userName = Request["userName"];
            string region = Request["regionName"];
            string singHeadId = Request["singHeadId"];
            string time = Request["time"];
            if (userName != "" && userName != null)
            {
                search += " and userName like '%"+ userName + "%'";
            }
            if (region != "" && region != null)
            {
                search += " and regionName like '%" + region + "%'";
            }
            if (singHeadId != "" && singHeadId != null)
            {
                search += " and singleHeadId like '%" + singHeadId + "%'";
            }
            if (time != null && time != "")
            {
                string[] sArray = time.Split('至');
                string startTime = sArray[0];
                string endTime = sArray[1];
                search += " and time BETWEEN '" + startTime + "' and '" + endTime + "'";
            }
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_SingleHead";
            tbd.OrderBy = "time desc";
            tbd.StrColumnlist = "remarks,singleHeadId,regionId,time,regionName,userName,allBillCount,allTotalPrice,allRealPrice";
            tbd.IntPageSize = pageSize;
            if (roleName == "超级管理员")
            {
                tbd.StrWhere = "type=1 and deleteState=0" + search;
            }
            else
            {
                tbd.StrWhere = "type=1 and deleteState=0 and regionId=" + regionId + search;
            }
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            //获取组织
            dsRegion = regionBll.select();
            //获取操作员
            dsUser = regionBll.selectUser();
            
            //生成table
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                sb.Append("<tr><td>" + dr["singleHeadId"].ToString() + "</td>");
                sb.Append("<td>" + dr["regionName"].ToString() + "</td >");
                sb.Append("<td>" + dr["userName"].ToString() + "</td >");
                int kindscount = sellBll.KindsNum(dr["singleHeadId"].ToString());
                sb.Append("<td>" + kindscount.ToString() + "</td >");
                sb.Append("<td>" + dr["allBillCount"].ToString() + "</td >");
                sb.Append("<td>" + dr["allRealPrice"].ToString() + "</td >");
                sb.Append("<td>" + dr["allTotalPrice"].ToString() + "</td >");
                sb.Append("<td>" + dr["time"].ToString() + "</td >");
                sb.Append("<td>" + dr["remarks"].ToString() + "</td >");
                sb.Append("<td>");
                if (dr["allBillCount"].ToString() != "0" && dr["allBillCount"].ToString() != "")
                {
                    sb.Append("<a href='checkStock.aspx?returnId=" + ds.Tables[0].Rows[i]["singleHeadId"].ToString() + "'><button class='btn btn-info btn-sm'><i class='fa fa-search'></i></button></a>");
                }
                else
                {
                    sb.Append("<a href='addStock.aspx?singleHeadId=" + ds.Tables[0].Rows[i]["singleHeadId"].ToString() + "'><button class='btn btn-success btn-sm'><i class='fa fa-plus fa-lg'></i></button></a>");
                    sb.Append("<button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button>");
                }
                sb.Append("</td></tr>");
            }
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            string op = Request["op"];
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
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 15)
                {
                    funcBookStock = true;
                }
            }
        }
    }
}