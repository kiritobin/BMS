using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using System.Data;
using bms.Model;
using System.Text;

namespace bms.Web.InventoryMGT
{
    using Result = Enums.OpResult;
    public partial class addReturn : System.Web.UI.Page
    {
        public double discount;
        public int totalCount, intPageCount, pageSize = 20, row, count = 0;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply;
        GoodsShelvesBll shelfbll = new GoodsShelvesBll();
        UserBll userBll = new UserBll();
        protected DataSet ds, shelf,dsPer;
        WarehousingBll wareBll = new WarehousingBll();
        StockBll stockBll = new StockBll();
        BookBasicBll basicBll = new BookBasicBll();
        string singId, singleHeadId;
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
            Monomers monoDiscount = wareBll.getDiscount();
            discount = monoDiscount.Discount;
            singId = Session["singId"].ToString();
            if (!IsPostBack)
            {
                User user = (User)Session["user"];
                int regId = user.ReginId.RegionId;
                shelf = shelfbll.Select(regId);
            }
            getData();
            getIsbn();
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
            if (op == "add")
            {
                long bookNum = Convert.ToInt64(Request["bookNum"]);
                BookBasicData bookBasicData = basicBll.SelectById(Convert.ToInt64(bookNum));
                string isbn = bookBasicData.Isbn;
                int billCount = Convert.ToInt32(Request["billCount"]);
                DataSet dsGoods = stockBll.SelectByBookNum(bookNum);
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
                        double discount = Convert.ToInt32(Request["disCount"]);
                        if (discount > 1 && discount <= 10)
                        {
                            discount = discount * 0.1;
                        }
                        else if (discount > 10)
                        {
                            discount = discount * 0.01;
                        }
                        double uPrice = bookBasicData.Price;
                        long monCount = wareBll.getCount(singleHeadId);
                        long monId;
                        if (monCount > 0)
                        {
                            monId = monCount + 1;
                        }
                        else
                        {
                            monId = 1;
                        }
                        Monomers monomers = new Monomers();
                        BookBasicData bookBasic = new BookBasicData();
                        bookBasic.Isbn = isbn;
                        bookBasic.Price = Convert.ToDouble(uPrice);
                        bookBasic.BookNum = bookNum;
                        monomers.BookNum = bookBasic;
                        monomers.Discount = discount;
                        monomers.Isbn = bookBasic;
                        monomers.UPrice = bookBasic;
                        monomers.MonomersId = Convert.ToInt32(monId);
                        monomers.Number = billCount;
                        monomers.RealPrice = Convert.ToDouble((billCount * uPrice * discount).ToString("0.00"));
                        monomers.TotalPrice = Convert.ToDouble((billCount * uPrice).ToString("0.00"));
                        monomers.Type = 2;
                        SingleHead single = new SingleHead();
                        single.SingleHeadId = singleHeadId;
                        monomers.SingleHeadId = single;
                        Result res = wareBll.updateDiscount(discount);
                        Result re = wareBll.SelectBybookNum(singleHeadId, bookNum.ToString(), 0);
                        if (re == Result.记录不存在)
                        {
                            if (res == Result.更新成功)
                            {
                                Result row = wareBll.insertMono(monomers);
                                if (row == Result.添加成功)
                                {
                                    int number, allBillCount = 0;
                                    double totalPrice, allTotalPrice = 0, realPrice, allRealPrice = 0;
                                    DataTable dtHead = wareBll.SelectMonomers(singleHeadId);
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
                                    Result update = wareBll.updateHead(single);
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
                                else
                                {
                                    Response.Write("添加失败");
                                    Response.End();
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

                int monId = Convert.ToInt32(Request["ID"]);
                Result row = wareBll.deleteMonomer(singId, monId,2);
                if (row == Result.删除成功)
                {
                    Response.Write("删除成功");
                    Response.End();
                }
                else
                {
                    Response.Write("删除成功");
                    Response.End();
                }
            }

        }
        public string getData()
        {
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string search = "";
            string singleHeadId = Request["ID"];
            string bookNum = Request["bookNum"];
            string isbn = Request["isbn"];
            if ((singleHeadId == "" || singleHeadId == null) && (bookNum == "" || bookNum == null) && (isbn == "" || isbn == null))
            {
                search = "deleteState=0 and type=2";
            }
            else if (singleHeadId != "" && singleHeadId != null && (bookNum == "" || bookNum == null) && (isbn == "" || isbn == null))
            {
                search = "deleteState=0 and type=2 and singleHeadId='" + singleHeadId + "'";
            }
            else if (bookNum != "" && bookNum != null && (singleHeadId == "" || singleHeadId == null) && (isbn == "" || isbn == null))
            {
                search = "deleteState=0 and type=2 and bookNum='" + bookNum + "'";
            }
            else if (isbn != "" && isbn != null && (bookNum == "" || bookNum == null) && (singleHeadId == "" || singleHeadId == null))
            {
                search = "deleteState=0 and type=2 and ISBN='" + isbn + "'";
            }
            else if (isbn != "" && isbn != null && bookNum != "" && bookNum != null && (singleHeadId == "" || singleHeadId == null))
            {
                search = "deleteState=0 and type=2 and ISBN='" + isbn + "' and bookNum='" + bookNum + "'";
            }
            else if (isbn != "" && isbn != null && singleHeadId != "" && singleHeadId != null && (bookNum == "" || bookNum == null))
            {
                search = "deleteState=0 and type=2 and ISBN='" + isbn + "' and singleHeadId='" + singleHeadId + "'";
            }
            else if (singleHeadId != "" && singleHeadId != null && bookNum != "" && bookNum != null && (isbn == "" || isbn == null))
            {
                search = "deleteState=0 and type=2 and singleHeadId='" + singleHeadId + "' and bookNum='" + bookNum + "'";
            }
            else
            {
                search = "deleteState=0 and type=2 and singleHeadId='" + singleHeadId + "' and bookNum='" + bookNum + "' and ISBN='" + isbn + "'";
            }
            string op = Request["op"];
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "T_Monomers";
            tbd.OrderBy = "monId";
            tbd.StrColumnlist = "bookNum,monId,ISBN,number,uPrice,totalPrice,realPrice,discount";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);

            //生成table
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<tbody>");
            int count = ds.Tables[0].Rows.Count;
            DataTable dt = ds.Tables[0];
            for (int i = 0; i < count; i++)
            {
                DataRow dr = dt.Rows[i];
                sb.Append("<tr><td>" + dr["monId"].ToString() + "</td>");
                sb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
                sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                sb.Append("<td>" + dr["number"].ToString() + "</td>");
                sb.Append("<td>" + dr["uPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["discount"].ToString() + "</td>");
                sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                sb.Append("<td>" + "<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash'></i></button></td></tr>");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount'/>");
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }

        public string getIsbn()
        {
            SingleHead single = new SingleHead();
            string op = Request["op"];
            string isbn = Request["isbn"];
            double disCount = Convert.ToDouble(Request["disCount"]);
            int billCount = Convert.ToInt32(Request["billCount"]);
            if (isbn != null && isbn != "")
            {
                BookBasicBll bookBasicBll = new BookBasicBll();
                DataSet bookDs = bookBasicBll.SelectByIsbn(isbn);
                if (bookDs != null && bookDs.Tables[0].Rows.Count > 0)
                {
                    if (disCount > 1 && disCount <= 10)
                    {
                        disCount = disCount * 0.1;
                    }
                    else if (disCount > 10)
                    {
                        disCount = disCount * 0.01;
                    }
                    double price = Convert.ToDouble(bookDs.Tables[0].Rows[0]["price"]);
                    long bookNum = Convert.ToInt64(bookDs.Tables[0].Rows[0]["bookNum"]);
                    double totalPrice = Convert.ToDouble((billCount * price).ToString("0.00"));
                    double realPrice = Convert.ToDouble((totalPrice * disCount).ToString("0.00"));
                    string singleId = Session["singleHeadId"].ToString();
                    long monCount = wareBll.getCount(singleHeadId);
                    long monId;
                    if (monCount > 0)
                    {
                        monId = monCount + 1;
                    }
                    else
                    {
                        monId = 1;
                    }
                    StringBuilder sb = new StringBuilder();
                    if (bookDs.Tables[0].Rows.Count == 1)
                    {
                        DataSet dsBooks = stockBll.SelectByBookNum(bookNum);
                        if (dsBooks != null && dsBooks.Tables[0].Rows.Count > 0)
                        {
                            int count = billCount;
                            int allCount = 0, allCounts = 0;
                            for (int i = 0; i < dsBooks.Tables[0].Rows.Count; i++)
                            {
                                allCount = Convert.ToInt32(dsBooks.Tables[0].Rows[i]["stockNum"]);
                                allCounts = allCounts + allCount;
                            }
                            if (billCount > allCounts)
                            {
                                Response.Write("库存数量不足");
                                Response.End();
                            }
                            else
                            {
                                //添加单体
                                Monomers monomers = new Monomers();
                                BookBasicData bookBasic = new BookBasicData();
                                SingleHead singleHead = new SingleHead();
                                bookBasic.Isbn = isbn;
                                bookBasic.Price = price;
                                bookBasic.BookNum = bookNum;
                                singleHead.SingleHeadId = singleHeadId;
                                monomers.Isbn = bookBasic;
                                monomers.UPrice = bookBasic;
                                monomers.BookNum = bookBasic;
                                monomers.Discount = disCount * 100;
                                monomers.MonomersId = Convert.ToInt32(monId);
                                monomers.Number = billCount;
                                monomers.TotalPrice = totalPrice;
                                monomers.RealPrice = realPrice;
                                monomers.SingleHeadId = singleHead;
                                monomers.Type = 2;
                                Result re = wareBll.SelectBybookNum(singleHeadId, bookNum.ToString(), 0);
                                Result res = wareBll.updateDiscount(disCount * 100);
                                if (re == Result.记录不存在)
                                {
                                    if (res == Result.更新成功)
                                    {
                                        Result row = wareBll.insertMono(monomers);
                                        if (row == Result.添加成功)
                                        {//更新单头信息
                                            int number, allBillCount = 0;
                                            double totalPrices, allTotalPrice = 0, realPrices, allRealPrice = 0;
                                            DataTable dtHead = wareBll.SelectMonomers(singleHeadId);
                                            int j = dtHead.Rows.Count;
                                            for (int i = 0; i < j; i++)
                                            {
                                                DataRow dr = dtHead.Rows[i];
                                                number = Convert.ToInt32(dr["number"]);
                                                totalPrices = Convert.ToDouble(dr["totalPrice"]);
                                                realPrices = Convert.ToDouble(dr["realPrice"]);
                                                allBillCount = allBillCount + number;
                                                allTotalPrice = allTotalPrice + totalPrices;
                                                allRealPrice = allRealPrice + realPrices;
                                            }
                                            single.SingleHeadId = singleHeadId;
                                            single.AllBillCount = allBillCount;
                                            single.AllTotalPrice = allTotalPrice;
                                            single.AllRealPrice = allRealPrice;
                                            Result update = wareBll.updateHead(single);
                                            if (update == Result.更新成功)
                                            {//更新库存信息
                                                for (int i = 0; i < dsBooks.Tables[0].Rows.Count; i++)
                                                {
                                                    billCount = count;
                                                    int stockNum = Convert.ToInt32(dsBooks.Tables[0].Rows[i]["stockNum"]);
                                                    int goodsId = Convert.ToInt32(dsBooks.Tables[0].Rows[i]["goodsShelvesId"]);
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
                                        else
                                        {
                                            Response.Write("添加失败");
                                            Response.End();
                                        }
                                    }
                                    else
                                    {
                                        Response.Write("添加失败");
                                        Response.End();
                                    }
                                }
                                else
                                {
                                    Response.Write("已添加过相同记录");
                                    Response.End();
                                }
                            }
                        }
                        else
                        {
                            Response.Write("库存数量不足");
                            Response.End();
                        }
                    }
                    else
                    {
                        //生成table
                        sb.Append("<tbody id='tbody'>");
                        int counts = bookDs.Tables[0].Rows.Count;
                        for (int i = 0; i < counts; i++)
                        {
                            DataRow dr = bookDs.Tables[0].Rows[i];
                            //sb.Append("<tr><td><input type='checkbox' name='checkbox' class='check' value='" + dr["bookNum"].ToString() + "' /></td>");
                            //sb.Append("<tr><td><input type='radio' name='radio' class='radio' value='" + dr["bookNum"].ToString() + "' /></td>");
                            sb.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + dr["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                            sb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
                            sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                            sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                            sb.Append("<td>" + dr["price"].ToString() + "</td>");
                            sb.Append("<td>" + dr["supplier"].ToString() + "</td></tr>");
                        }
                        sb.Append("</tbody>");
                        if (op == "isbn")
                        {
                            Response.Write(sb.ToString());
                            Response.End();
                        }
                        return sb.ToString();
                    }
                }
                else
                {
                    Response.Write("ISBN不存在");
                    Response.End();
                }
            }
            return null;
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