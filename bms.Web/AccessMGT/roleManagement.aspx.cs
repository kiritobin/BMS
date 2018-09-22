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
    public partial class roleManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 3, totalCount, intPageCount;
        public string search, roleId;
        public DataSet dsFun,ds;
        RSACryptoService rsa = new RSACryptoService();
        UserBll userBll = new UserBll();
        Role role = new Role();
        FunctionBll funBll = new FunctionBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            dsFun = funBll.Select();
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
                search = String.Format(" roleName {0}", "like '%" + search + "%'");
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
                for (int j = 0; j < k; j++)
                {
                    funId = dsFunc.Tables[0].Rows[j]["functionId"].ToString() + ",";
                    function = dsFunc.Tables[0].Rows[j]["functionName"].ToString() + ",";
                    functions = functions + function;
                    funIds = funIds + funId;
                }
                functions = functions.Substring(0, functions.Length - 1);
                sb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["roleName"].ToString() + "</ td >");
                sb.Append("<td>" + functions + "</ td >");
                sb.Append("<td><input type='hidden' value='" + ds.Tables[0].Rows[i]["roleId"].ToString() + "' class='roleId' />");
                sb.Append("<input type = 'hidden' value = '" + funIds + "' id = 'funId' />");
                sb.Append("<button class='btn btn-warning btn-sm btn-edit' data-toggle='modal' data-target='#myModa2'><i class='fa fa-pencil fa-lg'></i>&nbsp 编辑</button>");
                sb.Append("<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash-o fa-lg'></i>&nbsp 删除</button></td></ tr >");
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
    }
}