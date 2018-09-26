using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.InventoryMGT
{
    using Result = Enums.OpResult;
    public partial class addStock : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int singleHeadId=0;
            if (!IsPostBack)
            {
                string id = Request.QueryString["singleHeadId"];
                if(id != null&& id != "")
                {
                    Session["id"] = id;
                    singleHeadId = Convert.ToInt32(id);
                }
                else
                {
                    singleHeadId = Convert.ToInt32(Session["id"]);
                }
            }
            string op = Request["op"];
            if(op == "add")
            {
                string monomerID = Request["ID"];
                string isbn = Request["isbn"];
                string allCount = Request["allCount"];
                string price = Request["price"];
                string discount = Request["discount"];
                string realPrice = Request["realPrice"];
                string allPrice = Request["allPrice"];
                string goodsShelf = Request["goodsShelf"];
                string remark = Request["remark"];
                Monomers monomers = new Monomers();
                monomers.MonomersId = Convert.ToInt32(monomerID);
                monomers.SingleHeadId.SingleHeadId = Convert.ToInt32(singleHeadId);
                monomers.Isbn.Isbn = isbn;
                monomers.Number = Convert.ToInt32(allCount);
                monomers.UPrice.Price = Convert.ToDouble(price);
                monomers.TotalPrice = Convert.ToDouble(allPrice);
                monomers.RealPrice = Convert.ToDouble(realPrice);
                monomers.Discount = Convert.ToDouble(discount);
                monomers.GoodsShelvesId.GoodsShelvesId = Convert.ToInt32(goodsShelf);
                monomers.Type = 1;
                WarehousingBll wareBll = new WarehousingBll();
                Result row = wareBll.insert(monomers);
                if(row == Result.添加成功)
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
    }
}