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
    public partial class customerManagement : System.Web.UI.Page
    {
        protected DataSet ds = null;//获取客户数据集
        protected DataSet regionDs = null;//获取地区数据集
        protected int getCurrentPage;//当前页
        protected int totalPage;//总页数
        protected int pagesize = 4;
        protected string searchRegion;
        protected string showStr;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getData("");
            }
            if (searchRegion != null)
            {
                getData(SearchRegion());
            }
        }
        /// <summary>
        /// 获取基础数据
        /// </summary>
        public void getData(String strWhere)
        {
            string currentPage = Request.QueryString["currentPage"];
            if (currentPage == null || currentPage.Length < 0)
            {
                currentPage = "1";
            }
            CustomerBll cBll = new CustomerBll();
            RegionBll reBll = new RegionBll();
            regionDs = reBll.select();
            TableBuilder tBuilder = new TableBuilder()
            {
                StrTable = "V_Customer",
                StrColumnlist = "customerID,customerName,regionId,regionName",
                OrderBy = "customerID",
                StrWhere = strWhere,
                IntPageNum = int.Parse(currentPage),
                IntPageSize = pagesize
            };
            int totalCount;
            int pageCount;
            ds = cBll.selectByPage(tBuilder, out totalCount, out pageCount);
            getCurrentPage = int.Parse(currentPage);
            totalPage = pageCount;
        }
        /// <summary>
        /// 地区下拉查询
        /// </summary>
        /// <returns></returns>
        public string SearchRegion()
        {
            try
            {
                searchRegion = Request.QueryString["strWhere"];
                if (searchRegion.Length == 0 || searchRegion == "" || searchRegion == "0")
                {
                    searchRegion = "";
                }
                else
                {
                    showStr = searchRegion;
                    searchRegion = String.Format("regionId={0}", "'" + searchRegion + "'");
                }
            }
            catch { }
            return searchRegion;
        }
    }
}