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
                url: 'customerRs.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    op: "paging"

                },
                dataType: 'text',
                success: function (data) {
                    $("#table tbody").empty(); //清空table处首行
                    $("#table tbody").append(data); //加载table
                }
            });
        }
    });
})

//退出系统
function logout() {
    swal({
        title: "温馨提示:)",
        text: "您确定要退出系统吗？",
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        confirmButtonClass: 'btn btn-success',
        cancelButtonClass: 'btn btn-danger',
        buttonsStyling: false,
        allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
    }).then(function () {
        $.ajax({
            type: 'get',
            url: 'customerRs.aspx',
            datatype: 'text',
            data: {
                op: "logout"
            },
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

//地区下拉框查询
$("#cusSearch").change(function () {
    var cusId = $("#cusSearch").val();
    if (cusId != null || cusId != "") {
        $.ajax({
            type: 'post',
            url: 'customerRs.aspx',
            datatype: 'text',
            data: {
                cusId: cusId,
                op: "search"
            },
            success: function (data) {
                $("#intPageCount").remove();
                $("#table tr:not(:first)").remove(); //清空table处首行
                $("#table").append(data); //加载table
                $(".paging").empty();
                $('.paging').pagination({
                    //totalData: $("#countPage").val(), //数据总数
                    //showData: $("#totalCount").val(), //每页显示的条数
                    pageCount: $("#intPageCount").val(), //总页数
                    jump: true,
                    mode: 'fixed',//固定页码数量
                    coping: true,
                    homePage: '首页',
                    endPage: '尾页',
                    prevContent: '上页',
                    nextContent: '下页',
                    callback: function (api) {
                        var book = $("#bookSearch").val().trim();
                        var isbn = $("#isbnSearch").val().trim();
                        $.ajax({
                            type: 'Post',
                            url: 'customerRs.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                regionId: regionId,
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
    }
})