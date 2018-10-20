﻿using bms.Bll;
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
        string bookISBN, SaleHeadId, saleId, rsHead;
        double disCount;
        int number, d_Value, saleIdmonomerId;
        string bookNum;
        msg msg = new msg();
        User user = new User();
        BookBasicBll bookbll = new BookBasicBll();
        StockBll stockbll = new StockBll();
        replenishMentBll replenBll = new replenishMentBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            user = (User)Session["user"];
            type = Session["saleType"].ToString();
            saleId = Session["saleId"].ToString();
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
            //一书多好选择后执行
            if (op == "add")
            {
                showBook();
            }
            //添加销售单体
            if (op == "addsale")
            {
                bookISBN = Request["bookISBN"];
                disCount = double.Parse(Request["discount"]);
                number = Convert.ToInt32(Request["number"]);
                bookNum = Request["bookNum"].ToString();
                addsalemon();
            }
            if (op == "addRsMon")
            {
                bookISBN = Request["bookISBN"];
                disCount = double.Parse(Request["discount"]);
                d_Value = Convert.ToInt32(Request["count"]);
                number = Convert.ToInt32(Request["number"]);
                bookNum = Request["bookNum"].ToString();
                addsalemon();
            }
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
                    string state = salemonbll.getsaleHeadState(SaleHeadId);
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
            BookBasicBll bookbll = new BookBasicBll();
            BookBasicData book = bookbll.SelectById(booknum);
            string remarks = book.Remarks;
            if (remarks == "" || remarks == null)
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
            sb.Append("<td><input class='count textareaCount' type='number'/></td>");
            sb.Append("<td><input class='discount textareaDiscount' value='" + remarks + "' onkeyup='this.value=this.value.replace(/[^\r\n0-9]/g,'');' /></td>");
            sb.Append("<td>" + "" + "</td>");
            sb.Append("<td>" + "" + "</td></tr>");
            sb.Append("</tbody>");
            Response.Write(sb.ToString());
            Response.End();
        }
        /// <summary>
        /// 添加补货单头
        /// </summary>
        public Result addrsHead()
        {
            replenishMentHead rsHead = new replenishMentHead();
            replenishMentBll replenBll = new replenishMentBll();
            rsHead.KindsNum = 0;
            rsHead.Number = 0;
            rsHead.SaleTaskId = saleId;
            rsHead.UserId = user.UserId;
            rsHead.Time = DateTime.Now.ToLocalTime();
            return replenBll.InsertRsHead(rsHead);
        }
        /// <summary>
        /// 获取销售任务码洋限制
        /// </summary>
        public void getlimt()
        {
            DataSet limtds = saletaskbll.SelectBysaleTaskId(saleId);
            defaultCopy = double.Parse(limtds.Tables[0].Rows[0]["defaultCopy"].ToString());
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
            if (remarks == "" || remarks == null)
            {
                remarks = defaultdiscount;

            }
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < bookds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr class='first'><td>" + count + "</td>");
                sb.Append("<td>" + "<input type='text' class='isbn textareaISBN' value='" + bookds.Tables[0].Rows[i]["ISBN"].ToString() + "' onkeyup='this.value=this.value.replace(/[^\r\n0-9]/g,'');' /></td>");
                sb.Append("<td>" + bookds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                sb.Append("<td>" + bookds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                sb.Append("<td>" + bookds.Tables[0].Rows[i]["price"].ToString() + "</td>");
                sb.Append("<td><input class='count textareaCount' type='number'/></td>");
                sb.Append("<td><input class='discount textareaDiscount' value='" + remarks + "' onkeyup='this.value=this.value.replace(/[^\r\n0-9]/g,'');' /></td>");
                sb.Append("<td>" + "" + "</td>");
                sb.Append("<td>" + "" + "</td></tr>");
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
            string headState = salemonbll.getsaleHeadState(SaleHeadId);
            if (headState == "2")
            {
                msg.Messege = "该单据已完成，不能再添加数据！";
                Response.Write(ObjectToJson(msg));
                Response.End();
            }
            else
            {
                int RegionId = user.ReginId.RegionId;
                stockbook = stockbll.SelectByBookNum(bookNum, RegionId);
                if (stockbook != null)
                {
                    for (int i = 0; i < stockbook.Tables[0].Rows.Count; i++)
                    {
                        allstockNum += Convert.ToInt32(stockbook.Tables[0].Rows[i]["stockNum"]);
                    }
                    string tips = Request["tips"].ToString();
                    if (tips == "addsale")
                    {
                        //库存不足，生成询问是否生成补货单
                        if (number > allstockNum && number > 0)
                        {
                            d_Value = number - allstockNum;
                            msg.Count = d_Value.ToString();
                            msg.Count1 = allstockNum.ToString();
                            msg.Messege = "库存不足";
                            Response.Write(ObjectToJson(msg));
                            Response.End();
                        }
                        else if (allstockNum == 0 && number > 0)
                        {
                            d_Value = number;
                            msg.Count = d_Value.ToString();
                            msg.Count1 = allstockNum.ToString();
                            msg.Messege = "库存不足";
                            Response.Write(ObjectToJson(msg));
                            Response.End();
                        }
                    }
                    if (tips == "addMon")
                    {
                        //先添加销售单体
                        addSaleMon();
                    }
                    else
                    {
                        addSaleMon();
                    }
                }
                else
                {
                    msg.Messege = "无库存";
                    Response.Write(ObjectToJson(msg));
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 添加补货单体
        /// </summary>
        public void addRsmon()
        {
            //添加补货单体
            int rsMonomerId;
            int count = replenBll.countMon(saleId);
            if (count > 0)
            {
                rsMonomerId = count + 1;
            }
            else
            {
                rsMonomerId = 1;
            }
            BookBasicData book = bookbll.SelectById(bookNum);
            replenishMentMonomer replenMon = new replenishMentMonomer()
            {
                RsMonomerID=rsMonomerId,
                BookNum = bookNum,
                Isbn = book.Isbn,
                Author = book.Author,
                Supplier = book.Publisher,
                Count = d_Value,
                DateTime = DateTime.Now.ToLocalTime(),
                SaleTaskId = saleId,
                SaleHeadId = SaleHeadId,
                SaleIdMonomerId = saleIdmonomerId
            };
            Result addmonRes = replenBll.Insert(replenMon);
            if (addmonRes == Result.添加成功)
            {
                //更新补货单头
                int rskinds = replenBll.getkinds(rsHead);
                int rsnumber = replenBll.getsBookNumberSum(rsHead);
                replenishMentHead upRsHead = new replenishMentHead()
                {
                    SaleTaskId = rsHead,
                    KindsNum = rskinds,
                    Number = rsnumber,
                };
                replenBll.updateRsHead(upRsHead);
            }
            else
            {
                msg.Messege = "添加补货单体失败";
                Response.Write(ObjectToJson(msg));
                Response.End();
            }
        }

        /// <summary>
        /// 判断库存后添加销售单
        /// </summary>
        public void addSaleMon()
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
                int countnumber;
                if (number > allstockNum)
                {
                    countnumber = allstockNum;
                }
                else
                {
                    countnumber = number;
                }
                for (int i = 0; i < stockbook.Tables[0].Rows.Count; i++)
                {
                    int stockNum = Convert.ToInt32(stockbook.Tables[0].Rows[i]["stockNum"]);
                    int goodsId = Convert.ToInt32(stockbook.Tables[0].Rows[i]["goodsShelvesId"]);

                    if (countnumber <= stockNum)
                    {
                        int stockcount = 0;
                        if (number < 0)
                        {
                            //如果输入负数，判断库存是否为零，如果不为零则加库存，为零则添加负数的补货单
                            if (allstockNum >= 0 && number < 0)
                            {
                                int addnum = Math.Abs(number);
                                stockcount = stockNum + addnum;
                            }
                        }
                        else
                        {
                            stockcount = stockNum - countnumber;
                        }
                        Result upresult = stockbll.update(stockcount, goodsId, bookNum);
                        if (upresult == Result.更新成功)
                        {
                            //添加销售单体明细
                            Result result = addSalemonDetail();
                            if (result == Result.添加成功)
                            {
                                //
                                if (d_Value > 0)
                                {
                                    //单体添加成功，生成补货单
                                    //判断是否已有该销售任务的补货单头
                                    rsHead = replenBll.getRsHeadID(saleId);
                                    //已有补货单头,直接添加补货单体
                                    if (rsHead != "none")
                                    {
                                        addRsmon();
                                    }
                                    //没有补货单头，先生成补货单头，在添加补货单体
                                    else
                                    {
                                        Result resultrsHead = addrsHead();
                                        if (resultrsHead == Result.添加成功)
                                        {
                                            addRsmon();
                                        }
                                        else
                                        {
                                            msg.Messege = "添加补货单头失败";
                                            Response.Write(ObjectToJson(msg));
                                            Response.End();
                                        }
                                    }
                                }

                                ///更新销售单头
                                Result res = updateSalehead();
                                if (res == Result.更新成功)
                                {
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
                            }
                            else
                            {
                                msg.Messege = "添加失败";
                                Response.Write(ObjectToJson(msg));
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
                            msg.Messege = "添加成功";
                            Response.Write(ObjectToJson(msg));
                            Response.End();
                        }
                        if (upre == Result.更新失败)
                        {
                            msg.Messege = "添加失败";
                            Response.Write(ObjectToJson(msg));
                            Response.End();
                        }
                        if (stockbook.Tables[0].Rows.Count == 1 && upre == Result.更新成功)
                        {
                            Result addSalemonres = addSalemonDetail();
                            if (addSalemonres == Result.添加成功)
                            {
                                //单体添加成功，生成补货单
                                //判断是否已有该销售任务的补货单头
                                rsHead = replenBll.getRsHeadID(saleId);
                                //已有补货单头,直接添加补货单体
                                if (rsHead != "none")
                                {
                                    addRsmon();
                                }
                                //没有补货单头，先生成补货单头，在添加补货单体
                                else
                                {
                                    Result resultrsHead = addrsHead();
                                    if (resultrsHead == Result.添加成功)
                                    {
                                        addRsmon();
                                    }
                                    else
                                    {
                                        msg.Messege = "添加补货单头失败";
                                        Response.Write(ObjectToJson(msg));
                                        Response.End();
                                    }
                                }
                                Result res = updateSalehead();
                                if (res == Result.更新成功)
                                {
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
            }
            else
            {
                msg.Messege = "客户馆藏已存在";
                Response.Write(ObjectToJson(msg));
                Response.End();
            }
        }

        /// <summary>
        /// 添加单体详细信息
        /// </summary>
        /// <returns></returns>
        public Result addSalemonDetail()
        {
            BookBasicBll Bookbll = new BookBasicBll();
            BookBasicData book = new BookBasicData();
            book = Bookbll.SelectById(bookNum);
            string saleHeadId = SaleHeadId;
            //Session["saleheadId"].ToString();
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
            return salemonbll.Insert(newSalemon);
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
        /// 更新单头
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
            for (int i = 0; i < bookds.Tables[0].Rows.Count; i++)
            {
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
                string bookNum = ds.Tables[0].Rows[i]["bookNum"].ToString();
                int alreadyBought = salemonbll.getBookNumberSumByBookNum(bookNum, saleId);
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                strb.Append("<td>" + bookNum + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["number"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"].ToString() + "</td>");
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