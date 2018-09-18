using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.BasicInfor
{
    public partial class organizationalManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 5, getCurrentPage = 0, totalCount, intPageCount;
        public string search, strSearch, regionId, roleId, regionEdit, roleEdit;
        public DataSet dsRegion, dsRole, ds;
        RSACryptoService rsa = new RSACryptoService();
        UserBll userBll = new UserBll();
        RegionBll regionBll = new RegionBll();
        RoleBll roleBll = new RoleBll();
        User user = new User();
        Role role = new Role();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 获取数据
        /// </summary>
        protected void getData(string strWhere)
        {
            currentPage = Convert.ToInt32(Request.QueryString["currentPage"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "T_Region";
            tbd.OrderBy = "regionId";
            tbd.StrColumnlist = "regionId,regionName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = strWhere;
            tbd.IntPageNum = currentPage;
            getCurrentPage = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
        }
    }
}