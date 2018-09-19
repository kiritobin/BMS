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
    using Result = Enums.OpResult;
    public partial class userManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 2, getCurrentPage = 0, totalCount, intPageCount;
        public string search="", strSearch, regionId, roleId, regionEdit,roleEdit;
        public DataSet dsRegion, dsRole, ds;
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
            if(op == "add")
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
                if(row  == Result.添加成功)
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
            else if(op == "editData")
            {
                regionEdit = Request["region"];
                roleEdit = Request["role"];
            }
            else if(op == "edit")
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
            else if(op == "reset")
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
            else if(op == "del")
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
            search = Request["search"];
            regionId = Request["region"];
            roleId = Request["role"];
            if ((search == "" || search == null) && (regionId == null || regionId == "") && (roleId == null || roleId == ""))
            {
                search = "";
            }
            else if ((search != "" && search != null) && (regionId == null || regionId == "") && (roleId == null || roleId == ""))
            {
                search = String.Format(" userID {0} or userName {0} or regionName {0} or roleName {0}", "like '%" + search + "%'");
            }
            else if ((search == "" || search == null) && (regionId != "" && regionId !=null) && (roleId == null || roleId == ""))
            {
                search = "regionId=" + regionId;
            }
            else if ((search == "" || search == null) && (roleId != "" && roleId != null) && (regionId == null || regionId == ""))
            {
                search = "roleId=" + roleId;
            }
            else if ((search == "" || search == null) && (roleId != "" && roleId != null) && (regionId != null && regionId != ""))
            {
                search = "regionId=" + regionId + " and roleId=" + roleId;
            }
            else if ((search != "" && search != null) && (regionId != null && regionId != "") && (roleId == null || roleId == ""))
            {
                search = String.Format(" (userID {0} or userName {0} or regionName {0} or roleName {0}) and regionId = {1}", "like '%" + search + "%'", regionId);
            }
            else if ((search != "" && search != null) && (regionId == null || regionId == "") && (roleId != null && roleId != ""))
            {
                search = String.Format(" (userID {0} or userName {0} or regionName {0} or roleName {0}) and roleId={1}", "like '%" + search + "%'", roleId);
            }
            else
            {
                search = String.Format(" (userID {0} or userName {0} or regionName {0} or roleName {0}) and regionId = {1} and roleId={2}", "like '%" + search + "%'",regionId ,roleId);
            }
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_User";
            tbd.OrderBy = "userID";
            tbd.StrColumnlist = "userID,userName,regionName,roleName,regionId,roleId";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount,out intPageCount);
            //获取地区下拉框数据
            dsRegion = regionBll.select();
            //获取角色下拉框数据
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
                sb.Append("<button class='btn btn-warning btn-sm btn-edit' data-toggle='modal' data-target='#myModa2'><i class='fa fa-pencil fa-lg'></i>&nbsp 编辑</button>");
                sb.Append("<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash-o fa-lg'></i>&nbsp 删除</button></td></ tr >");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value=' "+ intPageCount + " ' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }

        public string getRegion()
        {
            //生成table
            StringBuilder sbRegion = new StringBuilder();
            sbRegion.Append("<tbody>");
            for (int i = 0; i < dsRegion.Tables[0].Rows.Count; i++)
            {
                sbRegion.Append("< option value = '" + dsRegion.Tables[0].Rows[i]["regionId"].ToString() + "'>" + dsRegion.Tables[0].Rows[i]["regionName"].ToString() + "</ option >");
            }
            sbRegion.Append("</tbody>");
            return sbRegion.ToString();
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