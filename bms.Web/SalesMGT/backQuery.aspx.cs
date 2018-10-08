﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using bms.Model;
using System.Data;
using System.Text;

namespace bms.Web.SalesMGT
{
    using NPOI.SS.Util;
    using Result = Enums.OpResult;
    public partial class backQuery : System.Web.UI.Page
    {
        public DataSet ds;
        public DataSet bookds;
        SellOffMonomerBll smBll = new SellOffMonomerBll();
        sellOffHeadBll shBll = new sellOffHeadBll();
        protected int totalCount;
        protected int intPageCount;
        public StringBuilder strbook = new StringBuilder();
        public string saleBookNum;
        BookBasicBll bookBll = new BookBasicBll();
        GoodsShelvesBll gbll = new GoodsShelvesBll();
        StockBll stbll = new StockBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            string op = Request["op"];
            GetData();
            if (op == "search")
            {
                string bookNum = Request["bookNum"];
                string ISBN = Request["ISBN"];
                bookds = bookBll.SelectByIsbn(ISBN);
                if (bookds != null)
                {
                    if (bookNum == "" || bookNum == null)
                    {
                        //如果有两条及两条以上显示表格
                        if (bookds.Tables[0].Rows.Count > 1)
                        {
                            getbook();
                        }
                        else
                        {
                            addSalemon();
                        }
                    }
                    else
                    {
                        addSalemon();
                    }
                }
                else
                {
                    Response.Write("暂无此数据");
                    Response.End();
                }
            }
            if (op == "back")
            {
                CheckBack();
            }
        }
        /// <summary>
        /// 添加销退单体
        /// </summary>
        public void addSalemon()
        {
            string isbn = Request["ISBN"];//ISBN
            string bookNo = Request["bookNum"];
            if (bookNo == null || bookNo == "")
            {
                bookNo = bookds.Tables[0].Rows[0]["bookNum"].ToString();//书号
            }
            int bookCount = smBll.getBookCount(bookNo);
            if (bookCount > 0)
            {
                BookBasicData book = new BookBasicData();
                book = bookBll.SelectById(long.Parse(bookNo));
                double unitPrice = book.Price;//定价
                double discount = double.Parse(Request["discount"]);//实际折扣
                int count = int.Parse(Request["count"]);//数量
                double totalPrice = unitPrice * count;//码洋
                double d = totalPrice * discount;
                double realPrice = Math.Round(d, 2);//实洋
                string headId = Session["sell"].ToString();//单头Id
                int moNum = smBll.GetCount(headId);
                int smId;
                smId = moNum + 1;//单体Id
                DateTime time = DateTime.Now;

                string sellId = Session["sell"].ToString();
                //获取默认折扣
                DataSet stds = smBll.getSaleTask(sellId);
                string stId = stds.Tables[0].Rows[0]["saleTaskId"].ToString();//销售任务Id
                DataSet countds = smBll.selctByBookNum(bookNo, stId);
                int num = 0;
                SellOffMonomer sm = new SellOffMonomer()
                {
                    SellOffMonomerId = smId.ToString(),
                    SellOffHeadId = headId,
                    BookNum = long.Parse(bookNo),
                    ISBN1 = isbn,
                    Count = count,
                    TotalPrice = totalPrice,
                    RealPrice = realPrice,
                    Price = unitPrice,
                    Time = time,
                    Discount = discount
                };
                DataSet smcountds = smBll.selecctSm(bookNo, sellId);
                int allcount = 0;
                if (countds != null)//获取销售中的相应的数量
                {
                    for (int i = 0; i < countds.Tables[0].Rows.Count; i++)
                    {
                        num = num + int.Parse(countds.Tables[0].Rows[i]["number"].ToString());
                    }
                }
                if (smcountds != null)//获取销退单体中已有的数量
                {
                    for (int i = 0; i < smcountds.Tables[0].Rows.Count; i++)
                    {
                        allcount = allcount + int.Parse(smcountds.Tables[0].Rows[i]["count"].ToString());
                    }
                }
                if (count > num || (count + allcount) > num)//判断销退数量是否大于销售数量
                {
                    Response.Write("数据过大");
                    Response.End();
                }
                else
                {
                    Result row = smBll.Insert(sm);
                    if (row == Result.添加成功)
                    {
                        insertStock();
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
            else
            {
                Response.Write("销售单中暂无此数据");
                Response.End();
            }
        }
        /// <summary>
        /// 写入库存
        /// </summary>
        public void insertStock()
        {
            string sellId = Session["sell"].ToString();//销退单头Id
            DataSet sellds = shBll.Select(sellId);
            string regionId = sellds.Tables[0].Rows[0]["regionId"].ToString();//地区Id
            DataSet gds = gbll.Select(int.Parse(regionId));
            string gid = gds.Tables[0].Rows[0]["goodsShelvesId"].ToString();
            int stockNum = int.Parse(Request["count"]);
            string bookNo = Request["bookNum"];//获取书号
            if (bookNo == null || bookNo == "")
            {
                bookNo = bookds.Tables[0].Rows[0]["bookNum"].ToString();//书号
            }
            Result row = stbll.update(stockNum, int.Parse(gid), long.Parse(bookNo));
            if(row == Result.更新成功)
            {
                Response.Write("更新成功");
                Response.End();
            }
            else
            {
                Response.Write("更新失败");
                Response.End();
            }
        }
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public String GetData()
        {
            Session["sell"] = "XT20181007000005";
            string sellId = Session["sell"].ToString();
            //获取默认折扣
            DataSet stds = smBll.getSaleTask(sellId);
            string stId = stds.Tables[0].Rows[0]["saleTaskId"].ToString();
            DataSet seds = smBll.getDisCount(stId);
            string dc = seds.Tables[0].Rows[0]["defaultDiscount"].ToString();
            double discount = double.Parse(dc);

            ds = smBll.Select(sellId);
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string sohId = ds.Tables[0].Rows[i]["sellOffHead"].ToString();
                if (sohId == sellId)
                {
                    sb.Append("<tr><td>" + (i+1)/*ds.Tables[0].Rows[i]["sellOffMonomerId"].ToString()*/ + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["isbn"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["price"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["count"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["dateTime"].ToString() + "</td>");
                    //sb.Append("<td>" + "<button class='btn btn-danger'><i class='fa fa-trash' aria-hidden='true'></i></button>" + "</td>");
                    sb.Append("</tr>");
                }
            }
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            sb.Append("<input type='hidden' value='" + discount + "' id='sellId' />");
            sb.Append("</tbody>");
            return sb.ToString();
        }
        /// <summary>
        /// 存在多条数据时，用表格展示出来
        /// </summary>
        /// <returns></returns>
        public String getbook()
        {
            strbook.Append("<thead>");
            strbook.Append("<tr>");
            strbook.Append("<th>" + "<div class='pretty inline'><input type = 'radio' name='radio'><label><i class='mdi mdi-check'></i></label></div>" + "</th>");
            strbook.Append("<th>" + "书号" + "</th>");
            strbook.Append("<th>" + "书名" + "</th>");
            strbook.Append("<th>" + "单价" + "</th>");
            //strbook.Append("<th>" + "出版社" + "</th>");
            strbook.Append("</tr>");
            strbook.Append("</thead>");
            strbook.Append("<tbody>");
            for (int i = 0; i < bookds.Tables[0].Rows.Count; i++)
            {
                strbook.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["price"].ToString() + "</td>");
                //strbook.Append("<td>" + bookds.Tables[0].Rows[i]["supplier"].ToString() + "</td></tr>");
            }
            strbook.Append("</tbody>");
            Response.Write(strbook.ToString());
            Response.End();
            return strbook.ToString();
        }
        /// <summary>
        /// 返回查询
        /// </summary>
        public void CheckBack()
        {
            string sellId = Session["sell"].ToString();
            DataSet backds = smBll.Select(sellId);
            int count = backds.Tables[0].Rows.Count;
            if (count > 0)
            {
                Response.Write("返回");
                Response.End();
            }
            else
            {
                Result row = shBll.Delete(sellId);
                if (row == Result.删除成功)
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