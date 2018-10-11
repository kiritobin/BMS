<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="bms.Web.CustomerMGT.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        //点击图片触发更换验证码图片的事件
        function refreshCode() {
            var code = document.getElementById("code");
            //code.src = "qrcode.aspx?id=" + Math.random();
            code.src = "qrcode.aspx?qrtext=" + Math.random();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        </div>
        <div>
            <img src="qrcode.aspx" id="code" onclick="refreshCode();" />
            <a href="javascript:void(0)" onclick="refreshCode()">换一张</a>
        </div>
    </form>
</body>
</html>
