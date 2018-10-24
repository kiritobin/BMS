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

namespace bms.Web.SalesMGT
{
    public partial class searchSalesDetail : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20;
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        SaleHeadBll saleHeadbll = new SaleHeadBll();
        public DataSet ds, headBasicds;
        public string saleheadId, saletaskId, time,userName,customerName;
        public int allkinds, allnumber;
        public double alltotalprice, allreadprice;
        protected void Page_Load(object sender, EventArgs e)
        {
            SaleTaskBll saletaskBll = new SaleTaskBll();
            saletaskId = Session["saleId"].ToString();
            DataSet userds = saletaskBll.getcustomerName(saletaskId);
            customerName = userds.Tables[0].Rows[0]["customerName"].ToString();
            getData();
            getSaleHeadBasic();
            print();
        }
        //获取单头信息
        public void getSaleHeadBasic()
        {
            headBasicds = saleHeadbll.getSaleHeadBasic(saletaskId, saleheadId);
            allkinds = int.Parse(headBasicds.Tables[0].Rows[0]["kindsNum"].ToString());
            allnumber = int.Parse(headBasicds.Tables[0].Rows[0]["number"].ToString());
            alltotalprice = double.Parse(headBasicds.Tables[0].Rows[0]["allTotalPrice"].ToString());
            allreadprice = double.Parse(headBasicds.Tables[0].Rows[0]["allRealPrice"].ToString());
            time = headBasicds.Tables[0].Rows[0]["dateTime"].ToString();
            userName = headBasicds.Tables[0].Rows[0]["userName"].ToString();
        }

        //获取基础数据
        public string getData()
        {
            saleheadId = Session["saleheadId"].ToString();
            saletaskId = Session["saleId"].ToString();

            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SaleMonomer";
            tb.OrderBy = "dateTime";
            tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,realDiscount,sum(number) as allnumber ,sum(realPrice) as allrealPrice,userName,customerName,regionName";
            //tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,number,realDiscount,realPrice,dateTime,alreadyBought";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = "saleTaskId='" + saletaskId + "' and saleHeadId='" + saleheadId + "' group by bookNum,bookName,ISBN,unitPrice,realDiscount";

            // tb.StrWhere = search == "" ? "deleteState=0 and saleHeadId=" + "'" + saleheadId + "'" + " and saleTaskId=" + "'" + saletaskId + "'" : search + " and deleteState=0 and saleHeadId=" + "'" + saleheadId + "'" + " and saleTaskId=" + "'" + saletaskId + "'";
            //获取展示的客户数据
            ds = salemonbll.selectBypage(tb, out totalCount, out intPageCount);
            //生成table
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (int.Parse(ds.Tables[0].Rows[i]["allnumber"].ToString()) != 0 && double.Parse(ds.Tables[0].Rows[i]["allrealPrice"].ToString()) != 0)
                {
                    strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"] + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"] + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"] + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"] + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["allnumber"] + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"] + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["allrealPrice"] + "</td>");
                }
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            strb.Append("<input type='hidden' value=' " + customerName + " ' id='customer' />");
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                strb.Append("<input type='hidden' value=' " + ds.Tables[0].Rows[0]["regionName"] + " ' id='region' />");
            }
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
        }
        //打印
        private void print()
        {
            saleheadId = Session["saleheadId"].ToString();
            saletaskId = Session["saleId"].ToString();
            string op = Request["op"];
            if (op=="print")
            {
                StringBuilder sb = new StringBuilder();
                SellOffMonomerBll sellOffMonomerBll = new SellOffMonomerBll();
                DataSet dataSet = sellOffMonomerBll.searchSalesDetail(saletaskId, saleheadId);
                DataRowCollection drc = dataSet.Tables[0].Rows;
                for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    sb.Append("<tr><td>" + (i + 1) + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"] + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"] + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"] + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"] + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["allnumber"] + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"] + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["allrealPrice"] + "</td></tr>");
                }
                Response.Write(sb);
                Response.End();
            }
        }
    }
}