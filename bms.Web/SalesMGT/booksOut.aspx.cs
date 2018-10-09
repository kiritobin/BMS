﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using bms.Model;
using bms.Bll;
using System.Data;
using System.Text;

namespace bms.Web.SalesMGT
{
    public partial class booksOut : System.Web.UI.Page
    {
        SaleMonomerBll smBll = new SaleMonomerBll();
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetData();
        }
        public String GetData()
        {
            ds = smBll.SelectBookRanking();
            StringBuilder sb = new StringBuilder();
            sb.Append("<tbody>");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + (i+1) + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["bookName"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["unitPrice"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["allNum"].ToString() + "</td>");
                sb.Append("<td>" + ds.Tables[0].Rows[i]["allTotalPrice"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            return sb.ToString();
        }
    }
}