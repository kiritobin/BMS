using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.InventoryMGT
{
    using Result = Enums.OpResult;
    public partial class addStock : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20, row, count = 0;
        public DataSet ds, dsGoods,dsPer;
        public DataTable dt;
        public double discount;
        public string singleHeadId = "";
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply;
        BookBasicBll basicBll = new BookBasicBll();
        WarehousingBll warehousingBll = new WarehousingBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
            Monomers monoDiscount = warehousingBll.getDiscount();
            discount = monoDiscount.Discount;
            if (!IsPostBack)
            {
                string id = Request.QueryString["singleHeadId"];
                //string id = "20180926000002";
                if (id != null&& id != "")
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
            getData();
            getIsbn();
            string op = Request["op"];
            long flow = (warehousingBll.getCount(singleHeadId) + 1);
            if (op == "add")
            {
                long bookNum = Convert.ToInt64(Request["bookNum"]);
                BookBasicData bookBasicData = basicBll.SelectById(Convert.ToInt64(bookNum));
                string isbn = bookBasicData.Isbn;
                int billCount = Convert.ToInt32(Request["billCount"]);
                //添加单体信息
                long monomerID = flow;
                long monId;
                if (monomerID > 0)
                {
                    monId = monomerID + 1;
                }
                else
                {
                    monId = 1;
                }
                discount = Convert.ToDouble(Request["discount"]);
                if (discount > 1 && discount <= 10)
                {
                    discount = discount * 0.1;
                }
                else if (discount > 10)
                {
                    discount = discount * 0.01;
                }
                string goodsShelf = Request["goodsShelf"];
                double price = bookBasicData.Price;
                Monomers monomers = new Monomers();
                monomers.MonomersId = Convert.ToInt32(monId);
                SingleHead singleHead = new SingleHead();
                singleHead.SingleHeadId = singleHeadId; 
                monomers.SingleHeadId= singleHead;
                monomers.Discount = discount*100;
                monomers.Number = billCount;
                monomers.Type = 1;
                BookBasicData bookBasic = new BookBasicData();
                bookBasic.Isbn = isbn;
                bookBasic.Price = price;
                bookBasic.BookNum = bookNum;
                monomers.Isbn = bookBasic;
                monomers.UPrice = bookBasic;
                monomers.BookNum = bookBasic;
                monomers.TotalPrice = Convert.ToDouble((price * billCount).ToString("0.00"));
                monomers.RealPrice = Convert.ToDouble((price * billCount * discount).ToString("0.00"));
                Result res = warehousingBll.updateDiscount(discount);
                Result re = warehousingBll.SelectBybookNum(singleHeadId,bookNum.ToString(),1);
                if (re == Result.记录不存在)
                {
                    if (res == Result.更新成功)
                    {
                        Result row = warehousingBll.insertMono(monomers);
                        if(row == Result.添加成功)
                        {//获取单头数据并更新单头
                            int number, allBillCount = 0;
                            double totalPrice, allTotalPrice = 0, realPrices, allRealPrice = 0;
                            DataTable dtHead = warehousingBll.SelectMonomers(singleHeadId);
                            int j = dtHead.Rows.Count;
                            for (int i = 0; i < j; i++)
                            {
                                DataRow dr = dtHead.Rows[i];
                                number = Convert.ToInt32(dr["number"]);
                                totalPrice = Convert.ToDouble(dr["totalPrice"]);
                                realPrices = Convert.ToDouble(dr["realPrice"]);
                                allBillCount = allBillCount + number;
                                allTotalPrice = allTotalPrice + totalPrice;
                                allRealPrice = allRealPrice + realPrices;
                            }
                            singleHead.AllBillCount = allBillCount;
                            singleHead.AllTotalPrice = allTotalPrice;
                            singleHead.AllRealPrice = allRealPrice;
                            Result update = warehousingBll.updateHead(singleHead);
                            if (update == Result.更新成功)
                            {//添加库存信息
                                Stock stock = new Stock();
                                stock.BookNum = bookBasicData;
                                stock.StockNum = allCount;
                                stock.ISBN = bookBasicData;
                                stock.RegionId = user.ReginId;
                                GoodsShelves goodsShelves = new GoodsShelves();
                                goodsShelves.GoodsShelvesId = Convert.ToInt32(goodsShelf);
                                stock.GoodsShelvesId = goodsShelves;
                                StockBll stockBll = new StockBll();
                                Result result = stockBll.insert(stock);
                                if (result == Result.添加成功)
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
                            else
                            {
                                Response.Write("添加失败");
                                Response.End();
                            }
                        }
                        else
                        {
                            Response.Write("添加失败");
                            Response.End();
                        }
                    }
                    else
                    {
                        Response.Write("添加失败");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("已添加过相同记录");
                    Response.End();
                }
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
            else if (action== "showIntersect")
            {
                int count = differentDt().Rows.Count;
                if (count>0)
                {
                    StringBuilder sb = new StringBuilder();
                    UserBll userBll = new UserBll();
                    int pageIndex = Convert.ToInt32(Request["page"]);
                    dt = userBll.SplitDataTable(differentDt(), pageIndex, 1);
                    DataRowCollection drc = dt.Rows;
                    sb.Append("<tbody>");
                    int allPage = count % pageSize;
                    if (allPage==0)
                    {
                        allPage = count/pageSize;
                    }
                    else
                    {
                        allPage = count / pageSize+1;
                    }
                    for (int i=0;i< count; i++)
                    {
                        sb.Append("<tr><td>" + (i + 1 + ((allPage - 1) * pageSize)) + "</td>");
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
                    sb.Append("<input type='hidden' value='" +allPage + "' id='intPageCount2' />");
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

        protected string getData()
        {
            UserBll userBll = new UserBll();
            GoodsShelvesBll goodsShelvesBll = new GoodsShelvesBll();
            User user = (User)Session["user"];
            int regionId = user.ReginId.RegionId;
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_Monomer";
            tbd.OrderBy = "singleHeadId";
            tbd.StrColumnlist = "singleHeadId,ISBN,bookName,supplier,number,uPrice,discount,totalPrice,realPrice";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "deleteState=0 and singleHeadId='" + singleHeadId + "'";
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            //展示货架
            dsGoods = goodsShelvesBll.Select(regionId);
            //生成table
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            int count = ds.Tables[0].Rows.Count;
            DataRowCollection drc = ds.Tables[0].Rows;
            for (int i = 0; i < count; i++)
            {
                sb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + drc[i]["singleHeadId"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["ISBN"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["bookName"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["supplier"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["number"].ToString() + "</td>");
                sb.Append("<td>" + drc[i]["uPrice"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["discount"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["totalPrice"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["realPrice"].ToString() + "</td ></tr>");
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
                string id = (warehousingBll.getCount(Session["id"].ToString()) + i+1).ToString();
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

        /// <summary>
        /// 通过isbn获取图书基础信息
        /// </summary>
        /// <returns></returns>
        public string getIsbn()
        {
            string op = Request["op"];
            string isbn = Request["isbn"];
            if (isbn != null && isbn != "")
            {
                StringBuilder sb = new StringBuilder();
                DataSet dsBook = basicBll.SelectByIsbn(isbn);
                if (dsBook == null || dsBook.Tables[0].Rows.Count <= 0)
                {
                    Response.Write("ISBN不存在");
                    Response.End();
                }
                else
                {
                    int count = dsBook.Tables[0].Rows.Count;
                    if (count == 1)
                    {//添加单体信息
                        double disCount = Convert.ToDouble(Request["disCount"]);
                        if (disCount > 1 && disCount <= 10)
                        {
                            disCount = disCount * 0.1;
                        }
                        else if (disCount > 10)
                        {
                            disCount = disCount * 0.01;
                        }
                        int billCount = Convert.ToInt32(Request["billCount"]);
                        int goodsShelf = Convert.ToInt32(Request["goodsShelf"]);
                        DataSet bookDs = basicBll.SelectByIsbn(isbn);
                        double price = Convert.ToDouble(bookDs.Tables[0].Rows[0]["price"]);
                        long bookNum = Convert.ToInt64(bookDs.Tables[0].Rows[0]["bookNum"]);
                        double totalPrice = Convert.ToDouble((billCount * price).ToString("0.00"));
                        double realPrice = Convert.ToDouble((totalPrice * disCount).ToString("0.00"));
                        long monCount = warehousingBll.getCount(singleHeadId);
                        long monId;
                        if (monCount > 0)
                        {
                            monId = monCount + 1;
                        }
                        else
                        {
                            monId = 1;
                        }
                        Monomers monomers = new Monomers();
                        BookBasicData bookBasic = new BookBasicData();
                        SingleHead singleHead = new SingleHead();
                        bookBasic.Isbn = isbn;
                        bookBasic.Price = price;
                        bookBasic.BookNum = bookNum;
                        singleHead.SingleHeadId = singleHeadId;
                        monomers.Isbn = bookBasic;
                        monomers.UPrice = bookBasic;
                        monomers.BookNum = bookBasic;
                        monomers.Discount = disCount * 100;
                        monomers.MonomersId = Convert.ToInt32(monId);
                        monomers.Number = billCount;
                        monomers.TotalPrice = totalPrice;
                        monomers.RealPrice = realPrice;
                        monomers.SingleHeadId = singleHead;
                        monomers.Type = 0;
                        Result re = warehousingBll.SelectBybookNum(singleHeadId, bookNum.ToString(), 1);
                        Result res = warehousingBll.updateDiscount(disCount);
                        if (re == Result.记录不存在)
                        {
                            if (res == Result.更新成功)
                            {
                                Result row = warehousingBll.insertMono(monomers);
                                if (row == Result.添加成功)
                                {//获取单头数据并更新单头
                                    int number, allBillCount = 0;
                                    double totalPrices, allTotalPrice = 0, realPrices, allRealPrice = 0;
                                    DataTable dtHead = warehousingBll.SelectMonomers(singleHeadId);
                                    int j = dtHead.Rows.Count;
                                    for (int i = 0; i < j; i++)
                                    {
                                        DataRow dr = dtHead.Rows[i];
                                        number = Convert.ToInt32(dr["number"]);
                                        totalPrices = Convert.ToDouble(dr["totalPrice"]);
                                        realPrices = Convert.ToDouble(dr["realPrice"]);
                                        allBillCount = allBillCount + number;
                                        allTotalPrice = allTotalPrice + totalPrices;
                                        allRealPrice = allRealPrice + realPrices;
                                    }
                                    singleHead.AllBillCount = allBillCount;
                                    singleHead.AllTotalPrice = allTotalPrice;
                                    singleHead.AllRealPrice = allRealPrice;
                                    Result update = warehousingBll.updateHead(singleHead);
                                    if (update == Result.更新成功)
                                    {//添加库存信息
                                        User user = (User)Session["user"];
                                        Stock stock = new Stock();
                                        stock.StockNum = billCount;
                                        stock.ISBN = bookBasic;
                                        stock.BookNum = bookBasic;
                                        stock.RegionId = user.ReginId;
                                        GoodsShelves goodsShelves = new GoodsShelves();
                                        goodsShelves.GoodsShelvesId = goodsShelf;
                                        stock.GoodsShelvesId = goodsShelves;
                                        StockBll stockBll = new StockBll();
                                        Result results = stockBll.GetByBookNum(bookNum, goodsShelf);
                                        if (results == Result.记录不存在)
                                        {
                                            Result result = stockBll.insert(stock);
                                            if (result == Result.添加成功)
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
                                        else
                                        {
                                            int rows = stockBll.getStockNum(bookNum, goodsShelf);
                                            Result result = stockBll.update(billCount + rows, goodsShelf, bookNum);
                                            if (result == Result.更新成功)
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
                                    else
                                    {
                                        Response.Write("添加失败");
                                        Response.End();
                                    }
                                }
                                else
                                {
                                    Response.Write("添加失败");
                                    Response.End();
                                }
                            }
                        }
                    }
                    else
                    {
                        //生成table
                        sb.Append("<tbody id='tbody'>");
                        for (int i = 0; i < count; i++)
                        {
                            DataRow dr = dsBook.Tables[0].Rows[i];
                            //sb.Append("<tr><td><input type='checkbox' name='checkbox' class='check' value='" + dr["bookNum"].ToString() + "' /></td>");
                            //sb.Append("<tr><td><input type='radio' name='radio' class='radio' value='" + dr["bookNum"].ToString() + "' /></td>");
                            sb.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + dr["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                            sb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
                            sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                            sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                            sb.Append("<td>" + dr["price"].ToString() + "</td>");
                            sb.Append("<td>" + dr["supplier"].ToString() + "</td></tr>");
                        }
                        sb.Append("</tbody>");
                        if (op == "isbn")
                        {
                            Response.Write(sb.ToString());
                            Response.End();
                        }
                    }
                }
                return sb.ToString();
            }
            return null;
        }

        protected void permission()
        {
            FunctionBll functionBll = new FunctionBll();
            User user = (User)Session["user"];
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