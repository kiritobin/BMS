$(document).ready(function () {
    //$('.paging').pagination({
    //    pageCount: $("#intPageCount").val(), //总页数
    //    jump: true,
    //    mode: 'fixed',//固定页码数量
    //    coping: true,
    //    homePage: '首页',
    //    endPage: '尾页',
    //    prevContent: '上页',
    //    nextContent: '下页',
    //    callback: function (api) {
    //        $.ajax({
    //            type: 'Post',
    //            url: 'backQuery.aspx',
    //            data: {
    //                page: api.getCurrent(), //页码
    //                op: "paging"
    //            },
    //            dataType: 'text',
    //            success: function (data) {
    //                $("#table tr:not(:first)").empty(); //清空table处首行
    //                $("#table").append(data); //加载table
    //            }
    //        });
    //    }
    //});
    $('#search_sim').bind('keypress', function (event) {//监听sim卡回车事件
        if (event.keyCode == "13") {
            var isbn = $("#search_sim").val();
            alert(isbn);
            $.ajax({
                type: 'Post',
                url: 'backQuery.aspx',
                data: {
                    isbn:isbn,
                    op: "searcisbn"
                },
                datatype: 'text',
                success: function (data) {
                    $("#Book").show();
                }
            });
        }
    });
})