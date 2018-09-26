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

namespace bms.Web.AccessMGT
{
    using System.Web.Security;
    using Result = Enums.OpResult;
    public partial class userManagement : CommonPage
    {
        public int currentPage = 1, pageSize = 5, totalCount, intPageCount;
        public string search = "";
        public DataSet dsRegion,dsRole,ds;
        RSACryptoService rsa = new RSACryptoService();
        UserBll userBll = new UserBll();
        RegionBll regionBll = new RegionBll();
        RoleBll roleBll = new RoleBll();
        User user = new User();
        Role role = new Role();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            //增、删、改操作
            Region region = new Region();
            string op = Request["op"];
            if (op == "add")
            {
                string name = Request["name"];
                string account = Request["account"];
                string regionID = Request["region"];
                string roleID = Request["role"];
                region.RegionId = Convert.ToInt32(regionID);
                role.RoleId = Convert.ToInt32(roleID);
                user.UserId = Convert.ToInt32(account);
                user.UserName = name;
                user.Pwd = rsa.Encrypt("000000");
                user.ReginId = region;
                user.RoleId = role;
                Result row = userBll.Insert(user);
                if (row == Result.记录存在)
                {
                    Response.Write("该用户已存在不能重复添加");
                    Response.End();
                }
                else if (row == Result.添加成功)
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
            else if (op == "edit")
            {
                string name = Request["name"];
                string account = Request["account"];
                string regionID = Request["region"];
                string roleID = Request["role"];
                region.RegionId = Convert.ToInt32(regionID);
                role.RoleId = Convert.ToInt32(roleID);
                user.UserName = name;
                user.ReginId = region;
                user.RoleId = role;
                Result row = userBll.Update(user);
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
            else if (op == "reset")
            {
                string account = Request["account"];
                user.UserId = Convert.ToInt32(account);
                user.Pwd = rsa.Encrypt("000000");
                Result row = userBll.UpdatePwd(user);
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
            else if (op == "del")
            {
                int account = Convert.ToInt32(Request["account"]);
                Result row = IsdeleteAdmin();
                if (row == Result.记录不存在)
                {
                    Result result = userBll.Delete(account);
                    if (result == Result.删除成功)
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
                else
                {
                    Response.Write("在其他表中有关联不能删除");
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

        /// <summary>
        /// 获取数据
        /// </summary>
        protected string getData()
        {
            currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string userName = Request["userName"];
            string region = Request["region"];
            string role = Request["role"];
            if ((userName == "" || userName == null) && (region == null || region == "") && (role == null || role == ""))
            {
                search = "deleteState=0";
            }
            else if ((userName != "" && userName != null) && (region == null || region == "") && (role == null || role == ""))
            {
                search = String.Format(" userName= '{0}' and deleteState=0", userName);
            }
            else if ((userName == "" || userName == null) && (region != "" && region != null) && (role == null || role == ""))
            {
                search = "regionName='" + region + "'  and deleteState=0";
            }
            else if ((userName == "" || userName == null) && (role != "" && role != null) && (region == null || region == ""))
            {
                search = "roleName='" + role + "'  and deleteState=0";
            }
            else if ((userName == "" || userName == null) && (role != "" && role != null) && (region != null && region != ""))
            {
                search = "regionName='" + region + "' and roleName='" + role + "'  and deleteState=0";
            }
            else if ((userName != "" && userName != null) && (region != null && region != "") && (role == null || role == ""))
            {
                search = String.Format(" userName= '{0}' and regionName = '{1}'  and deleteState=0", userName, region);
            }
            else if ((userName != "" && userName != null) && (region == null || region == "") && (role != null && role != ""))
            {
                search = String.Format(" userName= '{0}' and roleName='{1}'  and deleteState=0", userName, role);
            }
            else
            {
                search = String.Format(" userName= '{0}' and regionName = '{1}' and roleName='{2}'  and deleteState=0", userName, region, role);
            }
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_User";
            tbd.OrderBy = "userID";
            tbd.StrColumnlist = "userID,userName,regionName,roleName,regionId,roleId,deleteState";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            dsRegion = regionBll.select();
            dsRole = roleBll.select();
            //生成table
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["userID"].ToString() + "</td></td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["userName"].ToString() + "</ td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</ td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["roleName"].ToString() + "</ td >");
                sb.Append("<td><input type='hidden' value=" + ds.Tables[0].Rows[i]["regionId"].ToString() + " class='regionId' />");
                sb.Append("<input type='hidden' value=" + ds.Tables[0].Rows[i]["roleId"].ToString() + " class='roleId' />");
                sb.Append("<button class='btn btn-warning btn-sm btn-edit' data-toggle='modal' data-target='#myModa2'><i class='fa fa-pencil fa-lg'></i></button>");
                sb.Append("<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash-o fa-lg'></i></button></td></ tr >");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }

        /// <summary>
        /// 在删除前判断该记录在其他表中是否被引用
        /// </summary>
        /// <returns></returns>
        public Result IsdeleteAdmin()
        {
            string account = Context.Request["account"].ToString();
            Result row = Result.记录不存在;
            if (userBll.IsDelete("T_SingleHead", "userId", account) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_SellOffHead", "userId", account) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_Retail", "userId", account) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_ReplenishmentHead", "userId", account) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            return row;
        }
    }
}