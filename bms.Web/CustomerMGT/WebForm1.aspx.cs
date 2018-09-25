using bms.Bll;
using bms.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static bms.Bll.Enums;

namespace bms.Web.CustomerMGT
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 20, totalCount, intPageCount;
        public string search = "", last, row, num;

        protected void Button2_Click(object sender, EventArgs e)
        {
            UserBll userBll = new UserBll();
            DataTable dtInsert = new DataTable();
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            differentDt();
            except.Columns.Remove("id"); //移除匹配列
            dtInsert = except; //赋给新table
            TimeSpan ts = watch.Elapsed;
            dtInsert.TableName = "T_BookBasicData"; //导入的表名
            int a = userBll.BulkInsert(dtInsert);
            watch.Stop();
            double minute = ts.TotalMinutes; //计时
            string m = minute.ToString("0.00");
            if (a > 0)
            {
                BookBasicData bookNum = bookbll.getBookNum();
                OpResult result = bookbll.updateBookNum(last); //更新书号
                Response.Write("导入成功，总数据有" + row + "条，共导入" + a + "条数据" + "，共用时：" + m + "分钟");
                Response.End();
            }
            else
            {
                Response.Write("导入失败，总数据有" + row + "条，共导入" + a + "条数据");
                Response.End();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            differentDt();
            except.Columns.Remove("id"); //移除匹配列
            GridView1.DataSource = except;
            GridView1.DataBind();
        }

        public DataSet ds;
        DataTable except = new DataTable();//接受差集
        BookBasicBll bookbll = new BookBasicBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            //BookBasicData bookId = bookbll.getBookNum();
            //if (!IsPostBack)
            //{
            //    if (bookId.NewBookNum == "0" || bookId.NewBookNum == null)
            //    {
            //        num = "0";
            //    }
            //    else
            //    {
            //        num = bookId.NewBookNum;
            //    }
            //    ViewState["i"] = num;
            //}
        }
        //某字段table去重方法
        private DataTable GetDistinctSelf(DataTable SourceDt, string field1, string field2, string field3)
        {
            if (SourceDt.Rows.Count > 1)
            {
                for (int i = 1; i <= SourceDt.Rows.Count - 2; i++)
                {
                    string isbn = SourceDt.Rows[i][field1].ToString();
                    string bookName = SourceDt.Rows[i][field2].ToString();
                    double price = Convert.ToDouble(SourceDt.Rows[i][field3]);
                    DataRow[] rows = SourceDt.Select(string.Format("{0}='{3}' and {1}='{4}' and {2}={5}", field1, field2, field3, isbn, ToSBC(bookName), price));
                    if (rows.Length > 1)
                    {
                        SourceDt.Rows.RemoveAt(i);
                    }
                }
            }
            return SourceDt;
        }

        //excel读到table
        private DataTable excelToDt()
        {
            DataTable dt1 = new DataTable();
            string path = Session["path"].ToString();
            string strConn = "";
            //文件类型判断
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
                string strExcel1 = "select * from [Sheet1$]";
                OleDbDataAdapter oda1 = new OleDbDataAdapter(strExcel1, strConn);
                dt1.Columns.Add("id"); //匹配列，与结构一致
                oda1.Fill(dt1);
                row = dt1.Rows.Count.ToString(); //获取总数
                GetDistinctSelf(dt1, "ISBN", "书名", "单价");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            conn.Close();
            return dt1;
        }

        //书号算法并生成datatable列
        private DataTable addBookId()
        {
            BookBasicData _bookId = bookbll.getBookNum();
            if (_bookId.NewBookNum == "0" || _bookId.NewBookNum == null)
            {
                num = "0";
            }
            else
            {
                num = _bookId.NewBookNum;
            }
            ViewState["i"] = num;

            int row = excelToDt().Rows.Count;
            long a;
            if (num.Length>=8)
            {
                 a = Convert.ToInt64(ViewState["i"].ToString().Substring(10, 8));
            }
            else
            {
                 a = 0;
            }
            
            ArrayList list = new ArrayList();
            for (int i = 0; i < row; i++)
            {
                string bookId;
                a++;
                ViewState["i"] = a;
                string ss = a.ToString().PadLeft(8, '0');
                string isbn = excelToDt().Rows[i]["ISBN"].ToString();
                int count = isbn.Length;
                if (count >= 13) //大于13位书号
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
        private void differentDt()
        {
            BookBasicBll bookBasicBll = new BookBasicBll();
            int j = bookBasicBll.Select().Rows.Count;
            //数据库无数据时直接导入excel
            if (j <= 0)
            {
                except = addBookId();
            }
            else
            {
                except.Columns.Add("书号", typeof(long));
                except.Columns.Add("id", typeof(string));
                except.Columns.Add("ISBN", typeof(string));
                except.Columns.Add("书名", typeof(string));
                except.Columns.Add("供应商", typeof(string));
                except.Columns.Add("出版日期", typeof(string));
                except.Columns.Add("单价", typeof(double));
                except.Columns.Add("编目", typeof(string));
                except.Columns.Add("作者", typeof(string));
                except.Columns.Add("备注", typeof(string));
                except.Columns.Add("标识", typeof(string));

                DataRowCollection count = addBookId().Rows;
                foreach (DataRow row in count)//遍历excel数据集
                {
                    string isbn = row[2].ToString().Trim();
                    string bookName = ToSBC(row[3].ToString().Trim());
                    double price = Convert.ToDouble(row[6]);
                    DataRow[] rows = bookBasicBll.Select().Select(string.Format("ISBN='{0}' and bookName='{1}' and price={2}", isbn, bookName, price));
                    if (rows.Length == 0)//判断如果DataRow.Length为0，即该行excel数据不存在于表A中，就插入到dt3
                    {
                        except.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9]);
                    }
                }
            }
        }
        // 半角转全角：书名列
        private String ToSBC(String input)
        {
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
    }
}