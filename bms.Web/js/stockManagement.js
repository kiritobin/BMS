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
            var strWhere = $("#input-search").val();
            var regionId = $("#select-region").val();
            var roleId = $("#select-role").val();
            $.ajax({
                type: 'Post',
                url: 'stockManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    role: roleId,
                    region: regionId,
                    search: strWhere,
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
    var singHeadId = $("#singHeadId").val().trim();
    var regionName = $("#regionName").val().trim();
    var userName = $("#userName").val().trim();
    $.ajax({
        type: 'Post',
        url: 'stockManagement.aspx',
        data: {
            singHeadId: singHeadId,
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
                        url: 'stockManagement.aspx',
                        data: {
                            page: api.getCurrent(), //页码
                            singHeadId: singHeadId,
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