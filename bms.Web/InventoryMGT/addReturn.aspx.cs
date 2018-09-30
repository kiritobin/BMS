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

namespace bms.Web.InventoryMGT
{
    using Result = Enums.OpResult;
    public partial class addReturn : System.Web.UI.Page
    {

        public int totalCount, intPageCount, pageSize = 20, row, count = 0;
        GoodsShelvesBll shelfbll = new GoodsShelvesBll();
        UserBll userBll = new UserBll();
        protected DataSet ds, shelf;
        WarehousingBll wareBll = new WarehousingBll();
        StockBll stockBll = new StockBll();
        BookBasicBll basicBll = new BookBasicBll();
        string singId;
        protected void Page_Load(object sender, EventArgs e)
        {

            singId = Session["singId"].ToString();
            if (!IsPostBack)
            {
                getData();
                User user = (User)Session["user"];
                int regId = user.ReginId.RegionId;
                shelf = shelfbll.Select(regId);
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
            if (op == "add")
            {
                string singleHeadId = singId;
                long counts = wareBll.getCount(singleHeadId.ToString());
                long bookNum = Convert.ToInt64(Request["bookNum"]);
                int billCount = Convert.ToInt32(Request["billCount"].ToString());
                BookBasicData bookBasicData = basicBll.SelectById(Convert.ToInt64(bookNum));
                if (bookBasicData == null)
                {
                    Response.Write("书号不存在");
                    Response.End();
                }
                string isbn = bookBasicData.Isbn;
                double discount = Convert.ToDouble(bookBasicData.Remarks);
                if (discount > 1 && discount <= 10)
                {
                    discount = discount * 0.1;
                }
                else if (discount > 10)
                {
                    discount = discount * 0.01;
                }
                double uPrice = bookBasicData.Price;
                long monCount = wareBll.getCount(singleHeadId.ToString());
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
                monomers.Discount = discount;
                BookBasicData bookBasic = new BookBasicData();
                bookBasic.Isbn = isbn;
                bookBasic.Price = Convert.ToDouble(uPrice);
                bookBasic.BookNum = bookNum;
                monomers.BookNum = bookBasic;
                monomers.Isbn = bookBasic;
                monomers.UPrice = bookBasic;
                monomers.MonomersId = Convert.ToInt32(monId);
                monomers.Number = billCount;
                monomers.RealPrice = Convert.ToDouble((billCount * uPrice * discount).ToString("0.00"));
                SingleHead single = new SingleHead();
                single.SingleHeadId = singleHeadId.ToString();
                monomers.SingleHeadId = single;
                monomers.TotalPrice = Convert.ToDouble((billCount * uPrice).ToString("0.00"));
                monomers.Type = 2;
                Result reslt = wareBll.insertMono(monomers);
                if (reslt == Result.添加成功)
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
                        DataSet dsGoods = stockBll.SelectByIsbn(isbn);
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
            string op = Request["op"];
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "T_Monomers";
            tbd.OrderBy = "monId";
            tbd.StrColumnlist = "bookNum,monId,ISBN,number,uPrice,totalPrice,realPrice,discount";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "deleteState=0 and singleHeadId=" + singId;
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
    }
}