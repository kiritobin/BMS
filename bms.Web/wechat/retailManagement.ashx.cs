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
    /// retailManagement 的摘要说明
    /// </summary>
    public class retailManagement : IHttpHandler
    {
        public int totalCount, intPageCount, pageSize = 15;

        public void ProcessRequest(HttpContext context)
        {
            string op = context.Request["op"];
            if (op=="load")
            {
                Load(context);
            }
        }
        private void Load(HttpContext context)
        {
            Page page = new Page();
            RetailBll retailbll = new RetailBll();
            //获取分页数据
            int currentPage = Convert.ToInt32(context.Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "T_RetailHead";
            tb.OrderBy = "retailHeadId";
            tb.StrColumnlist = "retailHeadId,kindsNum,number,allTotalPrice,allRealPrice,payment";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = "deleteState=0 and (state=0 or state=1) and retailHeadId='LS20181026000001'";
            //获取展示的客户数据
            DataSet ds = retailbll.selectBypage(tb, out totalCount, out intPageCount);
            int aaa = ds.Tables[0].Rows.Count;
            string data = JsonHelper.ToJson(ds.Tables[0], "retail");
            page.data = data;
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