using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.AccessMGT
{
    using Result = Enums.OpResult;
    public partial class organizationalManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 5, totalCount, intPageCount;
        public string search, regionId;
        public DataSet ds;
        RegionBll regionBll = new RegionBll();
        UserBll userBll = new UserBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            getData();
            string op = Request["op"];
            if(op == "add")
            {
                string regionName = Request["name"];
                //添加分公司
                Result row = regionBll.insert(regionName);
                if (row == Result.添加成功)
                {
                    //获取分公司id
                    DataSet ds = regionBll.select();
                    int i = ds.Tables[0].Rows.Count;
                    Region region = new Region();
                    region.RegionId = Convert.ToInt32(ds.Tables[0].Rows[i]["regionId"].ToString());
                    //添加货架
                    GoodsShelves goods = new GoodsShelves();
                    goods.RegionId = region;
                    goods.ShelvesName = "未上架";
                    GoodsShelvesBll goodsBll = new GoodsShelvesBll();
                    //Result good = goodsBll.insert(goods);
                    //if (good == Result.添加成功)
                    //{
                    //    //添加销售计划
                    //    SaleTaskBll saleBll = new SaleTaskBll();
                    //    SaleTask sale = new SaleTask();
                    //    Result result = saleBll.insert(sale);
                    //    if (result == Result.添加成功)
                    //    {
                    //        Response.Write("添加成功");
                    //        Response.End();
                    //    }
                    //    else
                    //    {
                    //        Response.Write("添加失败");
                    //        Response.End();
                    //    }
                    //}
                    //else
                    //{
                    //    Response.Write("添加失败");
                    //    Response.End();
                    //}
                }
                else
                {
                    Response.Write("添加失败");
                    Response.End();
                }
            }
            else if(op == "del")
            {
                string regionId = Request["regionId"];
                Result result = IsdeleteAdmin(regionId);
                if (result == Result.记录不存在)
                {
                    Result row = regionBll.delete(Convert.ToInt32(regionId));
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
                else
                {
                    Response.Write("在其他表中有关联，不可删除");
                    Response.End();
                }
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        protected string getData()
        {
            currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            search = Request["search"];
            if (search != "" && search != null)
            {
                search = String.Format(" regionName {0}", "like '%" + search + "%'");
            }
            //获取分页数据
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "T_Region";
            tbd.OrderBy = "regionId";
            tbd.StrColumnlist = "regionId,regionName";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = search;
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);
            //生成table
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody class='text-center'>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * pageSize)) + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["regionName"].ToString() + "</ td >");
                sb.Append("<input type='hidden' value='" + ds.Tables[0].Rows[i]["regionId"].ToString() + "' id='regionId' />");
                sb.Append("<td><button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash-o fa-lg'></i></button></td></ tr >");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            string op = Request["op"];
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }

        /// <summary>
        /// 在删除前判断该记录在其他表中是否被引用
        /// </summary>
        /// <returns></returns>
        public Result IsdeleteAdmin(string regionId)
        {
            Result row = Result.记录不存在;
            if (userBll.IsDelete("T_Customer", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_User", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_GoodsShelves", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_Stock", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_SingleHead", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_SellOffHead", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            if (userBll.IsDelete("T_ReplenishmentHead", "regionId", regionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            return row;
        }
    }
}