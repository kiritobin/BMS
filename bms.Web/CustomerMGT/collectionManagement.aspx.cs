using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.CustomerMGT
{
    public partial class collectionManagement : System.Web.UI.Page
    {
        public int totalCount, intPageCount,pageSize=20;
        public DataSet ds,dsCustom;
        RegionBll regionBll = new RegionBll();
        UserBll userBll = new UserBll();
        string row , custom;
        LibraryCollectionBll libraryCollectionBll = new LibraryCollectionBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            custom = Request["custom"];
            string action = Request["action"];
            if (action=="import")
            {
                UserBll userBll = new UserBll();
                DataTable dtInsert = new DataTable();
                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                dtInsert = differentDt();
                TimeSpan ts = watch.Elapsed;
                dtInsert.TableName = "T_LibraryCollection"; //导入的表名
                int a = userBll.BulkInsert(dtInsert);
                watch.Stop();
                double minute = ts.TotalMinutes; //计时
                string m = minute.ToString("0.00");
                if (a > 0)
                {
                    Session["path"] = null; //清除路径session
                    Response.Write("导入成功，总数据有" + row+"条，共导入"+a+"条数据"+"，共用时："+ m+"分钟");
                    Response.End();
                }
                else
                {
                    Session["path"] = null; //清除路径session
                    Response.Write("导入失败，总数据有" + row + "条，共导入" + a + "条数据");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 取差
        /// </summary>
        /// <returns></returns>
        private DataTable differentDt()
        {
            LibraryCollectionBll libraryCollectionBll = new LibraryCollectionBll();
            DataTable dt3 = new DataTable();//接受差集的dt3
            int j = libraryCollectionBll.Select(custom).Rows.Count;
            //数据库无数据时直接导入excel
            if (j<=0)
            {
                dt3 = excelToDt();
            }
            else
            {
                dt3.Columns.Add("id", typeof(string));
                dt3.Columns.Add("ISBN", typeof(string));
                dt3.Columns.Add("书名", typeof(string));
                dt3.Columns.Add("定价", typeof(double));
                dt3.Columns.Add("馆藏数量", typeof(int));
                dt3.Columns.Add("客户ID", typeof(string));
                dt3.Columns.Add("state", typeof(int));
                DataRowCollection count = excelToDt().Rows;
                foreach (DataRow row in count)//遍历excel数据集
                {
                   DataRow[] rows = libraryCollectionBll.Select(custom).Select(string.Format("customerId='{0}' and ISBN='{1}'",custom, row[1].ToString().Trim()));
                    //DataRow[] rows = libraryCollectionBll.Select(custom).Select("customerId='" + custom + "' and ISBN='" + row[1].ToString().Trim() + "'");//查询excel数据集是否存在于表A，如果存在赋值给DataRow集合
                    if (rows.Length == 0)//判断如果DataRow.Length为0，即该行excel数据不存在于表A中，就插入到dt3
                    {
                        dt3.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5]);
                    }
                }
            }
            return dt3;
        }
        private DataTable excelToDt()
        {
            int custom = Convert.ToInt32(Request["custom"]);
            string path = Session["path"].ToString();
            DataTable dt1 = new DataTable();
            string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            //文件类型判断
            //string[] sArray = path.Split('.');
            //int count = sArray.Length - 1;
            //if (sArray[count] == "xls")
            //{
            //    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            //}
            //else if (sArray[count] == "xlsx")
            //{
            //    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            //}
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string strExcel1 = "select * from [Sheet1$]";
                OleDbDataAdapter oda1 = new OleDbDataAdapter(strExcel1, strConn);
                dt1.Columns.Add("id"); //id自增列
                oda1.Fill(dt1);
                row = dt1.Rows.Count.ToString(); //获取总数
                DataColumn dc = new DataColumn("客户ID", typeof(string));
                DataColumn dc2 = new DataColumn("state", typeof(int));
                dc.DefaultValue = custom; //默认客户值值列
                dc2.DefaultValue = 0; //默认值列
                dt1.Columns.Add(dc);
                dt1.Columns.Add(dc2);
                GetDistinctSelf(dt1, "ISBN", "客户ID"); //去重字段
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            conn.Close();
            return dt1;
        }

        /// <summary>
        /// 某字段去重
        /// </summary>
        /// <param name="SourceDt"></param>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        private DataTable GetDistinctSelf(DataTable SourceDt, string field1, string field2)
        {
            if (SourceDt.Rows.Count > 1)
            {
                for (int i = 1; i <= SourceDt.Rows.Count - 2; i++)
                {
                    string isbn = SourceDt.Rows[i][field1].ToString();
                    string customId = SourceDt.Rows[i][field2].ToString();
                    DataRow[] rows = SourceDt.Select(string.Format("{0}= '{2}' and {1}= '{3}'", field1, field2, isbn, customId));
                    if (rows.Length > 1)
                    {
                        SourceDt.Rows.RemoveAt(i);
                    }
                }
            }
            return SourceDt;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        protected string getData()
        {
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage==0)
            {
                currentPage = 1;
            }
            string book = Request["book"];
            string isbn = Request["isbn"];
            string search = "";
            if ((book == "" || book == null) && (isbn == "" || isbn == null))
            {
                search = "";
            }
            else if (book == "")
            {
                search = "ISBN=" + isbn;
            }
            else if (isbn == "")
            {
                search = "bookName like '%" + book+"%'";
            }
            else
            {
                search = "ISBN=" + isbn+ " and bookName like '%" + book+"%'";
            }
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_LibraryCollection";
            tbd.OrderBy = "ISBN";
            tbd.StrColumnlist = "bookName,ISBN,price,collectionNum,customerName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            //获取地区下拉框数据
            dsCustom = libraryCollectionBll.getCustomer();
            
            //生成table
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            int count = ds.Tables[0].Rows.Count;
            DataRowCollection drc = ds.Tables[0].Rows;
            for (int i = 0; i < count; i++)
            {
                sb.Append("<tr><td>" +(i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + drc[i]["ISBN"].ToString() + "</ td >");
                sb.Append("<td>" + drc[i]["bookName"].ToString() + "</ td >");
                sb.Append("<td>" + drc[i]["customerName"].ToString() + "</td>");
                sb.Append("<td>" + drc[i]["price"].ToString() + "</ td >");
                sb.Append("<td>" + drc[i]["collectionNum"].ToString() + "</ td ></ tr >");
                sb.Append("<td>" + "<button class='btn btn-danger btn-sm btn_delete'>" + "<i class='fa fa-trash-o fa-lg'></i>" + "</button>"+ "</ td ></ tr >");
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
    }
}