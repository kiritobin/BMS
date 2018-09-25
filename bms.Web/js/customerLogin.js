$(document).ready(function () {
    $("#submit").click(function () {
        var userName = $("#userName").val();
        if (userName == "") {
            alert("客户名不能为空");
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'customerLogin.aspx',
                data: {
                    userName: userName,
                    op: "login"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ === "登录成功") {
                        window.location.href = "/main.aspx";
                    }
                    else {
                        alert("登录失败");
                    }
                }
            });
        }
    })

})