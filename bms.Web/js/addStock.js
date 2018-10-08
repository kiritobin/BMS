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
            url: 'addStock.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

$("#btn-search").click(function () {
    var id = $("#ID").val();
    $.ajax({
        type: 'Post',
        url: 'addStock.aspx',
        data: {
            id: id,
            op:'paging'
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
                        url: 'addStock.aspx',
                        data: {
                            page: api.getCurrent(), //页码
                            id: id,
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
    })
})

$(document).ready(function () {
    sessionStorage.setItem("id", 1);
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
            $.ajax({
                type: 'Post',
                url: 'addStock.aspx',
                data: {
                    page: api.getCurrent(), //页码
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
    
    $('.paging2').pagination({
        //totalData: $("#countPage").val(), //数据总数
        //showData: $("#totalCount").val(), //每页显示的条数
        pageCount: $("#intPageCount2").val(), //总页数
        jump: false,
        mode: 'fixed',//固定页码数量
        coping: true,
        homePage: '首页',
        endPage: '尾页',
        prevContent: '上页',
        nextContent: '下页',
        callback: function (api) {
            $.ajax({
                type: 'Post',
                url: 'addStock.aspx',
                data: {
                    page: api.getCurrent(),
                    action:"showIntersect"
                },
                dataType: 'text',
                success: function (data) {
                    $("#table2 tr:gt(1)").empty(); //清空table2行
                    $("#table2").append(data); //加载table
                }
            });
        }
    });

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
        $(" #file").val("");
        sessionStorage.removeItem("import");
        sessionStorage.removeItem("succ");
        $("#close").show();
        $("#myModalLabe1").html("正在导入，请保持网络畅通，导入过程中请勿关闭页面");
        $("#img").attr("src", "../imgs/loading.gif");
        if (sessionStorage.getItem("import")=="导入成功") {
            window.location.reload();
        }
    });

    $("#close2").click(function () {
        $(" #file").val("");
        sessionStorage.removeItem("import");
        sessionStorage.removeItem("succ");
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

    $("#showIntersect").click(function () {
        var file = $("#file").val();
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
            $("#myModal2").modal("hide");
            $("#myModal1").modal("show");
            $("#myModalLabe1").html("正在读取数据");
            $("#close").hide();
            $.ajax({
                type: 'Post',
                url: 'addStock.aspx',
                data: {
                    action: "showIntersect"
                },
                dataType: 'text',
                success: function (data) {
                    $("#intPageCount2").remove();
                    $(".paging2").empty();
                    $("#myModal2").modal("show");
                    $("#myModal1").modal("hide");
                    $("#table2 tr:gt(1)").empty(); //清空table2行
                    $("#table2").append(data); //加载table

                    $('.paging2').pagination({
                        //totalData: $("#countPage").val(), //数据总数
                        //showData: $("#totalCount").val(), //每页显示的条数
                        pageCount: $("#intPageCount2").val(), //总页数
                        jump: false,
                        mode: 'fixed',//固定页码数量
                        coping: true,
                        homePage: '首页',
                        endPage: '尾页',
                        prevContent: '上页',
                        nextContent: '下页',
                        callback: function (api) {
                            $.ajax({
                                type: 'Post',
                                url: 'addStock.aspx',
                                data: {
                                    page: api.getCurrent(),
                                    action: "showIntersect"
                                },
                                dataType: 'text',
                                success: function (data) {
                                    $("#table2 tr:gt(1)").empty(); //清空table2行
                                    $("#table2").append(data); //加载table
                                }
                            });
                        }
                    });
                }
            });
        }
    });
});
//isbn回车
$("#table").delegate(".isbn", "keypress", function (e) {
    if (e.keyCode == 13) {
        var isbn = $(this).val();
        var count = $(this).parent().next().next().next().next().children().val();
        var discount = $(this).parent().next().next().next().next().next().next().children().val();
        if (isbn == "" || isbn == null) {
            alert("isbn不能为空");
        }
        //else if (count == "" || count == null) {
        //    $(this).parent().next().next().next().children().focus();
        //    alert("商品数量不能为空");
        //}
        //else if (discount == "" || discount == null) {
        //    alert("折扣不能为空");
        //}
        else {
            $.ajax({
                type: 'Post',
                url: 'addStock.aspx',
                data: {
                    isbn: isbn,
                    billCount: count,
                    discount: discount,
                    action:"isbn"
                },
                dataType: 'text',
                success: function (data) {
                    if (data.indexOf("much") >= 0) {
                        $("#myModal3").modal("show");
                        $("#table3 tr:not(:first)").empty(); //清空table处首行
                        $("#table3").append(data);
                    }
                    else if (data == "ISBN不存在") {
                        alert("ISBN不存在");
                    }
                    else if (data == "记录已存在") {
                        alert("记录已存在");
                    }
                    else {
                        $(".first").remove();
                        $("#table").append(data);
                        $(this).parent().next().next().next().next().children().focus();
                    }
                }
            })
           
        }
    }
})
//数量回车
$("#table").delegate(".count","keypress" ,function (e) {
    if (e.keyCode == 13) {
        var count = $(this).val();
        var price = $(this).parent().next().text();
        //var isbn = $(this).parent().prev().prev().prev().prev().children().val();
        //var count = $(this).val();
        var discount = $(this).parent().next().next().children().val();
        var total = $(this).parent().next().next().next();
        var real = $(this).parent().next().next().next().next();
        total.text(count * price);
        real.text(count * price * discount);
        if (count <= 0) {
            alert("数量不能为空");
        }
        else {
            var table = "<tr class='first'><td></td><td><input type='text' class='isbn search' value='' /></td><td></td><td></td><td></td><td><input type='text' class='count search' value='0' /></td><td></td><td><input type='text' class='discount search' value='0'</td><td></td><td></td><td><button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button></td></tr>";
            $("#table").append(table);
        }
    }
})

//下拉列表改变
$("#table").delegate(".goods", "change",function () {
    $(this).parent().next().children().focus();
});
//删除当前行
$("#table").delegate(".btn-danger", "click", function () {
    $(this).parent().parent().remove();
});


//回车事件
$("#isbn").keypress(function (e) {
    if (e.keyCode == 13) {
        $("#btnAdd").attr("disabled", false);
        var isbn = $("#isbn").val();
        var billCount = $("#billCount").val();
        var disCount = $("#disCount").val();
        var goodsShelf = $("#goodsShelf").val();
        if (isbn == "" || isbn == null) {
            swal({
                title: "温馨提示:)",
                text: "ISBN不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else if (billCount == "" || billCount == null) {
            swal({
                title: "温馨提示:)",
                text: "商品数量不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else if (disCount == "" || disCount == null) {
            swal({
                title: "温馨提示:)",
                text: "折扣不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else {
            $.ajax({
                type: 'Post',
                url: 'addStock.aspx',
                data: {
                    isbn: isbn,
                    billCount: billCount,
                    disCount: disCount,
                    goodsShelf: goodsShelf,
                    op: "isbn"
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "ISBN不存在") {
                        swal({
                            title: "错误提示:)",
                            text: "ISBN不存在",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                    } else if (data == "添加成功") {
                        swal({
                            title: data,
                            text: data,
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window.location.reload();
                        })
                    } else if (data == "添加失败") {
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                    } else {
                        $("#table3 tr:not(:first)").empty(); //清空table处首行
                        $("#table3").append(data); //加载table
                    }
                }
            })
        }
    }
})
$("#billCount").keypress(function (e) {
    if (e.keyCode == 13) {
        $("#btnAdd").attr("disabled", false);
        var isbn = $("#isbn").val();
        var billCount = $("#billCount").val();
        var disCount = $("#disCount").val();
        var goodsShelf = $("#goodsShelf").val();
        if (isbn == "" || isbn == null) {
            swal({
                title: "温馨提示:)",
                text: "ISBN不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else if (billCount == "" || billCount == null) {
            swal({
                title: "温馨提示:)",
                text: "商品数量不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else if (disCount == "" || disCount == null) {
            swal({
                title: "温馨提示:)",
                text: "折扣不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else {
            $.ajax({
                type: 'Post',
                url: 'addStock.aspx',
                data: {
                    isbn: isbn,
                    billCount: billCount,
                    disCount: disCount,
                    goodsShelf: goodsShelf,
                    op: "isbn"
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "ISBN不存在") {
                        swal({
                            title: "错误提示:)",
                            text: "ISBN不存在",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                    } else if (data == "添加成功") {
                        swal({
                            title: data,
                            text: data,
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window.location.reload();
                        })
                    } else if (data == "添加失败") {
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                    } else {
                        $("#table3 tr:not(:first)").empty(); //清空table处首行
                        $("#table3").append(data); //加载table
                    }
                }
            })
        }
    }
})
$("#disCount").keypress(function (e) {
    if (e.keyCode == 13) {
        $("#btnAdd").attr("disabled", false);
        var isbn = $("#isbn").val();
        var billCount = $("#billCount").val();
        var disCount = $("#disCount").val();
        var goodsShelf = $("#goodsShelf").val();
        if (isbn == "" || isbn == null) {
            swal({
                title: "温馨提示:)",
                text: "ISBN不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else if (billCount == "" || billCount == null) {
            swal({
                title: "温馨提示:)",
                text: "商品数量不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else if (disCount == "" || disCount == null) {
            swal({
                title: "温馨提示:)",
                text: "折扣不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else {
            $.ajax({
                type: 'Post',
                url: 'addStock.aspx',
                data: {
                    isbn: isbn,
                    billCount: billCount,
                    disCount: disCount,
                    goodsShelf: goodsShelf,
                    op: "isbn"
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "ISBN不存在") {
                        swal({
                            title: "错误提示:)",
                            text: "ISBN不存在",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                    } else if (data == "添加成功") {
                        swal({
                            title: data,
                            text: data,
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            window.location.reload();
                        })
                    } else if (data == "添加失败") {
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                    } else {
                        $("#table3 tr:not(:first)").empty(); //清空table处首行
                        $("#table3").append(data); //加载table
                    }
                }
            })
        }
    }
})

$("#btnImport").click(function () {
    $("#myModal2").modal("hide");
    $("#myModal1").modal("show");
    $("#close").hide();
    $("#myModalLabe1").html("正在导入");
            $.ajax({
                type: 'Post',
                url: 'addStock.aspx',
                data: {
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
                        sessionStorage.setItem("import", "导入失败");
                    }
                }
            });
});

$("#btnAdd").click(function () {
    var bookNum = $("input[type='radio']:checked").val();
    if (bookNum == "" || bookNum == null) {
        swal({
            title: "温馨提示:)",
            text: "请选择一条图书信息",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    }  {
        $.ajax({
            type: 'Post',
            url: 'addStock.aspx',
            data: {
                bookNum: bookNum,
                action: "add"
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
                        window.location.reload();
                    })
                }
            }
        })
    }
})
