$(document).ready(function () {
    $('.paging').pagination({
        //totalData: $("#totalCount").val(),
        //showData: $("#pageSize").val(),
        pageCount: $("#intPageCount").val(), //总页数
        jump: true,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            var strWhere = $("#input-search").val();
            var regionId = $("#select-region").val();
            var roleId = $("#select-role").val();
            $.ajax({
                type: 'Post',
                url: 'userManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    role: roleId,
                    region: regionId,
                    search: strWhere,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table tr:not(:first)").empty(); //清空table处首行
                    $("#table").append(data); //加载table
                }
            });
        }
    });
})

//点击查询按钮时
$("#btn-search").click(function () {
    var region = $("#input-region").val().trim();
    var role = $("#input-role").val().trim();
    var userName = $("#input-userName").val().trim();
    $.ajax({
        type: 'Post',
        url: 'userManagement.aspx',
        data: {
            role: role,
            region: region,
            userName: userName,
            op: "paging"
        },
        dataType: 'text',
        success: function (data) {
            $("#intPageCount").remove();
            $("#table tr:not(:first)").empty(); //清空table处首行
            $("#table").append(data); //加载table
            $(".paging").empty();
            $('.paging').pagination({
                //totalData: $("#totalCount").val(),
                //showData: $("#pageSize").val(),
                pageCount: $("#intPageCount").val(), //总页数
                jump: true,
                mode: 'fixed',//固定页码数量
                coping: true,
                homePage: '首页',
                endPage: '尾页',
                prevContent: '上页',
                nextContent: '下页',
                callback: function (api) {
                    $.ajax({
                        type: 'Post',
                        url: 'userManagement.aspx',
                        data: {
                            page: api.getCurrent(), //页码
                            role: role,
                            region: region,
                            userName: userName,
                            op: "paging"
                        },
                        dataType: 'text',
                        success: function (data) {
                            $("#table tr:not(:first)").empty(); //清空table处首行
                            $("#table").append(data); //加载table
                        }
                    });
                }
            });
        }
    });
});

//添加用户
$("#btnAdd").click(function () {
    var name = $("#inputName").val().trim();
    var account = $("#inputAccount").val().trim();
    var region = $("#model-select-region").val().trim();
    var role = $("#model-select-role").val().trim();
    if (name == null || name == "") {
        swal({
            title: "温馨提示:)",
            text: "姓名不能为空，请确认后再次提交!",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    } else if (account == null || account == "") {
        swal({
            title: "温馨提示:)",
            text: "账号不能为空，请确认后再次提交!",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    } else if (region == null || region == "") {
        swal({
            title: "温馨提示:)",
            text: "分公司不能为空，请确认后再次提交!",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    } else if (role == null || role == "") {
        swal({
            title: "温馨提示:)",
            text: "职位不能为空，请确认后再次提交!",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    } else {
        $.ajax({
            type: 'Post',
            url: 'userManagement.aspx',
            data: {
                name: name,
                account: account,
                region: region,
                role: role,
                op: "add"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "添加成功") {
                    swal({
                        title: "温馨提示:)",
                        text: "用户添加成功",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "success",
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.reload();
                    })
                } else if (succ == "该用户已存在不能重复添加") {
                    swal({
                        title: '温馨提示:)',
                        text: '该用户已存在不能重复添加',
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.reload();
                    })
                } else {
                    swal({
                        title: '温馨提示:)',
                        text: '添加失败请联系管理员',
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.reload();
                    })
                }
            }
        });
    }
})
$("#btn-add").click(function () {
    $("#model-select-region").val("");
    $("#model-select-role").val("");
})
//编辑用户
$("#table").delegate(".btn-edit", "click", function () {
    var account = $(this).parent().prev().prev().prev().prev().text().trim();
    var name = $(this).parent().prev().prev().prev().text().trim();
    var region = $(this).parent().prev().prev().text().trim();
    var role = $(this).parent().prev().text().trim();
    $("#edit-Account").val(account);
    $("#edit-Name").val(name);
    $("#editRegion").find("option:contains(" + region + ")").attr("selected", true);
    $("#editRole").find("option:contains(" + region + ")").attr("selected", true);
})
$("#btnEdit").click(function () {
    var account = $("#edit-Account").val();
    var region = $("#editRegion").val();
    var role = $("#editRole").val();
    var name = $("#edit-Name").val();
    if (region == "") {
        region = sessionStorage.getItem("region")
    } if (role == "") {
        role = sessionStorage.getItem("role")
    }
    $.ajax({
        type: 'Post',
        url: 'userManagement.aspx',
        data: {
            name: name,
            account: account,
            region: region,
            role: role,
            op: "edit"
        },
        dataType: 'text',
        success: function (succ) {
            if (succ == "更新成功") {
                swal({
                    title: "温馨提示:)",
                    text: "修改成功",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-success",
                    type: "success",
                    allowOutsideClick: false
                }).then(function () {
                    window.location.reload();
                })
            } else {
                swal({
                    title: "温馨提示:)",
                    text: "修改失败",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-success",
                    type: "warning",
                    allowOutsideClick: false
                }).then(function () {
                    window.location.reload();
                })
            }
        }
    })
})
//删除用户
$("#table").delegate(".btn-delete", "click", function () {
    var account = $(this).parent().prev().prev().prev().prev().text().trim(); swal({
        title: '温馨提示:)',
        text: '确定要删除账号为：' + account + '的用户吗？',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: '是的，删掉它!',
        cancelButtonText: '不，让我思考一下',
        confirmButtonClass: "btn btn-success",
        cancelButtonClass: "btn btn-danger",
        buttonsStyling: false,
        allowOutsideClick: false
    }).then(function () {
        $.ajax({
            type: 'Post',
            url: 'userManagement.aspx',
            data: {
                account: account,
                op: "del"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "删除成功") {
                    swal({
                        title: "温馨提示:)",
                        text: "用户删除成功",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "success",
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.reload();
                    })
                } else {
                    swal({
                        title: "温馨提示:)",
                        text: "用户删除失败",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "warning",
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.reload();
                    })
                }
            }
        })
    });
})