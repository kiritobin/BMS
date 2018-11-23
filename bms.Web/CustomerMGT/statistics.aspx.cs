using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Bll;
using System.Data;

namespace bms.Web.CustomerMGT
{
    using Result = Enums.OpResult;
    public partial class statistics : System.Web.UI.Page
    {
        public DataSet regionds;
        protected void Page_Load(object sender, EventArgs e)
        {
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
        }
    }
}