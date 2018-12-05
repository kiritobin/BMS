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
    using System.IO;
    using System.Runtime.Serialization.Json;
    using Result = Enums.OpResult;
    public partial class salesDetail : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 100, allstockNum = 0;
        public string type, defaultdiscount;
        public DataSet ds, bookds, stockbook;
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        SaleTaskBll saletaskbll = new SaleTaskBll();
        public StringBuilder strbook = new StringBuilder();
        public int allkinds, allnumber, numberLimit;
        public double alltotalprice, allreadprice, defaultCopy, priceLimit;
        string bookISBN, SaleHeadId, saleId;
        double disCount;
        int number, saleIdmonomerId;
        string bookNum;
        msg msg = new msg();
        User user = new User();
        BookBasicBll bookbll = new BookBasicBll();
        StockBll stockbll = new StockBll();
        replenishMentBll replenBll = new replenishMentBll();
        protected void Page_Load(object sender, EventArgs e)
        {

            user = (User)Session["user"];
            type = Session["saleType"].ToString();
            saleId = Session["saleId"].ToString();
            getData();
            getlimt();
            SaleTask task = saletaskbll.selectById(saleId);
            defaultdiscount = task.DefaultDiscount.ToString();
            SaleHeadId = Session["saleheadId"].ToString();

            //更新单头
            updateSalehead();
            string op = Request["op"];
            //isbn回车
            if (op == "search")
            {
                string ISBN = Request["ISBN"];
                bookds = bookbll.SelectByIsbn(ISBN);
                if (bookds != null)
                {
                    //如果有两条及两条以上显示表格
                    if (bookds.Tables[0].Rows.Count > 1)
                    {
                        getbook();
                    }
                    //只有一条数据
                    else
                    {
                        backbook();
                    }
                }
                else
                {
                    Response.Write("无数据");
                    Response.End();
                }
            }
            //一书多号选择后执行
            if (op == "add")
            {
                showBook();
            }
            //添加销售
            if (op == "addsale")
            {
                SaleHeadBll saleheadbll = new SaleHeadBll();
                string saletaskId = saleheadbll.SelectTaskByheadId(SaleHeadId);
                string customerId = saletaskbll.getCustomerId(saletaskId);
                int AlreadyBought = user.ReginId.RegionId;
                //判断馆藏
                LibraryCollectionBll library = new LibraryCollectionBll();
                bookISBN = Request["bookISBN"];
                Result libresult = library.Selectbook(customerId, bookISBN);
                if (libresult == Result.记录不存在)
                {
                    bookISBN = Request["bookISBN"];
                    disCount = double.Parse(Request["discount"]);
                    number = Convert.ToInt32(Request["number"]);
                    bookNum = Request["bookNum"].ToString();
                    addsalemon();
                }
                else
                {
                    msg.Messege = "客户馆藏已存在";
                    Response.Write(ObjectToJson(msg));
                    Response.End();
                }
            }
            //客户馆藏已存在，继续录入
            if (op == "addRsMon")
            {
                bookISBN = Request["bookISBN"];
                disCount = double.Parse(Request["discount"]);
                number = Convert.ToInt32(Request["number"]);
                bookNum = Request["bookNum"].ToString();
                addsalemon();
            }
            //完成单据
            if (op == "success")
            {
                //判断是否有单体
                int row = salemonbll.SelectBySaleHeadId(SaleHeadId);
                if (row > 0)
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
                else
                {
                    Response.Write("没有数据");
                    Response.End();
                }
            }
            //返回按钮
            if (op == "back")
            {
                updateSalehead();
                int row = salemonbll.SelectBySaleHeadId(SaleHeadId);
                if (row > 0)
                {
                    string state = salemonbll.getsaleHeadState(SaleHeadId, saleId);
                    if (state == "0")
                    {
                        Result res = salemonbll.updateHeadstate(saleId, SaleHeadId, 1);
                        if (res == Result.更新成功)
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
                    else
                    {
                        Response.Write("无数据");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("无数据");
                    Response.End();
                }

            }
        }
        /// <summary>
        /// 一号多书时选择其中一本后触发
        /// </summary>
        public void showBook()
        {
            int count = salemonbll.SelectBySaleHeadId(SaleHeadId);
            if (count == 0)
            {
                count = 1;
            }
            else
            {
                count += 1;
            }
            string booknum = Request["bookNum"];
            int alreadyBought = salemonbll.getBookNumberSumByBookNum(booknum, saleId);

            BookBasicBll bookbll = new BookBasicBll();
            BookBasicData book = bookbll.SelectById(booknum);
            string remarks = book.Remarks;
            if (defaultdiscount == "100")
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
            string ISBN = Request["isbn"];
            string bookname = Request["bookname"];
            string price = Request["price"];
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            sb.Append("<tr class='first'><td>" + count + "</td>");
            sb.Append("<td>" + "<input type='text' class='isbn textareaISBN' value='" + ISBN + "' onkeyup='this.value=this.value.replace(/[^\r\n0-9]/g,'');' /></td>");
            sb.Append("<td>" + booknum + "</td>");
            sb.Append("<td>" + bookname + "</td>");
            sb.Append("<td>" + price + "</td>");
            sb.Append("<td><input class='count textareaCount' type='number' value='" + defaultCopy.ToString() + "'/></td>");
            sb.Append("<td><input class='discount textareaDiscount' value='" + remarks + "' onkeyup='this.value=this.value.replace(/[^\r\n0-9]/g,'');' /></td>");
            sb.Append("<td>" + "" + "</td>");
            sb.Append("<td>" + alreadyBought + "</td></tr>");
            sb.Append("</tbody>");
            Response.Write(sb.ToString());
            Response.End();
        }
        /// <summary>
        /// 获取销售任务码洋限制
        /// </summary>
        public void getlimt()
        {
            DataSet limtds = saletaskbll.SelectBysaleTaskId(saleId);
            string copy = limtds.Tables[0].Rows[0]["defaultCopy"].ToString();
            if (copy == "" || copy == null)
            {
                defaultCopy = 0;
            }
            else
            {
                defaultCopy = double.Parse(copy);
            }
            numberLimit = int.Parse(limtds.Tables[0].Rows[0]["numberLimit"].ToString());
            priceLimit = double.Parse(limtds.Tables[0].Rows[0]["priceLimit"].ToString());
        }

        /// <summary>
        /// 只有一条数据时
        /// </summary>
        public void backbook()
        {
            int count = salemonbll.SelectBySaleHeadId(SaleHeadId);
            if (count == 0)
            {
                count = 1;
            }
            else
            {
                count += 1;
            }

            BookBasicBll bookbll = new BookBasicBll();
            string booknum = bookds.Tables[0].Rows[0]["bookNum"].ToString();
            BookBasicData book = bookbll.SelectById(booknum);
            string remarks = book.Remarks;
            if (defaultdiscount == "100")
            {
                if (double.Parse(remarks) < 1)
                {
                    remarks = (double.Parse(remarks) * 100).ToString();
                }
            }
            else
            {
                remarks = defaultdiscount;
            }
            //if (remarks == "" || remarks == null)
            //{
            //    remarks = defaultdiscount;
            //}
            //else
            //{
            //    if (double.Parse(remarks) < 1)
            //    {
            //        remarks = (double.Parse(remarks) * 100).ToString();
            //    }
            //}
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            int alreadyBought;
            string addbooknum;
            for (int i = 0; i < bookds.Tables[0].Rows.Count; i++)
            {
                addbooknum = bookds.Tables[0].Rows[i]["bookNum"].ToString();
                alreadyBought = salemonbll.getBookNumberSumByBookNum(addbooknum, saleId);
                sb.Append("<tr class='first'><td>" + count + "</td>");
                sb.Append("<td>" + "<input type='text' class='isbn textareaISBN' value='" + bookds.Tables[0].Rows[i]["ISBN"].ToString() + "' onkeyup='this.value=this.value.replace(/[^\r\n0-9]/g,'');' /></td>");
                sb.Append("<td>" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                sb.Append("<td>" + bookds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                sb.Append("<td>" + bookds.Tables[0].Rows[i]["price"].ToString() + "</td>");
                sb.Append("<td><input class='count textareaCount' type='number' value='" + defaultCopy.ToString() + "'/></td>");
                sb.Append("<td><input class='discount textareaDiscount' value='" + remarks + "' onkeyup='this.value=this.value.replace(/[^\r\n0-9]/g,'');' /></td>");
                sb.Append("<td>" + "" + "</td>");
                sb.Append("<td>" + alreadyBought + "</td></tr>");
            }
            sb.Append("</tbody>");
            Response.Write(sb.ToString());
            Response.End();
        }
        /// <summary>
        /// 添加销售
        /// </summary>
        public void addsalemon()
        {
            string headState = salemonbll.getsaleHeadState(SaleHeadId, saleId);
            if (headState == "2")
            {
                msg.Messege = "该单据已完成，不能再添加数据！";
                Response.Write(ObjectToJson(msg));
                Response.End();
            }
            else
            {
                int RegionId = user.ReginId.RegionId;
                DataSet stockbook = stockbll.SelectByBookNum(bookNum, RegionId);
                allstockNum = 0;
                for (int h = 0; h < stockbook.Tables[0].Rows.Count; h++)
                {
                    allstockNum += Convert.ToInt32(stockbook.Tables[0].Rows[h]["stockNum"]);
                }
                if (number > allstockNum)
                {
                    msg.Messege = "库存不足，当前最大库存为：" + allstockNum;
                    Response.Write(ObjectToJson(msg));
                    Response.End();
                }
                else
                {
                    BookBasicBll Bookbll = new BookBasicBll();
                    BookBasicData book = new BookBasicData();
                    book = Bookbll.SelectById(bookNum);
                    string saleHeadId = SaleHeadId;
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

                    for (int j = 0; j < stockbook.Tables[0].Rows.Count; j++)
                    {
                        int stockNum = Convert.ToInt32(stockbook.Tables[0].Rows[j]["stockNum"]);
                        int goodsId = Convert.ToInt32(stockbook.Tables[0].Rows[j]["goodsShelvesId"]);
                        if (number <= stockNum)
                        {
                            int stockcount = stockNum - number;
                            stockbll.update(stockcount, goodsId, bookNum);

                        }
                        else
                        {
                            number = number - stockNum;
                            stockbll.update(0, goodsId, bookNum);
                            if (number == 0)
                            {
                                break;
                            }
                        }
                    }
                    Result res = salemonbll.Insert(newSalemon);
                    if (res == Result.添加成功)
                    {
                        updateSalehead();
                        msg.DataTable = getData();
                        msg.DataTable1 = "<tr class='first'> <td></td><td><input type='text' id='ISBN' class='isbn textareaISBN' onkeyup='this.value=this.value.replace(/[^\r\n0-9]/g,'');' /> </td><td></td><td></td><td></td><td><input class='count textareaCount' type='number'/></td><td><input class='discount textareaDiscount' onkeyup='this.value=this.value.replace(/[^\r\n0-9]/g,'');' /></td><td></td><td></td></tr>";
                        msg.AllKinds = allkinds.ToString();
                        msg.Number = allnumber.ToString();
                        msg.AlltotalPrice = alltotalprice.ToString();
                        msg.AllrealPrice = allreadprice.ToString();
                        msg.Messege = "添加成功";
                        Response.Write(ObjectToJson(msg));
                        Response.End();
                    }
                    else
                    {
                        msg.Messege = "添加失败";
                        Response.Write(ObjectToJson(msg));
                        Response.End();
                    }
                }
            }

        }
        /// <summary>
        /// 对象转json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new System.IO.MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0; stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }

        /// <summary>
        /// 更新销售单头
        /// </summary>
        /// <returns>返回结果</returns>
        public Result updateSalehead()
        {
            //更新单头
            allkinds = int.Parse(salemonbll.getkinds(saleId, SaleHeadId).ToString());
            allnumber = salemonbll.getsBookNumberSum(SaleHeadId, saleId);
            alltotalprice = salemonbll.getsBookTotalPrice(SaleHeadId, saleId);
            allreadprice = salemonbll.getsBookRealPrice(SaleHeadId, saleId);
            //DataSet allds = salemonbll.SelectMonomers(SaleHeadId);
            //int j = allds.Tables[0].Rows.Count;
            //for (int h = 0; h < j; h++)
            //{
            //    allnumber += Convert.ToInt32(allds.Tables[0].Rows[h]["number"]);
            //    alltotalprice += Convert.ToInt32(allds.Tables[0].Rows[h]["totalPrice"]);
            //    allreadprice += Convert.ToInt32(allds.Tables[0].Rows[h]["realPrice"]);
            //}
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
        public void getbook()
        {
            string booknum;
            int isnull = 0;
            strbook.Append("<thead>");
            strbook.Append("<tr>");
            strbook.Append("<th>" + "<div class='pretty inline much'><input type = 'radio' disabled='disabled' name='radio'><label><i class='mdi mdi-check'></i></label></div>" + "</th>");
            strbook.Append("<th>" + "ISBN" + "</th>");
            strbook.Append("<th>" + "书号" + "</th>");
            strbook.Append("<th>" + "书名" + "</th>");
            strbook.Append("<th>" + "单价" + "</th>");
            strbook.Append("<th>" + "出版社" + "</th>");
            strbook.Append("</tr>");
            strbook.Append("</thead>");
            strbook.Append("<tbody>");
            int count = bookds.Tables[0].Rows.Count;
            for (int i = 0; i < bookds.Tables[0].Rows.Count; i++)
            {
                int booknumber = 0;
                booknum = bookds.Tables[0].Rows[i]["bookNum"].ToString();
                DataSet stockbook = stockbll.SelectByBookNum(booknum, user.ReginId.RegionId);
                for (int j = 0; j < stockbook.Tables[0].Rows.Count; j++)
                {
                    booknumber += Convert.ToInt32(stockbook.Tables[0].Rows[j]["stockNum"]);
                }
                if (booknumber == 0)
                {
                    isnull++;
                    if (isnull == count)
                    {
                        strbook.Append("<tr><td colspan='6'>该书无库存</td></tr>");
                    }
                    continue;
                }
                strbook.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["price"].ToString() + "</td>");
                strbook.Append("<td>" + bookds.Tables[0].Rows[i]["supplier"].ToString() + "</td></tr>");
            }
            strbook.Append("</tbody>");
            Response.Write(strbook.ToString());
            Response.End();
        }
        public string getData()
        {
            string saleheadId = Session["saleheadId"].ToString();
            string saletaskId = Session["saleId"].ToString();
            //type = "look";
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
            tb.OrderBy = "dateTime";
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
                string discount = ds.Tables[0].Rows[i]["realDiscount"].ToString();
                if (double.Parse(discount) < 1)
                {
                    discount = (double.Parse(discount) * 100).ToString();
                }
                string bookNum = ds.Tables[0].Rows[i]["bookNum"].ToString();
                int alreadyBought = salemonbll.getBookNumberSumByBookNum(bookNum, saleId);
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                strb.Append("<td>" + bookNum + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["number"].ToString() + "</td>");
                strb.Append("<td>" + discount + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realPrice"].ToString() + "</td>");
                strb.Append("<td>" + alreadyBought + "</td></tr>");
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
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