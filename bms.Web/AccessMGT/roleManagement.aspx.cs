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
    public partial class roleManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 1, totalCount, intPageCount;
        public string search, roleId;
        public DataSet ds;
        RSACryptoService rsa = new RSACryptoService();
        UserBll userBll = new UserBll();
        Role role = new Role();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        protected string getData()
        {
            currentPage = Convert.ToInt32(Request.QueryString["currentPage"]);
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
            tbd.StrTable = "V_Permission";
            tbd.OrderBy = "roleId";
            tbd.StrColumnlist = "roleId,roleName,functionName";
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
                sb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["roleName"].ToString() + "</ td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["functionName"].ToString() + "</ td >");
                sb.Append("<td><input type='hidden' value=" + ds.Tables[0].Rows[i]["roleId"].ToString() + " class='roleId' />");
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