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
            var bookName = $("#bookName").val().trim();
            var bookNum = $("#bookNum").val().trim();
            var btnISBN = $("#btnISBN").val().trim();
            $.ajax({
                type: 'Post',
                url: 'bookBasicManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    bookName: bookName,
                    bookNum: bookNum,
                    btnISBN: btnISBN,
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
    var bookName = $("#bookName").val().trim();
    var bookNum = $("#bookNum").val().trim();
    var bookISBN = $("#bookISBN").val().trim();
    $.ajax({
        type: 'Post',
        url: 'bookBasicManagement.aspx',
        data: {
            bookName: bookName,
            bookNum: bookNum,
            bookISBN: bookISBN,
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
                        url: 'bookBasicManagement.aspx',
                        data: {
                            page: api.getCurrent(), //页码
                            bookName: bookName,
                            bookNum: bookNum,
                            bookISBN: bookISBN,
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