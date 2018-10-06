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
    public partial class roleManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 3, totalCount, intPageCount;
        public string search, roleId;
        public DataSet dsFun,ds,dsPer;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply;
        RSACryptoService rsa = new RSACryptoService();
        UserBll userBll = new UserBll();
        RoleBll roleBll = new RoleBll();
        Role role = new Role();
        FunctionBll funBll = new FunctionBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
            getData();
            dsFun = funBll.Select();
            string op = Request["op"];
            if (op == "add")
            {
                insert();
            }
            else if (op == "edit")
            {
                updateRole();
            }
            else if(op == "editFun")
            {
                updateFunction();
            }
            else if(op == "del")
            {
                delete();
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
            search = Request["search"];
            if (search != "" && search != null)
            {
                search = String.Format(" roleName = '{0}'",search);
            }
            else
            {
                search = "";
            }
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "T_Role";
            tbd.OrderBy = "roleId";
            tbd.StrColumnlist = "roleId,roleName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            //生成table
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int roleId = Convert.ToInt32(ds.Tables[0].Rows[i]["roleId"].ToString());
                DataSet dsFunc = funBll.SelectByRoleId(roleId);
                string function="", functions = "", funId="", funIds="";
                int k = dsFunc.Tables[0].Rows.Count;
                int rows=0;
                if (k > 0)
                {
                    for (int j = 0; j < k; j++)
                    {
                        funId = dsFunc.Tables[0].Rows[j]["functionId"].ToString() + ",";
                        function = dsFunc.Tables[0].Rows[j]["functionName"].ToString() + ",";
                        functions = functions + function;
                        funIds = funIds + funId;
                    }
                    functions = functions.Substring(0, functions.Length - 1);
                    string[] row = functions.Split(',');
                    rows = row.Length;
                }
                sb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["roleName"].ToString() + "</ td >");
                sb.Append("<td style='width:500px;'>" + functions + "</ td >");
                sb.Append("<td><input type='hidden' value='" + rows + "' />");
                sb.Append("<input type='hidden' value='" + ds.Tables[0].Rows[i]["roleId"].ToString() + "' />");
                sb.Append("<input type = 'hidden' value = '" + funIds + "' />");
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
        /// 添加
        /// </summary>
        protected void insert()
        {
            string value = "", values = "", sqlText = "";
            string roleName = Request["roleName"];
            int row = roleBll.selectByroleName(roleName);
            if (row == 0)
            {
                role.RoleName = roleName;
                Result insert = roleBll.Insert(role);
                if (insert == Result.添加成功)
                {
                    int roleIds = roleBll.selectByroleName(roleName);
                    string funIds = Request["functionId"];
                    string func = funIds.Substring(0, funIds.Length - 1);
                    string[] functions = func.Split('?');
                    role.RoleName = roleName;
                    for (int i = 0; i < functions.Length; i++)
                    {
                        int functionId = Convert.ToInt32(functions[i]);
                        value = "(" + roleIds + "," + functionId + "),";
                        values = values + value;
                    }
                    sqlText = values.Substring(0, values.Length - 1);
                    Result inserts = roleBll.InsertPer(sqlText, roleIds,"添加");
                    if (inserts == Result.添加失败)
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
                Response.Write("该职位名已存在，请您输入新的职位名");
                Response.End();
            }
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        protected void updateRole()
        {
            string roleId = Request["roleId"];
            string roleName = Request["roleName"];
            int row = roleBll.selectByroleName(roleName);
            if (row == 0)
            {
                Role role = new Role();
                role.RoleId = Convert.ToInt32(roleId);
                role.RoleName = roleName;
                Result edit = roleBll.Update(role);
                if (edit == Result.更新成功)
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
                Response.Write("该职位名已存在，请您输入新的职位名");
                Response.End();
            }
        }

        /// <summary>
        /// 更新角色，功能关系
        /// </summary>
        protected void updateFunction()
        {
            string value = "", values = "", sqlText = "";
            int count = Convert.ToInt32(Request["rows"]);
            int roleId = Convert.ToInt32(Request["roleId"]);
            string roleName = Request["roleName"];
            string oldName = Request["oldName"];
            string funIds = Request["funIds"];
            if (oldName != roleName)
            {
                if (userBll.IsDelete("T_User", "roleId", roleId.ToString()) == Result.关联引用)
                {
                    Response.Write("该数据在其他表中被引用，不可编辑");
                    Response.End();
                }
                else
                {
                    //批量删除
                    Result del = roleBll.DeletePer(roleId, count);
                    if (del == Result.删除成功)
                    {
                        string func = funIds.Substring(0, funIds.Length - 1);
                        string[] functions = func.Split('?');
                        for (int i = 0; i < functions.Length; i++)
                        {
                            int functionId = Convert.ToInt32(functions[i]);
                            value = "values(" + roleId + "," + functionId + "),";
                            values = values + value;
                        }
                        sqlText = values.Substring(0, values.Length - 1);
                        Result inserts = roleBll.InsertPer(sqlText, roleId,"更新");
                        if (inserts == Result.添加失败)
                        {
                            Response.Write("更新失败");
                            Response.End();
                        }
                        else
                        {
                            Response.Write("更新成功");
                            Response.End();
                        }
                    }
                    else
                    {
                        Response.Write("更新失败");
                        Response.End();
                    }
                }
                Role role = new Role();
                role.RoleId = Convert.ToInt32(roleId);
                role.RoleName = roleName;
                Result edit = roleBll.Update(role);
                if (edit == Result.更新成功)
                {
                    
                }
                else
                {
                    Response.Write("更新失败");
                    Response.End();
                }
            }
            else
            {
                //批量删除
                Result del = roleBll.DeletePer(roleId,count);
                if (del == Result.删除成功)
                {
                    string func = funIds.Substring(0, funIds.Length - 1);
                    string[] functions = func.Split('?');
                    for (int i = 0; i < functions.Length; i++)
                    {
                        int functionId = Convert.ToInt32(functions[i]);
                        value = "(" + roleId + "," + functionId + "),";
                        values = values + value;
                    }
                    sqlText = values.Substring(0, values.Length - 1);
                    Result inserts = roleBll.InsertPer(sqlText, roleId,"更新");
                    if (inserts == Result.添加失败)
                    {
                        Response.Write("更新失败");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("更新成功");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("更新失败");
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 删除角色及角色，功能关系
        /// </summary>
        protected void delete()
        {
            int count = Convert.ToInt32(Request["rows"]);
            int roleId = Convert.ToInt32(Request["roleId"]);
            //批量删除
            if (userBll.IsDelete("T_User", "roleId", roleId.ToString()) == Result.关联引用)
            {
                Response.Write("该数据在其他表中被引用，不可删除");
                Response.End();
            }
            else
            {
                Result del = roleBll.Delete(roleId);
                if (del == Result.删除成功)
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
        protected void permission()
        {
            FunctionBll functionBll = new FunctionBll();
            User user = (User)Session["user"];
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
            }
        }
    }
}