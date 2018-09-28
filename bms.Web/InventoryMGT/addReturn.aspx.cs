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
        WarehousingBll warebll = new WarehousingBll();
        StockBll stockBll = new StockBll();
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
                string singleHeadId = Session["returnId"].ToString();
                long counts = warebll.getCount(Convert.ToInt64(singleHeadId));
                long monId;
                if (counts > 0)
                {
                    monId = counts + 1;
                }
                else
                {
                    monId = 1;
                }
                string ISBN = Request["addISBN"];
                int Num = int.Parse(Request["addNum"].ToString());
                string Price = Request["addPrice"];
                string Discount = Request["addDiscount"];
                string TotalPrice = Request["addTotalPrice"];
                string Ocean = Request["addOcean"];
                Monomers mon = new Monomers();
                SingleHead newHead = new SingleHead()
                {
                    SingleHeadId = singleHeadId
                };
                mon.SingleHeadId = newHead;
                mon.MonomersId = Convert.ToInt32(monId);
                mon.Number = Num;
                BookBasicData newBook = new BookBasicData()
                {
                    Price = int.Parse(Price),
                    Isbn = ISBN
                };
                mon.UPrice = newBook;
                mon.Isbn = newBook;
                mon.Discount = int.Parse(Discount);

                mon.TotalPrice = double.Parse(TotalPrice);
                mon.RealPrice = int.Parse(Ocean);
                mon.Type = 2;
                Result reslt = warebll.insertMono(mon);
                if (reslt == Result.添加成功)
                {
                    Response.Write("添加成功");
                    Response.End();
                    DataSet dsGoods = stockBll.SelectByIsbn(ISBN);
                    int count = Num;
                    int allCount = 0, allCounts = 0;
                    for (int i = 0; i < dsGoods.Tables[0].Rows.Count; i++)
                    {
                        allCount = Convert.ToInt32(dsGoods.Tables[0].Rows[i]["stockNum"]);
                    }
                    allCounts = allCounts + allCount;
                    if (Num > allCounts)
                    {
                        Response.Write("库存数量不足");
                        Response.End();
                    }
                    else
                    {
                        for (int i = 0; i < dsGoods.Tables[0].Rows.Count; i++)
                        {
                            Num = count;
                            int stockNum = Convert.ToInt32(dsGoods.Tables[0].Rows[i]["stockNum"]);
                            int goodsId = Convert.ToInt32(dsGoods.Tables[0].Rows[i]["goodsShelvesId"]);
                            if (Num <= stockNum)
                            {
                                int a = stockNum - Num;
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
                                count = Num - stockNum;
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
            if (op == "del")
            {

                int monId = Convert.ToInt32(Request["ID"]);
                Result row = warebll.deleteMonomer(singId, monId,2);
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
            tbd.StrTable = "V_Monomers";
            tbd.OrderBy = "monId";
            tbd.StrColumnlist = "monId,ISBN,number,uPrice,totalPrice,realPrice,discount,shelvesName";
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
                sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                sb.Append("<td>" + dr["number"].ToString() + "</td>");
                sb.Append("<td>" + dr["uPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["discount"].ToString() + "</td>");
                sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["shelvesName"].ToString() + "</td>");
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