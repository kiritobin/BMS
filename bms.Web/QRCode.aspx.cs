using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThoughtWorks.QRCode.Codec;

namespace bms.Web
{
    public partial class QRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string qrdata = Request.QueryString["qrtext"];
            QRCodeEncoder encoder = new QRCodeEncoder();
            encoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//编码方式(注意：BYTE能支持中文，ALPHA_NUMERIC扫描出来的都是数字)
            encoder.QRCodeScale = 4;//大小(值越大生成的二维码图片像素越高)
            encoder.QRCodeVersion = 0;//版本(注意：设置为0主要是防止编码的字符串太长时发生错误)
            encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//错误效验、错误更正(有4个等级)
            encoder.QRCodeBackgroundColor = Color.White;
            encoder.QRCodeForegroundColor = Color.Black;

            Bitmap bcodeBitmap = encoder.Encode(qrdata.ToString());

            //清除该页输出缓存，设置该页无缓存 
            Response.Buffer = true;
            Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
            Response.AppendHeader("Pragma", "No-Cache");
            //将验证码图片写入内存流，并将其以 "image/Png" 格式输出 
            MemoryStream ms = new MemoryStream();
            try
            {
                bcodeBitmap.Save(ms, ImageFormat.Png);
                Response.ClearContent();
                Response.ContentType = "image/Png";
                Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                //显式释放资源 
                bcodeBitmap.Dispose();
                bcodeBitmap.Dispose();
            }
        }
    }
}