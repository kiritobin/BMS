﻿using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.AccessMGT
{
    using System.Web.Security;
    using Result = Enums.OpResult;
    public partial class organizationalManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 20, totalCount, intPageCount;
        public string search, regionId,userName,regionName;
        public DataSet ds,dsPer;
        public int count;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail, isAdmin, funcBookStock;
        RegionBll regionBll = new RegionBll();
        UserBll userBll = new UserBll();
        RoleBll roleBll = new RoleBll();
        RSACryptoService rsa = new RSACryptoService();
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
            string op = Request["op"];
            //if (!IsPostBack)
            //{
            //    getData();
            //}
            if (op == "paging")
            {
                getData();
            }
            if (op == "add")
            {
                Insert();
            }
            if (op == "editor")
            {
                Update();
            }
            if (op == "del")
            {
                Delete();
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
        /// 获取数据
        /// </summary>
        protected string getData()
        {
            User user = new User();
            user = (User)Session["user"];
            currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            search = Request["search"];
            if (search != "" && search != null)
            {
                search = String.Format(" regionName {0} and deleteState=0", "like '%" + search + "%'");
            }
            else
            {
                search = "deleteState=0";
            }
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "T_Region";
            tbd.OrderBy = "regionId";
            tbd.StrColumnlist = "regionId,regionName,deleteState";
            tbd.IntPageSize = pageSize;
            if (user.RoleId.RoleName == "超级管理员")
            {
                tbd.StrWhere = search;
            }
            else
            {
                tbd.StrWhere = "regionId=" + user.ReginId.RegionId + " and " + search;
            }
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            //生成table
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["regionId"].ToString() + "</ td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</ td >");
                sb.Append("<td><button class='btn btn-warning btn-sm btn_Editor' data-toggle='modal' data-target='#myModal2'><i class='fa fa-pencil fa-lg'></i></button>");
                sb.Append("<input type='hidden' value='" + ds.Tables[0].Rows[i]["regionId"].ToString() + "' /><button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash-o fa-lg'></i></button></td>");
                sb.Append("<td style='display:none' clall='id'>" + ds.Tables[0].Rows[i]["regionId"].ToString() + "</ td ></ tr >");
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

        public void Insert()
        {
            int roleId = 0;
            string roleName = "微信零售";
            int row = roleBll.selectByroleName(roleName);
            if (row == 0)
            {
                Role role = new Role();
                role.RoleName = roleName;
                Result insert = roleBll.Insert(role);
                if (insert == Result.添加成功)
                {
                    roleId = roleBll.selectByroleName(roleName);
                    string sqlText = "(" + roleId + "," + 14 + ")";
                    Result inserts = roleBll.InsertPer(sqlText, roleId, "添加");
                    if (inserts == Result.添加失败)
                    {
                        Response.Write("添加失败");
                        Response.End();
                    }
                }
            }
            else
            {
                roleId = row;
            }
            string reName = Request["name"];
            Result result = regionBll.isExit(reName);
            if (result == Result.记录不存在)
            {
                Result rsInsert = regionBll.insert(reName);
                if (rsInsert == Result.添加成功)
                {
                    int regionId = regionBll.getRegionIdByName(reName);
                    Region region = new Region();
                    region.RegionId = Convert.ToInt32(regionId);
                    Role userRole = new Role();
                    userRole.RoleId = roleId;
                    User user = new User();
                    user.UserId = regionId+"01";
                    user.UserName = reName+"微信零售";
                    user.Pwd = rsa.Encrypt("000000");
                    user.ReginId = region;
                    user.RoleId = userRole;
                    Result rows = userBll.Insert(user);
                    if (rows == Result.记录存在)
                    {
                        Response.Write("添加失败");
                        Response.End();
                    }
                    else if (rows == Result.添加失败)
                    {
                        Response.Write("添加失败");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("添加成功");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("添加失败");
                    Response.End();
                }
            }
            else
            {
                Response.Write("该组织已经存在");
                Response.End();
            }
        }

        /// <summary>
        /// 添加组织，同时为添加的组织分配名为未上架的货架
        /// </summary>
        //public void Insert()
        //{
        //    GoodsShelvesBll gBll = new GoodsShelvesBll();
        //    int id = gBll.getMaxShelvesId();
        //    string reName = Request["name"];
        //    string shelvese = "未上架";
        //    int shelvesId = id;
        //    bool isExitId = true;
        //    do
        //    {
        //        shelvesId += 1;
        //        isExitId = gBll.isExitID(shelvesId);
        //    }
        //    while (isExitId);
        //    Result result = regionBll.isExit(reName);
        //    if (result == Result.记录不存在)
        //    {
        //        TableInsertion tb = new TableInsertion()
        //        {
        //            InShelvesId = shelvesId,
        //            InRegionName = reName,
        //            InShelvesName = shelvese,
        //            OutCount = count
        //        };
        //        Result row = regionBll.InsertManyTable(tb, out count);
        //        if (row == Result.添加成功)
        //        {
        //            Response.Write("添加成功");
        //            Response.End();
        //        }
        //        else
        //        {
        //            Response.Write("添加失败");
        //            Response.End();
        //        }
        //    }
        //    else
        //    {
        //        Response.Write("该名称已经存在");
        //        Response.End();
        //    }
        //}
        /// <summary>
        /// 更新地区
        /// </summary>
        public void Update()
        {
            string regionName = Request["name"];
            string regionId = Request["id"];
            Region region = new Region()
            {
                RegionId = Convert.ToInt32(regionId),
                RegionName = regionName
            };
            Result result = regionBll.isExit(regionName);
            if (result == Result.记录不存在)
            {
                Result row = regionBll.Update(region);
                if (row == Result.更新成功)
                {
                    Response.Write("更新成功");
                    Response.End();
                }
                else
                {
                    Response.Write("更新失败");
                    Response.End();
                }
            }
            else
            {
                Response.Write("该名称已经存在");
                Response.End();
            }
        }
        /// <summary>
        /// 删除组织
        /// </summary>
        public void Delete()
        {
            string regId = Request["regionId"];
            Result row = regionBll.delete(Convert.ToInt32(regId));
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




        /// <summary>
        /// 在删除前判断组织在其他表中是否被引用
        /// </summary>
        /// <returns></returns>
        public Result IsdeleteAdmin(string regionId)
        {
            Result row = Result.记录不存在;
            if (userBll.IsDelete("T_Customer", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_User", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_Stock", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_SingleHead", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_SellOffHead", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_ReplenishmentHead", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            return row;
        }
        /// <summary>
        /// 判断货架是否在其他表被引用
        /// </summary>
        /// <param name="shelvesId">货架Id</param>
        /// <returns></returns>
        public Result IsDeleteShelves(string shelvesId)
        {
            Result stock = regionBll.IsDelete("T_Stock", "goodsShelvesId", shelvesId);
            Result monomers = regionBll.IsDelete("T_Monomers", "goodsShelvesId", shelvesId);
            if (stock == Result.记录不存在)
            {
                if (monomers == Result.记录不存在)
                {
                    return Result.记录不存在;
                }
                else
                {
                    return Result.关联引用;
                }
            }
            else
            {
                return Result.关联引用;
            }
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