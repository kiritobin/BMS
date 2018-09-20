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
            var strWhere = $("#input-search").val().trim();
            $.ajax({
                type: 'Post',
                url: 'organizationalManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
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
    var strWhere = $("#input-search").val().trim();
    $.ajax({
        type: 'Post',
        url: 'organizationalManagement.aspx',
        data: {
            search: strWhere,
            op: "paging"
        },
        dataType: 'text',
        success: function (data) {
            $("#intPageCount").remove();//删除原总页数
            $("#table tr:not(:first)").empty(); //清空table处首行
            $("#table").append(data); //加载table
            $(".paging").empty();//清空分页内容
            $('.paging').pagination({//重新加载分页内容
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
                        url: 'organizationalManagement.aspx',
                        data: {
                            page: api.getCurrent(), //页码
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
        }
    });
});
//添加分公司
$("#btnAdd").click(function () {
    var name = $("#int-Name").val();
    if (name == "" || name == null) {
        alert("分公司名称不能为空");
    } else{
        $.ajax({
            type: 'Post',
            url: 'organizationalManagement.aspx',
            data: {
                name: name,
                op: "add"
            },
            dataType: 'text',
            success: function (data) {
                if (data == "添加成功") {
                    alert("添加成功");
                } else {
                    alert("添加失败");
                }
            }
        })
    }
})
//删除分公司
$(".btn-delete").click(function () {
    var id = $("#regionId").val();
    $.ajax({
        type: 'Post',
        url: 'organizationalManagement.aspx',
        data: {
            regionId: id,
            op: "del"
        },
        dataType: 'text',
        success: function (data) {
            if (data == "添加成功") {
                alert("添加成功");
            } else {
                alert("添加失败");
            }
        }
    })
})