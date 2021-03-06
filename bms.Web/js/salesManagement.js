﻿//退出系统
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
            url: 'salesManagement.aspx?op=logout',
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
            var saleTaskId = $("#ID").val().trim();
            var regionName = $("#regionName").find("option:selected").text();
            var userName = $("#userName").find("option:selected").text();
            if (regionName == "全部组织") {
                regionName = "";
            }
            if (userName == "全部操作员") {
                userName = "";
            }
            $.ajax({
                type: 'Post',
                url: 'salesManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    saleTaskId: saleTaskId,
                    regionName: regionName,
                    userName: userName,
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
    //查询
    $("#btn-search").click(function () {
        var saleTaskId = $("#ID").val().trim();
        var regionName = $("#regionName").find("option:selected").text();
        var userName = $("#userName").find("option:selected").text();
        if (regionName == "全部组织") {
            regionName = "";
        }
        if (userName == "全部操作员") {
            userName = "";
        }
        $.ajax({
            type: 'Post',
            url: 'salesManagement.aspx',
            data: {
                saleTaskId: saleTaskId,
                regionName: regionName,
                userName: userName,
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
                $("#sss").val(data);
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
                            url: 'salesManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                saleTaskId: saleTaskId,
                                regionName: regionName,
                                userName: userName,
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
    //结算所有
    $("#btn_succAll").click(function () {
        swal({
            title: "温馨提示:)",
            text: "是否结算该销售计划下的所有销售单？",
            type: "question",
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
                url: 'salesManagement.aspx',
                data: {
                    op: 'SettlementAll'
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "添加成功") {
                        swal({
                            title: "温馨提示:)",
                            text: "结算成功。",
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
                            title: "温馨提示:)",
                            text: "结算失败。",
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
    //点击销售单结算
    $("#table").delegate(".btn_succ", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        var taskId = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        swal({
            title: "温馨提示:)",
            text: "是否结算该销售单？",
            type: "question",
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
                url: 'salesManagement.aspx',
                data: {
                    ID: ID,
                    taskId: taskId,
                    op: 'Settlement'
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "添加成功") {
                        swal({
                            title: "温馨提示:)",
                            text: "结算成功。",
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
                            title: "提示:结算失败",
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

    /////点击销退btn_back
    //$("#table").delegate(".btn_back", "click", function () {
    //    var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
    //    var taskId = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
    //    swal({
    //        title: "温馨提示:)",
    //        text: "是否添加销退单？",
    //        type: "question",
    //        showCancelButton: true,
    //        confirmButtonColor: '#3085d6',
    //        cancelButtonColor: '#d33',
    //        confirmButtonText: '确定',
    //        cancelButtonText: '取消',
    //        confirmButtonClass: 'btn btn-success',
    //        cancelButtonClass: 'btn btn-danger',
    //        buttonsStyling: false,
    //        allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
    //    }).then(function () {
    //        $.ajax({
    //            type: 'Post',
    //            url: 'salesManagement.aspx',
    //            data: {
    //                ID: ID,
    //                taskId: taskId,
    //                op: 'saleback'
    //            },
    //            dataType: 'text',
    //            success: function (succ) {
    //                if (succ == "添加成功") {
    //                    swal({
    //                        title: "温馨提示:)",
    //                        text: "添加销退成功。",
    //                        type: "success",
    //                        confirmButtonColor: '#3085d6',
    //                        confirmButtonText: '确定',
    //                        confirmButtonClass: 'btn btn-success',
    //                        buttonsStyling: false,
    //                        allowOutsideClick: false
    //                    }).then(function () {
    //                        window.location.reload();
    //                    })
    //                } else {
    //                    swal({
    //                        title: "提示:添加销退失败。",
    //                        text: succ,
    //                        type: "warning",
    //                        confirmButtonColor: '#3085d6',
    //                        confirmButtonText: '确定',
    //                        confirmButtonClass: 'btn btn-success',
    //                        buttonsStyling: false,
    //                        allowOutsideClick: false
    //                    }).then(function () {
    //                    })
    //                }
    //            }
    //        })
    //    })
    //})

    //完成
    $("#finish").click(function () {
        swal({
            title: "温馨提示:)",
            text: "您确认要完成该销售任务吗？",
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
                url: 'salesManagement.aspx',
                data: {
                    op: "finish"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "成功") {
                        swal({
                            title: "温馨提示:)",
                            text: "保存完成。",
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window, location.reload();
                        })
                    }
                    else if (succ == "失败,有新建的单据" || succ == "失败,有采集中的单据") {
                        swal({
                            title: "温馨提示:)",
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
                    else {
                        swal({
                            title: "温馨提示:)",
                            text: "保存失败。",
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
    //添加
    $("#btnAdd").click(function () {
        var remarks = $("#remarks").val().trim();

        $.ajax({
            type: 'Post',
            url: 'salesManagement.aspx',
            data: {
                remarks: remarks,
                op: "add"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "添加成功") {
                    swal({
                        title: "温馨提示:)",
                        text: "添加成功",
                        type: "success",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        window, location.reload();
                    })
                }
                else if (succ == "该销售计划已完成") {
                    swal({
                        title: "温馨提示:)",
                        text: "该销售计划已完成,不能添加!",
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {

                    })
                }
                else {
                    swal({
                        title: "温馨提示:)",
                        text: "添加失败请联系技术人员!",
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
        //swal({
        //    title: "温馨提示:)",
        //    text: "您是否新建销售？",
        //    type: "warning",
        //    showCancelButton: true,
        //    confirmButtonColor: '#3085d6',
        //    cancelButtonColor: '#d33',
        //    confirmButtonText: '确定',
        //    cancelButtonText: '取消',
        //    confirmButtonClass: 'btn btn-success',
        //    cancelButtonClass: 'btn btn-danger',
        //    buttonsStyling: false,
        //    allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
        //}).then(function () {
        //    $.ajax({
        //        type: 'Post',
        //        url: 'salesManagement.aspx',
        //        data: {
        //            remarks: remarks,
        //            op: "add"
        //        },
        //        dataType: 'text',
        //        success: function (succ) {
        //            if (succ == "添加成功") {
        //                swal({
        //                    title: "温馨提示:)",
        //                    text: "添加成功",
        //                    type: "success",
        //                    confirmButtonColor: '#3085d6',
        //                    confirmButtonText: '确定',
        //                    confirmButtonClass: 'btn btn-success',
        //                    buttonsStyling: false,
        //                    allowOutsideClick: false
        //                }).then(function () {
        //                    window, location.reload();
        //                })
        //            }
        //            else if (succ == "该销售计划已完成") {
        //                swal({
        //                    title: "温馨提示:)",
        //                    text: "该销售计划已完成,不能添加!",
        //                    type: "warning",
        //                    confirmButtonColor: '#3085d6',
        //                    confirmButtonText: '确定',
        //                    confirmButtonClass: 'btn btn-success',
        //                    buttonsStyling: false,
        //                    allowOutsideClick: false
        //                }).then(function () {

        //                })
        //            }
        //            else {
        //                swal({
        //                    title: "温馨提示:)",
        //                    text: "添加失败请联系技术人员!",
        //                    type: "warning",
        //                    confirmButtonColor: '#3085d6',
        //                    confirmButtonText: '确定',
        //                    confirmButtonClass: 'btn btn-success',
        //                    buttonsStyling: false,
        //                    allowOutsideClick: false
        //                }).then(function () {

        //                })
        //            }
        //        }
        //    })
        //})
    })
    //删除
    $("#table").delegate(".btn_del", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        var taskId = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        var state = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        if (state == "新建单据") {
            swal({
                title: "温馨提示:)",
                text: "删除后将无法恢复,您确定要删除该项吗？？？",
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
                    url: 'salesManagement.aspx',
                    data: {
                        ID: ID,
                        taskId: taskId,
                        state:state,
                        op: 'del'
                    },
                    dataType: 'text',
                    success: function (succ) {
                        if (succ == "删除成功") {
                            swal({
                                title: "温馨提示:)",
                                text: "删除成功。",
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
                                title: "温馨提示:)",
                                text: "删除失败。",
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
        }
        else if (state == "采集中") {
            swal({
                title: "温馨提示:)",
                text: "单据采集中，现在不能删除",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            }).then(function () {
            })
        }
        else if (state == "单据已完成") {
            swal({
                title: "温馨提示:)",
                text: "单据已完成不能删除",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            }).then(function () {
            })
        }
        else if (state == "预采") {
            swal({
                title: "温馨提示:)",
                text: "此单头为预采单！是否继续删除？",
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
                    url: 'salesManagement.aspx',
                    data: {
                        ID: ID,
                        taskId: taskId,
                        state: state,
                        op: 'del'
                    },
                    dataType: 'text',
                    success: function (succ) {
                        if (succ == "删除成功") {
                            swal({
                                title: "温馨提示:)",
                                text: "删除成功。",
                                type: "success",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                                window.location.reload();
                            })
                        } else if (succ == "该预采单已有数据,不能删除") {
                            swal({
                                title: "温馨提示:)",
                                text: "该预采单已有数据,不能删除",
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                            })
                        } else {
                            swal({
                                title: "温馨提示:)",
                                text: "删除失败",
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
        }
    });

    //加
    $("#table").delegate(".add", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        var taskId = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        var state = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
        if (state == "单据已完成") {
            swal({
                title: "温馨提示:)",
                text: "单据已完成，不能添加。",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            }).then(function () { })
        } else {
            $.ajax({
                type: 'Post',
                url: 'salesManagement.aspx',
                data: {
                    ID: ID,
                    taskId: taskId,
                    op: 'addDetail'
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "成功") {
                        window.location.href = "../SalesMGT/salesDetail.aspx";
                    } else {
                        swal({
                            title: "温馨提示:)",
                            text: "单据已完成，不能添加。",
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () { })
                    }
                }
            })
        }
    })
    //看
    $("#table").delegate(".look", "click", function () {
        var ID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        var salestate = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        var type;
        if (salestate == "预采") {
            type = "copy";
        } else {
            type = "other";
        }
            $.ajax({
                type: 'Post',
                url: 'salesManagement.aspx',
                data: {
                    ID: ID,
                    type: type,
                    op: 'look'
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "成功") {
                        window.location.href = "../SalesMGT/searchSalesDetail.aspx";
                    }
                }
            })
    })
})
//弹出模态框获取焦点事件
$('#myModal').on('shown.bs.modal', function (e) {
    $('#remarks').focus();
});