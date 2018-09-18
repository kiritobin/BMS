$(document).ready(function () {
    //地区下拉框改变事件
    $("#select-region").change(function () {
        var region = $("#select-region").val().trim();
        var search = $("#search").val().trim();
        $.ajax({
            type: 'Post',
            url: 'collectionManagement.aspx',
            data: {
                region: region,
                search: search,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#table tr:not(:first)").empty(); //清空table处首行
                $("#table").append(data); //加载table
            }
        });
    })

    //点击查询按钮时
    $("#btn-search").click(function () {
        var region = $("#select-region").val().trim();
        var search = $("#search").val().trim();
        $.ajax({
            type: 'Post',
            url: 'collectionManagement.aspx',
            data: {
                region: region,
                search: search,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#table tr:not(:first)").empty(); //清空table处首行
                $("#table").append(data); //加载table
            }
        });
    });

    $('.paging').pagination({
        pageCount: $("#countPage").val(), //总页数
        jump: true,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            var region = $("#select-region").val().trim();
            var search = $("#search").val().trim();
            $.ajax({
                type: 'Post',
                url: 'collectionManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    region: region,
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
});