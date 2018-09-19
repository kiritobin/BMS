using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.CustomerMGT
{
    using System.Text;
    using Result = Enums.OpResult;
    public partial class customerManagement : System.Web.UI.Page
    {
        //protected DataSet ds = null;//获取客户数据集
        //protected DataSet regionDs = null;//获取地区数据集
        //protected int getCurrentPage;//当前页
        //protected int totalPage;//总页数
        //protected int pagesize = 4;
        //protected string searchRegion;//下拉查询
        //protected string showStr;//下拉查询
        //protected string strWhere;//输入框查询
        //protected string op;//请求ajax传入的op值
        //RSACryptoService RSAC = new RSACryptoService();
        //CustomerBll cBll = new CustomerBll();
        //RegionBll reBll = new RegionBll();

        public int totalCount, intPageCount;
        public DataSet regionDs, ds;
        CustomerBll cbll = new CustomerBll();
        RegionBll rbll = new RegionBll();
        UserBll userbll = new UserBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            //searchRegion = Request.QueryString["regionID"];
            //strWhere = Request.QueryString["strWhere"];
            //op = Context.Request["op"];
        }
        public string getData()
        {
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string search = Request["search"];
            string region = Request["region"];
            if (region == "" || region == null)
            {
                search = String.Format("customerID {0} or customerName {0} or regionName {0}", "like " + "'%" + search + "%'");
            }
            else if (search == "" || search == null)
            {
                String.Format("regionId={0}", "'" + region + "'");
            }
            else if((region == "" || region == null)&& (search == "" || search == null))
            {
                search = "";
            }
            else if(search != null || search != "")
            {
                search = String.Format("customerID {0} or customerName {0} or regionName {0}", " like " + "'%" + search + "%'");
            }

            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_Customer";
            tb.OrderBy = "customerID";
            tb.StrColumnlist = "customerID,customerName,regionId,regionName";
            tb.IntPageSize = 3;
            tb.IntPageNum = currentPage;
            tb.StrWhere = search;
            //获取展示的客户数据
            ds = userbll.selectByPage(tb, out totalCount, out intPageCount);
            //获取地区下拉数据
            regionDs = rbll.select();
            //生成table
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * 3)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerID"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</td>");
                //strb.Append("<td>" + ds.Tables[0].Rows[i]["regionId"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</td>");
                strb.Append("<td>" + "<button type='button' class='btn btn-default btn-sm reset_pwd'>" + "重置密码" + "</button>" + " </td>");
                strb.Append("<td>" + "<button class='btn btn-warning btn-sm btn_Editor' data-toggle='modal' data-target='#myModa2'>" + "<i class='fa fa-pencil fa-lg'></i>" + "&nbsp 编辑" + "</button>");
                strb.Append("<button class='btn btn-danger btn-sm btn_delete'>" + "<i class='fa fa-trash-o fa-lg'></i>&nbsp 删除" + "</button>" + " </td></tr>");
            }
            strb.Append("</tbody>");
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