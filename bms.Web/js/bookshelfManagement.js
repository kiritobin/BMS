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
            var region = $("#search_region").val().trim();
            var goods = $("#search_goods").val().trim();
            $.ajax({
                type: 'Post',
                url: 'bookshelfManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    region: region,
                    goods: goods,
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

    //查询按钮事件
    $("#btn-search").click(function () {
        var region = $("#search_region").val().trim();
        var goods = $("#search_goods").val().trim();
        $.ajax({
            type: 'Post',
            url: 'bookshelfManagement.aspx',
            data: {
                region: region,
                goods: goods,
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
                            url: 'bookshelfManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                region: region,
                                goods: goods,
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
        })
    })

    //删除按钮事件
    $("#table").delegate(".btn_delete", "click", function () {
        var shelId = $(this).parent().prev().prev().text();
        //弹窗
        swal({
            title: "温馨提示:)",
            text: "您确定要删除该条货架信息吗？删除将无法恢复",
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
                url: 'bookshelfManagement.aspx',
                data: {
                    shelfId: shelId,
                    op: "del"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "删除成功") {
                        swal({
                            title: "温馨提示",
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
                    }
                    else {
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
        })
    })
    $("#close").click(function () {
        if (sessionStorage.getItem("add") == "添加成功") {
            window.location.reload();
            sessionStorage.removeItem("add");
        }
    })
    //添加按钮事件
    $("#btnAdd").click(function () {
        var index = "";
        var roleName = $("#roleName").val().trim();
        var goodsId = $("#model-select-region").val();
        var region = "";
        if (roleName == "超级管理员") {
            region = $("#model-select-region").find("option:selected").val().trim();
            if (goodsId == "") {
                index = "false";
            }
            else {
                index = "true";
            }
        } else {
            region = "";
        }
        var shelfNo = $("#shelfNo").val().trim();
        var shelfName = $("#shelfName").val().trim();
        if (shelfNo == "") {
            swal({
                title: "温馨提示:)",
                text: "货架编号不能为空，请您输入货架编号!",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else if (shelfName == "") {
            swal({
                title: "温馨提示:)",
                text: "货架名称不能为空，请您输入货架名称!",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (index == "false") {
            swal({
                title: "温馨提示:)",
                text: "货架所在地区不能为空!",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'bookshelfManagement.aspx',
                data: {
                    regionId: region,
                    shelfNo: shelfNo,
                    shelfName: shelfName,
                    op: "add"
                },
                dataType: 'text',
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
                            //window.location.reload();
                        })
                    } else {
                        swal({
                            title: "错误提示",
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
    //上传
    $("#upload").click(function () {
        var location = $("input[name='file']").val();
        var point = location.lastIndexOf(".");
        var type = location.substr(point).toLowerCase();
        var uploadFiles = document.getElementById("file").files;
        if (uploadFiles.length == 0) {
            swal({
                title: "提示",
                text: "请选择要上传的文件",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            })
        }
        else if (type == ".xls" || type == ".xlsx") {
            ajaxFileUpload();
        }
        else {
            swal({
                title: "提示",
                text: "只允许上传.xls或者.xlsx格式的文件",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            })
        }
    });
    function ajaxFileUpload() {
        $.ajaxFileUpload(
            {
                url: '/CustomerMGT/upload.aspx', //用于文件上传的服务器端请求地址
                secureuri: false, //是否需要安全协议，一般设置为false
                fileElementId: 'file', //文件上传域的ID
                dataType: 'json', //返回值类型 一般设置为json
                success: function (data, status)  //服务器成功响应处理函数
                {
                    sessionStorage.setItem("succ", data.msg);
                    if (typeof (data.error) != 'undefined') {
                        if (data.error != '') {
                            swal({
                                title: "提示",
                                text: data.error,
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            })
                        } else {
                            $.ajax({
                                type: 'Post',
                                url: 'bookshelfManagement.aspx',
                                data: {
                                    op: "check"
                                },
                                dataType: 'text',
                                //beforeSend: function (XMLHttpRequest) { //开始请求
                                //    $("#myModalLabe1").html("正在读取数据");
                                //    $("#img").attr("src", "../imgs/loading.gif");
                                //    $("#myModal1").modal("show");
                                //    $("#close").hide();
                                //    show = "fail";
                                //},
                                success: function (succ) {
                                    $("#myModal1").modal("hide");
                                    $("#close2").show();
                                    $("#myModalLabe1").html("正在导入，请保持网络畅通，导入过程中请勿关闭页面");
                                    $("#img").attr("src", "../imgs/loading.gif");
                                    if (succ == "重复数据") {
                                        show = "fail";
                                        swal({
                                            title: "温馨提示:)",
                                            text: "存在重复记录，请修改后上传",
                                            buttonsStyling: false,
                                            confirmButtonClass: "btn btn-success",
                                            type: "warning",
                                            allowOutsideClick: false
                                        })
                                    }
                                    else if (succ == "上传成功") {
                                        show = "succ";

                                        swal({
                                            title: "温馨提示:)",
                                            text: succ,
                                            buttonsStyling: false,
                                            confirmButtonClass: "btn btn-success",
                                            type: "success",
                                            allowOutsideClick: false
                                        })
                                        $("#myModal1").modal("hide");
                                    }
                                    else {
                                        show = "fail";
                                        swal({
                                            title: "温馨提示:)",
                                            text: succ,
                                            buttonsStyling: false,
                                            confirmButtonClass: "btn btn-success",
                                            type: "warning",
                                            allowOutsideClick: false
                                        })
                                    }
                                }
                            });
                        }
                    }
                },
                error: function (data, status, e)//服务器响应失败处理函数
                {
                    swal({
                        title: "提示",
                        text: e,
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-success',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    })
                }
            }
        );
        return false;
    }

    //导入
    $("#btnImport").click(function () {
        var file = $("#file").val();
        var roleName = $("#roleName").val().trim();
        var regId;
        if (roleName == "超级管理员") {
            regId = $("#regName").val();
        } else {
            regId = "";
        }
        if (file == "" || file == null) {
            swal({
                title: "提示",
                text: "请上传文件",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            })
        }
        else if (regId == "" || regId == null) {
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
        else if (sessionStorage.getItem("succ") != "上传成功" || show == "fail") {
            swal({
                title: "提示",
                text: "文件未上传成功",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            })
        }
        else {
            $("#myModal1").modal("show");
            $("#close2").hide();
            $.ajax({
                type: 'Post',
                url: 'bookshelfManagement.aspx',
                data: {
                    regId: regId,
                    op: "import"
                },
                dataType: 'text',
                success: function (data) {
                    if (data.indexOf("导入成功") >= 0) {
                        $("#myModalLabe1").html(data);
                        $("#close2").show();
                        $("#img").attr("src", "../imgs/success.png");
                        sessionStorage.setItem("import", "导入成功");
                    } else if (data.indexOf("导入失败") >= 0) {
                        $("#myModalLabe1").html(data);
                        $("#close2").show();
                        $("#img").attr("src", "../imgs/lose.png");
                        sessionStorage.setItem("import", "导入失败");
                    }
                    else {
                        sessionStorage.setItem("import", "导入失败");
                        $("#close2").show();
                        swal({
                            title: "提示",
                            text: data,
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        })
                    }
                }
            });
        }
    });
    $("#close2").click(function () {
        $("#close2").show();
        $("#myModalLabe1").html("正在导入，请保持网络畅通，导入过程中请勿关闭页面");
        $("#img").attr("src", "../imgs/loading.gif");
        $(" #file").val("");
    });
    $("#close3").click(function () {
        if (sessionStorage.getItem("import") == "导入成功") {
            window.location.reload();
            sessionStorage.removeItem("import");
            sessionStorage.removeItem("succ");
            window.location.reload();
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
            url: 'bookshelfManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}
