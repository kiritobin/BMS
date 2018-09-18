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
        public int currentPage = 1, pageSize = 5, getCurrentPage = 0, totalCount, intPageCount;
        RegionBll regionBll = new RegionBll();
        UserBll userBll = new UserBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getData();
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        protected void getData()
        {
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_LibraryCollection";
            tbd.OrderBy = "bookName";
            tbd.StrColumnlist = "bookName,ISBN,price,collectionNum,customerName,regionName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "";
            tbd.IntPageNum = currentPage;
            getCurrentPage = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            //获取地区下拉框数据
            dsRegion = regionBll.select();
        }
    }
}