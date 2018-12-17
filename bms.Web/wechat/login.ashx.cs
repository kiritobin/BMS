using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static bms.Bll.Enums;

namespace bms.Web.wechat
{
    /// <summary>
    /// login 的摘要说明
    /// </summary>
    public class login : IHttpHandler
    {
        LoginBll loginBll = new LoginBll();
        CustomerBll ctBll = new CustomerBll();
        UserBll userBll = new UserBll();
        public void ProcessRequest(HttpContext context)
        {
            string op = context.Request["op"];
            if (op=="login")
            {
                Login(context);
            }
            
        }

        private void Login(HttpContext context)
        {
            string account = context.Request["userName"];
            OpResult row = userBll.isCustomer(account);
            if (row == OpResult.记录不存在)
            {
                context.Response.Write("该账号不存在");
                context.Response.End();
            }
            else
            {
                Customer custom = loginBll.getPwdByCustomId(account);
                string customID = custom.CustomerId.ToString();
                OpResult res = userBll.CustomersaletaskIsNull(customID);
                if (custom.CustomerId.ToString() == account && res==OpResult.记录存在)
                {
                    context.Response.Write("登录成功");
                    context.Response.End();

                }
                else if (custom.CustomerId.ToString() == account && res == OpResult.记录不存在)
                {
                    context.Response.Write("您还未创建销售计划");
                    context.Response.End();

                }
                else
                {
                    context.Response.Write("登录失败");
                    context.Response.End();
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}