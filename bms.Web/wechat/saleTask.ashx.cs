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
    /// saleTask 的摘要说明
    /// </summary>
    public class saleTask : IHttpHandler
    {
        public int totalCount, intPageCount, pageSize = 15;
        SaleTaskBll taskbll = new SaleTaskBll();
        public void ProcessRequest(HttpContext context)
        {
            string op = context.Request["op"];
            if (op == "load")
            {
                Load(context);
            }
            if (op == "search")
            {
                Load(context);
            }
        }

        private void Load(HttpContext context)
        {
            Page page = new Page();
            //获取分页数据
            int currentPage = Convert.ToInt32(context.Request["page"]);
            string userId = context.Request["userId"];
            string opeartion = context.Request["opeartion"];


            UserBll userbll = new UserBll();
            DataSet userds = userbll.selectByUserId(userId);
            int regionId = Int32.Parse(userds.Tables[0].Rows[0]["regionId"].ToString());
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SaleTask";
            tb.OrderBy = "saleTaskId";
            tb.StrColumnlist = "saleTaskId,defaultDiscount,defaultCopy,priceLimit,numberLimit,totalPriceLimit,startTime,finishTime,userId,userName,customerName,regionId";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            //tb.StrWhere = "deleteState=0 and (state=0 or state=1) and saleTaskId='" + saleTaskId + "'";
            if (opeartion != null && opeartion != "")
            {
                string condition = " and (saleTaskId like '%" + opeartion + "%' or customerName like'%" + opeartion + "%')";
                tb.StrWhere = "deleteState=0 and regionId='" + regionId + "'" + condition + " and ISNULL(finishTime)";
            }
            else
            {
                tb.StrWhere = "deleteState=0 and regionId='" + regionId + "' and ISNULL(finishTime)";
            }
            //获取展示的客户数据
            DataSet ds = taskbll.selectBypage(tb, out totalCount, out intPageCount);
            string json = JsonHelper.ToJson(ds.Tables[0], "detail");
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