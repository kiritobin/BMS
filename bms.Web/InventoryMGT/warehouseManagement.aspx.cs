using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using bms.Model;
using System.Data;

namespace bms.Web.BasicInfor
{
    using Result = Enums.OpResult;
    public partial class outboundList : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20, row, count;
        public DataSet ds;
        UserBll userBll = new UserBll();
        WarehousingBll wareBll = new WarehousingBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            Model.User user = (User)Session["user"];
            string op = Request["op"];
            if (op == "add")
            {
                count = wareBll.countHead(0) + 1;
                string billCount = Request["billCount"];
                string totalPrice = Request["totalPrice"];
                string realPrice = Request["realPrice"];
                SingleHead single = new SingleHead();
                single.AllBillCount = Convert.ToInt32(billCount);
                single.AllRealPrice = Convert.ToInt32(realPrice);
                single.AllTotalPrice = Convert.ToInt32(totalPrice);
                single.SingleHeadId = "CK" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                single.Time = DateTime.Now;
                single.Type = 0;
                single.User = user;
                single.Region = user.ReginId;
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
            if (op == "del")
            {
                string Id = Request["ID"];
                Result row = wareBll.deleteHead(Id);
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
        /// 获取分页数据
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string op = Request["op"];
            string search = "";
            string singleHeadId = Request["ID"];
            string regionName = Request["region"];
            string userName = Request["user"];
            if ((singleHeadId == "" || singleHeadId == null) && (regionName == "" || regionName == null) && (userName == "" || userName == null))
            {
                search = "deleteState=0 and type=0";
            }
            else if (singleHeadId != "" && singleHeadId != null && (regionName == "" || regionName == null) && (userName == "" || userName == null))
            {
                search = "deleteState=0 and type=0 and singleHeadId='" + singleHeadId + "'";
            }
            else if (regionName != "" && regionName != null && (singleHeadId == "" || singleHeadId == null) && (userName == "" || userName == null))
            {
                search = "deleteState=0 and type=0 and regionName='" + regionName + "'";
            }
            else if (userName != "" && userName != null && (regionName == "" || regionName == null) && (singleHeadId == "" || singleHeadId == null))
            {
                search = "deleteState=0 and type=0 and userName='" + userName + "'";
            }
            else if (userName != "" && userName != null && regionName != "" && regionName != null && (singleHeadId == "" || singleHeadId == null))
            {
                search = "deleteState=0 and type=0 and userName='" + userName + "' and regionName='" + regionName + "'";
            }
            else if (userName != "" && userName != null && singleHeadId != "" && singleHeadId != null && (regionName == "" || regionName == null))
            {
                search = "deleteState=0 and type=0 and userName='" + userName + "' and singleHeadId='" + singleHeadId + "'";
            }
            else if (singleHeadId != "" && singleHeadId != null && regionName != "" && regionName != null && (userName == "" || userName == null))
            {
                search = "deleteState=0 and type=0 and singleHeadId='" + singleHeadId + "' and regionName='" + regionName + "'";
            }
            else
            {
                search = "deleteState=0 and type=0 and singleHeadId='" + singleHeadId + "' and regionName='" + regionName + "' and userName='" + userName + "'";
            }
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_SingleHead";
            tbd.OrderBy = "singleHeadId";
            tbd.StrColumnlist = "singleHeadId,regionName,userName,allBillCount,allTotalPrice,allRealPrice,time";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);

            //生成table
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<tbody>");
            int count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                System.Data.DataRow dr = ds.Tables[0].Rows[i];
                sb.Append("<tr><td class='singleHeadId'>" + dr["singleHeadId"].ToString() + "</td>");
                sb.Append("<td>" + dr["regionName"].ToString() + "</ td >");
                sb.Append("<td>" + dr["userName"].ToString() + "</ td >");
                sb.Append("<td>" + dr["allBillCount"].ToString() + "</ td >");
                sb.Append("<td>" + dr["allTotalPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["allRealPrice"].ToString() + "</ td >");
                sb.Append("<td>" + dr["time"].ToString() + "</ td ></ tr >");
                sb.Append("<td><a href='addWarehouse.aspx?sId=" + dr["singleHeadId"].ToString() + "'><button class='btn btn-success btn-sm'><i class='fa fa-plus fa-lg'></i></button></a>");
                sb.Append("<a href='checkWarehouse.aspx?sId=" + dr["singleHeadId"].ToString() + "'><button class='btn btn-info btn-sm'><i class='fa fa-search'></i></button></a>");
                sb.Append("<input type='hidden' value='" + dr["singleHeadId"].ToString() + "'/>");
                sb.Append("<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash'></i></button></ td ></ tr >");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }
    }
}