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

namespace bms.Web.ReportStatistics
{
    public partial class retailDetails : System.Web.UI.Page
    {
        DataSet ds, dsPer;
        SaleMonomerBll salemonBll = new SaleMonomerBll();
        RetailBll retailBll = new RetailBll();
        public int totalCount, intPageCount, pageSize = 20;
        public DataSet dsUser = null;
        public string type = "", name = "", region="", groupType = "", userName, regionName, bookKinds, allBookCount, allPricePrint, realPricePrint;
        public DateTime dateOfMade;
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail, isAdmin, funcBookStock;
        protected void Page_Load(object sender, EventArgs e)
        {
            type = Request.QueryString["type"];
            name = Request.QueryString["name"];
            region = Request.QueryString["region"];
            if (!IsPostBack)
            {
                //初始化session
                if (Session["type"] == null || Session["type"].ToString() == "")
                {
                    Session["type"] = "";
                }
                if (Session["name"] == null || Session["name"].ToString() == "")
                {
                    Session["name"] = "";
                }
                if (Session["region"] == null || Session["region"].ToString() == "")
                {
                    Session["region"] = "";
                }
                //获取前端传输的值
                type = Request.QueryString["type"];
                name = Request.QueryString["name"];
                region = Request.QueryString["region"];
                //判断前端传值是否为空，若不为空将值存入到session中否则从session中取值，
                if (type == null || type == "")
                {
                    type = Session["type"].ToString();
                }
                else
                {
                    Session["type"] = type;
                }
                if (name == null || name == "")
                {
                    type = Session["type"].ToString();
                }
                else
                {
                    Session["name"] = type;
                }
                if (region == null || region == "")
                {
                    region = Session["region"].ToString();
                }
                else
                {
                    Session["region"] = region;
                }
            }
            getData();
            census();
            permission();
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
        /// 打印统计
        /// </summary>
        public void census()
        {
            string isbn = Request["isbn"];
            string price = Request["price"];
            string discount = Request["discount"];
            string user = Request["user"];
            string time = Request["time"];
            string looktime = Request["looktime"];
            string payment = Request["payment"];
            string strWhere = groupType;
            if (isbn != null && isbn != "")
            {
                strWhere += " and isbn='" + isbn + "'";
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
                        strWhere = "unitPrice < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere = "unitPrice = '" + number + "'";
                    }
                    else
                    {
                        strWhere = "unitPrice > '" + number + "'";
                    }
                }
                else
                {
                    if (type1 == "小")
                    {
                        strWhere += " and unitPrice < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere += " and unitPrice = '" + number + "'";
                    }
                    else
                    {
                        strWhere += " and unitPrice > '" + number + "'";
                    }
                }
            }
            if (discount != null && discount != "")
            {
                strWhere += " and realDiscount=" + discount;
            }
            if (user != null && user != "")
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
            if (payment != null && payment != "")
            {
                strWhere += " and payment='" + payment + "'";
            }
            else
            {
                strWhere += " and payment!='未支付'";
            }
            DataTable dt = retailBll.census(strWhere, type);
            bookKinds = dt.Rows[0]["品种数"].ToString();
            allBookCount = dt.Rows[0]["总数量"].ToString();
            allPricePrint = dt.Rows[0]["总码洋"].ToString();
            realPricePrint = dt.Rows[0]["总实洋"].ToString();
        }

