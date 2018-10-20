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
$("#back").click(function () {
    window.location.href = "stockManagement.aspx";
})

$(document).ready(function () {
    sessionStorage.setItem("kind", 0);
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

    $("#close2").click(function () {
        $(" #file").val("");
        $("#close").show();
        $("#myModalLabe1").html("正在导入，请保持网络畅通，导入过程中请勿关闭页面");
        $("#img").attr("src", "../imgs/loading.gif");
        if (sessionStorage.getItem("import") == "导入成功") {
            sessionStorage.removeItem("import");
            sessionStorage.removeItem("succ");
            //window.location.href = "stockManageMent.aspx";
            window.location.reload();
        }
    });

    //$("#close").click(function () {
    //    $(" #file").val("");
        
    //});

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

    //$("#showIntersect").click(function () {
    //    var file = $("#file").val();
    //    if (file == "" || file == null) {
    //        swal({
    //            title: "提示",
    //            text: "请上传文件",
    //            type: "warning",
    //            confirmButtonColor: '#3085d6',
    //            confirmButtonText: '确定',
    //            confirmButtonClass: 'btn btn-success',
    //            buttonsStyling: false,
    //            allowOutsideClick: false
    //        })
    //    }
    //    else if (sessionStorage.getItem("succ") != "上传成功") {
    //        swal({
    //            title: "提示",
    //            text: "文件未上传成功",
    //            type: "success",
    //            confirmButtonColor: '#3085d6',
    //            confirmButtonText: '确定',
    //            confirmButtonClass: 'btn btn-success',
    //            buttonsStyling: false,
    //            allowOutsideClick: false
    //        })
    //    }
    //    else {
    //        $("#myModal2").modal("hide");
    //        $("#myModal1").modal("show");
    //        $("#myModalLabe1").html("正在读取数据");
    //        $("#close").hide();
    //        $.ajax({
    //            type: 'Post',
    //            url: 'addStock.aspx',
    //            data: {
    //                action: "showIntersect"
    //            },
    //            dataType: 'text',
    //            success: function (data) {
    //                if (data == "库存中找不到数据") {
    //                    swal({
    //                        title: "提示",
    //                        text: data,
    //                        type: "warning",
    //                        confirmButtonColor: '#3085d6',
    //                        confirmButtonText: '确定',
    //                        confirmButtonClass: 'btn btn-success',
    //                        buttonsStyling: false,
    //                        allowOutsideClick: false
    //                    })
    //                    $("#myModal2").modal("hide");
    //                    $("#myModal1").modal("hide");
    //                }
    //                else {
    //                    $("#myModal2").modal("show");
    //                    $("#myModal1").modal("hide");
    //                    $("#table2 tr:not(:first)").empty(); //清空table处首行
    //                    //$("#table2 tr:gt(1)").empty(); //清空table2行
    //                    $("#table2").append(data); //加载table
    //                }
    //            }
    //        });
    //    }
    //});

});

//只允许数字
$("#table").delegate(".isbn", "keyup", function (e){
    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
}).bind("paste", function () {  //CTR+V事件处理    
    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
    }).css("ime-mode", "disabled"); 
$("#table").delegate(".count", "keyup", function (e) {
    $(this).val($(this).val().replace(/[^\r\n0-9]/g, ''));
}).bind("paste", function () {  //CTR+V事件处理    
    $(this).val($(this).val().replace(/[^\r\n0-9]/g, ''));
    }).css("ime-mode", "disabled"); 
