$(document).ready(function () {
    $("#btn").click(function () {
        var userId = $("userId").val();
        var oldpwd = $("$oldPwd").val();
        var newpwd = $("$newpwd").val();
        var confirmpwd = $("$confirmpwd")
        $.ajax({
            type: 'Post',
            url: '.aspx',
            data: {
                userId: userId,
                oldpwd: oldpwd,
                newpwd: newpwd,
                op: "change"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "修改成功") {
                    swal({
                        title: "温馨提示:)",
                        text: "密码修改成功,请重新登录",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "success",
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.href("login.aspx");
                    })
                }
                else {
                    swal({
                        title: "温馨提示:)",
                        text: "密码修改失败",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "error",
                        allowOutsideClick: false
                    }).then(function () {
                    })
                }
            }
        });
    })
})
