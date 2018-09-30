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
        public DataSet ds, dsGoods;
        public DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            string singleHeadId="";
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
            string op = Request["op"];
            WarehousingBll warehousingBll = new WarehousingBll();
            long flow = (warehousingBll.getCount(singleHeadId) + 1);
            if (op == "add")
            {
                string monomerID = flow.ToString();
                string isbn = Request["isbn"];
                string allCount = Request["allCount"];
                string price = Request["price"];
                string discount = Request["discount"];
                string realPrice = Request["realPrice"];
                string allPrice = Request["allPrice"];
                string goodsShelf = Request["goodsShelf"];
                Monomers monomers = new Monomers();
                monomers.MonomersId = Convert.ToInt32(monomerID);
                SingleHead singleHead = new SingleHead();
                singleHead.SingleHeadId = singleHeadId; 
                monomers.SingleHeadId= singleHead;
                BookBasicData bookBasicData = new BookBasicData();
                bookBasicData.Isbn = isbn;
                monomers.Isbn = bookBasicData;
                monomers.Number = Convert.ToInt32(allCount);
                bookBasicData.Price = Convert.ToDouble(price);
                monomers.UPrice = bookBasicData;
                monomers.TotalPrice = Convert.ToDouble(allPrice);
                monomers.RealPrice = Convert.ToDouble(realPrice);
                monomers.Discount = Convert.ToDouble(discount);
                monomers.Type = 1;
                WarehousingBll wareBll = new WarehousingBll();
                Result row = wareBll.insertMono(monomers);
                if(row == Result.添加成功)
                {
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
                    Result update = wareBll.updateHead(singleHead);
                    if (update == Result.更新成功)
                    {
                        Stock stock = new Stock();
                        stock.StockNum = Convert.ToInt32(allCount);
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
            tbd.StrTable = "T_Monomers";
            tbd.OrderBy = "singleHeadId";
            tbd.StrColumnlist = "singleHeadId,ISBN,number,uPrice,discount,totalPrice,realPrice";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "";
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