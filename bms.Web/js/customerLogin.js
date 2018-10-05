$(document).ready(function () {
    //用户名输入框获取焦点
    $("#userName").focus();
    //提交事件
    $("#submit").click(function () {
        var userName = $("#userName").val();
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
                        window.location.href = "/CustomerMGT/customerPurchase.aspx";
                    }
                    else {
                        swal({
                            title: "温馨提示:)",
                            text: succ,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-success",
                            type: "warning",
                            allowOutsideClick: false
                        }).then(function () {
                        })
                    }
                }
            });
        }
    })
    //回车事件
    $(document).keypress(function (e) {
        if (e.keyCode == 13) {
            var userName = $("#userName").val();
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
                            window.location.href = "/CustomerMGT/customerPurchase.aspx";
                        }
                        else {
                            swal({
                                title: "温馨提示:)",
                                text: succ,
                                buttonsStyling: false,
                                confirmButtonClass: "btn btn-success",
                                type: "warning",
                                allowOutsideClick: false
                            }).then(function () {
                            })
                        }
                    }
                });
            }
        }
    })
})