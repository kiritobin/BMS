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
            url: 'inventoryManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}
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
            var bookName = $("#bookName").val();
            var bookArea = $("#area").val();
            var bookISBN = $("#isbn").val();
            var supplier = $("#supplier").val();
            var stockNumber = $("#stock").val();
            $.ajax({
                type: 'Post',
                url: 'inventoryManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    supplier: supplier,
                    stockNumber: stockNumber,
                    bookName: bookName,
                    bookArea: bookArea,
                    bookISBN: bookISBN,
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

    $("#btn-search").click(function () {
        var bookName = $("#bookName").val();
        var bookArea = $("#area").val();
        var bookISBN = $("#isbn").val();
        var supplier = $("#supplier").val();
        var stockNumber = $("#stock").val();
        $.ajax({
            type: 'Post',
            url: 'inventoryManagement.aspx',
            data: {
                supplier: supplier,
                stockNumber: stockNumber,
                bookName: bookName,
                bookArea: bookArea,
                bookISBN: bookISBN,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#intPageCount").remove();
                $("#table tr:not(:first)").remove(); //清空table处首行
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
                            url: 'inventoryManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                supplier: supplier,
                                stockNumber: stockNumber,
                                bookName: bookName,
                                bookArea: bookArea,
                                bookISBN: bookISBN,
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