        public String Print()
        {
            string isbn = Request["isbn"];
            string price = Request["price"];
            string discount = Request["discount"];
            string user = Request["user"];
            string time = Request["time"];
            string looktime = Request["looktime"];
            string payment = Request["payment"];
            string strWhere = groupType;
            if (isbn != null && isbn != "")
            {
                strWhere += " and isbn='" + isbn + "'";
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
                        strWhere = "unitPrice < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere = "unitPrice = '" + number + "'";
                    }
                    else
                    {
                        strWhere = "unitPrice > '" + number + "'";
                    }
                }
                else
                {
                    if (type1 == "小")
                    {
                        strWhere += " and unitPrice < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere += " and unitPrice = '" + number + "'";
                    }
                    else
                    {
                        strWhere += " and unitPrice > '" + number + "'";
                    }
                }
            }
            if (discount != null && discount != "")
            {
                strWhere += " and realDiscount=" + discount;
            }
            if (user != null && user != "")
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
            if (payment != null && payment != "")
            {
                strWhere += " and payment='" + payment + "'";
            }
            else
            {
                strWhere += " and payment!='未支付'";
            }
            DataTable dt = retailBll.ExportExcel(strWhere, type);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + (i + 1) + "</td>");
                sb.Append("<td>" + dt.Rows[i]["ISBN"] + "</td>");
                sb.Append("<td>" + dt.Rows[i]["书名"] + "</td>");
                sb.Append("<td>" + dt.Rows[i]["单价"] + "</td>");
                sb.Append("<td>" + dt.Rows[i]["供应商"] + "</td>");
                sb.Append("<td>" + dt.Rows[i]["数量"] + "</td>");
                sb.Append("<td>" + dt.Rows[i]["码洋"] + "</td>");
                sb.Append("<td>" + dt.Rows[i]["实洋"] + "</td>");
                sb.Append("<td>" + dt.Rows[i]["折扣"] + "</td>");
                sb.Append("<td>" + dt.Rows[i]["交易时间"] + "</td>");
                sb.Append("<td>" + dt.Rows[i]["收银员"] + "</td>");
                sb.Append("<td>" + dt.Rows[i]["支付方式"] + "</td>");
                sb.Append("</tr>");
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            string strWhere = "";
            if (type == "regionName")
            {
                strWhere = "regionName ='" + name + "' and deleteState=0";
            }
            else if (type == "payment")
            {
                if (region != null && region != "")
                {
                    strWhere = "payment = '" + name + "' and deleteState=0 and regionName='"+region+"'";
                }
                else
                {
                    strWhere = "payment = '" + name + "' and deleteState=0";
                }
            }
            groupType = "where " + strWhere;
            dsUser = retailBll.getUser(groupType);
            string isbn = Request["isbn"];
            string price = Request["price"];
            string discount = Request["discount"];
            string user = Request["user"];
            string time = Request["time"];
            string looktime = Request["looktime"];
            string payment = Request["payment"];
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
                        strWhere = "unitPrice < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere = "unitPrice = '" + number + "'";
                    }
                    else
                    {
                        strWhere = "unitPrice > '" + number + "'";
                    }
                }
                else
                {
                    if (type1 == "小")
                    {
                        strWhere += " and unitPrice < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere += " and unitPrice = '" + number + "'";
                    }
                    else
                    {
                        strWhere += " and unitPrice > '" + number + "'";
                    }
                }
            }
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
            if (payment != null && payment != "")
            {
                strWhere += " and payment='" + payment + "'";
            }
            else
            {
                strWhere += " and payment!='未支付'";
            }
            strWhere += " group by bookNum,supplier," + type;
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "v_retailmonomer";
            tb.OrderBy = type;
            tb.StrColumnlist = "isbn,bookNum,bookName,unitPrice,sum(number) as number, sum(totalPrice) as totalPrice,sum(realPrice) as realPrice,realDiscount,dateTime,userName,payment,regionName,supplier";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = strWhere;
            //获取展示的客户数据
            ds = salemonBll.selectBypage(tb, out totalCount, out intPageCount);
            StringBuilder strb = new StringBuilder();
            int dscount = ds.Tables[0].Rows.Count;
            for (int i = 0; i < dscount; i++)
            {
                DataRow dr = ds.Tables[0].Rows[i];
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + dr["isbn"].ToString() + "</td>");
                strb.Append("<td>" + dr["bookNum"].ToString() + "</td>");
                strb.Append("<td>" + dr["bookName"].ToString() + "</td>");
                strb.Append("<td>" + dr["unitPrice"].ToString() + "</td>");
                strb.Append("<td>" + dr["number"].ToString() + "</td>");
                strb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                strb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                strb.Append("<td>" + dr["realDiscount"].ToString() + "</td>");
                strb.Append("<td>" + dr["dateTime"].ToString() + "</td>");
                strb.Append("<td>" + dr["userName"].ToString() + "</td>");
                strb.Append("<td>" + dr["supplier"].ToString() + "</td>");
                strb.Append("<td>" + dr["payment"].ToString() + "</td>");
                strb.Append("<td>" + dr["regionName"].ToString() + "</td></tr>");
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
            string isbn = Request.QueryString["isbn"];
            string price = Request.QueryString["price"];
            string discount = Request.QueryString["discount"];
            string user = Request.QueryString["user"];
            string time = Request.QueryString["time"];
            string looktime = Request.QueryString["looktime"];
            string payment = Request.QueryString["payment"];
            string strWhere = groupType;
            string fileName = name;
            if(isbn!=null&& isbn != "")
            {
                strWhere += " and isbn='" + isbn + "'";
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
                        strWhere = "unitPrice < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere = "unitPrice = '" + number + "'";
                    }
                    else
                    {
                        strWhere = "unitPrice > '" + number + "'";
                    }
                }
                else
                {
                    if (type1 == "小")
                    {
                        strWhere += " and unitPrice < '" + number + "'";
                    }
                    else if (type1 == "等")
                    {
                        strWhere += " and unitPrice = '" + number + "'";
                    }
                    else
                    {
                        strWhere += " and unitPrice > '" + number + "'";
                    }
                }
            }
            if (discount != null && discount != "")
            {
                strWhere += " and realDiscount=" + discount;
            }
            if (user != null && user != "")
            {
                strWhere += " and userName='" + user + "'";
            }
            if (time != null && time != "")
            {
                //strWhere += " and dateTime='" + time + "'";
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
            if (payment != null && payment != "")
            {
                strWhere += " and payment='" + payment + "'";
            }
            else
            {
                strWhere += " and payment!='未支付'";
            }
            string Name = fileName + "-零售明细-" + DateTime.Now.ToString("yyyyMMdd") + new Random(DateTime.Now.Second).Next(10000);
            DataTable dt = retailBll.ExportExcels(strWhere, type);
            if (dt != null && dt.Rows.Count > 0)
            {
                var path = Server.MapPath("~/download/报表导出/零售报表导出/" + Name + ".xlsx");
                ExcelHelper.x2007.TableToExcelForXLSX(dt, path);
                downloadfile(path);
            }
            else
            {
                Response.Write("<script>alert('没有数据，不能执行导出操作!');</script>");
                Response.End();
            }
        }


        /// <summary>
        /// 权限控制
        /// </summary>
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