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
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
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
            string op = Request["op"];
            WarehousingBll warehousingBll = new WarehousingBll();
            long flow = (warehousingBll.getCount(Convert.ToInt64(singleHeadId)) + 1);
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
            tbd.StrTable = "V_Monomers";
            tbd.OrderBy = "singleHeadId";
            tbd.StrColumnlist = "singleHeadId,ISBN,number,uPrice,discount,totalPrice,realPrice,shelvesName";
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
                string id = (warehousingBll.getCount(Convert.ToInt64(Session["id"])) + i+1).ToString();
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
    }
}