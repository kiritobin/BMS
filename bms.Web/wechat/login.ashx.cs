using bms.Bll;
using bms.DBHelper;
using bms.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.wechat
{
    using System.Collections;
    using System.Web.Security;
    using System.Web.SessionState;
    using Result = Enums.OpResult;
    /// <summary>
    /// login 的摘要说明
    /// </summary>
    public class login : IHttpHandler, IRequiresSessionState
    {
        LoginBll loginBll = new LoginBll();
        RSACryptoService rsa = new RSACryptoService();
        CustomerBll ctBll = new CustomerBll();
        UserBll userBll = new UserBll();

        //单点登录判断
        public static void isLogined(string userId ,HttpContext httpContext)
        {
            Hashtable hOnline = (Hashtable)httpContext.Application["Online"];
            if (hOnline != null)
            {
                int i = 0;
                while (i < hOnline.Count)
                {
                    IDictionaryEnumerator idE = hOnline.GetEnumerator();
                    string strKey = "";
                    while (idE.MoveNext())
                    {
                        if (idE.Value != null && idE.Value.ToString().Equals(userId))
                        {
                            strKey = idE.Key.ToString();
                            hOnline[strKey] = "Offline";
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
            hOnline[httpContext.Session.SessionID] = userId;
            httpContext.Application.Lock();
            httpContext.Application["Online"] = hOnline;
            httpContext.Application.UnLock();
        }

    public void ProcessRequest(HttpContext context)
        {
            string op = context.Request["op"];
            if (op == "login")
            {
                userlogin(context);
            }

        }

        private void userlogin(HttpContext context) {

            string account = context.Request["userName"];
            string rePwd = context.Request["pwd"];
            string pwd = RSACryptoService.DecryptByAES(rePwd);
            User user = loginBll.getPwdByUserId(account);
            string userPwd = rsa.Decrypt(user.Pwd);
            Result row = userBll.isUser(account);
            if (row == Result.记录不存在)
            {
                context.Response.Write("该账号不存在");
                context.Response.End();
            }
            else
            {
                loginmsg logs = new loginmsg();
                if (user.UserId.ToString() == account && userPwd == pwd)
                {
                    context.Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                    FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, account, DateTime.Now, DateTime.Now.AddDays(1), true, "staff"); //建立身份验证票对象 
                    string HashTicket = FormsAuthentication.Encrypt(Ticket); //加密序列化验证票为字符串 
                    HttpCookie UserCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket); //生成Cookie 
                    context.Response.Cookies.Add(UserCookie); //票据写入Cookie
                    isLogined(account, context);

                    logs.sid = RSACryptoService.EncryptByAES(context.Session.SessionID);
                    logs.msg = "登录成功";
                    logs.customID = user.UserId.ToString();
                    string json = JsonHelper.JsonSerializerBySingleData(logs);
                    context.Response.Write(json);
                    context.Response.End();
                }
                else
                {
                    logs.msg = "密码错误";
                    string json = JsonHelper.JsonSerializerBySingleData(logs);
                    context.Response.Write(json);
                    context.Response.End();
                }
            }
        }

        ////单点登录判断
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

        //private void Login(HttpContext context)
        //{
        //    loginmsg logs = new loginmsg();
        //    string account = context.Request["userName"];
        //    OpResult row = userBll.isCustomer(account);
        //    if (row == OpResult.记录不存在)
        //    {
        //        logs.msg = "该账号不存在";
        //        string json = JsonHelper.JsonSerializerBySingleData(logs);
        //        context.Response.Write(json);
        //        context.Response.End();
        //    }
        //    else
        //    {
        //        Customer custom = loginBll.getPwdByCustomId(account);
        //        string customID = custom.CustomerId.ToString();
        //        OpResult res = userBll.CustomersaletaskIsNull(customID);
        //        if (custom.CustomerId.ToString() == account && res == OpResult.记录存在)
        //        {
        //            string type = context.Request["type"];
        //            DataSet ds = userBll.getCustomersaletaskID(customID);
        //            if (ds.Tables[0].Rows.Count > 1 && type != "Confirm")
        //            {
        //                //checked saleTaskId,startTime,regionId,regionName
        //                DataTable dt = new DataTable();
        //                dt.Columns.Add("saleTaskId", typeof(string));
        //                dt.Columns.Add("name", typeof(string));
        //                dt.Columns.Add("checked", typeof(string));
        //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //                {
        //                    dt.Rows.Add(ds.Tables[0].Rows[i]["saleTaskId"].ToString(), ds.Tables[0].Rows[i]["regionName"].ToString(), "");
        //                }
        //                logs.customID = customID;
        //                logs.msgds = JsonHelper.ToJson(dt);
        //                logs.msg = "多个计划";
        //                string json = JsonHelper.JsonSerializerBySingleData(logs);
        //                context.Response.Write(json);
        //                context.Response.End();
        //            }
        //            else if (ds.Tables[0].Rows.Count > 1 && type == "Confirm")
        //            {
        //                string saletaskId = context.Request["saleTaskId"];
        //                DataSet saleTaskds = userBll.getsaletasktime(saletaskId);
        //                string saletaskID = saleTaskds.Tables[0].Rows[0]["saleTaskId"].ToString();
        //                //DateTime starTime = Convert.ToDateTime(saleTaskds.Tables[0].Rows[0]["startTime"].ToString());
        //                //DateTime nowTime = DateTime.Now;
        //                //TimeSpan timespan = nowTime - starTime;
        //                //int days = timespan.Days;
        //                //if (days > 3)
        //                //{
        //                //    logs.msg = "您上次的销售计划还未结束，请联系工作人员";
        //                //    string json = JsonHelper.JsonSerializerBySingleData(logs);
        //                //    context.Response.Write(json);
        //                //    context.Response.End();
        //                //}
        //                //else
        //                //{
        //                    logs.saletaskID = saletaskID;
        //                    logs.customID = customID;
        //                    logs.msg = "登录成功";
        //                    string json = JsonHelper.JsonSerializerBySingleData(logs);
        //                    context.Response.Write(json);
        //                    context.Response.End();
        //                //}
        //            }
        //            else
        //            {
        //                string saletaskID = ds.Tables[0].Rows[0]["saleTaskId"].ToString();
        //                //DateTime starTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["startTime"].ToString());
        //                //DateTime nowTime = DateTime.Now;
        //                //TimeSpan timespan = nowTime - starTime;
        //                //int days = timespan.Days;
        //                //if (days > 3)
        //                //{
        //                //    logs.msg = "您上次的销售计划还未结束，请联系工作人员";
        //                //    string json = JsonHelper.JsonSerializerBySingleData(logs);
        //                //    context.Response.Write(json);
        //                //    context.Response.End();
        //                //}
        //                //else
        //                //{
        //                    logs.saletaskID = saletaskID;
        //                    logs.customID = customID;
        //                    logs.msg = "登录成功";
        //                    string json = JsonHelper.JsonSerializerBySingleData(logs);
        //                    context.Response.Write(json);
        //                    context.Response.End();
        //               // }
        //            }

        //        }
        //        else if (custom.CustomerId.ToString() == account && res == OpResult.记录不存在)
        //        {
        //            logs.msg = "此次展会您还未创建销售计划";
        //            string json = JsonHelper.JsonSerializerBySingleData(logs);
        //            context.Response.Write(json);
        //            context.Response.End();

        //        }
        //        else
        //        {
        //            logs.msg = "登录失败";
        //            string json = JsonHelper.JsonSerializerBySingleData(logs);
        //            context.Response.Write(json);
        //            context.Response.End();
        //        }
        //    }
        //}

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}