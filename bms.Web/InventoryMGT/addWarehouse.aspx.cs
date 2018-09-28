using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using bms.Model;

namespace bms.Web.InventoryMGT
{
    using Result = Enums.OpResult;
    public partial class addWarehouse : System.Web.UI.Page
    {
        protected DataSet ds, dsGood;
        protected int pageSize=20, totalCount, intPageCount;
        string singleHeadId;
        UserBll userBll = new UserBll();
        WarehousingBll warehousingBll = new WarehousingBll();
        StockBll stockBll = new StockBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                singleHeadId = Request.QueryString["sId"];
                if(singleHeadId!="" && singleHeadId != null)
                {
                    Session["singleHeadId"] = singleHeadId;
                }
                else
                {
                    singleHeadId = Session["singleHeadId"].ToString();
                }
            }
            getData();
            User user = (User)Session["user"];
            string op = Request["op"];
            if(op == "add")
            {
                string isbn = Request["isbn"];
                int billCount = Convert.ToInt32(Request["billCount"]);
                string totalPrice = Request["totalPrice"];
                string realPrice = Request["realPrice"];
                string discount = Request["discount"];
                string uPrice = Request["uPrice"];
                long monCount = warehousingBll.getCount(Convert.ToInt64(singleHeadId));
                long monId;
                if (monCount > 0)
                {
                    monId = monCount + 1;
                }
                else
                {
                    monId = 1;
                }
                Monomers monomers = new Monomers();
                monomers.Discount = Convert.ToInt32(discount);
                BookBasicData bookBasic = new BookBasicData();
                bookBasic.Isbn = isbn;
                bookBasic.Price = Convert.ToDouble(uPrice);
                monomers.Isbn = bookBasic;
                monomers.UPrice = bookBasic;
                monomers.MonomersId = Convert.ToInt32(monId);
                monomers.Number = billCount;
                monomers.RealPrice = Convert.ToDouble(realPrice);
                SingleHead single = new SingleHead();
                single.SingleHeadId = singleHeadId;
                monomers.SingleHeadId = single;
                monomers.TotalPrice = Convert.ToDouble(totalPrice);
                monomers.Type = 0;
                Result row = warehousingBll.insertMono(monomers);
                if (row == Result.添加成功)
                {
                    Response.Write("添加成功");
                    Response.End();
                    DataSet dsGoods = stockBll.SelectByIsbn(isbn);
                    int count= billCount;
                    int allCount = 0,allCounts=0;
                    for (int i = 0; i < dsGoods.Tables[0].Rows.Count; i++)
                    {
                        allCount = Convert.ToInt32(dsGoods.Tables[0].Rows[i]["stockNum"]);
                    }
                    allCounts = allCounts + allCount;
                    if (billCount > allCounts)
                    {
                        Response.Write("库存数量不足");
                        Response.End();
                    }
                    else
                    {
                        for (int i = 0; i < dsGoods.Tables[0].Rows.Count; i++)
                        {
                            billCount = count;
                            int stockNum = Convert.ToInt32(dsGoods.Tables[0].Rows[i]["stockNum"]);
                            int goodsId = Convert.ToInt32(dsGoods.Tables[0].Rows[i]["goodsShelvesId"]);
                            if (billCount <= stockNum)
                            {
                                int a = stockNum - billCount;
                                Result result = stockBll.update(a, goodsId);
                                if (result == Result.更新成功)
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
                            else
                            {
                                count = billCount - stockNum;
                                Result result = stockBll.update(0, goodsId);
                                if(count == 0)
                                {
                                    Response.Write("添加成功");
                                    Response.End();
                                }
                                if (result == Result.更新失败)
                                {
                                    Response.Write("添加失败");
                                    Response.End();
                                }
                            }
                        }
                    }
                }
                else
                {
                    Response.Write("添加失败");
                    Response.End();
                }

            }
            if(op == "del")
            {
                int Id = Convert.ToInt32(Request["ID"]);
                Result row = warehousingBll.deleteMonomer(singleHeadId, Id,0);
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
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
            GoodsShelvesBll goods = new GoodsShelvesBll();
            int regionId = user.ReginId.RegionId;
            dsGood = goods.Select(regionId);
        }
        /// <summary>
        /// 获取分页数据
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
            string op = Request["op"];
            TableBuilder tbd = new TableBuilder();
            tbd.StrTable = "V_Monomers";
            tbd.OrderBy = "monId";
            tbd.StrColumnlist = "singleHeadId,monId,ISBN,number,uPrice,totalPrice,realPrice,discount,goodsShelvesId,shelvesName,type,deleteState";
            tbd.IntPageSize = pageSize;
            tbd.StrWhere = "deleteState=0 and singleHeadId='" + singleHeadId+"'";
            tbd.IntPageNum = currentPage;
            //获取展示的用户数据
            ds = userBll.selectByPage(tbd, out totalCount, out intPageCount);

            //生成table
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<tbody>");
            int count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                System.Data.DataRow dr = ds.Tables[0].Rows[i];
                sb.Append("<tr><td>" + dr["monId"].ToString() + "</td>");
                sb.Append("<td>" + dr["ISBN"].ToString() + "</td>");
                sb.Append("<td>" + dr["number"].ToString() + "</td>");
                sb.Append("<td>" + dr["uPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["totalPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["realPrice"].ToString() + "</td>");
                sb.Append("<td>" + dr["discount"].ToString() + "</td>");
                sb.Append("<td>" + dr["shelvesName"].ToString() + "</td>");
                sb.Append("<td><input type='hidden' value='" + dr["monId"].ToString() + "'/>");
                sb.Append("<button class='btn btn-danger btn-sm btn-delete'><i class='fa fa-trash'></i></button></td></tr>");
            }
            sb.Append("</tbody>");
            sb.Append("<input type='hidden' value='" + intPageCount + "' id='intPageCount' />");
            if (op == "paging")
            {
                Response.Write(sb.ToString());
                Response.End();
            }
            return sb.ToString();
        }
    }
}