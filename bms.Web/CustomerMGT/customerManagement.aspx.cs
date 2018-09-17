using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using bms.Model;
using System.Data;

namespace bms.Web.CustomerMGT
{
    public partial class customerManagement : System.Web.UI.Page
    {
        protected DataSet ds = null;//获取客户数据集
        protected DataSet regionDs = null;//获取地区数据集
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getData();
            }
        }
        public void getData()
        {
            CustomerBll cBll = new CustomerBll();
            RegionBll reBll = new RegionBll();
            regionDs = reBll.select();
            TableBuilder tBuilder = new TableBuilder()
            {
                StrTable = "V_Customer",
                StrWhere = "",
                IntPageNum = 1,
                IntPageSize = 4
            };
            int totalCount = 1;
            int pageCount;
            ds = cBll.selectByPage(tBuilder, out totalCount, out pageCount);
        }
    }
}