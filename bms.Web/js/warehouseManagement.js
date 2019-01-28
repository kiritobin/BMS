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
            url: 'warehouseManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}
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
            var ID = $("#ID").val();
            var region = $("#region").find("option:selected").text();
            var user = $("#user").find("option:selected").text();
            var time = $("#time").val();
            if (region == "全部收货组织") {
                regionName = "";
            }
            if (user == "全部操作员") {
                userName = "";
            }
            $.ajax({
                type: 'Post',
                url: 'warehouseManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    ID: ID,
                    region: region,
                    user: user,
                    time:time,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table tr:not(:first)").remove(); //清空table处首行
                    $("#table").append(data); //加载table
                }
            });
        }
    })

    //清空时间
    $("#modalClose").click(function () {
        $("#time").val("");
        $("#timeModal").modal('hide');
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
            $("#timeModal").modal('hide');
        }
    })
})
$("#close").click(function () {
    if (sessionStorage.getItem("add") == "添加成功") {
        window.location.reload();
        sessionStorage.removeItem("add");
    }
})
//添加退货单头
$("#btnAdd").click(function () {
    var regionId = $("#regionId").val();
    if (regionId == "") {
        swal({
            title: "温馨提示:)",
            text: "请选择收货组织",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    } else {
        $.ajax({
            type: 'Post',
            url: 'warehouseManagement.aspx',
            data: {
                regionId: regionId,
                op: "add"
            },
            datatype: 'text',
            success: function (succ) {
                if (succ == "添加成功") {
                    swal({
                        title: succ,
                        text: succ,
                        type: "success",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        sessionStorage.setItem("add", "添加成功");
                        window.location.reload();
                    })
                } else {
                    swal({
                        title: succ,
                        text: succ,
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                    })
                }
            }
        })
    }
})

//查询
$("#btn-search").click(function () {
    var ID = $("#ID").val();
    var region = $("#region").find("option:selected").text();
    var user = $("#user").find("option:selected").text();
    var time = $("#time").val();
    if (region == "全部收货组织") {
        regionName = "";
    }
    if (user == "全部操作员") {
        userName = "";
    }
    $.ajax({
        type: 'Post',
        url: 'warehouseManagement.aspx',
        data: {
            ID: ID,
            region: region,
            user: user,
            time:time,
            op: "paging"
        },
        datatype: 'text',
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
                        url: 'warehouseManagement.aspx',
                        data: {
                            page: api.getCurrent(), //页码
                            ID: ID,
                            region: region,
                            user: user,
                            time:time,
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
    })
});

//查看报表
$("#check").click(function () {
    var source = $("#bbsource").find("option:selected").text();
    var singleHeadId = $("#singleHeadId").val().trim();
    if (singleHeadId == "") {
        singleHeadId = "null";
    }
    if (source == "" && singleHeadId == "") {
        swal({
            title: "温馨提示:)",
            text: "请先选择组织或输入单据编号",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-success",
            type: "warning",
            allowOutsideClick: false
        })
    }
    else {
        window.location.href = "/InventoryMGT/inventoryStatistics.aspx?type=CK&&region=" + source + "&&singleHeadId=" + singleHeadId;
    }
});

//删除
$("#table").delegate(".btn-delete", "click", function () {
    var ID = $(this).prev().val();
    swal({
        title: "温馨提示:)",
        text: "删除后将无法恢复,您确定要删除吗？？？",
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
            url: 'warehouseManagement.aspx',
            data: {
                ID: ID,
                op: 'del'
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "删除成功") {
                    swal({
                        title: succ,
                        text: succ,
                        type: "success",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        window.location.reload();
                    })
                } else {
                    swal({
                        title: succ,
                        text: succ,
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                    })
                }
            }
        })
    })
});
