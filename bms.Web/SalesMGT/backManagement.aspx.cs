﻿using System;
using System.Data;
using bms.Bll;
using bms.Model;
using System.Text;
using System.Web.Security;

namespace bms.Web.SalesMGT
{
    using System.Globalization;
    using Result = Enums.OpResult;
    public partial class backManagement : System.Web.UI.Page
    {
        protected DataSet ds;
        sellOffHeadBll soBll = new sellOffHeadBll();
        UserBll uBll = new UserBll();
        SellOffMonomerBll smBll = new SellOffMonomerBll();
        protected int totalCount;
        protected int intPageCount;
        public DataSet cutds;
        protected double discount;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["saleId"] = "XSRW20181010000001";
            string op = Request["op"];
            getData();
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
            CustomerBll cutBll = new CustomerBll();
            //获取默认折扣
            SaleTaskBll saleBll = new SaleTaskBll();
            cutds = cutBll.select();
            string saleId = Session["saleId"].ToString();
            SaleTask sale = new SaleTask();
            sale = saleBll.selectById(saleId);
            discount = sale.DefaultDiscount;
            if (op == "add")
            {
                Insert();
            }
            if (op == "delete")//删除单头
            {
                Delete();
            }
            if (op == "addMonomer")//跳转到添加销售单体页面
            {
                string sellId = Request["sohId"];
                string state = Request["state"];
                Session["sellId"] = sellId;
                if (state == "已完成")
                {
                    Response.Write("单据已完成，无法进行修改");
                    Response.End();
                }
                else
                {
                    Response.Write("处理中");
                    Response.End();
                }
            }
            if(op== "searchMonomer")
            {
                string sellId = Request["sohId"];
                Session["sellId"] = sellId;
            }
        }
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public String getData()
        {
            //Session["saleId"] = "XSRW20181007000001";
            int pagesize = 20;
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string search = "";
            //string stockId = Request["stockId"];
            string sellId = Request["sellId"];
            string cutomerName = Request["customer"];
            string saleId = Session["saleId"].ToString();
            if ((sellId == "" || sellId == null) && (cutomerName == "" || cutomerName == null))
            {
                search = "saleTaskId='" + saleId + "' and deleteState=0";
            }
            else if (sellId != "" && sellId != null && (cutomerName == "" || cutomerName == null))
            {
                search = "saleTaskId='" + saleId + "' and deleteState=0 and sellOffHeadID=" + "'" + sellId + "'";
            }
            else if ((sellId == "" || sellId == null) && cutomerName != "" && cutomerName != null)
            {
                search = "saleTaskId='" + saleId + "' and deleteState=0 and customerName like " + "'%" + cutomerName + "%'";
            }
            else
            {
                search = "saleTaskId='" + saleId + "' and deleteState=0 and customerName like " + "'%" + cutomerName + "%'" + " and sellOffHeadID=" + "'" + sellId + "'";
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SellOffHead";
            tb.OrderBy = "saleTaskId";
            tb.StrColumnlist = "sellOffHeadID,saleTaskId,kinds,count,totalPrice,realPrice,userName,customerName,makingTime,defaultDiscount,state";
            tb.IntPageSize = pagesize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = search;
            ds = uBll.selectByPage(tb, out totalCount, out intPageCount);
            StringBuilder strb = new StringBuilder();
            int row = smBll.GetCount(sellId);//判断销退单头中是否有单体
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                //strb.Append("<tr><td>" + ds.Tables[0].Rows[i]["saleTaskId"].ToString() + "</td>");
                int state = int.Parse(ds.Tables[0].Rows[i]["state"].ToString());
                strb.Append("<tr>");
                strb.Append("<td class='sellId'>" + ds.Tables[0].Rows[i]["sellOffHeadID"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["userName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</td>");
                strb.Append("<td>" + (state > 0 ? "已完成" : "处理中") + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["kinds"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["count"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["defaultDiscount"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["makingTime"].ToString() + "</td>");
                //strb.Append("<td>" + "<button class='btn btn-success btn-sm btn_add'><i class='fa fa-plus fa-lg'></i></button>" + "<button class='btn btn-info btn-sm search_back'><i class='fa fa-search'></i></button>" + "<button class='btn btn-danger btn-sm btndelete'><i class='fa fa-trash'></i></button>" + "</td></tr>");
                strb.Append("<td>");
                if (state == 0)
                {
                    strb.Append("<button class='btn btn-success btn-sm btn_add'><i class='fa fa-plus fa-lg'></i></button>");
                }
                strb.Append("<button class='btn btn-info btn-sm search_back'><i class='fa fa-search'></i></button>");
                if (row > 0)
                {
                    strb.Append("<button class='btn btn-danger btn-sm btndelete'><i class='fa fa-trash'></i></button>");
                }
                strb.Append("</td>");
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            strb.Append("<input type='hidden' value='" + Session["saleId"].ToString() + "' id='saleTaskId' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
        }
        /// <summary>
        /// 添加销退单头
        /// </summary>
        public void Insert()
        {
            string saleTaskId = Session["saleId"].ToString();
            SaleTaskBll saleBll = new SaleTaskBll();
            SaleTask sale = saleBll.selectById(saleTaskId);
            User user = new User();
            user.UserId = sale.UserId;//用户Id
            string headId;
            string sellId;//单头Id
            sellOffHeadBll sellBll = new sellOffHeadBll();
            DateTime nowTime = DateTime.Now;
            string nowDt = nowTime.ToString("yyyy-MM-dd");
            long count = 0;
            //判断数据库中是否已经有记录
            DataSet backds = soBll.getAllTime();
            if (backds != null && backds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < backds.Tables[0].Rows.Count; i++)
                {
                    string time = backds.Tables[0].Rows[i]["makingTime"].ToString();
                    DateTime dt = Convert.ToDateTime(time);
                    string sqlTime = dt.ToString("yyyy-MM-dd");
                    if (sqlTime == nowDt)
                    {
                        //count += 1;
                        string id = backds.Tables[0].Rows[i]["sellOffHeadID"].ToString();
                        string st1 = id.Substring(2);
                        count = long.Parse(st1);
                        headId = (count + 1).ToString();
                        //生成流水号
                        if (count > 0)
                        {
                            sellId = "XT" + headId;
                            //count += 1;
                            //sellId = "XT" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                            //Session["sell"] = sellId;
                        }
                        else
                        {
                            count = 1;
                            sellId = "XT" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                            //Session["sell"] = sellId;
                        }
                        SaleTask st = new SaleTask()
                        {
                            SaleTaskId = saleTaskId
                        };
                        SellOffHead sell = new SellOffHead()
                        {
                            SellOffHeadId = sellId,
                            SaleTaskId = st,
                            MakingTime = nowTime,
                            User = user
                        };
                        Result row = sellBll.Insert(sell);
                        if (row == Result.添加成功)
                        {
                            Response.Write("添加成功");
                            Response.End();
                        }
                        else
                        {
                            Response.Write("添加失败");
                            Response.End();
                        }
                        break;
                    }
                }
            }
            else
            {
                count = 1;
                sellId = "XT" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                SaleTask st = new SaleTask()
                {
                    SaleTaskId = saleTaskId
                };
                SellOffHead sell = new SellOffHead()
                {
                    SellOffHeadId = sellId,
                    SaleTaskId = st,
                    MakingTime = nowTime,
                    User = user
                };
                Result row = sellBll.Insert(sell);
                if (row == Result.添加成功)
                {
                    Response.Write("添加成功");
                    Response.End();
                }
                else
                {
                    Response.Write("添加失败");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        public void Delete()
        {
            string sellId = Request["sohId"];
            int row = smBll.GetCount(sellId);
            if (row > 0)
            {
                Response.Write("该单据中存在数据，不能删除");
                Response.End();
            }
            else
            {
                Result result = soBll.Delete(sellId);
                if(result == Result.删除成功)
                {
                    Response.Write("删除成功");
                    Response.End();
                }
                else
                {
                    Response.Write("删除失败");
                    Response.End();
                }
            }
        }
    }
}