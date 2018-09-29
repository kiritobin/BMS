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
        public int currentPage = 1, pageSize = 20, totalCount, intPageCount,row;
        public string search = "", last, num;

        protected void Button2_Click(object sender, EventArgs e)
        {
            UserBll userBll = new UserBll();
            DataTable dtInsert = new DataTable();
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            dtInsert = serialNumber();
            TimeSpan ts = watch.Elapsed;
            dtInsert.TableName = "T_Monomers"; //导入的表名
            int a = userBll.BulkInsert(dtInsert);
            watch.Stop();
            double minute = ts.TotalMinutes; //计时
            string m = minute.ToString("0.00");
            if (a > 0)
            {
                Session["path"] = null; //清除路径session
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
            //differentDt();
            //except.Columns.Remove("id"); //移除匹配列
            UserBll userBll = new UserBll();
            WarehousingBll warehousingBll = new WarehousingBll();
            DataTable dt = userBll.SplitDataTable(differentDt(), 1,1);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        public DataSet ds;
       
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

        //excel读到table
        private DataTable excelToDt()
        {
            string path = Session["path"].ToString();
            DataTable dt1 = new DataTable();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string strExcel1 = "select * from [Sheet1$]";
                OleDbDataAdapter oda1 = new OleDbDataAdapter(strExcel1, strConn);
                dt1.Columns.Add("id"); //id自增列
                DataColumn sid = new DataColumn("单头ID", typeof(string));
                sid.DefaultValue = "20180926000002"; //默认值列
                dt1.Columns.Add(sid);
                oda1.Fill(dt1);
                row = dt1.Rows.Count; //获取总数
                DataColumn dc = new DataColumn("type", typeof(int));
                dc.DefaultValue = 1; //默认值列
                dt1.Columns.Add(dc);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return dt1;
        }

        public DataTable serialNumber()
        {
            WarehousingBll warehousingBll = new WarehousingBll();
            int row = excelToDt().Rows.Count;
            string now = DateTime.Now.ToString("yyyyMMdd");
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("流水号");
            dt.Columns.Add(dc);
            DataRow dataRow = null;
            for (int i = 0; i < row; i++)
            {
                string id = (warehousingBll.getCount(20180926000002) + i + 1).ToString();
                dataRow = dt.NewRow();
                dataRow["流水号"] = id;
                dt.Rows.Add(id);
            }
            return UniteDataTable(excelToDt(), dt);
        }
        private DataTable differentDt()
        {
            DataTable except = new DataTable();//接受差集
            WarehousingBll warehousingBll = new WarehousingBll();
            int j = warehousingBll.getISBNbook().Rows.Count;
            //数据库无数据时直接导入excel
            if (j <= 0)
            {
                except = serialNumber();
            }
            else
            {
                except.Columns.Add("id", typeof(int));
                except.Columns.Add("单头ID", typeof(string));
                except.Columns.Add("书名", typeof(string));
                except.Columns.Add("书号", typeof(string));
                except.Columns.Add("ISBN", typeof(string));
                except.Columns.Add("商品数量", typeof(int));
                except.Columns.Add("单价", typeof(double));
                except.Columns.Add("码洋", typeof(double));
                except.Columns.Add("实洋", typeof(double));
                except.Columns.Add("折扣", typeof(double));
                except.Columns.Add("type", typeof(int));
                except.Columns.Add("流水号", typeof(string));

                DataRowCollection count = serialNumber().Rows;
                foreach (DataRow row in count)//遍历excel数据集
                {
                    string bookName = row[2].ToString().Trim();
                    string isbn = row[4].ToString().Trim();
                    DataRow[] rows = warehousingBll.getISBNbook().Select(string.Format("ISBN='{0}' and bookName='{1}'", isbn, ToSBC(bookName)));
                    if (rows.Length != 0)
                    {
                        except.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9],row[10],row[11]);
                    }
                }
            }
            return except;
        }
        //合并两个table方法
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