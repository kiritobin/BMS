﻿$(document).ready(function () {
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
                    $("#table tr:not(:first)").empty(); //清空table处首行
                    $("#table").append(data); //加载table
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
                $("#table tr:not(:first)").empty(); //清空table处首行
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
                                $("#table tr:not(:first)").empty(); //清空table处首行
                                $("#table").append(data); //加载table
                            }
                        });
                    }
                });
            }
        });
    });

    //添加
    $("#btnAdd").click(function () {
        var billCount = $("#billCount").val().trim();
        var totalPrice = $("#totalPrice").val().trim();
        var realPrice = $("#realPrice").val().trim();
        var Price = $("#Price").val().trim();
        var saleCustmer = $("#saleCustmer").val().trim();
        if (billCount == "" || totalPrice == "" || realPrice == "" || Price == "" || saleCustmer == "") {
            swal({
                title: "温馨提示:)",
                text: "不能含有未填项!",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'salesManagement.aspx',
                data: {
                    Custmer: saleCustmer,
                    numberLimit: billCount,
                    priceLimit: totalPrice,
                    totalPriceLimit: realPrice,
                    defaultDiscount: Price,
                    op: "add"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "添加成功") {
                        swal({
                            title: "温馨提示",
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
                    } else {
                        swal({
                            title: "温馨提示",
                            text: succ,
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
        }
    })
    //删除
    $("#table").delegate(".btn_del", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().text();
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
            $.ajax({
                type: 'Post',
                url: 'salesManagement.aspx',
                data: {
                    ID: ID,
                    op: 'del'
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
                        })
                    }
                }
            })
        })
    });

    //加
    $("#table").delegate(".add", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().text();
        $.ajax({
            type: 'Post',
            url: 'salesManagement.aspx',
            data: {
                ID: ID,
                op: 'addDetail'
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "成功") {
                    window.location.href = "../SalesMGT/salesDetail.aspx";
                }
            }
        })
    })
    //看
    $("#table").delegate(".look", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().text();
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