$(document).ready(function () {
    $(".paging").pagination({
        pageCount: $("#intPageCount").val(),
        jump: true,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            var sellId = $("#region").val();
            var customer = $("#customer").find("option:selected").text();
            if (customer == "全部客户") {
                customer = "";
            }
            $.ajax({
                type: 'Post',
                url: 'backManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    sellId: sellId,
                    customer: customer,
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
    })
    //查询
    $("#btn-search").click(function () {
        //var stockId = $("#bill").val();
        var sellId = $("#region").val();
        var customer = $("#customer").find("option:selected").text();
        if (customer == "全部客户") {
            customer = "";
        }
        $.ajax({
            type: 'Post',
            url: 'backManagement.aspx',
            data: {
                //stockId: stockId,
                sellId: sellId,
                customer: customer,
                op: "paging"
            },
            datatype: 'text',
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
                        //var search = $("#btn-search").val().trim();
                        $.ajax({
                            type: 'Post',
                            url: 'backManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                //stockId: stockId,
                                sellId: sellId,
                                customer: customer,
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
    })
    //页面链接
    //查询按钮
    $("#table").delegate(".search_back", "click", function () {
        // window.location.href = 'backQuery.aspx'
        var id = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        $.ajax({
            type: 'Post',
            url: 'backManagement.aspx',
            data: {
                sohId: id,
                op: "searchMonomer"
            },
            dataType: 'text',
            success: function (succ) {
                window.location.href = 'backQuery.aspx?type=searchMonomer';
            }
        })
    })
    //添加单体
    $("#table").delegate(".btn_add", "click", function () {
        var id = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        var state = $(this).parent().prev().prev().prev().prev().prev().prev().text();
        $.ajax({
            type: 'Post',
            url: 'backManagement.aspx',
            data: {
                sohId: id,
                state: state,
                op: "addMonomer"
            },
            dataType: 'text',
            success: function (succ) {
                if (succ == "处理中") {
                    window.location.href = 'backQuery.aspx?type=addMonomer';
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
                        //window, location.reload();
                    })
                }
            }
        })
    })
    //添加单头
    $("#btn-add").click(function () {
        $.ajax({
            type: 'Post',
            url: 'backManagement.aspx',
            data: {
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
                        window, location.reload();
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
                        //window, location.reload();
                    })
                }
            }
        })
    })
    //删除
    $("#table").delegate(".btndelete", "click", function () {
        var id = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().text();
        $.ajax({
            type: 'Post',
            url: 'backManagement.aspx',
            data: {
                sohId: id,
                op: "delete"
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
                        window, location.reload();
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
                        //window, location.reload();
                    })
                }
            }
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
            url: 'backManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}
