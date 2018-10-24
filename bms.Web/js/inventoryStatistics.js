function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

$(document).ready(function () {
    var bookNum = $("#bookNum").val();
    var bookName = $("#bookName").val();
    var supplier = $("#supplier").val();
    var time = $("#time").val();
    var userName = $("#userName").val();
    var region = $("#region").val();

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
            var bookName = $("#bookName").val();
            var bookNum = $("#bookNum").val();
            var btnISBN = $("#bookISBN").val();
            $.ajax({
                type: 'Post',
                url: 'inventoryStatistics.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    bookNum: bookNum,
                    bookName: bookName,
                    supplier: supplier,
                    time: time,
                    userName: userName,
                    region: region,
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

    var type = GetQueryString("type");
    if (type == "RK") {
        $("#tjType").html("入&nbsp;库&nbsp;统&nbsp;计");
        $('#resource').attr('placeholder', "请输入来源组织");
    }
    else if (type == "CK") {
        $("#tjType").html("出&nbsp;库&nbsp;统&nbsp;计");
        $('#resource').attr('placeholder', "请输入收货组织");
    }
    else if (type == "TH") {
        $("#tjType").html("退&nbsp;货&nbsp;统&nbsp;计");
        $('#resource').attr('placeholder', "请输入收货组织");
    }
    $("#btn_search").click(function () {
        var bookNum = $("#bookNum").val();
        var bookName = $("#bookName").val();
        var supplier = $("#supplier").val();
        var time = $("#time").val();
        var userName = $("#userName").val();
        var region = $("#region").val();

        $.ajax({
            type: 'Post',
            url: 'inventoryStatistics.aspx',
            data: {
                bookNum: bookNum,
                bookName: bookName,
                supplier: supplier,
                time: time,
                userName: userName,
                region: region,
                op: "search"
            },
            dataType: 'text',
            success: function (data) {

            }
        });

    });
});