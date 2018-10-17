﻿$(document).ready(function () {
    //sessionStorage.removeItem("save");
    //打印
    $("#print").click(function () {
        $("#print_content").jqprint();
    })
    //只允许输入数字
    $("#table").delegate("#inputISBN", "keyup", function (e) {
        $(this).val($(this).val().replace(/[^\r\n0-9]/g, ''));
    }).bind("paste", function () {  //CTR+V事件处理    
        $(this).val($(this).val().replace(/[^\r\n0-9]/g, ''));
    }).css("ime-mode", "disabled");
    $("#table").delegate("#inputCount", "keyup", function (e) {
        $(this).val($(this).val().replace(/[^\r\n\-0-9]/g, ''));//允许输入“-”运算符号
    }).bind("paste", function () {  //CTR+V事件处理    
        $(this).val($(this).val().replace(/[^\r\n\-0-9]/g, ''));
    }).css("ime-mode", "disabled");
    //回车事件
    $(".addsell").keypress(function (e) {
        if (e.keyCode == 13) {
            var ISBN = $("#isbn").val();
            var realDiscount = $("#realDiscount").val();
            var count = $("#count").val();
            if (ISBN == "" || realDiscount == "" || count == "") {
                swal({
                    title: "温馨提示:)",
                    text: "不能含有未填项!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
            else if (count == "0") {
                swal({
                    title: "温馨提示:)",
                    text: "数量不能未0!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
            else {
                $.ajax({
                    type: 'Post',
                    url: 'backQuery.aspx',
                    data: {
                        ISBN: ISBN,
                        discount: realDiscount,
                        count: count,
                        op: "search"
                    },
                    dataType: 'text',
                    success: function (data) {
                        if (data == "添加成功" || data == "更新成功") {
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
                            //$("#tablebook th:not(:first)").empty(); //清空table处首行
                            $(".first").remove();
                            //$("#tablebook").empty();
                            $("#table").append(data); //加载table
                        }
                    }
                });
            }
        }
    })
    //销退单体添加
    $("#add_back").click(function () {
        //var discount = $("#sellId").val();
        //$("#realDiscount").val(discount);
        $("#myModa2").modal("show");
    })
    //返回事件
    $("#toBack").click(function () {
        window.location.href = "backManagement.aspx";
    });
    //保存单据
    $("#sure").click(function () {
        swal({
            title: "温馨提示:)",
            text: "保存单据后将无法再进行修改",
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
                url: 'backQuery.aspx',
                data: {
                    op: "sure"
                },
                dataType: 'text',
                success: function (succ) {
                    if (succ == "更新成功") {
                        swal({
                            title: "提示",
                            text: "保存成功",
                            type: "success",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-success',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                            //sessionStorage.setItem("save","succ");
                            window.location.reload();
                        })
                    } else {
                        swal({
                            title: "提示",
                            text: succ,
                            type: "warning",
                            confirmButtonColor: '#3085d6',
                            confirmButtonText: '确定',
                            confirmButtonClass: 'btn btn-danger',
                            buttonsStyling: false,
                            allowOutsideClick: false
                        }).then(function () {
                        })
                    }
                }
            })
        })
    })
    //ISBN输入框回车事件，查询到信息后返回页面进行显示
    $("#table").delegate("#inputISBN", "keypress", function (e) {
        if (e.keyCode == 13) {
            var isbn = $('table input:eq(0)').val();
            if (isbn == "") {
                swal({
                    title: "温馨提示:)",
                    text: "不能含有未填项!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            } else {
                $.ajax({
                    type: 'Post',
                    url: 'backQuery.aspx',
                    data: {
                        ISBN: isbn,
                        op: "search"
                    },
                    dataType: 'text',
                    success: function (data) {
                        if (data == "暂无此数据") {
                            swal({
                                title: "温馨提示",
                                text: data,
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-warning',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                                $("#table").find('tr').eq(1).find('td').eq(1).find('input').focus();
                            })
                        }
                        else if (data.indexOf("much") > 0) {
                            $("#tablebook").empty();
                            $("#myModa2").modal("show");
                            $("#tablebook").append(data); //加载table
                        }
                        else {
                            $("#table").empty();
                            $("#table").append(data); //加载table
                            $("#table tr:first").find("td").eq(5).children().focus();
                            $("#table").find('tr').eq(1).find('td').eq(4).find('input').focus();
                        }
                    }
                });
            }
        }
    })
    //一号多书时弹出模态框，确定书籍
    $("#sureBook").click(function () {
        var isbn = $("input[type='radio']:checked").parents('tr').find('td').eq(1).text();
        alert(isbn);
        var bookNum = $("input[type='radio']:checked").val();
        var price = $("input[type='radio']:checked").parents('tr').find('td').eq(4).text();
        var discount = $("input[type='radio']:checked").parents('tr').find('td').eq(4).text();
        $.ajax({
            type: 'Post',
            url: 'backQuery.aspx',
            data: {
                ISBN: isbn,
                bookNO: bookNum,
                price: price,
                op: "search"
            },
            dataType: 'text',
            success: function (data) {
                if (data == "销售单据中无此数据") {
                    swal({
                        title: "温馨提示",
                        text: data,
                        type: "warning",
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: '确定',
                        confirmButtonClass: 'btn btn-warning',
                        buttonsStyling: false,
                        allowOutsideClick: false
                    }).then(function () {
                        $("#myModa2").modal("show");
                    })
                }
                else {
                    $("#myModa2").modal("hide");
                    $("#table").empty();
                    $("#table").append(data); //加载table
                    $("#table tr:first").find("td").eq(5).children().focus();
                    $("#table").find('tr').eq(1).find('td').eq(4).find('input').focus();
                }
            }
        })
    })
    //添加数量输入框回车事件
    $("#table").delegate("#inputCount", "keypress", function (e) {
        if (e.keyCode == 13) {
            var isbn = $("#table").find('tr').eq(1).find('td').eq(1).find('input').val();
            var num = $("#table").find('tr').eq(1).find('td').eq(4).find('input').val();
            var bookNO = $("#table").find('tr').eq(1).find('td').eq(2).text();
            if (num == "") {
                swal({
                    title: "温馨提示:)",
                    text: "不能含有未填项!",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            } else {
                $.ajax({
                    type: 'Post',
                    url: 'backQuery.aspx',
                    data: {
                        ISBN: isbn,
                        count: num,
                        bookNum: bookNO,
                        op: "add"
                    },
                    dataType: 'text',
                    success: function (data) {
                        if (data == "添加成功") {
                            swal({
                                title: "温馨提示",
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
                        else {
                            swal({
                                title: "温馨提示",
                                text: data,
                                type: "warning",
                                confirmButtonColor: '#3085d6',
                                confirmButtonText: '确定',
                                confirmButtonClass: 'btn btn-warning',
                                buttonsStyling: false,
                                allowOutsideClick: false
                            }).then(function () {
                                $("#table").find('tr').eq(1).find('td').eq(4).find('input').focus();
                            })
                        }
                    }
                });
            }
        }
    })

})

//处理键盘事件 禁止后退键（Backspace）密码或单行、多行文本框除外
function banBackSpace(e) {
    var ev = e || window.event;//获取event对象
    var obj = ev.target || ev.srcElement;//获取事件源
    var t = obj.type || obj.getAttribute('type');//获取事件源类型
    //获取作为判断条件的事件类型
    var vReadOnly = obj.getAttribute('readonly');
    //处理null值情况
    vReadOnly = (vReadOnly == "") ? false : vReadOnly;
    //当敲Backspace键时，事件源类型为密码或单行、多行文本的，
    //并且readonly属性为true或enabled属性为false的，则退格键失效
    var flag1 = (ev.keyCode == 8 && (t == "password" || t == "text" || t == "textarea")
                && vReadOnly == "readonly") ? true : false;
    //当敲Backspace键时，事件源类型非密码或单行、多行文本的，则退格键失效
    var flag2 = (ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea")
                ? true : false;

    //判断
    if (flag2) {
        return false;
    }
    if (flag1) {
        return false;
    }
}

window.onload = function () {
    //禁止后退键 作用于Firefox、Opera
    document.onkeypress = banBackSpace;
    //禁止后退键  作用于IE、Chrome
    document.onkeydown = banBackSpace;
}