using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.AccessMGT
{
    using System.Data;
    using Result = Enums.OpResult;
    public partial class functionManagement : System.Web.UI.Page
    {
        public int currentPage = 1, pageSize = 5, getCurrentPage = 0, totalCount, intPageCount;
        public DataSet ds;
        public string search, strSearch;
        FunctionBll functionbll = new FunctionBll();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getData("");
            }
            //查询操作
            string type = Request.QueryString["type"];
            if (type == "search")
            {
                Search();
                getData(Search());
            }
            string op = Request["op"];
            if (op == "del")
            {
                int functionId = int.Parse(Context.Request["functionId"]);
                Result row = isDelete();
                if (row == Result.记录不存在)
                {
                    Result result = functionbll.Delete(functionId);
                    if (result==Result.删除成功)
                    {
                        Response.Write("删除成功");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("删除失败");
                        Response.End();
                    }

                }
                else
                {
                    Response.Write("在其它表里关联引用，不能删除");
                    Response.End();
                }
            }
            if (op == "add")
            {
                string functionName = Context.Request["functionName"];
                Function func = new Function();
                func.FunctionName = functionName;
                if (functionbll.Insert(func) == Result.添加成功)
                {
                    Response.Write("添加成功");
                    Response.End();
                }
                else
                {
                    Response.Write("添加失败");
                }
                
            }
        }
        public void getData(string strWhere)
        {
            currentPage = Convert.ToInt32(Request.QueryString["currentPage"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            //获取分页数据
            TableBuilder tabBuilder = new TableBuilder();
            tabBuilder.StrTable = "T_Function";
            tabBuilder.OrderBy = "functionId";
            tabBuilder.StrColumnlist = "functionId,functionName";
            tabBuilder.IntPageSize = pageSize;
            tabBuilder.StrWhere = strWhere;
            tabBuilder.IntPageNum = currentPage;
            getCurrentPage = currentPage;
            //获取展示数据
            ds = functionbll.selectByPage(tabBuilder, out totalCount, out intPageCount);
        }
        protected string Search()
        {
            try
            {
                search = Request.QueryString["search"];
                strSearch = Request.QueryString["search"];
                if (search.Length == 0)
                {
                    search = "";
                }
                else if (search == null)
                {
                    search = null;
                }
                else
                {
                    strSearch = search;
                    search = String.Format("functionName {0}", "like'%" + search + "%'");
                }
            }
            catch
            { }
            return search;
        }
        public Result isDelete()
        {
            string functionId = Context.Request["functionId"];
            Result row;
            if (functionbll.isDelete("T_Role", "functionId", functionId) == Result.关联引用)
            {
                row = Result.关联引用;
            }
            else
            {
                row = Result.记录不存在;
            }
            return row;
        }
    }

}