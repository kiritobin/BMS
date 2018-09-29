$(document).ready(function () {
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
                        window.location.href = "/main.aspx";
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

})