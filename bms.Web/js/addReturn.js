$(document).ready(function () {
    sessionStorage.setItem("kind", 0);
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
            url: 'addReturn.aspx?op=logout',
            datatype: 'text',
            data: {},
            success: function (data) {
                window.location.href = "../login.aspx";
            }
        });
    })
}

//只允许数字
//$("#table").delegate(".isbn", "keyup", function (e) {
//    $(this).val($(this).val().replace(/[^\w\.\/]/ig, ''));
//}).bind("paste", function () {  //CTR+V事件处理    
//    $(this).val($(this).val().replace(/[^\w\.\/]/ig, ''));
//}).css("ime-mode", "disabled");
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
        var _this = this;
        var sid = $(this).parent().prev().text().trim();
        var isbn = $(this).val().trim();
        var count = $(this).parent().next().next().next().next().children().val();
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
            _this.val("");
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
                url: 'addReturn.aspx',
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
                        $("#myModal").modal("show");
                        $("#table2 tr:not(:first)").empty(); //清空table处首行
                        $("#table2").append(data);
                    }
                    else if (data == "ISBN不存在") {
                        swal({
                            title: "温馨提示:)",
                            text: data,
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        _this.val("");
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
        sessionStorage.removeItem("not");
        var bookNum = $(this).parent().prev().prev().prev().text().trim();
        var sid = $(this).parent().prev().prev().prev().prev().prev().text().trim();
        var count = $(this).val().trim();
        $(this).text(count);
        var price = $(this).parent().next().text().trim();
        var last = $("#table tr:last").find("td").eq(1).children().text();
        var discount = $(this).parent().next().next().children().val().trim();
        var total = $(this).parent().next().next().next();
        var real = $(this).parent().next().next().next().next();
        total.text((count * price).toFixed(2));
        real.text((count * price * discount).toFixed(2));
        var _this = $(this);
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
            $.ajax({
                type: 'Post',
                url: 'addReturn.aspx',
                data: {
                    bookNum: bookNum,
                    count: count,
                    action: "checkNum"
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "库存不足") {
                        swal({
                            title: "温馨提示:)",
                            text: "库存不足",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        _this.val(0);
                    }
                    else {
                        if (last != "" || last == "0") {
                            var table = "<tr class='first'><td>" + (parseInt(sid) + 1) + "</td><td><textarea class='isbn textareaISBN' row='1' maxlength='13'></textarea></td><td></td><td></td><td><textarea class='count textareaCount' row='1'>0</textarea></td><td></td><td><textarea class='discount textareaDiscount' row='1'>0</textarea></td><td></td><td></td><td><button class='btn btn-danger btn-sm'><i class='fa fa-trash'></i></button></td></tr>";
                            $("#table").append(table);
                            $("#table tr:last").find("td").eq(1).children().focus();
                            sessionStorage.setItem("kind", parseInt(sessionStorage.getItem("kind")) + 1);
                            sessionStorage.setItem("not", "库存充足");
                        }
                    }
                }
            })
        }
    }
})
$("#table").delegate(".count", "change", function (e) {
    var bookNum = $(this).parent().prev().prev().prev().prev().text().trim();
    var count = $(this).val().trim();
    $(this).text(count);
    var price = $(this).parent().next().text();
    var discount = $(this).parent().next().next().text();
    var total = $(this).parent().next().next().next();
    var real = $(this).parent().next().next().next().next();
    total.text((count * price).toFixed(2));
    real.text((count * price * discount * 0.01).toFixed(2));
    var _this = $(this);
    //if (sessionStorage.getItem("not") == "库存充足") {
    //    $.ajax({
    //        type: 'Post',
    //        url: 'addReturn.aspx',
    //        data: {
    //            bookNum: bookNum,
    //            count: count,
    //            action: "checkNum"
    //        },
    //        dataType: 'text',
    //        success: function (data) {
    //            if (data == "库存不足") {
    //                swal({
    //                    title: "温馨提示:)",
    //                    text: data,
    //                    buttonsStyling: false,
    //                    confirmButtonClass: "btn btn-warning",
    //                    type: "warning"
    //                }).catch(swal.noop);
    //                _this.val(0);
    //            }
    //        }
    //    })
    //}
});

//删除当前行
$("#table").delegate(".btn-danger", "click", function () {
    $(this).parent().parent().remove();
    sessionStorage.setItem("kind", parseInt(sessionStorage.getItem("kind")) - 1);
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
        url: 'addReturn.aspx',
        data: {
            discount: discount,
            action: "changeDiscount"
        },
        dataType: 'text',
        success: function (data) {

        }
    })
});

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
            url: 'addReturn.aspx',
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
                    window.location.href = "returnManagement.aspx";
                    sessionStorage.removeItem("kind");
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
    } else {
        $.ajax({
            type: 'Post',
            url: 'addReturn.aspx',
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
                    $("#myModal").modal("hide");
                    $("#table tr:last").find("td").eq(5).children().focus();
                }
            }
        })
    }
})

$("#back").click(function () {
    window.location.href = "returnManagement.aspx";
})



