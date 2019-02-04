using bms.Bll;
using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace bms.Web.wechat
{
    /// <summary>
    /// monomersdetail 的摘要说明
    /// </summary>
    public class monomersdetail : IHttpHandler
    {
        public int totalCount, intPageCount, pageSize = 15;
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        string teamtype;
        public void ProcessRequest(HttpContext context)
        {
            string op = context.Request["op"];
            teamtype = context.Request["opreationType"];
            if (op == "load")
            {
                Load(context);
            }
            if (op == "search")
            {
                Load(context);
            }
        }

        //加载获取数据
        private void Load(HttpContext context)
        {
            string saleheadId = context.Request["saleheadID"];
            string saletaskId = context.Request["saletaskID"];
            string opeartion = context.Request["opeartion"];


            int currentPage = Convert.ToInt32(context.Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            if (teamtype == "team")
            {
                tb.StrTable = "(select * FROM v_salemonomer ORDER BY dateTime desc) table1";
            }
            else
            {
                tb.StrTable = "(select * FROM v_persalemonomer ORDER BY dateTime desc) table1";
            }


            tb.OrderBy = "dateTime desc";
            tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,realDiscount,sum(number) as allnumber ,sum(totalPrice) as alltotalPrice,userName,customerName,regionName,max(dateTime) as dateTime";
            //tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,number,realDiscount,realPrice,dateTime,alreadyBought";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            if (opeartion != null && opeartion != "")
            {
                string condition = " and (bookName like '%" + opeartion + "%' or ISBN like'%" + opeartion + "%')";
                tb.StrWhere = "deleteState=0 and saleTaskId='" + saletaskId + "' and saleHeadId='" + saleheadId + "' " + condition + " group by bookNum,bookName,ISBN,unitPrice HAVING allnumber!=0";
            }
            else
            {
                tb.StrWhere = "deleteState=0 and saleTaskId='" + saletaskId + "' and saleHeadId='" + saleheadId + "' group by bookNum,bookName,ISBN,unitPrice HAVING allnumber!=0";
            }

            DataSet summaryds;
            if (teamtype == "team")
            {
                summaryds = salemonbll.wechatPerSummary(tb.StrWhere, 1);
            }
            else
            {
                summaryds = salemonbll.wechatPerSummary(tb.StrWhere, 3);
            }


            DataSet ds = salemonbll.selectBypage(tb, out totalCount, out intPageCount);

            DataTable dt = new DataTable();
            dt.Columns.Add("bookNum", typeof(string));
            dt.Columns.Add("number", typeof(Int32));
            dt.Columns.Add("bookName", typeof(string));
            dt.Columns.Add("allnumber", typeof(long));
            dt.Columns.Add("unitPrice", typeof(double));
            dt.Columns.Add("alltotalPrice", typeof(double));
            //(i + 1 + ((currentPage - 1) * pageSize))
            int count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                int number = (i + 1 + ((currentPage - 1) * pageSize));
                dt.Rows.Add(ds.Tables[0].Rows[i]["bookNum"].ToString(), Convert.ToInt32(number), ds.Tables[0].Rows[i]["bookName"].ToString(), Convert.ToInt64(ds.Tables[0].Rows[i]["allnumber"].ToString()), Convert.ToDouble(ds.Tables[0].Rows[i]["unitPrice"].ToString()), Convert.ToDouble(ds.Tables[0].Rows[i]["alltotalPrice"].ToString()));
            }
            Page page = new Page();

            page.data = JsonHelper.ToJson(dt, "detail");
            page.summarydata = JsonHelper.ToJson(summaryds.Tables[0], "summar");
            page.currentPage = currentPage;
            page.totalCount = totalCount;
            page.intPageCount = intPageCount;
            string json = JsonHelper.JsonSerializerBySingleData(page);
            context.Response.Write(json);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}