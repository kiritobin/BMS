$(document).ready(function () {
    $("#printContent").hide();
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

    //$("#zhen").click(function () {
    //    var isbn = $("#isbn").val();
    //    var price = $("#price").val();
    //    var discount = $("#discount").val();
    //    var bookName = $("#bookName").val();
    //    var stockNumber = $("#stock").val();
    //    var t = $("#table").find('tr').length;
    //    if (t <= 1) {
    //        swal({
    //            title: "提示",
    //            text: "请先查询你要打印的内容",
    //            type: "warning",
    //            confirmButtonColor: '#3085d6',
    //            confirmButtonText: '确定',
    //            confirmButtonClass: 'btn btn-warning',
    //            buttonsStyling: false,
    //            allowOutsideClick: false
    //        });
    //    }
    //    else {
    //        $.ajax({
    //            type: 'Post',
    //            url: 'bookStockDetail.aspx',
    //            data: {
    //                op: "print",
    //                bookName:bookName,
    //                stockNumber:stockNumber,
    //                isbn:isbn,
    //                price:price,
    //                discount:discount
    //            },
    //            dataType: 'text',
    //            beforeSend: function (XMLHttpRequest) { //开始请求
    //                swal({
    //                    text: "正在获取数据",
    //                    imageUrl: "../imgs/load.gif",
    //                    imageHeight: 100,
    //                    imageWidth: 100,
    //                    width: 180,
    //                    showConfirmButton: false,
    //                    allowOutsideClick: false
    //                });
    //                //$("#printmodel").modal("toggle");
    //            },
    //            success: function (data) {
    //                $(".swal2-container").remove();
    //                $("#print_table tr:not(:first)").remove(); //清空table处首行
    //                $("#print_table").append(data); //加载table
    //                try {
    //                    MyPreview();
    //                }
    //                catch{
    //                    window.location.href = "/CLodop_Setup_for_Win32NT.html";
    //                }
    //            },
    //            error: function (XMLHttpRequest, textStatus) { //请求失败
    //                $(".swal2-container").remove();
    //                if (textStatus == 'timeout') {
    //                    var xmlhttp = window.XMLHttpRequest ? new window.XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHttp");
    //                    xmlhttp.abort();
    //                    swal({
    //                        title: "提示",
    //                        text: "请求超时",
    //                        type: "warning",
    //                        confirmButtonColor: '#3085d6',
    //                        confirmButtonText: '确定',
    //                        confirmButtonClass: 'btn btn-success',
    //                        buttonsStyling: false,
    //                        allowOutsideClick: false
    //                    });
    //                } else if (textStatus == "error") {
    //                    swal({
    //                        title: "提示",
    //                        text: "服务器内部错误",
    //                        type: "warning",
    //                        confirmButtonColor: '#3085d6',
    //                        confirmButtonText: '确定',
    //                        confirmButtonClass: 'btn btn-success',
    //                        buttonsStyling: false,
    //                        allowOutsideClick: false
    //                    });
    //                }
    //            }
    //        })
    //    }
    //})
    $("#a4").click(function () {
        $("#changeprint").attr("href", "../css/a4print.css");
        var isbn = $("#isbn").val();
        var price = $("#price").val();
        var discount = $("#discount").val();
        var bookName = $("#bookName").val();
        var stockNumber = $("#stock").val();
        $.ajax({
            type: 'Post',
            url: 'bookStockDetail.aspx',
            data: {
                op: "print",
                bookName: bookName,
                stockNumber: stockNumber,
                isbn: isbn,
                price: price,
                discount: discount
            },
            dataType: 'text',
            beforeSend: function (XMLHttpRequest) { //开始请求
                swal({
                    text: "正在获取数据",
                    imageUrl: "../imgs/load.gif",
                    imageHeight: 100,
                    imageWidth: 100,
                    width: 280,
                    showConfirmButton: true,
                    allowOutsideClick: false
                });
            },
            success: function (data) {
                $("#pname").html("<h3>库存明细</h3>");
                $(".swal2-container").remove();
                $("#print_table tr:not(:first)").remove(); //清空table处首行
                $("#print_table").append(data); //加载table
                $('#printContent').show();
                $("#printContent").jqprint();
                $('#printContent').hide();
            },
            error: function (XMLHttpRequest, textStatus) { //请求失败
                $(".swal2-container").remove();
                $('#printContent').hide();
                if (textStatus == 'timeout') {
                    var xmlhttp = window.XMLHttpRequest ? new window.XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHttp");
                    xmlhttp.abort();
                    swal({
                        title: "提示",
                        text: "请求超时",
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    });
                } else if (textStatus == "error") {
                    swal({
                        title: "提示",
                        text: "服务器内部错误",
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    });
                }
            }
        })
    })
    $("#zhen").click(function () {
        $("#changeprint").attr("href", "../css/duolianprint.css");
        var isbn = $("#isbn").val();
        var price = $("#price").val();
        var discount = $("#discount").val();
        var bookName = $("#bookName").val();
        var stockNumber = $("#stock").val();
        $.ajax({
            type: 'Post',
            url: 'bookStockDetail.aspx',
            data: {
                op: "print",
                bookName: bookName,
                stockNumber: stockNumber,
                isbn: isbn,
                price: price,
                discount: discount
            },
            dataType: 'text',
            beforeSend: function (XMLHttpRequest) { //开始请求
                swal({
                    text: "正在获取数据",
                    imageUrl: "../imgs/load.gif",
                    imageHeight: 100,
                    imageWidth: 100,
                    width: 180,
                    showConfirmButton: false,
                    allowOutsideClick: false
                });
            },
            success: function (data) {
                $("#pname").html("<h3>库存明细</h3>");
                $(".swal2-container").remove();
                $("#print_table tr:not(:first)").remove(); //清空table处首行
                $("#print_table").append(data); //加载table
                $('#printContent').show();
                $("#printContent").jqprint({importCSS:true});
                $('#printContent').hide();
            },
            error: function (XMLHttpRequest, textStatus) { //请求失败
                $(".swal2-container").remove();
                $('#printContent').hide();
                if (textStatus == 'timeout') {
                    var xmlhttp = window.XMLHttpRequest ? new window.XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHttp");
                    xmlhttp.abort();
                    swal({
                        title: "提示",
                        text: "请求超时",
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    });
                } else if (textStatus == "error") {
                    swal({
                        title: "提示",
                        text: "服务器内部错误",
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    });
                }
            }
        })
    })

    $("#price_ok").click(function () {
        var type = $("input[name='priceRadios']:checked").val();
        var price = $("#inputprice").val();
        if (type == "" || type == null) {
            swal({
                title: "提示",
                text: "请选择类型",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        }
        else if (price == "" || price == null) {
            swal({
                title: "提示",
                text: "请输入定价",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        }
        else {
            $("#price").val("");
            $("#price").val(type + price);
            $("#priceModal").modal('hide');
        }
    })
    $("#price_clear").click(function () {
        $("#price").val("");
        $("#inputprice").val("");
        $("#priceModal").modal('hide');
    })
});

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

var LODOP; //声明为全局变量
function MyPreview() {
    AddTitle();
    var iCurLine = 80;//标题行之后的数据从位置80px开始打印
    var j = $("#print_table").find("tr").length;
    var row = $("#print_table").find('tr');
    for (i = 1; i < j; i++) {
        LODOP.ADD_PRINT_TEXT(iCurLine, 15, 100, 20, row.eq(i).find('td').eq(1).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 100, 150, 20, row.eq(i).find('td').eq(2).text().trim());
        if (row.eq(i).find('td').eq(3).text().trim().length > 19) {
            LODOP.ADD_PRINT_TEXT(iCurLine, 220, 200, 20, row.eq(i).find('td').eq(3).text().trim());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 6);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 0);
        }
        else {
            LODOP.ADD_PRINT_TEXT(iCurLine, 220, 200, 20, row.eq(i).find('td').eq(3).text().trim());
        }
        LODOP.ADD_PRINT_TEXT(iCurLine, 415, 50, 20, row.eq(i).find('td').eq(4).text().trim());
        if (row.eq(i).find('td').eq(8).text().trim().length > 5) {
            LODOP.ADD_PRINT_TEXT(iCurLine, 455, 50, 20, row.eq(i).find('td').eq(8).text().trim());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 5);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 0);
        }
        else {
            LODOP.ADD_PRINT_TEXT(iCurLine, 455, 50, 20, row.eq(i).find('td').eq(8).text().trim());
        }
        LODOP.ADD_PRINT_TEXT(iCurLine, 500, 50, 20, row.eq(i).find('td').eq(5).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 560, 50, 20, row.eq(i).find('td').eq(6).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 620, 50, 20, row.eq(i).find('td').eq(7).text().trim());
        if (row.eq(i).find('td').eq(9).text().trim().length > 10) {
            LODOP.ADD_PRINT_TEXT(iCurLine, 690, 90, 20, row.eq(i).find('td').eq(9).text().trim());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 5);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 0);
        }
        else {
            LODOP.ADD_PRINT_TEXT(iCurLine, 690, 90, 20, row.eq(i).find('td').eq(9).text().trim());
        }
        iCurLine = iCurLine + 45;//每行占25px
        LODOP.ADD_PRINT_LINE(iCurLine - 5, 14, iCurLine - 5, 770, 0, 1);//横线
        //竖线
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 14, iCurLine - 50 + 45, 14, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 95, iCurLine - 50 + 45, 95, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 215, iCurLine - 50 + 45, 215, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 410, iCurLine - 50 + 45, 410, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 450, iCurLine - 50 + 45, 450, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 495, iCurLine - 50 + 45, 495, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 555, iCurLine - 50 + 45, 555, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 615, iCurLine - 50 + 45, 615, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 685, iCurLine - 50 + 45, 685, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 770, iCurLine - 50 + 45, 770, 0, 1);
        //LODOP.ADD_PRINT_LINE(iCurLine - 50, 725, iCurLine - 50 + 45, 725, 0, 1);
        //LODOP.ADD_PRINT_LINE(iCurLine - 50, 800, iCurLine - 50 + 45, 800, 0, 1);
        //LODOP.ADD_PRINT_LINE(iCurLine - 50, 900, iCurLine - 50 + 45, 900, 0, 1);
    }
    //LODOP.ADD_PRINT_LINE(iCurLine, 14, iCurLine, 860, 0, 1);
    LODOP.ADD_PRINT_LINE(iCurLine, 14, iCurLine, 14, 0, 1);
    //LODOP.SET_PRINT_PAGESIZE(3, 2000, 100, "");//这里3表示纵向打印且纸高“按内容的高度”；1385表示纸宽138.5mm；45表示页底空白4.5mm
    LODOP.SET_PRINT_MODE("PRINT_PAGE_PERCENT","Full-Width");
    LODOP.PREVIEW();
};
function AddTitle() {
    var printISBN = $("#print_table").find('tr').eq(0).find('th').eq(1).text().trim();
    var printbookNum = $("#print_table").find('tr').eq(0).find('th').eq(2).text().trim();
    var printbookName = $("#print_table").find('tr').eq(0).find('th').eq(3).text().trim();
    var printprice = $("#print_table").find('tr').eq(0).find('th').eq(4).text().trim();
    var printsupplier = $("#print_table").find('tr').eq(0).find('th').eq(5).text().trim();
    var printstockNum = $("#print_table").find('tr').eq(0).find('th').eq(6).text().trim();
    var printinDiscount = $("#print_table").find('tr').eq(0).find('th').eq(7).text().trim();
    var printoutDiscount = $("#print_table").find('tr').eq(0).find('th').eq(8).text().trim();
    var printreginName = $("#print_table").find('tr').eq(0).find('th').eq(9).text().trim();

    LODOP = getLodop();

    LODOP.PRINT_INIT("库存明细");
    LODOP.ADD_PRINT_TEXT(7, 341, 115, 30, "库存明细");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
    LODOP.ADD_PRINT_TEXT(50, 15, 100, 20, printISBN);
    LODOP.ADD_PRINT_TEXT(50, 120, 150, 20, printbookNum);
    LODOP.ADD_PRINT_TEXT(50, 220, 250, 20, printbookName);
    LODOP.ADD_PRINT_TEXT(50, 415, 100, 20, printprice);
    LODOP.ADD_PRINT_TEXT(50, 455, 100, 20, printsupplier);
    LODOP.ADD_PRINT_TEXT(50, 500, 100, 20, printstockNum);
    LODOP.ADD_PRINT_TEXT(50, 560, 100, 20, printinDiscount);
    LODOP.ADD_PRINT_TEXT(50, 620, 100, 20, printoutDiscount);
    LODOP.ADD_PRINT_TEXT(50, 690, 100, 20, printreginName);
    //横线
    LODOP.ADD_PRINT_LINE(45, 14, 45, 770, 0, 1);
    LODOP.ADD_PRINT_LINE(75, 14, 75, 770, 0, 1);
    //竖线
    LODOP.ADD_PRINT_LINE(45, 14, 75, 14, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 95, 75, 95, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 215, 75, 215, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 410, 75, 410, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 450, 75, 450, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 495, 75, 495, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 555, 75, 555, 0, 1);
    //LODOP.ADD_PRINT_LINE(45, 565, 75, 565, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 615, 75, 615, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 685, 75, 685, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 770, 75, 770, 0, 1);
};