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
    var name = $("#int-Name").val().trim();
    if (name == "" || name == null) {
        swal({
            title: "温馨提示:)",
            text: "公司名称不能为空，请确认后再次提交!",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
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
                    swal({
                        title: "温馨提示:)",
                        text: "公司添加成功",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "success"
                    }).catch(swal.noop)
                } else {
                    swal({
                        title: '温馨提示:)',
                        text: '公司添加失败',
                        type: 'error',
                        confirmButtonClass: "btn btn-info",
                        buttonsStyling: false
                    }).catch(swal.noop)
                }
            }
        })
    }
})
//删除分公司
$("#table").delegate(".btn-delete", "click", function () {
    var flag = swal({
        title: '温馨提示:)',
        text: '你确定要删除该分公司吗？',
        type: 'warning',
        showCancelButton: true,
        confirmButtonText: '是的，删掉它!',
        cancelButtonText: '不，让我思考一下',
        confirmButtonClass: "btn btn-success",
        cancelButtonClass: "btn btn-danger",
        buttonsStyling: false
    });
    if (flag == true) {
        var id = $(this).parent().parent().find("#regionId").val().trim();
        $.ajax({
            type: 'Post',
            url: 'organizationalManagement.aspx',
            data: {
                regionId: id,
                op: "del"
            },
            dataType: 'text',
            success: function (data) {
                if (data == "删除成功") {
                    swal({
                        title: "温馨提示:)",
                        text: "公司删除成功",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "success"
                    }).catch(swal.noop)
                } else {
                    alert(data);
                }
            }
        })
    }
})