﻿jeDate("#startTime", {
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

window.onload = function () {
    //$("#groupregion").hide();
    $("#groupPayType").hide();
}

$(document).ready(function () {
    $("#printRegions").hide();
    $("#regions").hide();
    $("#printContent").hide();
    $('.paging').pagination({
        pageCount: $("#intPageCount").val(), //总页数
        jump: true,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            var groupby = $("#groupby").find("option:selected").text();
            var regionName = $("#region").find("option:selected").text();
            var payType = $("#payType").find("option:selected").text();
            var groupbyType;
            if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部组织") {
                    regionName = "";
                }
                payType = "";
            } else if (groupby == "支付方式") {
                if (payType == "全部支付方式") {
                    payType = "";
                }
                groupbyType = "payment";
                regionName = "";
            } else {
                groupbyType = "state";
                regionName = "";
                payType = "";
            }
            if (groupbyType == "state") {
                swal({
                    title: "提示",
                    text: "请选择分组方式",
                    type: "warning",
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: '确定',
                    confirmButtonClass: 'btn btn-success',
                    buttonsStyling: false,
                    allowOutsideClick: false
                });
            } else {
                var time = $("#time").val();
                var page = api.getCurrent();
                $.ajax({
                    type: 'Post',
                    url: 'retailStatistics.aspx',
                    data: {
                        page: api.getCurrent(), //页码
                        groupbyType: groupbyType,
                        regionName: regionName,
                        time: time,
                        payment: payType,
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
                        if (groupby == "组织") {
                            $("#showType").text("组织");
                        } else if (groupby == "支付方式") {
                            $("#showType").text("支付方式");
                        } else {
                            $("#showType").text("支付方式");
                        }
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
        }
    });
    $(".paging").hide();
    //导出报表
    $("#exportAll").click(function () {
        if (!$("#table td:visible").length) {
            swal({
                title: "无查询条件或无数据",
                text: "若以选择条件请先点击查询再导出",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else {
            var groupby = $("#groupby").find("option:selected").text();
            var regionName = $("#region").find("option:selected").text();
            var payType = $("#payType").find("option:selected").text();
            var groupbyType;
            if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部组织") {
                    regionName = "";
                }
                payType = "";
            } else if (groupby == "支付方式") {
                if (payType == "全部支付方式") {
                    payType = "";
                }
                groupbyType = "payment";
                regionName = "";
            } else {
                groupbyType = "state";
                regionName = "";
                payType = "";
            }
            if (groupbyType == "state") {
                swal({
                    title: "提示",
                    text: "请选择分组方式",
                    type: "warning",
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: '确定',
                    confirmButtonClass: 'btn btn-success',
                    buttonsStyling: false,
                    allowOutsideClick: false
                });
            } else {
                var time = $("#time").val();
                window.location.href = "retailStatistics.aspx?op=exportAll&&groupbyType=" + groupbyType + "&&regionName=" + regionName + "&&time=" + time + "&&payment=" + payType;
            }
        }
    });
    //导出报表明细
    $("#exportDe").click(function () {
        if (!$("#table td:visible").length) {
            swal({
                title: "无查询条件或无数据",
                text: "若以选择条件请先点击查询再导出",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else {
            var groupby = $("#groupby").find("option:selected").text();
            var regionName = $("#region").find("option:selected").text();
            var payType = $("#payType").find("option:selected").text();
            var groupbyType;
            if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部组织") {
                    regionName = "";
                }
                payType = "";
            } else if (groupby == "支付方式") {
                if (payType == "全部支付方式") {
                    payType = "";
                }
                groupbyType = "payment";
                regionName = "";
            } else {
                groupbyType = "state";
                regionName = "";
                payType = "";
            }
            if (groupbyType == "state") {
                swal({
                    title: "提示",
                    text: "请选择分组方式",
                    type: "warning",
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: '确定',
                    confirmButtonClass: 'btn btn-success',
                    buttonsStyling: false,
                    allowOutsideClick: false
                });
            } else {
                var time = $("#time").val();
                window.location.href = "retailStatistics.aspx?op=exportDe&&groupbyType=" + groupbyType + "&&regionName=" + regionName + "&&time=" + time + "&&payment=" + payType;
            }
        }
    })
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

    //查看详情
    $("#table").delegate(".look", "click", function (e) {
        var looktime = $("#time").val();
        var groupby = $("#groupby").find("option:selected").text();
        var groupbyType;
        var region = "";
        if (groupby == "组织") {
            groupbyType = "regionName";
        } else if (groupby == "支付方式") {
            groupbyType = "payment";
            region = $(this).parent().prev().prev().prev().prev().prev().prev().text().trim();
        } else {
            groupbyType = "payment";
        }
        var name = $(this).parent().prev().prev().prev().prev().prev().text().trim();
        window.location.href = "retailDetails.aspx?type=" + groupbyType + "&name=" + name + "&looktime=" + looktime + "&region=" + region;
    });

    //点击查询按钮时
    $("#btn_search").click(function () {
        var groupby = $("#groupby").find("option:selected").text();//获取分组方式
        var regionName = $("#region").find("option:selected").text();//获取组织条件
        var payType = $("#payType").find("option:selected").text();//获取支付方式条件
        var groupbyType;
        if (groupby == "组织") {
            groupbyType = "regionName";
            if (regionName == "全部组织") {
                regionName = "";
            }
            if (payType == "全部支付方式") {
                payType = "";
            }
        } else if (groupby == "支付方式") {
            if (payType == "全部支付方式") {
                payType = "";
            }
            groupbyType = "payment";
            if (regionName == "全部组织") {
                regionName = "";
                $("#regions").hide();//当分组方式为支付方式且组织条件为所有时，隐藏表格组织表头
            }
        } else {
            groupbyType = "state";
            regionName = "";
            payType = "";
        }
        if (groupbyType == "state") {
            swal({
                title: "提示",
                text: "请选择分组方式",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        } else {
            var time = $("#time").val();
            $.ajax({
                type: 'Post',
                url: 'retailStatistics.aspx',
                data: {
                    groupbyType: groupbyType,
                    regionName: regionName,
                    time: time,
                    payment: payType,
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
                    if (groupby == "组织") {
                        $("#showType").text("组织");
                    } else {
                        $("#showType").text("支付方式");
                        if (regionName != "") {
                            $("#regions").show();
                        }
                    }
                    $("#intPageCount").remove();
                    $("#table tr:not(:first)").empty(); //清空table处首行
                    $("#table").append(data); //加载table
                    $(".paging").empty();
                    $('.paging').pagination({
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
                                url: 'retailStatistics.aspx',
                                data: {
                                    page: api.getCurrent(), //页码
                                    groupbyType: groupbyType,
                                    regionName: regionName,
                                    time: time,
                                    payment: payType,
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
                                    if (groupby == "组织") {
                                        $("#showType").text("组织");
                                    } else if (groupby == "支付方式") {
                                        $("#showType").text("支付方式");
                                    } else {
                                        $("#showType").text("支付方式");
                                    }
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
        }

    });
    //分组方式改变
    $("#groupby").change(function () {
        var groupby = $("#groupby").find("option:selected").text();
        if (groupby == "组织") {
            $("#groupregion").show();
            $("#groupPayType").hide();
        } else if (groupby == "支付方式") {
            $("#groupregion").show();
            $("#groupPayType").show();
        } else {
            $("#groupregion").show();
            $("#groupPayType").hide();
        }
    })

    $("#print").click(function () {
        var t = $("#table").find('tr').length;
        //alert(t);
        if (t <= 1) {
            swal({
                title: "提示",
                text: "请先查询你要打印的内容",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-warning',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        }
        else {
            $("#printmodel").modal("show");
        }
    })
})

$("#a4").click(function () {
    var regionName = $("#region").find("option:selected").text();
    $("#changeprint").attr("href", "../css/a4print.css");
    var groupby = $("#groupby").find("option:selected").text();
    var t = $("#table").find('tr').length;
    if (t <= 1) {
        swal({
            title: "提示",
            text: "请先查询你要打印的内容",
            type: "warning",
            confirmButtonColor: '#3085d6',
            confirmButtonText: '确定',
            confirmButtonClass: 'btn btn-warning',
            buttonsStyling: false,
            allowOutsideClick: false
        });
    }
    else {
        $.ajax({
            type: 'Post',
            url: 'retailStatistics.aspx',
            data: {
                op: 'print'
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
                $("#pname").html("<h3>零售统计</h3>");
                $(".swal2-container").remove();
                $("#printTabled tr:not(:first)").remove();
                $("#printTabled").append(data);
                $('#printContent').show();
                if (groupby == "支付方式") {
                    $("#printShowType").text("支付方式");
                    if (regionName != "" && regionName != null) {
                        $("#printRegions").show();
                    }
                }
                else if (groupby == "组织") {
                    $("#printShowType").text("组织");
                }
                $("#printContent").jqprint();
                $('#printContent').hide();
            },
            error: function (XMLHttpRequest, textStatus) { //请求失败
                $(".swal2-container").remove();
                $('#printTabled').hide();
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
    }
})
$("#zhen").click(function () {
    $("#changeprint").attr("href", "../css/duolianprint.css");
    var groupby = $("#groupby").find("option:selected").text();
    var t = $("#table").find('tr').length;
    if (t <= 1) {
        swal({
            title: "提示",
            text: "请先查询你要打印的内容",
            type: "warning",
            confirmButtonColor: '#3085d6',
            confirmButtonText: '确定',
            confirmButtonClass: 'btn btn-warning',
            buttonsStyling: false,
            allowOutsideClick: false
        });
    }
    else {
        $.ajax({
            type: 'Post',
            url: 'retailStatistics.aspx',
            data: {
                op: 'print'
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
                $("#pname").html("<h3>零售统计</h3>");
                $(".swal2-container").remove();
                $("#printTabled tr:not(:first)").remove();
                $("#printTabled").append(data);
                $('#printContent').show();
                if (groupby == "支付方式") {
                    $("#printShowType").text("支付方式");
                }
                else if (groupby == "组织") {
                    $("#printShowType").text("组织");
                }
                $("#printContent").jqprint({importCSS:true});
                $('#printContent').hide();
            },
            error: function (XMLHttpRequest, textStatus) { //请求失败
                $(".swal2-container").remove();
                $('#printTabled').hide();
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
    }
})
//$("#zhen").click(function () {
//    $.ajax({
//        type: 'Post',
//        url: 'retailStatistics.aspx',
//        dataType: 'text',
//        data: {
//            op: "print"
//        },
//        beforeSend: function (XMLHttpRequest) { //开始请求
//            swal({
//                text: "正在获取数据",
//                imageUrl: "../imgs/load.gif",
//                imageHeight: 100,
//                imageWidth: 100,
//                width: 180,
//                showConfirmButton: false,
//                allowOutsideClick: false
//            });
//        },
//        success: function (data) {
//            $(".swal2-container").remove();
//            $("#print_table tr:not(:first)").remove(); //清空table处首行
//            $("#print_table").append(data); //加载table 
//            try {
//                MyPreview();
//            }
//            catch{
//                window.location.href = "/CLodop_Setup_for_Win32NT.html";
//            }
//        },
//        error: function (XMLHttpRequest, textStatus) { //请求失败
//            $(".swal2-container").remove();
//            if (textStatus == 'timeout') {
//                var xmlhttp = window.XMLHttpRequest ? new window.XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHttp");
//                xmlhttp.abort();
//                swal({
//                    title: "提示",
//                    text: "请求超时",
//                    type: "warning",
//                    confirmButtonColor: '#3085d6',
//                    confirmButtonText: '确定',
//                    confirmButtonClass: 'btn btn-success',
//                    buttonsStyling: false,
//                    allowOutsideClick: false
//                });
//            } else if (textStatus == "error") {
//                swal({
//                    title: "提示",
//                    text: "服务器内部错误",
//                    type: "warning",
//                    confirmButtonColor: '#3085d6',
//                    confirmButtonText: '确定',
//                    confirmButtonClass: 'btn btn-success',
//                    buttonsStyling: false,
//                    allowOutsideClick: false
//                });
//            }
//        }
//    })
//})

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
            url: 'retailStatistics.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

var LODOP; //声明为全局变量
function MyPreview() {
    AddTitle();
    var iCurLine = 75;//标题行之后的数据从位置80px开始打印
    var j = $("#printTabled").find("tr").length;
    var row = $("#printTabled").find('tr');
    for (i = 1; i < j; i++) {
        LODOP.ADD_PRINT_TEXT(iCurLine, 15, 50, 20, i);
        if (row.eq(i).find('td').eq(1).text().trim().length > 12) {
            LODOP.ADD_PRINT_TEXT(iCurLine, 70, 200, 20, row.eq(i).find('td').eq(1).text().trim());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 6);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 0);
        }
        else {
            LODOP.ADD_PRINT_TEXT(iCurLine, 70, 200, 20, row.eq(i).find('td').eq(1).text().trim());
        }
        LODOP.ADD_PRINT_TEXT(iCurLine, 270, 80, 20, row.eq(i).find('td').eq(2).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 330, 50, 20, row.eq(i).find('td').eq(3).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 380, 100, 20, row.eq(i).find('td').eq(4).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 480, 100, 20, row.eq(i).find('td').eq(5).text().trim());
        iCurLine = iCurLine + 25;//每行占25px
        LODOP.ADD_PRINT_LINE(iCurLine - 5, 14, iCurLine - 5, 580, 0, 1);//横线
        //竖线
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 14, iCurLine - 30 + 25, 14, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 65, iCurLine - 30 + 25, 65, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 265, iCurLine - 30 + 25, 265, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 325, iCurLine - 30 + 25, 325, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 375, iCurLine - 30 + 25, 375, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 475, iCurLine - 30 + 25, 475, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 580, iCurLine - 30 + 25, 580, 0, 1);
    }
    LODOP.ADD_PRINT_LINE(iCurLine, 14, iCurLine, 580, 0, 1);
    LODOP.ADD_PRINT_LINE(iCurLine, 14, iCurLine, 14, 0, 1);
    LODOP.SET_PRINT_PAGESIZE(3, 1800, 100, "");//这里3表示纵向打印且纸高“按内容的高度”；1385表示纸宽138.5mm；45表示页底空白4.5mm
    LODOP.PREVIEW();
};
function AddTitle() {
    var LODOP; //声明为全局变量
    var group = $("#table").find('tr').eq(0).find('th').eq(1).text().trim();
    var kinds = $("#table").find('tr').eq(0).find('th').eq(2).text().trim();
    var num = $("#table").find('tr').eq(0).find('th').eq(3).text().trim();
    var totalPrice = $("#table").find('tr').eq(0).find('th').eq(4).text().trim();
    var realPrice = $("#table").find('tr').eq(0).find('th').eq(5).text().trim();
    LODOP = getLodop();

    LODOP.PRINT_INIT("零售统计");
    LODOP.SET_PRINT_PAGESIZE(3, 1800, 100, "");
    LODOP.ADD_PRINT_TEXT(3, 231, 120, 30, "零售统计");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
    LODOP.ADD_PRINT_TEXT(50, 15, 50, 20, "序号");
    LODOP.ADD_PRINT_TEXT(50, 70, 200, 20, group);
    LODOP.ADD_PRINT_TEXT(50, 270, 50, 20, kinds);
    LODOP.ADD_PRINT_TEXT(50, 330, 100, 20, num);
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
    LODOP.ADD_PRINT_TEXT(50, 380, 100, 20, totalPrice);
    LODOP.ADD_PRINT_TEXT(50, 480, 50, 20, realPrice);
    //横线
    LODOP.ADD_PRINT_LINE(45, 14, 45, 580, 0, 1);
    LODOP.ADD_PRINT_LINE(70, 14, 70, 580, 0, 1);
    //竖线
    LODOP.ADD_PRINT_LINE(45, 14, 70, 14, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 65, 70, 65, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 265, 70, 265, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 325, 70, 325, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 375, 70, 375, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 475, 70, 475, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 580, 70, 580, 0, 1);
};