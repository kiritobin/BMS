using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.InventoryMGT
{
    using Result = Enums.OpResult;
    public partial class lnventoryList : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 20, totalCount, intPageCount;
        public string search = "";
        public DataSet ds,dsRegion;
        UserBll userBll = new UserBll();
        RegionBll regionBll = new RegionBll();
        WarehousingBll wareBll = new WarehousingBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            string op = Request["op"];
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
            else if (op=="add")
            {
                User user = (User)Session["user"];
                int count = Convert.ToInt32(Request["count"]);
                double totalPrice = Convert.ToDouble(Request["totalPrice"]);
                double realPrice = Convert.ToDouble(Request["realPrice"]);
                string source = Request["source"];
                SingleHead single = new SingleHead();
                Region region = new Region();
                region.RegionId = Convert.ToInt32(source);
                single.Region = region;
                single.SingleHeadId = "RK" + DateTime.Now.Date.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                single.Time = DateTime.Now;
                single.Type = 1;
                single.User = user;
                Result row = wareBll.insertHead(single);
                if (row == Result.添加成功)
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
            else if (op=="del")
            {
                string Id = Request["account"];
                Result row = wareBll.deleteHead(Id, 1);
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
                search = " and singleHeadId=" + singHeadId;
            }
            else if ((userName == "" || userName == null) && (singHeadId != "" && singHeadId != null) && (region != null && region != ""))
            {
                search = " and regionName='" + region + "' and singleHeadId=" + singHeadId;
            }
            else if ((userName != "" && userName != null) && (region != null && region != "") && (singHeadId == null || singHeadId == ""))
            {
                search = String.Format(" and userName= '{0}' and regionName = '{1}'", userName, region);
            }
            else if ((userName != "" && userName != null) && (region == null || region == "") && (singHeadId != null && singHeadId != ""))
            {
                search = String.Format(" and userName= '{0}' and singleHeadId={1}", userName, singHeadId);
            }
            else
            {
                search = String.Format(" and userName= '{0}' and regionName = '{1}' and singleHeadId={2}  and deleteState=0", userName, region, singHeadId);
            }
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_SingleHead";
            tbd.OrderBy = "singleHeadId";
            tbd.StrColumnlist = "singleHeadId,regionId,time,regionName,userName,allBillCount,allTotalPrice,allRealPrice";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "type=1 and deleteState=0" + search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            dsRegion = regionBll.select();
            //生成table
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr><td>" + ds.Tables[0].Rows[i]["singleHeadId"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["userName"].ToString() + "</td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["allBillCount"].ToString() + "</td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["allTotalPrice"].ToString() + "</td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["allRealPrice"].ToString() + "</td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["time"].ToString() + "</td >");
                sb.Append("<td><a href='addStock.aspx?singleHeadId=" + ds.Tables[0].Rows[i]["singleHeadId"].ToString() + "'><button class='btn btn-success btn-sm'><i class='fa fa-plus fa-lg'></i></button></a>");
                sb.Append("<a href='checkStock.aspx?returnId=" + ds.Tables[0].Rows[i]["singleHeadId"].ToString() + "'><button class='btn btn-info btn-sm'><i class='fa fa-search'></i></button></a>");
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