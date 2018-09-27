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
        string singId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                singId = Session["singId"].ToString();
            }
            getData();
            User user = (User)Session["user"];
            int regId = user.ReginId.RegionId;
            shelf = shelfbll.Select(regId);
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
                long count = warebll.getCount(Convert.ToInt64(singleHeadId));
                long monId;
                if (count > 0)
                {
                    monId = count + 1;
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
                string shelfId = Request["shelfId"];
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
                GoodsShelves newShelf = new GoodsShelves()
                {
                    GoodsShelvesId = int.Parse(shelfId)
                };
                mon.GoodsShelvesId = newShelf;
                mon.Type = 2;
                Result reslt = warebll.insertMono(mon);
                if (reslt == Result.添加成功)
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
            if (op == "del")
            {

                int monId = Convert.ToInt32(Request["ID"]);
                Result row = warebll.deleteMonomer(Session["returnId"].ToString(), monId);
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
            tbd.StrWhere = "deleteState=0 and singleHeadId=" + Session["returnId"].ToString();
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
                sb.Append("<tr><td id='monId'>" + dr["monId"].ToString() + "</td>");
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