﻿using bms.Bll;
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
    public partial class checkStock : System.Web.UI.Page
    {
        public DataTable putDt;
        public DataSet ds, dsPer;
        public string putId, putOperator, putCount, putRegionName, putTotalPrice, putRealPrice, putTime, HeadId,userName,regionName;
        public int totalCount, intPageCount, pageSize = 20;
        RoleBll roleBll = new RoleBll();
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail, isAdmin, funcBookStock;
        WarehousingBll warehousingBll = new WarehousingBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HeadId = Request.QueryString["returnId"];
                if (HeadId == null || HeadId == "")
                {
                    HeadId = Session["HeadId"].ToString();
                }
                else
                {
                    Session["HeadId"] = HeadId;
                }
            }
            permission();
            //getData();
            string op = Request["op"];
            if (op == "paging")
            {
                getData();
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
            string exportOp = Request.QueryString["op"];
            if (exportOp == "export")
            {
                export();
            }
            putDt = warehousingBll.SelectSingleHead(HeadId);
            int count = putDt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                putId = putDt.Rows[i]["singleHeadId"].ToString();
                putOperator = putDt.Rows[i]["userName"].ToString();
                putCount = putDt.Rows[i]["allBillCount"].ToString();
                putRegionName = putDt.Rows[i]["regionName"].ToString();
                putTotalPrice = putDt.Rows[i]["allTotalPrice"].ToString();
                putRealPrice = putDt.Rows[i]["allRealPrice"].ToString();
                putTime = Convert.ToDateTime(putDt.Rows[i]["time"]).ToString("yyyy年MM月dd日");
            }
            if (op== "print")
            {
                //string HeadId = Request.QueryString["returnId"];
                StringBuilder sb = new StringBuilder();
                SaleMonomerBll saleMonomerBll = new SaleMonomerBll();
                DataSet dataSet = saleMonomerBll.checkStock(HeadId);
                DataRowCollection drc = dataSet.Tables[0].Rows;
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    sb.Append("<tr><td>" + (i + 1) + "</td>");
                    sb.Append("<td>" + drc[i]["ISBN"].ToString() + "</td >");
                    sb.Append("<td>" + drc[i]["bookName"].ToString() + "</td>");
                    sb.Append("<td>" + drc[i]["number"].ToString() + "</td>");
                    sb.Append("<td>" + drc[i]["uPrice"].ToString() + "</td >");
                    sb.Append("<td>" + drc[i]["discount"].ToString() + "</td >");
                    sb.Append("<td>" + drc[i]["totalPrice"].ToString() + "</td >");
                    sb.Append("<td>" + drc[i]["realPrice"].ToString() + "</td >");
                    sb.Append("<td>" + drc[i]["shelvesId"].ToString() + "</td ></tr >");
                }
                Response.Write(sb);
                Response.End();
            }
        }

        protected string getData()
        {
            string HeadId = Session["HeadId"].ToString();
            UserBll userBll = new UserBll();
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_Monomer";
            tbd.OrderBy = "time desc";
            tbd.StrColumnlist = "singleHeadId,ISBN,number,uPrice,discount,totalPrice,realPrice,shelvesId,shelvesName,bookName,regionName,time";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "deleteState=0 and singleHeadId='" + HeadId + "'";
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);

            //生成table
            StringBuilder sb = new StringBuilder();
            int count = ds.Tables[0].Rows.Count;
            DataRowCollection drc = ds.Tables[0].Rows;
            for (int i = 0; i < count; i++)
            {
                sb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + drc[i]["ISBN"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["bookName"].ToString() + "</td>");
                sb.Append("<td>" + drc[i]["number"].ToString() + "</td>");
                sb.Append("<td>" + drc[i]["uPrice"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["discount"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["totalPrice"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["realPrice"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["shelvesId"].ToString() + "</td ></tr >");
            }
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            if (count > 0)
            {
                sb.Append("<input type='hidden' value='" + drc[0]["regionName"].ToString() + "' id='sourceRegin' />");
            }
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }
        /// <summary>
        /// 权限控制
        /// </summary>
        protected void permission()
        {
            FunctionBll functionBll = new FunctionBll();
            User user = (User)Session["user"];
            userName = user.UserName;
            regionName = user.ReginId.RegionName;
            Role role = new Role();
            role = user.RoleId;
            int roleId = role.RoleId;
            dsPer = functionBll.SelectByRoleId(roleId);
            string userId = user.UserId;
            DataSet dsRole = roleBll.selectRole(userId);
            string roleName = dsRole.Tables[0].Rows[0]["roleName"].ToString();
            if (roleName == "超级管理员")
            {
                isAdmin = true;
            }
            for (int i = 0; i < dsPer.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 1)
                {
                    funcOrg = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 2)
                {
                    funcRole = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 3)
                {
                    funcUser = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 4)
                {
                    funcGoods = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 5)
                {
                    funcCustom = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 6)
                {
                    funcLibrary = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 7)
                {
                    funcBook = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 8)
                {
                    funcPut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 9)
                {
                    funcOut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 10)
                {
                    funcSale = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 11)
                {
                    funcSaleOff = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 12)
                {
                    funcReturn = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 13)
                {
                    funcSupply = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 14)
                {
                    funcRetail = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 15)
                {
                    funcBookStock = true;
                }
            }
        }

        /// <summary>
        /// //导出列表方法
        /// </summary>
        /// <param name="s_path">文件路径</param>
        public void downloadfile(string s_path)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(s_path);
            HttpContext.Current.Response.ContentType = "application/ms-download";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            HttpContext.Current.Response.WriteFile(file.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.End();
        }
        /// <summary>
        /// 导出
        /// </summary>
        public void export()
        {
            string name = "明细" + HeadId + "-" + DateTime.Now.ToString("yyyyMMdd") + new Random(DateTime.Now.Second).Next(10000);
            DataTable dt = warehousingBll.ExportExcels(HeadId);
            if (dt != null && dt.Rows.Count > 0)
            {
                var path = Server.MapPath("~/download/入库明细导出/" + name + ".xlsx");
                ExcelHelper.x2007.TableToExcelForXLSX(dt, path);
                downloadfile(path);
            }
            else
            {
                Response.Write("<script>alert('没有数据，不能执行导出操作!');</script>");
                Response.End();
            }
        }
    }
}