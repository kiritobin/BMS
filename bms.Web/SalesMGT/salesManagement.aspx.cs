﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.SalesMGT
{
    // using Result = Enums.OpResult;
    using Bll;
    using Model;
    using System.Data;
    using System.Text;

    public partial class salesManagement : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20;
        DataSet ds;
        SaleHeadBll saleheadbll = new SaleHeadBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            string op = Request["op"];
            if (op == "addDetail")
            {
                string saleheadId = Request["ID"];
                Session["saleheadId"] = saleheadId;
                Session["saleType"] = "look";
                Response.Write("成功");
                Response.End();
            }
            if (op == "look")
            {
                string saleheadId = Request["ID"];
                Session["saleheadId"] = saleheadId;
                Session["saleType"] = "addsale";
                Response.Write("成功");
                Response.End();
            }
        }
        public string getData()
        {
            string saleId = Session["saleId"].ToString();
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string saleTaskId = Request["saleTaskId"];
            string regionName = Request["regionName"];
            string userName = Request["userName"];
            string search = "";
            if ((saleTaskId == "" || saleTaskId == null) && (regionName == null || regionName == "") && (userName == null || userName == ""))
            {
                search = "";
            }
            else if ((saleTaskId != "" && saleTaskId != null) && (regionName == null || regionName == "") && (userName == null || userName == ""))
            {
                search = String.Format(" saleTaskId like '%{0}%'", saleTaskId);
            }
            else if ((saleTaskId == "" || saleTaskId == null) && (regionName != "" && regionName != null) && (userName == null || userName == ""))
            {
                search = "regionName=" + regionName;
            }
            else if ((saleTaskId == "" || saleTaskId == null) && (userName != "" && userName != null) && (regionName == null || regionName == ""))
            {
                search = "userName='" + userName + "'";
            }
            else if ((saleTaskId == "" || saleTaskId == null) && (userName != "" && userName != null) && (regionName != null && regionName != ""))
            {
                search = "regionName='" + regionName + "' and userName='" + userName + "'";
            }
            else if ((saleTaskId != "" && saleTaskId != null) && (regionName != null && regionName != "") && (userName == null || userName == ""))
            {
                search = String.Format(" saleTaskId like '%{0}%' and regionName = '{1}'", saleTaskId, regionName);
            }
            else if ((saleTaskId != "" && saleTaskId != null) && (regionName == null || regionName == "") && (userName != null && userName != ""))
            {
                search = String.Format(" saleTaskId like '%{0}%' and userName='{1}'", saleTaskId, userName);
            }
            else
            {
                search = String.Format(" saleTaskId like '%{0}%' and regionName = '{1}' and userName='{2}'", saleTaskId, regionName, userName);
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SaleHead";
            tb.OrderBy = "saleHeadId";
            tb.StrColumnlist = "saleHeadId,saleTaskId,kindsNum,number,allTotalPrice,allRealPrice,userName,regionName,dateTime";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = search == "" ? "deleteState=0 and saleTaskId=" + "'" + saleId + "'" : search + " and deleteState = 0 and saleTaskId=" + "'" + saleId + "'";
            //获取展示的客户数据
            ds = saleheadbll.selectBypage(tb, out totalCount, out intPageCount);
            //获取客户下拉数据
            //customerds = custBll.select();
            //生成table
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<td>" + ds.Tables[0].Rows[i]["saleHeadId"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["saleTaskId"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["userName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["kindsNum"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allTotalPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allRealPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["dateTime"].ToString() + "</td>");
                strb.Append("<td>" + "<button class='btn btn-success btn-sm add'><i class='fa fa-plus fa-lg'></i></button>");
                strb.Append("<button class='btn btn-info btn-sm look'><i class='fa fa-search'></i></button>");
                strb.Append("<button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button>" + " </td></tr>");
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
        }
    }
}