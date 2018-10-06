$(document).ready(function () {
    $("#btnchange").click(function () {
        var oldpwd = $("#oldPwd").val().trim();
        var newpwd = $("#newPwd").val().trim();
        var confirmpwd = $("#confirmpwd").val().trim();
        if (oldpwd == null || newpwd == null || confirmpwd == null) {
            swal({
                title: "温馨提示:)",
                text: "不能含有未填项",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-success",
                type: "warning",
                allowOutsideClick: false
            })
        }
        else if (newpwd != confirmpwd) {
            swal({
                title: "温馨提示:)",
                text: "两次输入的新密码不一致",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-success",
                type: "warning",
                allowOutsideClick: false
            })
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'changePwd.aspx',
                data: {
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
                            window.location.href = "../login.aspx";
                        })
                    }
                    else if (succ == "旧密码不匹配") {
                        swal({
                            title: "温馨提示:)",
                            text: "旧密码不匹配",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-success",
                            type: "warning",
                            allowOutsideClick: false
                        })
                    }
                    else {
                        swal({
                            title: "提示:)",
                            text: "密码修改失败",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-success",
                            type: "error",
                            allowOutsideClick: false
                        })
                    }
                }
            });
        }

    })
})

//退出系统
function logout() {
    swal({
        title: "温馨提示:)",
        text: "您确定要退出系统吗？",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger',
        buttonsStyling: false,
        allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
    }).then(function () {
        $.ajax({
            type: 'get',
            url: 'changePwd.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "login.aspx";
            }
        });
    })
}
