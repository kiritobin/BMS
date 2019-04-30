using bms.Bll;
using bms.DBHelper;
using bms.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.SalesMGT
{
    using Result = Enums.OpResult;
    public partial class customerRetail : System.Web.UI.Page
    {
        protected DataSet ds;
        protected int pageSize = 20, totalCount, intPageCount,kind,count;
        public double discount,allTotal,allReal;
        SaleHead single = new SaleHead();
        StockBll stockBll = new StockBll();
        BookBasicBll basicBll = new BookBasicBll();
        GoodsShelvesBll goods = new GoodsShelvesBll();
        DataTable monTable = new DataTable();
        RetailBll retailBll = new RetailBll();
        Common com = new Common();
        public List<long> bookNumList = new List<long>();
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["user"];
            getIsbn();
            string op = Request["op"];
            if (op == "add")
            {
                string headId = Request["headId"];
                string bookNum = Request["bookNum"].ToString();
                add(bookNum, headId,"one");
            }
            if (op == "newAdd")
            {
                string headId = insertHead();
                string bookNum = Request["bookNum"].ToString();
                add(bookNum, headId, "one");
            }
            if (op == "delete")
            {
                long bookNum = Convert.ToInt64(Request["bookNum"]);
                bookNumList = (List<long>)Session["List"];
                int index = bookNumList.IndexOf(bookNum);
                bookNumList.RemoveAt(index);
                Session["List"] = bookNumList;
            }
            if (op == "scann")
            {
                scann();
            }
            if(op == "change")
            {
                change();
            }
            if(op == "del")
            {
                delete();
            }
            if(op == "discount")
            {
                Discount();
            }
            if(op == "stock")
            {
                string headId = Request["headId"];
                DataSet dsEnd = retailBll.GetRetail(headId);
                if (dsEnd == null)
                {
                    Response.Write("此单据不存在:|");
                    Response.End();
                }
                else
                {
                    int row = dsEnd.Tables[0].Rows.Count;
                    for (int i = 0; i < row; i++)
                    {
                        DataRow dr = dsEnd.Tables[0].Rows[i];
                        string bookNum = dr["bookNum"].ToString();
                        int number = Convert.ToInt32(dr["number"]);
                        DataSet dsStock = stockBll.SelectByBookNum(bookNum, user.ReginId.RegionId);
                        if (dsStock != null)
                        {
                            int rows = dsStock.Tables[0].Rows.Count;
                            if (rows == 0)
                            {
                                Response.Write("此书籍无库存:|" + dr["bookName"]);
                                Response.End();
                            }
                            else
                            {
                                int stockNum = 0, stockNums = 0;
                                for (int j = 0; j < rows; j++)
                                {
                                    stockNum = Convert.ToInt32(dsStock.Tables[0].Rows[j]["stockNum"]);
                                    stockNums = stockNums + stockNum;
                                }
                                if (stockNums < number)
                                {
                                    Response.Write("此书籍库存不足:|" + dr["bookName"] + "|," + stockNums);
                                    Response.End();
                                }
                            }
                        }
                        else
                        {
                            Response.Write("此书籍无库存:|" + dr["bookName"]);
                            Response.End();
                        }
                    }
                }
            }
            if(op == "end")
            {
                String strConn = ConfigurationManager.ConnectionStrings["sqlConn"].ConnectionString;
                MySqlConnection sqlConn = new MySqlConnection(strConn);
                MySqlTransaction trans = null;
                try
                {
                    if (sqlConn != null && sqlConn.State == ConnectionState.Closed)
                    {
                        sqlConn.Open();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                trans = sqlConn.BeginTransaction();
                MySqlCommand cmd = sqlConn.CreateCommand();
                cmd.Transaction = trans;
                try
                {
                    string headId = Request["headId"];
                    string payType = Request["payType"];
                    DataSet dsEnd = retailBll.GetRetail(headId);
                    if (dsEnd != null && dsEnd.Tables[0].Rows.Count > 0)
                    {
                        int row = dsEnd.Tables[0].Rows.Count;

                        for (int i = 0; i < row; i++)
                        {
                            DataRow dr = dsEnd.Tables[0].Rows[i];
                            string bookNum = dr["bookNum"].ToString();
                            int number = Convert.ToInt32(dr["number"]);
                            count = number;
                            DataSet dsStock = stockBll.SelectByBookNum(bookNum, user.ReginId.RegionId);
                            if (dsStock != null && dsStock.Tables[0].Rows.Count > 0)
                            {
                                int rows = dsStock.Tables[0].Rows.Count;
                                for (int j = 0; j < rows; j++)
                                {
                                    number = count;
                                    int stockNum = Convert.ToInt32(dsStock.Tables[0].Rows[j]["stockNum"]);
                                    string goodsId = dsStock.Tables[0].Rows[j]["goodsShelvesId"].ToString();
                                    if (stockNum > number)
                                    {
                                        cmd.CommandText = "update T_Stock set stockNum="+ (stockNum - count) + " where goodsShelvesId="+ goodsId + " and bookNum='"+ bookNum+"'";
                                        int kucun = cmd.ExecuteNonQuery();
                                        if (kucun <= 0)
                                        {
                                            trans.Rollback();
                                            Response.Write("更新失败:|");
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        count = number - stockNum;
                                        cmd.CommandText = "update T_Stock set stockNum=0 where goodsShelvesId=" + goodsId + " and bookNum='" + bookNum+"'";
                                        int kucun = cmd.ExecuteNonQuery();
                                        if (kucun <= 0)
                                        {
                                            trans.Rollback();
                                            Response.Write("更新失败:|");
                                        }
                                        else if(count == 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Response.Write("此书籍无库存:|" + dr["bookName"].ToString());
                            }
                        }
                        cmd.CommandText = "update T_RetailHead set state=1,userId="+ user.UserId + ",payment='"+ payType + "' where retailHeadId='"+ headId+"'";
                        int kucun2 = cmd.ExecuteNonQuery();
                        if (kucun2 <= 0)
                        {
                            trans.Rollback();
                            Response.Write("更新失败:|");
                        }
                        else
                        {
                            StringBuilder sb = new StringBuilder();
                            DataSet dsNew = retailBll.GetRetail(headId);
                            if (dsNew != null && dsNew.Tables[0].Rows.Count > 0)
                            {
                                int counts = dsNew.Tables[0].Rows.Count;
                                sb.Append("<tbody>");
                                for (int i = 0; i < counts; i++)
                                {
                                    DataRow dr = dsNew.Tables[0].Rows[i];
                                    sb.Append("<tr><td style='font-size:14px;'>" + dr["bookName"].ToString() + "</td>");
                                    sb.Append("<td>" + dr["number"].ToString() + "</td>");
                                    sb.Append("<td>" + dr["unitPrice"].ToString() + "</td></tr>");
                                }
                                sb.Append("</tbody>");
                                trans.Commit();
                                Response.Write("更新成功:|" + sb.ToString());
                            }
                            else
                            {
                                trans.Rollback();
                                Response.Write("更新失败:|");
                            }
                        }
                    }
                    else
                    {
                        Response.Write("此单据不存在:|");
                    }
                }catch(Exception ex)
                {
                    trans.Rollback();
                    Response.Write("更新失败:|");
                }
                finally
                {
                    sqlConn.Close();
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 客户选择图书
        /// </summary>
        /// <returns></returns>
        public string getIsbn()
        {
            string op = Request["op"];
            string isbn = Request["isbn"];
            double disCount = Convert.ToDouble(Request["disCount"]);
            int billCount = Convert.ToInt32(Request["billCount"]);
            if (isbn != null && isbn != "")
            {
                BookBasicBll bookBasicBll = new BookBasicBll();
                DataSet bookDs = bookBasicBll.SelectByIsbn(isbn);
                if (bookDs != null && bookDs.Tables[0].Rows.Count > 0)
                {
                    int count = bookDs.Tables[0].Rows.Count;
                    if (count == 1)
                    {
                        string headId = "", bookNum = "";
                        if (op == "isbn")
                        {
                            headId = Request["headId"];
                            bookNum = bookDs.Tables[0].Rows[0]["bookNum"].ToString();
                        }
                        else if(op == "newRetail")
                        {
                            headId = insertHead();
                            bookNum = bookDs.Tables[0].Rows[0]["bookNum"].ToString();
                        }
                        add(bookNum, headId, "one");
                    }
                    if (op == "choose" || op == "newChoose")
                    {
                        int counts = bookDs.Tables[0].Rows.Count;
                        StringBuilder sb = new StringBuilder();
                        int i = 0;
                        while (i < counts)
                        {
                            DataRow dr = bookDs.Tables[0].Rows[i];
                            User user = (User)Session["user"];
                            string bookNum = dr["bookNum"].ToString();
                            int stockNum = stockBll.selectStockNum(dr["bookNum"].ToString(), user.ReginId.RegionId);
                            if (stockNum <= 0)
                            {
                                bookDs.Tables[0].Rows.RemoveAt(i);
                                counts--;
                            }
                            else
                            {
                                i++;
                            }
                        }
                        if (counts == 0)
                        {
                            Response.Write("无库存|:");
                            Response.End();
                        }
                        else if (counts == 1)
                        {
                            string headId = "";
                            if (op == "choose")
                            {
                                headId = Request["headId"];
                            }
                            else if (op == "newChoose")
                            {
                                headId = insertHead();
                            }
                            add(bookDs.Tables[0].Rows[0]["bookNum"].ToString(), headId, "other");
                        }
                        else
                        {
                            counts = bookDs.Tables[0].Rows.Count;
                            for (int j = 0; j < counts; j++)
                            {
                                DataRow dr = bookDs.Tables[0].Rows[j];
                                sb.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + dr["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                                sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                                sb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
                                sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                                sb.Append("<td>" + dr["price"].ToString() + "</td>");
                                sb.Append("<td>" + dr["supplier"].ToString() + "</td></tr>");
                            }
                            Response.Write("|:" + sb.ToString());
                            Response.End();
                        }
                    }
                    Response.Write("一号多书");
                    Response.End();
                }
                else
                {
                    Response.Write("ISBN不存在");
                    Response.End();
                }
            }
            return null;
        }
        /// <summary>
        /// 客户添加图书
        /// </summary>
        /// <param name="bookNum"></param>
        public void add(string bookNum,string headId,string addType)
        {
            Result record = retailBll.selectByBookNum(bookNum, headId);
            if (record == Result.记录不存在)
            {
                BookBasicData bookBasicData = basicBll.SelectById(bookNum);
                string isbn = bookBasicData.Isbn;
                string bookName = bookBasicData.BookName;
                double discount = 100;
                if (bookBasicData.Remarks != "")
                {
                    discount = Convert.ToDouble(bookBasicData.Remarks);
                }
                discount = discount * 0.01;
                int billCount = Convert.ToInt32(Request["billCount"]);
                if(billCount <= 0)
                {
                    billCount = 1;
                }
                int row = monTable.Rows.Count;
                double uPrice = bookBasicData.Price;
                SaleMonomer monomers = new SaleMonomer();
                double totalPrice = Convert.ToDouble((billCount * uPrice).ToString("0.00"));
                double realPrice = Convert.ToDouble((totalPrice * discount).ToString("0.00"));
                DataSet ds = retailBll.GetRetail(headId);
                int monId = 0;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    int k = ds.Tables[0].Rows.Count - 1;
                    monId = Convert.ToInt32(ds.Tables[0].Rows[k]["retailMonomerId"]);
                }
                monomers.ISBN1 = isbn;
                monomers.UnitPrice = uPrice;
                monomers.BookNum = bookNum;
                monomers.SaleIdMonomerId = monId + 1;
                monomers.RealDiscount = discount * 100;
                monomers.Number = 1;
                monomers.TotalPrice = uPrice;
                monomers.RealPrice = uPrice * discount;
                monomers.SaleHeadId = headId;
                monomers.Datetime = Convert.ToDateTime(com.getDate());
                Result mon = retailBll.InsertRetail(monomers);
                if (mon == Result.添加成功)
                {
                    SaleHead sale = retailBll.GetHead(headId);
                    SaleHead newSale = new SaleHead();
                    newSale.SaleHeadId = headId;
                    newSale.KindsNum = sale.KindsNum + 1;
                    newSale.Number = sale.Number + 1;
                    newSale.AllTotalPrice = sale.AllTotalPrice + monomers.TotalPrice;
                    newSale.AllRealPrice = sale.AllRealPrice + monomers.RealPrice;
                    Result update = retailBll.UpdateHeadNumber(newSale);
                    if (update == Result.更新成功)
                    {
                        StringBuilder sb = new StringBuilder();
                        DataSet dsNew = retailBll.GetRetail(headId);
                        if (dsNew != null && dsNew.Tables[0].Rows.Count > 0)
                        {
                            int counts = dsNew.Tables[0].Rows.Count;
                            for (int i = 0; i < counts; i++)
                            {
                                DataRow dr = dsNew.Tables[0].Rows[i];
                                sb.Append("<tr><td>" + dr["retailMonomerId"].ToString() + "</td>");
                                sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                                sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                                sb.Append("<td>" + dr["unitPrice"].ToString() + "</td>");
                                sb.Append("<td style='display:none'>" + dr["number"].ToString() + "</td>");
                                //sb.Append("<td><input class='numberEnd' type='number' style='width:50px;border:none;' name='points',min='1' value='" + dr["number"].ToString() + "'/></td>");
                                sb.Append("<td><div class='gw_num' style='width:100%'><em class='jian' style='height:100%;width:40%;'>-</em>");
                                sb.Append("<input type = 'text' min='1' value='" + dr["number"].ToString() + "' class='num' readonly='readonly' style='width:20%;height:100%'/>");
                                sb.Append("<em class='add' style='height:100%;width:40%;'>+</em></div></td>");
                                sb.Append("<td>" + dr["realDiscount"].ToString() + "</td>");
                                sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                                sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                                sb.Append("<td style='display:none'>" + dr["bookNum"].ToString() + "</td>");
                                sb.Append("<td><button class='btn btn-danger btn-sm delete'><i class='fa fa-trash'></i></button></td></tr>");
                            }
                            if (addType == "other")
                            {
                                Response.Write(addType + "|:" + sb.ToString() + "|:" + newSale.KindsNum + "," + newSale.Number + "," + newSale.AllTotalPrice + "," + newSale.AllRealPrice + "|:" + headId);
                                Response.End();
                            }
                            else
                            {
                                Response.Write(sb.ToString() + "|:" + newSale.KindsNum + "," + newSale.Number + "," + newSale.AllTotalPrice + "," + newSale.AllRealPrice + "|:" + headId);
                                Response.End();
                            }
                        }
                    }
                    else
                    {
                        Response.Write("添加失败|:");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("添加失败|:");
                    Response.End();
                }
            }
            else
            {
                Response.Write("已添加过此图书|:");
                Response.End();
            }
        }

        /// <summary>
        /// 收银扫描单据查询明细
        /// </summary>
        public void scann()
        {
            string retailId = Request["retailId"];
            SaleHead sale = retailBll.GetHead(retailId);
            if (sale == null)
            {
                Response.Write("记录不存在");
                Response.End();
            }
            else
            {
                int state = retailBll.GetRetailType(retailId);
                if (state == 1)
                {
                    Response.Write("此单据已结算");
                    Response.End();
                }
                else if (state == 2)
                {
                    Response.Write("此单据为退货单据");
                    Response.End();
                }
                else
                {
                    DataSet ds = retailBll.GetRetail(retailId);
                    if (ds == null)
                    {
                        Response.Write("记录不存在");
                        Response.End();
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        int counts = ds.Tables[0].Rows.Count;
                        for (int i = 0; i < counts; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];
                            sb.Append("<tr><td>" + dr["retailMonomerId"].ToString() + "</td>");
                            sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                            sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                            sb.Append("<td>" + dr["unitPrice"].ToString() + "</td>");
                            sb.Append("<td style='display:none'>" + dr["number"].ToString() + "</td>");
                            //sb.Append("<td><input class='numberEnd' type='number' style='width:50px;border:none;' name='points',min='1' value='" + dr["number"].ToString() + "'/></td>");
                            sb.Append("<td><div class='gw_num' style='width:100%'><em class='jian' style='height:100%;width:40%;'>-</em>");
                            sb.Append("<input type = 'text' min='1' value='" + dr["number"].ToString() + "' class='num' readonly='readonly' style='width:20%;height:100%'/>");
                            sb.Append("<em class='add' style='height:100%;width:40%;'>+</em></div></td>");
                            sb.Append("<td>" + dr["realDiscount"].ToString() + "</td>");
                            sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                            sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                            sb.Append("<td style='display:none'>" + dr["bookNum"].ToString() + "</td>");
                            sb.Append("<td><button class='btn btn-danger btn-sm delete'><i class='fa fa-trash'></i></button></td></tr>");
                        }
                        allReal = sale.AllRealPrice;
                        allTotal = sale.AllTotalPrice;
                        count = sale.Number;
                        kind = counts;
                        Response.Write("number:" + allTotal + "," + allReal + "," + count + "," + kind + "|:" + sb.ToString());
                        Response.End();
                    }
                }
            }
        }
        /// <summary>
        /// 收银修改数量
        /// </summary>
        public void change()
        {
            string type = Request["type"];
            int number = Convert.ToInt32(Request["number"]);
            int retailId = Convert.ToInt32(Request["retailId"]);
            string headId = Request["headId"];
            SaleMonomer monomer = retailBll.GetMonomer(retailId, headId);
            int oldNumber = monomer.Number;
            double oldTotal = monomer.TotalPrice;
            double oldReal = monomer.RealPrice;
            double price = monomer.UnitPrice;
            double realDiscount = monomer.RealDiscount;
            double total=0, real=0;
            //if (type == "jia")
            //{
            //    total = oldTotal + price;
            //    real = oldReal + (price * realDiscount * 0.01);
            //}
            //else if(type == "jian")
            //{
            //    total = oldTotal - price;
            //    real = oldReal - (price * realDiscount * 0.01);
            //}
            SaleMonomer sale = new SaleMonomer();
            sale.SaleIdMonomerId = retailId;
            sale.Number = number;
            sale.TotalPrice = number* price;
            sale.RealPrice = number * price* realDiscount*0.01;
            sale.SaleHeadId = headId;
            Result change = retailBll.UpdateNumber(sale);
            if (change == Result.更新成功)
            {
                SaleHead head = retailBll.GetHead(headId);
                SaleHead newHead = new SaleHead();
                int newNumber = 0;
                double newTotal = 0, newReal = 0;
                if (type == "jia")
                {
                    newNumber = head.Number + 1;
                    newTotal = head.AllTotalPrice + price;
                    newReal = head.AllRealPrice + (price * realDiscount * 0.01);
                }
                else if (type == "jian")
                {
                    newNumber = head.Number - 1;
                    newTotal = head.AllTotalPrice - price;
                    newReal = head.AllRealPrice - (price * realDiscount * 0.01);
                }
                newHead.SaleHeadId = headId;
                newHead.Number = newNumber;
                newHead.AllTotalPrice = newTotal;
                newHead.AllRealPrice = newReal;
                newHead.KindsNum = head.KindsNum;
                Result headRe = retailBll.UpdateHeadNumber(newHead);
                if(headRe == Result.更新成功)
                {
                    Response.Write("更新成功|:"+newNumber+","+newTotal+","+newReal+"|:"+ total+"|:"+ real);
                    Response.End();
                }
                else
                {
                    Response.Write("更新失败|:");
                    Response.End();
                }
            }
            else
            {
                Response.Write("更新失败|:");
                Response.End();
            }
        }
        /// <summary>
        /// 收银修改折扣
        /// </summary>
        public void Discount()
        {
            double discount = Convert.ToDouble(Request["discount"]);
            string retailId = Request["retailId"];
            DataSet ds = retailBll.GetRetail(retailId);
            if (ds == null)
            {
                Response.Write("记录不存在");
                Response.End();
            }
            else
            {
                double real = 0, reals = 0, allReal = 0, allReals = 0;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    double total = Convert.ToDouble(ds.Tables[0].Rows[i]["totalPrice"]);
                    real = Convert.ToDouble(ds.Tables[0].Rows[i]["realPrice"]);
                    allReal = allReal + real;
                    reals = total* discount * 0.01;
                    allReals = allReals + reals;
                    Result change = retailBll.UpdateDiscount(discount, reals, retailId);
                    if (change == Result.更新失败)
                    {
                        Response.Write("更新失败");
                        Response.End();
                    }
                }
                SaleHead head = retailBll.GetHead(retailId);
                Result reales = retailBll.UpdateHeadReal(head.AllRealPrice - allReal + allReals, retailId);
                if (reales == Result.更新成功)
                {
                    Response.Write("更新成功");
                    Response.End();
                }
                else{
                    Response.Write("更新失败");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 收银删除
        /// </summary>
        public void delete()
        {
            int retailMonomerId = Convert.ToInt32(Request["retailId"]);
            string retailHeadId = Request["headId"];
            SaleMonomer monomer = retailBll.GetMonomer(retailMonomerId, retailHeadId);
            Result del = retailBll.delete(retailMonomerId, retailHeadId);
            if(del == Result.删除成功)
            {
                string headId = Request["headId"];
                SaleHead saleHead = retailBll.GetHead(headId);
                SaleHead newHead = new SaleHead();
                newHead.SaleHeadId = headId;
                newHead.Number = saleHead.Number - monomer.Number;
                newHead.KindsNum = saleHead.KindsNum - 1;
                newHead.AllTotalPrice = saleHead.AllTotalPrice - monomer.TotalPrice;
                newHead.AllRealPrice = saleHead.AllRealPrice - monomer.RealPrice;
                Result head = retailBll.UpdateHeadNumber(newHead);
                if (head == Result.更新成功)
                {
                    Response.Write("删除成功|:" + newHead.Number + "|:" + newHead.AllTotalPrice + "|:" + newHead.AllRealPrice);
                    Response.End();
                }
                else
                {
                    Response.Write("删除失败|:");
                    Response.End();
                }
            }
            else
            {
                Response.Write("删除失败|:");
                Response.End();
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

        /// <summary>
        /// 支付，打印小票
        /// </summary>
        /// <param name="headId">单头Id</param>
        /// <returns></returns>
        public string Pay(string headId)
        {
            StringBuilder sb = new StringBuilder();
            DataSet dsNew = retailBll.GetRetail(headId);
            if (dsNew != null && dsNew.Tables[0].Rows.Count > 0)
            {
                int counts = dsNew.Tables[0].Rows.Count;
                sb.Append("<tbody>");
                for (int i = 0; i < counts; i++)
                {
                    DataRow dr = dsNew.Tables[0].Rows[i];
                    sb.Append("<tr><td style='font-size:14px;'>" + dr["bookName"].ToString() + "</td>");
                    sb.Append("<td>" + dr["number"].ToString() + "</td>");
                    sb.Append("<td>" + dr["unitPrice"].ToString() + "</td></tr>");
                }
                sb.Append("</tbody>");

                Response.Write("更新成功:|" + sb.ToString());
                return sb.ToString();
            }
            else
            {
                Response.Write("更新失败:|");
                Response.End();
                return null;
            }
        }
        /// <summary>
        /// 添加零售单头
        /// </summary>
        public string insertHead()
        {
            DateTime nowTime = Convert.ToDateTime(com.getDate());
            string nowDt = nowTime.ToString("yyyy-MM-dd");
            long counts = 0;
            //判断数据库中是否已经有记录
            DataSet backds = retailBll.getAllTime(0);
            if (backds != null && backds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < backds.Tables[0].Rows.Count; i++)
                {
                    string time = backds.Tables[0].Rows[i]["dateTime"].ToString();
                    DateTime dt = Convert.ToDateTime(time);
                    string sqlTime = dt.ToString("yyyy-MM-dd");
                    if (sqlTime == nowDt)
                    {
                        //count += 1;
                        string id = backds.Tables[0].Rows[i]["retailHeadId"].ToString();
                        int st1 = Convert.ToInt32(id.Substring(10));
                        if (st1 <= 0)
                        {
                            st1 = 0;
                        }
                        counts = st1 + 1;
                        break;
                    }
                    else
                    {
                        counts = 1;
                        break;
                    }
                }
                if (counts == 0)
                {
                    counts = 1;
                }
            }
            else
            {
                counts = 1;
            }
            User user = (User)Session["user"];
            string retailHeadId = "LS" + Convert.ToDateTime(com.getDate()).ToString("yyyyMMdd") + counts.ToString().PadLeft(6, '0');
            single.AllRealPrice = 0;
            single.AllTotalPrice = 0;
            single.KindsNum = 0;
            single.Number = 0;
            single.RegionId = user.ReginId.RegionId;
            single.SaleHeadId = retailHeadId;
            single.UserId = user.UserId;
            single.DateTime = Convert.ToDateTime(com.getDate());
            single.State = 0;
            single.PayType = "未支付";
            Result result = retailBll.InsertRetail(single);
            if(result== Result.添加失败)
            {
                Response.Write("添加失败");
                Response.End();
            }
            return retailHeadId;
        }
    }
}