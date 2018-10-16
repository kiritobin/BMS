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
            $.ajax({
                type: 'Post',
                url: 'retailManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
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
    $("#table").delegate(".btn_search", "click", function () {
        var retailHeadId = $(this).parent().prev().prev().prev().prev().prev().prev().text().trim();
        $.ajax({
            type: 'Post',
            url: 'retailManagement.aspx',
            data: {
                retailHeadId: retailHeadId,
                op: "search"
            },
            dataType: 'text',
            success: function (data) {
                if (data == "成功") {
                    window.location.href = "../SalesMGT/retailQuery.aspx"
                }
            }
        });
    })
})