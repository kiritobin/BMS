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
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.InventoryMGT
{
    using Result = Enums.OpResult;
    public partial class addStock : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20, row, count = 0;
        public DataSet ds, dsGoods, dsPer;
        public DataTable dt;
        public double discount;
        public string singleHeadId = "";
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply;
        BookBasicBll basicBll = new BookBasicBll();
        WarehousingBll warehousingBll = new WarehousingBll();
        GoodsShelvesBll goodsShelvesBll = new GoodsShelvesBll();
        BookBasicBll bookBasicBll = new BookBasicBll();
        public List<long> bookNumList = new List<long>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string kind = Request["kind"];
            if (kind == "0")
            {
                Session["List"] = new List<long>();
            }
            permission();
            Monomers monoDiscount = warehousingBll.getDiscount();
            discount = (monoDiscount.Discount) / 100;
            selectIsbn();
            if (!IsPostBack)
            {
                string id = Request.QueryString["singleHeadId"];
                //string id = "20180926000002";
                if (id != null && id != "")
                {
                    Session["id"] = id;
                    singleHeadId = id;
                }
                else
                {
                    singleHeadId = Session["id"].ToString();
                }
            }
            User user = (User)Session["user"];
            int regionId = user.ReginId.RegionId;
            dsGoods = goodsShelvesBll.Select(regionId);
            string op = Request["op"];
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
            if (op == "delete")
            {
                long bookNum = Convert.ToInt64(Request["bookNum"]);
                bookNumList = (List<long>)Session["List"];
                int index = bookNumList.IndexOf(bookNum);
                bookNumList.RemoveAt(index);
                Session["List"] = bookNumList;
            }
            string action = Request["action"];
            if (action == "import")
            {
                DataTable dtInsert = new DataTable();
                UserBll userBll = new UserBll();
                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                //dtInsert = differentDt();
                dtInsert.Columns.Remove("书名");
                int j = dtInsert.Rows.Count;
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
            else if (action == "showIntersect")
            {
                int count = differentDt().Rows.Count;
                if (count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    UserBll userBll = new UserBll();
                    //int pageIndex = Convert.ToInt32(Request["page"]);
                    //dt = userBll.SplitDataTable(differentDt(), pageIndex, 1);
                    DataRowCollection drc = differentDt().Rows;
                    sb.Append("<tbody>");
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append("<tr><td>" + i + "</td>");
                        sb.Append("<td>" + drc[i]["单头ID"].ToString() + "</td >");
                        sb.Append("<td>" + drc[i]["书名"].ToString() + "</td >");
                        sb.Append("<td>" + drc[i]["书号"].ToString() + "</td>");
                        sb.Append("<td>" + drc[i]["ISBN"].ToString() + "</td >");
                        sb.Append("<td>" + drc[i]["商品数量"].ToString() + "</td >");
                        sb.Append("<td>" + drc[i]["单价"].ToString() + "</td >");
                        sb.Append("<td>" + drc[i]["码洋"].ToString() + "</td >");
                        sb.Append("<td>" + drc[i]["实洋"].ToString() + "</td >");
                        sb.Append("<td>" + drc[i]["折扣"].ToString() + "</td >");
                        sb.Append("<td>" + drc[i]["流水号"].ToString() + "</td ></tr>");
                    }
                    sb.Append("</tbody>");
                    Response.Write(sb.ToString());
                    Response.End();
                }
                else
                {
                    Response.Write("库存中找不到数据");
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 读取excel数据到table中
        /// </summary>
        /// <returns></returns>
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
                sid.DefaultValue = Session["id"].ToString(); //默认值列
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

        /// <summary>
        /// 流水号
        /// </summary>
        /// <returns></returns>
        private DataTable serialNumber()
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
                string id = (warehousingBll.getCount(Session["id"].ToString()) + i + 1).ToString();
                dataRow = dt.NewRow();
                dataRow["流水号"] = id;
                dt.Rows.Add(id);
            }
            return UniteDataTable(excelToDt(), dt);
        }

        /// <summary>
        /// 合并两个table方法
        /// </summary>
        /// <param name="udt1">表1</param>
        /// <param name="udt2">表2</param>
        /// <returns></returns>
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

        /// <summary>
        /// 取两个table的差集
        /// </summary>
        /// <returns></returns>
        private DataTable differentDt()
        {
            DataTable intersect = new DataTable();//接受交集
            WarehousingBll warehousingBll = new WarehousingBll();
            int j = warehousingBll.getISBNbook().Rows.Count;
            //数据库无数据时直接导入excel
            if (j <= 0)
            {
                intersect = serialNumber();
            }
            else
            {
                intersect.Columns.Add("id", typeof(int));
                intersect.Columns.Add("单头ID", typeof(string));
                intersect.Columns.Add("书名", typeof(string));
                intersect.Columns.Add("书号", typeof(string));
                intersect.Columns.Add("ISBN", typeof(string));
                intersect.Columns.Add("商品数量", typeof(int));
                intersect.Columns.Add("单价", typeof(double));
                intersect.Columns.Add("码洋", typeof(double));
                intersect.Columns.Add("实洋", typeof(double));
                intersect.Columns.Add("折扣", typeof(double));
                intersect.Columns.Add("type", typeof(int));
                intersect.Columns.Add("流水号", typeof(string));

                DataRowCollection count = serialNumber().Rows;
                foreach (DataRow row in count)//遍历excel数据集
                {
                    string bookName = row[2].ToString().Trim();
                    string isbn = row[4].ToString().Trim();
                    DataRow[] rows = warehousingBll.getISBNbook().Select(string.Format("ISBN='{0}' and bookName='{1}'", isbn, ToSBC(bookName)));
                    if (rows.Length != 0)//判断如果DataRow.Length为0，即该行excel数据不存在于表A中，就插入到dt3
                    {
                        intersect.Rows.Add(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11]);
                    }
                }
            }

            return intersect;
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

        protected void selectIsbn()
        {
            //货架
            GoodsShelvesBll goodsShelvesBll = new GoodsShelvesBll();
            User user = (User)Session["user"];
            int regionId = user.ReginId.RegionId;
            dsGoods = goodsShelvesBll.Select(regionId);

            string action = Request["action"];
            string isbn = Request["isbn"];
            DataTable monTable = new DataTable();
            DataRow dr = monTable.NewRow();
            
            if (action == "isbn")
            {
                long monId = Convert.ToInt64(Request["sid"]);
                DataSet dsBook = basicBll.SelectByIsbn(isbn);
                if (dsBook == null || dsBook.Tables[0].Rows.Count <= 0)
                {
                    Response.Write("ISBN不存在");
                    Response.End();
                }
                else
                {
                    int billCount = Convert.ToInt32(Request["billCount"]);
                    double disCount = Convert.ToDouble(Request["discount"]);

                    string bookName = dsBook.Tables[0].Rows[0]["bookName"].ToString();
                    string bookNum = dsBook.Tables[0].Rows[0]["bookNum"].ToString();
                    string supplier = dsBook.Tables[0].Rows[0]["supplier"].ToString();
                    double uPrice = Convert.ToDouble(dsBook.Tables[0].Rows[0]["price"]);
                    double totalPrice = Convert.ToDouble((billCount * uPrice).ToString("0.00"));
                    double realPrice = Convert.ToDouble((totalPrice * disCount).ToString("0.00"));

                    monTable.Columns.Add("ISBN", typeof(string));
                    monTable.Columns.Add("uPrice", typeof(double));
                    monTable.Columns.Add("bookName", typeof(string));
                    monTable.Columns.Add("bookNum", typeof(string));
                    monTable.Columns.Add("discount", typeof(double));
                    monTable.Columns.Add("monId", typeof(long));
                    monTable.Columns.Add("number", typeof(string));
                    monTable.Columns.Add("totalPrice", typeof(double));
                    monTable.Columns.Add("realPrice", typeof(double));
                    monTable.Columns.Add("singleHeadId", typeof(string));
                    monTable.Columns.Add("supplier", typeof(string));

                    dr["ISBN"] = isbn;
                    dr["uPrice"] = uPrice;
                    dr["bookName"] = bookName;
                    dr["bookNum"] = bookNum;
                    dr["discount"] = discount;
                    dr["monId"] = monId;
                    dr["number"] = billCount;
                    dr["totalPrice"] = totalPrice;
                    dr["realPrice"] = realPrice;
                    dr["singleHeadId"] = singleHeadId;
                    dr["supplier"] = supplier;
                    monTable.Rows.Add(dr);
                    int count = dsBook.Tables[0].Rows.Count;
                    StringBuilder sb = new StringBuilder();
                    if (count == 1)
                    {
                        StringBuilder sbGoods = new StringBuilder();
                        sbGoods.Append("<select class='goods'>");
                        for (int j = 0; j < dsGoods.Tables[0].Rows.Count; j++)
                        {
                            sbGoods.Append("<option value='" + dsGoods.Tables[0].Rows[j]["goodsShelvesId"] + "'>" + dsGoods.Tables[0].Rows[j]["shelvesName"] + "</option>");
                        }
                        sbGoods.Append("</select>");
                        for (int i = 0; i < monTable.Rows.Count; i++)
                        {
                            bookNumList = (List<long>)Session["List"];
                            foreach (long bookNums in bookNumList)
                            {
                                if (bookNums == Convert.ToInt64(monTable.Rows[i]["bookNum"]))
                                {
                                    Response.Write("已添加过此图书");
                                    Response.End();
                                }
                            }
                            bookNumList.Add(Convert.ToInt64(monTable.Rows[i]["bookNum"]));
                            Session["List"] = bookNumList;
                            sb.Append("<tr><td>" + monTable.Rows[i]["monId"] + "</td>");
                            sb.Append("<td><textarea class='isbn textareaISBN' row='1' maxlength='13'>" + monTable.Rows[i]["ISBN"] + "</textarea></td>");
                            sb.Append("<td style='display:none'>" + monTable.Rows[i]["bookNum"] + "</td>");
                            sb.Append("<td>" + monTable.Rows[i]["bookName"] + "</td>");
                            sb.Append("<td>" + monTable.Rows[i]["supplier"] + "</td>");
                            sb.Append("<td>" + sbGoods.ToString() + "</td>");
                            sb.Append("<td><textarea class='count textareaCount' row='1'>" + 0 + "</textarea></td>");
                            sb.Append("<td>" + monTable.Rows[i]["uPrice"] + "</td>");
                            sb.Append("<td><textarea class='discount textareaDiscount' row='1'>" + monTable.Rows[i]["discount"] + "</textarea></td>");
                            sb.Append("<td>" + monTable.Rows[i]["totalPrice"] + "</td>");
                            sb.Append("<td>" + monTable.Rows[i]["realPrice"] + "</td>");
                            sb.Append("<td><button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button></td>");
                            sb.Append("<td style='display:none'></td></tr>");
                        }
                        Response.Write(sb.ToString());
                        Response.End();
                    }
                    else
                    {
                        StringBuilder sbGoods = new StringBuilder();
                        sbGoods.Append("<select class='goods'>");
                        for (int j = 0; j < dsGoods.Tables[0].Rows.Count; j++)
                        {
                            sbGoods.Append("<option value='" + dsGoods.Tables[0].Rows[j]["goodsShelvesId"] + "'>" + dsGoods.Tables[0].Rows[j]["shelvesName"] + "</option>");
                        }
                        sbGoods.Append("</select>");

                        sb.Append("<tbody>");
                        for (int i = 0; i < dsBook.Tables[0].Rows.Count; i++)
                        {
                            sb.Append("<tr><td><div class='pretty inline much'><input type = 'radio' name='radio' value='" + dsBook.Tables[0].Rows[i]["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                            sb.Append("<td>" + dsBook.Tables[0].Rows[i]["bookNum"].ToString() + "</td>");
                            sb.Append("<td>" + dsBook.Tables[0].Rows[i]["ISBN"].ToString() + "</td>");
                            sb.Append("<td>" + dsBook.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                            sb.Append("<td>" + dsBook.Tables[0].Rows[i]["price"].ToString() + "</td>");
                            sb.Append("<td>" + dsBook.Tables[0].Rows[i]["supplier"].ToString() + "</td></tr>");
                        }
                        sb.Append("</tbody>");
                        Response.Write(sb.ToString());
                        Response.End();
                        //Response.Write("一号多书");
                        //Response.End();
                    }
                }
            }
            else if (action == "add")
            {
                long monId = Convert.ToInt64(Request["sid"]);
                monTable.Columns.Add("ISBN", typeof(string));
                monTable.Columns.Add("uPrice", typeof(double));
                monTable.Columns.Add("bookName", typeof(string));
                monTable.Columns.Add("bookNum", typeof(string));
                monTable.Columns.Add("discount", typeof(double));
                monTable.Columns.Add("monId", typeof(long));
                monTable.Columns.Add("number", typeof(string));
                monTable.Columns.Add("totalPrice", typeof(double));
                monTable.Columns.Add("realPrice", typeof(double));
                monTable.Columns.Add("singleHeadId", typeof(string));
                monTable.Columns.Add("supplier", typeof(string));

                long bookNum = Convert.ToInt64(Request["bookNum"]);
                BookBasicData bookBasicData = bookBasicBll.SelectById(bookNum);
                string supplier = bookBasicData.Publisher;
                string bookName = bookBasicData.BookName;
                string publisher = bookBasicData.Publisher;
                double price = bookBasicData.Price;
                string _isbn = bookBasicData.Isbn;

                bookNumList = (List<long>)Session["List"];
                foreach (long bookNums in bookNumList)
                {
                    if (bookNums == bookNum)
                    {
                        Response.Write("已添加过此图书");
                        Response.End();
                    }
                }

                dr["ISBN"] = _isbn;
                dr["uPrice"] = price;
                dr["bookName"] = bookName;
                dr["bookNum"] = bookNum;
                dr["discount"] = discount;
                dr["monId"] = monId;
                dr["number"] = 0;
                dr["totalPrice"] = 0;
                dr["realPrice"] = 0;
                dr["singleHeadId"] = singleHeadId;
                dr["supplier"] = supplier;
                monTable.Rows.Add(dr);
                for (int k = 0; k < monTable.Rows.Count; k++)
                {
                    StringBuilder sbGoods = new StringBuilder();
                    StringBuilder sb = new StringBuilder();
                    sbGoods.Append("<select class='goods'>");
                    for (int j = 0; j < dsGoods.Tables[0].Rows.Count; j++)
                    {
                        sbGoods.Append("<option value='" + dsGoods.Tables[0].Rows[j]["goodsShelvesId"] + "'>" + dsGoods.Tables[0].Rows[j]["shelvesName"] + "</option>");
                    }
                    sbGoods.Append("</select>");
                    for (int i = 0; i < monTable.Rows.Count; i++)
                    {
                        sb.Append("<tr><td>" + monTable.Rows[i]["monId"] + "</td>");
                        sb.Append("<td><textarea class='isbn textareaISBN' row='1' maxlength='13'>" + monTable.Rows[i]["ISBN"] + "</textarea></td>");
                        sb.Append("<td style='display:none'>" + monTable.Rows[i]["bookNum"] + "</td>");
                        sb.Append("<td>" + monTable.Rows[i]["bookName"] + "</td>");
                        sb.Append("<td>" + monTable.Rows[i]["supplier"] + "</td>");
                        sb.Append("<td>" + sbGoods.ToString() + "</td>");
                        sb.Append("<td><textarea class='count textareaCount' row='1'>" + monTable.Rows[i]["number"] + "</textarea></td>");
                        sb.Append("<td>" + monTable.Rows[i]["uPrice"] + "</td>");
                        sb.Append("<td><textarea class='discount textareaDiscount' row='1'>" + monTable.Rows[i]["discount"] + "</textarea></td>");
                        sb.Append("<td>" + monTable.Rows[i]["totalPrice"] + "</td>");
                        sb.Append("<td>" + monTable.Rows[i]["realPrice"] + "</td>");
                        sb.Append("<td><button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button></td>");
                        sb.Append("<td style='display:none'></td></tr>");
                        bookNumList.Add(bookNum);
                        Session["List"] = bookNumList;
                    }
                    Response.Write(sb.ToString());
                    Response.End();
                }
            }
            else if (action == "save")
            {
                string json = Request["json"];
                DataTable dataTable = jsonToDt(json);
                int Count = dataTable.Rows.Count;
                Monomers monomers = new Monomers();
                BookBasicData book = new BookBasicData();
                int count, counts=0;
                double total, allTotal=0, real, allReal=0;
                for (int i = 0; i < Count; i++)
                {
                    DataRow drow = dataTable.Rows[i];
                    book.Isbn = drow["ISBN号"].ToString();
                    book.Price = Convert.ToDouble(drow["单价"]);
                    book.BookNum = Convert.ToInt64(drow["书号"]);
                    monomers.Isbn = book;
                    monomers.UPrice = book;
                    monomers.BookNum = book;
                    monomers.Discount = Convert.ToDouble(drow["折扣"]);
                    monomers.MonomersId = Convert.ToInt32(drow["单据编号"]);
                    monomers.Number = Convert.ToInt32(drow["商品数量"]);
                    monomers.TotalPrice = Convert.ToDouble(drow["码洋"]);
                    monomers.RealPrice = Convert.ToDouble(drow["实洋"]);
                    SingleHead single = new SingleHead();
                    single.SingleHeadId = Session["id"].ToString();
                    monomers.SingleHeadId = single;
                    count = Convert.ToInt32(drow["商品数量"]);
                    counts = counts + count;
                    total = Convert.ToDouble(drow["码洋"]);
                    allTotal = allTotal + total;
                    real = Convert.ToDouble(drow["实洋"]);
                    allReal = allReal + real;
                    Result row = warehousingBll.insertMono(monomers);
                    if (row == Result.添加失败)
                    {
                        Response.Write("添加失败");
                        Response.End();
                    }
                    else if (row == Result.添加成功)
                    {

                        BookBasicData bookBasic = new BookBasicData();
                        bookBasic.Isbn = book.Isbn;
                        bookBasic.BookNum = Convert.ToInt64(drow["书号"]);
                        Stock stock = new Stock();
                        stock.StockNum = Convert.ToInt32(drow["商品数量"]);
                        stock.ISBN = bookBasic;
                        stock.BookNum = bookBasic;
                        stock.RegionId = user.ReginId;
                        GoodsShelves goodsShelves = new GoodsShelves();
                        int goodsShelf = Convert.ToInt32(drow["货架号"]);
                        goodsShelves.GoodsShelvesId = goodsShelf;
                        stock.GoodsShelvesId = goodsShelves;
                        StockBll stockBll = new StockBll();
                        Result results = stockBll.GetByBookNum(Convert.ToInt64(drow["书号"]), goodsShelf);
                        if (results == Result.记录不存在)
                        {
                            Result result = stockBll.insert(stock);
                            if (result == Result.添加失败)
                            {
                                Response.Write("添加失败");
                                Response.End();
                            }
                        }
                        else
                        {
                            int rows = stockBll.getStockNum(Convert.ToInt64(drow["书号"]), goodsShelf);
                            Result result = stockBll.update(Convert.ToInt32(drow["商品数量"]) + rows, goodsShelf, Convert.ToInt64(drow["书号"]));
                            if (result == Result.更新失败)
                            {
                                Response.Write("添加失败");
                                Response.End();
                            }
                        }
                    }
                }
                SingleHead singleHead = new SingleHead();
                singleHead.SingleHeadId = Session["id"].ToString();
                singleHead.AllBillCount = counts;
                singleHead.AllRealPrice = allReal;
                singleHead.AllTotalPrice = allTotal;
                Result head = warehousingBll.updateHead(singleHead);
                if (head == Result.更新成功)
                {
                    Response.Write("添加成功");
                    Response.End();
                }
                else
                {
                    Response.Write("添加失败");
                    Response.End();
                }
            }
        }
            /// <summary>
            /// Json 字符串 转换为 DataTable数据集合
            /// </summary>
            /// <param name="json"></param>
            /// <returns></returns>
            public DataTable jsonToDt(string json)
            {
                DataTable dataTable = new DataTable();  //实例化
                DataTable result;
                try
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                    ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                    if (arrayList.Count > 0)
                    {
                        foreach (Dictionary<string, object> dictionary in arrayList)
                        {
                            if (dictionary.Keys.Count<string>() == 0)
                            {
                                result = dataTable;
                                return result;
                            }
                            //Columns
                            if (dataTable.Columns.Count == 0)
                            {
                                foreach (string current in dictionary.Keys)
                                {
                                    dataTable.Columns.Add(current, dictionary[current].GetType());
                                }
                            }
                            //Rows
                            DataRow dataRow = dataTable.NewRow();
                            foreach (string current in dictionary.Keys)
                            {
                                dataRow[current] = dictionary[current];
                            }
                            dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                        }
                    }
                }
                catch
                {
                }
                result = dataTable;
                return result;
            }
            protected void permission()
            {
                User user = (User)Session["user"];
                int regionId = user.ReginId.RegionId;
                FunctionBll functionBll = new FunctionBll();
                Role role = new Role();
                role = user.RoleId;
                int roleId = role.RoleId;
                dsPer = functionBll.SelectByRoleId(roleId);
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
                }
            }
        }
    }