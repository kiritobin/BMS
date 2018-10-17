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
            var search = $("#btn-search").val().trim();
            $.ajax({
                type: 'Post',
                url: 'salesManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    search: search,
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
    //查询
    $("#btn-search").click(function () {
        var saleTaskId = $("#ID").val().trim();
        var regionName = $("#regionName").val().trim();
        var userName = $("#userName").val().trim();
        $.ajax({
            type: 'Post',
            url: 'salesManagement.aspx',
            data: {
                saleTaskId: saleTaskId,
                regionName: regionName,
                userName: userName,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#intPageCount").remove();
                $("#table tr:not(:first)").remove(); //清空table处首行
                $("#table").append(data); //加载table
                $(".paging").empty();
                $('.paging').pagination({
                    //totalData: $("#countPage").val(), //数据总数
                    //showData: $("#totalCount").val(), //每页显示的条数
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
                            url: 'salesManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                saleTaskId: saleTaskId,
                                regionName: regionName,
                                userName: userName,
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
    //完成
    $("#finish").click(function () {
        swal({
            title: "温馨提示:)",
            text: "您确认要完成该销售任务吗？",
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
                url: 'salesManagement.aspx',
                data: {
                    op: "finish"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "成功") {
                        swal({
                            title: "温馨提示:)",
                            text: "保存完成。",
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
                    else if (succ == "未完成失败") {
                        swal({
                            title: "温馨提示:)",
                            text: "有未完成单据，请检查后再次操作。",
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                        })
                    }
                    else  {
                        swal({
                            title: "温馨提示:)",
                            text: "保存失败。",
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
            })
        })
    })
    //添加
    $("#btn_add").click(function () {
        swal({
            title: "温馨提示:)",
            text: "您是否新建销售？",
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
                url: 'salesManagement.aspx',
                data: {
                    op: "add"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "添加成功") {
                        swal({
                            title: "温馨提示:)",
                            text: "添加成功",
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
                    else {
                        swal({
                            title: "温馨提示:)",
                            text: "添加失败请联系技术人员!",
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
            })
        })
    })
    //删除
    $("#table").delegate(".btn_del", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        var taskId = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        var state = $(this).parent().prev().prev().prev().prev().prev().prev().prev().text().trim();
        if (state == "新建单据") {
            swal({
                title: "温馨提示:)",
                text: "删除后将无法恢复,您确定要删除该项吗？？？",
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
                    url: 'salesManagement.aspx',
                    data: {
                        ID: ID,
                        taskId: taskId,
                        op: 'del'
                    },
                    dataType: 'text',
                    success: function (succ) {
                        if (succ == "删除成功") {
                            swal({
                                title: "温馨提示:)",
                                text: "删除成功。",
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
                                title: "温馨提示:)",
                                text: "删除失败。",
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
                })
            })
        }
        else if (state == "采集中") {
            swal({
                title: "温馨提示:)",
                text: "单据采集中，现在不能删除。",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            }).then(function () {
            })
        }
        else if (state == "单据已完成") {
            swal({
                title: "温馨提示:)",
                text: "单据已完成不能删除。",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            }).then(function () {
            })
        }
    });

    //加
    $("#table").delegate(".add", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        var taskId = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        var state = $(this).parent().prev().prev().prev().prev().prev().prev().prev().text().trim();
        if (state == "单据已完成") {
            swal({
                title: "温馨提示:)",
                text: "单据已完成，不能添加。",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            }).then(function () { })
        } else {
            $.ajax({
                type: 'Post',
                url: 'salesManagement.aspx',
                data: {
                    ID: ID,
                    taskId: taskId,
                    op: 'addDetail'
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "成功") {
                        window.location.href = "../SalesMGT/salesDetail.aspx";
                    } else {
                        swal({
                            title: "温馨提示:)",
                            text: "单据已完成，不能添加。",
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () { })
                    }
                }
            })
        }
    })
    //看
    $("#table").delegate(".look", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        $.ajax({
            type: 'Post',
            url: 'salesManagement.aspx',
            data: {
                ID: ID,
                op: 'look'
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "成功") {
                    window.location.href = "../SalesMGT/salesDetail.aspx";
                }
            }
        })
    })
})