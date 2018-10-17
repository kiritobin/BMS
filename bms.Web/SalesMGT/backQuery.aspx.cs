using System;
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
                //string bookNum = Request["bookNum"];
                string ISBN = Request["ISBN"];
                string bookNum = Request["bookNO"];
                int bookCount = smBll.getBookCount(bookNum);
                if (bookNum == "" || bookNum == null)
                {
                    bookds = bookBll.SelectByIsbn(ISBN);
                    if (bookds != null && bookds.Tables[0].Rows.Count > 0)
                    {
                        //如果有两条及两条以上显示表格
                        if (bookds.Tables[0].Rows.Count > 1)
                        {
                            getbook();
                        }
                        Response.Write(GetData());
                        Response.End();
                    }
                    else
                    {
                        Response.Write("暂无此数据");
                        Response.End();
                    }
                }
                else if (bookCount == 0)
                {
                    Response.Write("销售单据中无此数据");
                    Response.End();
                }
                else
                {
                    Response.Write(GetData());
                    Response.End();
                }
            }
            if (op == "add")
            {
                addSalemon();
            }
            //保存单据
            if (op == "sure")
            {
                //SellOffHead sell = new SellOffHead();
                //sell.SellOffHeadId = Session["sellId"].ToString();
                //sell.State = 1;
                //Result result = shBll.Update(sell);
                //if (result == Result.更新成功)
                //{
                //    Response.Write("更新成功");
                //    Response.End();
                //}
                //else
                //{
                //    Response.Write("保存失败");
                //    Response.End();
                //}
                string sellId = Session["sellId"].ToString();
                int row = smBll.GetCount(sellId);
                if (row > 0)
                {
                    string result = updateSellHead();
                    Session["type"] = "search";
                    if (result == "更新成功")
                    {
                        Response.Write("更新成功");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("保存失败");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("该单据没有任何数据，无法保存");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 带输入框的tr列表
        /// </summary>
        /// <returns></returns>
        public String WriteBook()
        {
            string bookNum = "";
            string ISBN = "";
            double unitPrice = 0;
            string isbn = Request["ISBN"];
            bookNum = Request["bookNO"];
            if (isbn != "" || isbn != null)
            {
                ISBN = isbn;
            }
            if (bookNum == "" || bookNum == null)
            {
                if (bookds != null)
                {
                    bookNum = bookds.Tables[0].Rows[0]["bookNum"].ToString();//书号
                    BookBasicData book = new BookBasicData();
                    book = bookBll.SelectById(bookNum);
                    unitPrice = book.Price;//定价
                }
            }
            else
            {
                unitPrice = double.Parse(Request["price"]);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr calss='first'>");
            sb.Append("<td>" + "</td>");
            sb.Append("<td>" + "<input type='text' id='inputISBN' class='textareaISBN' autofocus='autofocus' value='" + ISBN + "' />" + "</td>");
            sb.Append("<td>" + bookNum + "</td>");
            sb.Append("<td>" + unitPrice + "</td>");
            sb.Append("<td>" + "<input type='text' id='inputCount' class='textareaCount' />" + "</td>");
            sb.Append("<td>" + "</td>");
            sb.Append("<td>" + "</td>");
            sb.Append("<td>" + "</td>");
            sb.Append("</tr>");
            //Response.Write(sb.ToString());
            //Response.End();
            return sb.ToString();
        }
        /// <summary>
        /// 添加销退单体
        /// </summary>
        public void addSalemon()
        {
            string sellId = Session["sellId"].ToString();
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
                book = bookBll.SelectById(bookNo);
                double unitPrice = book.Price;//定价
                                              //double discount = double.Parse(Request["discount"]);//实际折扣

                //获取默认折扣
                DataSet stds = smBll.getSaleTask(sellId);
                string stId = Session["saleId"].ToString();
                DataSet seds = smBll.getDisCount(stId);
                string dc = seds.Tables[0].Rows[0]["defaultDiscount"].ToString();
                double discount = double.Parse(dc);//默认折扣

                int count = int.Parse(Request["count"]);//数量
                double totalPrice = unitPrice * count;//码洋
                double d = totalPrice * (discount/100);
                double realPrice = Math.Round(d, 2);//实洋
                string headId = Session["sellId"].ToString();//单头Id
                int moNum = smBll.GetCount(headId);
                int smId;
                smId = moNum + 1;//单体Id
                DateTime time = DateTime.Now;

                //获取默认折扣
                //DataSet stds = smBll.getSaleTask(sellId);
                //string stId = stds.Tables[0].Rows[0]["saleTaskId"].ToString();//销售任务Id
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
                    if (row == Result.添加成功)//先添加销退体
                    {
                        string update = updateSellHead();
                        if (update == "更新成功")//后更新销退单头信息
                        {
                            string stock = insertStock();
                            if (stock == "更新成功")//最后写入库存
                            {
                                Response.Write("添加成功");
                                Response.End();
                            }
                            else
                            {
                                Response.Write("写入库存失败");
                                Response.End();
                            }
                        }
                        else
                        {
                            Response.Write("更新单头信息失败");
                            Response.End();
                        }
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
        public string insertStock()
        {
            string sellId = Session["sellId"].ToString();//销退单头Id
            DataSet sellds = shBll.Select(sellId);
            string regionId = sellds.Tables[0].Rows[0]["regionId"].ToString();//地区Id
            //DataSet gds = gbll.Select(int.Parse(regionId));
            //string gid = gds.Tables[0].Rows[0]["goodsShelvesId"].ToString();
            int newstockNum = int.Parse(Request["count"]);//写入的库存量
            string bookNo = Request["bookNum"];//获取书号
            if (bookNo == null || bookNo == "")
            {
                bookNo = bookds.Tables[0].Rows[0]["bookNum"].ToString();//书号
            }
            DataSet stockDs = stbll.SelectByBookNum(bookNo, int.Parse(regionId));
            string shelvesId = stockDs.Tables[0].Rows[0]["goodsShelvesId"].ToString();//获取货架Id
            string oldStockNum = stockDs.Tables[0].Rows[0]["stockNum"].ToString();//原来的库存量
            int stockNum = newstockNum + int.Parse(oldStockNum);
            Result row = stbll.update(stockNum, int.Parse(shelvesId), bookNo);
            if (row == Result.更新成功)
            {
                return "更新成功";
                //Response.Write("更新成功");
                //Response.End();
            }
            else
            {
                return "写入库存失败";
                //Response.Write("写入库存失败");
                //Response.End();
            }
        }
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public String GetData()
        {
            string type = Session["type"].ToString();
            string sellId = Session["sellId"].ToString();
            //获取默认折扣
            DataSet stds = smBll.getSaleTask(sellId);
            string stId = Session["saleId"].ToString();
            DataSet seds = smBll.getDisCount(stId);
            string dc = seds.Tables[0].Rows[0]["defaultDiscount"].ToString();
            double discount = double.Parse(dc);//默认折扣

            ds = smBll.Select(sellId);
            StringBuilder sb = new StringBuilder();
            sb.Append("<thead>");//表头
            sb.Append("<tr>");
            sb.Append("<th>" + "序号" + "</th>");
            sb.Append("<th>" + "ISBN号" + "</th>");
            sb.Append("<th>" + "书号" + "</th>");
            sb.Append("<th>" + "单价" + "</th>");
            sb.Append("<th>" + "数量" + "</th>");
            sb.Append("<th>" + "实际折扣" + "</th>");
            sb.Append("<th>" + "码洋" + "</th>");
            sb.Append("<th>" + "实洋" + "</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");//表体
            if (type != "search")
            {
                sb.Append(WriteBook());
            }
            int dtCount = ds.Tables[0].Rows.Count;
            for (int i = 0; i < dtCount; i++)
            {
                string sohId = ds.Tables[0].Rows[i]["sellOffHead"].ToString();
                string realCount = ds.Tables[0].Rows[i]["realDiscount"].ToString();
                if (sohId == sellId)
                {
                    sb.Append("<tr><td>" + (i + 1)/*ds.Tables[0].Rows[i]["sellOffMonomerId"].ToString()*/ + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["isbn"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["price"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["count"].ToString() + "</td>");
                    sb.Append("<td>" + double.Parse(realCount) + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"].ToString() + "</td>");
                    sb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["dateTime"].ToString() + "</td>");
                    //sb.Append("<td>" + "<button class='btn btn-danger'><i class='fa fa-trash' aria-hidden='true'></i></button>" + "</td>");
                    sb.Append("</tr>");
                }
            }
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            //sb.Append("<input type='hidden' value='" + discount + "' id='sellId' />");
            sb.Append("</tbody>");
            return sb.ToString();
        }
        /// <summary>
        /// 存在多条数据时，用表格展示出来
        /// </summary>
        /// <returns></returns>
        public String getbook()
        {
            string ISBN = Request["ISBN"];
            strbook.Append("<thead class='much'>");//thead
            strbook.Append("<tr>");
            strbook.Append("<th>" + "<div class='pretty inline'><input type = 'radio' name='radio'><label><i class='mdi mdi-check'></i></label></div>" + "</th>");
            strbook.Append("<th>" + "ISBN" + "</th>");
            strbook.Append("<th>" + "书号" + "</th>");
            strbook.Append("<th>" + "书名" + "</th>");
            strbook.Append("<th>" + "单价" + "</th>");
            //strbook.Append("<th>" + "出版社" + "</th>");
            strbook.Append("</tr>");
            strbook.Append("</thead>");//thead
            strbook.Append("<tbody>");//tbody
            for (int i = 0; i < bookds.Tables[0].Rows.Count; i++)
            {
                strbook.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                strbook.Append("<td>" + ISBN + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["price"].ToString() + "</td>");
            }
            strbook.Append("</tbody>");//tbody
            Response.Write(strbook.ToString());
            Response.End();
            return strbook.ToString();
        }
        /// <summary>
        /// 获取单体中相应的品种数量
        /// </summary>
        /// <returns></returns>
        public int getKinds()
        {
            string sellId = Session["sellId"].ToString();//销退单头Id
            int kinds = shBll.getKinds(sellId);
            return kinds;
        }
        /// <summary>
        /// 通过统计单体信息后更新单头
        /// </summary>
        public String updateSellHead()
        {
            string op = Request["op"];

            string sellId = Session["sellId"].ToString();
            DataSet countds = smBll.getAllNum(sellId);
            int allCount = int.Parse(countds.Tables[0].Rows[0]["sum(count)"].ToString());
            double AllPrice = double.Parse(countds.Tables[0].Rows[0]["sum(totalPrice)"].ToString());
            double realPrice = double.Parse(countds.Tables[0].Rows[0]["sum(realPrice)"].ToString());
            int kinds = getKinds();
            SellOffHead sell = new SellOffHead();
            sell.Kinds = kinds;
            sell.SellOffHeadId = sellId;
            sell.Count = allCount;
            sell.TotalPrice = AllPrice;
            sell.RealPrice = realPrice;
            if (op == "sure")
            {
                sell.State = 1;
            }
            else
            {
                sell.State = 0;
            }
            Result result = shBll.Update(sell);
            if (result == Result.更新成功)
            {
                return "更新成功";
                //Response.Write("更新成功");
                //Response.End();
            }
            else
            {
                return "更新单头信息失败";
                //Response.Write("更新单头信息失败");
                //Response.End();
            }
        }
    }
}