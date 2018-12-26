$(document).ready(function () {
    $("#print_table").hide();
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
            var isbn = $("#isbn").val();
            var price = $("#price").val();
            var discount = $("#discount").val();
            $.ajax({
                type: 'Post',
                url: 'bookStockDetail.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    isbn: isbn,
                    price: price,
                    discount: discount,
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
    //点击查询按钮
    $("#search").click(function () {
        var isbn = $("#isbn").val();
        var price = $("#price").val();
        var discount = $("#discount").val();
        var bookName = $("#bookName").val();
        var stockNumber = $("#stock").val();
        $.ajax({
            type: 'Post',
            url: 'bookStockDetail.aspx',
            data: {
                isbn: isbn,
                price: price,
                discount: discount,
                bookName: bookName,
                stockNumber: stockNumber,
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
                            url: 'bookStockDetail.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                isbn: isbn,
                                price: price,
                                discount: discount,
                                bookName: bookName,
                                stockNumber: stockNumber,
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
    //导出
    $("#export").click(function () {
        var isbn = $("#isbn").val();
        var price = $("#price").val();
        var discount = $("#discount").val();
        var bookName = $("#bookName").val();
        var stockNumber = $("#stock").val();
        window.location.href = "bookStockDetail.aspx?op=export&&bookName=" + bookName + "&&stockNumber=" + stockNumber+"&&isbn=" + isbn + "&&price=" + price + "&&discount=" + discount;
    })
    //返回上一页
    $("#back").click(function () {
        window.location.href = "bookStock.aspx";
    })
    $("#btn_number").click(function () {
        var type = $("input[name='optionsRadios']:checked").val();
        var number = $("#number").val();
        if (number == "" || number == null) {
            swal({
                title: "提示",
                text: "请输入数量",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else {
            $("#stock").val(type + number);
            $("#numberModal").modal('hide');
        }
    })
    $("#btn_clear").click(function () {
        $("#stock").val("");
        $("#number").val("");
        $("#numberModal").modal('hide');
    })
});

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
