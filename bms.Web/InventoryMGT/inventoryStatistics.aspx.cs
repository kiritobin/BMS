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

namespace bms.Web.InventoryMGT
{
    public partial class inventoryStatistics : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 20, totalCount, intPageCount, row, funCount;
        RoleBll roleBll = new RoleBll();
        BookBasicBll bookbll = new BookBasicBll();
        WarehousingBll warehousingBll = new WarehousingBll();
        public bool isAdmin;
        protected string sbNum, sjNum, total, real;
        protected void Page_Load(object sender, EventArgs e)
        {
            string tjType = Request.QueryString["type"];
            int type=0;
            if (tjType=="CK")
            {
                type = 0;
            }
            if (tjType=="RK")
            {
                type = 1;
            }
            if (tjType=="TH")
            {
                type = 2;
            }
            User user = (User)Session["user"];
            string userId = user.UserId;
            DataSet ds = roleBll.selectRole(userId);
            string roleName = ds.Tables[0].Rows[0]["roleName"].ToString();
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            DataSet dataSet = warehousingBll.getKinds(time, type);
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
            getData();
        }

        //获取数据
        protected string getData()
        {

            string bookNum = Request["bookNum"];
            string bookName = Request["bookName"];
            string supplier = Request["supplier"];
            string time = Request["time"];
            string userName = Request["userName"];
            string region = Request["region"];
            string resource = Request["resource"];

            User user = (User)Session["user"];
            int regionId = user.ReginId.RegionId;
            string tjType = Request["type"];
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

            currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string userId = user.UserId;
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "T_Monomers AS A,T_SingleHead AS B,T_User AS C ,T_BookBasicData AS D,t_region as E,t_region as F";
            tbd.OrderBy = "A.bookNum";
            if (isAdmin)
            {
                tbd.StrColumnlist = "C.userName,D.bookName,A.ISBN,A.bookNum,sum(A.number) as number,sum(A.totalPrice) as totalPrice ,sum(A.realPrice) as realPrice,E.regionName,D.supplier,B.time,F.regionName as userRegionName";
                tbd.StrWhere = "B.userId=C.userID and A.bookNum=D.bookNum and B.regionId=E.regionId and F.regionId=C.regionId and A.type="+type+" group by C.userName,D.bookName,A.ISBN,A.bookNum,E.regionName";
            }
            else
            {
                tbd.StrColumnlist = "C.userName,D.bookName,A.ISBN,A.bookNum,sum(A.number) as number,sum(A.totalPrice) as totalPrice ,sum(A.realPrice) as realPrice,E.regionName,D.supplier,B.time";
                tbd.StrWhere = "B.userId=C.userID and A.bookNum=D.bookNum and B.regionId=E.regionId and C.regionId=F.regionId and C.regionId="+ regionId + " and A.type="+type+" group by C.userName,D.bookName,A.ISBN,A.bookNum,E.regionName";
            }
            tbd.IntPageSize = pageSize;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            DataSet ds = bookbll.selectBypage(tbd, out totalCount, out intPageCount);

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
                    sb.Append("<td>" + dr["userRegionName"].ToString() + "</td>");
                }
                sb.Append("<td>" + dr["time"].ToString() + "</td></tr>");
            }
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
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