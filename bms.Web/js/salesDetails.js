﻿jeDate("#startTime", {
    theme: {
        bgcolor: "#D91600",
        pnColor: "#FF6653"
    },
    multiPane: true,
    format: "YYYY-MM-DD"
});
jeDate("#endTime", {
    theme: {
        bgcolor: "#D91600",
        pnColor: "#FF6653"
    },
    multiPane: true,
    format: "YYYY-MM-DD"
});

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
            var groupby = $("#groupby").find("option:selected").text();
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
            } else if (groupby == "客户") {
                groupbyType = "customerName";
            } else {
                groupbyType = "state";
            }
            var supplier = $("#supplier").val();
            var regionName = $("#region").val();
            var time = $("#time").val();
            var customerName = $("#customer").val();
            var salestate = $("#state").find("option:selected").text();
            var saleHeadState;
            if (salestate == "空") {
                saleHeadState = "0";
            } else if (salestate == "销售") {
                saleHeadState = "1";
            } else if (salestate == "预采") {
                saleHeadState = "3";
            }
            var page = api.getCurrent();
            $.ajax({
                type: 'Post',
                url: 'salesStatistics.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    saleHeadState: saleHeadState,
                    groupbyType: groupbyType,
                    supplier: supplier,
                    regionName: regionName,
                    time: time,
                    customerName: customerName,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    if (groupby == "供应商") {
                        $("#showType").text("供应商");
                    }
                    else if (groupby == "组织") {
                        $("#showType").text("组织");
                    } else if (groupby == "客户") {
                        $("#showType").text("客户");
                    } else {
                        $("#showType").text("客户");
                    }
                    $("#table tr:not(:first)").remove(); //清空table处首行
                    $("#table").append(data); //加载table
                    $("#intPageCount").remove();
                }
            });

        }
    });
    //清空时间
    $("#modalClose").click(function () {
        $("#time").val("");
        $("#myModal").modal('hide');
    })
    //选择时间后确定
    $("#btnOK").click(function () {
        var startTime = $("#startTime").val();
        var endTime = $("#endTime").val();
        if (startTime == "" || startTime == null) {
            swal({
                title: "提示",
                text: "请选择开始时间",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else if (endTime == "" || endTime == null) {
            swal({
                title: "提示",
                text: "请选择结束时间",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else {
            $("#time").val(startTime + "至" + endTime);
            $("#myModal").modal('hide');
        }
    })
});

$("#search").click(function () {
    var isbn = $("#isbn").val();
    var price = $("#price").val();
    var discount = $("#discount").val();
    var user = $("#user").val();
    var time = $("#time").val();
    var state = $("#state").val();
    $.ajax({
        type: 'Post',
        url: 'salesStatistics.aspx',
        data: {
            isbn: isbn,
            price: price,
            discount: discount,
            user: user,
            time: time,
            state: state,
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
                    $.ajax({
                        type: 'Post',
                        url: 'salesStatistics.aspx',
                        data: {
                            isbn: isbn,
                            price: price,
                            discount: discount,
                            user: user,
                            time: time,
                            state: state,
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
    })
})