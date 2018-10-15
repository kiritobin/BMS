using bms.Bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.BasicInfor
{
    using Model;
    using System.Text;
    using System.Web.Security;
    using Result = Enums.OpResult;
    public partial class bookshelfManagement : System.Web.UI.Page
    {
        public string userName,regionName;
        public int totalCount, intPageCount, PageSize=10;
        public DataSet regionDs, ds,dsPer;
        GoodsShelvesBll shelvesbll = new GoodsShelvesBll();
        RegionBll rbll = new RegionBll();
        protected bool funcOrg, funcRole, funcUser, funcGoods, funcCustom, funcLibrary, funcBook, funcPut, funcOut, funcSale, funcSaleOff, funcReturn, funcSupply, funcRetail;
        protected void Page_Load(object sender, EventArgs e)
        {
            permission();
            getData();
            string op = Request["op"];
            if (op == "add")
            {
                int regionId = int.Parse(Request["regionId"]);
                string shelfName = Request["shelfName"];

                Region reg = new Region()
                {
                    RegionId = regionId
                };
                GoodsShelves shelves = new GoodsShelves()
                {
                    ShelvesName = shelfName,
                    RegionId = reg
                };
                Result row = shelvesbll.selectByName(shelves);
                if (row == Result.记录不存在)
                {
                    Result result = shelvesbll.Insert(shelves);
                    if (result == Result.添加成功)
                    {
                        Response.Write("添加成功");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("添加成功");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("货架名已存在");
                    Response.End();
                }
            }
            if (op == "del")
            {
                int shelfId = int.Parse(Request["shelfId"]);
                //Result result = isDelete();
                //if (result == Result.记录不存在)
                //{
                    Result row = shelvesbll.Delete(shelfId);
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
                //}
                //else
                //{
                //    Response.Write("在其它表里关联引用，不能删除");
                //    Response.End();
                //}
            }
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
        }

        /// <summary>
        /// 判断在其他表中是否有关联
        /// </summary>
        /// <returns></returns>
        public Result isDelete()
        {
            string shelfId = Request["shelfId"];
            Result row;
            if (shelvesbll.isDelete("T_Monomers", "goodsShelvesId", shelfId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            else
            {
                row = Result.记录不存在;
            }
            if (shelvesbll.isDelete("T_Stock", "goodsShelvesId", shelfId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            else
            {
                row = Result.记录不存在;
            }
            return row;
        }
        /// <summary>
        /// 获取基础数据
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
            string goods = Request["goods"];
            string region = Request["region"];
            string search;
            if ((region == "" || region == null) && (goods == "" || goods == null))
            {
                search = "deleteState=0";
            }
            else if ((goods != null || goods != "") && (region == "" || region == null))
            {

                search = String.Format("shelvesName='{0}' and deleteState=0", goods);
            }
            else if ((goods == null || goods == "") && (region != "" || region != null))
            {
                search = String.Format("regionName='{0}' and deleteState=0", region);
            }
            else
            {
                search = String.Format("regionName='{0}' and shelvesName='{1}' and deleteState=0", region, goods);
            }

            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_GoodsShelves";
            tb.OrderBy = "goodsShelvesId";
            tb.StrColumnlist = "goodsShelvesId,shelvesName,regionId,regionName";
            tb.IntPageSize = PageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = search;
            //获取展示的客户数据
            ds = shelvesbll.selectByPage(tb, out totalCount, out intPageCount);
            //获取地区下拉数据
            regionDs = rbll.select();
            //生成table
            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * PageSize)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["goodsShelvesId"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["shelvesName"].ToString() + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</td>");
                strb.Append("<td>" + "<button class='btn btn-danger btn-sm btn_delete'>" + "<i class='fa fa-trash-o fa-lg'></i>" + "</button>" + " </td></tr>");
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

        protected void permission()
        {
            FunctionBll functionBll = new FunctionBll();
            User user = (User)Session["user"];
            userName = user.UserName;
            regionName = user.ReginId.RegionName;
            Role role = new Role();
            role = user.RoleId;
            int roleId = role.RoleId;
            dsPer = functionBll.SelectByRoleId(roleId);
            for (int i=0;i<dsPer.Tables[0].Rows.Count;i++)
            {
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) ==1)
                {
                    funcOrg = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 2)
                {
                    funcRole = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 3)
                {
                    funcUser = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 4)
                {
                    funcGoods = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 5)
                {
                    funcCustom = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 6)
                {
                    funcLibrary = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 7)
                {
                    funcBook = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 8)
                {
                    funcPut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 9)
                {
                    funcOut = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 10)
                {
                    funcSale = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 11)
                {
                    funcSaleOff = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 12)
                {
                    funcReturn = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 13)
                {
                    funcSupply = true;
                }
                if (Convert.ToInt32(dsPer.Tables[0].Rows[i]["functionId"]) == 14)
                {
                    funcRetail = true;
                }
            }
        }
    }
}