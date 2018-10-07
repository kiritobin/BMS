﻿$(document).ready(function () {
    $("#upload").click(function () {
        var location = $("input[name='file']").val();
        var point = location.lastIndexOf(".");
        var type = location.substr(point).toLowerCase();
        var uploadFiles = document.getElementById("file").files;
        if (uploadFiles.length == 0) {
            swal({
                title: "温馨提示:)",
                text: "请选择要上传的文件",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-success",
                type: "warning",
                allowOutsideClick: false
            })
        }
        else if (type == ".xls") {
            ajaxFileUpload();
        }
        else {
            swal({
                title: "温馨提示:)",
                text: "只允许上传.xls格式的文件",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-success",
                type: "warning",
                allowOutsideClick: false
            })
        }
    });

    $("#btnImport").click(function () {
        var file = $("#file").val();
        if (file == "" || file == null) {
            swal({
                title: "温馨提示:)",
                text: "请上传文件",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-success",
                type: "warning",
                allowOutsideClick: false
            })
        }
        else if (sessionStorage.getItem("succ")!="上传成功") {
            swal({
                title: "温馨提示:)",
                text: "文件未上传成功",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-success",
                type: "warning",
                allowOutsideClick: false
            })
        }
        else {
            $("#myModal1").modal("show");
            $("#close").hide();
            $.ajax({
                type: 'Post',
                url: 'bookBasicManagement.aspx',
                data: {
                    action: "import"
                },
                dataType: 'text',
                success: function (data) {
                    if (data.indexOf("导入成功") >= 0) {
                        $("#myModalLabe1").html(data);
                        $("#close").show();
                        $("#img").attr("src", "../imgs/success.png");
                        sessionStorage.setItem("import", "导入成功");
                        sessionStorage.removeItem("succ");
                    } else if (data.indexOf("导入失败") >= 0) {
                        $("#myModalLabe1").html(data);
                        $("#close").show();
                        $("#img").attr("src", "../imgs/lose.png");
                        sessionStorage.removeItem("succ");
                        sessionStorage.setItem("import", "导入失败");
                    }
                    else {
                        $("#close").show();
                        swal({
                            title: "温馨提示:)",
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-success",
                            type: "warning",
                            allowOutsideClick: false
                        })
                        sessionStorage.setItem("import", "导入失败");
                    }
                }
            });
        }
    });

    $("#close").click(function () {
        $("#close").show();
        $("#myModalLabe1").html("正在导入，请保持网络畅通，导入过程中请勿关闭页面");
        $("#img").attr("src", "../imgs/loading.gif");
        $(" #file").val("");
        sessionStorage.removeItem("import");
        sessionStorage.removeItem("succ");
        if (sessionStorage.getItem("import")=="导入成功") {
            window.location.reload();
        }
    });

    function ajaxFileUpload() {
        $.ajaxFileUpload(
            {
                url: '../CustomerMGT/upload.aspx', //用于文件上传的服务器端请求地址
                secureuri: false, //是否需要安全协议，一般设置为false
                fileElementId: 'file', //文件上传域的ID
                dataType: 'json', //返回值类型 一般设置为json
                success: function (data, status)  //服务器成功响应处理函数
                {
                    console.log(data.msg);
                    if (typeof (data.error) != 'undefined') {
                        if (data.error != '') {
                            swal({
                                title: "温馨提示:)",
                                text: data.error,
                                buttonsStyling: false,
                                confirmButtonClass: "btn btn-success",
                                type: "warning",
                                allowOutsideClick: false
                            })
                        } else {
                            swal({
                                title: "温馨提示:)",
                                text: data.msg,
                                buttonsStyling: false,
                                confirmButtonClass: "btn btn-success",
                                type: "warning",
                                allowOutsideClick: false
                            })
                            sessionStorage.setItem("succ", data.msg);
                        }
                    }
                },
                error: function (data, status, e)//服务器响应失败处理函数
                {
                    swal({
                        title: "温馨提示:)",
                        text:e,
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "warning",
                        allowOutsideClick: false
                    })
                }
            }
        );
        return false;
    }

    $(function () {
        $(".txtVerify").focus(function () {
            if ($(this).val().length == 0) {
                $(this).css("border-color", "#ddd");
            }
        });
        $(".txtVerify").blur(function () {
            if ($(this).val().length == 0) {
                $(this).css("border-color", "red");
            }
        });
        //提交按钮单机非空验证
        $("#btnAdd").click(function () {
            //$(".txtVerify").each(function () {
            //    var val = $(this).val().trim();
            //    if (val == "") {
            //        var name = $(this).attr("name");
            //        alert("您的" + name + "信息为空，请确认后再次提交");
            //        $(this).focus();
            //        return false;
            //    }
            //    else {
            //        alert("提交成功");
            //    }
            //});
            if ($(".txtTitle").val() == "") {
                swal({
                    title: "温馨提示:)",
                    text: "书名不能为空，请确认后再次提交!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
                $(this).focus();
            }
            else if ($(".txtAuthor").val() == "") {
                swal({
                    title: "温馨提示:)",
                    text: "作者不能为空，请确认后再次提交!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
                $(this).focus();
            }
            else if ($(".txtPrice").val() == "") {
                swal({
                    title: "温馨提示:)",
                    text: "价格不能为空，请确认后再次提交!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
                $(this).focus();
            }
            else if ($(".txtTime").val() == "") {
                swal({
                    title: "温馨提示:)",
                    text: "出版时间不能为空，请确认后再次提交!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
                $(this).focus();
            }
            else if ($(".txtPress").val() == "") {
                swal({
                    title: "温馨提示:)",
                    text: "出版社不能为空，请确认后再次提交!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
                $(this).focus();
            }
            else if ($(".txtISBN").val() == "") {
                swal({
                    title: "温馨提示:)",
                    text: "ISBN不能为空，请确认后再次提交!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
                $(this).focus();
            }
            else if ($(".txtCatalogue").val() == "") {
                swal({
                    title: "温馨提示:)",
                    text: "编目不能为空，请确认后再次提交!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
                $(this).focus();
            }
            else if ($(".txtId").val() == "") {
                swal({
                    title: "温馨提示:)",
                    text: "标识不能为空，请确认后再次提交!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
                $(this).focus();
            }
            else {
                swal({
                    title: "温馨提示:)",
                    text: "数据添加成功",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-success",
                    type: "success"
                }).catch(swal.noop)
            }
        });

    });


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
            var bookName = $("#bookName").val().trim();
            var bookNum = $("#bookNum").val().trim();
            var btnISBN = $("#bookISBN").val().trim();
            $.ajax({
                type: 'Post',
                url: 'bookBasicManagement.aspx',
                data: {
                    page: api.getCurrent(), //页码
                    bookName: bookName,
                    bookNum: bookNum,
                    btnISBN: btnISBN,
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
})

//点击查询按钮时
$("#btn-search").click(function () {
    var bookName = $("#bookName").val().trim();
    var bookNum = $("#bookNum").val().trim();
    var bookISBN = $("#bookISBN").val().trim();
    $.ajax({
        type: 'Post',
        url: 'bookBasicManagement.aspx',
        data: {
            bookName: bookName,
            bookNum: bookNum,
            bookISBN: bookISBN,
            op: "paging"
        },
        dataType: 'text',
        success: function (data) {
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
                        url: 'bookBasicManagement.aspx',
                        data: {
                            page: api.getCurrent(), //页码
                            bookName: bookName,
                            bookNum: bookNum,
                            bookISBN: bookISBN,
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
//点击删除
$("#table").delegate(".btn-delete", "click", function () {
    var bookNum = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
    swal({
        title: "温馨提示",
        text: "您确定要删除该书籍信息吗？",
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
            url: 'bookBasicManagement.aspx',
            data: {
                bookNum: bookNum,
                op: "del"
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
                        type: "success",
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
            url: 'bookBasicManagement.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

$("#retail").click(function () {
    $.ajax({
        type: 'Post',
        url: 'bookBasicManagement.aspx',
        data: {
            op: "retail"
        },
        dataType: 'text',
        success: function (succ) {
            
        }
    })
})
