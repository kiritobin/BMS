using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.CustomerMGT
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //excel读到table
        private DataTable excelToDt()
        {
            DataTable dt1 = new DataTable();
            string path = @"D:\WeChat\WeChat Files\Mr_zwh_ing\Files\基础数据.xlsx";
            string strConn = "";
            //文件类型判断
            string[] sArray = path.Split('.');
            int count = sArray.Length - 1;
            if (sArray[count] == "xls")
            {
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (sArray[count] == "xlsx")
            {
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string strExcel1 = "select * from [Sheet1$]";
                OleDbDataAdapter oda1 = new OleDbDataAdapter(strExcel1, strConn);
                dt1.Columns.Add("id"); //匹配列，与结构一致
                oda1.Fill(dt1);
                //GetDistinctSelf(dt1, "ISBN", "书名", "单价");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                Response.End();
            }
            finally
            {
                conn.Close();
            }
            return dt1;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            string path = @"D:\WeChat\WeChat Files\Mr_zwh_ing\Files\基础数据.xlsx";
            DataTable dtNpoi = EPPlusHelper.WorksheetToTable(path);
            GridView1.DataSource = dtNpoi;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DataTable dtExcel = excelToDt() ;
            GridView1.DataSource = dtExcel;
            GridView1.DataBind();
        }
    }
}