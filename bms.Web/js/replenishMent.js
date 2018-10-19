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
            var search = $("#customer").val();
            $.ajax({
                type: 'Post',
                url: 'replenishMent.aspx',
                data: {
                    page: api.getCurrent(), //页码
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
    //点击查看
    $("#table").delegate(".btn_search", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().text().trim();
        $.ajax({
            type: 'Post',
            url: 'replenishMent.aspx',
            data: {
                ID: ID,
                op: 'search'
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "成功") {
                    window.location.href = "../InventoryMGT/checkRs.aspx";
                }
            }
        })
    })
    //点击删除
    $("#table").delegate(".btn_del", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        var state = $(this).parent().prev().text().trim();
        if (state == "已完成") {
            swal({
                title: "温馨提示:)",
                text: "删除后将无法恢复，您确定要删除吗？？？",
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
                    type: 'Post',
                    url: 'replenishMent.aspx',
                    data: {
                        ID: ID,
                        op: 'del'
                    },
                    dataType: 'text',
                    success: function (succ) {
                        if (succ == "删除成功") {
                            swal({
                                title: "提示",
                                text: "删除成功",
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            })
                        } else {
                            swal({
                                title: "提示",
                                text: succ,
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            })
                        }
                    }
                })
            })
        } else {
            swal({
                title: "提示",
                text: "该补货单未完成，不能删除",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            })
        }

    })
    //点击查询按钮时
    $("#btn_search").click(function () {
        var search = $("#customer").val().trim();
        $.ajax({
            type: 'Post',
            url: 'replenishMent.aspx',
            data: {
                search: search,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#intPageCount").remove();
                $("#table tr:not(:first)").remove(); //清空table处首行
                $("#table").append(data); //加载table
                $(".paging").empty();
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
                            url: 'tradeManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                search: search,
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