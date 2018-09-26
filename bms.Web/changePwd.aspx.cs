using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static bms.Bll.Enums;

namespace bms.Web
{
    using Result = Enums.OpResult;
    public partial class changePwd : System.Web.UI.Page
    {

        UserBll userbll = new UserBll();
        RSACryptoService rsa = new RSACryptoService();
        protected void Page_Load(object sender, EventArgs e)
        {
            string op = Request["op"];
            if (op == "change")
            {
                User user = (User)Session["user"];
                int userId = user.UserId;
                string Oldpwd = rsa.Decrypt(user.Pwd);

                string oldpwd = Request["oldpwd"];

                if (Oldpwd.Equals(oldpwd))
                {
                    string newpwd = Request["newpwd"];
                    User newUser = new User();

                    newUser.UserId = userId;
                    newUser.Pwd = rsa.Encrypt(newpwd);
                    Result result = userbll.UpdatePwd(newUser);
                    if (result == Result.更新成功)
                    {
                        Response.Write("修改成功");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("修改失败");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("旧密码不匹配");
                    Response.End();
                }
            }
        }
    }
}