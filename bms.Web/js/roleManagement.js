$(document).ready(function () {
    $(".paging").pagination({
        pageCount: $("#intPageCount").val(), //总页数
        jump: true,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            var search = $("#btn-search").val();
            $.ajax({
                type: 'Post',
                url: 'roleManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    search: search,
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

    //点击查询按钮时
    $("#btn-search").click(function () {
        var strWhere = $("#input-search").val().trim();
        $.ajax({
            type: 'Post',
            url: 'roleManagement.aspx',
            data: {
                search: strWhere,
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
                            url: 'roleManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
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
            }
        });
    });

    //添加职位
    $("#btnAdd").click(function () {
        var roleName = $("#addRoleName").val();
        var obj = document.getElementsByName('checkbox'); 
        var functionId = "";
        for (var i = 0; i < obj.length; i++) {
            if (obj[i].checked) functionId += obj[i].value + '?';
        }
        if (functionId == "") {
            swal({
                title: "未选择任何项",
                text: "请至少选择一项权限",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            }).then(function () {
            })
        } else {
            $.ajax({
                type: 'Post',
                url: 'roleManagement.aspx',
                data: {
                    roleName: roleName,
                    functionId: functionId,
                    op: "add"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "添加成功") {
                        swal({
                            title: succ,
                            text: succ,
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window.location.reload();
                        })
                    } else {
                        swal({
                            title: succ,
                            text: succ,
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window.location.reload();
                        })
                    }
                }
            })
        }
    })
    //关闭添加模态框
    $("#model-btnclose1").click(function () {
        var obj = $("input[name=checkbox]");//获取复选框
        for (var j = 0; j < obj.length; j++) {
            obj[j].checked = false;
        }
    })

    //编辑职位
    $("#table").delegate(".btn-edit", "click", function () {
        var roleName = $(this).parent().prev().prev().text().trim();
        sessionStorage.setItem("roleName", roleName);
        $("#editRoleName").val(roleName);
        var rows = $(this).prev().prev().prev().val();
        sessionStorage.setItem("rows", rows);
        var roleId = $(this).prev().prev().val();
        sessionStorage.setItem("roleId", roleId);
        var fun = $(this).prev().val();
        sessionStorage.setItem("funId", fun);
        $("#function").hide();
        sessionStorage.setItem("editFun", "false");
    });
    //编辑功能
    $("#editFun").click(function () {
        $(this).hide();
        $("#function").show();
        var fun = sessionStorage.getItem("funId");
        var funId = fun.replace(" ", "");
        var strs = new Array();
        strs = funId.split(","); //分割功能id字符串 
        var obj = $("input[name=checkboxEdit]");//获取复选框
        for (var i = 0; i < strs.length; i++) {
            for (var j = 0; j < obj.length; j++) {
                var a = obj[j].value;
                var b = strs[i];
                if (obj[j].value == strs[i]) {
                    obj[j].checked = true;
                }
            }
        }
        sessionStorage.setItem("editFun", "true");
    })
    //提交编辑
    $("#btnEdit").click(function () {
        var roleName = $("#editRoleName").val().trim();
        if (sessionStorage.getItem("editFun") == "true") {
            var obj = $("input[name=checkboxEdit]");//获取复选框
            var strs = "";
            if (obj.length > 0) {
                for (var j = 0; j < obj.length; j++) {
                    if (obj[j].checked == true) {
                        var str = obj[j].value + "?";
                        strs = strs + str;
                    }
                }
                $.ajax({
                    type: 'Post',
                    url: 'roleManagement.aspx',
                    data: {
                        rows: sessionStorage.getItem("rows"),
                        roleId: sessionStorage.getItem("roleId"),
                        oldName: sessionStorage.getItem("roleName"),
                        roleName: roleName,
                        funIds: strs,
                        op: "editFun"
                    },
                    dataType: 'text',
                    success: function (succ) {
                        if (succ == "更新成功") {
                            swal({
                                title: succ,
                                text: succ,
                                type: "success",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                                window.location.reload();
                            })
                        } else {
                            swal({
                                title: succ,
                                text: succ,
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                                window.location.reload();
                            })
                        }
                    }
                })
            } else {
                swal({
                    title: "未选择任何项",
                    text: "请至少选择一项权限",
                    type: "warning",
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: '确定',
                    confirmButtonClass: 'btn btn-success',
                    buttonsStyling: false,
                    allowOutsideClick: false
                }).then(function () {
                })
            }
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'roleManagement.aspx',
                data: {
                    roleId: sessionStorage.getItem("roleId"),
                    roleName: roleName,
                    op: "edit"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "更新成功") {
                        swal({
                            title: succ,
                            text: succ,
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window.location.reload();
                        })
                    } else {
                        swal({
                            title: succ,
                            text: succ,
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window.location.reload();
                        })
                    }
                }
            })
        }
    })
    //关闭编辑模态框
    $("#model-btnclose2").click(function () {
        var obj = $("input[name=checkboxEdit]");//获取复选框
        for (var j = 0; j < obj.length; j++) {
            obj[j].checked = false;
        }
        $("#editFun").show();
    })

    //删除职位
    $("#table").delegate(".btn-delete", "click", function () {
        swal({
            title: "是否删除？",
            text: "删除后将无法恢复！！！",
            type: "question",
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
            var rows = $(".btn-delete").prev().prev().prev().prev().val();
            var roleId = $(".btn-delete").prev().prev().prev().val();
            $.ajax({
                type: 'Post',
                url: 'roleManagement.aspx',
                data: {
                    rows: rows,
                    roleId: roleId,
                    op:'del'
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "删除成功") {
                        swal({
                            title: succ,
                            text: succ,
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window.location.reload();
                        })
                    } else {
                        swal({
                            title: succ,
                            text: succ,
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window.location.reload();
                        })
                    }
                }
            })
        })
    });
});

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
            url: 'roleManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}
