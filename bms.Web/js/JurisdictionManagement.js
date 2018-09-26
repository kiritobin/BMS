$(document).ready(function () {
    $('.paging').pagination({
        pageCount: $("#intPageCount").val(), //总页数
        jump: true,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            var search = $("#search_All").val().trim();
            $.ajax({
                type: 'Post',
                url: 'JurisdictionManagement.aspx',
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
})
//查询按钮事件
$("#btn-search").click(function () {
    var search = $("#search_All").val().trim();
    $.ajax({
        type: 'Post',
        url: 'jurisdictionManagement.aspx',
        data: {
            search: search,
            op: "paging"
        },
        dataType: 'text',
        success: function (data) {
            $("#intPageCount").remove();
            $("#table tr:not(:first)").empty(); //清空table处首行
            $("#table").append(data); //加载table
            $(".paging").empty();
            $('.paging').pagination({
                pageCount: $("#intPageCount").val(), //总页数
                jump: true,
                mode: 'fixed',//固定页码数量
                coping: true,
                homePage: '首页',
                endPage: '尾页',
                prevContent: '上页',
                nextContent: '下页',
                callback: function (api) {
                    var search = $("#search_All").val().trim();
                    $.ajax({
                        type: 'Post',
                        url: 'JurisdictionManagement.aspx',
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
        }
    })
})
//点击添加按钮时
$("#btnAdd").click(function () {
    var name = $("#functionName").val();
    if (name == "") {
        swal({
            title: "温馨提示:)",
            text: "功能名称不能为空，请您重新输入!",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type:"warning"
        }).catch(swal.noop)
    }
    else {
        $.ajax({
            type: 'Post',
            url: 'jurisdictionManagement.aspx',
            data: {
                functionName: name,
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
                        window, location.reload();
                    })
                } else {
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
                        window, location.reload();
                    })
                }
            }
        })
    }
})
//删除用户

$("#table").delegate(".btn-delete", "click", function () {
    var functionId = $(this).prev().val();
    //弹窗
    swal({
        title: "温馨提示:)",
        text: "您确定要删除该用户吗？",
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
            type: 'Post',
            url: 'jurisdictionManagement.aspx',
            data: {
                functionId: functionId,
                op: "del"
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
                        window, location.reload();
                    })
                } else {
                    swal({
                        title: succ,
                        text: succ,
                        type: "wraning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        window, location.reload();
                    })
                }
            }
        })
    })
})

function logout() {
    //alert("退出登录");
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
            url: 'jurisdictionManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}