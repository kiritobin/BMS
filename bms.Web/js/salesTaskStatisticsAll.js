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
            var bookNum = $("#bookNum").val();
            var bookName = $("#bookName").val();
            var regionName = $("#regionName").val();
            var time = $("#time").val();
            var customerName = $("#customerName").val();
            $.ajax({
                type: 'Post',
                url: 'salesTaskStatisticsAll.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    bookNum: bookNum,
                    bookName: bookName,
                    regionName: regionName,
                    time: time,
                    customerName: customerName,
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
    $("#btn_search").click(function () {
        var bookNum = $("#bookNum").val();
        var bookName = $("#bookName").val();
        var regionName = $("#regionName").val();
        var time = $("#time").val();
        var customerName = $("#customerName").val();
        $.ajax({
            type: 'Post',
            url: 'salesTaskStatisticsAll.aspx',
            data: {
                bookNum: bookNum,
                bookName: bookName,
                regionName: regionName,
                time: time,
                customerName: customerName,
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
                            url: 'salesTaskStatisticsAll.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                bookNum: bookNum,
                                bookName: bookName,
                                regionName: regionName,
                                time: time,
                                customerName: customerName,
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
})