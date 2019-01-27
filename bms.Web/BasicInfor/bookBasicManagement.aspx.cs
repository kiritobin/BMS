using bms.Bll;
using bms.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.BasicInfor
{
    using bms.DBHelper;
    using System.Web.Security;
    using Result = Enums.OpResult;
    public partial class bookBasicManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 20, totalCount, intPageCount,row,funCount;
        public string search = "", last, num,userName,regionName;
        public DataSet ds, dsPer;
        public DataTable dsSupplier;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply,funcRetail, isAdmin, funcBookStock;
        DataTable except = new DataTable();//接受差集
        BookBasicBll bookbll = new BookBasicBll();
        UserBll userBll = new UserBll();
        SaleHeadBll saleBll = new SaleHeadBll();
        SaleHead single = new SaleHead();
        DataTable excel = new DataTable();
        RoleBll roleBll = new RoleBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
            //获取书号
            BookBasicData bookId = bookbll.getBookNum();
            if (!IsPostBack)
            {
                if (bookId.NewBookNum == "0" || bookId.NewBookNum == null||bookId.NewBookNum.Length<8)
                {
                    num = "0";
                }
                else
                {
                    num = bookId.NewBookNum;
                }
                ViewState["i"] = num;
            }
            getData();
            string op = Request["op"];
            if (op == "del")
            {
                string bookNum = Request["bookNum"].ToString();
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
                    Response.Write("此资料已有关联订单，无法删除！");
                    Response.End();
                }
            }
            if (op=="check")
            {
                test();
            }
            string action = Request["action"];
            if (action == "import")
            {
                differentDt();
                //DataTable dtInsert = new DataTable();
                //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                //watch.Start();
                //differentDt();
                //except.Columns.Remove("id"); //移除匹配列
                //dtInsert = except; //赋给新table
                //TimeSpan ts = watch.Elapsed;
                //dtInsert.TableName = "T_BookBasicData"; //导入的表名
                //int a = userBll.BulkInsert(dtInsert);
                //watch.Stop();
                //double minute = ts.TotalMinutes; //计时
                //string m = minute.ToString("0.00");
                //if (a > 0)
                //{
                //    int cf = row - a;
                //    BookBasicData bookNum = bookbll.getBookNum();
                //    Result result = bookbll.updateBookNum(last); //更新书号
                //    //Response.Write("导入成功，总数据有" + row + "条，共导入" + a + "条数据" + "，共用时：" + m + "分钟");
                //    Response.Write("导入成功，共导入数据"+a+"条数据，共有重复数据"+cf+"条");
                //    Response.End();
                //}
                //else
                //{
                //    Response.Write("导入失败，可能重复导入");
                //    Response.End();
                //}
            }
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
        }

        //某字段table去重方法
        private DataTable GetDistinctSelf(DataTable SourceDt, string field1, string field2, string field3)
        {
            int j = SourceDt.Rows.Count;
            int counts = 0;
            if (j > 1)
            {
                    int k = j - 1;
                    int i = 0;
                    while (i <= k)
                    {
                        DataRow dr = SourceDt.Rows[i];
                        string isbn = dr[field1].ToString();
                        string bookName = dr[field2].ToString();
                        double price = Convert.ToDouble(dr[field3]);
                        DataRow[] rows = SourceDt.Select(string.Format("{0}='{3}' and {1}='{4}' and {2}={5}", field1, field2, field3, isbn, ToSBC(bookName), price));
                        //if (rows.Length > 1)
                        //{
                        //    SourceDt.Rows.RemoveAt(i);
                        //    k = k - 1;
                        //}
                        //else
                        //{
                        //    i++;
                        //}
                        if (rows.Length == 1)
                        {
                            BookBasicData basicData = new BookBasicData();
                            basicData.BookNum = dr[0].ToString();
                            basicData.Isbn = isbn;
                            basicData.BookName = bookName;

                            basicData.Publisher = dr[4].ToString();
                            basicData.Time = dr[5].ToString();
                            //basicData.PublishTime = Convert.ToDateTime(rows[5]);
                            basicData.Price = Convert.ToDouble(dr[6]);
                            basicData.Catalog = dr[7].ToString();
                            basicData.Author = dr[8].ToString();
                            basicData.Remarks = dr[9].ToString();
                            basicData.Dentification = dr[10].ToString();
                            Result result = bookbll.Insert(basicData);
                            if (result == Result.添加失败)
                            {
                                Response.Write("导入失败，可能重复导入");
                                Response.End();
                            }
                            else
                            {
                                counts++;
                            }
                            i++;
                        }
                        else
                        {
                            SourceDt.Rows.RemoveAt(i);
                            k = k - 1;
                        }
                    }
                    int cf = row - counts;
                    Response.Write("导入成功，共导入数据" + counts + "条数据，共有重复数据" + cf + "条");
                    Response.End();

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
                row = dt1.Rows.Count; //获取总数
                //GetDistinctSelf(dt1, "ISBN", "书名", "单价");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End() ;
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
            //excel = excelToDt();
            excel = npoi();
            int row = excel.Rows.Count;
            string a;
            string oldBookId = ViewState["i"].ToString();
            if (oldBookId.Length>8)
            {
                a = oldBookId.Substring(oldBookId.Length-8, 8);
            }
            else
            {
                a = "0";
            }
            DataTable dt = new DataTable();
            DataColumn dc = new DataColumn("书号");
            dt.Columns.Add(dc);
            DataRow dataRow = null;
            string bookId;
            for (int i = 0; i < row; i++)
            {
                a=(Convert.ToInt32(a)+1).ToString();
                //ViewState["i"] = a;
                string ss = a.PadLeft(8, '0');
                string isbn = excel.Rows[i]["ISBN"].ToString();
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
                ViewState["i"] = bookId;
                dataRow = dt.NewRow();
                dataRow["书号"] = bookId;
                dt.Rows.Add(bookId);
            }
    
            DataRow dr_last = dataRow;
            last = dr_last["书号"].ToString();
            return UniteDataTable(dt, excel);
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
            string discount = Request["discount"];
            string discount2 = Request["discount2"];
            string bookGys = Request["bookGys"];
            if (bookName != "" && bookName != null)
            {
                if (search == "" || search == null)
                {
                    search += "bookName like '%" + bookName + "%'";
                }
                else
                {
                    search += " and bookName like '%" + bookName + "%'";
                }
            }
            if (bookNum != "" && bookNum != null)
            {
                if (search == "" || search == null)
                {
                    search += "bookNum like '%" + bookNum + "%'";
                }
                else
                {
                    search += " and bookNum like '%" + bookNum + "%'";
                }
            }
            if (bookISBN != "" && bookISBN != null)
            {
                if (search == "" || search == null)
                {
                    search += "isbn like '%" + bookISBN + "%'";
                }
                else
                {
                    search += " and isbn like '%" + bookISBN + "%'";
                }
            }
            if (discount != "" && discount != null)
            {
                string[] sArray = discount.Split('于');
                string type = sArray[0];
                string number = sArray[1];
                try
                {
                    if (search == "" || search == null)
                    {
                        if (type == "小")
                        {
                            search = "remarks < '" + number + "'";
                        }
                        else if (type == "等")
                        {
                            search = "remarks = '" + number + "'";
                        }
                        else
                        {
                            search = "remarks > '" + number + "'";
                        }
                    }
                    else
                    {
                        if (type == "小")
                        {
                            search += " and remarks < '" + number + "'";
                        }
                        else if (type == "等")
                        {
                            search += " and remarks = '" + number + "'";
                        }
                        else
                        {
                            search += " and remarks > '" + number + "'";
                        }
                    }
                }
                catch
                {
                    Response.Write("数据库存在不符合格式的数据");
                    Response.End();
                }
            }
            if (discount2 != "" && discount2 != null)
            {
                string[] sArray = discount2.Split('于');
                string type = sArray[0];
                string number = sArray[1];
                try
                {
                    if (search == "" || search == null)
                    {
                        if (type == "小")
                        {
                            search = "author < '" + number + "'";
                        }
                        else if (type == "等")
                        {
                            search = "author = '" + number + "'";
                        }
                        else
                        {
                            search = "author > '" + number + "'";
                        }
                    }
                    else
                    {
                        if (type == "小")
                        {
                            search += " and author < '" + number + "'";
                        }
                        else if (type == "等")
                        {
                            search += " and author = '" + number + "'";
                        }
                        else
                        {
                            search += " and author > '" + number + "'";
                        }
                    }
                }
                catch
                {
                    Response.Write("数据库存在不符合格式的数据");
                    Response.End();
                }
            }
            if (bookGys != "" && bookGys != null)
            {
                if (search == "" || search == null)
                {
                    search += "supplier like '%" + bookGys + "%'";
                }
                else
                {
                    search += " and supplier like '%" + bookGys + "%'";
                }
            }
            //if ((bookName == "" || bookName == null) && (bookNum == null || bookNum == "") && (bookISBN == null || bookISBN == ""))
            //{
            //    search = "";
            //}
            //else if ((bookName != "" && bookName != null) && (bookNum == null || bookNum == "") && (bookISBN == null || bookISBN == ""))
            //{
            //    search = String.Format(" bookName like '%{0}%'", bookName);
            //}
            //else if ((bookName == "" || bookName == null) && (bookNum != "" && bookNum != null) && (bookISBN == null || bookISBN == ""))
            //{
            //    search = "bookNum like '%" + bookNum + "%'";
            //}
            //else if ((bookName == "" || bookName == null) && (bookISBN != "" && bookISBN != null) && (bookNum == null || bookNum == ""))
            //{
            //    search = "ISBN like '%" + bookISBN + "%'";
            //}
            //else if ((bookName == "" || bookName == null) && (bookISBN != "" && bookISBN != null) && (bookNum != null && bookNum != ""))
            //{
            //    search = "bookNum like '%" + bookNum + "%' and ISBN like '%" + bookISBN + "%'";
            //}
            //else if ((bookName != "" && bookName != null) && (bookNum != null && bookNum != "") && (bookISBN == null || bookISBN == ""))
            //{
            //    search = String.Format(" bookName like '%{0}%' and bookNum like '{1}'", bookName, bookNum);
            //}
            //else if ((bookName != "" && bookName != null) && (bookNum == null || bookNum == "") && (bookISBN != null && bookISBN != ""))
            //{
            //    search = String.Format(" bookName like '%{0}%' and ISBN like '%{1}%'", bookName, bookISBN);
            //}
            //else
            //{
            //    search = String.Format(" bookName like '%{0}%' and bookNum like '%{1}%' and ISBN like '%{2}%'", bookName, bookNum, bookISBN);
            //}
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
            //获取供应商
            //获取供应商
            dsSupplier = bookbll.selectSupplier();
            //生成table
            StringBuilder sb = new StringBuilder();
            int j = ds.Tables[0].Rows.Count;
            for (int i = 0; i < j; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                sb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
                sb.Append("<td>" + dr["bookName"].ToString() + "</td >");
                sb.Append("<td>" + dr["price"].ToString() + "</td >");
                sb.Append("<td>" + dr["publishTime"].ToString() + "</td >");
                sb.Append("<td>" + dr["supplier"].ToString() + "</td >");
                sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                sb.Append("<td>" + dr["catalog"].ToString() + "</td>");
                sb.Append("<td>" + dr["author"].ToString() + "</td >");
                sb.Append("<td>" + dr["remarks"].ToString() + "</td>");
                sb.Append("<td>" + dr["dentification"].ToString() + "</td>");
                if (funcBook)
                {
                    sb.Append("<td>" + "<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash-o fa-lg'></i></button></td></tr>");
                }
            }
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
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
            if (bookbll.IsDelete("t_salemonomer", "bookNum", bookNum) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (bookbll.IsDelete("t_stock", "bookNum", bookNum) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (bookbll.IsDelete("t_replenishmentmonomer", "bookNum", bookNum) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (bookbll.IsDelete("t_selloffmonomer", "bookNum", bookNum) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (bookbll.IsDelete("t_retailmonomer", "bookNum", bookNum) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (bookbll.IsDelete("t_monomers", "bookNum", bookNum) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            return row;
        }

        //private void excelNo()
        //{
        //    BookBasicBll bookBasicBll = new BookBasicBll();
        //        except.Columns.Add("书号", typeof(string));
        //        except.Columns.Add("id", typeof(string));
        //        except.Columns.Add("ISBN", typeof(string));
        //        except.Columns.Add("书名", typeof(string));
        //        except.Columns.Add("供应商", typeof(string));
        //        except.Columns.Add("出版日期", typeof(string));
        //        except.Columns.Add("单价", typeof(double));
        //        except.Columns.Add("预收数量", typeof(string));
        //        except.Columns.Add("进货折扣", typeof(string));
        //        except.Columns.Add("销售折扣", typeof(string));
        //        except.Columns.Add("备注", typeof(string));

        //        DataRowCollection count = addBookId().Rows;
        //        int counts = 0;
        //        foreach (DataRow row in count)//遍历excel数据集
        //        {
        //            try
        //            {
        //                string isbn = row[2].ToString().Trim();
        //                string bookName = ToSBC(row[3].ToString().Trim());
        //                double price = Convert.ToDouble(row[6]);
        //                DataRow[] rows = excelToDt().Select(string.Format("ISBN='{0}' and 书名='{1}' and 单价={2}", isbn, bookName, price));
        //                if (rows.Length == 1)//判断如果DataRow.Length为0，即该行excel数据不存在于表A中，就插入到dt3
        //                {
        //                    //except.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10]);
        //                    BookBasicData basicData = new BookBasicData();
        //                    basicData.BookNum = row[0].ToString();
        //                    basicData.Isbn = isbn;
        //                    basicData.BookName = bookName;
        //                    basicData.Publisher = row[4].ToString();
        //                    basicData.Time = row[5].ToString();
        //                    //basicData.PublishTime = Convert.ToDateTime(row[5]);
        //                    basicData.Price = Convert.ToDouble(row[6]);
        //                    basicData.Catalog = row[7].ToString();
        //                    basicData.Author = row[8].ToString();
        //                    basicData.Remarks = row[9].ToString();
        //                    basicData.Dentification = row[10].ToString();
        //                    Result result = bookBasicBll.Insert(basicData);
        //                    if (result == Result.添加失败)
        //                    {
        //                        Response.Write("导入失败，可能重复导入");
        //                        Response.End();
        //                    }
        //                    else
        //                    {
        //                        counts++;
        //                    }
        //                }
        //            else
        //            {
        //                excelToDt().Rows.RemoveAt();
        //            }
        //            }
        //            catch (Exception ex)
        //            {
        //                Response.Write(ex);
        //                Response.End();
        //            }
        //        }
        //        int cf = row - counts;
        //        Response.Write("导入成功，共导入数据" + counts + "条数据，共有重复数据" + cf + "条");
        //        Response.End();
            
        //}

        private void differentDt()
        {
            BookBasicBll bookBasicBll = new BookBasicBll();
            int j = bookBasicBll.SelectCount();
            //数据库无数据时直接导入excel
            if (j <= 0)
            {
                //except = addBookId();
                //except= GetDistinctSelf(addBookId(), "ISBN", "书名", "单价"); 
                excelNo();
                
            }
            else
            {
                //except.Columns.Add("书号", typeof(string));
                //except.Columns.Add("id", typeof(string));
                //except.Columns.Add("ISBN", typeof(string));
                //except.Columns.Add("书名", typeof(string));
                //except.Columns.Add("供应商", typeof(string));
                //except.Columns.Add("出版日期", typeof(string));
                //except.Columns.Add("单价", typeof(double));
                //except.Columns.Add("预收数量", typeof(string));
                //except.Columns.Add("进货折扣", typeof(string));
                //except.Columns.Add("销售折扣", typeof(string));
                //except.Columns.Add("备注", typeof(string));
                BookBasicData bookId = bookbll.getBookNum();
                DataRowCollection count = addBookId().Rows;
                int counts = 0;
                DataTable dataTable = bookBasicBll.Select();
                bool isNull=false;
                int rowls =0;
                int kz = 0;
                foreach (DataRow row in count)//遍历excel数据集
                {
                    try
                    {
                        string isbn = row[2].ToString().Trim();
                        string bookName = ToSBC(row[3].ToString().Trim());
                        string supplier = row[4].ToString().Trim();
                        string accept = row[8].ToString().Trim();
                        string sale = row[9].ToString().Trim();
                        string price = row[6].ToString().Trim();

                        if (price==""||isbn==""||bookName=="")
                        {
                            price = "0";
                            isNull = true;
                            kz++;
                            continue;
                        }
                        DataRow[] rows = dataTable.Select(string.Format("ISBN='{0}' and bookName='{1}'  and supplier='{2}' and price='{3}' and author='{4}' and remarks='{5}'", isbn,bookName,supplier, price, accept, sale));
                        //DataRow[] rows = dataTable.Select(string.Format("ISBN='{0}' and bookName='{1}' and price={2}", isbn, bookName, Convert.ToDouble(price)));
                        if (rows.Length == 0)//判断如果DataRow.Length为0，即该行excel数据不存在于表A中，就插入到dt3
                        {
                            //except.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10]);
                            BookBasicData basicData = new BookBasicData();
                            basicData.BookNum = row[0].ToString();
                            basicData.Isbn = isbn;
                            basicData.BookName = bookName;
                            basicData.Publisher = row[4].ToString();
                            basicData.Time = row[5].ToString();
                            //basicData.PublishTime = Convert.ToDateTime(row[5]);
                            basicData.Price = Convert.ToDouble(price);
                            basicData.Catalog = row[7].ToString();
                            basicData.Author = row[8].ToString();
                            basicData.Remarks = row[9].ToString();
                            basicData.Dentification = row[10].ToString();
                            basicData.Remarks1 = row[11].ToString();
                            basicData.Remarks2 = row[12].ToString();
                            basicData.Remarks3 = row[13].ToString();
                            Result result = bookBasicBll.Insert(basicData);
                            if(result == Result.添加失败)
                            {
                                Response.Write("远程服务器未响应");
                                Response.End();
                            }
                            else
                            {
                                counts++;
                            }
                        }
                        rowls++;
                }
                    catch (Exception ex)
                {
                    Response.Write(ex);
                    Response.End();
                }
            }
                string s = last;
                Result reg = bookbll.updateBookNum(s); //更新书号
                //bookId.NewBookNum = bookId.NewBookNum.Substring(bookId.NewBookNum.Length - 8);
                //last = last.ToString().Substring(last.ToString().Length - 8);
                //if (Convert.ToInt64(bookId.NewBookNum) < Convert.ToInt64(last))
                //{
                //    Result reg = bookbll.updateBookNum(s); //更新书号
                //}
                Session["path"] = null; //清除路径session
                int cf = row - counts;
                if (counts==0)
                {
                    if (isNull)
                    {
                        Response.Write("导入成功，共导入数据" + counts + "条数据，共有重复数据" + cf + "条，共有错误数据"+kz.ToString());
                        Response.End();
                    }
                    else
                    {
                        Response.Write("导入失败，共导入数据" + counts + "条数据，共有重复数据" + cf + "条");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("导入成功，共导入数据" + counts + "条数据，共有重复数据" + cf + "条");
                    Response.End();
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

        protected void permission()
        {
            FunctionBll functionBll = new FunctionBll();
            User user = (User)Session["user"];
            userName = user.UserName;
            regionName = user.ReginId.RegionName;
            Role role = new Role();
            role = user.RoleId;
            int roleId = role.RoleId;
            dsPer = functionBll.SelectByRoleId(roleId);
            string userId = user.UserId;
            DataSet dsRole = roleBll.selectRole(userId);
            string roleName = dsRole.Tables[0].Rows[0]["roleName"].ToString();
            if (roleName=="超级管理员")
            {
                isAdmin = true;
            }
            for (int i = 0; i < dsPer.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 1)
                {
                    funcOrg = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 2)
                {
                    funcRole = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 3)
                {
                    funcUser = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 4)
                {
                    funcGoods = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 5)
                {
                    funcCustom = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 6)
                {
                    funcLibrary = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 7)
                {
                    funcBook = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 8)
                {
                    funcPut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 9)
                {
                    funcOut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 10)
                {
                    funcSale = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 11)
                {
                    funcSaleOff = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 12)
                {
                    funcReturn = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 13)
                {
                    funcSupply = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 14)
                {
                    funcRetail = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 15)
                {
                    funcBookStock = true;
                }
            }
        }

        private void check()
        {
            //excel = excelToDt();
            excel = npoi();
            GetDistinctTable(excel);
        }

        #region  记录Excel中的重复列
        /// <summary>
        /// 记录Excel中的重复列
        /// </summary>
        /// <param name="dt">需要获取重复列的表</param>
        /// <returns>提示重复信息</returns>
        private string GetDistinctTable(DataTable dt)
        {
            DataTable dtClone = dt.Copy(); 
            string isbn = string.Empty;
            string bookName = string.Empty;
            string price = string.Empty;
            string repeatExcel = string.Empty;
            //for (int i = dtClone.Rows.Count - 1; i >= 0; i--)
            int i = dtClone.Rows.Count;
            while (dtClone.Rows.Count > 0)
            {
                isbn = dtClone.Rows[dtClone.Rows.Count][1].ToString().Trim();
                bookName = dtClone.Rows[dtClone.Rows.Count][2].ToString().Trim();
                price = dtClone.Rows[dtClone.Rows.Count][5].ToString().Trim();
                dtClone.Rows[dtClone.Rows.Count].Delete();
                dtClone.AcceptChanges();
                for (int j = 1; j < dtClone.Rows.Count; j++)
                {
                    if (isbn == dtClone.Rows[j][1].ToString().Trim() && bookName == dtClone.Rows[j][2].ToString().Trim() && price == dtClone.Rows[j][5].ToString().Trim())
                    {
                        //如果重复了，进行记录
                        repeatExcel += "Excel中第" + (i).ToString() + "行有重复\r\n";
                        break;
                    }
                }
            }
            dtClone.Clear();
            Response.Write(repeatExcel);
            Response.End();
            return repeatExcel;
        }
        #endregion

        private string test()
        {
            //excel = excelToDt();
            string s = "";
            excel = npoi();
            try
            {
                string[] strComuns = { "ISBN", "书名", "单价", "进货折扣", "销售折扣", "供应商" };
                if (DataTableHelper.isRepeatDt(excel, strComuns))
                {
                    s = "存在重复记录";
                }
            }
            catch (Exception ex)
            {
                s = ex.ToString() ;
            }
            Response.Write(s);
            Response.End();
            return s;
        }

        private void excelNo()
        {
            DataTable dataTable = addBookId();
            int counts = 0;
            DataRowCollection count = dataTable.Rows;
            foreach (DataRow row in count)//遍历excel数据集
            {
                try
                {
                    string isbn = row[2].ToString().Trim();
                    string bookName = ToSBC(row[3].ToString().Trim());
                    double price = Convert.ToDouble(row[6]);
                        BookBasicData basicData = new BookBasicData();
                        basicData.BookNum = row[0].ToString();
                        basicData.Isbn = isbn;
                        basicData.BookName = bookName;
                        basicData.Publisher = row[4].ToString();
                        basicData.Time = row[5].ToString();
                        //basicData.PublishTime = Convert.ToDateTime(row[5]);
                        basicData.Price = Convert.ToDouble(row[6]);
                        basicData.Catalog = row[7].ToString();
                        basicData.Author = row[8].ToString();
                        basicData.Remarks = row[9].ToString();
                        basicData.Dentification = row[10].ToString();
                    basicData.Remarks1 = row[11].ToString();
                    basicData.Remarks2 = row[12].ToString();
                    basicData.Remarks3 = row[13].ToString();
                    Result result = bookbll.Insert(basicData);
                        if (result == Result.添加失败)
                        {
                            Response.Write("导入失败，可能重复导入");
                            Response.End();
                        }
                        else
                        {
                            Result reg = bookbll.updateBookNum(row[0].ToString()); //更新书号
                            counts++;
                        }
                }
                catch (Exception ex)
                {
                    Response.Write(ex);
                    Response.End();
                }
            }
            int cf = row - counts;
            if (counts == 0)
            {
                Response.Write("导入失败，共导入数据" + counts + "条数据，共有重复数据" + cf + "条");
                Response.End();
            }
            else
            {
                Response.Write("导入成功，共导入数据" + counts + "条数据，共有重复数据" + cf + "条");
                Response.End();
            }
        }

        private DataTable npoi()
        {
            string path = Session["path"].ToString();
            DataTable dtNpoi=new DataTable();
            dtNpoi = ExcelHelp.excelToDt(path,"excel");
            dtNpoi.Columns.Add("id").SetOrdinal(0);
            row = dtNpoi.Rows.Count;
            return dtNpoi;
        }
    }
}