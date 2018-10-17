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
    public partial class salesTaskStatistics : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20;
        public string saletaskId, userName, customerName, startTime, finishTime;
        public int allkinds, allnumber;
        public double alltotalprice, allreadprice;
        SaleTaskBll saletaskbll = new SaleTaskBll();
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            saletaskId = Session["saleId"].ToString();
            getBasic();
        }
        public void getBasic()
        {
            DataSet ds = saletaskbll.getSaleTaskStatistics(saletaskId);
            if (ds != null)
            {
                string number = ds.Tables[0].Rows[0]["number"].ToString();
                if (number == "" || number == null)
                {
                    allnumber = 0;
                    alltotalprice = 0;
                    allreadprice = 0;
                }
                else
                {
                    allnumber = int.Parse(ds.Tables[0].Rows[0]["number"].ToString());
                    alltotalprice = double.Parse(ds.Tables[0].Rows[0]["totalPrice"].ToString());
                    allreadprice = double.Parse(ds.Tables[0].Rows[0]["realPrice"].ToString());
                }

            }
            //统计种数
            allkinds = saletaskbll.getkindsBySaleTaskId(saletaskId);
            DataSet userds = saletaskbll.getcustomerName(saletaskId);
            if (userds != null)
            {
                userName = userds.Tables[0].Rows[0]["userName"].ToString();
                customerName = userds.Tables[0].Rows[0]["customerName"].ToString();
                startTime = userds.Tables[0].Rows[0]["startTime"].ToString();
                finishTime = userds.Tables[0].Rows[0]["finishTime"].ToString();
                if (finishTime == "" || finishTime == null)
                {
                    finishTime = "此销售任务还未结束";
                }
            }
        }
        //获取基础数据
        public string getData()
        {
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SaleMonomer";
            tb.OrderBy = "dateTime";
            tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,realDiscount,sum(number) as allnumber ,sum(realPrice) as allrealPrice";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = "saleTaskId='" + saletaskId + "' group by bookNum,bookName,ISBN,unitPrice,realDiscount";
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
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"] + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"] + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"] + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"] + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"] + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["allnumber"] + "</td>");
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["allrealPrice"] + "</td>");
                    strb.Append("<td>" + 0 + "</td></tr>");
                }
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