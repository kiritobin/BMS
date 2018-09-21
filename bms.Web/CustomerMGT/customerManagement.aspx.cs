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

        public int totalCount, intPageCount, pageSize=20;
        public DataSet regionDs, ds;
        CustomerBll cbll = new CustomerBll();
        RegionBll rbll = new RegionBll();
        UserBll userbll = new UserBll();
        RSACryptoService rasc = new RSACryptoService();
        protected void Page_Load(object sender, EventArgs e)
        {
            string op = Context.Request["op"];
            if (!IsPostBack)
            {
                getData();
            }
            if(op == "add")
            {
                AddCustomer();
            }
            else if(op == "editor")
            {
                UpdateCustomer();
            }
            else if(op == "del")
            {
                Delete();
            }
            else if(op == "reset")
            {
                ResetPwd();
            }
        }
        /// <summary>
        /// 获取基础数据及查询方法
        /// </summary>
        /// <returns></returns>
        public string getData()
        {
            //获取分页数据
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string search = Request["search"];
            if(search == "" || search == null)
            {
                search = "";
            }
            else
            {
                search = "customerName ='" + search +"'";
            }

            TableBuilder tb = new TableBuilder();
            tb.StrTable = "T_Customer";
            tb.OrderBy = "customerID";
            tb.StrColumnlist = "customerID,customerName";
            tb.IntPageSize = pageSize;
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
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerID"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["customerName"].ToString() + "</td>");
                strb.Append("<td>" + "<button type='button' class='btn btn-default btn-sm reset_pwd'>" + "重置密码" + "</button>" + " </td>");
                strb.Append("<td>" + "<button class='btn btn-warning btn-sm btn_Editor' data-toggle='modal' data-target='#myModa2'>" + "<i class='fa fa-pencil fa-lg'></i>" + "&nbsp 编辑" + "</button>");
                strb.Append("<button class='btn btn-danger btn-sm btn_delete'>" + "<i class='fa fa-trash-o fa-lg'></i>&nbsp 删除" + "</button>" + " </td></tr>");
            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
        }
        /// <summary>
        /// 添加客户
        /// </summary>
        public void AddCustomer()
        {
            string customerId = Request["customerId"];
            string customerName = Request["cutomerName"];
            string zoneId = Request["zoneId"];
            //if(customerId == "" || customerName == "" || zoneId == "")
            //{
            //    Response.Write("有未填项");
            //    Response.End();
            //}
            //else
            //{
                string pwd = rasc.Encrypt("000000");
                Region reg = new Region()
                {
                    RegionId = Convert.ToInt32(zoneId)
                };
                Customer ct = new Customer()
                {
                    CustomerId = Convert.ToInt32(customerId),
                    CustomerName = customerName,
                    CustomerPwd = pwd,
                    RegionId = reg
                };
                Result row = cbll.Insert(ct);
                if (row == Result.添加成功)
                {
                    Response.Write("添加成功");
                    Response.End();
                }
                else
                {
                    Response.Write("添加失败");
                    Response.End();
                }
            //}
        }
        /// <summary>
        /// 更新客户信息
        /// </summary>
        public void UpdateCustomer()
        {
            string id = Context.Request["customerid"];
            string name = Context.Request["customername"];
            Region reg = new Region();
            Customer cust = new Customer()
            {
                CustomerId = int.Parse(id),
                CustomerName = name
            };
            Result row = cbll.update(cust);
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
        public void Delete()
        {
            int id = Convert.ToInt32(Request["cutomerId"]);
            Result isDelete = cbll.IsDelete("T_LibraryCollection", "customerId", id.ToString());
            if(isDelete == Result.关联引用)
            {
                Response.Write("该客户已被关联到其他表，不能删除！");
            }
            else
            {
                Result row = cbll.Delete(id);
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
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        public void ResetPwd()
        {
            int id = Convert.ToInt32(Request["customerid"]);
            string pwd = rasc.Encrypt("000000");
            Result row = cbll.ResetPwd(id, pwd);
                if(row == Result.更新成功)
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