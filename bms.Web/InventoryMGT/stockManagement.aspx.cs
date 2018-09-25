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
            string region = Request["regionName"];
            string singHeadId = Request["singHeadId"];
            if ((userName == "" || userName == null) && (region == null || region == "") && (singHeadId == null || singHeadId == ""))
            {
                search = "";
            }
            else if ((userName != "" && userName != null) && (region == null || region == "") && (singHeadId == null || singHeadId == ""))
            {
                search = String.Format(" and userName= '{0}'", userName);
            }
            else if ((userName == "" || userName == null) && (region != "" && region != null) && (singHeadId == null || singHeadId == ""))
            {
                search = " and regionName='" + region + "'";
            }
            else if ((userName == "" || userName == null) && (singHeadId != "" && singHeadId != null) && (region == null || region == ""))
            {
                search = " and singHeadId='" + singHeadId + "'";
            }
            else if ((userName == "" || userName == null) && (singHeadId != "" && singHeadId != null) && (region != null && region != ""))
            {
                search = " and regionName='" + region + "' and singHeadId='" + singHeadId + "'";
            }
            else if ((userName != "" && userName != null) && (region != null && region != "") && (singHeadId == null || singHeadId == ""))
            {
                search = String.Format(" and userName= '{0}' and regionName = '{1}'", userName, region);
            }
            else if ((userName != "" && userName != null) && (region == null || region == "") && (singHeadId != null && singHeadId != ""))
            {
                search = String.Format(" and userName= '{0}' and singHeadId='{1}'", userName, singHeadId);
            }
            else
            {
                search = String.Format(" and userName= '{0}' and regionName = '{1}' and singHeadId='{2}'  and deleteState=0", userName, region, singHeadId);
            }
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_SingleHead";
            tbd.OrderBy = "singleHeadId";
            tbd.StrColumnlist = "singleHeadId,regionId,userId,time,regionName,userName,allBillCount,allTotalPrice,allRealPrice,type,deleteState,saveState";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "type=1 and deleteState=0" + search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            //生成table
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr><td>" + ds.Tables[0].Rows[i]["singleHeadId"].ToString() + "</td></td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["time"].ToString() + "</ td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</ td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["userName"].ToString() + "</ td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["allBillCount"].ToString() + "</ td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["allTotalPrice"].ToString() + "</ td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["allRealPrice"].ToString() + "</ td >");
                sb.Append("<td><input type='hidden' value=" + ds.Tables[0].Rows[i]["regionId"].ToString() + " class='regionId' />");
                sb.Append("<input type='hidden' value=" + ds.Tables[0].Rows[i]["userId"].ToString() + " class='userId' />");
                sb.Append("<button class='btn btn-info btn-sm' onclick='window.location.href='checkReturn.aspx'' data-toggle='modal' data-target='#myModa2'><i class='fa fa-search'></i></button>");
                sb.Append("<button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button></td></ tr >");
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