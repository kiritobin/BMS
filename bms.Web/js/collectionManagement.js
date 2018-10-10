$(document).ready(function () {
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
                url: 'collectionManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    book: book,
                    isbn: isbn,
                    op: "paging"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table tr:not(:first)").empty(); //清空table处首行
                    $("#table").append(data); //加载table
                }
            });
        }
    });


    //点击查询按钮时
    $("#btn-search").click(function () {
        var book = $("#bookSearch").val().trim();
        var isbn = $("#isbnSearch").val().trim();
        var custom = $("#cusSearch").val().trim();
        $.ajax({
            type: 'Post',
            url: 'collectionManagement.aspx',
            data: {
                book: book,
                isbn: isbn,
                custom: custom,
                op: "paging"
            },
            dataType: 'text',
            success: function (data) {
                $("#intPageCount").remove();
                $("#table tr:not(:first)").empty(); //清空table处首行
                $("#table").append(data); //加载table
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
                            url: 'collectionManagement.aspx',
                            data: {
                                page: api.getCurrent(), //页码
                                book: book,
                                isbn: isbn,
                                op: "paging"
                            },
                            dataType: 'text',
                            success: function (data) {
                                $("#table tr:not(:first)").empty(); //清空table处首行
                                $("#table").append(data); //加载table
                            }
                        });
                    }
                });
            }
        });
    });

    $("#btnImport").click(function () {
        var custom = $("#model-select-custom").val();
        var file = $("#file").val();
 
        if (custom == "" || custom == null) {
            swal({
                title: "提示",
                text: "请选择客户",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            })
        }
        else if (file == "" || file == null) {
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
        else if (sessionStorage.getItem("succ") != "上传成功") {
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
            $("#close").hide();
            $.ajax({
                type: 'Post',
                url: 'collectionManagement.aspx',
                data: {
                    custom:custom,
                    action: "import"
                },
                dataType: 'text',
                success: function (data) {
                    if (data.indexOf("导入成功") >= 0) {
                        $("#myModalLabe1").html(data);
                        $("#close").show();
                        $("#img").attr("src", "../imgs/success.png");
                        sessionStorage.setItem("import","导入成功");
                    } else if (data.indexOf("导入失败") >= 0) {
                        $("#myModalLabe1").html(data);
                        $("#close").show();
                        $("#img").attr("src", "../imgs/lose.png");
                        sessionStorage.setItem("import", "导入失败");
                    }
                    else {
                        sessionStorage.setItem("import", "导入失败");
                        $("#close").show();
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

    $("#btndel").click(function () {
        var custom = $("#sel-del").val().trim();
        if (custom == "") {
            swal({
                title: "提示",
                text: "请选择客户",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            })
        }
        else {
            swal({
                title: '温馨提示:)',
                text: '确定要删除吗？',
                type: 'warning',
                showCancelButton: true,
                confirmButtonText: '是的，删掉它!',
                cancelButtonText: '不，让我思考一下',
                confirmButtonClass: "btn btn-success",
                cancelButtonClass: "btn btn-danger",
                buttonsStyling: false,
                allowOutsideClick: false
            }).then(function () {
                $.ajax({
                    type: 'Post',
                    url: 'collectionManagement.aspx',
                    data: {
                        custom: custom,
                        action: "del"
                    },
                    dataType: 'text',
                    success: function (data) {
                        if (data == "删除成功") {
                            swal({
                                title: "温馨提示:)",
                                text: "用户删除成功",
                                buttonsStyling: false,
                                confirmButtonClass: "btn btn-success",
                                type: "success",
                                allowOutsideClick: false
                            }).then(function () {
                                window.location.reload();
                            })
                        } else {
                            swal({
                                title: "温馨提示:)",
                                text: "删除失败，无客户数据",
                                buttonsStyling: false,
                                confirmButtonClass: "btn btn-success",
                                type: "warning",
                                allowOutsideClick: false
                            }).then(function () {
                            })
                        }
                    }
                });
            })
        }
    })

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
        else if (type == ".xls") {
            ajaxFileUpload();
        }
        else {
            swal({
                title: "提示",
                text: "只允许上传.xls格式的文件",
                type: "warning",
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-success',
                buttonsStyling: false,
                allowOutsideClick: false
            })
        }
    });

    $("#close").click(function () {
        $("#close").show();
        $("#myModalLabe1").html("正在导入，请保持网络畅通，导入过程中请勿关闭页面");
        $("#img").attr("src", "../imgs/loading.gif");
        $(" #file").val("");
        
        if (sessionStorage.getItem("import")=="导入成功") {
            window.location.reload();
            sessionStorage.removeItem("import");
            sessionStorage.removeItem("succ");
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
                    console.log(data.msg);
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
                            swal({
                                title: "提示",
                                text: data.msg,
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-success',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            })
                        }
                    }
                },
                error: function (data, status, e)//服务器响应失败处理函数
                {
                    swal({
                        title: "提示",
                        text:e,
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

    //$("#table").delegate(".btn_delete", "click", function () {
    //    var libraryId = $(this).parent().prev().prev().prev().prev().prev().prev().text().trim();
    //    swal({
    //        title: '温馨提示:)',
    //        text: '确定要删除吗？',
    //        type: 'warning',
    //        showCancelButton: true,
    //        confirmButtonText: '是的，删掉它!',
    //        cancelButtonText: '不，让我思考一下',
    //        confirmButtonClass: "btn btn-success",
    //        cancelButtonClass: "btn btn-danger",
    //        buttonsStyling: false,
    //        allowOutsideClick: false
    //    }).then(function () {
    //        $.ajax({
    //            type: 'Post',
    //            url: 'collectionManagement.aspx',
    //            data: {
    //                libraryId: libraryId,
    //                action: "del"
    //            },
    //            dataType: 'text',
    //            success: function (succ) {
    //                if (succ == "删除成功") {
    //                    swal({
    //                        title: "温馨提示:)",
    //                        text: "用户删除成功",
    //                        buttonsStyling: false,
    //                        confirmButtonClass: "btn btn-success",
    //                        type: "success",
    //                        allowOutsideClick: false
    //                    }).then(function () {
    //                        window.location.reload();
    //                    })
    //                } else {
    //                    swal({
    //                        title: "温馨提示:)",
    //                        text: "删除失败",
    //                        buttonsStyling: false,
    //                        confirmButtonClass: "btn btn-success",
    //                        type: "warning",
    //                        allowOutsideClick: false
    //                    }).then(function () {
    //                    })
    //                }
    //            }
    //        })
    //    });
    //})
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
            url: 'collectionManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}
