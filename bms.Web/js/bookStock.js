﻿$(document).ready(function () {
    $("#printContent").hide();//隐藏打印内容
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
            var supplier = $("#supplier").find("option:selected").text();
            var regionName = $("#region").find("option:selected").text();
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
                if (supplier == "全部供应商") {
                    supplier = "";
                }
                regionName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部组织") {
                    regionName = "";
                }
                supplier = "";
            } else {
                groupbyType = "state";
                supplier = "";
                regionName = "";
            }
            var page = api.getCurrent();
            $.ajax({
                type: 'Post',
                url: 'bookStock.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    groupbyType: groupbyType,
                    supplier: supplier,
                    regionName: regionName,
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
                    if (groupby == "供应商") {
                        $("#showType").text("供应商");
                        $("#showType2").text("组织名称");
                    }
                    else if (groupby == "组织") {
                        $("#showType").text("组织名称");
                        $("#showType2").text("供应商");
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
            var supplier = $("#supplier").find("option:selected").text();
            var regionName = $("#region").find("option:selected").text();
            var regionId = $("#regionId").find("option:selected").val();
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
                if (supplier == "全部供应商") {
                    supplier = "";
                }
                regionName = "";
            }
            else if (groupby == "组织") {
                regionId = "";
                groupbyType = "regionName";
                if (regionName == "全部组织") {
                    regionName = "";
                }
                supplier = "";
            } else {
                groupbyType = "state";
                supplier = "";
                regionName = "";
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
            }
            window.location.href = "bookStock.aspx?op=exportAll&&groupbyType=" + groupbyType + "&&supplier=" + supplier + "&&regionName=" + regionName + "&&regionId="+regionId;
        }
    })
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
            var supplier = $("#supplier").find("option:selected").text();
            var regionName = $("#region").find("option:selected").text();
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
                if (supplier == "全部供应商") {
                    supplier = "";
                }
                regionName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部组织") {
                    regionName = "";
                }
                supplier = "";
            } else {
                groupbyType = "state";
                supplier = "";
                regionName = "";
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
            }
            window.location.href = "bookStock.aspx?op=exportDe&&groupbyType=" + groupbyType + "&&supplier=" + supplier + "&&regionName=" + regionName;
        }
    })
    //查看详情
    $("#table").delegate(".look", "click", function (e) {
        var groupby = $("#groupby").find("option:selected").text();
        var regionId = $(this).prev().val();
        var groupbyType;
        if (groupby == "供应商") {
            groupbyType = "supplier";
        }
        else if (groupby == "组织") {
            groupbyType = "regionName";
        } else {
            groupbyType = "supplier";
        }
        var name = $(this).parent().prev().prev().prev().prev().text();
        window.location.href = "bookStockDetail.aspx?type=" + groupbyType + "&&name=" + name + "&&regionId=" + regionId;
    })

    //点击查询按钮时
    $("#btn_search").click(function () {
        var groupby = $("#groupby").find("option:selected").text();
        var supplier = $("#supplier").find("option:selected").text();
        var regionName = $("#region").find("option:selected").text();
        var regionId = $("#regionId").find("option:selected").val();
        var groupbyType;
        if (groupby == "供应商") {
            groupbyType = "supplier";
            if (supplier == "全部供应商") {
                supplier = "";
            }
            regionName = "";
        }
        else if (groupby == "组织") {
            regionId = "";
            groupbyType = "regionName";
            if (regionName == "全部组织") {
                regionName = "";
            }
            supplier = "";
        } else {
            groupbyType = "state";
            supplier = "";
            regionName = "";
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
            $.ajax({
                type: 'Post',
                url: 'bookStock.aspx',
                data: {
                    groupbyType: groupbyType,
                    supplier: supplier,
                    regionName: regionName,
                    regionId:regionId,
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
                    if (groupby == "供应商") {
                        $("#showType").text("供应商");
                        $("#showType2").text("组织名称");
                    }
                    else if (groupby == "组织") {
                        $("#showType").text("组织名称");
                        $("#showType2").text("供应商");
                    }
                    $("#intPageCount").remove();
                    $("#table tr:not(:first)").empty(); //清空table处首行
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
                                url: 'bookStock.aspx',
                                data: {
                                    page: api.getCurrent(), //页码
                                    groupbyType: groupbyType,
                                    supplier: supplier,
                                    regionName: regionName,
                                    regionId: regionId,
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
                                    if (groupby == "供应商") {
                                        $("#showType").text("供应商");
                                    }
                                    else if (groupby == "组织") {
                                        $("#showType").text("组织");
                                    }
                                    $("#table tr:not(:first)").remove(); //清空table处首行
                                    $("#table").append(data); //加载table
                                    $("#intPageCount").remove();
                                }
                                , error: function (XMLHttpRequest, textStatus) { //请求失败
                                    $(".swal2-container").remove();
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
        if (groupby == "供应商") {
            $("#printshowType").text("供应商");
            $("#printshowType2").text("组织名称");
            $("#groupsupplier").show();
            $("#groupregionId").show();
            $("#groupregion").hide(); 
            $('#groupregion').selectpicker('refresh');
        }
        else if (groupby == "组织") {
            $("#printshowType").text("组织名称");
            $("#printshowType2").text("供应商");
            $("#groupsupplier").hide();
            $("#groupregionId").hide();
            $("#groupregion").show();
        } else if (groupby == "客户") {
            $("#groupsupplier").hide();
            $("#groupregion").hide();
        } else {
            $("#groupsupplier").hide();
            $("#groupregion").hide();
        }
    })

    //打印
    $("#print").click(function () {
        $("#printTabled").show();
        if (!$("#table td:visible").length) {
            swal({
                title: "无查询条件或无数据",
                text: "若以选择条件请先点击查询再打印",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            });
        }
        else {
            var groupby = $("#groupby").find("option:selected").text();
            var supplier = $("#supplier").find("option:selected").text();
            var regionName = $("#region").find("option:selected").text();
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
                if (supplier == "全部供应商") {
                    supplier = "";
                }
                regionName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部组织") {
                    regionName = "";
                }
                supplier = "";
            } else {
                groupbyType = "state";
                supplier = "";
                regionName = "";
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
            }
            else {
                $("#printmodel").modal("show");
            }
            //window.location.href = "bookStock.aspx?op=print&&groupbyType=" + groupbyType + "&&supplier=" + supplier + "&&regionName=" + regionName;
        }
    })

})

$("#a4").click(function () {
    $("#changeprint").attr("href", "../css/a4print.css");
    var groupby = $("#groupby").find("option:selected").text();
    var supplier = $("#supplier").find("option:selected").text();
    var regionName = $("#region").find("option:selected").text();
    var regionId = $("#regionId").find("option:selected").val();
    var groupbyType;
    if (groupby == "供应商") {
        groupbyType = "supplier";
        if (supplier == "全部供应商") {
            supplier = "";
        }
        regionName = "";
    }
    else if (groupby == "组织") {
        regionId = "";
        groupbyType = "regionName";
        if (regionName == "全部组织") {
            regionName = "";
        }
        supplier = "";
    } else {
        groupbyType = "state";
        supplier = "";
        regionName = "";
    }
    if (!$("#table td:visible").length) {
        swal({
            title: "无查询条件或无数据",
            text: "若以选择条件请先点击查询再打印",
            type: "warning",
            confirmButtonColor: '#3085d6',
            confirmButtonText: '确定',
            confirmButtonClass: 'btn btn-success',
            buttonsStyling: false,
            allowOutsideClick: false
        });
    }
    else {
        $.ajax({
            type: 'Post',
            url: 'bookStock.aspx',
            data: {
                op: "print",
                groupbyType: groupbyType,
                supplier: supplier,
                regionId: regionId,
                regionName: regionName
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
                $("#pname").html("<h3>库存统计</h3>");
                $(".swal2-container").remove();
                $("#printTabled tr:not(:first)").remove();
                $("#printTabled").append(data);
                $('#printContent').show();
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
    var supplier = $("#supplier").find("option:selected").text();
    var regionName = $("#region").find("option:selected").text();
    var regionId = $("#regionId").find("option:selected").val();
    var groupbyType;
    if (groupby == "供应商") {
        groupbyType = "supplier";
        if (supplier == "全部供应商") {
            supplier = "";
        }
        regionName = "";
    }
    else if (groupby == "组织") {
        regionId = "";
        groupbyType = "regionName";
        if (regionName == "全部组织") {
            regionName = "";
        }
        supplier = "";
    } else {
        groupbyType = "state";
        supplier = "";
        regionName = "";
    }
    if (!$("#table td:visible").length) {
        swal({
            title: "无查询条件或无数据",
            text: "若以选择条件请先点击查询再打印",
            type: "warning",
            confirmButtonColor: '#3085d6',
            confirmButtonText: '确定',
            confirmButtonClass: 'btn btn-success',
            buttonsStyling: false,
            allowOutsideClick: false
        });
    }
    else {
        $.ajax({
            type: 'Post',
            url: 'bookStock.aspx',
            data: {
                op: "print",
                groupbyType: groupbyType,
                supplier: supplier,
                regionId: regionId,
                regionName: regionName
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
                $("#pname").html("<h3>库存统计</h3>");
                $(".swal2-container").remove();
                $("#printTabled tr:not(:first)").remove();
                $("#printTabled").append(data);
                $('#printContent').show();
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
//    var groupby = $("#groupby").find("option:selected").text();
//    var supplier = $("#supplier").find("option:selected").text();
//    var regionName = $("#region").find("option:selected").text();
//    var groupbyType;
//    if (groupby == "供应商") {
//        groupbyType = "supplier";
//        if (supplier == "全部供应商") {
//            supplier = "";
//        }
//        regionName = "";
//    }
//    else if (groupby == "组织") {
//        groupbyType = "regionName";
//        if (regionName == "全部组织") {
//            regionName = "";
//        }
//        supplier = "";
//    } else {
//        groupbyType = "state";
//        supplier = "";
//        regionName = "";
//    }
//    $.ajax({
//        type: 'Post',
//        url: 'bookStock.aspx',
//        data: {
//            op: "print",
//            groupbyType: groupbyType,
//            supplier: supplier,
//            regionName: regionName
//        },
//        dataType: 'text',
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
//            if (groupby == "供应商") {
//                $("#printshowType").text("供应商");
//                $("#printshowType2").text("组织名称");
//            }
//            else if (groupby == "组织") {
//                $("#printshowType").text("组织名称");
//                $("#printshowType2").text("供应商");
//            }
//            $(".swal2-container").remove();
//            $("#printTabled tr:not(:first)").remove(); //清空table处首行
//            $("#printTabled").append(data); //加载table
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
//    });
//})

window.onload = function () {
    $("#groupsupplier").hide();
    $("#groupregion").hide();
}

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
            url: 'bookStock.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

var group = $("#printTabled").find('tr').eq(0).find('th').eq(1).text().trim();
var kinds = $("#printTabled").find('tr').eq(0).find('th').eq(2).text().trim();
var num = $("#printTabled").find('tr').eq(0).find('th').eq(3).text().trim();
var totalPrice = $("#printTabled").find('tr').eq(0).find('th').eq(4).text().trim();
//var realPrice = $("#printTabled").find('tr').eq(0).find('th').eq(5).text().trim();
var LODOP; //声明为全局变量
function MyPreview() {
    AddTitle();
    var iCurLine = 75;//标题行之后的数据从位置80px开始打印
    var j = $("#printTabled").find("tr").length;
    var row = $("#printTabled").find('tr');
    for (i = 1; i < j; i++) {
        LODOP.ADD_PRINT_TEXT(iCurLine, 15, 50, 20, i);
        if (row.eq(i).find('td').eq(3).text().trim().length > 15) {
            LODOP.ADD_PRINT_TEXT(iCurLine, 70, 200, 20, row.eq(i).find('td').eq(3).text().trim());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 6);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 0);
        }
        else {
            LODOP.ADD_PRINT_TEXT(iCurLine, 70, 200, 20, row.eq(i).find('td').eq(3).text().trim());
        }
        LODOP.ADD_PRINT_TEXT(iCurLine, 270, 100, 20, row.eq(i).find('td').eq(2).text().trim());
        LODOP.ADD_PRINT_TEXT(iCurLine, 370, 100, 20, row.eq(i).find('td').eq(1).text().trim());
        if (row.eq(i).find('td').eq(4).text().trim().length > 7) {
            LODOP.ADD_PRINT_TEXT(iCurLine, 470, 100, 20, row.eq(i).find('td').eq(4).text().trim());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 6);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 0);
        }
        else {
            LODOP.ADD_PRINT_TEXT(iCurLine, 470, 100, 20, row.eq(i).find('td').eq(4).text().trim());
        }
        iCurLine = iCurLine + 25;//每行占25px
        LODOP.ADD_PRINT_LINE(iCurLine - 5, 14, iCurLine - 5, 565, 0, 1);//横线
        //竖线
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 14, iCurLine - 30 + 25, 14, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 65, iCurLine - 30 + 25, 65, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 265, iCurLine - 30 + 25, 265, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 365, iCurLine - 30 + 25, 365, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 465, iCurLine - 30 + 25, 465, 0, 1);
        LODOP.ADD_PRINT_LINE(iCurLine - 30, 565, iCurLine - 30 + 25, 565, 0, 1);
        //LODOP.ADD_PRINT_LINE(iCurLine - 30, 670, iCurLine - 30 + 25, 670, 0, 1);
    }
    LODOP.ADD_PRINT_LINE(iCurLine, 14, iCurLine, 565, 0, 1);
    LODOP.ADD_PRINT_LINE(iCurLine, 14, iCurLine, 14, 0, 1);
    LODOP.SET_PRINT_PAGESIZE(3, 1500, 100, "");//这里3表示纵向打印且纸高“按内容的高度”；1385表示纸宽138.5mm；45表示页底空白4.5mm
    LODOP.SET_PRINT_MODE("PRINT_PAGE_PERCENT","Full-Width");
    LODOP.PREVIEW();
};
function AddTitle() {
    LODOP = getLodop();
    LODOP.PRINT_INIT("库存统计");
    LODOP.SET_PRINT_PAGESIZE(3, 1500, 100, "");
    LODOP.ADD_PRINT_TEXT(7, 252, 113, 30, "库存统计");
    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
    LODOP.ADD_PRINT_TEXT(50, 15, 50, 20, "序号");
    LODOP.ADD_PRINT_TEXT(50, 70, 200, 20, group);
    LODOP.ADD_PRINT_TEXT(50, 270, 100, 20, kinds);
    LODOP.ADD_PRINT_TEXT(50, 370, 100, 20, num);
    LODOP.ADD_PRINT_TEXT(50, 470, 100, 20, totalPrice);

    //横线
    LODOP.ADD_PRINT_LINE(45, 14, 45, 565, 0, 1);
    LODOP.ADD_PRINT_LINE(70, 14, 70, 565, 0, 1);
    //竖线
    LODOP.ADD_PRINT_LINE(45, 14, 70, 14, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 65, 70, 65, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 265, 70, 265, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 365, 70, 365, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 465, 70, 465, 0, 1);
    LODOP.ADD_PRINT_LINE(45, 565, 70, 565, 0, 1);
    //LODOP.ADD_PRINT_LINE(45, 670, 70, 670, 0, 1);
};
