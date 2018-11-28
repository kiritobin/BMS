$(document).ready(function () {
    //用户名输入框获取焦点
    $("#userName").focus();
    //提交事件
    $("#submit").click(function () {
        var pubKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCAnNXR7lHTpPH/97QOxIp+UusK9/RH5elvEPv6ssL37xGo8vQHh7CCsOonUWWVdi1iVegi7fRCkWeUVlta61EuX141+eKnZcdJe81NeUZ1h3N77JbzElbhhi8Wln6U27xpfkskKASLhQ4dS9DqoJQN/YUhBaBpER287Wjf3X6WmQIDAQAB";
        var encrypt = new JSEncrypt();
        encrypt.setPublicKey(pubKey);

        var userName = $("#userName").val();
        var pwd = $("#userPwd").val();
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
                    op: "login"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ === "登录成功") {
                        window.location.href = "/BasicInfor/bookBasicManagement.aspx";
                    }
                    else {
                        swal({
                            title: "温馨提示:)",
                            text: "登录失败",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-success",
                            type: "warning",
                            allowOutsideClick: false
                        })
                    }
                }
            });
        }
    });
    //回车事件
    $(document).keypress(function (e) {
        if (e.keyCode == 13) {
            var pubKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCAnNXR7lHTpPH/97QOxIp+UusK9/RH5elvEPv6ssL37xGo8vQHh7CCsOonUWWVdi1iVegi7fRCkWeUVlta61EuX141+eKnZcdJe81NeUZ1h3N77JbzElbhhi8Wln6U27xpfkskKASLhQ4dS9DqoJQN/YUhBaBpER287Wjf3X6WmQIDAQAB";
            var encrypt = new JSEncrypt();
            encrypt.setPublicKey(pubKey);

            var userName = $("#userName").val();
            var pwd = $("#userPwd").val();
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
                        op: "login"
                    },
                    dataType: 'text',
                    success: function (succ) {
                        if (succ === "登录成功") {
                            window.location.href = "/BasicInfor/bookBasicManagement.aspx";
                        }
                        else {
                            swal({
                                title: "温馨提示",
                                text: "登录失败",
                                buttonsStyling: false,
                                confirmButtonClass: "btn btn-success",
                                type: "warning",
                                allowOutsideClick: false
                            })
                        }
                    }
                });
            }
        }
    })
});