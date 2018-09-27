using bms.Bll;
using bms.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web
{
    using Result = Enums.OpResult;
    public partial class login : System.Web.UI.Page
    {
        LoginBll loginBll = new LoginBll();
        RSACryptoService rsa = new RSACryptoService();

        //单点登录判断
        //private void isLogined(string id)
        //{
        //    Hashtable hOnline = (Hashtable)Application["Online"];
        //    if (hOnline != null)
        //    {
        //        int i = 0;
        //        while (i < hOnline.Count)
        //        {
        //            IDictionaryEnumerator idE = hOnline.GetEnumerator();
        //            string strKey = "";
        //            while (idE.MoveNext())
        //            {
        //                if (idE.Value != null && idE.Value.ToString().Equals(id))
        //                {
        //                    //already login              
        //                    strKey = idE.Key.ToString();
        //                    hOnline[strKey] = "XXXXXX";
        //                    break;
        //                }
        //            }
        //            i = i + 1;
        //        }
        //    }
        //    else
        //    {
        //        hOnline = new Hashtable();
        //    }
        //    hOnline[Session.SessionID] = id;
        //    Application.Lock();
        //    Application["Online"] = hOnline;
        //    Application.UnLock();
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            UserBll userBll = new UserBll();
            string op = Request["op"];
            string userType = Request["user"];
            if (op == "login")
            {
                string account = Request["userName"];
                string pwd = rsa.Decrypt(Request["pwd"]);
                User user = loginBll.getPwdByUserId(account);
                string userPwd = rsa.Decrypt(user.Pwd);
                Result row = userBll.SelectDeleteState(int.Parse(account));
                if (row == Result.记录不存在)
                {
                    Response.Write("该账号不存在");
                    Response.End();
                }
                else
                {
                    if (user.UserId.ToString() == account && userPwd == pwd)
                    {
                        Session["user"] = user;
                        Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                        FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, account, DateTime.Now, DateTime.Now.AddMinutes(30), true, "staff"); //建立身份验证票对象 
                        string HashTicket = FormsAuthentication.Encrypt(Ticket); //加密序列化验证票为字符串 
                        Session["HashTicket"] = HashTicket;
                        HttpCookie UserCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket); //生成Cookie 
                        Context.Response.Cookies.Add(UserCookie); //票据写入Cookie
                                                                  //isLogined(account);
                        Response.Write("登录成功");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("登录失败");
                        Response.End();
                    }
                }
            }
        }
    }
}