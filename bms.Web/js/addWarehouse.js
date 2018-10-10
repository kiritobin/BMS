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
            url: 'addWarehouse.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

$(document).ready(function () {
    sessionStorage.setItem("kind", 0);
})

//回车事件
$("#isbn").keypress(function (e) {
    if (e.keyCode == 13) {
        $("#btnAdd").attr("disabled", false);
        var billCount = $("#billCount").val();
        var disCount = $("#disCount").val();
        var isbn = $("#isbn").val();
        if (isbn == "" || isbn == null) {
            swal({
                title: "温馨提示:)",
                text: "ISBN不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (billCount == null || billCount=="") {
            swal({
                title: "温馨提示:)",
                text: "商品数量不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (disCount == "" || disCount == null) {
            swal({
                title: "温馨提示:)",
                text: "折扣不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'addWarehouse.aspx',
                data: {
                    billCount: billCount,
                    disCount: disCount,
                    isbn: isbn,
                    op: "isbn"
                },
                dataType: 'text',
                success: function (data) {
                    if (data=="添加成功") {
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
                    }
                    else if (data == "添加失败") {
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        $("#btnAdd").attr("disabled", true);
                    }
                    else if (data == "库存数量不足"){
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        $("#btnAdd").attr("disabled", true);
                    }
                    else if (data == "已添加过相同记录") {
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        $("#btnAdd").attr("disabled", true);
                    }
                    else {
                        $("#btnAdd").attr("disabled", false);
                        $("#table2 tr:not(:first)").empty(); //清空table处首行
                        $("#table2").append(data); //加载table
                    }
                }
            })
        }
    }
})
$("#billCount").keypress(function (e) {
    if (e.keyCode == 13) {
        $("#btnAdd").attr("disabled", false);
        var billCount = $("#billCount").val();
        var disCount = $("#disCount").val();
        var isbn = $("#isbn").val();
        if (isbn == "" || isbn == null) {
            swal({
                title: "温馨提示:)",
                text: "ISBN不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (billCount == null || billCount == "") {
            swal({
                title: "温馨提示:)",
                text: "商品数量不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (disCount == "" || disCount == null) {
            swal({
                title: "温馨提示:)",
                text: "折扣不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'addWarehouse.aspx',
                data: {
                    billCount: billCount,
                    disCount: disCount,
                    isbn: isbn,
                    op: "isbn"
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "添加成功") {
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
                    }
                    else if (data == "添加失败") {
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        $("#btnAdd").attr("disabled", true);
                    }
                    else if (data == "库存数量不足") {
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        $("#btnAdd").attr("disabled", true);
                    }
                    else if (data == "已添加过相同记录") {
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        $("#btnAdd").attr("disabled", true);
                    }
                    else {
                        $("#btnAdd").attr("disabled", false);
                        $("#table2 tr:not(:first)").empty(); //清空table处首行
                        $("#table2").append(data); //加载table
                    }
                }
            })
        }
    }
})
$("#disCount").keypress(function (e) {
    if (e.keyCode == 13) {
        $("#btnAdd").attr("disabled", false);
        var billCount = $("#billCount").val();
        var disCount = $("#disCount").val();
        var isbn = $("#isbn").val();
        if (isbn == "" || isbn == null) {
            swal({
                title: "温馨提示:)",
                text: "ISBN不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (billCount == null || billCount == "") {
            swal({
                title: "温馨提示:)",
                text: "商品数量不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else if (disCount == "" || disCount == null) {
            swal({
                title: "温馨提示:)",
                text: "折扣不能为空，请您重新输入",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        }
        else {
            $.ajax({
                type: 'Post',
                url: 'addWarehouse.aspx',
                data: {
                    billCount: billCount,
                    disCount: disCount,
                    isbn: isbn,
                    op: "isbn"
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "添加成功") {
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
                    }
                    else if (data == "添加失败") {
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        $("#btnAdd").attr("disabled", true);
                    }
                    else if (data == "库存数量不足") {
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        $("#btnAdd").attr("disabled", true);
                    }
                    else if (data == "已添加过相同记录") {
                        swal({
                            title: data,
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        $("#btnAdd").attr("disabled", true);
                    }
                    else {
                        $("#btnAdd").attr("disabled", false);
                        $("#table2 tr:not(:first)").empty(); //清空table处首行
                        $("#table2").append(data); //加载table
                    }
                }
            })
        }
    }
})

//返回按钮点击事件
$("#back").click(function () {
    window.location.href = "warehouseManagement.aspx";
})

//只允许数字
$("#table").delegate(".isbn", "keyup", function (e) {
    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
}).bind("paste", function () {  //CTR+V事件处理    
    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
}).css("ime-mode", "disabled");
$("#table").delegate(".count", "keyup", function (e) {
    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
}).bind("paste", function () {  //CTR+V事件处理    
    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
}).css("ime-mode", "disabled");
$("#table").delegate(".discount", "keyup", function (e) {
    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
}).bind("paste", function () {  //CTR+V事件处理    
    $(this).val($(this).val().replace(/[^0-9.]/g, ''));
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
                url: 'addWarehouse.aspx',
                data: {
                    kind: sessionStorage.getItem("kind"),
                    sid: sid,
                    isbn: isbn,
                    billCount: count,
                    discount: discount,
                    action: "isbn"
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
$("#table").delegate(".count", "keypress", function (e) {
    if (e.keyCode == 13) {
        var sid = $(this).parent().prev().prev().prev().prev().prev().prev().text().trim();
        var count = $(this).val().trim();
        $(this).text(count);
        var price = $(this).parent().next().text().trim();
        var goodsId = $(this).parent().prev().children().val().trim();
        var gId = $(this).parent().next().next().next().next().next().next();
        gId.text(goodsId);
        var last = $("#table tr:last").eq(1).text().trim();
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

        //$.ajax({
        //    type: 'Post',
        //    url: 'addStock.aspx',
        //    data: {
        //        sid:sid,
        //        action: "count"
        //    },
        //    dataType: 'text',
        //    success: function (data) {

        //    }
        //})
    }
})
$("#table").delegate(".count", "change", function (e) {
    var count = $(this).val().trim();
    $(this).text(count);
});
//下拉列表改变
$("#table").delegate(".goods", "change", function () {
    var goodsId = $(this).val().trim();
    var gId = $(this).parent().next().next().next().next().next().next().next();
    gId.text(goodsId);
    $(this).parent().next().children().focus();
});
//删除当前行
$("#table").delegate(".btn-danger", "click", function () {
    $(this).parent().parent().remove();
    sessionStorage.setItem("kind", parseInt(sessionStorage.getItem("kind")) - 1);
});
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
    } {
        $.ajax({
            type: 'Post',
            url: 'addStock.aspx',
            data: {
                sid: sid,
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
//折扣改变事件
$("#table").delegate(".discount", "change", function () {
    var c = $(this).val().trim();
    $(this).text(c);
    var count = $(this).parent().prev().prev().children().text();
    var price = $(this).parent().prev().text();
    var discount = $(this).text();
    var total = $(this).parent().next();
    var real = $(this).parent().next().next();
    alert((count * price * discount).toFixed(2));
    total.text((count * price).toFixed(2));
    real.text((count * price * discount).toFixed(2));
});
$("#save").click(function () {
    
})

