﻿using bms.Bll;
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
    public partial class customerLogin : System.Web.UI.Page
    {
        //单点登录判断
        LoginBll loginBll = new LoginBll();
        CustomerBll ctBll = new CustomerBll();
        UserBll userBll = new UserBll();
        private void isLogined(string id)
        {
            Hashtable hOnline = (Hashtable)Application["Online"];
            if (hOnline != null)
            {
                int i = 0;
                while (i < hOnline.Count)
                {
                    IDictionaryEnumerator idE = hOnline.GetEnumerator();
                    string strKey = "";
                    while (idE.MoveNext())
                    {
                        if (idE.Value != null && idE.Value.ToString().Equals(id))
                        {
                            //already login              
                            strKey = idE.Key.ToString();
                            hOnline[strKey] = "XXXXXX";
                            break;
                        }
                    }
                    i = i + 1;
                }
            }
            else
            {
                hOnline = new Hashtable();
            }
            hOnline[Session.SessionID] = id;
            Application.Lock();
            Application["Online"] = hOnline;
            Application.UnLock();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string op = Request["op"];
            if (op=="login")
            {
                string account = Request["userName"];
                Result row = userBll.isCustomer(account);
                if (row == Result.记录不存在)
                {
                    Response.Write("该账号不存在");
                    Response.End();
                }
                else
                {
                    Customer custom = loginBll.getPwdByCustomId(account);
                    if (custom.CustomerId.ToString() == account)
                    {
                        Session.Timeout = 1440;
                        Session["custom"] = custom;
                        Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                        FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, account, DateTime.Now, DateTime.Now.AddDays(1), true, "customer"); //建立身份验证票对象 
                        string HashTicket = FormsAuthentication.Encrypt(Ticket); //加密序列化验证票为字符串 
                        //Session["HashTicket"] = HashTicket;
                        HttpCookie UserCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket); //生成Cookie 
                        Context.Response.Cookies.Add(UserCookie); //票据写入Cookie
                        isLogined(account);
                        Response.Write("登录成功");
                        Response.End();

                    }
                    else
                    {
                        Response.Write("登录失败");
                        Response.End();
                    }
                }
            };
        }
    }
}