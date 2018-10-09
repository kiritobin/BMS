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
        public int totalCount, intPageCount, pageSize = 20, allstockNum = 0;
        public string type, defaultdiscount;
        public DataSet ds, bookds;
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        SaleTaskBll saletaskbll = new SaleTaskBll();
        public StringBuilder strbook = new StringBuilder();
        public int allkinds, allnumber;
        public double alltotalprice, allreadprice;
        string bookISBN, SaleHeadId, saleId;
        double disCount;
        int number;
        long bookNum;
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            type = Session["saleType"].ToString();
            saleId = Session["saleId"].ToString();
            SaleTask task = saletaskbll.selectById(saleId);
            defaultdiscount = ((task.DefaultDiscount) * 100).ToString();
            SaleHeadId = Session["saleheadId"].ToString();

            //更新单头
            allkinds = int.Parse(salemonbll.getkinds(saleId, SaleHeadId).ToString());
            DataSet allds = salemonbll.SelectMonomers(SaleHeadId);
            int j = allds.Tables[0].Rows.Count;
            for (int h = 0; h < j; h++)
            {
                allnumber += Convert.ToInt32(allds.Tables[0].Rows[h]["number"]);
                alltotalprice += Convert.ToInt32(allds.Tables[0].Rows[h]["totalPrice"]);
                allreadprice += Convert.ToInt32(allds.Tables[0].Rows[h]["realPrice"]);
            }
            updateSalehead();
            string op = Request["op"];
            //if (op == "back")
            //{
            //    int count = salemonbll.SelectBySaleHeadId(SaleHeadId);
            //    if (count > 0)
            //    {
            //        Response.Write("已有数据");
            //        Response.End();
            //    }
            //    else
            //    {
            //        Result result = salemonbll.realDelete(SaleHeadId);
            //        if (result == Result.删除成功)
            //        {
            //            Response.Write("删除成功");
            //            Response.End();
            //        }
            //        else
            //        {
            //            Response.Write("删除失败");
            //            Response.End();
            //        }
            //    }
            //}
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
                        bookISBN = Request["ISBN"];
                        disCount = double.Parse(Request["disCount"]) / 100;
                        number = Convert.ToInt32(Request["number"]);
                        bookNum = long.Parse(bookds.Tables[0].Rows[0]["bookNum"].ToString());
                        addsalemon();
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
                bookISBN = Request["ISBN"];
                disCount = double.Parse(Request["disCount"]) / 100;
                number = Convert.ToInt32(Request["number"]);
                bookNum = long.Parse(Request["bookNum"]);
                addsalemon();
            }
            if (op == "success")
            {
                //修改单头状态为2
                Result result = salemonbll.updateHeadstate(saleId, SaleHeadId, 2);
                if (result == Result.更新成功)
                {
                    Response.Write("状态修改成功");
                    Response.End();
                }
                else
                {
                    Response.Write("状态修改失败");
                    Response.End();
                }
            }
        }
        public void addsalemon()
        {
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
                    Response.Write("库存不足");
                    Response.End();
                }
                else
                {
                    SaleHeadBll saleheadbll = new SaleHeadBll();
                    string saletaskId = saleheadbll.SelectTaskByheadId(SaleHeadId);
                    string customerId = saletaskbll.getCustomerId(saletaskId);
                    int AlreadyBought = 0;

                    //判断馆藏
                    LibraryCollectionBll library = new LibraryCollectionBll();
                    Result libresult = library.Selectbook(customerId, bookISBN);
                    if (libresult == Result.记录不存在)
                    {

                        for (int i = 0; i < stockbook.Tables[0].Rows.Count; i++)
                        {
                            int stockNum = Convert.ToInt32(stockbook.Tables[0].Rows[i]["stockNum"]);
                            int goodsId = Convert.ToInt32(stockbook.Tables[0].Rows[i]["goodsShelvesId"]);
                            int countnumber = number;
                            if (countnumber <= stockNum)
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
                                    salemonbll.updateHeadstate(saleId, SaleHeadId, 1);
                                }
                                else
                                {
                                    saleIdmonomerId = count + 1;
                                }

                                //获取已购数
                                int frequency = salemonbll.SelectnumberBysaletask(saletaskId, bookNum.ToString());
                                if (frequency > 0)
                                {
                                    DataSet bookcount = salemonbll.SelectCountBybookNum(saletaskId, bookNum.ToString());
                                    AlreadyBought = Convert.ToInt32(bookcount.Tables[0].Rows[0]["alreadyBought"]);
                                    AlreadyBought += number;
                                }
                                else
                                {
                                    AlreadyBought = number;
                                }
                                int price = Convert.ToInt32(book.Price);
                                int totalPrice = price * number;
                                double realPrice = totalPrice * disCount;
                                DateTime Time = DateTime.Now.ToLocalTime();
                                SaleMonomer newSalemon = new SaleMonomer()
                                {
                                    AlreadyBought = AlreadyBought,
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
                                //更新库存
                                int stockcount = 0;
                                if (number < 0)
                                {
                                    number = Math.Abs(number);
                                    stockcount = stockNum + number;
                                }
                                else
                                {
                                    stockcount = stockNum - number;
                                }
                                Result upresult = stockbll.update(stockcount, goodsId, bookNum);
                                if (upresult == Result.更新成功)
                                {
                                    //添加
                                    Result result = salemonbll.Insert(newSalemon);
                                    if (result == Result.添加成功)
                                    {
                                        Result res = updateSalehead();
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
                                countnumber = countnumber - stockNum;
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
        /// <summary>
        /// 更新单头
        /// </summary>
        /// <returns>返回结果</returns>
        public Result updateSalehead()
        {
            //添加成功 更新单头
            SaleHead salehead = new SaleHead();
            salehead.SaleTaskId = saleId;
            salehead.SaleHeadId = SaleHeadId;
            salehead.KindsNum = allkinds;
            salehead.Number = allnumber;
            salehead.AllTotalPrice = alltotalprice;
            salehead.AllRealPrice = allreadprice;
            Result res = salemonbll.updateHead(salehead);
            return res;
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
            string saletaskId = Session["saleId"].ToString();
            type = Session["saleType"].ToString();
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
            tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,number,realDiscount,realPrice,dateTime,alreadyBought";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            //tb.StrWhere = search;
            tb.StrWhere = search == "" ? "deleteState=0 and saleHeadId=" + "'" + saleheadId + "'" + " and saleTaskId=" + "'" + saletaskId + "'" : search + " and deleteState=0 and saleHeadId=" + "'" + saleheadId + "'" + " and saleTaskId=" + "'" + saletaskId + "'";
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
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["alreadyBought"].ToString() + "</td></tr>");
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