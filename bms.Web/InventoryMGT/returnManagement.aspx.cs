﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using bms.Model;
using System.Data;

namespace bms.Web.BasicInfor
{
    using Result = Enums.OpResult;
    public partial class replenishList : System.Web.UI.Page
    {
        public string userName, regionName, roleName;
        public int totalCount, intPageCount, pageSize = 20, row, count = 0;
        public DataSet ds, dsRegion, dsPer,dsUser;
        RoleBll roleBll = new RoleBll();
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail, isAdmin, funcBookStock;
        UserBll userBll = new UserBll();
        sellOffHeadBll sellBll = new sellOffHeadBll();
        RegionBll regionBll = new RegionBll();
        WarehousingBll wareBll = new WarehousingBll();
        FunctionBll functionBll = new FunctionBll();
        User user = new User();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (User)Session["user"];
            roleName = user.RoleId.RoleName;
            permission();
            //获取组织
            dsRegion = regionBll.select();
            //获取操作员
            dsUser = regionBll.selectUser();
            //getData();
            string op = Request["op"];
            if (op == "paging")
            {
                getData();
            }
            if (op == "add")
            {
                DateTime nowTime = DateTime.Now;
                string nowDt = nowTime.ToString("yyyy-MM-dd");
                long count = 0;
                //判断数据库中是否已经有记录
                DataSet backds = wareBll.getAllTime(2);
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
                string regionId = Request["regionId"];
                string remarks = Request["remarks"];
                if(remarks==""|| remarks == null)
                {
                    remarks = "";
                }
                SingleHead single = new SingleHead();
                Region region = new Region();
                region.RegionId = Convert.ToInt32(regionId);
                single.Region = region;
                single.SingleHeadId = "TH" + DateTime.Now.Date.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                single.Time = DateTime.Now;
                single.Type = 2;
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
            if (op == "del")
            {
                string Id = Request["ID"];
                Result row = wareBll.deleteHead(Id, 2);
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
            if (op == "session")
            {
                Session["singId"] = Request["ID"];
                Response.Write("成功");
                Response.End();
            }
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
            string search = "";
            string singleHeadId = Request["ID"];
            string regionName = Request["region"];
            string userName = Request["user"];
            string time = Request["time"];

            if (singleHeadId != "" && singleHeadId != null)
            {
                    search += " and singleHeadId like '%" + singleHeadId + "%'";
            }
            if (regionName != "" && regionName != null)
            {
                    search += " and userRegionName like '%" + regionName + "%'";
            }
            if (userName != "" && userName != null)
            {
                    search += " and userName like '%" + userName + "%'";
            }
            if (time != null && time != "")
            {
                string[] sArray = time.Split('至');
                string startTime = sArray[0];
                string endTime = sArray[1];
                search += " and time BETWEEN '" + startTime + "' and '" + endTime + "'";
            }
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_SingleHead";
            tbd.OrderBy = "time desc";
            tbd.StrColumnlist = "remarks,singleHeadId,userRegionName,userName,allBillCount,allTotalPrice,allRealPrice,time";
            tbd.IntPageSize = pageSize;
            if (user.RoleId.RoleName == "超级管理员")
            {
                tbd.StrWhere = "deleteState=0 and type=2" + search;
            }
            else
            {
                tbd.StrWhere = "deleteState=0 and type=2" + search + " and regionId=" + user.ReginId.RegionId;
            }
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            //获取组织
            //dsRegion = regionBll.select();
            //获取操作员
            //dsUser = regionBll.selectUser();
            //生成table
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            int count = ds.Tables[0].Rows.Count;
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < count; i++)
            {
                DataRow dr = dt.Rows[i];
                sb.Append("<tr><td>" + dr["singleHeadId"].ToString() + "</td>");
                sb.Append("<td>" + dr["userRegionName"].ToString() + "</td>");
                sb.Append("<td>" + dr["userName"].ToString() + "</td>");
                int kindscount = sellBll.KindsNum(dr["singleHeadId"].ToString());
                sb.Append("<td>" + kindscount.ToString() + "</td >");
                sb.Append("<td>" + dr["allBillCount"].ToString() + "</td>");
                sb.Append("<td>" + dr["allTotalPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["allRealPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["time"].ToString() + "</ td >");
                sb.Append("<td>" + dr["remarks"].ToString() + "</td >");
                sb.Append("<td>");
                if (dr["allBillCount"].ToString() != "0" && dr["allBillCount"].ToString() != "")
                {
                    sb.Append("<a href='checkReturn.aspx?sId=" + dr["singleHeadId"].ToString() + "'><button class='btn btn-info btn-sm'><i class='fa fa-search'></i></button></a>");
                }
                else
                {
                    sb.Append("<a href='addReturn.aspx?sId=" + dr["singleHeadId"].ToString() + "'><button class='btn btn-success btn-sm'><i class='fa fa-plus fa-lg'></i></button></a>");
                    sb.Append("<input type='hidden' value='" + dr["singleHeadId"].ToString() + "'/>");
                    sb.Append("<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash'></i></button>");
                }
                sb.Append("</td></tr>");
            }
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }
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
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 15)
                {
                    funcBookStock = true;
                }
            }
        }
    }
}