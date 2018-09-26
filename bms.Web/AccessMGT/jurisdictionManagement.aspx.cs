using bms.Bll;
using bms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.AccessMGT
{
    using System.Text;
    using Result = Enums.OpResult;
    public partial class JurisdictionManagement : CommonPage
    {
        public int totalCount, intPageCount;
        public DataSet ds;
        public string search, strSearch;
        FunctionBll functionbll = new FunctionBll();

        protected void Page_Load(object sender, EventArgs e)
        {

            getData();
            string op = Request["op"];
            if (op == "del")
            {
                int functionId = int.Parse(Context.Request["functionId"]);
                Result row = isDelete();
                if (row == Result.记录不存在)
                {
                    Result result = functionbll.Delete(functionId);
                    if (result == Result.删除成功)
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
        public string getData()
        {
            int currentPage = Convert.ToInt32(Request["page"]);
            if (currentPage == 0)
            {
                currentPage = 1;
            }
            string search = Request["search"];

            if (search == "" || search == null)
            {
                search = "";
            }
            else
            {
                search = String.Format("functionName {0}", "like'%" + search + "%'");
            }
            //获取分页数据
            TableBuilder tabBuilder = new TableBuilder();
            tabBuilder.StrTable = "T_Function";
            tabBuilder.OrderBy = "functionId";
            tabBuilder.StrColumnlist = "functionId,functionName";
            tabBuilder.IntPageSize = 3;
            tabBuilder.StrWhere = search;
            tabBuilder.IntPageNum = currentPage;
            //获取展示数据
            ds = functionbll.selectByPage(tabBuilder, out totalCount, out intPageCount);

            StringBuilder strb = new StringBuilder();
            strb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                strb.Append("<tr><td>" + (i + 1 + ((currentPage - 1) * 3)) + "</td>");
                strb.Append("<td>" + ds.Tables[0].Rows[i]["functionName"].ToString() + "</td>");
                strb.Append("<td>" + "<input type='hidden' class='functionId' value='" + ds.Tables[0].Rows[i]["functionId"].ToString() + "'/>" + "<button class='btn btn-danger btn-sm btn-delete'>" + "<i class='fa fa-trash-o fa-lg'></i>" + "&nbsp 删除" + "</button>" + " </td></ tr >");

            }
            strb.Append("</tbody>");
            strb.Append("<input type='hidden' value=' " + intPageCount + " ' id='intPageCount' />");
            string op = Request["op"];
            if (op=="paging")
            {
                Response.Write(strb.ToString());
                Response.End();
            }
            return strb.ToString();
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