$(document).ready(function () {
    $("#submit").click(function () {
        var pubKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCAnNXR7lHTpPH/97QOxIp+UusK9/RH5elvEPv6ssL37xGo8vQHh7CCsOonUWWVdi1iVegi7fRCkWeUVlta61EuX141+eKnZcdJe81NeUZ1h3N77JbzElbhhi8Wln6U27xpfkskKASLhQ4dS9DqoJQN/YUhBaBpER287Wjf3X6WmQIDAQAB";
        var encrypt = new JSEncrypt();
        encrypt.setPublicKey(pubKey);

        var userName = $("#userName").val();
        var pwd = $("#userPwd").val();
        var user = $('input[name="user"]:checked').val();
        if (userName == "") {
            alert("用户名不能为空");
        }
        else if (pwd == "") {
            alert("密码不能为空");
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'login.aspx',
                data: {
                    userName: userName,
                    pwd: encrypt.encrypt(pwd),
                    user:user,
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
    });
});