using bms.Bll;
using bms.DBHelper;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace bms.Web.wechat
{
    /// <summary>
    /// retailManagement1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
     [System.Web.Script.Services.ScriptService]
    public class retailManagement1 : System.Web.Services.WebService
    {
        public int totalCount, intPageCount, pageSize = 15;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string Load(int currentPage)
        {
            Page page = new Page();
            RetailBll retailbll = new RetailBll();
            //获取分页数据
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            TableBuilder tb = new TableBuilder();
            tb.StrTable = "T_RetailHead";
            tb.OrderBy = "retailHeadId";
            tb.StrColumnlist = "retailHeadId,kindsNum,number,allTotalPrice,allRealPrice,payment";
            tb.IntPageSize = pageSize;
            tb.IntPageNum = currentPage;
            tb.StrWhere = "deleteState=0 and (state=0 or state=1) and retailHeadId='LS20181026000001'";
            //获取展示的客户数据
            DataSet ds = retailbll.selectBypage(tb, out totalCount, out intPageCount);
            int aaa = ds.Tables[0].Rows.Count;
            string data = JsonHelper.ToJson(ds.Tables[0], "retail");
            page.data = data;
            page.totalCount = totalCount;
            page.intPageCount = intPageCount;
            string json = JsonHelper.JsonSerializerBySingleData(page);
            return json;
        }
    }
}
