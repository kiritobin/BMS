using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using static bms.Bll.Enums;

namespace bms.Web.wechat
{
    /// <summary>
    /// test 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
     [System.Web.Script.Services.ScriptService]
    public class test : System.Web.Services.WebService
    {
        LoginBll loginBll = new LoginBll();
        CustomerBll ctBll = new CustomerBll();
        UserBll userBll = new UserBll();
        
        [WebMethod]
        public string Login(string account)
        {
            OpResult row = userBll.isCustomer(account);
            if (row == OpResult.记录不存在)
            {
                return "该账户不存在";
            }
            else
            {
                Customer custom = loginBll.getPwdByCustomId(account);
                string customID = custom.CustomerId.ToString();
                OpResult res = userBll.CustomersaletaskIsNull(customID);
                if (custom.CustomerId.ToString() == account && res == OpResult.记录存在)
                {
                    return "登录成功";

                }
                else if (custom.CustomerId.ToString() == account && res == OpResult.记录不存在)
                {
                    return "您还未创建销售计划";

                }
                else
                {
                    return "登录失败";
                }
            }
        }
    }
}
