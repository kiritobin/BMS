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

namespace bms.Web.BasicInfor
{
    using Result = Enums.OpResult;
    public partial class bookBasicManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 20, totalCount, intPageCount;
        public string search = "";
        public DataSet ds;
        BookBasicBll bookbll = new BookBasicBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            string op = Request["op"];
            if (op == "del")
            {
                long bookNum = long.Parse(Request["bookNum"]);
                Result row = isDelete();
                if (row == Result.记录不存在)
                {
                    Result result = bookbll.Delete(bookNum);
                    if (result == Result.删除成功)
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
                else
                {
                    Response.Write("在其他表中有关联不能删除");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        protected string getData()
        {
            currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string bookName = Request["bookName"];
            string bookNum = Request["bookNum"];
            string bookISBN = Request["bookISBN"];
            if ((bookName == "" || bookName == null) && (bookNum == null || bookNum == "") && (bookISBN == null || bookISBN == ""))
            {
                search = "";
            }
            else if ((bookName != "" && bookName != null) && (bookNum == null || bookNum == "") && (bookISBN == null || bookISBN == ""))
            {
                search = String.Format(" bookName= '{0}'", bookName);
            }
            else if ((bookName == "" || bookName == null) && (bookNum != "" && bookNum != null) && (bookISBN == null || bookISBN == ""))
            {
                search = "bookNum=' " + bookNum + "'";
            }
            else if ((bookName == "" || bookName == null) && (bookISBN != "" && bookISBN != null) && (bookNum == null || bookNum == ""))
            {
                search = "ISBN='" + bookISBN + "'";
            }
            else if ((bookName == "" || bookName == null) && (bookISBN != "" && bookISBN != null) && (bookNum != null && bookNum != ""))
            {
                search = "bookNum='" + bookNum + "' and ISBN='" + bookISBN + "'";
            }
            else if ((bookName != "" && bookName != null) && (bookNum != null && bookNum != "") && (bookISBN == null || bookISBN == ""))
            {
                search = String.Format(" bookName= '{0}' and bookNum = '{1}'", bookName, bookNum);
            }
            else if ((bookName != "" && bookName != null) && (bookNum == null || bookNum == "") && (bookISBN != null && bookISBN != ""))
            {
                search = String.Format(" bookName= '{0}' and ISBN='{1}'", bookName, bookISBN);
            }
            else
            {
                search = String.Format(" bookName= '{0}' and bookNum = '{1}' and ISBN='{2}'", bookName, bookNum, bookISBN);
            }
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "T_BookBasicData";
            tbd.OrderBy = "bookNum";
            tbd.StrColumnlist = "bookNum,ISBN,bookName,publishTime,price,supplier,catalog,author,remarks,dentification";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = bookbll.selectBypage(tbd, out totalCount, out intPageCount);

            //生成table
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["author"].ToString() + "</td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["price"].ToString() + "</td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["publishTime"].ToString() + "</td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["supplier"].ToString() + "</td >");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["catalog"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["remarks"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["dentification"].ToString() + "</td>");
                sb.Append("<td>" + "<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash-o fa-lg'></i></button></td></tr>");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }

        public Result isDelete()
        {
            string bookNum = Request["bookNum"];
            Result row = Result.记录不存在;
            if (bookbll.IsDelete("T_ReplenishmentMonomer", "bookNum", bookNum) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (bookbll.IsDelete("T_SellOffMonomer", "bookNum", bookNum) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (bookbll.IsDelete("T_SaleMonomer", "bookNo", bookNum) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (bookbll.IsDelete("T_Monomers", "bookNum", bookNum) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            return row;
        }
    }
}