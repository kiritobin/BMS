﻿//时间选择器
jeDate("#startTime", {
    theme: {
        bgcolor: "#D91600",
        pnColor: "#FF6653"
    },
    multiPane: true,
    format: "YYYY-MM-DD"
});
jeDate("#endTime", {
    theme: {
        bgcolor: "#D91600",
        pnColor: "#FF6653"
    },
    multiPane: true,
    format: "YYYY-MM-DD"
});

//地址栏获取
//console.log(getUrlParam(location.href,"参数名"));
function getUrlParam(url, name) {
    var pattern = new RegExp("[?&]" + name + "\=([^&]+)", "g");
    var matcher = pattern.exec(url);
    var items = null;
    if (null != matcher) {
        try {
            items = decodeURIComponent(decodeURIComponent(matcher[1]));
        } catch (e) {
            try {
                items = decodeURIComponent(matcher[1]);
            } catch (e) {
                items = matcher[1];
            }
        }
    }
    return items;
}  

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
            var user = $("#user").val();
            var time = $("#time").val();
            var looktime = getUrlParam(location.href, "looktime");
            var state = $("#state").val();
            $.ajax({
                type: 'Post',
                url: 'salesDetails.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    isbn: isbn,
                    price: price,
                    discount: discount,
                    user: user,
                    time: time,
                    looktime: looktime,
                    state: state,
                    op: "paging"
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
                    $("#table tr:not(:first)").remove(); //清空table处首行
                    $("#table").append(data); //加载table
                    $("#intPageCount").remove();
                }
                , error: function (XMLHttpRequest, textStatus) { //请求失败
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
                , complete: function (XMLHttpRequest, status) { //请求完成后最终执行参数
                    setTimeout(function () {
                        $(".swal2-container").remove();
                    }, 1000);
                }
            });
        }
    });
    //清空时间
    $("#modalClose").click(function () {
        $("#time").val("");
        $("#myModal").modal('hide');
    });
    //选择时间后确定
    $("#btnOK").click(function () {
        var startTime = $("#startTime").val();
        var endTime = $("#endTime").val();
        if (startTime == "" || startTime == null) {
            swal({
                title: "提示",
                text: "请选择开始时间",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else if (endTime == "" || endTime == null) {
            swal({
                title: "提示",
                text: "请选择结束时间",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else {
            $("#time").val(startTime + "至" + endTime);
            $("#myModal").modal('hide');
        }
    });
    //点击查询按钮
    $("#search").click(function () {
        var isbn = $("#isbn").val();
        var price = $("#price").val();
        var discount = $("#discount").val();
        var user = $("#user").val();
        var time = $("#time").val();
        var looktime = getUrlParam(location.href, "looktime");
        var state = $("#state").val();
        $.ajax({
            type: 'Post',
            url: 'salesDetails.aspx',
            data: {
                isbn: isbn,
                price: price,
                discount: discount,
                user: user,
                time: time,
                looktime: looktime,
                state: state,
                op: "paging"
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
                            url: 'salesDetails.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                isbn: isbn,
                                price: price,
                                discount: discount,
                                user: user,
                                time: time,
                                state: state,
                                op: "paging"
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
                                $("#table tr:not(:first)").remove(); //清空table处首行
                                $("#table").append(data); //加载table
                                $("#intPageCount").remove();
                            }
                            , error: function (XMLHttpRequest, textStatus) { //请求失败
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
                            , complete: function (XMLHttpRequest, status) { //请求完成后最终执行参数
                                setTimeout(function () {
                                    $(".swal2-container").remove();
                                }, 1000);
                            }
                        });
                    }
                });
            }
            , error: function (XMLHttpRequest, textStatus) { //请求失败
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
            , complete: function (XMLHttpRequest, status) { //请求完成后最终执行参数
                setTimeout(function () {
                    $(".swal2-container").remove();
                }, 1000);
            }
        });
    });
    //导出
    $("#export").click(function () {
        var isbn = $("#isbn").val();
        var price = $("#price").val();
        var discount = $("#discount").val();
        var user = $("#user").val();
        var time = $("#time").val();
        var state = $("#state").val();
        var looktime = getUrlParam(location.href, "looktime");
        window.location.href = "salesDetails.aspx?op=export&&isbn=" + isbn + "&&price=" + price + "&&discount=" + discount + "&&user=" + user + "&&time=" + time + "&&state=" + state + "&&looktime=" + looktime;
    })
    //返回上一页
    $("#back").click(function () {
        window.location.href = "salesStatistics.aspx";
    })
    //$("#zhen").click(function () {
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
    //        var isbn = $("#isbn").val();
    //        var price = $("#price").val();
    //        var discount = $("#discount").val();
    //        var user = $("#user").val();
    //        var time = $("#time").val();
    //        var state = $("#state").val();
    //        var looktime = getUrlParam(location.href, "looktime");
    //        $.ajax({
    //            type: 'Post',
    //            url: 'salesDetails.aspx',
    //            data: {
    //                isbn: isbn,
    //                price: price,
    //                discount: discount,
    //                user: user,
    //                time: time,
    //                state: state,
    //                looktime: looktime,
    //                op: "print"
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
        var user = $("#user").val();
        var time = $("#time").val();
        var looktime = getUrlParam(location.href, "looktime");
        var state = $("#state").val();
        $.ajax({
            type: 'Post',
            url: 'salesDetails.aspx',
            data: {
                isbn: isbn,
                price: price,
                discount: discount,
                user: user,
                time: time,
                state: state,
                looktime: looktime,
                op: "print"
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
                $("#pname").html("<h3>销售明细</h3>");
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
        var user = $("#user").val();
        var time = $("#time").val();
        var looktime = getUrlParam(location.href, "looktime");
        var state = $("#state").val();
        $.ajax({
            type: 'Post',
            url: 'salesDetails.aspx',
            data: {
                isbn: isbn,
                price: price,
                discount: discount,
                user: user,
                time: time,
                state: state,
                looktime: looktime,
                op: "print"
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
                $("#pname").html("<h3>销售明细</h3>");
                $(".swal2-container").remove();
                $("#print_table tr:not(:first)").remove(); //清空table处首行
                $("#print_table").append(data); //加载table
                $('#printContent').show();
                $("#printContent").jqprint({importCSS: true});
                $('#printContent').hide();

                //$(".swal2-container").remove();
                //$("#print_table tr:not(:first)").remove(); //清空table处首行
                //$("#print_table").append(data); //加载table
                //$('#printContent').show();
                //dayin();
                //$("#printContent").jqprint();
                //var LODOP; //声明为全局变量 
                //LODOP = getLodop();
                //LODOP.PRINT_INIT("打印控件功能演示_Lodop功能_表单一");
                //LODOP.SET_PRINT_STYLE("FontSize", 18);
                //LODOP.SET_PRINT_STYLE("Bold", 1);
                //LODOP.ADD_PRINT_TEXT(10, 500, 260, 39, "打印页面部分内容");
                ////var strBodyStyle = "<style>table td{ border: 1 solid #000000;text-align:center; };table,tr,th{ border: 1 solid #000000}</style>";
                //var strBodyStyle = "<style>#pname{margin-left:-800px}#print_table{width:45%}#print_table td{border: 1px;border-style: solid;border-color: #666666;} #print_table tr td:nth-child(1){font-size:18px;width:1%;}#print_table tr td:nth-child(2){font-size:18px;width:1%;}#print_table tr td:nth-child(3){font-size:18px;width:5%;}#print_table tr td:nth-child(4){font-size:18px;width:1%;}#print_table tr td:nth-child(5){font-size:18px;width:1%;}#print_table tr td:nth-child(7){font-size:18px;width:1%;}#print_table tr td:nth-child(6){font-size:18px;width:1%;}</style>";
                //var strFormHtml = strBodyStyle + "<body>" + document.getElementById("printContent").innerHTML + "</body>";
                //LODOP.ADD_PRINT_HTM(50, 10, 2000, 700, strFormHtml);
                //LODOP.SET_PRINT_MODE("PRINT_PAGE_PERCENT", "Full-Width");
                //LODOP.PREVIEW();
                //$('#printContent').hide();
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
        LODOP.ADD_PRINT_TEXT(iCurLine, 455, 50, 20, row.eq(i).find('td').eq(5).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 490, 50, 20, row.eq(i).find('td').eq(6).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 530, 50, 20, row.eq(i).find('td').eq(7).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 570, 50, 20, row.eq(i).find('td').eq(8).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 610, 100, 20, row.eq(i).find('td').eq(9).text().trim());
        if (row.eq(i).find('td').eq(10).text().trim().length > 5) {
            LODOP.ADD_PRINT_TEXT(iCurLine, 685, 50, 20, row.eq(i).find('td').eq(10).text().trim());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 6);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 0);
        }
        else {
            LODOP.ADD_PRINT_TEXT(iCurLine, 685, 50, 20, row.eq(i).find('td').eq(10).text().trim());
        }
        if (row.eq(i).find('td').eq(11).text().trim().length > 10) {
            LODOP.ADD_PRINT_TEXT(iCurLine, 730, 85, 20, row.eq(i).find('td').eq(11).text().trim());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 6);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 0);
        }
        else {
            LODOP.ADD_PRINT_TEXT(iCurLine, 730, 85, 20, row.eq(i).find('td').eq(11).text().trim());
        }
        LODOP.ADD_PRINT_TEXT(iCurLine, 805, 85, 20, row.eq(i).find('td').eq(12).text().trim());
        iCurLine = iCurLine + 45;//每行占25px
        LODOP.ADD_PRINT_LINE(iCurLine - 5, 14, iCurLine - 5, 900, 0, 1);//横线
        //竖线
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 14, iCurLine - 50 + 45, 14, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 95, iCurLine - 50 + 45, 95, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 215, iCurLine - 50 + 45, 215, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 410, iCurLine - 50 + 45, 410, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 450, iCurLine - 50 + 45, 450, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 485, iCurLine - 50 + 45, 485, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 525, iCurLine - 50 + 45, 525, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 565, iCurLine - 50 + 45, 565, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 605, iCurLine - 50 + 45, 605, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 680, iCurLine - 50 + 45, 680, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 725, iCurLine - 50 + 45, 725, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 800, iCurLine - 50 + 45, 800, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 50, 900, iCurLine - 50 + 45, 900, 0, 1);
    }
    LODOP.ADD_PRINT_LINE(iCurLine, 14, iCurLine, 860, 0, 1);
    LODOP.ADD_PRINT_LINE(iCurLine, 14, iCurLine, 14, 0, 1);
    //LODOP.ADD_PRINT_TEXT(iCurLine + 5, 20, 300, 20, "打印时间：" + (new Date()).toLocaleDateString() + " " + (new Date()).toLocaleTimeString());
    //LODOP.ADD_PRINT_TEXT(iCurLine + 5, 346, 150, 20, "合计金额：" + document.getElementById("HJ").value);
    LODOP.SET_PRINT_PAGESIZE(3, 2200, 100, "");//这里3表示纵向打印且纸高“按内容的高度”；1385表示纸宽138.5mm；45表示页底空白4.5mm
    LODOP.SET_PRINT_MODE("PRINT_PAGE_PERCENT","Full-Width");
    LODOP.PREVIEW();
};
function AddTitle() {
    LODOP = getLodop();
    LODOP.PRINT_INIT("销售明细");
    LODOP.SET_PRINT_PAGESIZE(3, 2000, 100, "");
    LODOP.ADD_PRINT_TEXT(6, 355, 120, 30, "销售明细");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
    LODOP.ADD_PRINT_TEXT(50, 15, 100, 20, "商品编号");
    LODOP.ADD_PRINT_TEXT(50, 120, 150, 20, "书号");
    LODOP.ADD_PRINT_TEXT(50, 220, 250, 20, "商品名称");
    LODOP.ADD_PRINT_TEXT(50, 415, 100, 20, "定价");
    LODOP.ADD_PRINT_TEXT(50, 455, 100, 20, "数量");
    LODOP.ADD_PRINT_TEXT(50, 490, 100, 20, "码洋");
    LODOP.ADD_PRINT_TEXT(50, 530, 100, 20, "实洋");
    LODOP.ADD_PRINT_TEXT(50, 570, 100, 20, "销折");
    LODOP.ADD_PRINT_TEXT(50, 610, 100, 20, "时间");
    LODOP.ADD_PRINT_TEXT(50, 685, 100, 20, "操作员");
    LODOP.ADD_PRINT_TEXT(50, 730, 100, 20, "供应商");
    LODOP.ADD_PRINT_TEXT(50, 805, 100, 20, "采集状态");
    //横线
    LODOP.ADD_PRINT_LINE(45, 14, 45, 900, 0, 1);
    LODOP.ADD_PRINT_LINE(75, 14, 75, 900, 0, 1);
    //竖线
    LODOP.ADD_PRINT_LINE(45, 14, 75, 14, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 95, 75, 95, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 215, 75, 215, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 410, 75, 410, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 450, 75, 450, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 485, 75, 485, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 525, 75, 525, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 565, 75, 565, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 605, 75, 605, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 680, 75, 680, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 725, 75, 725, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 800, 75, 800, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 900, 75, 900, 0, 1);
};

function dayin() {
    var LODOP; //声明为全局变量 
    LODOP = getLodop();
    LODOP.PRINT_INIT("销售明细");
    //var printStyleCss = "<link rel='stylesheet' type='text/css' href='../css/materialdesignicons.min.css'>";
    var strBodyStyle = "<style>#print_table input{ background:white}#pname{margin-left:-800px}#print_table{width:45%;}#print_table td{border: 1px;border-style: solid;border-color: #666666;} #print_table tr td:nth-child(1){font-size:15px;width:1%;}#print_table tr td:nth-child(2){font-size:15px;width:1%;}#print_table tr td:nth-child(3){font-size:15px;width:5%;}#print_table tr td:nth-child(4){font-size:15px;width:1%;}#print_table tr td:nth-child(5){font-size:15px;width:1%;}#print_table tr td:nth-child(7){font-size:15px;width:1%;}#print_table tr td:nth-child(8){font-size:15px;width:1%;}#print_table tr td:nth-child(6){font-size:15px;width:1%;}</style>";
    var strFormHtml = strBodyStyle + "<body>" + document.getElementById("printContent").innerHTML + "</body>";
    LODOP.SET_PRINT_PAGESIZE(1, 240, 280, "多联");
    LODOP.ADD_PRINT_HTM(25, 20, 1500,820, strFormHtml);
    LODOP.PREVIEW();
}