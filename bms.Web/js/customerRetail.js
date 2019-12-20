$(document).ready(function () {
    $("#search").focus();
    sessionStorage.setItem("kind", 0);
    sessionStorage.setItem("totalPrice", 0);
    sessionStorage.setItem("realPrice", 0);
    sessionStorage.setItem("content", "show");
    sessionStorage.setItem("payType", "现金");
    sessionStorage.removeItem("retailId");
    sessionStorage.removeItem("numberEnd");
    setInterval("showTime()", 1000);
    $("#sale").hide();
    $(".noneDiscount").hide();
    $("#btnSettle").hide();
    $("#preRecord").hide();
    $("#btnClose").hide();
})
//获取当前时间
function showTime() {
    var time = new Date();
    var m = time.getMonth() + 1;
    var t = time.getFullYear() + "-" + pad(m, 2) + "-" + pad(time.getDate(), 2) + " " + pad(time.getHours(), 2) + ":" + pad(time.getMinutes(), 2) + ":" + pad(time.getSeconds(), 2);
    $("#time").text(t);
}
function pad(num, n) {
    return (Array(n).join(0) + num).slice(-n);
}
//输入isbn后回车
$("#search").keypress(function (e) {
    if (e.keyCode == 13) {
        //回车事件触发
        if (e.keyCode == 13) {
            sessionStorage.setItem("ISBN", $("#search").val())
            var kind = $("#kind").text().trim();
            var isbn = $("#search").val();
            if (isbn == "" || isbn == null) {
                swal({
                    title: "ISBN不能为空",
                    text: "ISBN不能为空，请您重新输入",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            } else {
                if (sessionStorage.getItem("retailId") == null || sessionStorage.getItem("retailId") == undefined) {
                    $.ajax({
                        type: 'Post',
                        url: 'customerRetail.aspx',
                        data: {
                            isbn: isbn,
                            op: 'newRetail'
                        },
                        dataType: 'text',
                        success: function (succ) {
                            var datas = succ.split("|:");
                            var data = datas[0];
                            $("#search").val("");
                            if (data == "添加失败") {
                                swal({
                                    title: data,
                                    text: data,
                                    buttonsStyling: false,
                                    confirmButtonClass: "btn btn-warning",
                                    type: "warning"
                                }).catch(swal.noop);
                            } else if (data == "ISBN不存在") {
                                swal({
                                    title: "ISBN:" + isbn,
                                    text: "不存在",
                                    buttonsStyling: false,
                                    confirmButtonClass: "btn btn-warning",
                                    type: "warning"
                                }).catch(swal.noop);
                            } else if (data == "已添加过此图书") {
                                swal({
                                    title: "已添加过ISBN：" + sessionStorage.getItem("ISBN") + "的图书",
                                    text: "需要继续添加可前往修改数量",
                                    buttonsStyling: false,
                                    confirmButtonClass: "btn btn-warning",
                                    type: "warning"
                                }).catch(swal.noop);
                            } else if (data == "一号多书") {
                                $.ajax({
                                    type: 'Post',
                                    url: 'customerRetail.aspx',
                                    data: {
                                        isbn: isbn,
                                        op: 'newChoose'
                                    },
                                    dataType: 'text',
                                    success: function (succ) {
                                        var datas = succ.split("|:");
                                        var data = datas[1];
                                        if (datas[0] == "无库存") {
                                            swal({
                                                title: "书籍库存不足",
                                                buttonsStyling: false,
                                                confirmButtonClass: "btn btn-warning",
                                                type: "warning"
                                            }).catch(swal.noop);
                                        } else if (datas[0] == "other") {
                                            sessionStorage.setItem("retailId", datas[3]);
                                            if (sessionStorage.getItem("kind") == "0") {
                                                $(".first").remove();
                                            }
                                            $("#table").prepend(data);
                                            //计算合计内容
                                            var kinds = parseInt(sessionStorage.getItem("kind")) + 1;
                                            var math = datas[2].split(",");
                                            //展示合计内容
                                            $("#kind").text(math[0]);
                                            $("#number").text(math[1]);
                                            $("#total").text(parseFloat(math[2]).toFixed(2));
                                            $("#real").text(parseFloat(math[3]).toFixed(2));
                                            //清空输入框并获取焦点
                                            $("#search").val("");
                                            $("#search").focus();
                                        }
                                        else {
                                            $("#myModal").modal("show");
                                            $("#table2 tr:not(:first)").empty(); //清空table处首行
                                            $("#table2").append(succ); //加载table
                                        }
                                    }
                                })
                            } else {
                                var math = datas[1].split(",");
                                if (sessionStorage.getItem("kind") == "0" || sessionStorage.getItem("kind") == 0) {
                                    $(".first").remove();
                                    sessionStorage.setItem("kind", 1);
                                }
                                sessionStorage.setItem("retailId", datas[2]);
                                $("#table tr:not(:first)").empty();//清空除第一行以外的信息
                                $("#table").prepend(data);
                                $("#kind").text(math[0]);
                                $("#number").text(math[1]);
                                $("#total").text(math[2]);
                                $("#real").text(math[3]);
                                //获取焦点
                                $("#search").focus();
                            }
                        }
                    })
                } else {
                    $.ajax({
                        type: 'Post',
                        url: 'customerRetail.aspx',
                        data: {
                            isbn: isbn,
                            headId: sessionStorage.getItem("retailId"),
                            op: 'isbn'
                        },
                        dataType: 'text',
                        success: function (succ) {
                            var datas = succ.split("|:");
                            var data = datas[0];
                            $("#search").val("");
                            if (data == "添加失败") {
                                swal({
                                    title: data,
                                    text: data,
                                    buttonsStyling: false,
                                    confirmButtonClass: "btn btn-warning",
                                    type: "warning"
                                }).catch(swal.noop);
                            } else if (data == "ISBN不存在") {
                                swal({
                                    title: "ISBN:" + isbn,
                                    text: "不存在",
                                    buttonsStyling: false,
                                    confirmButtonClass: "btn btn-warning",
                                    type: "warning"
                                }).catch(swal.noop);
                            } else if (data == "已添加过此图书") {
                                swal({
                                    title: "已添加过ISBN：" + sessionStorage.getItem("ISBN") + "的图书",
                                    text: "需要继续添加可前往修改数量",
                                    buttonsStyling: false,
                                    confirmButtonClass: "btn btn-warning",
                                    type: "warning"
                                }).catch(swal.noop);
                            } else if (data == "一号多书") {
                                $.ajax({
                                    type: 'Post',
                                    url: 'customerRetail.aspx',
                                    data: {
                                        isbn: isbn,
                                        headId: sessionStorage.getItem("retailId"),
                                        op: 'choose'
                                    },
                                    dataType: 'text',
                                    success: function (succ) {
                                        var datas = succ.split("|:");
                                        var data = datas[1];
                                        if (datas[0] == "无库存") {
                                            swal({
                                                title: "书籍库存不足",
                                                buttonsStyling: false,
                                                confirmButtonClass: "btn btn-warning",
                                                type: "warning"
                                            }).catch(swal.noop);
                                        } else if (datas[0] == "已添加过此图书") {
                                            swal({
                                                title: "已添加过此图书",
                                                buttonsStyling: false,
                                                confirmButtonClass: "btn btn-warning",
                                                type: "warning"
                                            }).catch(swal.noop);
                                        } else if (datas[0] == "other") {
                                            if (sessionStorage.getItem("kind") == "0") {
                                                $(".first").remove();
                                            }
                                            $("#table tr:not(:first)").empty();//清空除第一行以外的信息
                                            $("#table").prepend(data);
                                            //计算合计内容
                                            var kinds = parseInt(sessionStorage.getItem("kind")) + 1;
                                            var math = datas[2].split(",");
                                            //展示合计内容
                                            $("#kind").text(math[0]);
                                            $("#number").text(math[1]);
                                            $("#total").text(parseFloat(math[2]).toFixed(2));
                                            $("#real").text(parseFloat(math[3]).toFixed(2));
                                            //清空输入框并获取焦点
                                            $("#search").val("");
                                            $("#search").focus();
                                        }
                                        else {
                                            $("#myModal").modal("show");
                                            $("#table2 tr:not(:first)").empty(); //清空table处首行
                                            $("#table2").append(succ); //加载table
                                        }
                                    }
                                })
                            } else {
                                var math = datas[1].split(",");
                                if (sessionStorage.getItem("kind") == "0" || sessionStorage.getItem("kind") == 0) {
                                    $(".first").remove();
                                    sessionStorage.setItem("kind", 1)
                                }
                                $("#table tr:not(:first)").empty();//清空除第一行以外的信息
                                $("#table").prepend(data);
                                $("#kind").text(math[0]);
                                $("#number").text(math[1]);
                                $("#total").text(math[2]);
                                $("#real").text(math[3]);
                                //获取焦点
                                $("#search").focus();
                            }
                        }
                    });
                }
            }
        }
    }
});
//选择一号多书中需要的图书
$("#btnAdd").click(function () {
    var bookNum = $("input[type='radio']:checked").val();
    if (sessionStorage.getItem("retailId") == "" || sessionStorage.getItem("retailId") == null) {
        $.ajax({
            type: 'Post',
            url: 'customerRetail.aspx',
            data: {
                bookNum: bookNum,
                op: 'newAdd'
            },
            dataType: 'text',
            success: function (succ) {
                var datas = succ.split("|:");
                var data = datas[0];
                $("#search").val("");
                if (data == "已添加过此图书") {
                    swal({
                        title: "已添加过ISBN：" + sessionStorage.getItem("ISBN") + "的图书",
                        text: "需要继续添加可前往修改数量",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                }
                else if (data == "other") {
                    var math = datas[2].split(",");
                    sessionStorage.setItem("retailId", datas[3])
                    if (sessionStorage.getItem("kind") == "0" || sessionStorage.getItem("kind") == 0) {
                        $(".first").remove();
                        sessionStorage.setItem("kind", 1)
                    }
                    $("#table tr:not(:first)").empty();//清空除第一行以外的信息
                    $("#table").prepend(datas[1]);
                    $("#kind").text(math[0]);
                    $("#number").text(math[1]);
                    $("#total").text(math[2]);
                    $("#real").text(math[3]);
                    //获取焦点
                    $("#search").focus();
                    $("#myModal").modal("hide")
                }
                else {
                    var math = datas[1].split(",");
                    sessionStorage.setItem("retailId", datas[2]);
                    if (sessionStorage.getItem("kind") == "0" || sessionStorage.getItem("kind") == 0) {
                        $(".first").remove();
                        sessionStorage.setItem("kind", 1);
                    }
                    $("#table tr:not(:first)").empty();//清空除第一行以外的信息
                    $("#table").prepend(data);
                    $("#kind").text(math[0]);
                    $("#number").text(math[1]);
                    $("#total").text(math[2]);
                    $("#real").text(math[3]);
                    //获取焦点
                    $("#search").focus();
                    $("#myModal").modal("hide");
                }
            }
        })
    } else {
        $.ajax({
            type: 'Post',
            url: 'customerRetail.aspx',
            data: {
                bookNum: bookNum,
                headId: sessionStorage.getItem("retailId"),
                op: 'add'
            },
            dataType: 'text',
            success: function (succ) {
                var datas = succ.split("|:");
                var data = datas[0];
                $("#search").val("");
                if (data == "已添加过此图书") {
                    swal({
                        title: "已添加过ISBN：" + sessionStorage.getItem("ISBN") + "的图书",
                        text: "需要继续添加可前往修改数量",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                }
                else if (data == "other") {
                    var math = datas[2].split(",");
                    if (sessionStorage.getItem("kind") == "0" || sessionStorage.getItem("kind") == 0) {
                        $(".first").remove();
                        sessionStorage.setItem("kind", 1)
                    }
                    $("#table tr:not(:first)").empty();//清空除第一行以外的信息
                    $("#table").prepend(datas[1]);
                    $("#kind").text(math[0]);
                    $("#number").text(math[1]);
                    $("#total").text(math[2]);
                    $("#real").text(math[3]);
                    //获取焦点
                    $("#search").focus();
                    $("#myModal").modal("hide")
                }
                else {
                    var math = datas[1].split(",");
                    if (sessionStorage.getItem("kind") == "0" || sessionStorage.getItem("kind") == 0) {
                        $(".first").remove();
                        sessionStorage.setItem("kind", 1)
                    }
                    $("#table tr:not(:first)").empty();//清空除第一行以外的信息
                    $("#table").prepend(data);
                    $("#kind").text(math[0]);
                    $("#number").text(math[1]);
                    $("#total").text(math[2]);
                    $("#real").text(math[3]);
                    //获取焦点
                    $("#search").focus();
                    $("#myModal").modal("hide")
                }
            }
        })
    }
})

//扫描单头id，显示单据明细
$("#scannSea").keypress(function (e) {
    if (e.keyCode == 13) {
        var retailId = $("#scannSea").val();
        sessionStorage.setItem("retailId", retailId);
        $.ajax({
            type: 'Post',
            url: 'customerRetail.aspx',
            data: {
                retailId: retailId,
                op: 'scann'
            },
            dataType: 'text',
            success: function (data) {
                if (data == "记录不存在") {
                    swal({
                        title: "记录不存在",
                        text: "单据编号：" + retailId + "记录不存在",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                } else if (data == "此单据已结算") {
                    swal({
                        title: "此单据已结算",
                        text: "单据编号：" + retailId + "已结算",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                } else if (data == "此单据为退货单据") {
                    swal({
                        title: "此单据为退货单据",
                        text: "单据编号：" + retailId + "为退货单据",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                } else {
                    var datas = data.split("|:");
                    var math = datas[0].split(",");
                    var total = math[0].split(":");
                    $("#total").text(total[1]);
                    $("#real").text(math[1]);
                    $("#number").text(math[2]);
                    $("#kind").text(math[3]);
                    $("#table tr:not(:first)").empty();//清空除第一行以外的信息
                    $("#table").prepend(datas[1]);
                    $("#scannSea").val("");
                    $("#myModal1").modal("hide");
                }
            }
        })
    }
})

//加的效果
$("#table").delegate(".add", "click", function () {
    var id = $(this).parent().parent().prev().prev().prev().prev().prev().text().trim();
    var n = $(this).prev().val();
    var num = parseInt(n) + 1;
    if (num == 0) { return; }
    var price = parseFloat($(this).parent().parent().prev().prev().text().trim());
    var discount = parseFloat($(this).parent().parent().next().text().trim());
    if (discount > 1) {
        discount = discount * 0.01;
    }
    //修改表格内容
    $(this).prev().val(num);//数量
    $(this).parent().parent().next().next().text(parseFloat(price * num).toFixed(2))//码洋
    $(this).parent().parent().next().next().next().text(parseFloat(price * num * discount).toFixed(2))//实洋
    $.ajax({
        type: 'Post',
        url: 'customerRetail.aspx',
        data: {
            retailId: id,
            headId: sessionStorage.getItem("retailId"),
            number: num,
            type: 'jia',
            op: 'change'
        },
        dataType: 'text',
        success: function (data) {
            var datas = data.split("|:");
            var succ = datas[0];
            if (succ == "更新成功") {
                var math = datas[1].split(",");
                $("#number").text(math[0]);
                $("#total").text(math[1]);
                $("#real").text(math[2]);
                total.text(datas[2]);
                real.text(datas[3]);
            } else if (succ == "更新失败") {
                swal({
                    title: "修改数量失败",
                    text: "修改数量失败",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
        }
    })
});
//减的效果
$("#table").delegate(".jian", "click", function () {
    var id = $(this).parent().parent().prev().prev().prev().prev().prev().text().trim();
    var n = $(this).next().val();
    var num = parseInt(n) - 1;
    if (num <= 0) {
        swal({
            title: "数量至少为1",
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning btnStyle",
            type: "warning"
        }).catch(swal.noop);
    }
    else {
        var price = parseFloat($(this).parent().parent().prev().prev().text().trim());
        var discount = parseFloat($(this).parent().parent().next().text().trim());
        if (discount > 10) {
            discount = discount * 0.01;
        } else if (discount < 10 && discount > 1) {
            discount = discount * 0.1;
        }
        //修改表格内容
        $(this).next().val(num);//数量
        $(this).parent().parent().next().next().text(parseFloat(price * num).toFixed(2))//码洋
        $(this).parent().parent().next().next().next().text(parseFloat(price * num * discount).toFixed(2))//实洋
        $.ajax({
            type: 'Post',
            url: 'customerRetail.aspx',
            data: {
                retailId: id,
                headId: sessionStorage.getItem("retailId"),
                number: num,
                type: 'jian',
                op: 'change'
            },
            dataType: 'text',
            success: function (data) {
                var datas = data.split("|:");
                var succ = datas[0];
                if (succ == "更新成功") {
                    var math = datas[1].split(",");
                    $("#number").text(math[0]);
                    $("#total").text(math[1]);
                    $("#real").text(math[2]);
                    total.text(datas[2]);
                    real.text(datas[3]);
                } else if (succ == "更新失败") {
                    swal({
                        title: "修改数量失败",
                        text: "修改数量失败",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                }
            }
        })
    }
});
//收银删除
$("table").delegate(".delete", "click", function () {
    var id = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().text().trim();
    var tr = $(this).parent().parent();
    swal({
        title: "删除记录",
        text: "您确定要删除吗？",
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
            url: 'customerRetail.aspx',
            data: {
                retailId: id,
                headId: sessionStorage.getItem("retailId"),
                op: 'del'
            },
            dataType: 'text',
            success: function (data) {
                var succ = data.split("|:");
                if (succ[0] == "删除成功") {
                    tr.remove();
                    var kind = parseInt($("#kind").text());
                    $("#kind").text(kind - 1);
                    $("#number").text(succ[1]);
                    $("#total").text(succ[2]);
                    $("#real").text(succ[3]);
                }
                else if (succ[0] == "删除失败") {
                    swal({
                        title: "删除失败",
                        text: "删除失败",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                }
            }
        })
    })
})

//收银结算
$("#Settlement").click(function () {
    if (sessionStorage.getItem("retailId") == null || sessionStorage.getItem("retailId") == undefined) {
        swal({
            title: "请先扫描小票",
            text: "请先扫描小票",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
    } else {
        $("#totalEnd").text($("#total").text().trim());
        $("#realEnd").text($("#real").text().trim());
        sessionStorage.setItem("total", $("#total").text().trim());
        sessionStorage.setItem("real", $("#real").text().trim());
        sessionStorage.setItem("numberEnd", $("#number").text().trim());
        $.ajax({
            type: 'Post',
            url: 'customerRetail.aspx',
            data: {
                headId: sessionStorage.getItem("retailId"),
                op: 'stock'
            },
            dataType: 'text',
            success: function (succ) {
                var datas = succ.split(":|");
                var data = datas[0];
                if (data == "此书籍库存不足") {
                    var succs = datas[1].split("|,");
                    swal({
                        title: "书名:" + succs[0] + ",库存不足",
                        text: "最大库存数量:" + succs[1],
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                } else if (data == "此书籍无库存") {
                    swal({
                        title: "书籍不存在",
                        text: "书名:" + datas[1] + "不存在",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                } else if (data == "此单据不存在") {
                    swal({
                        title: "此单据不存在",
                        text: "",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                } else {
                    $("#myModal2").modal("show");
                }
            }
        });
    }
})
//收银折扣
$("#discountEnd").keypress(function (e) {
    if (e.keyCode == 13) {
        var discount = $("#discountEnd").val().trim();
        var retailId = sessionStorage.getItem("retailId");
        if (parseFloat(discount) > 100) {
            swal({
                title: "提示",
                text: "折扣不能为大于100",
                type: "warning",
                showCancelButton: false,
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-warning',
                buttonsStyling: false,
                allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
            }).then(function () {
                $("#discountEnd").val("100")
                sessionStorage.setItem("discount", 100);
            })
        }
        else if (parseFloat(discount) < 0) {
            swal({
                title: "提示",
                text: "折扣不能小于0",
                type: "warning",
                showCancelButton: false,
                confirmButtonColor: '#3085d6',
                confirmButtonText: '确定',
                confirmButtonClass: 'btn btn-warning',
                buttonsStyling: false,
                allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
            }).then(function () {
                $("#discountEnd").val("100")
                sessionStorage.setItem("discount", 100);
            })
        }
        else if (discount == "") {
            $("#copeEnd").focus();
            $(".discount").hide();
            $(".noneDiscount").show();
            sessionStorage.setItem("content", "none");
        }
        else {
            sessionStorage.setItem("discount", discount);
            $.ajax({
                type: 'Post',
                url: 'customerRetail.aspx',
                data: {
                    discount: discount,
                    retailId: retailId,
                    op: 'discount'
                },
                dataType: 'text',
                success: function (data) {
                    if (data == "更新成功") {
                        var paytype = $("#payType").find("input[name='paytype']:checked").val();
                        var total = $("#totalEnd").text().trim();
                        var real = total * parseFloat(discount) * 0.01;
                        $("#realEnd").text((real).toFixed(2));
                        if (paytype == "第三方") {
                            $("#copeEnd").val((real).toFixed(2));
                        }
                        $("#copeEnd").focus();
                    } else if (data == "更新失败") {
                        swal({
                            title: "错误提示",
                            text: "修改失败",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).catch(swal.noop);
                        $("#discountEnd").val("");
                    }
                }
            })
        }
    }
})
//选择第三方付款
$("#threePay").click(function () {
    var discount = $("#discountEnd").val();
    if (discount == "0" || discount == 0) {
        discount = $("#discountEnd").val("");
        $("#discountEnd").focus();
    }
    $("#change").text("");
    $("#copeEnd").val($("#realEnd").text().trim());
    sessionStorage.setItem("realPrice", $("#realEnd").text().trim());
    $("#copeEnd").focus();
    $("#paytype").html("第三方");
    sessionStorage.setItem("payType", "第三方");
})
//选择现金付款
$("#moneyPay").click(function () {
    $("#copeEnd").val($("#realEnd").text().trim());
    $("#copeEnd").focus();
    $("#paytype").html("现金");
    sessionStorage.setItem("payType", "现金");
})
//收银输入实收计算找零
$("#copeEnd").keypress(function (e) {
    if (e.keyCode == 13) {
        sessionStorage.setItem("give", $("#copeEnd").val());
        var give = parseFloat($("#copeEnd").val());
        var cope = parseFloat($("#realEnd").text().trim());
        sessionStorage.setItem("realPrice", cope);
        if (give < cope) {
            swal({
                title: "实付金额不能小于实洋金额",
                text: "",
                buttonsStyling: false,
                confirmButtonClass: "btn btn-warning",
                type: "warning"
            }).catch(swal.noop);
        } else {
            var real = give - cope;
            sessionStorage.setItem("dibs", real);
            $("#change").text((real).toFixed(2));
            $("#btnSettle").show();
            $("#btnSettle").focus();
        }
    }
})
//首次打印
$("#btnSettle").click(function () {
    $("#btnSettle").attr('disabled', true);
    var discount = $("#discountEnd").val().trim();
    if (parseFloat(discount > 100)) {
        swal({
            title: "折扣不能为大于100",
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
        $("#btnSettle").attr('disabled', false);
    }
    else if (parseFloat(discount) < 0) {
        swal({
            title: "折扣不能小于0",
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
        $("#btnSettle").attr('disabled', false);
    }
    else if ($("#copeEnd").val().trim() == "" || $("#copeEnd").val().trim() == 0 || $("#copeEnd").val().trim() == "0") {
        swal({
            title: "实付金额不能为空或0",
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning",
            type: "warning"
        }).catch(swal.noop);
        $("#btnSettle").attr('disabled', false);
    }
    else {
        if (discount == "") {
            $(".discount").remove();
            $(".noneDiscount").show();
            sessionStorage.setItem("content", "none");
        } else {
            $(".noneDiscount").remove();
            sessionStorage.setItem("content", "show");
        }
        $.ajax({
            type: 'Post',
            url: 'customerRetail.aspx',
            data: {
                headId: sessionStorage.getItem("retailId"),
                payType: sessionStorage.getItem("payType"),
                op: 'end'
            },
            dataType: 'text',
            success: function (succ) {
                $("#btnSettle").attr('disabled', false);
                var datas = succ.split(":|");
                var data = datas[0];
                if (data == "此书籍库存不足") {
                    swal({
                        title: "此书籍库存不足",
                        text: "书名:" + datas[1] + "库存不足",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                } else if (data == "此书籍无库存") {
                    swal({
                        title: "书籍不存在",
                        text: "书名:" + datas[1] + "不存在",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                } else if (data == "此单据不存在") {
                    swal({
                        title: "此单据不存在",
                        text: "",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                } else if (data == "更新失败") {
                    swal({
                        title: "网络故障，结算失败",
                        text: "",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                } else if (data == "更新成功") {
                    var discount = parseFloat(sessionStorage.getItem("discount"));
                    $("#id").text(sessionStorage.getItem("retailId"));
                    if (sessionStorage.getItem("content") == "show") {
                        $("#allnumber").text(sessionStorage.getItem("numberEnd"));
                        $("#alltotal").text(sessionStorage.getItem("total"));
                        $("#alldiscount").text(discount + "%");
                        $("#allreal").text(sessionStorage.getItem("realPrice"));
                    } else {
                        $("#noneNumber").text(sessionStorage.getItem("numberEnd"));
                        $("#noneTotal").text("￥ " + sessionStorage.getItem("total"));
                    }
                    $("#allcope").text(parseFloat(sessionStorage.getItem("give")).toFixed(2));
                    $("#cope").text(parseFloat((sessionStorage.getItem("give")) - parseFloat(sessionStorage.getItem("dibs"))).toFixed(2));
                    $("#allchange").text(parseFloat(sessionStorage.getItem("dibs")).toFixed(2));
                    $("#tablePay tr:not(:first)").empty()
                    $("#tablePay").append(datas[1]);
                    $("#sale").show();
                    //$("#myModal2").modal("hide");
                    //一维码
                    //JsBarcode("#img", sessionStorage.getItem("retailId"), {
                    //    displayValue: false, //是否在条形码下方显示文字
                    //    //fontSize: 15,//设置文本的大小
                    //    margin: 0,//设置条形码周围的空白边距
                    //    width: 10,//设置条之间的宽度
                    //    height: 50,//高度
                    //});
                    var headId = sessionStorage.getItem("retailId");
                    //二维码
                    jQuery('#output').qrcode({
                        render: "canvas",
                        foreground: "black",
                        background: "white",
                        text: headId
                    });
                    $("canvas").attr("id", "qrcode");
                    var canvas = document.getElementById('qrcode');
                    var context = canvas.getContext('2d');
                    var image = new Image();
                    var strDataURI = canvas.toDataURL("image/png");
                    document.getElementById('img').src = strDataURI;
                    try {
                        var status = "";
                        var LODOP = getLodop();
                        //LODOP.SET_LICENSES("", "3C5743518A25D4EEFBB1CCB8C6FF9A49", "C94CEE276DB2187AE6B65D56B3FC2848", "");
                        LODOP.ADD_PRINT_HTM(0, 25, 900, 500, document.getElementById("sale").innerHTML);
                        LODOP.SET_PRINTER_INDEX("BTP-U60(U) 1");
                        LODOP.SET_PRINT_PAGESIZE(3, 700, 100, "");
                        LODOP.PRINT();
                        LODOP.SET_PRINT_MODE("CATCH_PRINT_STATUS", true);
                        LODOP.On_Return = function (TaskID, Value) {
                            status = Value;
                        };
                        if (status != "" || status != null) {
                            LODOP.ADD_PRINT_HTM(0, 25, 900, 500, document.getElementById("sale").innerHTML);
                            LODOP.SET_PRINTER_INDEX("BTP-U60(U) 1");
                            LODOP.SET_PRINT_PAGESIZE(3, 700, 100, "");
                            LODOP.PRINT();
                        }
                        $(this).hide();
                        $("#settleClose").hide();
                        $("#preRecord").show();
                        $("#btnClose").show();
                    } catch (err) {
                        window.location.href = "/CLodop_Setup_for_Win32NT.html";
                    }
                }
            }, error: function (XMLHttpRequest, textStatus, errorThrown) {
                $("#btnSettle").attr('disabled', false);
                swal({
                    title: "结算失败",
                    text: "",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
        })
    }
})
//右上角按钮关闭结算模态框
$("#settleClose").click(function () {
    $("#totalEnd").text("");
    $("#discountEnd").val("");
    $("#realEnd").text("");
    $("#copeEnd").val("");
    $("#change").text("");
})
//补打上一条记录
$("#preRecord").click(function () {
    var status = "";
    var LODOP = getLodop();
    LODOP.ADD_PRINT_HTM(0, 25, 900, 500, document.getElementById("sale").innerHTML);
    LODOP.SET_PRINTER_INDEX("BTP-U60(U) 1");
    LODOP.SET_PRINT_PAGESIZE(3, 700, 100, "");
    LODOP.PRINT();
    LODOP.SET_PRINT_MODE("CATCH_PRINT_STATUS", true);
    LODOP.On_Return = function (TaskID, Value) {
        status = Value;
    };
    if (status != "" || status != null) {
        LODOP.ADD_PRINT_HTM(0, 25, 900, 500, document.getElementById("sale").innerHTML);
        LODOP.SET_PRINTER_INDEX("BTP-U60(U) 1");
        LODOP.SET_PRINT_PAGESIZE(3, 700, 100, "");
        LODOP.PRINT();
    }
})
//收银完成关闭模态框
$("#btnClose").click(function () {
    sessionStorage.removeItem("retailId");
    $("#myModal2").modal("hide");
    window.location.reload();
})
//弹出模态框获取焦点事件
$('#myModal1').on('shown.bs.modal', function (e) {
    $('#scannSea').focus();
});
$('#myModal2').on('shown.bs.modal', function (e) {
    $('#discountEnd').focus();
});
