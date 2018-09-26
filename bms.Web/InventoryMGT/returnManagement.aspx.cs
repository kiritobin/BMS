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
    public partial class replenishList : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20, row, count = 0;
        public DataSet ds;
        UserBll userBll = new UserBll();
        WarehousingBll wareBll = new WarehousingBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["user"];
            string op = Request["op"];
            if (op == "add")
            {
                count = wareBll.countHead(2)+1;
                string billCount = Request["billCount"];
                string totalPrice = Request["totalPrice"];
                string realPrice = Request["realPrice"];
                SingleHead single = new SingleHead();
                single.AllBillCount = Convert.ToInt32(billCount);
                single.AllRealPrice = Convert.ToInt32(realPrice);
                single.AllTotalPrice = Convert.ToInt32(totalPrice);
                single.Region = user.ReginId;
                single.SingleHeadId = "TH"+DateTime.Now+ count.ToString().PadLeft(6, '0');
                single.Time = DateTime.Now;
                single.Type = 2;
                single.User = user;
                Result row = wareBll.insertHead(single);
                if(row == Result.添加成功)
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
            if(op == "del")
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
            if(op == "search")
            {
                string singleHeadId = Request["ID"];
                string regionName = Request["region"];
                string userName = Request["user"];
                if((singleHeadId ==""||singleHeadId == null)&& (regionName == "" || regionName == null)&& (userName == "" || userName == null))
                {
                    search = "";
                }
                else if(singleHeadId != "" && singleHeadId != null && (regionName == "" || regionName == null) && (userName == "" || userName == null))
                {
                    search = "singleHeadId='" + singleHeadId + "'";
                }
                else if (regionName != "" && regionName != null && (singleHeadId == "" || singleHeadId == null) && (userName == "" || userName == null))
                {
                    search = "regionName='" + regionName + "'";
                }
                else if (userName != "" && userName != null && (regionName == "" || regionName == null) && (singleHeadId == "" || singleHeadId == null))
                {
                    search = "userName='" + userName + "'";
                }
                else if (userName != "" && userName != null && regionName != "" && regionName != null && (singleHeadId == "" || singleHeadId == null))
                {
                    search = "userName='" + userName + "' and regionName='" + regionName + "'";
                }
                else if (userName != "" && userName != null && singleHeadId != "" && singleHeadId != null && (regionName == "" || regionName == null))
                {
                    search = "userName='" + userName + "' and singleHeadId='" + singleHeadId + "'";
                }
                else if (singleHeadId != "" && singleHeadId != null && regionName != "" && regionName != null && (userName == "" || userName == null))
                {
                    search = "singleHeadId='" + singleHeadId + "' and regionName='" + regionName + "'";
                }
                else
                {
                    search = "singleHeadId='" + singleHeadId + "' and regionName='" + regionName + "' and userName='" + userName + "'";
                }
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
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < count; i++)
            {
                DataRow dr = dt.Rows[i];
                sb.Append("<tr><td id='singleHeadId'>" + dr["singleHeadId"].ToString() + "</td>");
                sb.Append("<td>" + dr["regionName"].ToString() + "</ td >");
                sb.Append("<td>" + dr["userName"].ToString() + "</ td >");
                sb.Append("<td>" + dr["allBillCount"].ToString() + "</ td >");
                sb.Append("<td>" + dr["allTotalPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["allRealPrice"].ToString() + "</ td >");
                sb.Append("<td>" + dr["time"].ToString() + "</ td ></ tr >");
                sb.Append("<td><a href='addReturn.aspx?returnId=" + dr["singleHeadId"].ToString() + "'><button class='btn btn-success btn-sm'><i class='fa fa-plus fa-lg'></i></button></a>");
                sb.Append("<a href='checkReturn.aspx?returnId=" + dr["singleHeadId"].ToString() + "'><button class='btn btn-info btn-sm'><i class='fa fa-search'></i></button></a>");
                sb.Append("<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash'></i></button></ td ></ tr >");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }
    }
}