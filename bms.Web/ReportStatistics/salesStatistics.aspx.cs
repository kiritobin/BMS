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

namespace bms.Web.reportStatistics
{
    public partial class salesStatistics : System.Web.UI.Page
    {
        DataSet ds;
        SaleMonomerBll salemonBll = new SaleMonomerBll();
        public int totalCount, intPageCount, pageSize = 20;
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();

            //string op = Request["op"].ToString();
            //if (op == "exportAll")
            //{

            //}
            //if (op == "exportDe")
            //{

            //}
        }
        /// <summary>
        /// 获取基础数据
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            User user = (User)Session["user"];
            int regionId = user.ReginId.RegionId;
            string roleName = user.RoleId.RoleName;
            string search = "";
            string strWhere = "";
            string groupbyType = Request["groupbyType"];
            string supplier = Request["supplier"];
            string regionName = Request["regionName"];
            string customerName = Request["customerName"];

            string saleHeadState = Request["saleHeadState"];
            string time = Request["time"];
           

            if (groupbyType == "state" || groupbyType == null)
            {
                groupbyType = "customerName";
            }
            if (supplier != "" && supplier != null)
            {
                strWhere = "supplier='" + supplier + "'";
            }
            if (regionName != "" && regionName != null)
            {
                strWhere = "regionName='" + regionName + "'";
            }
            if (customerName != "" && customerName != null)
            {
                strWhere = "customerName='" + customerName + "'";
            }

            if (saleHeadState != null && saleHeadState != "")
            {
                if (strWhere != null && strWhere != "")
                {
                    if (saleHeadState == "0")
                    {
                        strWhere += " and state='0'";
                    }
                    else if (saleHeadState == "3")
                    {
                        strWhere += " and state='3'";
                    }
                    else
                    {
                        strWhere += " and (state='1' or state='2')";
                    }

                }
                else
                {
                    if (saleHeadState == "0")
                    {

                        strWhere = " state='0'";
                    }
                    else if (saleHeadState == "3")
                    {
                        strWhere = " state='3'";
                    }
                    else
                    {
                        strWhere += " (state='1' or state='2')";
                    }

                }
            }
            if (time != null && time != "")
            {
                string[] sArray = time.Split('至');
                string startTime = sArray[0];
                string endTime = sArray[1];
                if (strWhere != null && strWhere != "")
                {
                    strWhere += " and dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
                }
                else
                {
                    strWhere = "dateTime BETWEEN'" + startTime + "' and '" + endTime + "'";
                }
            }

            if (roleName != "超级管理员")
            {
                if (strWhere == "" || strWhere == null)
                {
                    search = "regionId=" + regionId;
                }
                else
                {
                    search = "and regionId=" + regionId;
                }

            }
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            //string saleHeadId = Request["saleTaskId"];
            //string regionName = Request["regionName"];
            //string userName = Request["userName"];
            //string search = "";

            TableBuilder tb = new TableBuilder();
            tb.StrTable = "v_salemonomer";
            tb.OrderBy = "allTotalPrice desc";
            if (groupbyType == "supplier")
            {
                tb.StrColumnlist = "supplier, sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice";
            }
            else if (groupbyType == "regionName")
            {
                tb.StrColumnlist = "regionName, sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice";
            }
            else
            {
                tb.StrColumnlist = "customerName, sum(number) as allNumber, sum(totalPrice) as allTotalPrice,sum(realPrice) as allRealPrice";
            }
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            if (strWhere == "" || strWhere == null)
            {
                if (groupbyType == "supplier")
                {
                    tb.StrWhere = "supplier like'%'" + search + " GROUP BY " + groupbyType;
                }
                if (groupbyType == "regionName")
                {
                    tb.StrWhere = "regionName like'%'" + search + " GROUP BY " + groupbyType;
                }
                if (groupbyType == "customerName")
                {
                    tb.StrWhere = "customerName like'%'" + search + " GROUP BY " + groupbyType;
                }

            }
            else
            {
                tb.StrWhere = strWhere + search + " GROUP BY " + groupbyType;
            }
            //tb.StrWhere = search == "" ? "deleteState=0 and saleTaskId=" + "'" + saleId + "'" : search + " and deleteState = 0 and saleTaskId=" + "'" + saleId + "'";
            //获取展示的客户数据
            ds = salemonBll.selectBypage(tb, out totalCount, out intPageCount);
            StringBuilder strb = new StringBuilder();
            int dscount = ds.Tables[0].Rows.Count;
            string kinds;
            string type;
            string condition;
            string state = "";
            if (saleHeadState != null && saleHeadState != "")
            {
                state = saleHeadState;
            }
            for (int i = 0; i < dscount; i++)
            {
                //序号 (i + 1 + ((currentPage - 1) * pageSize)) 
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");

                if (groupbyType == "supplier")
                {
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["supplier"].ToString() + "</td>");
                    type = "supplier";
                    condition = ds.Tables[0].Rows[i]["supplier"].ToString();
                }
                else if (groupbyType == "regionName")
                {
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</td>");
                    type = "regionName";
                    condition = ds.Tables[0].Rows[i]["regionName"].ToString();
                }
                else
                {
                    strb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</td>");
                    type = "customerName";
                    condition = ds.Tables[0].Rows[i]["customerName"].ToString();
                }
                kinds = salemonBll.getkindsGroupBy(condition, type, state,time).ToString();
                strb.Append("<td>" + kinds + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allNumber"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allTotalPrice"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["allRealPrice"].ToString() + "</td>");
                strb.Append("<td><button class='btn btn-info btn-sm look'><i class='fa fa-search'></i></button></td></tr>");
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
    }
}