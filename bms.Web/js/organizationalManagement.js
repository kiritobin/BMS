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
            var strWhere = $("#input-search").val();
            $.ajax({
                type: 'Post',
                url: 'organizationalManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    search: strWhere,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table tr:not(:first)").remove(); //清空table处首行
                    $("#table").append(data); //加载table
                    $("#intPageCount").remove();
                }
            });
        }
    });

    //点击查询按钮时
    $("#btn-search").click(function () {
        var strWhere = $("#input-search").val();
        $.ajax({
            type: 'Post',
            url: 'organizationalManagement.aspx',
            data: {
                search: strWhere,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#intPageCount").remove();//删除原总页数
                $("#table tr:not(:first)").remove(); //清空table处首行
                $("#table").append(data); //加载table
                $(".paging").empty();//清空分页内容
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
                        var strWhere = $("#input-search").val();
                        $.ajax({
                            type: 'Post',
                            url: 'organizationalManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                search: strWhere,
                                op: "paging"
                            },
                            dataType: 'text',
                            success: function (data) {
                                $("#table tr:not(:first)").remove(); //清空table处首行
                                $("#table").append(data); //加载table
                                $("#intPageCount").remove();
                            }
                        });
                    }
                });
            }
        });
    });
    //添加分公司
    $("#btnAdd").click(function () {
        var name = $("#int-Name").val().trim();
        if (name == "" || name == null) {
            swal({
                title: "温馨提示:)",
                text: "公司名称不能为空，请确认后再次提交!",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else {
            $.ajax({
                type: 'Post',
                url: 'organizationalManagement.aspx',
                data: {
                    name: name,
                    op: "add"
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "添加成功") {
                        swal({
                            title: "温馨提示:)",
                            text: "公司添加成功",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-success",
                            type: "success",
                            allowOutsideClick: false
                        }).then(function () {
                            sessionStorage.setItem("add", "添加成功");
                            //window.location.reload();
                        })
                    }
                    else if (data == "该组织已经存在") {
                        swal({
                            title: "温馨提示:)",
                            text: "该组织已经存在",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-info",
                            type: "error",
                            allowOutsideClick: false
                        }).then(function () {
                        })
                    }
                    else {
                        swal({
                            title: '温馨提示:)',
                            text: "添加失败",
                            type: 'error',
                            confirmButtonClass: "btn btn-info",
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                        })
                    }
                }
            })
        }
    })
    $("#close").click(function () {
        if (sessionStorage.getItem("add") == "添加成功") {
            window.location.reload();
            sessionStorage.removeItem("add");
        }
    })
    //编辑组织名称
    $("#table").delegate(".btn_Editor", "click", function () {
        var name = $(this).parent().prev().text().trim();
        var id = $(this).parent().next().text().trim();
        $("#editor_Name").val(name);
        $("#editor_id").val(id);
    })
    $("#save_Editor").click(function () {
        var name = $("#editor_Name").val();
        var id = $("#editor_id").val();
        if (name == "" || name == null) {
            swal({
                title: "温馨提示:)",
                text: "公司名称不能为空，请确认后再次提交!",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else {
            $.ajax({
                type: 'Post',
                url: 'organizationalManagement.aspx',
                data: {
                    name: name,
                    id: id,
                    op: "editor"
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "更新成功") {
                        swal({
                            title: data,
                            text: data,
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
                            title: '温馨提示:)',
                            text: "更新失败",
                            type: 'error',
                            confirmButtonClass: "btn btn-info",
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                        })
                    }
                }
            })
        }
    })

    //删除分公司
    $("#table").delegate(".btn-delete", "click", function () {
        var id = $(this).prev().val().trim();
        swal({
            title: '温馨提示:)',
            text: '你确定要删除该分公司吗？',
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
                url: 'organizationalManagement.aspx',
                data: {
                    regionId: id,
                    op: "del"
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "删除成功") {
                        swal({
                            title: "温馨提示:)",
                            text: "公司删除成功",
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
                            text: "公司删除失败",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-success",
                            type: "warning",
                            allowOutsideClick: false
                        }).then(function () {
                        })
                    }
                }
            })
        })
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
            url: 'organizationalManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}
