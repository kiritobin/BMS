$(document).ready(function () {

    var url = getUrlParam(location.href, "logout");
    if (url == "force") {
        swal({
            title: "注意",
            text: "帐号已在别处登录 ，你将被强迫下线（若非本人登录，请注意保护密码安全）！",
            type: "warning",
            confirmButtonColor: '#3085d6',
            confirmButtonText: '确定',
            confirmButtonClass: 'btn btn-success',
            buttonsStyling: false,
            allowOutsideClick: false
        })
    }
    else {
        //用户名输入框获取焦点
        $("#userName").focus();
    }

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
                dataType: 'json',
                success: function (succ) {
                    if (succ["succ"]) {
                        sessionStorage.setItem("tokenId", succ["tokenId"]);
                        window.location.href = "welcomePage.aspx";
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
                },
                error: function (XMLHttpRequest, textStatus) { //请求失败
                    swal({
                        title: "温馨提示:)",
                        text: "登录失败，请重试",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "warning",
                        allowOutsideClick: false
                    })
                }
            });
        }
    });
    //回车事件
    $("#userName").keypress(function (e) {
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
                    dataType: 'json',
                    success: function (succ) {
                        if (succ["succ"]) {
                            sessionStorage.setItem("tokenId", succ["tokenId"]);
                            window.location.href = "welcomePage.aspx";
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
                    },
                    error: function (XMLHttpRequest, textStatus) { //请求失败
                        swal({
                            title: "温馨提示:)",
                            text: "登录失败，请重试",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-success",
                            type: "warning",
                            allowOutsideClick: false
                        })
                    }
                });
            }
        }
    })
    //回车事件
    $("#userPwd").keypress(function (e) {
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
                    dataType: 'json',
                    success: function (succ) {
                        if (succ["succ"]) {
                            sessionStorage.setItem("tokenId", succ["tokenId"]);
                            window.location.href = "welcomePage.aspx";
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
                    },
                    error: function (XMLHttpRequest, textStatus) { //请求失败
                        swal({
                            title: "温馨提示:)",
                            text: "登录失败，请重试",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-success",
                            type: "warning",
                            allowOutsideClick: false
                        })
                    }
                });
            }
        }
    })
});

//禁止回退
$(function () {
    　　if (window.history && window.history.pushState) {
        　　$(window).on('popstate', function () {
            　　window.history.pushState('forward', null, '#');
            　　window.history.forward(1);
        　　});
    　　}
    　　window.history.pushState('forward', null, '#'); //在IE中必须得有这两行
    　　window.history.forward(1);
　　})

//地址栏获取
//console.log(getUrlParam(location.href,"参数名"));
function getUrlParam(url, name) {
    var pattern = new RegExp("[?&]" + name + "\=([^&]+)", "g");
    var matcher = pattern.exec(url);
    var items = null;
    if (null != matcher) {
        try {
            items = decodeURIComponent(decodeURIComponent(matcher[1]));
        } catch (e) {
            try {
                items = decodeURIComponent(matcher[1]);
            } catch (e) {
                items = matcher[1];
            }
        }
    }
    return items;
}  