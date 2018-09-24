using bms.Bll;
using System;
using System.Collections;
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
        protected string r, num, seconds,last;
        DataTable dt3 = new DataTable();//接受差集的dt3
        DataTable dt4 = new DataTable();//交集
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                num = "0";
                ViewState["i"] = num;
            }
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
                GetDistinctSelf(dt1, "ISBN", "书名", "单价");
                //DataColumn dc = new DataColumn("客户ID", typeof(string));
                //DataColumn dc2 = new DataColumn("state", typeof(int));
                //dc.DefaultValue = custom; //默认值列
                //dc2.DefaultValue = 0; //默认值列
                //dt1.Columns.Add(dc);
                //dt1.Columns.Add(dc2);
                //GetDistinctSelf(dt1, "ISBN", "客户ID"); //去重字段
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            conn.Close();
            return dt1;
        }

        //某字段table去重方法
        private DataTable GetDistinctSelf(DataTable SourceDt, string field1, string field2, string field3)
        {
            if (SourceDt.Rows.Count > 1)
            {
                for (int i = 1; i <= SourceDt.Rows.Count - 2; i++)
                {
                    DataRow[] rows = SourceDt.Select(string.Format("{0}='{3}' and {1}='{4}' and {2}='{5}'", field1, field2, field3, SourceDt.Rows[i][field1], SourceDt.Rows[i][field2], SourceDt.Rows[i][field3]));
                    if (rows.Length > 1)
                    {
                        SourceDt.Rows.RemoveAt(i);
                        //int count = i + 2;
                        //r += count + "、";
                    }
                }
                //r = r.Substring(0, r.Length - 1); //在excel中重复的行数
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
            BookBasicBll bookBasicBll = new BookBasicBll();
            GridView3.DataSource = bookBasicBll.Select();
            GridView3.DataBind();
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
                DataRow[] rows = libraryCollectionBll.Select(custom).Select(string.Format("customerId='{0}' and ISBN='{1}'", custom, row[1].ToString().Trim()));
                //DataRow[] rows = libraryCollectionBll.Select(custom).Select("customerId='" + custom + "' and ISBN='" + row[1].ToString().Trim() + "'");//查询excel数据集是否存在于表A，如果存在赋值给DataRow集合
                if (rows.Length == 0)//判断如果DataRow.Length为0，即该行excel数据不存在于表A中，就插入到dt3
                {
                    dt3.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5]);
                }
            }
            GridView2.DataSource = dt3;
            GridView2.DataBind();
            return dt3;
        }

        public String ToSBC(String input)
        {
            // 半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
        }
        
        /// <summary>
        /// 取差
        /// </summary>
        /// <returns></returns>
        /// 
        private void differentDt()
        {
            LibraryCollectionBll libraryCollectionBll = new LibraryCollectionBll();
            BookBasicBll bookBasicBll = new BookBasicBll();
            
            int j = bookBasicBll.Select().Rows.Count;
            //数据库无数据时直接导入excel
            if (j <= 0)
            {
                dt3 = addBookId();
            }
            else
            {
                dt3.Columns.Add("书号", typeof(long));
                dt3.Columns.Add("id", typeof(string));
                dt3.Columns.Add("ISBN", typeof(string));
                dt3.Columns.Add("书名", typeof(string));
                dt3.Columns.Add("供应商", typeof(string));
                dt3.Columns.Add("出版日期", typeof(string));
                dt3.Columns.Add("单价", typeof(double));
                dt3.Columns.Add("编目", typeof(string));
                dt3.Columns.Add("作者", typeof(string));
                dt3.Columns.Add("备注", typeof(string));
                dt3.Columns.Add("标识", typeof(string));

                dt4.Columns.Add("书号", typeof(long));
                dt4.Columns.Add("id", typeof(string));
                dt4.Columns.Add("ISBN", typeof(string));
                dt4.Columns.Add("书名", typeof(string));
                dt4.Columns.Add("供应商", typeof(string));
                dt4.Columns.Add("出版日期", typeof(string));
                dt4.Columns.Add("单价", typeof(double));
                dt4.Columns.Add("编目", typeof(string));
                dt4.Columns.Add("作者", typeof(string));
                dt4.Columns.Add("备注", typeof(string));
                dt4.Columns.Add("标识", typeof(string));

                DataRowCollection count = addBookId().Rows;
                foreach (DataRow row in count)//遍历excel数据集
                {
                    string isbn = row[2].ToString().Trim();
                    string bookName = ToSBC(row[3].ToString().Trim());
                    double price= Convert.ToDouble(row[6]);
                    DataRow[] rows = bookBasicBll.Select().Select(string.Format("ISBN='{0}' and bookName='{1}' and price={2}", isbn, bookName,price));
                    if (rows.Length == 0)//判断如果DataRow.Length为0，即该行excel数据不存在于表A中，就插入到dt3
                    {
                        dt3.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5],row[6],row[7],row[8],row[9]);
                    }
                    else
                    {
                        dt4.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9]);
                    }
                }
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            LibraryCollectionBll libraryCollectionBll = new LibraryCollectionBll();
            GridView3.DataSource = libraryCollectionBll.Select(custom);
            GridView3.DataBind();
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            //LibraryCollectionBll libraryCollectionBll = new LibraryCollectionBll();
            //IEnumerable<DataRow> query2 = excelToDt().AsEnumerable().Except(libraryCollectionBll.Select(custom).AsEnumerable(), DataRowComparer.Default);
            //DataTable dt3 = query2.CopyToDataTable();
            GridView2.DataSource = addBookId();
            GridView2.DataBind();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            differentDt();

            dt3.Columns.Remove("id");
            GridView3.DataSource = dt3;
            GridView3.DataBind();
        }

        //书号算法并生成datatable
        private DataTable addBookId()
        {
            int row = excelToDt().Rows.Count;
            int a = Int32.Parse(ViewState["i"].ToString());
            ArrayList list = new ArrayList();
            for (int i = 0; i < row; i++)
            {
                string bookId;
                a++;
                ViewState["i"] = a;
                string ss = a.ToString().PadLeft(8, '0');
                string isbn = excelToDt().Rows[i]["ISBN"].ToString();
                int count = isbn.Length;
                if (count == 13)
                {
                    bookId = isbn.Substring(3, 10);
                    bookId = bookId + ss;
                }
                else
                {
                    bookId = isbn + ss;
                }
                list.Add(bookId);
            }
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("书号");
            dt.Columns.Add(dc);
            DataRow dataRow;
            int j = list.Count;
            for (int i = 0; i < list.Count; i++)
            {

                dataRow = dt.NewRow();
                string k = list[i].ToString();
                dataRow["书号"] = list[i].ToString();
                dt.Rows.Add(k);
            }
            DataRow dr_last = dt.AsEnumerable().Last<DataRow>();
            last = dr_last["书号"].ToString();
            return UniteDataTable(dt, excelToDt());
        }

        //合并两个table方法,合并书号列
        private DataTable UniteDataTable(DataTable udt1, DataTable udt2)
        {
            DataTable udt3 = udt1.Clone();
            for (int i = 0; i < udt2.Columns.Count; i++)
            {
                udt3.Columns.Add(udt2.Columns[i].ColumnName);
            }
            object[] obj = new object[udt3.Columns.Count];

            for (int i = 0; i < udt1.Rows.Count; i++)
            {
                udt1.Rows[i].ItemArray.CopyTo(obj, 0);
                udt3.Rows.Add(obj);
            }

            if (udt1.Rows.Count >= udt2.Rows.Count)
            {
                for (int i = 0; i < udt2.Rows.Count; i++)
                {
                    for (int j = 0; j < udt2.Columns.Count; j++)
                    {
                        udt3.Rows[i][j + udt1.Columns.Count] = udt2.Rows[i][j].ToString();
                    }
                }
            }
            else
            {
                DataRow dr3;
                for (int i = 0; i < udt2.Rows.Count - udt1.Rows.Count; i++)
                {
                    dr3 = udt3.NewRow();
                    udt3.Rows.Add(dr3);
                }
                for (int i = 0; i < udt2.Rows.Count; i++)
                {
                    for (int j = 0; j < udt2.Columns.Count; j++)
                    {
                        udt3.Rows[i][j + udt1.Columns.Count] = udt2.Rows[i][j].ToString();
                    }
                }
            }
            return udt3;
        }
    }
}