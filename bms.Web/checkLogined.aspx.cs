using bms.Bll;
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
    public partial class checkLogined : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = "";
            string sessionId = Request["sid"];
            if (sessionId==null||sessionId=="")
            {
                Hashtable hOnline = (Hashtable)Application["Online"];
                string ss = Session.SessionID;
                if (hOnline != null)
                {
                    IDictionaryEnumerator idE = hOnline.GetEnumerator();
                    while (idE.MoveNext())
                    {
                        if (idE.Key != null && idE.Key.ToString().Equals(Session.SessionID))
                        {
                            if (idE.Value != null && "Offline".Equals(idE.Value.ToString()))
                            {
                                hOnline.Remove(Session.SessionID);
                                Application.Lock();
                                Application["Online"] = hOnline;
                                Application.UnLock();
                                //删除身份凭证
                                FormsAuthentication.SignOut();
                                //设置Cookie的值为空
                                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                                //设置Cookie的过期时间为上个月今天
                                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
                                Session.Abandon();
                                s = "已登录";
                                Response.Write(s);
                                Response.End();
                                return;
                            }
                            break;
                        }
                    }
                    Response.Write(s);
                    Response.End();
                }
            }
            else
            {
                sessionId = RSACryptoService.DecryptByAES(sessionId);
                Hashtable hOnline = (Hashtable)Application["Online"];
                if (hOnline != null)
                {
                    IDictionaryEnumerator idE = hOnline.GetEnumerator();
                    while (idE.MoveNext())
                    {
                        if (idE.Key != null && idE.Key.ToString().Equals(sessionId))
                        {
                            if (idE.Value != null && "Offline".Equals(idE.Value.ToString()))
                            {
                                hOnline.Remove(sessionId);
                                Application.Lock();
                                Application["Online"] = hOnline;
                                Application.UnLock();
                                //删除身份凭证
                                FormsAuthentication.SignOut();
                                //设置Cookie的值为空
                                Response.Cookies[FormsAuthentication.FormsCookieName].Value = null;
                                //设置Cookie的过期时间为上个月今天
                                Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddMonths(-1);
                                Session.Abandon();
                                s = "已登录";
                                Response.Write(s);
                                Response.End();
                                return;
                            }
                            break;
                        }
                    }
                    Response.Write(s);
                    Response.End();
                }
            }
            
        }
    }
}