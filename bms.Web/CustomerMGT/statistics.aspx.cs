using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using System.Data;
using bms.Model;
using System.Web.Security;

namespace bms.Web.CustomerMGT
{
    using Result = Enums.OpResult;
    public partial class statistics : System.Web.UI.Page
    {
        public DataSet regionds, dsPer;
        public string operatorId;
        RoleBll roleBll = new RoleBll();
        public User user = new User();
        public string userName, regionName;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail, isAdmin, funcBookStock;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                permission();
                User user = (User)Session["user"];
                operatorId = user.UserId.ToString();
            }
            RegionBll regBll = new RegionBll();
            ConfigurationBll cBll = new ConfigurationBll();
            Result result;
            regionds = regBll.select();
            string op = Request["op"];
            if (op == "sure")
            {
                string startTime = Request["startDt"].ToString(),
                endTime = Request["endDt"].ToString(),
                regionName = Request["regName"].ToString();
                string type = Request["type"].ToString();
                DateTime st = DateTime.Parse(startTime);
                DateTime et = DateTime.Parse(endTime);
                result = cBll.isExist(regionName);
                if (result == Result.记录不存在)
                {
                    result = cBll.Insert(st, et, regionName,type);
                    if(result == Result.添加成功)
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
                else
                {
                    result = cBll.Update(st, et, regionName,type);
                    if(result == Result.更新成功)
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
            }
            if (op == "goScreen")
            {
                string regionName = Request["regName"].ToString();
                if (regionName == "选择组织" || regionName == null)
                {
                    User user = (User)Session["user"];
                    regionName = user.ReginId.RegionName;
                }
                Session["regionName"] = regionName;
                result = cBll.isExist(regionName);
                if (result == Result.记录不存在)
                {
                    Response.Write("记录不存在");
                    Response.End();
                }
                else
                {
                    Response.Write("记录存在");
                    Response.End();
                }
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

        protected void permission()
        {
            FunctionBll functionBll = new FunctionBll();
            user = (User)Session["user"];
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