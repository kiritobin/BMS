using bms.Bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.CustomerMGT
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public string custom = "20001";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            GridView1.DataSource = excelToDt();
            GridView1.DataBind();
        }

        private DataTable excelToDt()
        {
            DataTable dt1 = new DataTable();
            string strConn = "";
            string path = Session["path"].ToString();
            string[] sArray = path.Split('.');
            int count = sArray.Length - 1;
            if (sArray[count] == "xls")
            {
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (sArray[count] == "xlsx")
            {
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string strExcel1 = "select * from [QueryResult$]";
                OleDbDataAdapter oda1 = new OleDbDataAdapter(strExcel1, strConn);
                dt1.Columns.Add("id"); //id自增列
                oda1.Fill(dt1);
                DataColumn dc = new DataColumn("客户ID", typeof(string));
                DataColumn dc2 = new DataColumn("state", typeof(int));
                dc.DefaultValue = custom; //默认值列
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

        private DataTable GetDistinctSelf(DataTable SourceDt, string field1, string field2)
        {
            if (SourceDt.Rows.Count > 1)
            {
                for (int i = 1; i <= SourceDt.Rows.Count - 2; i++)
                {
                    DataRow[] rows = SourceDt.Select(string.Format("{0}='{2}' and {1}='{3}'", field1, field2, SourceDt.Rows[i][field1], SourceDt.Rows[i][field2]));
                    if (rows.Length > 1)
                    {
                        SourceDt.Rows.RemoveAt(i);
                    }
                }
            }
            return SourceDt;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DataTable _dt = new DataTable();
            UserBll userBll = new UserBll();
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            _dt = chaji();
            TimeSpan ts = watch.Elapsed;
            int a = 0;
            _dt.TableName = "T_LibraryCollection";

            a = userBll.BulkInsert(_dt);
            watch.Stop();
            string seconds = "succ:" + ts.TotalMilliseconds;
            if (a > 0)
            {
                Response.Write("导入成功" + a);
                Response.End();
            }
            else
            {
                Response.Write("导入失败" + a);
                Response.End();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            chaji();
        }

        private DataTable chaji()
        {
            LibraryCollectionBll libraryCollectionBll = new LibraryCollectionBll();
            DataTable dt3 = new DataTable();//接受差集的dt3
            dt3.Columns.Add("id", typeof(string));
            dt3.Columns.Add("ISBN", typeof(string));
            dt3.Columns.Add("书名", typeof(string));
            dt3.Columns.Add("定价", typeof(double));
            dt3.Columns.Add("馆藏数量", typeof(int));
            dt3.Columns.Add("客户ID", typeof(string));
            dt3.Columns.Add("state", typeof(int));
            foreach (DataRow row in excelToDt().Rows)//遍历excel数据集
            {

                DataRow[] rows = libraryCollectionBll.Select(custom).Select("customerId='" + custom + "' and ISBN='" + row[1].ToString().Trim() + "'");//查询excel数据集是否存在于表A，如果存在赋值给DataRow集合
                if (rows.Length == 0)//判断如果DataRow.Length为0，即该行excel数据不存在于表A中，就插入到dt3
                {
                    dt3.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5]);
                }
            }
            GridView2.DataSource = dt3;
            GridView2.DataBind();
            return dt3;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            LibraryCollectionBll libraryCollectionBll = new LibraryCollectionBll();
            GridView3.DataSource = libraryCollectionBll.Select(custom);
            GridView3.DataBind();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            LibraryCollectionBll libraryCollectionBll = new LibraryCollectionBll();
            IEnumerable<DataRow> query2 = excelToDt().AsEnumerable().Except(libraryCollectionBll.Select(custom).AsEnumerable(), DataRowComparer.Default);
            DataTable dt3 = query2.CopyToDataTable();
            GridView2.DataSource = dt3;
            GridView2.DataBind();
        }
    }
}