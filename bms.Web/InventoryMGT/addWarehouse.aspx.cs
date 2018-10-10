using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using bms.Model;

namespace bms.Web.InventoryMGT
{
    using Result = Enums.OpResult;
    public partial class addWarehouse : System.Web.UI.Page
    {
        protected DataSet ds, dsGoods,dsPer;
        protected int pageSize=20, totalCount, intPageCount;
        public double discount;
        string singleHeadId;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply;
        SingleHead single = new SingleHead();
        UserBll userBll = new UserBll();
        WarehousingBll warehousingBll = new WarehousingBll();
        StockBll stockBll = new StockBll();
        BookBasicBll basicBll = new BookBasicBll();
        GoodsShelvesBll goods = new GoodsShelvesBll();
        public List<long> bookNumList = new List<long>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string kind = Request["kind"];
            if (kind == "0")
            {
                Session["List"] = new List<long>();
            }
            permission();
            Monomers monoDiscount = warehousingBll.getDiscount();
            discount = (monoDiscount.Discount);
            selectIsbn();

            if (!IsPostBack)
            {
                singleHeadId = Request.QueryString["sId"];
                if (singleHeadId != "" && singleHeadId != null)
                {
                    Session["singleHeadId"] = singleHeadId;
                }
                else
                {
                    singleHeadId = Session["singleHeadId"].ToString();
                }
            }
            User user = (User)Session["user"];
            int regId = user.ReginId.RegionId;
            string op = Request["op"];
            if (op == "add")
            {
                long bookNum = Convert.ToInt64(Request["bookNum"]);
                //BookBasicData bookBasicData = basicBll.SelectById(Convert.ToInt64(bookNum));
                //string isbn = bookBasicData.Isbn;


                int billCount = Convert.ToInt32(Request["billCount"]);
                DataSet dsGoods = stockBll.SelectByBookNum(bookNum,regId);
                if (dsGoods != null && dsGoods.Tables[0].Rows.Count > 0)
                {
                    int count = billCount;
                    int allCount = 0, allCounts = 0;
                    for (int i = 0; i < dsGoods.Tables[0].Rows.Count; i++)
                    {
                        allCount = Convert.ToInt32(dsGoods.Tables[0].Rows[i]["stockNum"]);
                        allCounts = allCounts + allCount;
                    }
                    if (billCount > allCounts)
                    {
                        Response.Write("库存数量不足");
                        Response.End();
                    }
                    else
                    {
                        long monCount = warehousingBll.getCount(singleHeadId);
                        long monId;
                        if (monCount > 0)
                        {
                            monId = monCount + 1;
                        }
                        else
                        {
                            monId = 1;
                        }
                        int number, allBillCount = 0;
                        double totalPrice, allTotalPrice = 0, realPrice, allRealPrice = 0;
                        DataTable dtHead = warehousingBll.SelectMonomers(singleHeadId);
                        int j = dtHead.Rows.Count;
                        for (int i = 0; i < j; i++)
                        {
                            DataRow dr = dtHead.Rows[i];
                            number = Convert.ToInt32(dr["number"]);
                            totalPrice = Convert.ToDouble(dr["totalPrice"]);
                            realPrice = Convert.ToDouble(dr["realPrice"]);
                            allBillCount = allBillCount + number;
                            allTotalPrice = allTotalPrice + totalPrice;
                            allRealPrice = allRealPrice + realPrice;
                        }
                        single.AllBillCount = allBillCount;
                        single.AllTotalPrice = allTotalPrice;
                        single.AllRealPrice = allRealPrice;
                        Result update = warehousingBll.updateHead(single);
                        if (update == Result.更新成功)
                        {
                            for (int i = 0; i < dsGoods.Tables[0].Rows.Count; i++)
                            {
                                billCount = count;
                                int stockNum = Convert.ToInt32(dsGoods.Tables[0].Rows[i]["stockNum"]);
                                int goodsId = Convert.ToInt32(dsGoods.Tables[0].Rows[i]["goodsShelvesId"]);
                                if (billCount <= stockNum)
                                {
                                    int a = stockNum - billCount;
                                    Result result = stockBll.update(a, goodsId, bookNum);
                                    if (result == Result.更新成功)
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
                                else
                                {
                                    count = billCount - stockNum;
                                    Result result = stockBll.update(0, goodsId, bookNum);
                                    if (count == 0)
                                    {
                                        Response.Write("添加成功");
                                        Response.End();
                                    }
                                    if (result == Result.更新失败)
                                    {
                                        Response.Write("添加失败");
                                        Response.End();
                                    }
                                }
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
                    Response.Write("库存不足");
                    Response.End();
                }

            }
            if (op == "del")
            {
                int Id = Convert.ToInt32(Request["ID"]);
                Result row = warehousingBll.deleteMonomer(singleHeadId, Id, 0);
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
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
            int regionId = user.ReginId.RegionId;
            dsGoods = goods.Select(regionId);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns></returns>
        //public string getData()
        //{
        //    //获取分页数据
        //    int currentPage = Convert.ToInt32(Request["page"]);
        //    if (currentPage == 0)
        //    {
        //        currentPage = 1;
        //    }
        //    string op = Request["op"];
        //    TableBuilder tbd = new TableBuilder();
        //    tbd.StrTable = "V_Monomer";
        //    tbd.OrderBy = "monId";
        //    tbd.StrColumnlist = "bookName,supplier,bookNum,singleHeadId,monId,ISBN,number,uPrice,totalPrice,realPrice,discount,type";
        //    tbd.IntPageSize = pageSize;
        //    tbd.StrWhere = "deleteState=0 and singleHeadId='" + singleHeadId+"'";
        //    tbd.IntPageNum = currentPage;
        //    //获取展示的用户数据
        //    ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);

        //    //生成table
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.Append("<tbody>");
        //    int count = ds.Tables[0].Rows.Count;
        //    for (int i = 0; i < count; i++)
        //    {
        //        DataRow dr = ds.Tables[0].Rows[i];
        //        sb.Append("<tr><td>" + dr["monId"].ToString() + "</td>");
        //        sb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
        //        sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
        //        sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
        //        sb.Append("<td>" + dr["supplier"].ToString() + "</td>");
        //        sb.Append("<td>" + dr["number"].ToString() + "</td>");
        //        sb.Append("<td>" + dr["uPrice"].ToString() + "</td>");
        //        sb.Append("<td>" + dr["discount"].ToString() + "</td>");
        //        sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
        //        sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
        //        sb.Append("<td><input type='hidden' value='" + dr["monId"].ToString() + "'/>");
        //        sb.Append("<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash'></i></button></td></tr>");
        //    }
        //    sb.Append("</tbody>");
        //    sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
        //    if (op == "paging")
        //    {
        //        Response.Write(sb.ToString());
        //        Response.End();
        //    }
        //    return sb.ToString();
        //}

        //public string getIsbn()
        //{
        //    string op = Request["op"];
        //    string isbn = Request["isbn"];
        //    double disCount = Convert.ToDouble(Request["disCount"]);
        //    int billCount = Convert.ToInt32(Request["billCount"]);
        //    if (isbn != null && isbn != "")
        //    {
        //        BookBasicBll bookBasicBll = new BookBasicBll();
        //        DataSet bookDs = bookBasicBll.SelectByIsbn(isbn);
        //        if (bookDs != null && bookDs.Tables[0].Rows.Count > 0)
        //        {
        //            if (disCount > 1 && disCount <= 10)
        //            {
        //                disCount = disCount * 0.1;
        //            }
        //            else if (disCount > 10)
        //            {
        //                disCount = disCount * 0.01;
        //            }
        //            double price = Convert.ToDouble(bookDs.Tables[0].Rows[0]["price"]);
        //            long bookNum = Convert.ToInt64(bookDs.Tables[0].Rows[0]["bookNum"]);
        //            double totalPrice = Convert.ToDouble((billCount * price).ToString("0.00"));
        //            double realPrice = Convert.ToDouble((totalPrice * disCount).ToString("0.00"));
        //            string singleId = Session["singleHeadId"].ToString();
        //            long monCount = warehousingBll.getCount(singleHeadId);
        //            long monId;
        //            if (monCount > 0)
        //            {
        //                monId = monCount + 1;
        //            }
        //            else
        //            {
        //                monId = 1;
        //            }
        //            StringBuilder sb = new StringBuilder();
        //            if (bookDs.Tables[0].Rows.Count == 1)
        //            {
        //                DataSet dsBooks = stockBll.SelectByBookNum(bookNum);
        //                if (dsBooks != null && dsBooks.Tables[0].Rows.Count > 0)
        //                {
        //                    int count = billCount;
        //                    int allCount = 0, allCounts = 0;
        //                    for (int i = 0; i < dsBooks.Tables[0].Rows.Count; i++)
        //                    {
        //                        allCount = Convert.ToInt32(dsBooks.Tables[0].Rows[i]["stockNum"]);
        //                        allCounts = allCounts + allCount;
        //                    }
        //                    if (billCount > allCounts)
        //                    {
        //                        Response.Write("库存数量不足");
        //                        Response.End();
        //                    }
        //                    else
        //                    {
        //                        //添加单体
        //                        Monomers monomers = new Monomers();
        //                        BookBasicData bookBasic = new BookBasicData();
        //                        SingleHead singleHead = new SingleHead();
        //                        bookBasic.Isbn = isbn;
        //                        bookBasic.Price = price;
        //                        bookBasic.BookNum = bookNum;
        //                        singleHead.SingleHeadId = singleHeadId;
        //                        monomers.Isbn = bookBasic;
        //                        monomers.UPrice = bookBasic;
        //                        monomers.BookNum = bookBasic;
        //                        monomers.Discount = disCount * 100;
        //                        monomers.MonomersId = Convert.ToInt32(monId);
        //                        monomers.Number = billCount;
        //                        monomers.TotalPrice = totalPrice;
        //                        monomers.RealPrice = realPrice;
        //                        monomers.SingleHeadId = singleHead;
        //                        monomers.Type = 0;
        //                        Result re = warehousingBll.SelectBybookNum(singleHeadId, bookNum.ToString(), 0);
        //                        Result res = warehousingBll.updateDiscount(disCount * 100);
        //                        if (re == Result.记录不存在)
        //                        {
        //                            if (res == Result.更新成功)
        //                            {
        //                                Result row = warehousingBll.insertMono(monomers);
        //                                if (row == Result.添加成功)
        //                                {//更新单头信息
        //                                    int number, allBillCount = 0;
        //                                    double totalPrices, allTotalPrice = 0, realPrices, allRealPrice = 0;
        //                                    DataTable dtHead = warehousingBll.SelectMonomers(singleHeadId);
        //                                    int j = dtHead.Rows.Count;
        //                                    for (int i = 0; i < j; i++)
        //                                    {
        //                                        DataRow dr = dtHead.Rows[i];
        //                                        number = Convert.ToInt32(dr["number"]);
        //                                        totalPrices = Convert.ToDouble(dr["totalPrice"]);
        //                                        realPrices = Convert.ToDouble(dr["realPrice"]);
        //                                        allBillCount = allBillCount + number;
        //                                        allTotalPrice = allTotalPrice + totalPrices;
        //                                        allRealPrice = allRealPrice + realPrices;
        //                                    }
        //                                    single.SingleHeadId = singleHeadId;
        //                                    single.AllBillCount = allBillCount;
        //                                    single.AllTotalPrice = allTotalPrice;
        //                                    single.AllRealPrice = allRealPrice;
        //                                    Result update = warehousingBll.updateHead(single);
        //                                    if (update == Result.更新成功)
        //                                    {//更新库存信息
        //                                        for (int i = 0; i < dsBooks.Tables[0].Rows.Count; i++)
        //                                        {
        //                                            billCount = count;
        //                                            int stockNum = Convert.ToInt32(dsBooks.Tables[0].Rows[i]["stockNum"]);
        //                                            int goodsId = Convert.ToInt32(dsBooks.Tables[0].Rows[i]["goodsShelvesId"]);
        //                                            if (billCount <= stockNum)
        //                                            {
        //                                                int a = stockNum - billCount;
        //                                                Result result = stockBll.update(a, goodsId, bookNum);
        //                                                if (result == Result.更新成功)
        //                                                {
        //                                                    Response.Write("添加成功");
        //                                                    Response.End();
        //                                                }
        //                                                else
        //                                                {
        //                                                    Response.Write("添加失败");
        //                                                    Response.End();
        //                                                }
        //                                            }
        //                                            else
        //                                            {
        //                                                count = billCount - stockNum;
        //                                                Result result = stockBll.update(0, goodsId, bookNum);
        //                                                if (count == 0)
        //                                                {
        //                                                    Response.Write("添加成功");
        //                                                    Response.End();
        //                                                }
        //                                                if (result == Result.更新失败)
        //                                                {
        //                                                    Response.Write("添加失败");
        //                                                    Response.End();
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        Response.Write("添加失败");
        //                                        Response.End();
        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    Response.Write("添加失败");
        //                                    Response.End();
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Response.Write("添加失败");
        //                                Response.End();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Response.Write("已添加过相同记录");
        //                            Response.End();
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    Response.Write("库存数量不足");
        //                    Response.End();
        //                }
        //            }
        //            else
        //            {
        //                //生成table
        //                sb.Append("<tbody id='tbody'>");
        //                int counts = bookDs.Tables[0].Rows.Count;
        //                for (int i = 0; i < counts; i++)
        //                {
        //                    DataRow dr = bookDs.Tables[0].Rows[i];
        //                    //sb.Append("<tr><td><input type='checkbox' name='checkbox' class='check' value='" + dr["bookNum"].ToString() + "' /></td>");
        //                    //sb.Append("<tr><td><input type='radio' name='radio' class='radio' value='" + dr["bookNum"].ToString() + "' /></td>");
        //                    sb.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + dr["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
        //                    sb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
        //                    sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
        //                    sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
        //                    sb.Append("<td>" + dr["price"].ToString() + "</td>");
        //                    sb.Append("<td>" + dr["supplier"].ToString() + "</td></tr>");
        //                }
        //                sb.Append("</tbody>");
        //                if (op == "isbn")
        //                {
        //                    Response.Write(sb.ToString());
        //                    Response.End();
        //                }
        //                return sb.ToString();
        //            }
        //        }
        //        else
        //        {
        //            Response.Write("ISBN不存在");
        //            Response.End();
        //        }
        //    }
        //    return null;
        //}

        protected void selectIsbn()
        {
            //货架
            GoodsShelvesBll goodsShelvesBll = new GoodsShelvesBll();
            User user = (User)Session["user"];
            int regionId = user.ReginId.RegionId;
            dsGoods = goodsShelvesBll.Select(regionId);

            string action = Request["action"];
            string isbn = Request["isbn"];
            DataTable monTable = new DataTable();
            DataRow dr = monTable.NewRow();

            if (action == "isbn")
            {
                long monId = Convert.ToInt64(Request["sid"]);
                DataSet dsBook = basicBll.SelectByIsbn(isbn);
                if (dsBook == null || dsBook.Tables[0].Rows.Count <= 0)
                {
                    Response.Write("ISBN不存在");
                    Response.End();
                }
                else
                {
                    int billCount = Convert.ToInt32(Request["billCount"]);
                    double disCount = Convert.ToDouble(Request["discount"]);

                    string bookName = dsBook.Tables[0].Rows[0]["bookName"].ToString();
                    string bookNum = dsBook.Tables[0].Rows[0]["bookNum"].ToString();
                    string supplier = dsBook.Tables[0].Rows[0]["supplier"].ToString();
                    double uPrice = Convert.ToDouble(dsBook.Tables[0].Rows[0]["price"]);
                    double totalPrice = Convert.ToDouble((billCount * uPrice).ToString("0.00"));
                    double realPrice = Convert.ToDouble((totalPrice * disCount).ToString("0.00"));

                    monTable.Columns.Add("ISBN", typeof(string));
                    monTable.Columns.Add("uPrice", typeof(double));
                    monTable.Columns.Add("bookName", typeof(string));
                    monTable.Columns.Add("bookNum", typeof(string));
                    monTable.Columns.Add("discount", typeof(double));
                    monTable.Columns.Add("monId", typeof(long));
                    monTable.Columns.Add("number", typeof(string));
                    monTable.Columns.Add("totalPrice", typeof(double));
                    monTable.Columns.Add("realPrice", typeof(double));
                    monTable.Columns.Add("singleHeadId", typeof(string));
                    monTable.Columns.Add("supplier", typeof(string));

                    dr["ISBN"] = isbn;
                    dr["uPrice"] = uPrice;
                    dr["bookName"] = bookName;
                    dr["bookNum"] = bookNum;
                    dr["discount"] = discount;
                    dr["monId"] = monId;
                    dr["number"] = billCount;
                    dr["totalPrice"] = totalPrice;
                    dr["realPrice"] = realPrice;
                    dr["singleHeadId"] = singleHeadId;
                    dr["supplier"] = supplier;
                    monTable.Rows.Add(dr);
                    int count = dsBook.Tables[0].Rows.Count;
                    StringBuilder sb = new StringBuilder();
                    if (count == 1)
                    {
                        for (int i = 0; i < monTable.Rows.Count; i++)
                        {
                            bookNumList = (List<long>)Session["List"];
                            foreach (long bookNums in bookNumList)
                            {
                                if (bookNums == Convert.ToInt64(monTable.Rows[i]["bookNum"]))
                                {
                                    Response.Write("已添加过此图书");
                                    Response.End();
                                }
                            }
                            bookNumList.Add(Convert.ToInt64(monTable.Rows[i]["bookNum"]));
                            Session["List"] = bookNumList;
                            sb.Append("<tr><td>" + monTable.Rows[i]["monId"] + "</td>");
                            sb.Append("<td><textarea class='isbn textareaISBN' row='1' maxlength='13'>" + monTable.Rows[i]["ISBN"] + "</textarea></td>");
                            sb.Append("<td style='display:none'>" + monTable.Rows[i]["bookNum"] + "</td>");
                            sb.Append("<td>" + monTable.Rows[i]["bookName"] + "</td>");
                            sb.Append("<td>" + monTable.Rows[i]["supplier"] + "</td>");
                            sb.Append("<td><textarea class='count textareaCount' row='1'>" + 0 + "</textarea></td>");
                            sb.Append("<td>" + monTable.Rows[i]["uPrice"] + "</td>");
                            sb.Append("<td><textarea class='discount textareaDiscount' row='1'>" + monTable.Rows[i]["discount"] + "</textarea></td>");
                            sb.Append("<td>" + monTable.Rows[i]["totalPrice"] + "</td>");
                            sb.Append("<td>" + monTable.Rows[i]["realPrice"] + "</td>");
                            sb.Append("<td><button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button></td></tr>");
                        }
                        Response.Write(sb.ToString());
                        Response.End();
                    }
                    else
                    {
                        sb.Append("<tbody>");
                        for (int i = 0; i < dsBook.Tables[0].Rows.Count; i++)
                        {
                            sb.Append("<tr><td><div class='pretty inline much'><input type = 'radio' name='radio' value='" + dsBook.Tables[0].Rows[i]["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                            sb.Append("<td>" + dsBook.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                            sb.Append("<td>" + dsBook.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                            sb.Append("<td>" + dsBook.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                            sb.Append("<td>" + dsBook.Tables[0].Rows[i]["price"].ToString() + "</td>");
                            sb.Append("<td>" + dsBook.Tables[0].Rows[i]["supplier"].ToString() + "</td></tr>");
                        }
                        sb.Append("</tbody>");
                        Response.Write(sb.ToString());
                        Response.End();
                        //Response.Write("一号多书");
                        //Response.End();
                    }
                }
            }
            else if (action == "add")
            {
                long monId = Convert.ToInt64(Request["sid"]);
                monTable.Columns.Add("ISBN", typeof(string));
                monTable.Columns.Add("uPrice", typeof(double));
                monTable.Columns.Add("bookName", typeof(string));
                monTable.Columns.Add("bookNum", typeof(string));
                monTable.Columns.Add("discount", typeof(double));
                monTable.Columns.Add("monId", typeof(long));
                monTable.Columns.Add("number", typeof(string));
                monTable.Columns.Add("totalPrice", typeof(double));
                monTable.Columns.Add("realPrice", typeof(double));
                monTable.Columns.Add("singleHeadId", typeof(string));
                monTable.Columns.Add("supplier", typeof(string));

                long bookNum = Convert.ToInt64(Request["bookNum"]);
                BookBasicData bookBasicData = basicBll.SelectById(bookNum);
                string supplier = bookBasicData.Publisher;
                string bookName = bookBasicData.BookName;
                string publisher = bookBasicData.Publisher;
                double price = bookBasicData.Price;
                string _isbn = bookBasicData.Isbn;

                bookNumList = (List<long>)Session["List"];
                foreach (long bookNums in bookNumList)
                {
                    if (bookNums == bookNum)
                    {
                        Response.Write("已添加过此图书");
                        Response.End();
                    }
                }

                dr["ISBN"] = _isbn;
                dr["uPrice"] = price;
                dr["bookName"] = bookName;
                dr["bookNum"] = bookNum;
                dr["discount"] = discount;
                dr["monId"] = monId;
                dr["number"] = 0;
                dr["totalPrice"] = 0;
                dr["realPrice"] = 0;
                dr["singleHeadId"] = singleHeadId;
                dr["supplier"] = supplier;
                monTable.Rows.Add(dr);
                for (int k = 0; k < monTable.Rows.Count; k++)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < monTable.Rows.Count; i++)
                    {
                        sb.Append("<tr><td>" + monTable.Rows[i]["monId"] + "</td>");
                        sb.Append("<td><textarea class='isbn textareaISBN' row='1' maxlength='13'>" + monTable.Rows[i]["ISBN"] + "</textarea></td>");
                        sb.Append("<td style='display:none'>" + monTable.Rows[i]["bookNum"] + "</td>");
                        sb.Append("<td>" + monTable.Rows[i]["bookName"] + "</td>");
                        sb.Append("<td>" + monTable.Rows[i]["supplier"] + "</td>");
                        sb.Append("<td><textarea class='count textareaCount' row='1'>" + monTable.Rows[i]["number"] + "</textarea></td>");
                        sb.Append("<td>" + monTable.Rows[i]["uPrice"] + "</td>");
                        sb.Append("<td><textarea class='discount textareaDiscount' row='1'>" + monTable.Rows[i]["discount"] + "</textarea></td>");
                        sb.Append("<td>" + monTable.Rows[i]["totalPrice"] + "</td>");
                        sb.Append("<td>" + monTable.Rows[i]["realPrice"] + "</td>");
                        sb.Append("<td><button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button></td></tr>");
                        bookNumList.Add(bookNum);
                        Session["List"] = bookNumList;
                    }
                    Response.Write(sb.ToString());
                    Response.End();
                }
            }
            else if (action == "save")
            {
                string json = Request["json"];
                DataTable dataTable = jsonToDt(json);
                int Count = dataTable.Rows.Count;
                Monomers monomers = new Monomers();
                BookBasicData book = new BookBasicData();
                int count, counts = 0;
                double total, allTotal = 0, real, allReal = 0;
                for (int i = 0; i < Count; i++)
                {
                    DataRow drow = dataTable.Rows[i];
                    book.Isbn = drow["ISBN号"].ToString();
                    book.Price = Convert.ToDouble(drow["单价"]);
                    book.BookNum = Convert.ToInt64(drow["书号"]);
                    monomers.Isbn = book;
                    monomers.UPrice = book;
                    monomers.BookNum = book;
                    monomers.Discount = Convert.ToDouble(drow["折扣"]);
                    monomers.MonomersId = Convert.ToInt32(drow["单据编号"]);
                    monomers.Number = Convert.ToInt32(drow["商品数量"]);
                    monomers.TotalPrice = Convert.ToDouble(drow["码洋"]);
                    monomers.RealPrice = Convert.ToDouble(drow["实洋"]);
                    SingleHead single = new SingleHead();
                    single.SingleHeadId = Session["id"].ToString();
                    monomers.SingleHeadId = single;
                    count = Convert.ToInt32(drow["商品数量"]);
                    counts = counts + count;
                    total = Convert.ToDouble(drow["码洋"]);
                    allTotal = allTotal + total;
                    real = Convert.ToDouble(drow["实洋"]);
                    allReal = allReal + real;
                    Result row = warehousingBll.insertMono(monomers);
                    if (row == Result.添加失败)
                    {
                        Response.Write("添加失败");
                        Response.End();
                    }
                    else if (row == Result.添加成功)
                    {

                        BookBasicData bookBasic = new BookBasicData();
                        bookBasic.Isbn = book.Isbn;
                        bookBasic.BookNum = Convert.ToInt64(drow["书号"]);
                        Stock stock = new Stock();
                        stock.StockNum = Convert.ToInt32(drow["商品数量"]);
                        stock.ISBN = bookBasic;
                        stock.BookNum = bookBasic;
                        stock.RegionId = user.ReginId;
                        GoodsShelves goodsShelves = new GoodsShelves();
                        int goodsShelf = Convert.ToInt32(drow["货架号"]);
                        goodsShelves.GoodsShelvesId = goodsShelf;
                        stock.GoodsShelvesId = goodsShelves;
                        StockBll stockBll = new StockBll();
                        Result results = stockBll.GetByBookNum(Convert.ToInt64(drow["书号"]), goodsShelf);
                        if (results == Result.记录不存在)
                        {
                            Result result = stockBll.insert(stock);
                            if (result == Result.添加失败)
                            {
                                Response.Write("添加失败");
                                Response.End();
                            }
                        }
                        else
                        {
                            int rows = stockBll.getStockNum(Convert.ToInt64(drow["书号"]), goodsShelf);
                            Result result = stockBll.update(Convert.ToInt32(drow["商品数量"]) + rows, goodsShelf, Convert.ToInt64(drow["书号"]));
                            if (result == Result.更新失败)
                            {
                                Response.Write("添加失败");
                                Response.End();
                            }
                        }
                    }
                }
                SingleHead singleHead = new SingleHead();
                singleHead.SingleHeadId = Session["id"].ToString();
                singleHead.AllBillCount = counts;
                singleHead.AllRealPrice = allReal;
                singleHead.AllTotalPrice = allTotal;
                Result head = warehousingBll.updateHead(singleHead);
                if (head == Result.更新成功)
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
            else if (action == "changeDiscount")
            {
                double discount = Convert.ToDouble(Request["discount"]);
                Result result = warehousingBll.updateDiscount(discount);
                if (result == Result.更新成功)
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
            else if (action=="checkNum")
            {
                int regId = user.ReginId.RegionId;
                long bookNum = Convert.ToInt64(Request["bookNum"]);
                int billCount = Convert.ToInt32(Request["count"]);
                DataSet dsGoods = stockBll.SelectByBookNum(bookNum, regId);
                int j = dsGoods.Tables[0].Rows.Count;
                if (dsGoods != null && dsGoods.Tables[0].Rows.Count > 0)
                {
                    int count = billCount;
                    int allCount = 0, allCounts = 0;
                    for (int i = 0; i < dsGoods.Tables[0].Rows.Count; i++)
                    {
                        allCount = Convert.ToInt32(dsGoods.Tables[0].Rows[i]["stockNum"]);
                        allCounts = allCounts + allCount;
                    }
                    if (billCount > allCounts)
                    {
                        Response.Write("库存不足");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("库存不足");
                    Response.End();
                }

            }
        }
        /// <summary>
        /// Json 字符串 转换为 DataTable数据集合
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public DataTable jsonToDt(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        //Columns
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        //Rows
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }
                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
        protected void permission()
        {
            FunctionBll functionBll = new FunctionBll();
            User user = (User)Session["user"];
            Role role = new Role();
            role = user.RoleId;
            int roleId = role.RoleId;
            dsPer = functionBll.SelectByRoleId(roleId);
            for (int i = 0; i < dsPer.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 1)
                {
                    funcOrg = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 2)
                {
                    funcRole = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 3)
                {
                    funcUser = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 4)
                {
                    funcGoods = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 5)
                {
                    funcCustom = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 6)
                {
                    funcLibrary = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 7)
                {
                    funcBook = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 8)
                {
                    funcPut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 9)
                {
                    funcOut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 10)
                {
                    funcSale = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 11)
                {
                    funcSaleOff = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 12)
                {
                    funcReturn = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 13)
                {
                    funcSupply = true;
                }
            }
        }
    }
}