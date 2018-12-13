using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bms.Web.CustomerMGT
{
    public partial class uploadCollection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpFileCollection files = Request.Files;
            string msg = string.Empty;
            string error = string.Empty;
            if (files.Count > 0)
            {
                string director = Server.MapPath("/uploads/"); //上传目录
                if (!Directory.Exists(director))
                {
                    Directory.CreateDirectory(director);
                }
                string now = DateTime.Now.ToString("yyyyMMddHHmmss");
                string path = director + Path.GetFileName(now + "-" + files[0].FileName.Replace(" ", ""));
                if (File.Exists(path))
                {
                    msg = "上传失败，文件存在";
                }
                if ((files[0].ContentLength/1000) > 1024000) //1G大小
                {
                    msg = "文件大小超过限制";
                }
                else
                {
                    files[0].SaveAs(path);
                    //返回json数据
                    Session["path"] = path;
                    msg = "上传成功";
                }
                string res = "{ error:'" + error + "', msg:'" + msg + "'}";
                Response.Write(res);
                Response.End();
            }
        }
    }
}