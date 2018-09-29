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
            var search = $("#btn-search").val().trim();
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
                    $("#table tr:not(:first)").empty(); //清空table处首行
                    $("#table").append(data); //加载table
                }
            });
        }
    });
    //点击查询按钮时
    $("#btn-search").click(function () {
        var search = $("#search_All").val().trim();
        $.ajax({
            type: 'Post',
            url: 'tradeManagement.aspx',
            data: {
                search: search,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#intPageCount").remove();
                $("#table tr:not(:first)").empty(); //清空table处首行
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
                                $("#table tr:not(:first)").empty(); //清空table处首行
                                $("#table").append(data); //加载table
                            }
                        });
                    }
                });
            }
        });
    });
    //添加销售任务
    $("#btnAdd").click(function () {
        var billCount = $("#billCount").val().trim();
        var totalPrice = $("#totalPrice").val().trim();
        var realPrice = $("#realPrice").val().trim();
        var Price = $("#Price").val().trim();
        var saleCustmer = $("#saleCustmer").val().trim();
        if (billCount=="" || totalPrice=="" || realPrice=="" || Price=="" || saleCustmer=="") {
            swal({
                title: "温馨提示:)",
                text: "不能含有未填项!",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'tradeManagement.aspx',
                data: {
                    Custmer: saleCustmer,
                    numberLimit: billCount,
                    priceLimit: totalPrice,
                    totalPriceLimit: realPrice,
                    defaultDiscount:Price,
                    op: "add"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "添加成功") {
                        swal({
                            title: "温馨提示",
                            text: "添加成功",
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window, location.reload();
                        })
                    } else {
                        swal({
                            title: "温馨提示",
                            text: succ,
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window, location.reload();
                        })
                    }
                }
            })
        }
    })
})