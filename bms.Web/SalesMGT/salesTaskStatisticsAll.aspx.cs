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
    public partial class salesTaskStatisticsAll : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20;
        public string saletaskId, userName, customerName, startTime, finishTime;
        public string allkinds, allnumber = "0";
        public string alltotalprice = "0", allreadprice = "0";
        SaleTaskBll saletaskbll = new SaleTaskBll();
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        User user = new User();
        bool isNotAdmin;
        RoleBll robll = new RoleBll();
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (User)Session["user"];
            string userId = user.UserId;
            DataSet ds = robll.selectRole(userId);
            string roleName = ds.Tables[0].Rows[0]["roleName"].ToString();
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            int region = user.ReginId.RegionId;
            if (roleName != "超级管理员")
            {
                isNotAdmin = true;
                //time = "2018";
                DataSet dsSum = saletaskbll.getAllpriceRegion(region);
                allnumber = dsSum.Tables[0].Rows[0]["allNum"].ToString();
                alltotalprice = dsSum.Tables[0].Rows[0]["allPrice"].ToString();
                allreadprice = dsSum.Tables[0].Rows[0]["allRealPrice"].ToString();
                allkinds = dsSum.Tables[0].Rows[0]["allKinds"].ToString();
                //allkinds = saletaskbll.getAllkindsRegion(time, region).ToString();
            }
            else
            {
                //time = "2018";
                DataSet dsSum = saletaskbll.getAllprice();
                allnumber = dsSum.Tables[0].Rows[0]["allNum"].ToString();
                alltotalprice = dsSum.Tables[0].Rows[0]["allRealPrice"].ToString();
                allreadprice = dsSum.Tables[0].Rows[0]["allPrice"].ToString();
                //allkinds = saletaskbll.getAllkinds(time).ToString();
                allkinds = dsSum.Tables[0].Rows[0]["allKinds"].ToString();
            }
            user = (User)Session["user"];
            getData();
            //saletaskId = Session["saleId"].ToString();
            //getBasic();
            //print();
        }
        //public void getBasic()
        //{
        //    DataSet ds = saletaskbll.getSaleTaskStatistics(saletaskId);
        //    if (ds != null)
        //    {
        //        string number = ds.Tables[0].Rows[0]["number"].ToString();
        //        if (number == "" || number == null)
        //        {
        //            allnumber = "0";
        //            alltotalprice = "0";
        //            allreadprice = "0";
        //        }
        //        else
        //        {
        //            allnumber = ds.Tables[0].Rows[0]["number"].ToString();
        //            alltotalprice = ds.Tables[0].Rows[0]["totalPrice"].ToString();
        //            allreadprice = ds.Tables[0].Rows[0]["realPrice"].ToString();
        //        }
        //    }
        //    //统计种数
        //    allkinds = saletaskbll.getkindsBySaleTaskId(saletaskId).ToString();
        //    DataSet userds = saletaskbll.getcustomerName(saletaskId);
        //    if (userds != null && userds.Tables[0].Rows.Count>0)
        //    {
        //        userName = userds.Tables[0].Rows[0]["userName"].ToString();
        //        customerName = userds.Tables[0].Rows[0]["customerName"].ToString();
        //        startTime = userds.Tables[0].Rows[0]["startTime"].ToString();
        //        finishTime = userds.Tables[0].Rows[0]["finishTime"].ToString();
        //        if (finishTime == "" || finishTime == null)
        //        {
        //            finishTime = "此销售任务还未结束";
        //        }
        //    }
        //}
        //获取基础数据
        public string getData()
        {

            string bookNum = Request["bookNum"];
            string bookName = Request["bookName"];
            string regionName = Request["regionName"];
            string time = Request["time"];
            string customerName = Request["customerName"];
            string search = "";
            if (bookNum != null && bookNum != "")
            {
                search = "bookNum=" + bookNum + "'";
            }
            if (bookName != null && bookName != "")
            {
                if (search != null && search != "")
                {
                    search = search + " and bookName='" + "'";
                }
                else
                {
                    search = "bookName='" + bookNum + "'";
                }
            }
            if (regionName != null && regionName != "")
            {
                if (search != null && search != "")
                {
                    search = search + " and regionName='" + "'";
                }
                else
                {
                    search = "regionName=" + regionName + "'";
                }
            }

            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = @"t_salemonomer as sm,t_bookbasicdata as book,t_user as us,t_customer as ct,t_salehead as head,t_saletask as task,t_region as rg ";
            tb.OrderBy = @"sm.dateTime";
            tb.StrColumnlist = @" sm.ISBN,sm.bookNum,book.bookName,sm.unitPrice,sum(sm.number) as allNum,sum(sm.totalPrice) as allPrice,head.state,sm.dateTime,us.userID,us.userName,ct.customerID,ct.customerName,rg.regionName";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = search == "" ? search : search + " group by bookNum,bookName,ISBN,unitPrice";
            if (!isNotAdmin)
            {
                tb.StrWhere = @"(head.state=1 or head.state=2) and sm.saleTaskId = task.saleTaskId and task.userId = us.userID and sm.bookNum = book.bookNum AND sm.ISBN = book.ISBN and task.customerId = ct.customerID and us.regionId = rg.regionId and head.userId = us.userID AND head.regionId = rg.regionId AND head.saleTaskId = task.saleTaskId AND sm.saleHeadId = head.saleHeadId group by sm.bookNum";
            }
            else
            {
                tb.StrWhere = @"(head.state=1 or head.state=2) and rg.regionId=" + user.ReginId.RegionId + " and sm.saleTaskId = task.saleTaskId and task.userId = us.userID and sm.bookNum = book.bookNum AND sm.ISBN = book.ISBN and task.customerId = ct.customerID and us.regionId = rg.regionId and head.userId = us.userID AND head.regionId = rg.regionId AND head.saleTaskId = task.saleTaskId AND sm.saleHeadId = head.saleHeadId group by sm.bookNum";
            }
            //获取展示的客户数据
            ds = salemonbll.selectBypage(tb, out totalCount, out intPageCount);
            //生成table
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allNum"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allPrice"] + "</td>");
                strb.Append("<td>" + Convert.ToDateTime(ds.Tables[0].Rows[i]["datetime"]).ToString("yyyy/MM/dd") + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["userName"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"] + "</td>");
                strb.Append("</tr>");
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
        }
        //private void print()
        //{
        //    string op = Request["op"];
        //    if (op == "print")
        //    {
        //        SaleTaskBll saleTaskBll = new SaleTaskBll();
        //        DataSet ds = saleTaskBll.salesTaskStatistics(Session["saleId"].ToString());
        //        StringBuilder strb = new StringBuilder();
        //        strb.Append("<tbody>");
        //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //        {
        //            if (int.Parse(ds.Tables[0].Rows[i]["allnumber"].ToString()) != 0 && double.Parse(ds.Tables[0].Rows[i]["allrealPrice"].ToString()) != 0)
        //            {
        //                strb.Append("<tr><td>" + (i + 1) + "</td>");
        //                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"] + "</td>");
        //                strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"] + "</td>");
        //                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"] + "</td>");
        //                strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"] + "</td>");
        //                strb.Append("<td>" + ds.Tables[0].Rows[i]["allnumber"] + "</td>");
        //                strb.Append("<td>" + ds.Tables[0].Rows[i]["allrealPrice"] + "</td></tr>");
        //            }
        //        }
        //        strb.Append("</tbody>");
        //        Response.Write(strb.ToString());
        //        Response.End();
        //    }
        //}
    }
}