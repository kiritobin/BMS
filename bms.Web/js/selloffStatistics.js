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
    $("#groupsupplier").hide();
    $("#groupregion").hide();
    $("#groupcustom").hide();
}

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
            var groupby = $("#groupby").find("option:selected").text();
            var supplier = $("#supplier").find("option:selected").text();
            var regionName = $("#region").find("option:selected").text();
            var customerName = $("#customer").find("option:selected").text();
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
                if (supplier == "全部") {
                    supplier = "";
                }
                regionName = "";
                customerName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部") {
                    regionName = "";
                }
                supplier = "";
                customerName = "";
            } else if (groupby == "客户") {
                if (customerName == "全部") {
                    customerName = "";
                }
                groupbyType = "customerName";
                supplier = "";
                regionName = "";
            } else {
                groupbyType = "state";
                supplier = "";
                regionName = "";
                customerName = "";
            }
            var time = $("#time").val();
            var page = api.getCurrent();
            $.ajax({
                type: 'Post',
                url: 'selloffStatistics.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    groupbyType: groupbyType,
                    supplier: supplier,
                    regionName: regionName,
                    time: time,
                    customerName: customerName,
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
                    } else {
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
            var customerName = $("#customer").find("option:selected").text();
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
                if (supplier == "全部") {
                    supplier = "";
                }
                regionName = "";
                customerName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部") {
                    regionName = "";
                }
                supplier = "";
                customerName = "";
            } else if (groupby == "客户") {
                if (customerName == "全部") {
                    customerName = "";
                }
                groupbyType = "customerName";
                supplier = "";
                regionName = "";
            } else {
                groupbyType = "state";
                supplier = "";
                regionName = "";
                customerName = "";
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
                window.location.href = "selloffStatistics.aspx?op=exportAll&&groupbyType=" + groupbyType + "&&supplier=" + supplier + "&&regionName=" + regionName + "&&time=" + time + "&&customerName=" + customerName;
            }
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
            var customerName = $("#customer").find("option:selected").text();
            var groupbyType;
            if (groupby == "供应商") {
                groupbyType = "supplier";
                if (supplier == "全部") {
                    supplier = "";
                }
                regionName = "";
                customerName = "";
            }
            else if (groupby == "组织") {
                groupbyType = "regionName";
                if (regionName == "全部") {
                    regionName = "";
                }
                supplier = "";
                customerName = "";
            } else if (groupby == "客户") {
                if (customerName == "全部") {
                    customerName = "";
                }
                groupbyType = "customerName";
                supplier = "";
                regionName = "";
            } else {
                groupbyType = "state";
                supplier = "";
                regionName = "";
                customerName = "";
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
                window.location.href = "selloffStatistics.aspx?op=exportDe&&groupbyType=" + groupbyType + "&&supplier=" + supplier + "&&regionName=" + regionName + "&&time=" + time + "&&customerName=" + customerName;
            }
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
        } else if (groupby == "客户") {
            groupbyType = "customerName";
        } else {
            groupbyType = "supplier";
        }
        var name = $(this).parent().prev().prev().prev().prev().prev().text();
        window.location.href = "sellOffDetail.aspx?type=" + groupbyType + "&&name=" + name;
    })

    //点击查询按钮时
    $("#btn_search").click(function () {
        var groupby = $("#groupby").find("option:selected").text();
        var supplier = $("#supplier").find("option:selected").text();
        var regionName = $("#region").find("option:selected").text();
        var customerName = $("#customer").find("option:selected").text();
        var groupbyType;
        if (groupby == "供应商") {
            groupbyType = "supplier";
            if (supplier == "全部") {
                supplier = "";
            }
            regionName = "";
            customerName = "";
        }
        else if (groupby == "组织") {
            groupbyType = "regionName";
            if (regionName == "全部") {
                regionName = "";
            }
            supplier = "";
            customerName = "";
        } else if (groupby == "客户") {
            if (customerName == "全部") {
                customerName = "";
            }
            groupbyType = "customerName";
            supplier = "";
            regionName = "";
        } else {
            groupbyType = "state";
            supplier = "";
            regionName = "";
            customerName = "";
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
                url: 'selloffStatistics.aspx',
                data: {
                    groupbyType: groupbyType,
                    supplier: supplier,
                    regionName: regionName,
                    time: time,
                    customerName: customerName,
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
                    } else {
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
                                url: 'selloffStatistics.aspx',
                                data: {
                                    page: api.getCurrent(), //页码
                                    groupbyType: groupbyType,
                                    supplier: supplier,
                                    regionName: regionName,
                                    time: time,
                                    customerName: customerName,
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
                                    } else {
                                        $("#showType").text("客户");
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

            $("#groupcustom").hide();
        }
        else if (groupby == "组织") {
            $("#groupsupplier").hide();
            $("#groupregion").show();
            $("#groupcustom").hide();
        } else if (groupby == "客户") {
            $("#groupsupplier").hide();
            $("#groupregion").hide();
            $("#groupcustom").show();
        } else {
            $("#groupsupplier").hide();
            $("#groupregion").hide();
            $("#groupcustom").hide();
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
            url: 'selloffStatistics.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}