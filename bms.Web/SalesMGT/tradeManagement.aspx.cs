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
    using Result = Enums.OpResult;
    public partial class tradeManagement : System.Web.UI.Page
    {
        public string userName, regionName;
        public DataSet ds, customerds, dsPer;
        public int totalCount, intPageCount, pageSize = 20;
        SaleTaskBll saleBll = new SaleTaskBll();
        CustomerBll custBll = new CustomerBll();
        string SaleHeadId, saleTaskId;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail;
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
            getData();
            string op = Request["op"];
            //添加销售任务
            if (op == "add")
            {
                int count = saleBll.getCount();
                if (count > 0)
                {
                    string time = saleBll.getSaleTaskTime();
                    string nowTime = DateTime.Now.ToLocalTime().ToString();
                    string equalsTime = nowTime.Substring(0, 10);
                    if (time.Equals(equalsTime))
                    {
                        count += 1;
                        saleTaskId = "XSRW" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                    }
                    else
                    {
                        count = 1;
                        saleTaskId = "XSRW" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                    }

                }
                else
                {
                    count = 1;
                    saleTaskId = "XSRW" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
                }
                int custmerID = Convert.ToInt32(Request["Custmer"]);
                Customer customer = new Customer();
                customer.CustomerId = custmerID;
                int numberLimit = Convert.ToInt32(Request["numberLimit"]);
                int priceLimit = Convert.ToInt32(Request["priceLimit"]);
                int totalPriceLimit = Convert.ToInt32(Request["totalPriceLimit"]);
                double defaultDiscount = double.Parse(Request["defaultDiscount"]);
                User user = (User)Session["user"];
                int userId = user.UserId;
                DateTime StartTime = DateTime.Now.ToLocalTime();
                SaleTask saleTask = new SaleTask()
                {
                    SaleTaskId = saleTaskId,
                    UserId = userId,
                    Customer = customer,
                    DefaultDiscount = defaultDiscount,
                    DefaultCopy = "",
                    NumberLimit = numberLimit,
                    PriceLimit = priceLimit,
                    TotalPiceLimit = totalPriceLimit,
                    StartTime = StartTime,
                };
                Result result = saleBll.insert(saleTask);
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
                string saleID = Request["ID"];
                Result isDelete = saleBll.IsDelete("T_SellOffHead", "saleTaskId", saleID);
                if (isDelete == Result.记录不存在)
                {
                    isDelete = saleBll.IsDelete("T_ReplenishmentHead", "saleTaskId", saleID);
                    if (isDelete == Result.记录不存在)
                    {
                        isDelete = saleBll.IsDelete("T_SaleHead", "saleTaskId", saleID);
                    }
                }
                if (isDelete == Result.关联引用)
                {
                    Response.Write("该客户已被关联到其他表，不能删除！");
                }
                else
                {
                    Result result = saleBll.Delete(saleID);
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
            }
            //查看
            if (op == "look")
            {
                string saleId = Request["ID"];
                Session["saleId"] = saleId;
                Session["type"] = "look";
                Response.Write("成功");
                Response.End();
            }
            //销售
            if (op == "sale")
            {
                string saleId = Request["ID"];
                Session["saleId"] = saleId;
                Session["type"] = "add";
                string finishState = saleBll.getSaleTaskFinishTime(saleId);
                if (finishState == null || finishState == "")
                {
                    Response.Write("可以");
                    Response.End();
                }
                else
                {
                    Response.Write("不可以");
                    Response.End();
                }

            }
            //添加销退
            if (op == "saleback")
            {
                string saleTaskId = Request["ID"];
                Session["saleId"] = saleTaskId;
                string finishState = saleBll.getSaleTaskFinishTime(saleTaskId);
                if (finishState == null || finishState == "")
                {
                    Response.Write("不可以");
                    Response.End();
                }
                else
                {
                    Response.Write("可以");
                    Response.End();
                }
            }
            if (op == "isEdit")
            {
                string saleID = Request["saleId"];
                string state = saleBll.getSaleTaskFinishTime(saleID);
                if (state == "" || state == null)
                {
                    Response.Write("可以编辑");
                    Response.End();
                }
                else
                {

                    Response.Write("不可以编辑");
                    Response.End();
                }
            }
            //编辑
            if (op == "edit")
            {
                string saleId = Request["saleId"];
                double allprice = double.Parse(Request["allpricemlimited"]);
                int number = int.Parse(Request["numberlimited"]);
                double price = double.Parse(Request["pricelimited"]);
                double defaultDiscount = double.Parse(Request["defaultDiscount"]);
                int row = saleBll.update(number, price, allprice, defaultDiscount, saleId);
                if (row > 0)
                {
                    Response.Write("保存成功");
                    Response.End();
                }
                else
                {
                    Response.Write("保存失败");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 获取基础数据及查询方法
        /// </summary>
        public string getData()
        {
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string search = Request["search"];
            if (search == "" || search == null)
            {
                search = "deleteState=0";
            }
            else
            {
                search = String.Format(" saleTaskId {0} and deleteState=0", "like '%" + search + "%'");
            }

            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_SaleTask";
            tb.OrderBy = "saleTaskId";
            tb.StrColumnlist = "saleTaskId,defaultDiscount,priceLimit,numberLimit,totalPriceLimit,startTime,finishTime,userId,userName,customerName";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = search;
            //获取展示的客户数据
            ds = saleBll.selectBypage(tb, out totalCount, out intPageCount);
            //获取客户下拉数据
            customerds = custBll.select();
            //生成table
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string time = ds.Tables[0].Rows[i]["finishTime"].ToString();
                if (time == "" || time == null)
                {
                    time = "销售任务采集中";
                }
                strb.Append("<tr><td>" + ds.Tables[0].Rows[i]["saleTaskId"].ToString() + "</td>");
                strb.Append("<td><nobr>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</nobr></td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["userName"].ToString() + "</td>");
                strb.Append("<td>" + Double.Parse(ds.Tables[0].Rows[i]["defaultDiscount"].ToString()) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["numberLimit"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["priceLimit"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["totalPriceLimit"].ToString() + "</td>");
                strb.Append("<td><nobr>" + ds.Tables[0].Rows[i]["startTime"].ToString() + "</nobr></td>");
                strb.Append("<td><nobr>" + time + "</nobr></td>");
                strb.Append("<td style='width:100px;'>" + "<button class='btn btn-success btn-sm btn_sale'>销售</button>");
                strb.Append("<button class='btn btn-success btn-sm btn_back'>销退</button></td>");
                strb.Append("<td style='width:150px;'><button class='btn btn-success btn-sm btn_search'>&nbsp 查看 &nbsp</button> <button class='btn btn-sm btn-success edited'>&nbsp 编辑 &nbsp</button>");
                strb.Append("<button class='btn btn-danger btn-sm btn_del'><i class='fa fa-trash'></i></button>" + "</td></tr>");
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
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