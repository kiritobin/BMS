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
    using Result = Enums.OpResult;
    public partial class bookshelfManagement : System.Web.UI.Page
    {
        public int totalCount, intPageCount;
        public DataSet regionDs, ds;
        GoodsShelvesBll shelvesbll = new GoodsShelvesBll();
        RegionBll rbll = new RegionBll();
        protected void Page_Load(object sender, EventArgs e)
        {
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
            if (op == "del")
            {
                int shelfId = int.Parse(Request["shelfId"]);
                Result result = isDelete();
                if (result == Result.记录不存在)
                {
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
                }
                else
                {
                    Response.Write("在其它表里关联引用，不能删除");
                    Response.End();
                }
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
                search = "";
            }
            else if ((goods != null || goods != "") && (region == "" || region == null))
            {

                search = String.Format("shelvesName= '{0}'", goods);
            }
            else if ((goods == null || goods == "") && (region != "" || region != null))
            {
                search = String.Format("regionName='{0}'", region);
            }
            else
            {
                search = String.Format("regionName='{0}' and shelvesName= {1}", region, goods);
            }

            TableBuilder tb = new TableBuilder();
            tb.StrTable = "V_GoodsShelves";
            tb.OrderBy = "goodsShelvesId";
            tb.StrColumnlist = "goodsShelvesId,shelvesName,regionId,regionName";
            tb.IntPageSize = 6;
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
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * 3)) + "</td>");
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
    }
}