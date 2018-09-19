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
    using Result = Enums.OpResult;
    public partial class customerManagement : System.Web.UI.Page
    {
        protected DataSet ds = null;//获取客户数据集
        protected DataSet regionDs = null;//获取地区数据集
        protected int getCurrentPage;//当前页
        protected int totalPage;//总页数
        protected int pagesize = 4;
        protected string searchRegion;//下拉查询
        protected string showStr;//下拉查询
        protected string strWhere;//输入框查询
        protected string op;//请求ajax传入的op值
        RSACryptoService RSAC = new RSACryptoService();
        CustomerBll cBll = new CustomerBll();
        RegionBll reBll = new RegionBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            searchRegion = Request.QueryString["regionID"];
            strWhere = Request.QueryString["strWhere"];
            op = Context.Request["op"];

            if (!IsPostBack)
            {
                getData("");
            }
            if (searchRegion != "null" && searchRegion!="" && searchRegion != null)
            {
                getData(SearchRegion());
            }
            if (strWhere != null)
            {
                getData(SearchStrWhere());
            }
            if (op == "add")
            {
                InsertCustomer();
            }
            if(op== "editData")
            {
                UpdateCustomer();
            }
            if (op == "del")
            {
                DeleteCustomer();
            }
            if(op== "reset")
            {
                RestPwd();
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
               // searchRegion = Request.QueryString["strWhere"];
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

        /// <summary>
        /// 输入框查询
        /// </summary>
        /// <returns></returns>
        public string SearchStrWhere()
        {
            if (strWhere.Length == 0 || strWhere == "" || strWhere == "")
            {
                strWhere = "";
            }
            else
            {
                strWhere = String.Format("customerID {0} or customerName {0} or regionName {0}", "like " + "'%" + strWhere + "%'");
            }
            return strWhere;
        }
        /// <summary>
        /// 添加客户
        /// </summary>
        public void InsertCustomer()
        {
            string custId = Context.Request["customerId"],
                custName = Context.Request["cutomerName"],
                regId = Context.Request["zoneId"],
                pwd = "000000";
            string custPwd = RSAC.Encrypt(pwd);
            bool isCustomerId = cBll.SelectById(custId);
            if (isCustomerId == true)
            {
                Response.Write("该账号已经存在");
                Response.End();
            }
            else
            {
                Region reg = new Region()
                {
                    RegionId = int.Parse(regId)
                };
                Customer cust = new Customer()
                {
                    CustomerId = Convert.ToInt32(custId),
                    CustomerName = custName,
                    CustomerPwd = custPwd,
                    RegionId = reg
                };
                Result result = cBll.Insert(cust);
                if (result == Result.添加成功)
                {
                    Response.Write("添加成功");
                    Response.End();
                }
                else
                {
                    Response.Write("添加失败");
                    Response.End();
                }
            }
        }
        /// <summary>
        /// 更新客户信息
        /// </summary>
        public void UpdateCustomer()
        {
            string custId = Context.Request["customerId"];
            string custName = Context.Request["customername"];
            string regId = Context.Request["regionid"];
            Region reg = new Region()
            {
                RegionId = int.Parse(regId)
            };
            Customer customer = new Customer()
            {
                CustomerId = Convert.ToInt32(custId),
                CustomerName = custName,
                RegionId = reg
            };
            Result row = cBll.update(customer);
            if(row == Result.更新成功)
            {
                Response.Write("更新成功");
                Response.End();
            }
            else
            {
                Response.Write("更新失败");
                Response.End();
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        public void DeleteCustomer()
        {
            string customerID = Context.Request["cutomerId"];
            Result row = cBll.Delete(int.Parse(customerID));
            if (row == Result.删除成功)
            {
                Response.Write("删除成功");
                Response.End();
            }
            else
            {
                Response.Write("删除失败");
                Response.End();
            }
        }
        public void RestPwd()
        {
            string customerId = Context.Request["customerid"];
            string pwd = RSAC.Encrypt("000000");
            Result result = cBll.ResetPwd(int.Parse(customerId), pwd);
            if(result == Result.更新成功)
            {
                Response.Write("重置成功");
                Response.End();
            }
            else
            {
                Response.Write("重置失败");
                Response.End();
            }
        }
    }
}