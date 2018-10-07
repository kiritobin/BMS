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
    using Result = Enums.OpResult;
    public partial class retail : System.Web.UI.Page
    {
        protected DataSet ds;
        protected int pageSize = 20, totalCount, intPageCount;
        public double discount;
        string singleHeadId;
        SaleHead single = new SaleHead();
        UserBll userBll = new UserBll();
        SaleMonomerBll retailBll = new SaleMonomerBll();
        StockBll stockBll = new StockBll();
        BookBasicBll basicBll = new BookBasicBll();
        GoodsShelvesBll goods = new GoodsShelvesBll();
        DataTable monTable = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
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
            if (op == "add")
            {
                add();
            }
            if(op == "insert")
            {
                insert();
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
            tbd.StrTable = "V_Monomer";
            tbd.OrderBy = "monId";
            tbd.StrColumnlist = "bookName,supplier,bookNum,singleHeadId,monId,ISBN,number,uPrice,totalPrice,realPrice,discount,type";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "deleteState=0 and singleHeadId='" + singleHeadId + "'";
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
            double disCount = Convert.ToDouble(Request["disCount"]);
            int billCount = Convert.ToInt32(Request["billCount"]);
            if (isbn != null && isbn != "")
            {
                BookBasicBll bookBasicBll = new BookBasicBll();
                DataSet bookDs = bookBasicBll.SelectByIsbn(isbn);
                if (bookDs != null && bookDs.Tables[0].Rows.Count > 0)
                {
                    add();
                }
                else
                {
                    Response.Write("ISBN不存在");
                    Response.End();
                }
            }
            return null;
        }
        public void add()
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
                    long monCount = retailBll.SelectBySaleHeadId(singleHeadId);
                    long monId;
                    if (monCount > 0)
                    {
                        monId = monCount + 1;
                    }
                    else
                    {
                        monId = 1;
                    }
                    SaleMonomer monomers = new SaleMonomer();
                    double totalPrice = Convert.ToDouble((billCount * uPrice).ToString("0.00"));
                    double realPrice = Convert.ToDouble((totalPrice * discount).ToString("0.00"));
                    Result re = retailBll.SelectBybookNum(singleHeadId, bookNum.ToString());
                    if (re == Result.记录不存在)
                    {
                        DataRow monRow = monTable.NewRow();
                        monRow["ISBN"] = isbn;
                        monRow["unitPrice"] = uPrice;
                        monRow["bookNum"] = bookNum;
                        monRow["realDiscount"] = discount * 100;
                        monRow["retailMonomerId"] = Convert.ToInt32(monId);
                        monRow["number"] = billCount;
                        monRow["totalPrice"] = totalPrice;
                        monRow["realPrice"] = realPrice;
                        monRow["retailHeadId"] = singleHeadId;
                        monTable.Rows.Add(monRow);
                    }
                    else
                    {
                        Response.Write("已添加过此书籍，如需继续添加，可修改数量");
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

        public void insert()
        {
            SaleMonomer monomers = new SaleMonomer();
            int Count = monTable.Rows.Count;
            for (int i = 0; i < Count; i++)
            {
                DataRow dr = monTable.Rows[i];
                monomers.ISBN1 = dr["ISBN"].ToString();
                monomers.UnitPrice = Convert.ToDouble(dr["unitPrice"]);
                monomers.BookNum = Convert.ToInt64(dr["bookNum"]);
                monomers.RealDiscount = Convert.ToDouble(dr["realDiscount"]);
                monomers.SaleIdMonomerId = Convert.ToInt32(dr["retailMonomerId"]);
                monomers.Number = Convert.ToInt32(dr["number"]);
                monomers.TotalPrice = Convert.ToDouble(dr["totalPrice"]);
                monomers.RealPrice = Convert.ToDouble(dr["realPrice"]);
                monomers.SaleHeadId = dr["retailHeadId"].ToString();
                Result row = retailBll.Insert(monomers);
                if (row == Result.添加失败)
                {
                    Response.Write("添加失败");
                    Response.End();
                }
            }
            Response.Write("添加成功");
            Response.End();
        }
    }
}