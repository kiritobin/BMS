<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="bms.Web.CustomerMGT.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <p>
                <input type="file" id="file1" name="file" />
            </p>
            <input type="button" value="上传" id="upload" />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Button" />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Button" />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Button" />
        </div>
        1
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="Button" />
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        2
        <asp:GridView ID="GridView2" runat="server">
        </asp:GridView>
        3
        <asp:GridView ID="GridView3" runat="server">
        </asp:GridView>
    </form>
</body>
<script type="text/javascript" src="https://www.idaobin.com/test/jquery-3.2.1.js"></script>
<script type="text/javascript" src="https://www.idaobin.com/test/ajaxfileupload.js"></script>
<script type="text/javascript">
    $(function () {  //文件类型判断
        $("#upload").click(function () {
            var location = $("input[name='file']").val();
            var point = location.lastIndexOf(".");
            var type = location.substr(point).toLowerCase();
            var uploadFiles = document.getElementById("file1").files;
            if (uploadFiles.length == 0) {
                alert("请选择要上传的文件");
            }
            else if (type == ".xlsx" || type == ".xls") {
                ajaxFileUpload();
                $.ajax({
                    type: 'Post',
                    url: 'WebForm1.aspx',
                    data: {
                        op: "upload"
                    },
                    dataType: 'text',
                    success: function (succ) {

                    }
                });
            }
            else {
                alert("只允许上传.xlsx或者.xls格式的文件");
            }
        });
    });

    function ajaxFileUpload() {
        $.ajaxFileUpload(
            {
                url: '/CustomerMGT/uploadCollection.aspx', //用于文件上传的服务器端请求地址
                secureuri: false, //是否需要安全协议，一般设置为false
                fileElementId: 'file1', //文件上传域的ID
                dataType: 'json', //返回值类型 一般设置为json
                success: function (data, status)  //服务器成功响应处理函数
                {
                    console.log(data.msg);
                    if (typeof (data.error) != 'undefined') {
                        if (data.error != '') {
                            alert(data.error);
                        } else {
                            alert(data.msg);
                        }
                    }
                },
                error: function (data, status, e)//服务器响应失败处理函数
                {
                    alert(e);
                }
            }
        );
        return false;
    }
</script>
</html>
