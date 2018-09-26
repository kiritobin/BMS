using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using System.Data;
using bms.Model;

namespace bms.Web.InventoryMGT
{
    using Result = Enums.OpResult;
    public partial class addReturn : System.Web.UI.Page
    {
        GoodsShelvesBll shelfbll = new GoodsShelvesBll();
        protected DataSet ds, shelf;
        WarehousingBll warebll = new WarehousingBll();
        protected void Page_Load(object sender, EventArgs e)
        {
            string returnId = Request.QueryString["returnId"];
            Session["returnId"] = returnId;

            User user = (User)Session["user"];
            int regId = user.ReginId.RegionId;
            shelf = shelfbll.Select(regId);
            string op = Request["op"];
            if (op == "logout")
            {
                //删除身份凭证
                FormsAuthentication.SignOut();
                //设置Cookie的值为空
                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                //设置Cookie的过期时间为上个月今天
                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
            }
            if (op == "add")
            {
                int singleHeadId = int.Parse(Session["returnId"].ToString());
                
                int count = warebll.getCount(singleHeadId);
                int monId;
                if (count > 0)
                {
                    monId = count + 1;
                }
                else
                {
                    monId = 1;
                }
                string ISBN = Request["addISBN"];
                int Num = int.Parse(Request["addNum"].ToString());
                string Price = Request["addPrice"];
                string Discount = Request["addDiscount"];
                string TotalPrice = Request["addTotalPrice"];
                string Ocean = Request["addOcean"];
                string shelfId = Request["shelfId"];
                int type = 2;
                Monomers mon = new Monomers();
                mon.SingleHeadId.SingleHeadId = singleHeadId;
                mon.MonomersId = monId;
                mon.Number = Num;
                mon.UPrice.Price = int.Parse(Price);
                mon.Discount = int.Parse(Discount);
                mon.Isbn.Isbn = ISBN;
                mon.TotalPrice = int.Parse(TotalPrice);
                mon.RealPrice = int.Parse(Ocean);
                mon.GoodsShelvesId.GoodsShelvesId = int.Parse(shelfId);
                mon.Type = type;
               Result reslt =  warebll.insert(mon);
                if (reslt==Result.添加成功)
                {
                    Response.Write("添加成功");
                    Response.End();
                } else
                {
                    Response.Write("添加失败");
                    Response.End();
                }
            }
        }
    }
}