$("#table").delegate(".discount", "keyup", function (e) {
    $(this).val($(this).val().replace(/[^\r\n0-9]/g, ''));
}).bind("paste", function () {  //CTR+V事件处理    
    $(this).val($(this).val().replace(/[^\r\n0-9]/g, ''));
}).css("ime-mode", "disabled"); 
//isbn回车
$("#table").delegate(".isbn", "keypress", function (e) {
    if (e.keyCode == 13) {
        //$(".count").text('0');
        var sid = $(this).parent().prev().text().trim();
        var isbn = $(this).val().trim();
        var count = $(this).parent().next().next().next().next().children().val().trim();
        var discount = $(this).parent().next().next().next().next().next().next().children().val();
        var goodsId = $(this).parent().next().next().next().children().val();
        if (isbn == "" || isbn == null) {
            swal({
                title: "温馨提示:)",
                text: "ISBN不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
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
                    kind: sessionStorage.getItem("kind"),
                    sid: sid,
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
                        swal({
                            title: "温馨提示:)",
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                    }
                    else if (data == "已添加过此图书") {
                        swal({
                            title: "温馨提示:)",
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                    }
                    else {
                        $(".first").remove();
                        $("#table").append(data);
                        $("#table tr:last").find("td").eq(5).children().focus();
                    }
                }
            })
        }
    }
})

//数量回车
$("#table").delegate(".count","keypress" ,function (e) {
    if (e.keyCode == 13) {
        var sid = $(this).parent().prev().prev().prev().prev().prev().prev().text().trim();
        var count = $(this).val().trim();
        $(this).text(count);
        var price = $(this).parent().next().text().trim();
        var goodsId = $(this).parent().prev().children().val();
        var gId = $(this).parent().next().next().next().next().next().next();
        gId.text(goodsId);
        var last = $("#table tr:last").find("td").eq(1).children().text();
        var discount = $(this).parent().next().next().children().val().trim();
        var total = $(this).parent().next().next().next();
        var real = $(this).parent().next().next().next().next();
        total.text((count * price).toFixed(2));
        real.text((count * price * discount).toFixed(2));
        if (count <= 0) {
            swal({
                title: "温馨提示:)",
                text: "数量不能为空",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            if (last != "" || last == "0") {
                var table = "<tr class='first'><td>" + (parseInt(sid) + 1) + "</td><td><textarea class='isbn textareaISBN' row='1' maxlength='13'></textarea></td><td></td><td></td><td></td><td><textarea class='count textareaCount' row='1'>0</textarea></td><td></td><td><textarea class='discount textareaDiscount' row='1'>0</textarea></td><td></td><td></td><td><button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button></td></tr>";
                $("#table").append(table);
                $("#table tr:last").find("td").eq(1).children().focus();
                sessionStorage.setItem("kind", parseInt(sessionStorage.getItem("kind")) + 1);
            }
        }
    }
})
$("#table").delegate(".count", "change", function (e) {
    var count = $(this).val().trim();
    $(this).text(count);
    var price = $(this).parent().next().text();
    var discount = $(this).parent().next().next().text();
    var total = $(this).parent().next().next().next();
    var real = $(this).parent().next().next().next().next();
    total.text((count * price).toFixed(2));
    real.text((count * price * discount * 0.01).toFixed(2));
});
//下拉列表改变
$("#table").delegate(".goods", "change", function () {
    var goodsId = $(this).val().trim();
    var gId = $(this).parent().next().next().next().next().next().next().next();
    gId.text(goodsId);
    $(this).parent().next().children().focus();
});
//折扣改变事件
$("#table").delegate(".discount", "change", function () {
    var c = $(this).val().trim();
    $(this).text(c);
    var count = $(this).parent().prev().prev().children().text();
    var price = $(this).parent().prev().text();
    var discount = $(this).text();
    var total = $(this).parent().next();
    var real = $(this).parent().next().next();
    total.text((count * price).toFixed(2));
    real.text((count * price * discount * 0.01).toFixed(2));
        $.ajax({
            type: 'Post',
            url: 'addStock.aspx',
            data: {
                discount: discount,
                action: "changeDiscount"
            },
            dataType: 'text',
            success: function (data) {

            }
        })
});
//删除当前行
$("#table").delegate(".btn-danger", "click", function () {
    $(this).parent().parent().remove();
    sessionStorage.setItem("kind", parseInt(sessionStorage.getItem("kind")) - 1);
});

$("#btnImport").click(function () {
    var file = $("#file").val();
    var goods = $("#goods").val();
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
    else if (sessionStorage.getItem("succ") != "上传成功") {
        swal({
            title: "温馨提示:)",
            text: "文件未上传成功",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-success",
            type: "warning",
            allowOutsideClick: false
        })
    }
    else if (goods==""||goods==null) {
        swal({
            title: "温馨提示:)",
            text: "请选择货架",
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
            url: 'addStock.aspx',
            data: {
                goods:goods,
                action: "import"
            },
            dataType: 'text',
            success: function (data) {
                if (data.indexOf("导入成功") >= 0) {
                    //swal({
                    //    title: "提示",
                    //    text: "保存成功",
                    //    type: "success",
                    //    confirmButtonColor: '#3085d6',
                    //    confirmButtonText: '确定',
                    //    confirmButtonClass: 'btn btn-success',
                    //    buttonsStyling: false,
                    //    allowOutsideClick: false
                    //}).then(function () {
                    //    //sessionStorage.setItem("save","succ");
                    //    window.location.reload();
                    //})
                    $("#myModalLabe1").html(data);
                    $("#close").show();
                    $("#img").attr("src", "../imgs/success.png");
                    sessionStorage.setItem("import", "导入成功");
                    //window.location.href = "stockManageMent.aspx";
                } else if (data.indexOf("导入失败") >= 0) {
                    $("#myModalLabe1").html(data);
                    $("#close").show();
                    $("#img").attr("src", "../imgs/lose.png");
                    sessionStorage.setItem("import", "导入失败");
                }
                else {
                    $("#myModal1").modal("hide");
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
    }
});
$("#close").click(function () {
    window.location.href = "stockManageMent.aspx";
})

$("#btnAdd").click(function () {
    var sid = $("#table tr:last").find("td").eq(0).text();
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
                sid:sid,
                bookNum: bookNum,
                action: "add"
            },
            datatype: 'text',
            success: function (succ) {
                if (succ == "已添加过此图书") {
                    swal({
                        title: "温馨提示:)",
                        text: succ,
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                }
                else {
                    $(".first").remove();
                    $("#table").append(succ);
                    $("#myModal3").modal("hide");
                    $("#table tr:last").find("td").eq(5).children().focus();
                }
            }
        })
    }
})

$("#save").click(function () {
    var last = $('#table tr:last').find('td').eq(1).children().text();
    swal({
        title: "温馨提示",
        text: "您确定要保存吗？",
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
        if (last == "") {
            $("#table tr:last").remove();
        }
        var table = $('#table').tableToJSON(); // Convert the table into a javascript object
        var json = JSON.stringify(table);
        $.ajax({
            type: 'Post',
            url: 'addStock.aspx',
            data: {
                json: json,
                action: "save"
            },
            datatype: 'text',
            success: function (succ) {
                if (succ == "添加成功") {
                    swal({
                        title: "温馨提示:)",
                        text: "保存成功",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-success",
                        type: "success",
                        allowOutsideClick: false
                    })
                    sessionStorage.removeItem("kind");
                    window.location.href = "stockManagement.aspx";
                }
                else {
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
        })
    })
})
