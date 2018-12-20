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
    /// addmonomers 的摘要说明
    /// </summary>
    public class addmonomers : IHttpHandler
    {
        public int totalCount, intPageCount, pageSize = 15;
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        loginmsg msg = new loginmsg();
        public void ProcessRequest(HttpContext context)
        {
            string op = context.Request["op"];
            if (op == "load")
            {
                Load(context);
            }
            if (op == "isbn")
            {
                getBook(context);
            }

        }
        private void Load(HttpContext context)
        {
            string saleheadId = context.Request["saleheadID"];
            string saletaskId = context.Request["saletaskID"];
            int currentPage = Convert.ToInt32(context.Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SaleMonomer";
            tb.OrderBy = "dateTime desc";
            tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,realDiscount,sum(number) as allnumber ,sum(realPrice) as allrealPrice,userName,customerName,regionName";
            //tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,number,realDiscount,realPrice,dateTime,alreadyBought";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = "saleTaskId='" + saletaskId + "' and saleHeadId='" + saleheadId + "' group by bookNum,bookName,ISBN,unitPrice HAVING allnumber!=0";
            DataSet summaryds = salemonbll.wechatSummary(tb.StrWhere);
            DataSet ds = salemonbll.selectBypage(tb, out totalCount, out intPageCount);

            DataTable dt = new DataTable();
            dt.Columns.Add("number", typeof(Int32));
            dt.Columns.Add("bookName", typeof(string));
            dt.Columns.Add("allnumber", typeof(long));
            dt.Columns.Add("unitPrice", typeof(double));
            dt.Columns.Add("allrealPrice", typeof(double));
            //(i + 1 + ((currentPage - 1) * pageSize))
            int count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                int number = (i + 1 + ((currentPage - 1) * pageSize));
                dt.Rows.Add(Convert.ToInt32(number), ds.Tables[0].Rows[i]["bookName"].ToString(), Convert.ToInt64(ds.Tables[0].Rows[i]["allnumber"].ToString()), Convert.ToDouble(ds.Tables[0].Rows[i]["unitPrice"].ToString()), Convert.ToDouble(ds.Tables[0].Rows[i]["allrealPrice"].ToString()));
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

        private void getBook(HttpContext context)
        {

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