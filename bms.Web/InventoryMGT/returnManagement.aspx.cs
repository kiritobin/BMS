using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using bms.Model;

namespace bms.Web.BasicInfor
{
    using Result = Enums.OpResult;
    public partial class replenishList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["user"];
            string op = Request["op"];
            if (op == "add")
            {
                string billCount = Request["billCount"];
                string totalPrice = Request["totalPrice"];
                string realPrice = Request["realPrice"];
                SingleHead single = new SingleHead();
                single.AllBillCount = Convert.ToInt32(billCount);
                single.AllRealPrice = Convert.ToInt32(realPrice);
                single.AllTotalPrice = Convert.ToInt32(totalPrice);
                single.Region = user.ReginId;
                single.SingleHeadId = "TH";
                single.Time = DateTime.Now;
                single.Type = 2;
                single.User = user;
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