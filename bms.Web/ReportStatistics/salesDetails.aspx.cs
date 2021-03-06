﻿using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.ReportStatistics
{
    public partial class salesDetails : System.Web.UI.Page
    {
        DataSet ds, dsPer;
        SaleMonomerBll salemonBll = new SaleMonomerBll();
        SalesDetailsBll detailsBll = new SalesDetailsBll();
        SaleTaskBll saletaskbll = new SaleTaskBll();
        public int totalCount, intPageCount, pageSize = 20;
        public DataSet dsUser = null;
        //统计字段
        public string saletaskId, stauserName, customerName, startTime, finishTime;

        public string stasupplier, staregionName, stacustomerName;
        public int allkinds, allnumber;
        public double alltotalprice, allreadprice;


        public string type = "", name = "",regionId="", groupType = "", userName, regionName;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail, isAdmin, funcBookStock;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                permission();
                type = Request.QueryString["type"];
                name = Request.QueryString["name"];
                regionId = Request.QueryString["regionId"];
                if ((type == null || type == "") || (name == null) || (regionId==null|| regionId==""))
                {
                    type = Session["type"].ToString();
                    name = Session["name"].ToString();
                    regionId = Session["regionId"].ToString();
                }
                else
                {
                    Session["type"] = type;
                    Session["name"] = name;
                    Session["regionId"] = regionId;
                }
            }
            if (type == "supplier")
            {
                stasupplier = name;
            }
            else if (type == "regionName")
            {
                staregionName = name;
            }
            else if (type == "customerName")
            {
                stacustomerName = name;
            }


            getData();
            getBasic();
            string exportOp = Request.QueryString["op"];
            if (exportOp == "export")
            {
                export();
            }
            string op = Request["op"];
            if (op == "print")
            {
                Response.Write(Print());
                Response.End();
            }
        }
        /// <summary>
        /// 获取统计信息
        /// </summary>
        public void getBasic()
        {
            string strwhere = Session["strWhere"].ToString();

            saletaskId = saletaskbll.getSaleTaskid(strwhere);

            DataSet ds = saletaskbll.getSaleTaskStatistics(saletaskId);
            if (ds != null)
            {
                string number = ds.Tables[0].Rows[0]["number"].ToString();
                if (number == "" || number == null)
                {
                    allnumber = 0;
                    alltotalprice = 0;
                    allreadprice = 0;
                }
                else
                {
                    allnumber = int.Parse(ds.Tables[0].Rows[0]["number"].ToString());
                    alltotalprice = double.Parse(ds.Tables[0].Rows[0]["totalPrice"].ToString());
                    allreadprice = double.Parse(ds.Tables[0].Rows[0]["realPrice"].ToString());
                }
            }
            //统计种数
            allkinds = saletaskbll.getkindsBySaleTaskId(saletaskId);
            DataSet userds = saletaskbll.getcustomerName(saletaskId);
            if (userds != null)
            {
                //stasupplier, staregionName, ;
                userName = userds.Tables[0].Rows[0]["userName"].ToString();
                stacustomerName = userds.Tables[0].Rows[0]["customerName"].ToString();
                //startTime = userds.Tables[0].Rows[0]["startTime"].ToString();
                startTime = Convert.ToDateTime(userds.Tables[0].Rows[0]["startTime"].ToString()).ToString("yyyy年MM月dd日");
                finishTime = userds.Tables[0].Rows[0]["finishTime"].ToString();
                regionName = userds.Tables[0].Rows[0]["regionName"].ToString();
                if (finishTime == "" || finishTime == null)
                {
                    finishTime = "此销售任务还未结束";
                }
                else
                {
                    finishTime = Convert.ToDateTime(finishTime).ToString("yyyy年MM月dd日");
                }
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            string strWhere = "";
            if (type == "supplier")
            {
                if (regionId != null || regionId != "")
                {
                    strWhere = "supplier = '" + name + "' and deleteState=0 and regionId="+ regionId + " ";
                }
                else {
                    strWhere = "supplier = '" + name + "' and deleteState=0";
                }
               
            }
            else if (type == "regionName")
            {
                strWhere = "regionName = '" + name + "' and deleteState=0";
            }
            else if (type == "customerName")
            {

                if (regionId != null || regionId != "")
                {
                    strWhere = "customerName = '" + name + "' and deleteState=0 and regionId=" + regionId + " ";
                }
                else
                {
                    strWhere = "customerName = '" + name + "' and deleteState=0";
                }
            }
            groupType = strWhere;
            dsUser = detailsBll.getUser(groupType);
            string isbn = Request["isbn"];
            string price = Request["price"];
            string discount = Request["discount"];
            string user = Request["user"];
            string time = Request["time"];
            string looktime = Request["looktime"];
            string state = Request["state"];
            if (isbn != null && isbn != "")
            {
                strWhere += " and isbn like '%" + isbn + "%'";
            }
            if (price != "" && price != null)
            {
                string[] sArray = price.Split('于');
                string type1 = sArray[0];
                string number = sArray[1];
                if (strWhere == "" || strWhere == null)
                {
                    if (type1 == "小")
                    {
                        strWhere = "price < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere = "price = '" + number + "'";
                    }
                    else
                    {
                        strWhere = "price > '" + number + "'";
                    }
                }
                else
                {
                    if (type1 == "小")
                    {
                        strWhere += " and price < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere += " and price = '" + number + "'";
                    }
                    else
                    {
                        strWhere += " and price > '" + number + "'";
                    }
                }
            }
            //if (price != null && price != "")
            //{
            //    strWhere += " and price='" + price + "'";
            //}
            if (discount != null && discount != "")
            {
                strWhere += " and realDiscount='" + discount + "'";
            }
            if (user != null && user != "" && user != "0")
            {
                strWhere += " and userName='" + user + "'";
            }
            if (time != null && time != "")
            {
                string[] sArray = time.Split('至');
                string startTime = sArray[0];
                string endTime = sArray[1];
                strWhere += " and dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
            }
            if (time == "" || time == null)
            {
                if (looktime != null && looktime != "" && looktime != "null")
                {
                    string[] sArray = looktime.Split('至');
                    string startTime = sArray[0];
                    string endTime = sArray[1];
                    strWhere += " and dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
                }
            }

            if (state != null && state != "" && state != "-1")
            {
                if (state == "1")
                {
                    strWhere += " and (state='1' or state='2')";
                }
                else
                {
                    strWhere += " and state='" + state + "'";
                }
            }
            strWhere += " group by bookNum,userName,supplier";
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "v_allsalemonomer";

            // tb.OrderBy = "id";
            tb.OrderBy = "dateTime desc";
            tb.StrColumnlist = "id,isbn,bookNum,bookName,price,sum(number) as number, sum(totalPrice) as totalPrice,sum(realPrice) as realPrice,realDiscount,dateTime,userName,state,supplier";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = strWhere + " having number!=0";
            Session["strWhere"] = strWhere;
            //获取展示的客户数据
            ds = salemonBll.selectBypage(tb, out totalCount, out intPageCount);
            StringBuilder strb = new StringBuilder();
            int dscount = ds.Tables[0].Rows.Count;
            string states = "", stateName = "";
            for (int i = 0; i < dscount; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                //序号 (i + 1 + ((currentPage - 1) * pageSize)) 
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + dr["isbn"].ToString() + "</td>");
                strb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
                strb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                strb.Append("<td>" + dr["price"].ToString() + "</td>");
                strb.Append("<td>" + dr["number"].ToString() + "</td>");
                strb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                strb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                strb.Append("<td>" + dr["realDiscount"].ToString() + "</td>");
                strb.Append("<td>" + dr["dateTime"].ToString() + "</td>");
                strb.Append("<td>" + dr["userName"].ToString() + "</td>");
                states = dr["state"].ToString();
                if (states == "0")
                {
                    stateName = "新建单据";
                }
                else if (states == "1" || states == "2")
                {
                    stateName = "现采";
                }
                else
                {
                    stateName = "预采";
                }
                strb.Append("<td>" + stateName + "</td>");
                strb.Append("<td>" + dr["supplier"].ToString() + "</td></tr>");
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

        /// <summary>
        /// //导出列表方法
        /// </summary>
        /// <param name="s_path">文件路径</param>
        public void downloadfile(string s_path)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(s_path);
            HttpContext.Current.Response.ContentType = "application/ms-download";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Type", "application/octet-stream");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            HttpContext.Current.Response.WriteFile(file.FullName);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 导出
        /// </summary>
        public void export()
        {
            string strWhere = "";
            if (type == "supplier")
            {
                if (regionId != null && regionId != "")
                {
                    strWhere = "supplier = '" + name + "' and deleteState=0 and regionId="+regionId;
                }
                else {
                    strWhere = "supplier = '" + name + "' and deleteState=0";
                }
                
            }
            else if (type == "regionName")
            {
                if (regionId != null && regionId != "")
                {
                    strWhere = "regionName = '" + name + "' and deleteState=0 and regionId=" + regionId;
                }
                else
                {
                    strWhere = "regionName = '" + name + "' and deleteState=0";
                }
              
            }
            else if (type == "customerName")
            {
                if (regionId != null && regionId != "")
                {
                    strWhere = "customerName = '" + name + "' and deleteState=0 and regionId=" + regionId;
                }
                else
                {
                    strWhere = "customerName = '" + name + "' and deleteState=0";
                }
                
            }
            groupType = strWhere;
            dsUser = detailsBll.getUser(groupType);
            string isbn = Request["isbn"];
            string price = Request["price"];
            string discount = Request["discount"];
            string user = Request["user"];
            string time = Request["time"];
            string looktime = Request["looktime"];
            string state = Request["state"];
            if (isbn != null && isbn != "")
            {
                strWhere += " and isbn like '%" + isbn + "%'";
            }
            if (price != "" && price != null)
            {
                string[] sArray = price.Split('于');
                string type1 = sArray[0];
                string number = sArray[1];
                if (strWhere == "" || strWhere == null)
                {
                    if (type1 == "小")
                    {
                        strWhere = "price < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere = "price = '" + number + "'";
                    }
                    else
                    {
                        strWhere = "price > '" + number + "'";
                    }
                }
                else
                {
                    if (type1 == "小")
                    {
                        strWhere += " and price < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere += " and price = '" + number + "'";
                    }
                    else
                    {
                        strWhere += " and price > '" + number + "'";
                    }
                }
            }
            //if (price != null && price != "")
            //{
            //    strWhere += " and price='" + price + "'";
            //}
            if (discount != null && discount != "")
            {
                strWhere += " and realDiscount='" + discount + "'";
            }
            if (user != null && user != "" && user != "0")
            {
                strWhere += " and userName='" + user + "'";
            }
            if (time != null && time != "")
            {
                string[] sArray = time.Split('至');
                string startTime = sArray[0];
                string endTime = sArray[1];
                strWhere += " and dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
            }
            if (time == "" || time == null)
            {
                if (looktime != null && looktime != "" && looktime != "null")
                {
                    string[] sArray = looktime.Split('至');
                    string startTime = sArray[0];
                    string endTime = sArray[1];
                    strWhere += " and dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
                }
            }
            if (state != null && state != "" && state != "-1")
            {
                if (state == "1")
                {
                    strWhere += " and (state='1' or state='2')";
                }
                else
                {
                    strWhere += " and state='" + state + "'";
                }
            }

            string Name = name + "-销售明细-" + DateTime.Now.ToString("yyyyMMdd") + new Random(DateTime.Now.Second).Next(10000);
            DataTable dt = detailsBll.ExportExcels(strWhere, type);
            if (dt != null && dt.Rows.Count > 0)
            {
                var path = Server.MapPath("~/download/报表导出/销售报表导出/" + Name + ".xlsx");
                ExcelHelper.x2007.TableToExcelForXLSX(dt, path);
                downloadfile(path);
            }
            else
            {
                Response.Write("<script>alert('没有数据，不能执行导出操作!');</script>");
                Response.End();
            }
        }

        public String Print()
        {
            string strWhere = "";
            if (type == "supplier")
            {
                strWhere = "supplier = '" + name + "' and deleteState=0";
            }
            else if (type == "regionName")
            {
                strWhere = "regionName = '" + name + "' and deleteState=0";
            }
            else if (type == "customerName")
            {
                strWhere = "customerName = '" + name + "' and deleteState=0";
            }
            groupType = strWhere;
            dsUser = detailsBll.getUser(groupType);
            string isbn = Request["isbn"];
            string price = Request["price"];
            string discount = Request["discount"];
            string user = Request["user"];
            string time = Request["time"];
            string looktime = Request["looktime"];
            string state = Request["state"];
            if (isbn != null && isbn != "")
            {
                strWhere += " and isbn like '%" + isbn + "%'";
            }
            if (price != "" && price != null)
            {
                string[] sArray = price.Split('于');
                string type1 = sArray[0];
                string number = sArray[1];
                if (strWhere == "" || strWhere == null)
                {
                    if (type1 == "小")
                    {
                        strWhere = "price < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere = "price = '" + number + "'";
                    }
                    else
                    {
                        strWhere = "price > '" + number + "'";
                    }
                }
                else
                {
                    if (type1 == "小")
                    {
                        strWhere += " and price < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere += " and price = '" + number + "'";
                    }
                    else
                    {
                        strWhere += " and price > '" + number + "'";
                    }
                }
            }
            //if (price != null && price != "")
            //{
            //    strWhere += " and price='" + price + "'";
            //}
            if (discount != null && discount != "")
            {
                strWhere += " and realDiscount='" + discount + "'";
            }
            if (user != null && user != "" && user != "0")
            {
                strWhere += " and userName='" + user + "'";
            }
            if (time != null && time != "")
            {
                string[] sArray = time.Split('至');
                string startTime = sArray[0];
                string endTime = sArray[1];
                strWhere += " and dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
            }
            if (time == "" || time == null)
            {
                if (looktime != null && looktime != "")
                {
                    string[] sArray = looktime.Split('至');
                    string startTime = sArray[0];
                    string endTime = sArray[1];
                    strWhere += " and dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
                }
            }
            if (state != null && state != "" && state != "-1")
            {
                if (state == "1")
                {
                    strWhere += " and (state='1' or state='2')";
                }
                else
                {
                    strWhere += " and state='" + state + "'";
                }
            }

            DataTable dt = detailsBll.ExportExcels(strWhere, type);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + (i + 1) + "</td>");
                sb.Append("<td>" + dt.Rows[i][0] + "</td>");
                //sb.Append("<td>" + dt.Rows[i][1] + "</td>");
                sb.Append("<td>" + dt.Rows[i][2] + "</td>");
                sb.Append("<td>" + dt.Rows[i][3] + "</td>");
                sb.Append("<td>" + dt.Rows[i][4] + "</td>");
                sb.Append("<td>" + dt.Rows[i][5] + "</td>");
                sb.Append("<td>" + dt.Rows[i][6] + "</td>");
                sb.Append("<td>" + dt.Rows[i][7] + "</td>");
                sb.Append("<td>" + dt.Rows[i][10] + "</td>");

                if (type == "customerName")
                {
                    sb.Append("<td>" + dt.Rows[i][9] + "</td>");
                }
                if (type == "regionName")
                {
                    sb.Append("<td>" + dt.Rows[i][11] + "</td>");
                    sb.Append("<td>" + dt.Rows[i][9] + "</td>");
                }
                if (type == "supplier")
                {
                    sb.Append("<td>" + dt.Rows[i][11] + "</td>");
                }
                sb.Append("<td>" + dt.Rows[i][16] + "</td>");
                sb.Append("</tr>");
            }
            return sb.ToString();
        }

        protected void permission()
        {
            RoleBll roleBll = new RoleBll();
            FunctionBll functionBll = new FunctionBll();
            User user = (User)Session["user"];
            userName = user.UserName;
            regionName = user.ReginId.RegionName;
            Role role = new Role();
            role = user.RoleId;
            int roleId = role.RoleId;
            dsPer = functionBll.SelectByRoleId(roleId);
            string userId = user.UserId;
            DataSet dsRole = roleBll.selectRole(userId);
            string roleName = dsRole.Tables[0].Rows[0]["roleName"].ToString();
            if (roleName == "超级管理员")
            {
                isAdmin = true;
            }
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
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 15)
                {
                    funcBookStock = true;
                }
            }
        }
    }
}