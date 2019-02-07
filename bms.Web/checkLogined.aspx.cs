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
                string cry = RSACryptoService.DecryptByAES(sessionId);
                if (cry != "cancel")
                {
                    Hashtable hOnline = (Hashtable)Application["Online"];
                    if (hOnline != null)
                    {
                        IDictionaryEnumerator idE = hOnline.GetEnumerator();
                        while (idE.MoveNext())
                        {
                            if (idE.Key != null && idE.Key.ToString().Equals(cry))
                            {
                                if (idE.Value != null && "Offline".Equals(idE.Value.ToString()))
                                {
                                    hOnline.Remove(cry);
                                    Application.Lock();
                                    Application["Online"] = hOnline;
                                    Application.UnLock();

                                    Session.Abandon();
                                    s = "已登录";
                                    Response.Write(s);
                                    Response.End();
                                    return;
                                }
                                break;
                            }
                        }

                    }
                }
                else
                {
                    s = "已失效";
                }
                Response.Write(s);
                Response.End();
            }
        }
    }
}