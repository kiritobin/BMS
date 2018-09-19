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

namespace bms.Web.CustomerMGT
{
    public partial class collectionManagement : System.Web.UI.Page
    {
        public int totalCount, intPageCount,pageSize=5;
        public DataSet dsRegion, ds;
        RegionBll regionBll = new RegionBll();
        UserBll userBll = new UserBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        protected string getData()
        {
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage==0)
            {
                currentPage = 1;
            }
            string search = Request["search"];
            string region = Request["region"];
            if (region==""|| region==null)
            {
                search = String.Format(" bookName {0} or ISBN {0} or price {0} or collectionNum {0} or customerName {0} or regionName {0}", "like '%" + search + "%'");
            }
            else if(search==""||search==null)
            {
                search = String.Format("regionId={0}", region);
            }
            else if((region == "" || region == null)&&(search == "" || search == null))
            {
                search = "";
            }
            else
            {
                search = String.Format(" bookName {0} or ISBN {0} or price {0} or collectionNum {0} or customerName {0} or regionName {0} and regionId={1}", "like '%" + search + "%'", region);
            }
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_LibraryCollection";
            tbd.OrderBy = "bookName";
            tbd.StrColumnlist = "bookName,ISBN,price,collectionNum,customerName,regionName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            //获取地区下拉框数据
            dsRegion = regionBll.select();
            //生成table
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr><td>" +(i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</td><td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</ td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"].ToString() + "</ td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["price"].ToString() + "</ td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["collectionNum"].ToString() + "</ td ></ tr >");
            }
            sb.Append("</tbody>");
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