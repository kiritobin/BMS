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
        public string userName, regionName;
        protected DataSet ds, dsGoods,dsPer;
        protected int pageSize=20, totalCount, intPageCount;
        public double discount;
        string singleHeadId;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail;
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
            string op = Request["op"];
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
        }

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
                    single.SingleHeadId = Session["singleHeadId"].ToString();
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
                        long bookNum = Convert.ToInt64(drow["书号"]);
                        int billCount = Convert.ToInt32(drow["商品数量"]);
                        DataSet dsGoods = stockBll.SelectByBookNum(bookNum, user.ReginId.RegionId);
                        for (int j = 0; j < dsGoods.Tables[0].Rows.Count; j++)
                        {
                            billCount = count;
                            int stockNum = Convert.ToInt32(dsGoods.Tables[0].Rows[j]["stockNum"]);
                            int goodsId = Convert.ToInt32(dsGoods.Tables[0].Rows[j]["goodsShelvesId"]);
                            if (billCount <= stockNum)
                            {
                                int a = stockNum - billCount;
                                Result result = stockBll.update(a, goodsId, bookNum);
                                if (result == Result.更新失败)
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
                                    Session["List"] = null;
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
                }
                SingleHead singleHead = new SingleHead();
                singleHead.SingleHeadId = Session["singleHeadId"].ToString();
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
            else if (action == "checkNum")
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
            userName = user.UserName;
            regionName = user.ReginId.RegionName;
            int roleId = user.RoleId.RoleId;
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
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 14)
                {
                    funcRetail = true;
                }
            }
        }
    }
}