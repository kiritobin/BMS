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
    public partial class inventoryStatistics : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 20, totalCount, intPageCount, row, funCount;
        RoleBll roleBll = new RoleBll();
        BookBasicBll bookbll = new BookBasicBll();
        WarehousingBll warehousingBll = new WarehousingBll();
        RegionBll regionBll = new RegionBll();
        public bool isAdmin;
        public DataTable dsSupplier, dsUser,dsSource;
        public DataSet dsRegion;
        protected string sbNum, sjNum, total, real, title;
        protected void Page_Load(object sender, EventArgs e)
        {
            string tjType = Request["type"];
            string tjRegion = Request["region"];
            string tjHeadId = Request["singleHeadId"];
            string tjuserName = Request["userName"];
            string tjtime = Request["time"];
            string defaultWhere = "";
            if(tjRegion != "null" && tjRegion != "" && tjRegion != null)
            {
                if (defaultWhere == "")
                {
                    defaultWhere += "regionName='" + tjRegion + "'";
                }
                else
                {
                    defaultWhere += " and regionName='" + tjRegion + "'";
                }
            }
            if (tjHeadId != "null" && tjHeadId != "" && tjHeadId != null)
            {
                if (defaultWhere == "")
                {
                    defaultWhere += "singleHeadId='" + tjHeadId + "'";
                }
                else
                {
                    defaultWhere += " and singleHeadId='" + tjHeadId + "'";
                }
            }
            if (tjuserName != "null" && tjuserName != "" && tjuserName != null)
            {
                if (defaultWhere == "")
                {
                    defaultWhere += "userName='" + tjuserName + "'";
                }
                else
                {
                    defaultWhere += " and userName='" + tjuserName + "'";
                }
            }
            if (tjtime != "null" && tjtime != "" && tjtime != null)
            {
                if (defaultWhere == "")
                {
                    string[] sArray = tjtime.Split('至');
                    string startTime = sArray[0];
                    string endTime = sArray[1];
                    defaultWhere += "time BETWEEN '" + startTime + "' and '" + endTime + "'";
                }
                else
                {
                    string[] sArray = tjtime.Split('至');
                    string startTime = sArray[0];
                    string endTime = sArray[1];
                    defaultWhere += " and time BETWEEN '" + startTime + "' and '" + endTime + "'";
                }
            }
            int type=0;
            if (tjType=="CK")
            {
                type = 0;
                title = "接收组织";
            }
            if (tjType=="RK")
            {
                type = 1;
                title = "入库来源";
            }
            if (tjType=="TH")
            {
                type = 2;
                title = "接收组织";
            }
            User user = (User)Session["user"];
            string userId = user.UserId;
            DataSet ds = roleBll.selectRole(userId);
            string roleName = ds.Tables[0].Rows[0]["roleName"].ToString();
            DataSet dataSet = warehousingBll.getKinds(type,defaultWhere);
            sjNum = dataSet.Tables[0].Rows[0]["pz"].ToString();
            sbNum = dataSet.Tables[0].Rows[0]["sl"].ToString();
            total = dataSet.Tables[0].Rows[0]["my"].ToString();
            real = dataSet.Tables[0].Rows[0]["sy"].ToString();
            //string time = "2018-10-10";
            int region = user.ReginId.RegionId;
            if (roleName == "超级管理员")
            {
                isAdmin = true;
            }
            //getData();
            //获取供应商
            dsSupplier = bookbll.selectSupplier();
            //获取组织
            dsRegion = regionBll.select();
            //获取制单员
            dsUser = bookbll.selectZdy();
            //获取来源组织/收货组织
            dsSource = bookbll.selectSource();
            string op = Request["op"];
            if (op == "paging")
            {
                getData();
            }
        }

        //获取数据
        protected string getData()
        {
            currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }

            string bookIsbn = Request["bookIsbn"];
            string bookName = Request["bookName"];
            string supplier = Request["supplier"];
            string time = Request["time"];
            string userName = Request["userName"];
            string userregion = Request["userregion"];
            string resource = Request["resource"];

            User user = (User)Session["user"];
            int regionId = user.ReginId.RegionId;
            string tjType = Request["type"];
            string where = "";
            int type = 0;
            if (tjType == "CK")
            {
                type = 0;
            }
            if (tjType == "RK")
            {
                type = 1;
            }
            if (tjType == "TH")
            {
                type = 2;
            }
            if ((bookIsbn == "" || bookIsbn == null) && (bookName == "" || bookName == null) && (supplier == "" || supplier == null) && (time == "" || time == null) && (userName == "" || userName == null) && (userregion == "" || userregion == null) && (resource == "" || resource == null))
            {
                string tjRegion = Request["region"];
                string tjuserName = Request["userName"];
                string tjHeadId = Request["singleHeadId"];
                string tjtime = Request["time"];
                if (tjRegion != "null" && tjRegion != "" && tjRegion != null)
                {
                    where += " and regionName='" + tjRegion + "'";
                }
                if (tjuserName != "null" && tjuserName != "" && tjuserName != null)
                {
                    where += " and userName='" + tjuserName + "'";
                }
                if (tjHeadId != "null" && tjHeadId != "" && tjHeadId != null)
                {
                    where += " and singleHeadId='" + tjHeadId + "'";
                }
                if (tjtime != "null" && tjtime != "" && tjtime != null)
                {
                    string[] sArray = tjtime.Split('至');
                    string startTime = sArray[0];
                    string endTime = sArray[1];
                    where += " and time BETWEEN '" + startTime + "' and '" + endTime + "'";
                }
            }
            else
            {
                if (bookIsbn != null && bookIsbn != "")
                {
                    where += " and isbn='" + bookIsbn + "'";
                }
                if (bookName != null && bookName != "")
                {
                    where += " and bookName like '%" + bookName + "%'";
                }
                if (supplier != null && supplier != "")
                {
                    where += " and supplier='" + supplier + "'";
                }
                if (time != null && time != "")
                {
                    string[] sArray = time.Split('至');
                    string startTime = sArray[0];
                    string endTime = sArray[1];
                    where += " and time BETWEEN '" + startTime + "' and '" + endTime + "'";
                }
                //if (time != null && time != "")
                //{
                //    where += " and time like '" + time + "%'";
                //}
                if (userName != null && userName != "")
                {
                    where += " and userName='" + userName + "'";
                }
                if (userregion != null && userregion != "")
                {
                    where += " and userRegion='" + userregion + "'";
                }
                if (resource != null && resource != "")
                {
                    where += " and regionName='" + resource + "'";
                }
                currentPage = Convert.ToInt32(Request["page"]);
                if (currentPage == 0)
                {
                    currentPage = 1;
                }
            }
            string userId = user.UserId;
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "v_stockstatistics";
            tbd.OrderBy = "id";
            if (isAdmin)
            {
                tbd.StrColumnlist = "id,type,userName,bookName,ISBN,bookNum,sum(number) as number,sum(totalPrice) as totalPrice ,sum(realPrice) as realPrice,regionName,supplier,time,userRegion";
                tbd.StrWhere = "type=" + type + where + " group by userName,bookName,ISBN,bookNum,regionName,userRegion";
            }
            else
            {
                tbd.StrColumnlist = "id,type,userName,bookName,ISBN,bookNum,sum(number) as number,sum(totalPrice) as totalPrice ,sum(realPrice) as realPrice,regionName,supplier,time";
                tbd.StrWhere = "regionId=" + regionId + where + " and type=" + type + " group by userName,bookName,ISBN,bookNum,regionName";
            }
            tbd.IntPageSize = pageSize;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            DataSet ds = bookbll.selectBypage(tbd, out totalCount, out intPageCount);
            //获取供应商
            //dsSupplier = bookbll.selectSupplier();
            //获取组织
            //dsRegion = regionBll.select();
            //获取制单员
            //dsUser = bookbll.selectZdy();
            //获取来源组织/收货组织
            //dsSource = bookbll.selectSource();
            StringBuilder sb = new StringBuilder();
            int j = ds.Tables[0].Rows.Count;
            for (int i = 0; i < j; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                sb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + dr["bookNum"].ToString() + "</td >");
                sb.Append("<td>" + dr["ISBN"].ToString() + "</td >");
                sb.Append("<td>" + dr["bookName"].ToString() + "</td >");
                sb.Append("<td>" + dr["supplier"].ToString() + "</td >");
                sb.Append("<td>" + dr["number"].ToString() + "</td >");
                sb.Append("<td>" + dr["totalPrice"].ToString() + "</td >");
                sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["regionName"].ToString() + "</td>");
                sb.Append("<td>" + dr["userName"].ToString() + "</td>");
                if (isAdmin)
                {
                    sb.Append("<td>" + dr["userRegion"].ToString() + "</td>");
                }
                sb.Append("<td>" + dr["time"].ToString() + "</td></tr>");
            }
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(sb.ToString() + "|:" + sjNum + "|:" + sbNum + "|:" + total + "|:" + real);
                Response.End();
            }
            return sb.ToString();
        }
    }
}