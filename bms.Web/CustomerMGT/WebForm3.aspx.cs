using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.CustomerMGT
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ExcelHelper excelHelper = new ExcelHelper();
            string path = @"C:\Users\daobin\Desktop\基础数据.xls";
            DataTable dt = ExcelHelper.GetDataTable(path);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}