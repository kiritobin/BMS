$(document).ready(function () {
    $("#submit").click(function () {
        var pubKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCAnNXR7lHTpPH/97QOxIp+UusK9/RH5elvEPv6ssL37xGo8vQHh7CCsOonUWWVdi1iVegi7fRCkWeUVlta61EuX141+eKnZcdJe81NeUZ1h3N77JbzElbhhi8Wln6U27xpfkskKASLhQ4dS9DqoJQN/YUhBaBpER287Wjf3X6WmQIDAQAB";
        var encrypt = new JSEncrypt();
        encrypt.setPublicKey(pubKey);

        var userName = $("#userName").val();
        var pwd = $("#userPwd").val();
        var user = $('input[name="user"]:checked').val();
        if (userName == "") {
            swal({
                title: "温馨提示:)",
                text: "用户名不能为空",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-success",
                type: "warning",
                allowOutsideClick: false
            })
        }
        else if (pwd == "") {
            swal({
                title: "温馨提示:)",
                text: "密码不能为空",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-success",
                type: "warning",
                allowOutsideClick: false
            })
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
                    swal({
                        title: "温馨提示:)",
                        text: "登录成功",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "success",
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.href = "/main.aspx";
                    })
                }
                else {
                    swal({
                        title: "温馨提示:)",
                        text: "登录失败",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "warning",
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.reload();
                    })
                }
            }
            });
        }
    });
});