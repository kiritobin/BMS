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
    public partial class collectionManagement : System.Web.UI.Page
    {
        public DataSet dsRegion, ds;
        public int currentPage, pageSize =1, getCurrentPage = 0, totalCount, intPageCount;
        public string search, strSearch,region,strRegion;
        RegionBll regionBll = new RegionBll();
        UserBll userBll = new UserBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Search();
                getData(Search());
            }
            string type = Request.QueryString["type"];
            if (type == "search")
            {
                Search();
                getData(Search());
            }
            else if (type == "region")
            {
                regionSearch();
                getData(regionSearch());
            }
            else if(type=="searchRegion")
            {
                strRegion = Request.QueryString["region"];
                search = Request.QueryString["search"];
                string searchRegion = String.Format(" bookName {0} or ISBN {0} or price {0} or collectionNum {0} or customerName {0} or regionName {0} and regionId={1}", "like '%" + search + "%'" , strRegion);
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        protected void getData(String strWhere)
        {
            //获取分页数据
            currentPage = Convert.ToInt32(Request.QueryString["currentPage"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_LibraryCollection";
            tbd.OrderBy = "bookName";
            tbd.StrColumnlist = "bookName,ISBN,price,collectionNum,customerName,regionName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = strWhere;
            tbd.IntPageNum = currentPage;
            getCurrentPage = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            //获取地区下拉框数据
            dsRegion = regionBll.select();
        }

        /// <summary>
        /// 输入框查询
        /// </summary>
        /// <returns>返回查询参数</returns>
        public string Search()
        {
            try
            {
                search = Request.QueryString["search"];
                strSearch = Request.QueryString["search"];
                if (search == null)
                {
                    search = "";
                }
                else
                {
                    search = String.Format(" bookName {0} or ISBN {0} or price {0} or collectionNum {0} or customerName {0} or regionName {0}", "like '%" + search + "%'");
                }
            }
            catch
            {
            }
            return search;
        }

        private string regionSearch()
        {
            try
            {
                region = Request.QueryString["region"];
                strRegion = Request.QueryString["region"];
                if (region == null)
                {
                    region = "";
                }
                else
                {
                    region = String.Format("regionId=" + region);
                }
            }
            catch
            {
            }
            return region;
        }
    }
}