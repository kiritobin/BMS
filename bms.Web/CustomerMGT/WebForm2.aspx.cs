using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThoughtWorks.QRCode.Codec;

namespace bms.Web.CustomerMGT
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 20, totalCount, intPageCount, row, funCount;
        public string search = "", last, num, userName, regionName;
        public DataSet ds, dsPer;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail;
        DataTable except = new DataTable();//接受差集
        BookBasicBll bookbll = new BookBasicBll();
        UserBll userBll = new UserBll();
        SaleHeadBll saleBll = new SaleHeadBll();
        SaleHead single = new SaleHead();
        protected void Page_Load(object sender, EventArgs e)
        {
            BookBasicData bookId = bookbll.getBookNum();
            if (!IsPostBack)
            {
                if (bookId.NewBookNum == "0" || bookId.NewBookNum == null)
                {
                    num = "0";
                }
                else
                {
                    num = bookId.NewBookNum;
                }
                ViewState["i"] = num;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            DataTable dtInsert = new DataTable();
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            differentDt();
            except.Columns.Remove("id"); //移除匹配列
            dtInsert = except; //赋给新table
            TimeSpan ts = watch.Elapsed;
            dtInsert.TableName = "T_BookBasicData"; //导入的表名
            int k = except.Rows.Count;
            GridView1.DataSource = dtInsert;
            GridView1.DataBind();
            //int a = userBll.BulkInsert(dtInsert);
        }
        //某字段table去重方法
        private DataTable GetDistinctSelf(DataTable SourceDt, string field1, string field2, string field3)
        {
            int j = SourceDt.Rows.Count;
            if (j > 1)
            {
                int k = j - 2;
                int i = 0;
                while (i <= k)
                {
                    DataRow dr = SourceDt.Rows[i];
                    string isbn = dr[field1].ToString();
                    string bookName = dr[field2].ToString();
                    double price = Convert.ToDouble(dr[field3]);
                    DataRow[] rows = SourceDt.Select(string.Format("{0}='{3}' and {1}='{4}' and {2}={5}", field1, field2, field3, isbn, ToSBC(bookName), price));
                    if (rows.Length > 1)
                    {
                        SourceDt.Rows.RemoveAt(i);
                        k = k - 1;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return SourceDt;
        }
        //excel读到table
        private DataTable excelToDt()
        {
            DataTable dt1 = new DataTable();
            string path = @"C:\Users\daobin\Desktop\23.xlsx";
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
                row = dt1.Rows.Count; //获取总数
                GetDistinctSelf(dt1, "ISBN", "书名", "单价");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }
            finally
            {
                conn.Close();
            }
            return dt1;
        }
        //书号算法并生成datatable列
        private DataTable addBookId()
        {
            int row = excelToDt().Rows.Count;
            long a;
            if (ViewState["i"].ToString().Length >= 18)
            {
                a = Convert.ToInt64(ViewState["i"].ToString().Substring(10, 8));
            }
            else
            {
                a = 0;
            }
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("书号");
            dt.Columns.Add(dc);
            DataRow dataRow = null;
            string bookId;
            for (int i = 0; i < row; i++)
            {
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
                dataRow = dt.NewRow();
                dataRow["书号"] = bookId;
                dt.Rows.Add(bookId);
            }

            DataRow dr_last = dataRow;
            last = dr_last["书号"].ToString();
            return UniteDataTable(dt, excelToDt());
        }

        //合并两个table方法,合并书号列
        private DataTable UniteDataTable(DataTable udt1, DataTable udt2)
        {
            DataTable udt3 = udt1.Clone();
            int row1 = udt1.Rows.Count;
            int row2 = udt2.Rows.Count;
            int colum1 = udt1.Columns.Count;
            int colum2 = udt2.Columns.Count;
            int colum3 = udt3.Columns.Count;
            for (int i = 0; i < colum2; i++)
            {
                udt3.Columns.Add(udt2.Columns[i].ColumnName);
            }
            object[] obj = new object[colum3];
            for (int i = 0; i < row1; i++)
            {
                udt1.Rows[i].ItemArray.CopyTo(obj, 0);
                udt3.Rows.Add(obj);
            }

            if (row1 >= row2)
            {
                for (int i = 0; i < row2; i++)
                {
                    DataRow dataRow2 = udt2.Rows[i];
                    DataRow dataRow3 = udt3.Rows[i];
                    for (int j = 0; j < colum2; j++)
                    {
                        dataRow3[j + colum1] = dataRow2[j].ToString();
                    }
                }
            }
            else
            {
                DataRow dr3;
                for (int i = 0; i < row2 - row1; i++)
                {
                    dr3 = udt3.NewRow();
                    udt3.Rows.Add(dr3);
                }
                for (int i = 0; i < row2; i++)
                {
                    for (int j = 0; j < colum2; j++)
                    {
                        udt3.Rows[i][j + colum1] = udt2.Rows[i][j].ToString();
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
                except.Columns.Add("书号", typeof(string));
                except.Columns.Add("id", typeof(string));
                except.Columns.Add("ISBN", typeof(string));
                except.Columns.Add("书名", typeof(string));
                except.Columns.Add("供应商", typeof(string));
                except.Columns.Add("出版日期", typeof(string));
                except.Columns.Add("单价", typeof(double));
                except.Columns.Add("预收数量", typeof(string));
                except.Columns.Add("进货折扣", typeof(string));
                except.Columns.Add("销售折扣", typeof(string));
                except.Columns.Add("备注", typeof(string));

                DataRowCollection count = addBookId().Rows;
                foreach (DataRow row in count)//遍历excel数据集
                {
                    try
                    {
                        string isbn = row[2].ToString().Trim();
                        string bookName = ToSBC(row[3].ToString().Trim());
                        double price = Convert.ToDouble(row[6]);
                        DataRow[] rows = bookBasicBll.Select().Select(string.Format("ISBN='{0}' and bookName='{1}' and price={2}", isbn, bookName, price));
                        if (rows.Length == 0)//判断如果DataRow.Length为0，即该行excel数据不存在于表A中，就插入到dt3
                        {
                            except.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10]);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex);
                        Response.End();
                    }
                }
            }
        }

        /// <summary>
        /// 半角转全角：书名列
        /// </summary>
        /// <param name="input">需要转换的字符串</param>
        /// <returns></returns>
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