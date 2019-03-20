using bms.Bll;
using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using static bms.Bll.Enums;

namespace bms.Web.wechat
{

    using Result = Enums.OpResult;
    /// <summary>
    /// headManagement 的摘要说明
    /// </summary>
    public class headManagement : IHttpHandler
    {

        public int totalCount, intPageCount, pageSize = 15;
        SaleHeadBll saleheadlbll = new SaleHeadBll();

        public bool IsReusable
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            string op = context.Request["op"];
            if (op == "load")
            {
                Load(context);
            }
            if (op == "addhead")
            {
                addHead(context);
            }
            if (op == "delhead")
            {
                del(context);
            }
        }

        private void Load(HttpContext context)
        {
            Page page = new Page();
            //获取分页数据
            int currentPage = Convert.ToInt32(context.Request["page"]);
            string saleTaskId = context.Request["saleTaskId"].ToString();
            int userId = Convert.ToInt32(context.Request["userId"].ToString());
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            string type = context.Request["type"];
            if (type == "team")
            {
                tb.StrTable = "t_salehead";
                tb.StrWhere = "deleteState=0 and userId=" + userId + " and state!=3 and saleTaskId='" + saleTaskId + "'";
            }
            else
            {
                tb.StrTable = "t_salehead_copy";
                tb.StrWhere = "deleteState=0 and userId=" + userId + " and state=3 and saleTaskId='" + saleTaskId + "'";
            }
            tb.OrderBy = "saleHeadId";
            tb.StrColumnlist = "saleHeadId,kindsNum,number,allRealPrice";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            //tb.StrWhere = "deleteState=0 and (state=0 or state=1) and saleTaskId='" + saleTaskId + "'";

            //获取展示的客户数据
            DataSet ds = saleheadlbll.selectBypage(tb, out totalCount, out intPageCount);
            string json = JsonHelper.ToJson(ds.Tables[0]);
            context.Response.Write(json);
            context.Response.End();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="context"></param>
        private void addHead(HttpContext context)
        {
            string customeID = context.Request["customID"];
            string saleId = context.Request["saletaskID"];
            UserBll user = new UserBll();
            DataSet userds = user.selectByUserId(customeID);
            int regionId = Convert.ToInt32(userds.Tables[0].Rows[0]["regionId"]);
            SaleHeadBll saleheadbll = new SaleHeadBll();
            SaleHead salehead = new SaleHead();
            string SaleHeadId;
            int count;
            string type = context.Request["type"];
            if (type == "team")
            {
                count = saleheadbll.getCount();
            }
            else
            {
                count = saleheadbll.WeChatcountHead();
            }
            if (count > 0)
            {
                string time;
                if (type == "team")
                {
                    time = saleheadbll.getSaleHeadTime();
                }
                else
                {
                    time = saleheadbll.getPerSaleHeadTime();
                }
                string nowTime = DateTime.Now.ToLocalTime().ToString();
                string equalsTime = DateTime.Now.ToLocalTime().ToString("yyyyMMdd");
                if (time.Equals(equalsTime))
                {
                    string getheadId;
                    nowTime = DateTime.Now.ToString("yyyy-MM-dd");
                    if (type == "team")
                    {
                        getheadId = saleheadbll.getSaleHeadIdByTime(nowTime);
                    }
                    else
                    {
                        getheadId = saleheadbll.getPerSaleHeadIdByTime(nowTime);
                    }

                    if (getheadId == "" || getheadId == null)
                    {
                        count = 1;
                        SaleHeadId = "XS" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                    }
                    else
                    {
                        string js = getheadId.Remove(0, getheadId.Length - 6);
                        count = Convert.ToInt32(js) + 1;
                        SaleHeadId = "XS" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                    }
                }
                else
                {
                    count = 1;
                    SaleHeadId = "XS" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                }
            }
            else
            {
                count = 1;
                SaleHeadId = "XS" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
            }
            salehead.SaleHeadId = SaleHeadId;
            salehead.SaleTaskId = saleId;
            salehead.KindsNum = 0;
            salehead.Number = 0;
            salehead.AllTotalPrice = 0;
            salehead.AllRealPrice = 0;
            salehead.UserId = customeID;
            salehead.RegionId = regionId;
            salehead.DateTime = DateTime.Now.ToLocalTime();
            Result result;
            if (type == "team")
            {
                salehead.State = 0;
                result = saleheadbll.Insert(salehead);
            }
            else
            {
                salehead.State = 3;
                result = saleheadbll.perInsert(salehead);
            }

            if (result == Result.添加成功)
            {
                context.Response.Write("添加成功");
                context.Response.End();
            }
            else
            {
                context.Response.Write("添加失败");
                context.Response.End();
            }
        }

        private void del(HttpContext context)
        {
            string salehead = context.Request["saleheadID"];
            string saleTaskId = context.Request["saletaskID"];
            SaleMonomerBll salemonbll = new SaleMonomerBll();
            string type = context.Request["type"];
            int count;
            if (type == "team")
            {
                count = salemonbll.SelectcountbyHeadID(salehead, saleTaskId);
            }
            else
            {
                count = salemonbll.WeChatSelectcountbyHeadID(salehead, saleTaskId);
            }
            if (count == 0)
            {
                Result result;
                if (type == "team")
                {
                    result = salemonbll.DeleteHead(saleTaskId, salehead, type);
                }
                else
                {
                    result = salemonbll.DeleteHead(saleTaskId, salehead, "");
                }

                if (result == Result.删除成功)
                {
                    context.Response.Write("删除成功");
                    context.Response.End();
                }
                else
                {
                    context.Response.Write("删除失败");
                    context.Response.End();
                }
            }
            else
            {
                context.Response.Write("已有数据不能删除");
                context.Response.End();
            }

        }
    }
}