﻿$(document).ready(function () {
    $("#search").focus();
    sessionStorage.setItem("kind", 0);
    sessionStorage.setItem("number", 0);
    sessionStorage.setItem("totalPrice", 0);
    sessionStorage.setItem("realPrice", 0);
    setInterval("showTime()", 1000);
    //$("#ticket").hide();
    $("#preRecord").text(sessionStorage.getItem("preRecord"));
})
//点击tr勾选此书
$("#table2").delegate("tr", "click", function (e) {
    if (!$(this).is(":checked")) {
        //$("input[type='radio']").attr("checked", true);
        $(this).find("input[type='radio']").prop("checked", true);
    }
})
//获取当前时间
function showTime() {
    var time = new Date();
    var m = time.getMonth() + 1;
    var t = time.getFullYear() + "-" + pad(m, 2) + "-" + pad(time.getDate(), 2) + " " + pad(time.getHours(), 2) + ":" + pad(time.getMinutes(), 2) + ":" + pad(time.getSeconds(), 2);
    $("#time").text(t);
}
function endTime() {
    var time = new Date();
    var m = time.getMonth() + 1;
    var t = time.getFullYear() + "-" + pad(m, 2) + "-" + pad(time.getDate(), 2) + " " + pad(time.getHours(), 2) + ":" + pad(time.getMinutes(), 2) + ":" + pad(time.getSeconds(), 2);
    $("#timeEnd").text(t);
}
function pad(num, n) {
    return (Array(n).join(0) + num).slice(-n);
}
//输入isbn后回车
$("#search").keypress(function (e) {
    sessionStorage.setItem("ISBN", $("#search").val())
    //回车事件触发
    if (e.keyCode == 13) {
        $("#table2 tr:not(:first)").remove();
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
                url: 'retail.aspx?userId=',
                data: {
                    kind: sessionStorage.getItem("kind"),
                    isbn: isbn,
                    op: 'isbn'
                },
                dataType: 'text',
                success: function (data) {
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
                        }).then(function () {
                            $("#search").val("");
                            });
                    } else if (data == "已添加过此图书") {
                        swal({
                            title: "已添加过ISBN：" + sessionStorage.getItem("ISBN") + "的图书",
                            text: "需要继续添加可前往修改数量",
                            buttonsStyling: false,
                            confirmButtonClass: "btn btn-warning",
                            type: "warning"
                        }).then(function () {
                            $("#search").val("");
                            });
                    } else if (data == "一号多书") {
                        $.ajax({
                            type: 'Post',
                            url: 'retail.aspx',
                            data: {
                                isbn: isbn,
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
                                        title: "已添加过ISBN：" + sessionStorage.getItem("ISBN") + "的图书",
                                        text: "需要继续添加可前往修改数量",
                                        buttonsStyling: false,
                                        confirmButtonClass: "btn btn-warning",
                                        type: "warning"
                                    }).catch(swal.noop);
                                }else if (datas[0] == "other") {
                                    if (sessionStorage.getItem("kind") == "0") {
                                        //$("#table tr:eq(1)").empty();
                                        $(".first").remove();
                                    }
                                    $("#table").prepend(data);
                                    //计算合计内容
                                    var kinds = parseInt(sessionStorage.getItem("kind")) + 1;
                                    var numbers = parseInt(sessionStorage.getItem("number")) + 1;
                                    var totalPrices = parseFloat(sessionStorage.getItem("totalPrice")) + parseFloat($("#table tbody tr:first").find("td:eq(6)").text().trim());
                                    var realPrices = parseFloat(sessionStorage.getItem("realPrice")) + parseFloat($("#table tbody tr:first").find("td:eq(7)").text().trim());
                                    sessionStorage.setItem("kind", kinds);
                                    sessionStorage.setItem("number", numbers);
                                    sessionStorage.setItem("totalPrice", parseFloat(totalPrices).toFixed(2));
                                    sessionStorage.setItem("realPrice", parseFloat(realPrices).toFixed(2));
                                    //展示合计内容
                                    $("#number").text(numbers);
                                    $("#total").text(parseFloat(totalPrices).toFixed(2));
                                    //$("#real").text(realPrices);
                                    $("#real").text(parseFloat(realPrices).toFixed(2));
                                    $("#kind").text(kinds);
                                    //清空输入框并获取焦点
                                    $("#search").val("");
                                    $("#search").focus();
                                }
                                else {
                                    $("#myModal").modal("show");
                                    $("#table2 tr:not(:first)").remove(); //清空table处首行
                                    $("#table2").prepend(data); //加载table
                                }
                            }
                        })
                    } else {
                        if (sessionStorage.getItem("kind") == "0") {
                            //$("#table tr:eq(1)").empty();
                            $(".first").remove();
                        }
                        $("#table").prepend(data);
                        //计算合计内容
                        var kinds = parseInt(sessionStorage.getItem("kind")) + 1;
                        var numbers = parseInt(sessionStorage.getItem("number")) + 1;
                        var totalPrices = parseFloat(sessionStorage.getItem("totalPrice")) + parseFloat($("#table tbody tr:first").find("td:eq(6)").text().trim());
                        var realPrices = parseFloat(sessionStorage.getItem("realPrice")) + parseFloat($("#table tbody tr:first").find("td:eq(7)").text().trim());
                        sessionStorage.setItem("kind", kinds);
                        sessionStorage.setItem("number", numbers);
                        sessionStorage.setItem("totalPrice", parseFloat(totalPrices).toFixed(2));
                        sessionStorage.setItem("realPrice", parseFloat(realPrices).toFixed(2));
                        //展示合计内容
                        $("#number").text(numbers);
                        $("#total").text(parseFloat(totalPrices).toFixed(2));
                        //$("#real").text(realPrices);
                        $("#real").text(parseFloat(realPrices).toFixed(2));
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
        url: 'retail.aspx?userId=',
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
            $("#table").prepend(data);
            var kinds = parseInt(sessionStorage.getItem("kind")) + 1;
            var numbers = parseInt(sessionStorage.getItem("number")) + 1;
            var totalPrices = parseFloat(sessionStorage.getItem("totalPrice")) + parseFloat($("#table tbody tr:first").find("td:eq(6)").text().trim());
            var realPrices = parseFloat(sessionStorage.getItem("realPrice")) + parseFloat($("#table tbody tr:first").find("td:eq(7)").text().trim());
            sessionStorage.setItem("kind", kinds);
            sessionStorage.setItem("number", numbers);
            sessionStorage.setItem("totalPrice", parseFloat(totalPrices).toFixed(2));
            sessionStorage.setItem("realPrice", parseFloat(realPrices).toFixed(2));
            //展示合计内容
            $("#number").text(numbers);
            $("#total").text(parseFloat(totalPrices).toFixed(2));
            //$("#real").text(realPrices);
            $("#real").text(parseFloat(realPrices).toFixed(2));
            $("#kind").text(kinds);
            //清空输入框并获取焦点
            $("#search").val("");
            $("#search").focus();
        }
    })
})

