using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.AccessMGT
{
    using Result = Enums.OpResult;
    public partial class userManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 5, getCurrentPage = 0, totalCount, intPageCount;
        public string search, strSearch, regionId, roleId, regionEdit,roleEdit;
        public DataSet dsRegion, dsRole, ds;
        RSACryptoService rsa = new RSACryptoService();
        UserBll userBll = new UserBll();
        RegionBll regionBll = new RegionBll();
        RoleBll roleBll = new RoleBll();
        User user = new User();
        Role role = new Role();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Search();
                getData(Search());
            }
            //查询操作
            string type = Request.QueryString["type"];
            if(type == "search")
            {
                Search();
                getData(Search());
            }
            else if(type == "region")
            {

                regionId = Request.QueryString["region"];
                string strWhere = "regionId=" + "'" + regionId + "'";
                getData(strWhere);
            }
            else if(type == "role")
            {
                roleId = Request.QueryString["role"];
                string strWhere = "regionId='" + roleId + "'";
                getData(strWhere);
            }
            else if(type == "all")
            {
                regionId = Request["region"];
                roleId = Request["role"];
                string strWhere = "reginId='" + regionId + "' and roleId='" + roleId + "'";
                getData(strWhere);
            }
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
        protected void getData(string strWhere)
        {
            currentPage = Convert.ToInt32(Request.QueryString["currentPage"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_User";
            tbd.OrderBy = "userID";
            tbd.StrColumnlist = "userID,userName,regionName,roleName,regionId,roleId";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = strWhere;
            tbd.IntPageNum = currentPage;
            getCurrentPage = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount,out intPageCount);
            //获取地区下拉框数据
            dsRegion = regionBll.select();
            //获取角色下拉框数据
            dsRole = roleBll.select();
        }

        /// <summary>
        /// 输入框查询
        /// </summary>
        /// <returns>返回查询参数</returns>
        protected string Search()
        {
            try
            {
                search = Request.QueryString["search"];
                strSearch = Request.QueryString["search"];
                if (search.Length == 0)
                {
                    search = "";
                }
                else if (search == null)
                {
                    search = "";
                }
                else
                {
                    search = String.Format(" userID {0} or userName {0} or regionName {0} or roleName {0}", "like '%" + search + "%'");
                }
            }
            catch
            {
            }
            return search;
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