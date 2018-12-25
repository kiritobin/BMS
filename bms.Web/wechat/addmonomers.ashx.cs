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
        loginmsg msg = new loginmsg();
        BookBasicBll bookbll = new BookBasicBll();

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
            if (op == "isbn")
            {
                getBook(context);
            }
            if (op == "addsale")
            {
                addsale(context);
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
            tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,realDiscount,sum(number) as allnumber ,sum(totalPrice) as alltotalPrice,userName,customerName,regionName";
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
            dt.Columns.Add("alltotalPrice", typeof(double));
            //(i + 1 + ((currentPage - 1) * pageSize))
            int count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                int number = (i + 1 + ((currentPage - 1) * pageSize));
                dt.Rows.Add(Convert.ToInt32(number), ds.Tables[0].Rows[i]["bookName"].ToString(), Convert.ToInt64(ds.Tables[0].Rows[i]["allnumber"].ToString()), Convert.ToDouble(ds.Tables[0].Rows[i]["unitPrice"].ToString()), Convert.ToDouble(ds.Tables[0].Rows[i]["alltotalPrice"].ToString()));
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
                string customerId = context.Request["customerId"];
                LibraryCollectionBll library = new LibraryCollectionBll();
                Result libresult = library.Selectbook(customerId, ISBN);
                if (libresult == Result.记录不存在 || type == "continue")
                {

                    string saleId = context.Request["saletaskID"];
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
                        dt.Columns.Add("color", typeof(string));
                        for (int i = 0; i < bookds.Tables[0].Rows.Count; i++)
                        {
                            dt.Rows.Add(bookds.Tables[0].Rows[i]["bookNum"].ToString(), Convert.ToInt32((i + 1)), bookds.Tables[0].Rows[i]["bookName"].ToString(), Convert.ToDouble(bookds.Tables[0].Rows[i]["price"].ToString()), "");
                        }

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
                    //只有一条数据
                    else
                    {
                        book book = new book();
                        //bookNum,ISBN,price,author,bookName,supplier
                        book.BookNum = bookds.Tables[0].Rows[0]["bookNum"].ToString();
                        book.BookName = bookds.Tables[0].Rows[0]["bookName"].ToString();
                        book.Price = double.Parse(bookds.Tables[0].Rows[0]["price"].ToString());
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

        private void addsale(HttpContext context)
        {
            SaleHeadBll saleheadbll = new SaleHeadBll();
            string SaleHeadId = context.Request["saleheadID"];
            string saleId = context.Request["saletaskID"];
            int number = Convert.ToInt32(context.Request["number"]);
            string bookNum = context.Request["bookNum"].ToString();
            string type = context.Request["type"];
            DataSet bookNumds = salemonbll.getsalemonDetail(SaleHeadId, saleId, bookNum);
            if (bookNumds != null && bookNumds.Tables[0].Rows.Count > 0 && type != "continue")
            {
                context.Response.Write("已购买");
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
        public void addsalemon(HttpContext context)
        {
            string SaleHeadId = context.Request["saleheadID"];
            string saleId = context.Request["saletaskID"];
            int number = Convert.ToInt32(context.Request["number"]);
            string bookNum = context.Request["bookNum"];

            BookBasicBll bookbll = new BookBasicBll();
            BookBasicData bookData = bookbll.SelectById(bookNum);
            SaleTaskBll saletaskbll = new SaleTaskBll();
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
            BookBasicBll Bookbll = new BookBasicBll();
            BookBasicData book = new BookBasicData();
            book = Bookbll.SelectById(bookNum);
            string saleHeadId = SaleHeadId;
            int saleIdmonomerId;
            int count = salemonbll.SelectBySaleHeadId(saleHeadId);
            if (count == 0)
            {
                saleIdmonomerId = 1;
                salemonbll.updateHeadstate(saleId, SaleHeadId, saleIdmonomerId);
            }
            else
            {
                saleIdmonomerId = count + 1;
            }
            int price = Convert.ToInt32(book.Price);
            int totalPrice = price * number;
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
            Result res = salemonbll.Insert(newSalemon);
            if (res == Result.添加成功)
            {
                Result upresult = updateSalehead(context);
                if (upresult == Result.更新成功)
                {

                }
                context.Response.Write("添加成功");
                context.Response.End();
            }
            else
            {
                context.Response.Write("添加失败");
                context.Response.End();
            }
        }
        public Result updateSalehead(HttpContext context)
        {
            string SaleHeadId = context.Request["saleheadID"];
            string saleId = context.Request["saletaskID"];
            int allnumber;
            double alltotalprice;
            double allreadprice;

            int allkinds = int.Parse(salemonbll.getkinds(saleId, SaleHeadId).ToString());
            DataSet ds = salemonbll.calculationSaleHead(SaleHeadId, saleId);
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
            Result res = salemonbll.wechatSummary(salehead);
            return res;
        }

    }
    public class book
    {
        public string BookNum { get; set; }
        public string number { get; set; }
        public string BookName { get; set; }
        public double Price { get; set; }
        public string type { get; set; }
    }
}