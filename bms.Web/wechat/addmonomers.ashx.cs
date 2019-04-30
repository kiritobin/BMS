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
    using Result = Enums.OpResult;
    /// <summary>
    /// addmonomers 的摘要说明
    /// </summary>
    /// 
    public class addmonomers : IHttpHandler
    {
        public int totalCount, intPageCount, pageSize = 15;
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        BookBasicBll bookbll = new BookBasicBll();
        SaleTaskBll saletaskbll = new SaleTaskBll();
        StockBll stobll = new StockBll();
        string teamtype;
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
            teamtype = context.Request["opreationType"];

            if (op == "load")
            {
                updateSalehead(context);
                Load(context);
            }
            if (op == "isbn")
            {
                getBook(context);
            }
            if (op == "addsale")
            {
                addsale(context);
            }
            if (op == "change")
            {
                changeNum(context);
            }
            if (op == "del")
            {
                del(context);
            }

        }
        /// <summary>
        /// 删除
        /// </summary>
        private void del(HttpContext context)
        {
            string saletaskId = context.Request["saletaskID"];
            string saleheadId = context.Request["saleheadID"];
            string bookNum = context.Request["bookNum"];
            string condition = "saleTaskId='" + saletaskId + "' and saleHeadId='" + saleheadId + "' and bookNum='" + bookNum + "'";
            Result res;
            if (teamtype == "team")
            {
                res = salemonbll.wechatPerDel(condition, 1);
            }
            else
            {
                res = salemonbll.wechatPerDel(condition, 3);
            }


            if (res == Result.删除成功)
            {
                //删除成功更新单头
                updateSalehead(context);

                context.Response.Write("删除成功");
                context.Response.End();
            }
            else
            {
                context.Response.Write("删除失败");
                context.Response.End();
            }

        }

        private void Load(HttpContext context)
        {
            string saleheadId = context.Request["saleheadID"];
            string saletaskId = context.Request["saletaskID"];
            int currentPage = Convert.ToInt32(context.Request["page"]);
            string type = context.Request["type"];
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
            if (type == "repeat")
            {
                string bookNum = context.Request["bookNum"];
                tb.StrWhere = "deleteState=0 and saleTaskId='" + saletaskId + "' and saleHeadId='" + saleheadId + "' and bookNum='" + bookNum + "' group by bookNum,bookName,ISBN,unitPrice HAVING allnumber!=0";
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

        private void getBook(HttpContext context)
        {
            string ISBN = context.Request["ISBN"];
            string type = context.Request["type"];
            DataSet bookds = bookbll.SelectByIsbn(ISBN);
            if (bookds != null)
            {
                string saleId = context.Request["saletaskID"];
                string customerId = saletaskbll.getCustomerId(saleId);
                LibraryCollectionBll library = new LibraryCollectionBll();
                Result libresult = library.Selectbook(customerId, ISBN);
                if (libresult == Result.记录不存在 || type == "continue")
                {

                    SaleTaskBll saletaskbll = new SaleTaskBll();
                    DataSet limtds = saletaskbll.SelectBysaleTaskId(saleId);
                    string copy = limtds.Tables[0].Rows[0]["defaultCopy"].ToString();
                    //如果有两条及两条以上
                    if (bookds.Tables[0].Rows.Count > 1)
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("bookNum", typeof(string));
                        dt.Columns.Add("rownum", typeof(int));
                        dt.Columns.Add("bookName", typeof(string));
                        dt.Columns.Add("unitPrice", typeof(double));
                        dt.Columns.Add("count", typeof(int));
                        dt.Columns.Add("color", typeof(string));
                        for (int i = 0; i < bookds.Tables[0].Rows.Count; i++)
                        {
                            int regionid = saletaskbll.GetregionidBysaleid(saleId);
                            int count = stobll.selectStockNum(bookds.Tables[0].Rows[i]["bookNum"].ToString(), regionid);
                            if (count == 0) continue;
                            dt.Rows.Add(bookds.Tables[0].Rows[i]["bookNum"].ToString(), Convert.ToInt32((i + 1)), bookds.Tables[0].Rows[i]["bookName"].ToString(), Convert.ToDouble(bookds.Tables[0].Rows[i]["price"].ToString()), count, "");
                        }
                        if (dt.Rows.Count == 1)
                        {
                            book book = new book();
                            //bookNum,ISBN,price,author,bookName,supplier
                            int regionid = saletaskbll.GetregionidBysaleid(saleId);
                            int count = stobll.selectStockNum(dt.Rows[0]["bookNum"].ToString(), regionid);
                            if (count != 0)
                            {
                                book.BookNum = bookds.Tables[0].Rows[0]["bookNum"].ToString();
                                book.BookName = bookds.Tables[0].Rows[0]["bookName"].ToString();
                                book.Price = double.Parse(bookds.Tables[0].Rows[0]["price"].ToString());
                                book.count = count;
                                if (copy == "" || copy == null)
                                {
                                    book.number = "0";
                                }
                                else
                                {
                                    book.number = copy;
                                }
                                string json = JsonHelper.JsonSerializerBySingleData(book);
                                context.Response.Write(json);
                                context.Response.End();
                            }
                            else
                            {
                                context.Response.Write("无数据");
                                context.Response.End();
                            }
                        }
                        else if (dt.Rows.Count != 0)
                        {
                            Page page = new Page();
                            if (copy == "" || copy == null)
                            {
                                page.number = "0";
                            }
                            else
                            {
                                page.number = copy;
                            }
                            page.data = JsonHelper.ToJson(dt, "book");
                            page.type = "books";
                            string json = JsonHelper.JsonSerializerBySingleData(page);
                            context.Response.Write(json);
                            context.Response.End();
                        }
                        else
                        {
                            context.Response.Write("无数据");
                            context.Response.End();
                        }

                    }
                    //只有一条数据
                    else
                    {
                        book book = new book();
                        //bookNum,ISBN,price,author,bookName,supplier
                        int regionid = saletaskbll.GetregionidBysaleid(saleId);
                        int count = stobll.selectStockNum(bookds.Tables[0].Rows[0]["bookNum"].ToString(), regionid);
                        if (count != 0)
                        {
                            book.BookNum = bookds.Tables[0].Rows[0]["bookNum"].ToString();
                            book.BookName = bookds.Tables[0].Rows[0]["bookName"].ToString();
                            book.Price = double.Parse(bookds.Tables[0].Rows[0]["price"].ToString());
                            book.count = count;
                            if (copy == "" || copy == null)
                            {
                                book.number = "0";
                            }
                            else
                            {
                                book.number = copy;
                            }
                            string json = JsonHelper.JsonSerializerBySingleData(book);
                            context.Response.Write(json);
                            context.Response.End();
                        }
                        else
                        {
                            context.Response.Write("无数据");
                            context.Response.End();
                        }

                    }
                }
                else
                {
                    context.Response.Write("馆藏存在");
                    context.Response.End();
                }

            }
            else
            {
                context.Response.Write("无数据");
                context.Response.End();
            }
        }
        /// <summary>
        /// 判断此书在此销售任务中是否已购买？
        /// </summary>
        /// <param name="saletaskid">销售任务id</param>
        /// <param name="booknum">书号</param>
        public Boolean isexites(string saletaskid, string booknum)
        {
            return salemonbll.isexites(saletaskid, booknum);
        }
        private void addsale(HttpContext context)
        {
            SaleHeadBll saleheadbll = new SaleHeadBll();
            string SaleHeadId = context.Request["saleheadID"];
            string saleId = context.Request["saletaskID"];
            int number = Convert.ToInt32(context.Request["number"]);
            string bookNum = context.Request["bookNum"].ToString();
            string type = context.Request["type"];
            string saletype = context.Request["saletype"];
            DataSet bookNumds;

            if (teamtype == "team")
            {
                bookNumds = salemonbll.getsalemonDetail(SaleHeadId, saleId, bookNum);
            }
            else
            {
                bookNumds = salemonbll.getPersalemonDetail(SaleHeadId, saleId, bookNum);
            }
            if (bookNumds != null && bookNumds.Tables[0].Rows.Count > 0 && type != "continue")
            {
                context.Response.Write("已购买");
                context.Response.End();
            }
            else
            {
                if (isexites(saleId, bookNum) && saletype != "continue")
                {
                    context.Response.Write("以销售");
                    context.Response.End();
                }
                else
                {

                    if (number < 0)
                    {
                        number = Math.Abs(number);
                        if (bookNumds != null)
                        {
                            int booknumber = int.Parse(bookNumds.Tables[0].Rows[0]["number"].ToString());
                            if (number > booknumber)
                            {
                                context.Response.Write("输入的负数不能大于已购数量，已购数为:" + booknumber);
                                context.Response.End();
                            }
                            else
                            {
                                number = number * -1;
                                addsalemon(context);
                            }
                        }
                        else
                        {
                            context.Response.Write("该书籍没有购买过，数量不能为负数");
                            context.Response.End();
                        }
                    }
                    else
                    {
                        addsalemon(context);
                    }
                }
            }
        }

        private void changeNum(HttpContext context)
        {
            SaleHeadBll saleheadbll = new SaleHeadBll();
            string SaleHeadId = context.Request["saleheadID"];
            string saleId = context.Request["saletaskID"];
            int number = Convert.ToInt32(context.Request["number"]);
            string bookNum = context.Request["bookNum"].ToString();
            DataSet bookNumds;
            if (teamtype == "team")
            {
                bookNumds = salemonbll.getsalemonDetail(SaleHeadId, saleId, bookNum);
            }
            else
            {
                bookNumds = salemonbll.getPersalemonDetail(SaleHeadId, saleId, bookNum);
            }

            if (number < 0)
            {
                number = Math.Abs(number);
                if (bookNumds != null)
                {
                    int booknumber = int.Parse(bookNumds.Tables[0].Rows[0]["number"].ToString());
                    if (number > booknumber)
                    {
                        context.Response.Write("输入的负数不能大于已购数量，已购数为:" + booknumber);
                        context.Response.End();
                    }
                    else
                    {
                        number = number * -1;
                        addsalemon(context);
                    }
                }
            }
            else
            {
                addsalemon(context);
            }

        }
        public void addsalemon(HttpContext context)
        {
            string SaleHeadId = context.Request["saleheadID"];
            string saleId = context.Request["saletaskID"];
            int number = Convert.ToInt32(context.Request["number"]);
            string bookNum = context.Request["bookNum"];
            SaleTaskBll saletaskbll = new SaleTaskBll();
            BookBasicData book = new BookBasicData();
            BookBasicBll bookbll = new BookBasicBll();
            if (teamtype == "team")
            {
                int regionid = saletaskbll.GetregionidBysaleid(saleId);
                DataSet stockbook = stobll.SelectByBookNum(bookNum, regionid);
                int allstockNum = 0;
                for (int h = 0; h < stockbook.Tables[0].Rows.Count; h++)
                {
                    allstockNum += Convert.ToInt32(stockbook.Tables[0].Rows[h]["stockNum"]);
                }
                if (number > allstockNum)
                {
                    context.Response.Write("库存数量不足，当前最大库存为：" + allstockNum);
                    context.Response.End();
                }
                else
                {
                    BookBasicBll Bookbll = new BookBasicBll();
                    book = Bookbll.SelectById(bookNum);
                    string remarks = book.Remarks;
                    string defaultdiscount;
                    string bookISBN = book.Isbn;
                    string saleHeadId = SaleHeadId;
                    SaleTask task = saletaskbll.selectById(saleId);
                    defaultdiscount = task.DefaultDiscount.ToString();
                    if (defaultdiscount == "-1")
                    {
                        if (double.Parse(remarks) < 1)
                        {
                            remarks = (double.Parse(remarks) * 100).ToString();
                        }
                    }
                    double disCount = double.Parse(remarks);
                    int count = salemonbll.SelectBySaleHeadId(saleHeadId);
                    int saleIdmonomerId;
                    if (count == 0)
                    {
                        saleIdmonomerId = 1;
                        salemonbll.updateHeadstate(saleId, SaleHeadId, 1);
                    }
                    else
                    {
                        saleIdmonomerId = count + 1;
                    }
                    double price = Convert.ToInt32(book.Price);
                    double totalPrice = price * number;
                    double realPrice = totalPrice * (disCount / 100);
                    DateTime Time = DateTime.Now.ToLocalTime();
                    SaleMonomer newSalemon = new SaleMonomer()
                    {
                        AlreadyBought = 0,
                        SaleIdMonomerId = saleIdmonomerId,
                        BookNum = bookNum,
                        ISBN1 = bookISBN,
                        SaleHeadId = saleHeadId,
                        Number = number,
                        UnitPrice = price,
                        TotalPrice = totalPrice,
                        RealPrice = realPrice,
                        RealDiscount = disCount,
                        Datetime = Time,
                        SaleTaskId = saleId
                    };

                    //for (int j = 0; j < stockbook.Tables[0].Rows.Count; j++)
                    //{
                    //    int stockNum = Convert.ToInt32(stockbook.Tables[0].Rows[j]["stockNum"]);
                    //    string goodsId = stockbook.Tables[0].Rows[j]["goodsShelvesId"].ToString();
                    //    if (number <= stockNum)
                    //    {
                    //        int stockcount = stockNum - number;
                    //        stobll.update(stockcount, goodsId, bookNum);

                    //    }
                    //    else
                    //    {
                    //        number = number - stockNum;
                    //        stobll.update(0, goodsId, bookNum);
                    //        if (number == 0)
                    //        {
                    //            break;
                    //        }
                    //    }
                    //}
                    Result insertres = salemonbll.addsale(regionid,newSalemon); 
                    string op = context.Request["op"];
                    if (insertres == Result.添加成功)
                    {
                        //更新单头
                        updateSalehead(context);

                        if (op == "change")
                        {
                            context.Response.Write("修改成功");
                            context.Response.End();
                        }
                        else
                        {
                            context.Response.Write("添加成功");
                            context.Response.End();
                        }
                    }
                    else
                    {
                        if (op == "change")
                        {
                            context.Response.Write("修改失败");
                            context.Response.End();
                        }
                        else
                        {
                            context.Response.Write("添加失败");
                            context.Response.End();
                        }
                    }
                }
            }
            else
            {
                BookBasicData bookData = bookbll.SelectById(bookNum);
                string remarks = bookData.Remarks;
                string defaultdiscount;
                string bookISBN = bookData.Isbn;
                SaleTask task = saletaskbll.selectById(saleId);
                defaultdiscount = task.DefaultDiscount.ToString();
                if (defaultdiscount == "-1")
                {
                    if (double.Parse(remarks) < 1)
                    {
                        remarks = (double.Parse(remarks) * 100).ToString();
                    }
                }
                //if (remarks == "" || remarks == null)
                //{
                //    remarks = defaultdiscount;
                //}
                else
                {
                    remarks = defaultdiscount;
                }
                double disCount = double.Parse(remarks);
                book = bookbll.SelectById(bookNum);
                string saleHeadId = SaleHeadId;
                int saleIdmonomerId;
                int count = salemonbll.SelectByPerSaleHeadId(saleHeadId);
                if (count == 0)
                {
                    saleIdmonomerId = 1;
                    salemonbll.updatePerHeadstate(saleId, SaleHeadId, 1);
                }
                else
                {
                    saleIdmonomerId = count + 1;
                }
                double price = Convert.ToDouble(book.Price);
                double totalPrice = price * number;
                double realPrice = totalPrice * (disCount / 100);
                DateTime Time = DateTime.Now.ToLocalTime();
                SaleMonomer newSalemon = new SaleMonomer()
                {
                    AlreadyBought = 0,
                    SaleIdMonomerId = saleIdmonomerId,
                    BookNum = bookNum,
                    ISBN1 = bookISBN,
                    SaleHeadId = saleHeadId,
                    Number = number,
                    UnitPrice = price,
                    TotalPrice = totalPrice,
                    RealPrice = realPrice,
                    RealDiscount = disCount,
                    Datetime = Time,
                    SaleTaskId = saleId
                };
                Result res = salemonbll.perInsert(newSalemon);


                string op = context.Request["op"];
                if (res == Result.添加成功)
                {
                    //更新单头
                    updateSalehead(context);

                    if (op == "change")
                    {
                        context.Response.Write("修改成功");
                        context.Response.End();
                    }
                    else
                    {
                        context.Response.Write("添加成功");
                        context.Response.End();
                    }
                }
                else
                {
                    if (op == "change")
                    {
                        context.Response.Write("修改失败");
                        context.Response.End();
                    }
                    else
                    {
                        context.Response.Write("添加失败");
                        context.Response.End();
                    }
                }
            }

        }
        /// <summary>
        /// 更新单头
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Result updateSalehead(HttpContext context)
        {
            string SaleHeadId = context.Request["saleheadID"];
            string saleId = context.Request["saletaskID"];
            int allnumber;
            double alltotalprice;
            double allreadprice;

            DataSet ds;
            int allkinds;
            if (teamtype == "team")
            {
                allkinds = int.Parse(salemonbll.getkinds(saleId, SaleHeadId).ToString());
                ds = salemonbll.calculationSaleHead(SaleHeadId, saleId);
            }
            else
            {
                allkinds = int.Parse(salemonbll.getperkinds(saleId, SaleHeadId).ToString());
                ds = salemonbll.calculationPerSaleHead(SaleHeadId, saleId);
            }
            if (ds == null)
            {
                allnumber = 0;
                alltotalprice = 0;
                allreadprice = 0;
            }
            else
            {
                allnumber = int.Parse(ds.Tables[0].Rows[0]["数量"].ToString());
                alltotalprice = double.Parse(ds.Tables[0].Rows[0]["总码洋"].ToString());
                allreadprice = double.Parse(ds.Tables[0].Rows[0]["总实洋"].ToString());
            }
            //添加成功 更新单头
            SaleHead salehead = new SaleHead();
            salehead.SaleTaskId = saleId;
            salehead.SaleHeadId = SaleHeadId;
            salehead.KindsNum = allkinds;
            salehead.Number = allnumber;
            salehead.AllTotalPrice = alltotalprice;
            salehead.AllRealPrice = allreadprice;

            Result res;
            if (teamtype == "team")
            {
                res = salemonbll.wechatupdateHead(salehead);
            }
            else
            {
                res = salemonbll.wechatupdatePerHead(salehead);
            }

            return res;
        }

    }
    public class book
    {
        public string BookNum { get; set; }
        public string number { get; set; }
        public string BookName { get; set; }
        public double Price { get; set; }
        public int count { get; set; }
        public string type { get; set; }
    }
}