﻿$(document).ready(function () {
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
                url: 'regionRs.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    op: "paging"

                },
                dataType: 'text',
                success: function (data) {
                    $("#table tbody").empty(); //清空table处首行
                    $("#table tbody").append(data); //加载table
                }
            });
        }
    });
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
            url: 'regionRs.aspx',
            datatype: 'text',
            data: {
                op:"logout"
            },
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

//地区下拉框查询
$("#regionSearch").change(function (){
    var regionId = $("#regionSearch").val();
    if (regionId != null || regionId != "") {
        $.ajax({
            type: 'post',
            url: 'regionRs.aspx',
            datatype: 'text',
            data: {
                regionId: regionId,
                op:"paging"
            },
            success: function (succ) {
                var data = succ.split(":|");
                $("#intPageCount").remove();
                $("#table tr:not(:first)").remove(); //清空table处首行
                $("#table").append(data[0]); //加载table
                $("#kinds").val(data[1]);
                $("#count").val(data[2]);
                $("#region").val(data[3]);
                $(".paging").empty();
                $('.paging').pagination({
                    //totalData: $("#countPage").val(), //数据总数
                    //showData: $("#totalCount").val(), //每页显示的条数
                    pageCount: $("#intPageCount").val(), //总页数
                    jump: true,
                    mode: 'fixed',//固定页码数量
                    coping: true,
                    homePage: '首页',
                    endPage: '尾页',
                    prevContent: '上页',
                    nextContent: '下页',
                    callback: function (api) {
                        var book = $("#bookSearch").val().trim();
                        var isbn = $("#isbnSearch").val().trim();
                        $.ajax({
                            type: 'Post',
                            url: 'regionRs.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                regionId: regionId,
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
    }
})

//打印
$("#print").click(function () {
    //$("#content").jqprint();
    var regionId = $("#regionSearch").val();
    if (regionId == 0 || regionId == "0") {
        swal({
            title: "提示",
            text: "请选择地区",
            type: "warning",
            confirmButtonColor: '#3085d6',
            confirmButtonText: '确定',
            confirmButtonClass: 'btn btn-success',
            buttonsStyling: false,
            allowOutsideClick: false
        })
    }
    else {
        $.ajax({
            type: 'Post',
            url: 'regionRs.aspx',
            data: {
                regionId: regionId,
                op: 'print'
            },
            dataType: 'text',
            success: function (succ) {
                var data = succ.split(":|");
                $("#table tr:not(:first)").remove();
               // $("#table").append(data);
                $("#table").append(data[0]); //加载table
                $("#kinds").val(data[1]);
                $("#count").val(data[2]);
                $("#region").val(data[3]);
                var status = "";
                var LODOP = getLodop();
                var link = "";
                var style = "";
                LODOP.SET_PRINT_MODE("CATCH_PRINT_STATUS", true);
                LODOP.On_Return = function (TaskID, Value) {
                    status = Value;
                };
                if (status != "" || status != null) {
                    link = "<link rel='stylesheet' type='text/css' href='../css/zgz.css'><link rel='stylesheet' href='../css/material-dashboard.min.css'><link rel='stylesheet' type='text/css' href='../css/lgd.css'>";
                    style = "<style>body{background-color:white !important;}#table tr td{border: 1px solid black !important;padding:5px 5px;font-size:13px;}</style>";
                    LODOP.ADD_PRINT_HTM(0, 0, "100%", "100%", link + style + "<body>" + document.getElementById("content").innerHTML + "</body>");
                    //LODOP.SET_PRINTER_INDEX("Send To OneNote 2016");
                    LODOP.SET_PRINT_PAGESIZE(3, "100%", "", "");
                    LODOP.PREVIEW();
                    window.location.reload();
                }
            }
        })
    }
})