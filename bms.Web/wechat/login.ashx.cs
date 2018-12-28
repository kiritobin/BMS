using bms.Bll;
using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
            if (op == "login")
            {
                Login(context);
            }

        }

        private void Login(HttpContext context)
        {
            loginmsg logs = new loginmsg();
            string account = context.Request["userName"];
            OpResult row = userBll.isCustomer(account);
            if (row == OpResult.记录不存在)
            {
                logs.msg = "该账号不存在";
                string json = JsonHelper.JsonSerializerBySingleData(logs);
                context.Response.Write(json);
                context.Response.End();
            }
            else
            {
                Customer custom = loginBll.getPwdByCustomId(account);
                string customID = custom.CustomerId.ToString();
                OpResult res = userBll.CustomersaletaskIsNull(customID);
                if (custom.CustomerId.ToString() == account && res == OpResult.记录存在)
                {
                    string type = context.Request["type"];
                    DataSet ds = userBll.getCustomersaletaskID(customID);
                    if (ds.Tables[0].Rows.Count > 1 && type != "Confirm")
                    {
                        //checked saleTaskId,startTime,regionId,regionName
                        DataTable dt = new DataTable();
                        dt.Columns.Add("saleTaskId", typeof(string));
                        dt.Columns.Add("name", typeof(string));
                        dt.Columns.Add("checked", typeof(string));
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            dt.Rows.Add(ds.Tables[0].Rows[i]["saleTaskId"].ToString(), ds.Tables[0].Rows[i]["regionName"].ToString(), "");
                        }
                        logs.customID = customID;
                        logs.msgds = JsonHelper.ToJson(dt);
                        logs.msg = "多个计划";
                        string json = JsonHelper.JsonSerializerBySingleData(logs);
                        context.Response.Write(json);
                        context.Response.End();
                    }
                    else if (ds.Tables[0].Rows.Count > 1 && type == "Confirm")
                    {
                        string saletaskId = context.Request["saleTaskId"];
                        DataSet saleTaskds = userBll.getsaletasktime(saletaskId);
                        string saletaskID = saleTaskds.Tables[0].Rows[0]["saleTaskId"].ToString();
                        DateTime starTime = Convert.ToDateTime(saleTaskds.Tables[0].Rows[0]["startTime"].ToString());
                        DateTime nowTime = DateTime.Now;
                        TimeSpan timespan = nowTime - starTime;
                        int days = timespan.Days;
                        if (days > 3)
                        {
                            logs.msg = "您上次的销售计划还未结束，请联系工作人员";
                            string json = JsonHelper.JsonSerializerBySingleData(logs);
                            context.Response.Write(json);
                            context.Response.End();
                        }
                        else
                        {
                            logs.saletaskID = saletaskID;
                            logs.customID = customID;
                            logs.msg = "登录成功";
                            string json = JsonHelper.JsonSerializerBySingleData(logs);
                            context.Response.Write(json);
                            context.Response.End();
                        }
                    }
                    else
                    {
                        string saletaskID = ds.Tables[0].Rows[0]["saleTaskId"].ToString();
                        DateTime starTime = Convert.ToDateTime(ds.Tables[0].Rows[0]["startTime"].ToString());
                        DateTime nowTime = DateTime.Now;
                        TimeSpan timespan = nowTime - starTime;
                        int days = timespan.Days;
                        if (days > 3)
                        {
                            logs.msg = "您上次的销售计划还未结束，请联系工作人员";
                            string json = JsonHelper.JsonSerializerBySingleData(logs);
                            context.Response.Write(json);
                            context.Response.End();
                        }
                        else
                        {
                            logs.saletaskID = saletaskID;
                            logs.customID = customID;
                            logs.msg = "登录成功";
                            string json = JsonHelper.JsonSerializerBySingleData(logs);
                            context.Response.Write(json);
                            context.Response.End();
                        }
                    }

                }
                else if (custom.CustomerId.ToString() == account && res == OpResult.记录不存在)
                {
                    logs.msg = "此次展会您还未创建销售计划";
                    string json = JsonHelper.JsonSerializerBySingleData(logs);
                    context.Response.Write(json);
                    context.Response.End();

                }
                else
                {
                    logs.msg = "登录失败";
                    string json = JsonHelper.JsonSerializerBySingleData(logs);
                    context.Response.Write(json);
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