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
window.onload = function () {
    $("#groupsupplier").hide();
    $("#groupregion").hide();
}

///数据加载中
//$(function () {
//    $.ajax({
//        type: 'Post',
//        url: 'stockStatistics.aspx',
//        data: {
//            op: "paging"
//        },
//        dataType: 'text',
//        beforeSend: function (XMLHttpRequest) { //开始请求
//            swal({
//                text: "数据加载中",
//                imageUrl: "../imgs/load.gif",
//                imageHeight: 100,
//                imageWidth: 100,
//                width: 180,
//                showConfirmButton: false,
//                allowOutsideClick: false
//            });
//        },
//        success: function (data) {
//            $("#table").append(data); //加载table
//            $(".swal2-container").remove();
//        },
//        error: function (XMLHttpRequest, textStatus) { //请求失败
//            if (textStatus == 'timeout') {
//                var xmlhttp = window.XMLHttpRequest ? new window.XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHttp");
//                xmlhttp.abort();
//                $(".swal2-container").remove();
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
//                $(".swal2-container").remove();
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
//});

$(document).ready(function () {
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
            var time = $("#time").val();
            var groupby = $("#groupby").find("option:selected").text();
            var supplier = $("#supplier").find("option:selected").text();
            var regionName = $("#region").find("option:selected").text();
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
                if (supplier == "全部") {
                    supplier = "";
                }
                regionName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部") {
                    regionName = "";
                }
                supplier = "";
            }  else {
                groupbyType = "state";
                supplier = "";
                regionName = "";
            }
            var page = api.getCurrent();
            $.ajax({
                type: 'Post',
                url: 'stockStatistics.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    groupbyType: groupbyType,
                    supplier: supplier,
                    regionName: regionName,
                    time:time,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    if (groupby == "供应商") {
                        $("#showType").text("供应商");
                    }
                    else if (groupby == "组织") {
                        $("#showType").text("组织");
                    } else if (groupby == "客户") {
                        $("#showType").text("客户");
                    } 
                    $("#table tr:not(:first)").remove(); //清空table处首行
                    $("#table").append(data); //加载table
                    $("#intPageCount").remove();
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
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
                if (supplier == "全部") {
                    supplier = "";
                }
                regionName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部") {
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
            var time = $("#time").val();
            window.location.href = "stockStatistics.aspx?op=exportAll&&&&groupbyType=" + groupbyType + "&&supplier=" + supplier + "&&regionName=" + regionName + "&&time=" + time;
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
                if (supplier == "全部") {
                    supplier = "";
                }
                regionName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部") {
                    regionName = "";
                }
                supplier = "";
            }  else {
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
            var time = $("#time").val();
            window.location.href = "stockStatistics.aspx?op=exportDe&&groupbyType=" + groupbyType + "&&supplier=" + supplier + "&&regionName=" + regionName + "&&time=" + time;
        }
    })
    //清空时间
    $("#modalClose").click(function () {
        $("#time").val("");
        $("#myModal").modal('hide');
    })
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
    })
    //查看详情
    $("#table").delegate(".look", "click", function (e) {
        var groupby = $("#groupby").find("option:selected").text();
        var groupbyType;
        if (groupby == "供应商") {
            groupbyType = "supplier";
        }
        else if (groupby == "组织") {
            groupbyType = "regionName";
        } else {
            groupbyType = "supplier";
        }
        var name = $(this).parent().prev().prev().prev().prev().prev().text();
        window.location.href = "stockDetails.aspx?type=" + groupbyType + "&&name=" + name;
    })

    //点击查询按钮时
    $("#btn_search").click(function () {
        var groupby = $("#groupby").find("option:selected").text();
        var supplier = $("#supplier").find("option:selected").text();
        var regionName = $("#region").find("option:selected").text();
        var time = $("#time").val();
        var groupbyType;
        if (groupby == "供应商") {
            groupbyType = "supplier";
            if (supplier == "全部") {
                supplier = "";
            }
            regionName = "";
        }
        else if (groupby == "组织") {
            groupbyType = "regionName";
            if (regionName == "全部") {
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
                url: 'stockStatistics.aspx',
                data: {
                    groupbyType: groupbyType,
                    supplier: supplier,
                    regionName: regionName,
                    time:time,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    if (groupby == "供应商") {
                        $("#showType").text("供应商");
                    }
                    else if (groupby == "组织") {
                        $("#showType").text("组织");
                    } else if (groupby == "客户") {
                        $("#showType").text("客户");
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
                                url: 'stockStatistics.aspx',
                                data: {
                                    page: api.getCurrent(), //页码
                                    groupbyType: groupbyType,
                                    supplier: supplier,
                                    regionName: regionName,
                                    time: time,
                                    op: "paging"
                                },
                                dataType: 'text',
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
                            });
                        }
                    });
                }
            });
        }

    });
    //分组方式改变
    $("#groupby").change(function () {
        var groupby = $("#groupby").find("option:selected").text();
        if (groupby == "供应商") {
            $("#groupsupplier").show();
            $("#groupregion").hide();
            $('#groupregion').selectpicker('refresh');
        }
        else if (groupby == "组织") {
            $("#groupsupplier").hide();
            $("#groupregion").show();
        } else if (groupby == "客户") {
            $("#groupsupplier").hide();
            $("#groupregion").hide();
        } else {
            $("#groupsupplier").hide();
            $("#groupregion").hide();
        }
    })
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
            url: 'stockStatistics.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}