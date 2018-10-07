using bms.Bll;
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
    using Result = Enums.OpResult;
    public partial class salesDetail : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20;
        public string type, defaultdiscount;
        public DataSet ds, bookds;
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        public StringBuilder strbook = new StringBuilder();
        public int allkinds, allnumber;
        public double alltotalprice, allreadprice;
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();

            string saleId = Session["saleId"].ToString();
            SaleTaskBll saletaskbll = new SaleTaskBll();
            SaleTask task = saletaskbll.selectById(saleId);
            defaultdiscount = ((task.DefaultDiscount) * 100).ToString();
            string op = Request["op"];
            if (op == "back")
            {
                string SaleHeadId = Session["saleheadId"].ToString();
                int count = salemonbll.SelectBySaleHeadId(SaleHeadId);
                if (count > 0)
                {
                    Response.Write("已有数据");
                    Response.End();
                }
                else
                {
                    Result result = salemonbll.realDelete(SaleHeadId);
                    if (result == Result.删除成功)
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
            if (op == "search")
            {
                string ISBN = Request["ISBN"];
                BookBasicBll bookbll = new BookBasicBll();
                bookds = bookbll.SelectByIsbn(ISBN);
                if (bookds != null)
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
                    Response.Write("无数据");
                    Response.End();
                }
            }
            if (op == "add")
            {
                string bookISBN = Request["ISBN"];
                double disCount = double.Parse(Request["disCount"]) / 100;
                int number = Convert.ToInt32(Request["number"]);
                long bookNum = long.Parse(Request["bookNum"]);
                int allstockNum = 0;
                StockBll stockbll = new StockBll();
                DataSet stockbook = stockbll.SelectByBookNum(bookNum);
                if (stockbook != null)
                {
                    for (int i = 0; i < stockbook.Tables[0].Rows.Count; i++)
                    {
                        allstockNum += Convert.ToInt32(stockbook.Tables[0].Rows[i]["stockNum"]);
                    }
                    if (number > allstockNum)
                    {
                        Response.Write("库存不足，当前最大库存为：" + allstockNum);
                        Response.End();
                    }
                    else
                    {
                        string saleHead = Session["saleheadId"].ToString();
                        SaleHeadBll saleheadbll = new SaleHeadBll();
                        string saletaskId = saleheadbll.SelectTaskByheadId(saleHead);
                        string customerId = saletaskbll.getCustomerId(saletaskId);
                        LibraryCollectionBll library = new LibraryCollectionBll();
                        Result libresult = library.Selectbook(customerId, bookISBN);
                        if (libresult == Result.记录不存在)
                        {
                            for (int i = 0; i < stockbook.Tables[0].Rows.Count; i++)
                            {
                                int stockNum = Convert.ToInt32(stockbook.Tables[0].Rows[i]["stockNum"]);
                                int goodsId = Convert.ToInt32(stockbook.Tables[0].Rows[i]["goodsShelvesId"]);
                                if (number <= stockNum)
                                {
                                    BookBasicBll Bookbll = new BookBasicBll();
                                    BookBasicData book = new BookBasicData();
                                    book = Bookbll.SelectById(bookNum);
                                    string saleHeadId = Session["saleheadId"].ToString();
                                    int saleIdmonomerId;
                                    int count = salemonbll.SelectBySaleHeadId(saleHeadId);
                                    if (count == 0)
                                    {
                                        saleIdmonomerId = 1;
                                    }
                                    else
                                    {
                                        saleIdmonomerId = count + 1;
                                    }
                                    int price = Convert.ToInt32(book.Price);
                                    int totalPrice = price * number;
                                    double realPrice = totalPrice * disCount;
                                    DateTime Time = DateTime.Now.ToLocalTime();
                                    SaleMonomer newSalemon = new SaleMonomer()
                                    {
                                        SaleIdMonomerId = saleIdmonomerId,
                                        BookNum = bookNum,
                                        ISBN1 = bookISBN,
                                        SaleHeadId = saleHeadId,
                                        Number = number,
                                        UnitPrice = price,
                                        TotalPrice = totalPrice,
                                        RealPrice = realPrice,
                                        RealDiscount = disCount,
                                        Datetime = Time
                                    };
                                    int stockcount = stockNum - number;
                                    Result upresult = stockbll.update(stockcount, goodsId, bookNum);
                                    if (upresult == Result.更新成功)
                                    {
                                        Result result = salemonbll.Insert(newSalemon);
                                        if (result == Result.添加成功)
                                        {
                                            allkinds = salemonbll.SelectBySaleHeadId(saleHeadId);
                                            DataSet allds = salemonbll.SelectMonomers(saleHeadId);
                                            int j = allds.Tables[0].Rows.Count;
                                            for (int h = 0; h < j; h++)
                                            {
                                                string hh = allds.Tables[0].Rows[h]["number"].ToString();
                                                allnumber += Convert.ToInt32(allds.Tables[0].Rows[h]["number"]);
                                                alltotalprice += Convert.ToInt32(allds.Tables[0].Rows[h]["totalPrice"]);
                                                allreadprice += Convert.ToInt32(allds.Tables[0].Rows[h]["realPrice"]);
                                            }
                                            SaleHead salehead = new SaleHead();
                                            salehead.SaleHeadId = saleHeadId;
                                            salehead.KindsNum = allkinds;
                                            salehead.Number = allnumber;
                                            salehead.AllTotalPrice = alltotalprice;
                                            salehead.AllRealPrice = allreadprice;
                                            Result res = salemonbll.updateHead(salehead);
                                            if (res == Result.更新成功)
                                            {
                                                Response.Write("添加成功");
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
                                    number = number - stockNum;
                                    Result upre = stockbll.update(0, goodsId, bookNum);
                                    if (number == 0)
                                    {
                                        Response.Write("添加成功");
                                        Response.End();
                                    }
                                    if (upre == Result.更新失败)
                                    {
                                        Response.Write("添加失败");
                                        Response.End();
                                    }
                                }
                            }
                        }
                        else
                        {
                            Response.Write("客户馆藏已存在");
                            Response.End();
                        }
                    }
                }
                else
                {
                    Response.Write("无库存");
                    Response.End();
                }

            }
        }
        /// <summary>
        /// 只有一条数据时直接添加
        /// </summary>
        public void addSalemon()
        {
            string bookISBN = Request["ISBN"];
            double disCount = double.Parse(Request["disCount"]) / 100;
            int number = Convert.ToInt32(Request["number"]);
            int allstockNum = 0;
            long bookNum = long.Parse(bookds.Tables[0].Rows[0]["bookNum"].ToString());
            StockBll stockbll = new StockBll();
            DataSet book = stockbll.SelectByBookNum(bookNum);
            if (book != null)
            {
                for (int i = 0; i < book.Tables[0].Rows.Count; i++)
                {
                    allstockNum += Convert.ToInt32(book.Tables[0].Rows[i]["stockNum"]);
                }
                if (number > allstockNum)
                {
                    Response.Write("库存不足，当前最大库存为：" + allstockNum);
                    Response.End();
                }
                else
                {
                    for (int i = 0; i < book.Tables[0].Rows.Count; i++)
                    {
                        int stockNum = Convert.ToInt32(book.Tables[0].Rows[i]["stockNum"]);
                        int goodsId = Convert.ToInt32(book.Tables[0].Rows[i]["goodsShelvesId"]);
                        if (number <= stockNum)
                        {
                            BookBasicBll Bookbll = new BookBasicBll();
                            BookBasicData book1 = new BookBasicData();
                            book1 = Bookbll.SelectById(bookNum);
                            string saleHeadId = Session["saleheadId"].ToString();
                            int saleIdmonomerId;
                            int count = salemonbll.SelectBySaleHeadId(saleHeadId);
                            if (count == 0)
                            {
                                saleIdmonomerId = 1;
                            }
                            else
                            {
                                saleIdmonomerId = count + 1;
                            }
                            int price = Convert.ToInt32(book1.Price);
                            int totalPrice = price * number;
                            double realPrice = totalPrice * disCount;
                            DateTime Time = DateTime.Now.ToLocalTime();
                            SaleMonomer newSalemon = new SaleMonomer()
                            {
                                SaleIdMonomerId = saleIdmonomerId,
                                BookNum = bookNum,
                                ISBN1 = bookISBN,
                                SaleHeadId = saleHeadId,
                                Number = number,
                                UnitPrice = price,
                                TotalPrice = totalPrice,
                                RealPrice = realPrice,
                                RealDiscount = disCount,
                                Datetime = Time
                            };
                            int stockcount = stockNum - number;
                            Result upresult = stockbll.update(stockcount, goodsId, bookNum);
                            if (upresult == Result.更新成功)
                            {
                                Result result = salemonbll.Insert(newSalemon);
                                if (result == Result.添加成功)
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
                        else
                        {
                            number = number - stockNum;
                            Result upre = stockbll.update(0, goodsId, bookNum);
                            if (number == 0)
                            {
                                Response.Write("添加成功");
                                Response.End();
                            }
                            if (upre == Result.更新失败)
                            {
                                Response.Write("添加失败");
                                Response.End();
                            }
                        }
                    }

                }

            }
            else
            {
                Response.Write("无库存");
                Response.End();
            }
        }
        /// <summary>
        /// 查询到多条数据时展示数据
        /// </summary>
        /// <returns></returns>
        public string getbook()
        {
            strbook.Append("<thead>");
            strbook.Append("<tr>");
            strbook.Append("<th>" + "<div class='pretty inline'><input type = 'radio' name='radio'><label><i class='mdi mdi-check'></i></label></div>" + "</th>");
            strbook.Append("<th>" + "书号" + "</th>");
            strbook.Append("<th>" + "ISBN" + "</th>");
            strbook.Append("<th>" + "书名" + "</th>");
            strbook.Append("<th>" + "单价" + "</th>");
            strbook.Append("<th>" + "出版社" + "</th>");
            strbook.Append("</tr>");
            strbook.Append("</thead>");
            strbook.Append("<tbody>");
            for (int i = 0; i < bookds.Tables[0].Rows.Count; i++)
            {
                strbook.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["price"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["supplier"].ToString() + "</td></tr>");
            }
            strbook.Append("</tbody>");
            Response.Write(strbook.ToString());
            Response.End();
            return strbook.ToString();
        }
        public string getData()
        {
            string saleheadId = Session["saleheadId"].ToString();
            type = Session["saleType"].ToString();
            //string saleId = Session["saleId"].ToString();
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string bookName = Request["bookName"];
            string ISBN = Request["ISBN"];
            string search;
            if ((ISBN == "" || ISBN == null) && (bookName == "" || bookName == null))
            {
                search = "";
            }
            else if ((bookName != null || bookName != "") && (ISBN == "" || ISBN == null))
            {

                search = String.Format("bookName='{0}'", bookName);
            }
            else if ((bookName == null || bookName == "") && (ISBN != "" || ISBN != null))
            {
                search = String.Format("ISBN='{0}'", ISBN);
            }
            else
            {
                search = String.Format("ISBN='{0}' and bookName='{1}'", ISBN, bookName);
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SaleMonomer";
            tb.OrderBy = "bookNum";
            tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,number,realDiscount,realPrice,dateTime";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            //tb.StrWhere = search;
            tb.StrWhere = search == "" ? "deleteState=0 and saleHeadId=" + "'" + saleheadId + "'" : search + " and deleteState=0 and saleHeadId=" + "'" + saleheadId + "'";
            //获取展示的客户数据
            ds = salemonbll.selectBypage(tb, out totalCount, out intPageCount);
            //生成table
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["number"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td></tr>");
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