//加的效果
$("#table").delegate(".add", "click", function () {
    var n = $(this).prev().val();
    var num = parseInt(n) + 1;
    if (num == 0) { return; }
    $(this).prev().val(num);
    var price = parseFloat($(this).parent().parent().prev().prev().text().trim());
    var discount = parseFloat($(this).parent().parent().next().text().trim());
    if (discount > 1) {
        discount = discount * 0.01;
    }
    //计算合计内容
    sessionStorage.setItem("number", parseInt(sessionStorage.getItem("number")) + 1);
    var totalPrices = parseFloat(sessionStorage.getItem("totalPrice")) + price;
    var realPrices = parseFloat(sessionStorage.getItem("realPrice")) + price * discount;
    sessionStorage.setItem("totalPrice", parseFloat(totalPrices).toFixed(2));
    sessionStorage.setItem("realPrice", parseFloat(realPrices).toFixed(2));
    //表格内展示
    var totalPrice = parseFloat(num * price).toFixed(2);
    var realPrice = parseFloat(totalPrice * discount).toFixed(2);
    $(this).parent().parent().next().next().text(parseFloat(totalPrice).toFixed(2));
    $(this).parent().parent().next().next().next().text(parseFloat(realPrice).toFixed(2));
    //展示合计内容
    $("#number").text(sessionStorage.getItem("number"));
    $("#total").text(parseFloat(totalPrices).toFixed(2));
    $("#real").text(parseFloat(realPrices).toFixed(2));
    $("#kind").text(parseInt(sessionStorage.getItem("kind")));
    $(this).parent().parent().prev().text($(this).prev().val());
});
//减的效果
$("#table").delegate(".jian", "click", function () {
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
        $(this).next().val(num);
        var price = parseFloat($(this).parent().parent().prev().prev().text().trim());
        var discount = parseFloat($(this).parent().parent().next().text().trim());
        if (discount > 10) {
            discount = discount * 0.01;
        } else if (discount < 10 && discount > 1) {
            discount = discount * 0.1;
        }
        //计算合计内容
        sessionStorage.setItem("number", parseInt(sessionStorage.getItem("number")) - 1);
        var totalPrices = parseFloat(sessionStorage.getItem("totalPrice")) - price;
        var realPrices = parseFloat(sessionStorage.getItem("realPrice")) - (price * discount);
        sessionStorage.setItem("totalPrice", parseFloat(totalPrices).toFixed(2));
        sessionStorage.setItem("realPrice", parseFloat(realPrices).toFixed(2));
        //表格内展示
        var totalPrice = parseFloat(num * price).toFixed(2);
        var realPrice = parseFloat(totalPrice * discount).toFixed(2);
        $(this).parent().parent().next().next().text(parseFloat(totalPrice).toFixed(2));
        $(this).parent().parent().next().next().next().text(parseFloat(realPrice).toFixed(2));
        //展示合计内容
        $("#number").text(sessionStorage.getItem("number"));
        $("#total").text(parseFloat(totalPrices).toFixed(2));
        $("#real").text(parseFloat(realPrices).toFixed(2));
        $("#kind").text(parseInt(sessionStorage.getItem("kind")));
        $(this).parent().parent().prev().text($(this).next().val());
    }
});
//打印
$("#insert").click(function () {
    if (sessionStorage.getItem("kind") == "0") {
        swal({
            title: "请先输入书籍信息",
            text: "",
            buttonsStyling: false,
            confirmButtonClass: "btn btn-warning btnStyle",
            type: "warning"
        }).catch(swal.noop);
    } else {
        var table = $("#table").tableToJSON();
        var json = JSON.stringify(table);
        $.ajax({
            type: 'Post',
            url: 'retail.aspx?userId=',
            data: {
                json: json,
                op: 'insert'
            },
            dataType: 'text',
            success: function (succ) {
                var datas = succ.split("|:");
                var data = datas[0];
                if (data == "添加失败") {
                    swal({
                        title: "建议再次点击打印一次",
                        text: "",
                        buttonsStyling: false,
                        confirmButtonClass: "btn btn-warning",
                        type: "warning"
                    }).catch(swal.noop);
                } else {
                    $("#ticket").show();
                    var headId = datas[1];
                    sessionStorage.setItem("preRecord", headId);
                    $("#kindEnd").text(sessionStorage.getItem("kind"));
                    $("#numberEnd").text(sessionStorage.getItem("number"));
                    $("#totalEnd").text(parseFloat(sessionStorage.getItem("totalPrice")).toFixed(2));
                    $("#realEnd").text(parseFloat(sessionStorage.getItem("realPrice")).toFixed(2));
                    $("#discount").text((parseFloat(sessionStorage.getItem("realPrice")) / parseFloat(sessionStorage.getItem("totalPrice")) * 100).toFixed(2))
                    $("#timeEnd").text(endTime())
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

                    //一维码
                    //JsBarcode("#img", headId, {
                    //    displayValue: false, //是否在条形码下方显示文字
                    //    //fontSize: 15,//设置文本的大小
                    //    margin: 0,//设置条形码周围的空白边距
                    //    width: 10,//设置条之间的宽度
                    //    height: 50,//高度
                    //});
                    try {
                        var status = "";
                        var LODOP = getLodop();
                        //LODOP.SET_LICENSES("", "3C5743518A25D4EEFBB1CCB8C6FF9A49", "C94CEE276DB2187AE6B65D56B3FC2848", "");
                        //LODOP.ADD_PRINT_HTM(0, 50, 900, 850, document.getElementById("ticket").innerHTML);
                        //LODOP.SET_PRINTER_INDEX("BTP-U60(U) 1");
                        //LODOP.SET_PRINT_PAGESIZE(1, 700, 900, "");
                        //LODOP.PREVIEW();
                        LODOP.SET_PRINT_MODE("CATCH_PRINT_STATUS", true);
                        LODOP.On_Return = function (TaskID, Value) {
                            status = Value;
                        };
                        if (status != "" || status != null) {
                            LODOP.ADD_PRINT_HTM(0, 50, 900, 500, document.getElementById("ticket").innerHTML);
                            LODOP.SET_PRINTER_INDEX("BTP-U60(U) 1");
                            LODOP.SET_PRINT_PAGESIZE(1, 700, 900, "");
                            //LODOP.PREVIEW();
                            LODOP.PRINT();
                            $("#preRecord").text(sessionStorage.getItem("preRecord"));
                            window.location.reload();
                            sessionStorage.removeItem("kind");
                            sessionStorage.removeItem("number");
                            sessionStorage.removeItem("totalPrice");
                            sessionStorage.removeItem("realPrice");
                        }
                    } catch (err) {
                        window.location.href = "/CLodop_Setup_for_Win32NT.html";
                    }
                }
            }
        })
    }
})
//补打上一条记录单据
$("#preRecord").click(function () {
    var headId = $("#preRecord").text().trim();
    $.ajax({
        type: 'Post',
        url: 'retail.aspx?userId=',
        data: {
            headId: headId,
            op: 'preRecord'
        },
        dataType: 'text',
        success: function (succ) {
            var data = succ.split(":|");
            if (data == "无记录") {
                swal({
                    title: "此前未最新记录",
                    text: "无上一条记录",
                    buttonsStyling: false,
                    confirmButtonClass: "btn btn-warning",
                    type: "warning"
                }).catch(swal.noop);
            } else {
                var kinds = data[0];
                var number = data[1];
                var total = data[2];
                var real = data[3];
                $("#ticket").show();
                $("#kindEnd").text(kinds);
                $("#numberEnd").text(number);
                $("#totalEnd").text(total);
                $("#realEnd").text(real);
                $("#timeEnd").text(endTime())
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

                //一维码
                //JsBarcode("#img", headId, {
                //    displayValue: false, //是否在条形码下方显示文字
                //    //fontSize: 15,//设置文本的大小
                //    margin: 0,//设置条形码周围的空白边距
                //    width: 10,//设置条之间的宽度
                //    height: 50,//高度
                //});
                var status = "";
                var LODOP = getLodop();
                //LODOP.ADD_PRINT_HTM(0, 50, 900, 850, document.getElementById("ticket").innerHTML);
                //LODOP.SET_PRINTER_INDEX("BTP-U60(U) 1");
                //LODOP.SET_PRINT_PAGESIZE(1, 700, 900, "");
                //LODOP.PREVIEW();
                LODOP.SET_PRINT_MODE("CATCH_PRINT_STATUS", true);
                LODOP.On_Return = function (TaskID, Value) {
                    status = Value;
                };
                if (status != "" || status != null) {
                    LODOP.ADD_PRINT_HTM(0, 50, 900, 500, document.getElementById("ticket").innerHTML);
                    LODOP.SET_PRINTER_INDEX("BTP-U60(U) 1");
                    LODOP.SET_PRINT_PAGESIZE(1, 700, 550, "");
                    //LODOP.PREVIEW();
                    LODOP.PRINT();
                    window.location.reload();
                }
            }
        }
    })
})
//删除
$("#table").delegate(".btn-delete", "click", function () {
    var bookNum = $(this).parent().prev().text().trim();
    var totalPrice = $(this).parent().prev().prev().prev().text().trim();
    var realPrice = $(this).parent().prev().prev().text().trim();
    var number = $(this).parent().prev().prev().prev().prev().prev().prev().text().trim();
    $(this).parent().parent().remove();
    $.ajax({
        type: 'Post',
        url: 'retail.aspx?userId=',
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
            sessionStorage.setItem("number", parseFloat(sessionStorage.getItem("number") - parseInt(number)).toFixed(2));
            sessionStorage.setItem("totalPrice", parseFloat(sessionStorage.getItem("totalPrice") - parseFloat(totalPrice)).toFixed(2));
            sessionStorage.setItem("realPrice", parseInt(sessionStorage.getItem("realPrice") - parseFloat(realPrice)));
            //展示合计内容
            $("#number").text(sessionStorage.getItem("number"));
            $("#total").text(parseFloat(sessionStorage.getItem("totalPrice")).toFixed(2));
            $("#real").text(parseFloat(sessionStorage.getItem("realPrice")).toFixed(2));
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
        confirmButtonClass: 'btn btn-success btnStyle',
        cancelButtonClass: 'btn btn-danger btnStyle',
        buttonsStyling: false,
        allowOutsideClick: false    //用户无法通过点击弹窗外部关闭弹窗
    }).then(function () {
        window.location.reload();
    })
})
