using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.SalesMGT
{
    using System.Web.Security;
    using Result = Enums.OpResult;

    public partial class salesManagement : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20, SettlementAllCount;
        DataSet ds, dsPer;
        SaleHeadBll saleheadbll = new SaleHeadBll();
        BookBasicBll bookbll = new BookBasicBll();
        StockBll stockbll = new StockBll();
        User user = new User();
        replenishMentMonomer respMon = new replenishMentMonomer();
        replenishMentBll replenBll = new replenishMentBll();
        replenishMentHead rsHead = new replenishMentHead();
        SaleMonomerBll salemonbll = new SaleMonomerBll();
        public string type, userName, regionName, saleTaskid, finishTime = "";
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (User)Session["user"];
            permission();

            string op = Request["op"];
            saleTaskid = Session["saleId"].ToString();
            SaleTaskBll saleBll = new SaleTaskBll();
            finishTime = saleBll.getSaleTaskFinishTime(saleTaskid);
            getData();
            string saleheadId = Request["ID"];
            type = Session["type"].ToString();
            //添加销售单体
            if (op == "addDetail")
            {
                if (finishTime != null && finishTime != "")
                {
                    Response.Write("该销售计划已完成");
                    Response.End();
                }
                else
                {
                    SaleMonomerBll salemonbll = new SaleMonomerBll();
                    int state = salemonbll.saleheadstate(saleTaskid, saleheadId);
                    if (state == 2)
                    {
                        Response.Write("失败");
                        Response.End();
                    }
                    else
                    {
                        Session["saleheadId"] = saleheadId;
                        Session["saleType"] = "addsale";
                        Response.Write("成功");
                        Response.End();
                    }
                }
            }
            //if (op == "saleback")
            //{
            //    saleback();
            //}
            if (op == "Settlement")
            {
                string salehead = Request["ID"];
                string taskId = Request["taskId"];
                Settlement(taskId, salehead, 0);
            }
            if (op == "SettlementAll")
            {
                SettlementAll();
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
            //查看
            if (op == "look")
            {
                Session["saleheadId"] = saleheadId;
                Session["saleType"] = "look";
                Response.Write("成功");
                Response.End();
            }
            //完成此销售任务
            if (op == "finish")
            {
                //判断该销售任务下是否还有未完成单据0新建单据 1未完成，2完成，3未结算
                SaleTaskBll salebll = new SaleTaskBll();
                DataSet saleHeadStateds = salebll.SelectHeadStateBySaleId(saleTaskid);
                int count = saleHeadStateds.Tables[0].Rows.Count;
                int state = 4;
                for (int i = 0; i < count; i++)
                {
                    state = Convert.ToInt32(saleHeadStateds.Tables[0].Rows[i]["state"]);
                    if (state == 0 || state == 1)
                    {
                        break;
                    }
                }
                if (state == 0)
                {
                    Response.Write("失败,有新建的单据");
                    Response.End();
                }
                if (state == 1)
                {
                    Response.Write("失败,有采集中的单据");
                    Response.End();
                }
                else
                {
                    DateTime finishTime = DateTime.Now.ToLocalTime();
                    int row = salebll.updatefinishTime(finishTime, saleTaskid);
                    if (row > 0)
                    {
                        Response.Write("成功");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("失败");
                        Response.End();
                    }
                }
                //if (state < 2)
                //{
                //    Response.Write("未完成失败");
                //    Response.End();
                //}
                //else if (state == 2)
                //{
                //    DateTime finishTime = DateTime.Now.ToLocalTime();
                //    int row = salebll.updatefinishTime(finishTime, saleTaskid);
                //    if (row > 0)
                //    {
                //        Response.Write("成功");
                //        Response.End();
                //    }
                //    else
                //    {
                //        Response.Write("失败");
                //        Response.End();
                //    }
                //}
            }
            //添加
            if (op == "add")
            {
                //获取销售任务的状态
                SaleHeadBll saleheadbll = new SaleHeadBll();
                SaleHead salehead = new SaleHead();
                string saleId = Session["saleId"].ToString();
                string SaleHeadId;
                int count = saleheadbll.getCount();
                if (count > 0)
                {
                    string time = saleheadbll.getSaleHeadTime();
                    string nowTime = DateTime.Now.ToLocalTime().ToString();
                    string equalsTime = nowTime.Substring(0, 10);
                    if (time.Equals(equalsTime))
                    {
                        nowTime = DateTime.Now.ToString("yyyy-MM-dd");
                        string getheadId = saleheadbll.getSaleHeadIdByTime(nowTime);
                        if (getheadId == "" || getheadId == null)
                        {
                            count = 1;
                            SaleHeadId = "XS" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                        }
                        else
                        {
                            string js = getheadId.Remove(0, getheadId.Length - 6);
                            count = Convert.ToInt32(js) + 1;
                            SaleHeadId = "XS" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                        }
                    }
                    else
                    {
                        count = 1;
                        SaleHeadId = "XS" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                    }
                }
                else
                {
                    count = 1;
                    SaleHeadId = "XS" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                }
                salehead.SaleHeadId = SaleHeadId;
                salehead.SaleTaskId = saleId;
                salehead.KindsNum = 0;
                salehead.Number = 0;
                salehead.AllTotalPrice = 0;
                salehead.AllRealPrice = 0;
                User user = (User)Session["user"];
                salehead.UserId = user.UserId;
                salehead.RegionId = user.ReginId.RegionId;
                salehead.DateTime = DateTime.Now.ToLocalTime();
                salehead.State = 0;
                Result result = saleheadbll.Insert(salehead);
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
            //删除
            if (op == "del")
            {
                string salehead = Request["ID"];
                SaleMonomerBll salemonbll = new SaleMonomerBll();
                int state = salemonbll.saleheadstate(saleTaskid, salehead);
                if (state == 0)
                {
                    Result result = salemonbll.realDelete(saleTaskid, salehead);
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
                else if (state == 1)
                {
                    Response.Write("单据采集中");
                    Response.End();
                }
                else if (state == 2)
                {
                    Response.Write("单据完成");
                    Response.End();
                }
                else if (state == 3)
                {
                    Result result = salemonbll.realDeleteHeadAndMon(saleTaskid, salehead);
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
                    Response.Write("删除失败");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 单据销退
        /// </summary>
        //public void saleback()
        //{
        //    string saleHeadId = Request["ID"];
        //    string saleTaskId = Request["taskId"];
        //    string saleHeadstate = salemonbll.getsaleHeadState(saleHeadId, saleTaskId);
        //    if (saleHeadstate == "3")
        //    {
        //        Result res = salemonbll.updateHeadstate(saleTaskId, saleHeadId, 2);
        //        if(res==Result.更新成功)
        //        {


        //        }
        //    }
        //    else
        //    {

        //    }
        //}

        /// <summary>
        /// 结算整个销售任务
        /// </summary>
        public void SettlementAll()
        {
            DataTable saleHeadDt = saleheadbll.getSaleHeadIdbyStaskId(saleTaskid);
            SettlementAllCount = saleHeadDt.Rows.Count;
            for (int k = 0; k < SettlementAllCount; k++)
            {
                string saleHeadId = saleHeadDt.Rows[k]["saleHeadId"].ToString();
                salemonbll.updateHeadstate(saleTaskid, saleHeadId, 2);
                if (k == SettlementAllCount - 1)
                {
                    Settlement(saleTaskid, saleHeadId, 0);
                }
                else
                {
                    Settlement(saleTaskid, saleHeadId, SettlementAllCount);
                }
            }

        }

        /// <summary>
        /// 单个销售单结算
        /// </summary>
        public void Settlement(string saleTaskid, string saleHead, int scaler)
        {
            int RegionId = user.ReginId.RegionId;
            DataTable dt = saleheadbll.getSaleAllbyHeadIdAndStaskId(saleTaskid, saleHead);
            int number = 0;
            string bookNum, bookName, saleHeadId, saleTaskId, Isbn, Author, Supplier;
            int saleIdMonomerId, rsMonomerId;
            int allstockNum = 0;//总库存
            int bhnum = 0;
            BookBasicData book = new BookBasicData();
            saleHeadId = dt.Rows[0]["saleHeadId"].ToString();
            saleTaskId = dt.Rows[0]["saleTaskId"].ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bookNum = dt.Rows[i]["bookNum"].ToString();
                DataSet stockbook = stockbll.SelectByBookNum(bookNum, RegionId);
                allstockNum = 0;
                for (int h = 0; h < stockbook.Tables[0].Rows.Count; h++)
                {
                    allstockNum += Convert.ToInt32(stockbook.Tables[0].Rows[h]["stockNum"]);
                }
                number = int.Parse(dt.Rows[i]["number"].ToString());
                //库存小于数量时生成补货单
                if (allstockNum < number)
                {
                    book = bookbll.SelectById(bookNum);
                    Isbn = book.Isbn;
                    Supplier = book.Publisher;
                    Author = book.Author;
                    bookName = dt.Rows[i]["bookName"].ToString();
                    saleIdMonomerId = int.Parse(dt.Rows[i]["saleIdMonomerId"].ToString());
                    bhnum = number - allstockNum;

                    string rsHead = replenBll.getRsHeadID(saleTaskId);
                    //判断该销售任务是否已有补货记录，如果没有则新增
                    if (rsHead == "none")
                    {
                        Result res = addrsHead(saleTaskId);
                        if (res == Result.添加成功)
                        {
                            int count = replenBll.countMon(saleTaskId);
                            if (count > 0)
                            {
                                rsMonomerId = count + 1;
                            }
                            else
                            {
                                rsMonomerId = 1;
                            }
                            respMon.BookNum = bookNum;
                            respMon.Count = bhnum;
                            respMon.Supplier = Supplier;
                            respMon.DateTime = DateTime.Now.ToLocalTime();
                            respMon.Isbn = Isbn;
                            respMon.SaleIdMonomerId = saleIdMonomerId;
                            respMon.SaleTaskId = saleTaskId;
                            respMon.Author = Author;
                            respMon.SaleHeadId = saleHeadId;
                            respMon.RsMonomerID = rsMonomerId;
                            Result addmonRes = replenBll.Insert(respMon);
                            if (addmonRes == Result.添加成功)
                            {
                                //更新补货单头
                                int rskinds = replenBll.getkinds(saleTaskId);
                                int rsnumber = replenBll.getsBookNumberSum(saleTaskId);
                                replenishMentHead upRsHead = new replenishMentHead()
                                {
                                    SaleTaskId = saleTaskId,
                                    KindsNum = rskinds,
                                    Number = rsnumber,
                                };
                                replenBll.updateRsHead(upRsHead);
                                for (int t = 0; t < stockbook.Tables[0].Rows.Count; t++)
                                {
                                    int goodsID = int.Parse(stockbook.Tables[0].Rows[t]["goodsShelvesId"].ToString());
                                    stockbll.update(0, goodsID, bookNum);
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
                            Response.Write("添加单头失败");
                            Response.End();
                        }

                    }
                    else
                    {
                        int count = replenBll.countMon(saleTaskId);
                        if (count > 0)
                        {
                            rsMonomerId = count + 1;
                        }
                        else
                        {
                            rsMonomerId = 1;
                        }
                        respMon.BookNum = bookNum;
                        respMon.Count = bhnum;
                        respMon.Supplier = Supplier;
                        respMon.DateTime = DateTime.Now.ToLocalTime();
                        respMon.Isbn = Isbn;
                        respMon.SaleIdMonomerId = saleIdMonomerId;
                        respMon.SaleTaskId = saleTaskId;
                        respMon.Author = Author;
                        respMon.SaleHeadId = saleHeadId;
                        respMon.RsMonomerID = rsMonomerId;
                        Result addmonRes = replenBll.Insert(respMon);
                        if (addmonRes == Result.添加成功)
                        {
                            //更新补货单头
                            int rskinds = replenBll.getkinds(saleTaskId);
                            int rsnumber = replenBll.getsBookNumberSum(saleTaskId);
                            replenishMentHead upRsHead = new replenishMentHead()
                            {
                                SaleTaskId = saleTaskId,
                                KindsNum = rskinds,
                                Number = rsnumber,
                            };
                            replenBll.updateRsHead(upRsHead);
                            for (int t = 0; t < stockbook.Tables[0].Rows.Count; t++)
                            {
                                int goodsID = int.Parse(stockbook.Tables[0].Rows[t]["goodsShelvesId"].ToString());
                                stockbll.update(0, goodsID, bookNum);
                            }
                        }
                        else
                        {
                            Response.Write("添加补货单失败");
                            Response.End();
                        }
                    }
                }
                //库存足则直接减库存
                else
                {
                    for (int j = 0; j < stockbook.Tables[0].Rows.Count; j++)
                    {
                        int stockNum = Convert.ToInt32(stockbook.Tables[0].Rows[j]["stockNum"]);
                        int goodsId = Convert.ToInt32(stockbook.Tables[0].Rows[j]["goodsShelvesId"]);
                        if (number <= stockNum)
                        {
                            int stockcount = stockNum - number;
                            stockbll.update(stockcount, goodsId, bookNum);
                            number = 0;
                        }
                        else
                        {
                            number = number - stockNum;
                            stockbll.update(0, goodsId, bookNum);
                        }
                    }
                }
            }
            if (scaler == 0)
            {
                //循环完后更新销售单的状态
                Result upHeadstate = salemonbll.updateHeadstate(saleTaskId, saleHeadId, 2);
                if (upHeadstate == Result.更新成功)
                {
                    Response.Write("添加成功");
                    Response.End();
                }
                else
                {
                    Response.Write("更新销售单头失败");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 添加销售单头
        /// </summary>
        /// <param name="saleId">销售任务id</param>
        /// <returns></returns>
        public Result addrsHead(string saleId)
        {
            replenishMentHead rsHead = new replenishMentHead();
            replenishMentBll replenBll = new replenishMentBll();
            rsHead.KindsNum = 0;
            rsHead.Number = 0;
            rsHead.SaleTaskId = saleId;
            rsHead.UserId = user.UserId;
            rsHead.Time = DateTime.Now.ToLocalTime();
            return replenBll.InsertRsHead(rsHead);
        }
        public string getData()
        {
            string saleId = Session["saleId"].ToString();
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string saleHeadId = Request["saleTaskId"];
            string regionName = Request["regionName"];
            string userName = Request["userName"];
            string search = "";
            if ((saleHeadId == "" || saleHeadId == null) && (regionName == null || regionName == "") && (userName == null || userName == ""))
            {
                search = "";
            }
            else if ((saleHeadId != "" && saleHeadId != null) && (regionName == null || regionName == "") && (userName == null || userName == ""))
            {
                search = String.Format(" saleHeadId like '%{0}%'", saleHeadId);
            }
            else if ((saleHeadId == "" || saleHeadId == null) && (regionName != "" && regionName != null) && (userName == null || userName == ""))
            {
                search = "regionName='" + regionName + "'";
            }
            else if ((saleHeadId == "" || saleHeadId == null) && (userName != "" && userName != null) && (regionName == null || regionName == ""))
            {
                search = "userName='" + userName + "'";
            }
            else if ((saleHeadId == "" || saleHeadId == null) && (userName != "" && userName != null) && (regionName != null && regionName != ""))
            {
                search = "regionName='" + regionName + "' and userName='" + userName + "'";
            }
            else if ((saleHeadId != "" && saleHeadId != null) && (regionName != null && regionName != "") && (userName == null || userName == ""))
            {
                search = String.Format(" saleHeadId like '%{0}%' and regionName = '{1}'", saleHeadId, regionName);
            }
            else if ((saleHeadId != "" && saleHeadId != null) && (regionName == null || regionName == "") && (userName != null && userName != ""))
            {
                search = String.Format(" saleHeadId like '%{0}%' and userName='{1}'", saleHeadId, userName);
            }
            else
            {
                search = String.Format(" saleHeadId like '%{0}%' and regionName = '{1}' and userName='{2}'", saleHeadId, regionName, userName);
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SaleHead";
            tb.OrderBy = "saleHeadId";
            tb.StrColumnlist = "saleHeadId,saleTaskId,kindsNum,number,allTotalPrice,allRealPrice,userName,regionName,dateTime,state";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = search == "" ? "deleteState=0 and saleTaskId=" + "'" + saleId + "'" : search + " and deleteState = 0 and saleTaskId=" + "'" + saleId + "'";
            //获取展示的客户数据
            ds = saleheadbll.selectBypage(tb, out totalCount, out intPageCount);
            //获取客户下拉数据
            //customerds = custBll.select();
            //生成table
            StringBuilder strb = new StringBuilder();
            int dscount = ds.Tables[0].Rows.Count;
            for (int i = 0; i < dscount; i++)
            {
                string state = ds.Tables[0].Rows[i]["state"].ToString();
                if (state == "0")
                {
                    state = "新建单据";
                }
                else if (state == "1")
                {
                    state = "采集中";
                }
                else if (state == "2")
                {
                    state = "单据已完成";
                }
                else
                {
                    state = "预采";
                }
                //else if (state == "3")
                //{
                //    state = "未结算";
                //}
                strb.Append("<tr><td>" + ds.Tables[0].Rows[i]["saleHeadId"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["saleTaskId"].ToString() + "</td>");
                strb.Append("<td>" + state + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["userName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["kindsNum"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allTotalPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allRealPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["dateTime"].ToString() + "</td>");
                strb.Append("<td>");
                //if (state == "未结算")
                //{
                //    strb.Append("<button class='btn btn-success btn-sm btn_succ'>结算</button>");
                //    //strb.Append("<button class='btn btn-success btn-sm btn_back'>销退</button>");
                //}
                if ((state == "新建单据" || state == "采集中") && (finishTime == null || finishTime == ""))
                {
                    strb.Append("<button class='btn btn-success btn-sm add'><i class='fa fa-plus fa-lg'></i></button>");
                }
                strb.Append("<button class='btn btn-info btn-sm look'><i class='fa fa-search'></i></button>");
                strb.Append("<button class='btn btn-danger btn-sm btn_del'><i class='fa fa-trash'></i></button>" + "</td></tr>");
            }
            strb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
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
            }
        }

    }
}