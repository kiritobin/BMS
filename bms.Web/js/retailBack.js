$(document).ready(function () {
    $("#search").focus();
    sessionStorage.setItem("kind", 0);
    sessionStorage.setItem("number", 0);
    sessionStorage.setItem("totalPrice", 0);
    sessionStorage.setItem("realPrice", 0);
    setInterval("showTime()", 1000);
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
//点击扫描按钮
$("#btnSearch").click(function () {
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
        $.ajax({
            type: 'Post',
            url: 'retailBack.aspx',
            data: {
                kind: sessionStorage.getItem("kind"),
                isbn: isbn,
                op: 'isbn'
            },
            dataType: 'text',
            success: function (data) {
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
                        url: 'retailBack.aspx',
                        data: {
                            isbn: isbn,
                            op: 'choose'
                        },
                        dataType: 'text',
                        success: function (succ) {
                            $("#myModal").modal("show");
                            $("#table2 tr:not(:first)").empty(); //清空table处首行
                            $("#table2").append(succ); //加载table
                        }
                    })
                } else {
                    $("#table").append(data);
                    //计算合计内容
                    var kinds = parseInt(sessionStorage.getItem("kind")) + 1;
                    var numbers = parseInt(sessionStorage.getItem("number")) + parseInt($("#table tr:last").find("td:eq(4)").children().val());
                    var totalPrices = parseFloat(sessionStorage.getItem("totalPrice")) + parseFloat($("#table tr:last").find("td:eq(6)").text().trim());
                    var realPrices = parseFloat(sessionStorage.getItem("realPrice")) + parseFloat($("#table tr:last").find("td:eq(7)").text().trim());
                    sessionStorage.setItem("kind", kinds);
                    sessionStorage.setItem("number", numbers);
                    sessionStorage.setItem("totalPrice", totalPrices);
                    sessionStorage.setItem("realPrice", realPrices);
                    //展示合计内容
                    $("#number").text(numbers);
                    $("#total").text(totalPrices);
                    $("#real").text(realPrices);
                    $("#kind").text(kinds);
                    //清空输入框并获取焦点
                    $("#search").val("");
                    $("#search").focus();
                }
            }
        })
    }
})
//输入isbn后回车
$("#search").keypress(function (e) {
    sessionStorage.setItem("ISBN", $("#search").val())
    //回车事件触发
    if (e.keyCode == 13) {
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
            $.ajax({
                type: 'Post',
                url: 'retailBack.aspx',
                data: {
                    kind: sessionStorage.getItem("kind"),
                    isbn: isbn,
                    op: 'isbn'
                },
                dataType: 'text',
                success: function (data) {
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
                        $("#search").val("");
                    } else if (data == "一号多书") {
                        $.ajax({
                            type: 'Post',
                            url: 'retailBack.aspx',
                            data: {
                                isbn: isbn,
                                op: 'choose'
                            },
                            dataType: 'text',
                            success: function (succ) {
                                $("#myModal").modal("show");
                                $("#table2 tr:not(:first)").empty(); //清空table处首行
                                $("#table2").append(succ); //加载table
                            }
                        })
                    } else {
                        if (sessionStorage.getItem("kind") == "0") {
                            //$("#table tr:eq(1)").empty();
                            $(".first").remove();
                        }
                        $("#table").append(data);
                        //计算合计内容
                        var kinds = parseInt(sessionStorage.getItem("kind")) + 1;
                        var numbers = parseInt(sessionStorage.getItem("number")) + parseInt($("#table tr:last").find("td:eq(4)").children().val());
                        var totalPrices = parseFloat(sessionStorage.getItem("totalPrice")) + parseFloat($("#table tr:last").find("td:eq(6)").text().trim());
                        var realPrices = parseFloat(sessionStorage.getItem("realPrice")) + parseFloat($("#table tr:last").find("td:eq(7)").text().trim());
                        sessionStorage.setItem("kind", kinds);
                        sessionStorage.setItem("number", numbers);
                        sessionStorage.setItem("totalPrice", totalPrices);
                        sessionStorage.setItem("realPrice", realPrices);
                        //展示合计内容
                        $("#number").text(numbers);
                        $("#total").text(totalPrices);
                        $("#real").text(realPrices);
                        $("#kind").text(kinds);
                        //清空输入框并获取焦点
                        $("#search").val("");
                        $("#search").focus();
                    }
                }
            })
        }
    }
})
//选择一号多书中需要的图书
$("#btnAdd").click(function () {
    var bookNum = $("input[type='radio']:checked").val();
    $.ajax({
        type: 'Post',
        url: 'retailBack.aspx',
        data: {
            bookNum: bookNum,
            op: 'add'
        },
        dataType: 'text',
        success: function (data) {
            if (data == "已添加过此图书") {
                swal({
                    title: "已添加过ISBN：" + sessionStorage.getItem("ISBN") + "的图书",
                    text: "需要继续添加可前往修改数量",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
                $("#search").val("");
            }
            $("#myModal").modal("hide")
            if (sessionStorage.getItem("kind") == "0") {
                //$("#table tr:eq(1)").empty();
                $(".first").remove();
            }
            $("#table").append(data);
            var kinds = parseInt(sessionStorage.getItem("kind")) + 1;
            var numbers = parseInt(sessionStorage.getItem("number")) + 1;
            var totalPrices = parseFloat(sessionStorage.getItem("totalPrice")) + parseFloat($("#table tr:last").find("td:eq(6)").text().trim());
            var realPrices = parseFloat(sessionStorage.getItem("realPrice")) + parseFloat($("#table tr:last").find("td:eq(7)").text().trim());
            sessionStorage.setItem("kind", kinds);
            sessionStorage.setItem("number", numbers);
            sessionStorage.setItem("totalPrice", totalPrices);
            sessionStorage.setItem("realPrice", realPrices);
            //展示合计内容
            $("#number").text(numbers);
            $("#total").text(totalPrices);
            $("#real").text(realPrices);
            $("#kind").text(kinds);
            //清空输入框并获取焦点
            $("#search").val("");
            $("#search").focus();
        }
    })
})
//修改数量
$("#table").delegate(".number", "change", function (e) {
    var number = parseInt($(this).val());
    var total = parseFloat($(this).parent().next().next().text().trim());
    var real = parseFloat($(this).parent().next().next().next().text().trim());
    var price = parseFloat($(this).parent().prev().prev().text().trim());
    var discount = parseFloat($(this).parent().next().text().trim());
    if (discount > 10) {
        discount = discount * 0.01;
    } else if (discount < 10 && discount > 1) {
        discount = discount * 0.1;
    }
    var totalPrice = parseFloat(number * price);
    var realPrice = parseFloat(totalPrice * discount);
    //计算合计内容
    var count = parseInt($(this).parent().prev().text().trim());
    var counts = parseInt($(this).val());
    if (counts > count) {
        sessionStorage.setItem("number", parseInt(sessionStorage.getItem("number")) + 1);
    } else if (counts < count) {
        sessionStorage.setItem("number", parseInt(sessionStorage.getItem("number")) - 1);
    }
    $(this).parent().prev().text(counts);
    var totalPrices = parseFloat(sessionStorage.getItem("totalPrice")) - total + totalPrice;
    var realPrices = parseFloat(sessionStorage.getItem("realPrice")) - real + realPrice;
    sessionStorage.setItem("totalPrice", parseFloat(totalPrices));
    sessionStorage.setItem("realPrice", parseFloat(realPrices));
    $(this).parent().next().next().text(parseFloat(totalPrice));
    $(this).parent().next().next().next().text(parseFloat(realPrice));
    //展示合计内容
    $("#number").text(sessionStorage.getItem("number"));
    $("#total").text(parseFloat(totalPrices));
    $("#real").text(parseFloat(realPrices));
    $("#kind").text(parseInt(sessionStorage.getItem("kind")));
})
//保存提交
$("#insert").click(function () {
    var table = $("#table").tableToJSON();
    var json = JSON.stringify(table);
    $.ajax({
        type: 'Post',
        url: 'retailBack.aspx',
        data: {
            json: json,
            op: 'insert'
        },
        dataType: 'text',
        success: function (data) {
            if (data == "库存不足") {
                swal({
                    title: "无库存",
                    text: "无库存",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            } else if (data == "添加成功") {
                swal({
                    title: "退货成功:)",
                    text: "退货成功",
                    type: "success",
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: '确定',
                    confirmButtonClass: 'btn btn-success',
                    buttonsStyling: false,
                    allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
                }).then(function () {
                    window.location.reload();
                    $("#search").focus();
                    sessionStorage.removeItem("kind");
                    sessionStorage.removeItem("number");
                    sessionStorage.removeItem("totalPrice");
                    sessionStorage.removeItem("realPrice");
                })
            } else {
                swal({
                    title: "退货失败",
                    text: "退货失败",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            }
        }
    })
})
//删除
$("#table").delegate(".btn-delete", "click", function () {
    var bookNum = $(this).parent().prev().val().trim();
    var totalPrice = $(this).parent().prev().prev().prev().text().trim();
    var realPrice = $(this).parent().prev().prev().text().trim();
    var number = $(this).parent().prev().prev().prev().prev().prev().children().val().trim();
    $(this).parent().parent().remove();
    $.ajax({
        type: 'Post',
        url: 'retailBack.aspx',
        data: {
            bookNum: bookNum,
            totalPrice: totalPrice,
            realPrice: realPrice,
            number: number,
            op: 'delete'
        },
        dataType: 'text',
        success: function (data) {
            sessionStorage.setItem("kind", parseInt(sessionStorage.getItem("kind") - 1));
            sessionStorage.setItem("number", parseFloat(sessionStorage.getItem("number") - parseInt(number)));
            sessionStorage.setItem("totalPrice", parseFloat(sessionStorage.getItem("totalPrice") - parseFloat(totalPrice)));
            sessionStorage.setItem("realPrice", parseInt(sessionStorage.getItem("realPrice") - parseFloat(realPrice)));
            //展示合计内容
            $("#number").text(sessionStorage.getItem("number"));
            $("#total").text(parseFloat(totalPrices));
            $("#real").text(parseFloat(realPrices));
            $("#kind").text(parseInt(sessionStorage.getItem("kind")));
            $("#search").val("");
            $("#search").focus();
        }
    })
});
//删除此单
$("#giveup").click(function () {
    swal({
        title: "温馨提示:)",
        text: "您确定要放弃此单吗",
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
        window.location.reload();
    })
})