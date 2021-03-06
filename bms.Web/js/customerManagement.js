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
            var search = $("#btn-search").val().trim();
            $.ajax({
                type: 'Post',
                url: 'customerManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    search: search,
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

    //点击查询按钮时
    $("#btn-search").click(function () {
        var search = $("#search_All").val().trim();
        $.ajax({
            type: 'Post',
            url: 'customerManagement.aspx',
            data: {
                search: search,
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
                            url: 'customerManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                search: search,
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

    //添加客户
    $("#btnAdd").click(function () {
        var id = $("#customerId").val().trim();
        var name = $("#customerName").val().trim();
        if (id == "") {
            swal({
                title: "温馨提示:)",
                text: "账号不能为空，请您确认后再次添加!",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (name == "") {
            swal({
                title: "温馨提示:)",
                text: "客户名称不能为空，请您确认后再次添加!",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'customerManagement.aspx',
                data: {
                    customerId: id,
                    cutomerName: name,
                    op: "add"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "添加成功") {
                        swal({
                            title: "温馨提示",
                            text: "添加成功",
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            sessionStorage.setItem("add", "添加成功");
                            //window, location.reload();
                        })
                    } else {
                        swal({
                            title: "温馨提示",
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
    $("#close").click(function () {
        if (sessionStorage.getItem("add") == "添加成功") {
            window.location.reload();
            sessionStorage.removeItem("add");
        }
    })
    //提交编辑
    $("#table").delegate(".btn_Editor", "click", function () {
        var custId = $(this).parent().prev().prev().text().trim();
        var custName = $(this).parent().prev().text().trim();
        $(".editor_name").val(custName);
        $(".editor_id").text(custId);
    })
    $(".sava_Editor").click(function () {
        var custId = $(".editor_id").text();
        var custName = $(".editor_name").val();
        if (custName == "") {
            swal({
                title: "温馨提示:)",
                text: "客户名称不能为空，请您确认后再次添加!",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else {
            $.ajax({
                type: 'Post',
                url: 'customerManagement.aspx',
                data: {
                    customerid: custId,
                    customername: custName,
                    op: "editor"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "更新成功") {
                        swal({
                            title: "温馨提示",
                            text: "更新成功",
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
                            title: "温馨提示",
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
            });
        }
    })
    //删除
    $("#table").delegate(".btn_delete", "click", function () {
        var custId = $(this).parent().prev().prev().text().trim();
        //弹窗
        swal({
            title: "温馨提示:)",
            text: "删除了将无法恢复,确定删除该项吗？",
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
                url: 'customerManagement.aspx',
                data: {
                    cutomerId: custId,
                    op: "del"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "删除成功") {
                        swal({
                            title: "提示",
                            text: "删除成功",
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
                            title: "提示",
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
            url: 'customerManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}


