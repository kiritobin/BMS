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
    public partial class searchSalesDetail : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20;
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        SaleHeadBll saleHeadbll = new SaleHeadBll();
        public DataSet ds, headBasicds;
        public string saleheadId, saletaskId, time, userName, customerName;
        public int allkinds, allnumber;
        public double alltotalprice, allreadprice;
        protected void Page_Load(object sender, EventArgs e)
        {
            string exportOp = Request.QueryString["op"];
            if (exportOp == "export")
            {
                export();
            }
            SaleTaskBll saletaskBll = new SaleTaskBll();
            saletaskId = Session["saleId"].ToString();
            DataSet userds = saletaskBll.getcustomerName(saletaskId);
            customerName = userds.Tables[0].Rows[0]["customerName"].ToString();
            getData();
            getSaleHeadBasic();
            print();
        }
        //获取单头信息
        public void getSaleHeadBasic()
        {
            headBasicds = saleHeadbll.getSaleHeadBasic(saletaskId, saleheadId);
            allkinds = int.Parse(salemonbll.getkinds(saletaskId, saleheadId).ToString());
            DataSet ds = salemonbll.calculationSaleHead(saleheadId, saletaskId);
            if (ds == null)
            {
                allnumber = 0;
                alltotalprice = 0;
                allreadprice = 0;
            }
            else
            {
                allnumber = int.Parse(ds.Tables[0].Rows[0]["数量"].ToString());
                alltotalprice = double.Parse(ds.Tables[0].Rows[0]["总码洋"].ToString());
                allreadprice = double.Parse(ds.Tables[0].Rows[0]["总实洋"].ToString());
            }

            //allkinds = int.Parse(headBasicds.Tables[0].Rows[0]["kindsNum"].ToString());
            //allnumber = int.Parse(headBasicds.Tables[0].Rows[0]["number"].ToString());
            //alltotalprice = double.Parse(headBasicds.Tables[0].Rows[0]["allTotalPrice"].ToString());
            //allreadprice = double.Parse(headBasicds.Tables[0].Rows[0]["allRealPrice"].ToString());
            time = headBasicds.Tables[0].Rows[0]["dateTime"].ToString();
            userName = headBasicds.Tables[0].Rows[0]["userName"].ToString();
        }

        //获取基础数据
        public string getData()
        {
            saleheadId = Session["saleheadId"].ToString();
            saletaskId = Session["saleId"].ToString();

            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SaleMonomer";
            tb.OrderBy = "dateTime";
            tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,realDiscount,sum(number) as allnumber ,sum(realPrice) as allrealPrice,userName,customerName,regionName";
            //tb.StrColumnlist = "bookNum,bookName,ISBN,unitPrice,number,realDiscount,realPrice,dateTime,alreadyBought";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = "saleTaskId='" + saletaskId + "' and saleHeadId='" + saleheadId + "' group by bookNum,bookName,ISBN,unitPrice HAVING allnumber!=0";

            // tb.StrWhere = search == "" ? "deleteState=0 and saleHeadId=" + "'" + saleheadId + "'" + " and saleTaskId=" + "'" + saletaskId + "'" : search + " and deleteState=0 and saleHeadId=" + "'" + saleheadId + "'" + " and saleTaskId=" + "'" + saletaskId + "'";
            //获取展示的客户数据
            ds = salemonbll.selectBypage(tb, out totalCount, out intPageCount);
            //生成table
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allnumber"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"] + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allrealPrice"] + "</td>");
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            strb.Append("<input type='hidden' value=' " + customerName + " ' id='customer' />");
            if (ds.Tables[0].Rows.Count > 0 && ds != null)
            {
                strb.Append("<input type='hidden' value=' " + ds.Tables[0].Rows[0]["regionName"] + " ' id='region' />");
            }
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
        }

        /// <summary>
        /// //导出列表方法
        /// </summary>
        /// <param name="s_path">文件路径</param>
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
        /// <summary>
        /// 导出
        /// </summary>
        public void export()
        {
            string name = "销售明细" + Session["saleheadId"].ToString() + "-" + DateTime.Now.ToString("yyyyMMdd") + new Random(DateTime.Now.Second).Next(10000);
            DataTable dt = salemonbll.ExportExcel(Session["saleheadId"].ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                var path = Server.MapPath("~/download/销售明细导出/" + name + ".xlsx");
                ExcelHelper.x2007.TableToExcelForXLSX(dt, path);
                downloadfile(path);
            }
            else
            {
                Response.Write("<script>alert('没有数据，不能执行导出操作!');</script>");
                Response.End();
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        private void print()
        {
            saleheadId = Session["saleheadId"].ToString();
            saletaskId = Session["saleId"].ToString();
            string op = Request["op"];
            if (op == "print")
            {
                StringBuilder sb = new StringBuilder();
                SellOffMonomerBll sellOffMonomerBll = new SellOffMonomerBll();
                DataSet dataSet = sellOffMonomerBll.searchSalesDetail(saletaskId, saleheadId);
                DataRowCollection drc = dataSet.Tables[0].Rows;
                int datacount = dataSet.Tables[0].Rows.Count;
                for (int i = 0; i < datacount; i++)
                {
                    sb.Append("<tr><td>" + (i + 1) + "</td>");
                    sb.Append("<td>" + dataSet.Tables[0].Rows[i]["ISBN"] + "</td>");
                    sb.Append("<td>" + dataSet.Tables[0].Rows[i]["bookNum"] + "</td>");
                    sb.Append("<td>" + dataSet.Tables[0].Rows[i]["bookName"] + "</td>");
                    sb.Append("<td>" + dataSet.Tables[0].Rows[i]["unitPrice"] + "</td>");
                    sb.Append("<td>" + dataSet.Tables[0].Rows[i]["allnumber"] + "</td>");
                    sb.Append("<td>" + dataSet.Tables[0].Rows[i]["realDiscount"] + "</td>");
                    sb.Append("<td>" + dataSet.Tables[0].Rows[i]["allrealPrice"] + "</td></tr>");
                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["ISBN"] + "</td>");
                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["bookNum"] + "</td>");
                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"] + "</td>");
                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"] + "</td>");
                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["allnumber"] + "</td>");
                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["realDiscount"] + "</td>");
                    //sb.Append("<td>" + ds.Tables[0].Rows[i]["allrealPrice"] + "</td></tr>");
                }
                Response.Write(sb);
                Response.End();
            }
        }
    }
}