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

    public partial class salesManagement : System.Web.UI.Page
    {
        public int totalCount, intPageCount, pageSize = 20;
        DataSet ds, dsPer;
        SaleHeadBll saleheadbll = new SaleHeadBll();
        public string type, userName, regionName;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail;
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
            getData();
            string op = Request["op"];
            string saleTaskid = Session["saleId"].ToString(); ;
            string saleheadId = Request["ID"];
            type = Session["type"].ToString();
            //添加销售单体
            if (op == "addDetail")
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
                //判断该销售任务下是否还有未完成单据 0 1未完成，2完成
                SaleTaskBll salebll = new SaleTaskBll();
                DataSet saleHeadStateds = salebll.SelectHeadStateBySaleId(saleTaskid);
                int count = saleHeadStateds.Tables[0].Rows.Count;
                int state = 3;
                for (int i = 0; i < count; i++)
                {
                    state = Convert.ToInt32(saleHeadStateds.Tables[0].Rows[i]["state"]);
                    if (state < 2)
                    {
                        break;
                    }
                }
                if (state < 2)
                {
                    Response.Write("未完成失败");
                    Response.End();
                }
                else if (state == 2)
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
            }
            //添加
            if (op == "add")
            {
                //获取销售任务的状态
                SaleHeadBll saleheadbll = new SaleHeadBll();
                SaleHead salehead = new SaleHead();
                string saleId = Session["saleId"].ToString();
                string SaleHeadId;
                int count = saleheadbll.getCount(saleId);
                if (count > 0)
                {
                    string time = saleheadbll.getSaleHeadTime();
                    string nowTime = DateTime.Now.ToLocalTime().ToString();
                    string equalsTime = nowTime.Substring(0, 10);
                    if (time.Equals(equalsTime))
                    {
                        count += 1;
                        SaleHeadId = "XS" + DateTime.Now.ToString("yyyyMMdd") + count.ToString().PadLeft(6, '0');
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
                else
                {
                    Response.Write("删除失败");
                    Response.End();
                }
            }
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
            string saleTaskId = Request["saleTaskId"];
            string regionName = Request["regionName"];
            string userName = Request["userName"];
            string search = "";
            if ((saleTaskId == "" || saleTaskId == null) && (regionName == null || regionName == "") && (userName == null || userName == ""))
            {
                search = "";
            }
            else if ((saleTaskId != "" && saleTaskId != null) && (regionName == null || regionName == "") && (userName == null || userName == ""))
            {
                search = String.Format(" saleTaskId like '%{0}%'", saleTaskId);
            }
            else if ((saleTaskId == "" || saleTaskId == null) && (regionName != "" && regionName != null) && (userName == null || userName == ""))
            {
                search = "regionName=" + regionName;
            }
            else if ((saleTaskId == "" || saleTaskId == null) && (userName != "" && userName != null) && (regionName == null || regionName == ""))
            {
                search = "userName='" + userName + "'";
            }
            else if ((saleTaskId == "" || saleTaskId == null) && (userName != "" && userName != null) && (regionName != null && regionName != ""))
            {
                search = "regionName='" + regionName + "' and userName='" + userName + "'";
            }
            else if ((saleTaskId != "" && saleTaskId != null) && (regionName != null && regionName != "") && (userName == null || userName == ""))
            {
                search = String.Format(" saleTaskId like '%{0}%' and regionName = '{1}'", saleTaskId, regionName);
            }
            else if ((saleTaskId != "" && saleTaskId != null) && (regionName == null || regionName == "") && (userName != null && userName != ""))
            {
                search = String.Format(" saleTaskId like '%{0}%' and userName='{1}'", saleTaskId, userName);
            }
            else
            {
                search = String.Format(" saleTaskId like '%{0}%' and regionName = '{1}' and userName='{2}'", saleTaskId, regionName, userName);
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
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
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
                strb.Append("<td>" + ds.Tables[0].Rows[i]["saleHeadId"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["saleTaskId"].ToString() + "</td>");
                strb.Append("<td>" + state + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["userName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["kindsNum"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allTotalPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allRealPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["dateTime"].ToString() + "</td>");
                strb.Append("<td>" + "<button class='btn btn-success btn-sm add'><i class='fa fa-plus fa-lg'></i></button>");
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