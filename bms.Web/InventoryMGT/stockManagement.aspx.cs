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

namespace bms.Web.InventoryMGT
{
    public partial class lnventoryList : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 5, totalCount, intPageCount;
        public string search = "";
        public DataSet ds;
        UserBll userBll = new UserBll();
        protected void Page_Load(object sender, EventArgs e)
        {

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
    }
}