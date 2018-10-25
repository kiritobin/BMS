using bms.Bll;
using bms.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        UserBll userBll = new UserBll();
        StockBll stockBll = new StockBll();
        BookBasicBll basicBll = new BookBasicBll();
        GoodsShelvesBll goods = new GoodsShelvesBll();
        DataTable monTable = new DataTable();
        RetailBll retailBll = new RetailBll();
        public List<long> bookNumList = new List<long>();
        protected void Page_Load(object sender, EventArgs e)
        {
            getIsbn();
            string op = Request["op"];
            if (op == "add")
            {
                string headId = Request["headId"];
                string bookNum = Request["bookNum"].ToString();
                add(bookNum, headId);
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
            if(op== "change")
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
                        User user = (User)Session["user"];
                        DataSet dsStock = stockBll.SelectByBookNum(bookNum, user.ReginId.RegionId);
                        int rows = dsStock.Tables[0].Rows.Count;
                        if (rows == 0)
                        {
                            Response.Write("此书籍无库存:|" + dr["bookName"]);
                            Response.End();
                        }
                        else
                        {
                            int stockNum = 0, stockNums=0;
                            for (int j = 0; j < rows; j++)
                            {
                                stockNum = Convert.ToInt32(dsStock.Tables[0].Rows[j]["stockNum"]);
                                stockNums = stockNums + stockNum;
                            }
                            if (stockNums < number)
                            {
                                Response.Write("此书籍库存不足:|" + dr["bookName"]+"|,"+ stockNums);
                                Response.End();
                            }
                        }
                    }
                }
            }
            if(op == "end")
            {
                string headId = Request["headId"];
                DataSet dsEnd = retailBll.GetRetail(headId);
                int row = dsEnd.Tables[0].Rows.Count;
                for (int i = 0; i < row; i++)
                {
                    DataRow dr = dsEnd.Tables[0].Rows[i];
                    string bookNum = dr["bookNum"].ToString();
                    int number = Convert.ToInt32(dr["number"]);
                    count = number;
                    User user = (User)Session["user"];
                    DataSet dsStock = stockBll.SelectByBookNum(bookNum, user.ReginId.RegionId);
                    int rows = dsStock.Tables[0].Rows.Count;
                    for (int j = 0; j < rows; j++)
                    {
                        number = count;
                        int stockNum = Convert.ToInt32(dsStock.Tables[0].Rows[j]["stockNum"]);
                        int goodsId = Convert.ToInt32(dsStock.Tables[0].Rows[j]["goodsShelvesId"]);
                        if (stockNum > number)
                        {
                            Result stock = stockBll.update(stockNum - count, goodsId, bookNum);
                            if (stock == Result.更新失败)
                            {
                                Response.Write("更新失败:|");
                                Response.End();
                            }
                            else
                            {
                                Result end = retailBll.updateType(headId,user);
                                if(end == Result.更新失败)
                                {
                                    Response.Write("更新失败:|");
                                    Response.End();
                                }
                                break;
                            }
                        }
                        else
                        {
                            count = number - stockNum;
                            Result stock = stockBll.update(0, goodsId, bookNum);
                            if (stock == Result.更新失败)
                            {
                                Response.Write("更新失败:|");
                                Response.End();
                            }
                            if (count == 0)
                            {
                                Result end = retailBll.updateType(headId, user);
                                if (end == Result.更新失败)
                                {
                                    Response.Write("更新失败:|");
                                    Response.End();
                                }
                                break;
                            }
                        }
                    }
                }
                Pay(headId);
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
                int count = bookDs.Tables[0].Rows.Count;
                if (bookDs != null && bookDs.Tables[0].Rows.Count > 0)
                {
                    if (count == 1)
                    {
                        string headId = Request["headId"];
                        string bookNum = bookDs.Tables[0].Rows[0]["bookNum"].ToString();
                        add(bookNum, headId);
                    }
                    if (op == "choose")
                    {
                        int counts = bookDs.Tables[0].Rows.Count;
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < counts; i++)
                        {
                            DataRow dr = bookDs.Tables[0].Rows[i];
                            sb.Append("<tr><td><div class='pretty inline'><input type = 'radio' name='radio' value='" + dr["bookNum"].ToString() + "'><label><i class='mdi mdi-check'></i></label></div></td>");
                            sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                            sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                            sb.Append("<td>" + dr["price"].ToString() + "</td>");
                            sb.Append("<td>" + dr["supplier"].ToString() + "</td></tr>");
                        }
                        Response.Write(sb.ToString());
                        Response.End();
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
        public void add(string bookNum,string headId)
        {
            Result record = retailBll.selectByBookNum(bookNum, headId);
            if (record == Result.记录不存在)
            {
                BookBasicData bookBasicData = basicBll.SelectById(bookNum);
                string isbn = bookBasicData.Isbn;
                string bookName = bookBasicData.BookName;
                int billCount = Convert.ToInt32(Request["billCount"]);
                double discount = 100;
                if (bookBasicData.Remarks == "")
                {
                    discount = 100;
                }
                discount = discount * 0.01;
                int row = monTable.Rows.Count;
                double uPrice = bookBasicData.Price;
                SaleMonomer monomers = new SaleMonomer();
                double totalPrice = Convert.ToDouble((billCount * uPrice).ToString("0.00"));
                double realPrice = Convert.ToDouble((totalPrice * discount).ToString("0.00"));
                DataSet ds = retailBll.GetRetail(headId);
                int k = ds.Tables[0].Rows.Count-1;
                int monId = Convert.ToInt32(ds.Tables[0].Rows[k]["retailMonomerId"]);
                monomers.ISBN1 = isbn;
                monomers.UnitPrice = uPrice;
                monomers.BookNum = bookNum;
                monomers.SaleIdMonomerId = monId + 1;
                monomers.RealDiscount = discount * 100;
                monomers.Number = 1;
                monomers.TotalPrice = uPrice;
                monomers.RealPrice = uPrice * discount;
                monomers.SaleHeadId = headId;
                monomers.Datetime = DateTime.Now;
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
                        int counts = dsNew.Tables[0].Rows.Count;
                        for (int i = 0; i < counts; i++)
                        {
                            DataRow dr = dsNew.Tables[0].Rows[i];
                            sb.Append("<tr><td>" + dr["retailMonomerId"].ToString() + "</td>");
                            sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                            sb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                            sb.Append("<td>" + dr["unitPrice"].ToString() + "</td>");
                            sb.Append("<td style='display:none'>" + dr["number"].ToString() + "</td>");
                            sb.Append("<td><input class='numberEnd' type='number' style='width:50px;border:none;' name='points',min='1' value='" + dr["number"].ToString() + "'/></td>");
                            sb.Append("<td>" + dr["realDiscount"].ToString() + "</td>");
                            sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                            sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                            sb.Append("<td style='display:none'>" + dr["bookNum"].ToString() + "</td>");
                            sb.Append("<td><button class='btn btn-danger btn-sm delete'><i class='fa fa-trash'></i></button></td></tr>");
                        }
                        Response.Write(sb.ToString() + "|:" + newSale.KindsNum + "," + newSale.Number + "," + newSale.AllTotalPrice + "," + newSale.AllRealPrice);
                        Response.End();
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
                        sb.Append("<td><input class='numberEnd' type='number' style='width:50px;border:none;' name='points',min='1' value='" + dr["number"].ToString() + "'/></td>");
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
        /// <summary>
        /// 收银修改数量
        /// </summary>
        public void change()
        {
            int number = Convert.ToInt32(Request["number"]);
            int retailId = Convert.ToInt32(Request["retailId"]);
            string headId = Request["headId"];
            SaleMonomer monomer = retailBll.GetMonomer(retailId, headId);
            int oldNumber = monomer.Number;
            double oldTotal = monomer.TotalPrice;
            double oldReal = monomer.RealPrice;
            double price = monomer.UnitPrice;
            double realDiscount = monomer.RealDiscount;
            double total = number * price;
            double real = total * realDiscount * 0.01;
            SaleMonomer sale = new SaleMonomer();
            sale.SaleIdMonomerId = retailId;
            sale.Number = number;
            sale.TotalPrice = total;
            sale.RealPrice = real;
            Result change = retailBll.UpdateNumber(sale);
            if (change == Result.更新成功)
            {
                SaleHead head = retailBll.GetHead(headId);
                SaleHead newHead = new SaleHead();
                int newNumber = head.Number - oldNumber + number;
                double newTotal = head.AllTotalPrice - oldTotal + total;
                double newReal = head.AllRealPrice - oldReal + real;
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
                Response.Write("更新成功");
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

        public string Pay(string headId)
        {
            DataSet dsPay = retailBll.GetRetail(headId);
            StringBuilder sb = new StringBuilder();
            DataSet dsNew = retailBll.GetRetail(headId);
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

            Response.Write("更新成功:|"+sb.ToString());
            Response.End();
            return sb.ToString();
        }
    }
}