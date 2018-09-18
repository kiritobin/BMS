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
            //增删查改操作
            string op = Request["op"];
            if(op == "add")
            {
                string account = Request["account"];
                string region = Request["region"];
                string role = Request["role"];
            }
            else if(op == "editData")
            {
                string region = Request["region"];
                string role = Request["role"];
            }
            else if(op == "edit")
            {
                string account = Request["account"];
                string region = Request["region"];
                string role = Request["role"];
            }
            else if(op == "reset")
            {
                string account = Request["account"];
            }
            else if(op == "del")
            {
                string account = Request["account"];
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        protected void getData()
        {
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_User";
            tbd.OrderBy = "userID";
            tbd.StrColumnlist = "userID,userName,regionName,roleName,regionId,roleId";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "";
            tbd.IntPageNum = currentPage;
            getCurrentPage = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount,out intPageCount);
            //获取地区下拉框数据
            dsRegion = regionBll.select();
            //获取角色下拉框数据
            dsRole = roleBll.select();
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
                    search = String.Format(" userID {0} or userName {0} or regionName {0} or roleName {0}", "like '%" + search + "%'");
                }
            }
            catch
            {
            }
            return search;
        }

        /// <summary>
        /// 分公司下拉框查询
        /// </summary>
        /// <returns>返回查询参数</returns>
        public string regionSelect()
        {
            string region = Request["region"];
        }
        /// <summary>
        /// 职位下拉框查询
        /// </summary>
        /// <returns>返回查询参数</returns>
        public string roleSelect()
        {
            string role = Request["role"];
        }
        /// <summary>
        /// 分公司、职位同时查找
        /// </summary>
        /// <returns>返回查询参数</returns>
        public string allSelect()
        {
            string type = Request["type"];
            string region = Request["region"];
            string role = Request["role"];
        }
    }
}