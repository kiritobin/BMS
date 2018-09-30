using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
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
        protected DataSet ds, dsGood;
        protected int pageSize=20, totalCount, intPageCount;
        public double dsicount;
        string singleHeadId;
        UserBll userBll = new UserBll();
        WarehousingBll warehousingBll = new WarehousingBll();
        StockBll stockBll = new StockBll();
        BookBasicBll basicBll = new BookBasicBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            Monomers monoDiscount = warehousingBll.getDiscount();
            dsicount = monoDiscount.Discount;
            if (!IsPostBack)
            {
                singleHeadId = Request.QueryString["sId"];
                if(singleHeadId!="" && singleHeadId != null)
                {
                    Session["singleHeadId"] = singleHeadId;
                }
                else
                {
                    singleHeadId = Session["singleHeadId"].ToString();
                }
            }
            getData();
            getIsbn();
            User user = (User)Session["user"];
            string op = Request["op"];
            if(op == "add")
            {
                long bookNum = Convert.ToInt64(Request["bookNum"]);
                BookBasicData bookBasicData = basicBll.SelectById(Convert.ToInt64(bookNum));
                string isbn = bookBasicData.Isbn;
                int billCount = Convert.ToInt32(Request["billCount"]);
                DataSet dsGoods = stockBll.SelectByIsbn(isbn);
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
                        }else if(discount > 10)
                        {
                            discount = discount * 0.01;
                        }
                        double uPrice = bookBasicData.Price;
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
                        monomers.RealPrice = Convert.ToDouble((billCount * uPrice* discount).ToString("0.00"));
                        monomers.TotalPrice = Convert.ToDouble((billCount* uPrice).ToString("0.00"));
                        monomers.Type = 0;
                        SingleHead single = new SingleHead();
                        single.SingleHeadId = singleHeadId;
                        monomers.SingleHeadId = single;
                        Result res = warehousingBll.updateDiscount(discount);
                        if (res == Result.更新成功)
                        {
                            Result row = warehousingBll.insertMono(monomers);
                            if (row == Result.添加成功)
                            {
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
                                            Result result = stockBll.update(a, goodsId);
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
                                            Result result = stockBll.update(0, goodsId);
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
                }
                else
                {
                    Response.Write("库存不足");
                    Response.End();
                }

            }
            if(op == "del")
            {
                int Id = Convert.ToInt32(Request["ID"]);
                Result row = warehousingBll.deleteMonomer(singleHeadId, Id,0);
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
            GoodsShelvesBll goods = new GoodsShelvesBll();
            int regionId = user.ReginId.RegionId;
            dsGood = goods.Select(regionId);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string op = Request["op"];
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_Monomer";
            tbd.OrderBy = "monId";
            tbd.StrColumnlist = "bookName,supplier,bookNum,singleHeadId,monId,ISBN,number,uPrice,totalPrice,realPrice,discount,type";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "deleteState=0 and singleHeadId='" + singleHeadId+"'";
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);

            //生成table
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<tbody>");
            int count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                sb.Append("<tr><td>" + dr["monId"].ToString() + "</td>");
                sb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
                sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                sb.Append("<td>" + dr["supplier"].ToString() + "</td>");
                sb.Append("<td>" + dr["number"].ToString() + "</td>");
                sb.Append("<td>" + dr["uPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["discount"].ToString() + "</td>");
                sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                sb.Append("<td><input type='hidden' value='" + dr["monId"].ToString() + "'/>");
                sb.Append("<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash'></i></button></td></tr>");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }

        public string getIsbn()
        {
            string op = Request["op"];
            string isbn = Request["isbn"];
            if (isbn != null && isbn != "")
            {
                DataSet dsBook = basicBll.SelectByIsbn(isbn);
                StringBuilder sb = new StringBuilder();
                if (dsBook == null || dsBook.Tables[0].Rows.Count <= 0)
                {
                    Response.Write("ISBN不存在");
                    Response.End();
                }
                else
                {
                    //生成table
                    sb.Append("<tbody id='tbody'>");
                    int count = dsBook.Tables[0].Rows.Count;
                    for (int i = 0; i < count; i++)
                    {
                        DataRow dr = dsBook.Tables[0].Rows[i];
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
                }
                return sb.ToString();
            }
            return null;
        }
    }
}