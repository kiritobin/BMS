using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using System.Data;
using bms.Model;

namespace bms.Web.CustomerMGT
{
    using Result = Enums.OpResult;
    public partial class statistics : System.Web.UI.Page
    {
        public DataSet regionds;
        public string operatorId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                User user = (User)Session["user"];
                operatorId = user.UserId.ToString();
            }
            RegionBll regBll = new RegionBll();
            ConfigurationBll cBll = new ConfigurationBll();
            Result result;
            regionds = regBll.select();
            string op = Request["op"];
            if (op == "sure")
            {
                string startTime = Request["startDt"].ToString(),
                endTime = Request["endDt"].ToString(),
                regionName = Request["regName"].ToString();
                DateTime st = DateTime.Parse(startTime);
                DateTime et = DateTime.Parse(endTime);
                result = cBll.isExist(regionName);
                if (result == Result.记录不存在)
                {
                    result = cBll.Insert(st, et, regionName);
                    if(result == Result.添加成功)
                    {
                        Response.Write("添加成功");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("添加失败");
                        Response.End();
                    }
                }
                else
                {
                    result = cBll.Update(st, et, regionName);
                    if(result == Result.更新成功)
                    {
                        Response.Write("更新成功");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("更新失败");
                        Response.End();
                    }
                }
            }
            if (op == "goScreen")
            {
                string regionName = Request["regName"].ToString();
                if (regionName == "选择组织" || regionName == null)
                {
                    User user = (User)Session["user"];
                    regionName = user.ReginId.RegionName;
                }
                Session["regionName"] = regionName;
                result = cBll.isExist(regionName);
                if (result == Result.记录不存在)
                {
                    Response.Write("记录不存在");
                    Response.End();
                }
                else
                {
                    Response.Write("记录存在");
                    Response.End();
                }
            }
        }
    }
}