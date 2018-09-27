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
        public DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            string singleHeadId="";
            if (!IsPostBack)
            {
                //string id = Request.QueryString["singleHeadId"];
                string id = "20180926000002";
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
            string op = Request["op"];
            if(op == "add")
            {
                string monomerID = Request["ID"];
                string isbn = Request["isbn"];
                string allCount = Request["allCount"];
                string price = Request["price"];
                string discount = Request["discount"];
                string realPrice = Request["realPrice"];
                string allPrice = Request["allPrice"];
                string goodsShelf = Request["goodsShelf"];
                string remark = Request["remark"];
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
                GoodsShelves goodsShelves = new GoodsShelves();
                goodsShelves.GoodsShelvesId = Convert.ToInt32(goodsShelf);
                monomers.GoodsShelvesId = goodsShelves;
                monomers.Type = 1;
                WarehousingBll wareBll = new WarehousingBll();
                Result row = wareBll.insertMono(monomers);
                if(row == Result.添加成功)
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
                UserBll userBll = new UserBll();
                DataTable dtInsert = new DataTable();
                System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                dtInsert = excelToDt();
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
        }

        protected string getData()
        {
            UserBll userBll = new UserBll();
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_Monomers";
            tbd.OrderBy = "singleHeadId";
            tbd.StrColumnlist = "singleHeadId,ISBN,number,uPrice,discount,totalPrice,realPrice,shelvesName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "";
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);

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
                sb.Append("<td>" + drc[i]["realPrice"].ToString() + "</td >");
                sb.Append("<td>" + drc[i]["shelvesName"].ToString() + "</td ></tr >");
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
            WarehousingBll warehousingBll = new WarehousingBll();
            string h2o = (warehousingBll.countHead(1)+1).ToString().PadLeft(6, '0');
            string now = DateTime.Now.ToString("yyyyMMdd");
            string id = "RK"+now+ h2o;
            string path = Session["path"].ToString();
            DataTable dt1 = new DataTable();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
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
                DataColumn dcId = new DataColumn("dcId", typeof(string));
                DataColumn dcH2o = new DataColumn("dcH2o", typeof(string));
                dcId.DefaultValue = Session[id].ToString(); //默认值列
                dcH2o.DefaultValue = (id);
                dt1.Columns.Add(dcId);
                dt1.Columns.Add(dcH2o);
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
            conn.Close();
            return dt1;
        }
    }
}