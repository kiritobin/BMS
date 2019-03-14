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
    public partial class salesTaskStatistics : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20;
        public string saletaskId, userName, customerName, startTime, finishTime, regionName;
        public int allkinds, allnumber;
        public double alltotalprice, allreadprice;
        SaleTaskBll saletaskbll = new SaleTaskBll();
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        public DataSet ds;
        string state;
        protected void Page_Load(object sender, EventArgs e)
        {
            saletaskId = Session["saleId"].ToString();
            //state = salemonbll.getsaleHeadStatesBysaleTaskId(saletaskId);
            getData();
            getBasic();
            //if (state == "3")
            //{
            //    getBasic("3");
            //}
            //else
            //{
            //    getBasic("1");
            //}
            //print();

            string op = Request["op"];
            if (op == "excel")
            {
                export();
            }
            //if (op == "excel" && state == "3")
            //{
            //    export("3");
            //}
            //else if (op == "excel")
            //{
            //    export("1");
            //}
            if (op == "print")
            {
                Response.Write(Print());
                Response.End();
            }
        }

        public String Print()
        {
            DataTable dt = saletaskbll.ExportExcels(saletaskId);
            StringBuilder strb = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1) + "</td>");
                strb.Append("<td>" + dt.Rows[i][2] + "</td>");
                strb.Append("<td>" + dt.Rows[i][0] + "</td>");
                strb.Append("<td>" + dt.Rows[i][1] + "</td>");
                strb.Append("<td>" + dt.Rows[i][3] + "</td>");
                strb.Append("<td>" + dt.Rows[i][4] + "</td>");
                strb.Append("<td>" + dt.Rows[i][5] + "</td>");
                strb.Append("<td>" + dt.Rows[i][6] + "</td>");
                strb.Append("<td>" + dt.Rows[i][7] + "</td></tr>");
            }
            return strb.ToString();
        }
        public void getBasic()
        {
            DataSet ds = saletaskbll.getSaleTaskStatistics(saletaskId);
            if (ds != null)
            {
                string number = ds.Tables[0].Rows[0]["number"].ToString();
                if (number == "" || number == null)
                {
                    allnumber = 0;
                    alltotalprice = 0;
                    allreadprice = 0;
                }
                else
                {
                    allnumber = int.Parse(ds.Tables[0].Rows[0]["number"].ToString());
                    alltotalprice = double.Parse(ds.Tables[0].Rows[0]["totalPrice"].ToString());
                    allreadprice = double.Parse(ds.Tables[0].Rows[0]["realPrice"].ToString());
                }
            }
            //统计种数
            allkinds = saletaskbll.getkindsBySaleTaskId(saletaskId);
            DataSet userds = saletaskbll.getcustomerName(saletaskId);
            if (userds != null)
            {
                userName = userds.Tables[0].Rows[0]["userName"].ToString();
                customerName = userds.Tables[0].Rows[0]["customerName"].ToString();
                //startTime = userds.Tables[0].Rows[0]["startTime"].ToString();
                startTime = Convert.ToDateTime(userds.Tables[0].Rows[0]["startTime"].ToString()).ToString("yyyy年MM月dd日");
                finishTime = userds.Tables[0].Rows[0]["finishTime"].ToString();
                regionName = userds.Tables[0].Rows[0]["regionName"].ToString();
                if (finishTime == "" || finishTime == null)
                {
                    finishTime = "此销售任务还未结束";
                }
                else
                {
                    finishTime = Convert.ToDateTime(finishTime).ToString("yyyy年MM月dd日");
                }
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        public void export()
        {
            DataTable dt = saletaskbll.ExportExcels(saletaskId);
            string name = "销售任务" + saletaskId;
            if (dt != null && dt.Rows.Count > 0)
            {
                var path = Server.MapPath("../uploads/export/SaleTaskExport/" + name + ".xlsx");
                ExcelHelper.x2007.TableToExcelForXLSX(dt, path);
                downloadfile(path);
            }
            else
            {
                Response.Write("<script language='javascript'>alert('查询不到数据，不能执行导出操作!')</script>");
            }
        }
        public void downloadfile(string s_path)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(s_path);
            HttpContext.Current.Response.ContentType = "application/ms-download";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            HttpContext.Current.Response.WriteFile(file.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.End();
        }

        //public string print()
        //{
        //    int currentPage = Convert.ToInt32(Request["page"]);
        //    if (currentPage == 0)
        //    {
        //        currentPage = 1;
        //    }
        //    TableBuilder tb = new TableBuilder();
        //    tb.StrTable = "V_SaleMonomer";
        //    tb.OrderBy = "dateTime";
        //    tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,sum(number) as allnumber ,sum(realPrice) as allrealPrice,sum(totalPrice) as totalPrice,regionName,supplier,author";
        //    tb.IntPageSize = pageSize;
        //    tb.IntPageNum = currentPage;
        //    tb.StrWhere = " saleTaskId='" + saletaskId + "' group by bookNum,bookName,ISBN,unitPrice";
        //    //获取展示的客户数据
        //    ds = salemonbll.selectBypage(tb, out totalCount, out intPageCount);
        //    //生成table
        //    StringBuilder strb = new StringBuilder();
        //    strb.Append("<tbody>");
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        if (int.Parse(ds.Tables[0].Rows[i]["allnumber"].ToString()) != 0 && double.Parse(ds.Tables[0].Rows[i]["allrealPrice"].ToString()) != 0)
        //        {
        //            strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
        //            strb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"] + "</td>");
        //            strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"] + "</td>");
        //            strb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"] + "</td>");
        //            strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"] + "</td>");
        //            strb.Append("<td>" + ds.Tables[0].Rows[i]["allnumber"] + "</td>");
        //            strb.Append("<td>" + ds.Tables[0].Rows[i]["author"] + "</td>");
        //            strb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"] + "</td>");
        //            strb.Append("<td>" + ds.Tables[0].Rows[i]["supplier"] + "</td></tr>");
        //        }
        //    }
        //    strb.Append("</tbody>");
        //    strb.Append("<input type='hidden' value='" + intPageCount + " ' id='intPageCount' />");
        //    strb.Append("<input type='hidden' value='" + ds.Tables[0].Rows[0]["regionName"] + " ' id='region' />");
        //    string op = Request["op"];
        //    if (op == "paging")
        //    {
        //        Response.Write(strb.ToString());
        //        Response.End();
        //    }
        //    return strb.ToString();
        //}

        //获取基础数据
        public string getData()
        {
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "v_allsalemonomer";
            tb.OrderBy = "dateTime desc";
            //tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,number ,realPrice ,totalPrice,regionName,supplier,author";

            tb.StrColumnlist = "state,bookNum,bookName,ISBN,unitPrice,sum(number) as allnumber ,sum(realPrice) as allrealPrice,sum(totalPrice) as totalPrice,regionName,supplier,author";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = " saleTaskId='" + saletaskId + "' group by bookNum,bookName,ISBN,unitPrice";
            //获取展示的客户数据
            ds = salemonbll.selectBypage(tb, out totalCount, out intPageCount);
            //生成table
            StringBuilder strb = new StringBuilder();
            if (ds.Tables[0].Rows.Count > 0)
            {
                strb.Append("<tbody>");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (int.Parse(ds.Tables[0].Rows[i]["allnumber"].ToString()) != 0 && double.Parse(ds.Tables[0].Rows[i]["allrealPrice"].ToString()) != 0)
                    {
                      
                        strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                        strb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"] + "</td>");
                        strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"] + "</td>");
                        strb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"] + "</td>");
                        strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"] + "</td>");
                        strb.Append("<td>" + ds.Tables[0].Rows[i]["allnumber"] + "</td>");
                        strb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"] + "</td></tr>");
                    }
                    //if (int.Parse(ds.Tables[0].Rows[i]["number"].ToString()) != 0 && double.Parse(ds.Tables[0].Rows[i]["number"].ToString()) != 0)
                    //{
                    //    strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                    //    strb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"] + "</td>");
                    //    strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"] + "</td>");
                    //    strb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"] + "</td>");
                    //    strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"] + "</td>");
                    //    strb.Append("<td>" + ds.Tables[0].Rows[i]["number"] + "</td>");
                    //    strb.Append("<td>" + ds.Tables[0].Rows[i]["totalPrice"] + "</td></tr>");
                    //}
                }
                strb.Append("</tbody>");
                strb.Append("<input type='hidden' value='" + intPageCount + " ' id='intPageCount' />");
                strb.Append("<input type='hidden' value='" + ds.Tables[0].Rows[0]["regionName"] + " ' id='region' />");
            }
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
        }
    }
}