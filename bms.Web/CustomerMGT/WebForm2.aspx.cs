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
        private String ToSBC(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new String(c);
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
                value = "(" + dt.Rows[i]["ISBN"].ToString() + "," + ToSBC(dt.Rows[i]["bookName"].ToString()) + "," + dt.Rows[i]["price"].ToString() + "," + dt.Rows[i]["collectionNum"].ToString() + "," + dt.Rows[i]["customerId"].ToString() + "),";
                values = values + value;
            }
            values = values.Substring(0, values.Length - 1);


            Response.Write(values);
            Response.End();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            data();
        }
    }
}