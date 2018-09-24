using bms.Bll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.CustomerMGT
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void data()
        {
            LibraryCollectionBll libraryCollectionBll = new LibraryCollectionBll();
            DataTable dt = libraryCollectionBll.Select("20001");
            GridView1.DataSource = dt;
            GridView1.DataBind();
            string value = "",values = "";
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            data();
        }
    }
}