using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.AccessMGT
{
    public partial class userManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 5, getCurrentPage = 0, totalCount, intPageCount;
        public string search, strSearch;
        public DataSet dsRegion, dsRole, ds;
        UserBll userBll = new UserBll();
        RegionBll regionBll = new RegionBll();
        RoleBll roleBll = new RoleBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Search();
                getData();
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        protected void getData()
        {
            //获取分页数据
            TableBuilder tbd = new TableBuilder()
            {
                StrTable = "V_User",
                OrderBy = "userID",
                StrColumnlist = "userID,userName,regionName,roleName",
                IntPageSize = pageSize,
                IntPageNum = currentPage,
                StrWhere = " "
            };
            getCurrentPage = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount,out intPageCount);
            //获取地区下拉框数据
            dsRegion = regionBll.select();
            //获取角色下拉框数据
            dsRole = roleBll.select();
        }

        /// <summary>
        /// 查询筛选方法
        /// </summary>
        /// <returns>返回查询参数</returns>
        public string Search()
        {
            try
            {
                search = Request.QueryString["search"];
                strSearch = Request.QueryString["search"];
                if (search.Length == 0)
                {
                    search = "";
                }
                else if (search == null)
                {
                    search = "";
                }
                else
                {
                    search = String.Format(" teaAccount {0} or teaName {0} or collegeName {0} or sex {0} or phone {0} or Email {0} ", "like '%" + search + "%'");
                }
            }
            catch
            {
            }
            return search;
        }
    }
}